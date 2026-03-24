<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/RadControls.Master" CodeBehind="CertifChangeAutoCheckTJ.aspx.cs" Inherits="ZYRYJG.StAnManage.CertifChangeAutoCheckTJ" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function onRequestStart(sender, args) {
                if (args.get_eventTarget().indexOf("ButtonOutput") >= 0) {
                    args.set_enableAjax(false);
                }
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <ClientEvents OnRequestStart="onRequestStart" />
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ButtonSearch">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ButtonSearch" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="ButtonOutput" />
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ButtonOutput">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ButtonOutput" />
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Windows7" />
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Windows7">
    </telerik:RadWindowManager>
    <div class="div_out">
        <div class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt; 统计分析 &gt;&gt;
                从业人员业务统计 &gt;&gt; <strong>证书变更社保符合审批统计</strong>
            </div>
        </div>
        <div class="content">

            <div class="DivTitleOn" onclick="DivOnOff(this,'Td3',event);" title="折叠">
                统计说明
            </div>
            <div class="DivContent" id="Td3">
                【证书变更社保符合审批统计】是按时间段统计“社保符合的证书变更申请单批量审批通过”数量。<br />
                &nbsp;&nbsp;输入查询时间段，如“自 2010-1-1 至 2010-12-31 ” 即可查询全年证书变化情况，不输时间表示查询所有数据。
            </div>
            <table class="bar_cx">
                <tr>
                    <td align="right" nowrap="nowrap" style="width: 100px;">统计时间范围：自
                    </td>
                    <td align="left" style="width: 320px;">
                        <telerik:RadDatePicker ID="RadDatePicker_StartDate" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                            Width="150px" />
                        <div class="RadPicker">至</div>
                        <telerik:RadDatePicker ID="RadDatePicker_EndDate" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                            Width="150px" />
                    </td>
                    <td align="right" nowrap="nowrap" style="width: 100px;">统计方式：
                    </td>
                    <td align="left" style="width: 130px;">

                        <asp:RadioButtonList ID="RadioButtonListTJType" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="年度" Value="年度" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="月份" Value="月份"></asp:ListItem>
                        </asp:RadioButtonList>


                    </td>
                    <td align="left" nowrap="nowrap">
                        <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
                    </td>
                </tr>
            </table>
            <div style="width: 98%; margin: 0 auto; padding-top: 8px; border: none;">
                <telerik:RadGrid ID="RadGrid1" AutoGenerateColumns="False" runat="server" Skin="Blue"
                    EnableAjaxSkinRendering="False" EnableEmbeddedSkins="False" Width="100%" GridLines="None"
                    BorderWidth="0" OnExcelExportCellFormatting="RadGrid1_ExcelExportCellFormatting">
                    <ClientSettings EnableRowHoverStyle="true">
                    </ClientSettings>
                    <HeaderContextMenu EnableEmbeddedSkins="False">
                    </HeaderContextMenu>
                    <MasterTableView CommandItemDisplay="None" NoMasterRecordsText="　没有可显示的记录" Width="100%">
                        <Columns>
                            <telerik:GridBoundColumn UniqueName="TJDate" DataField="TJDate" HeaderText="统计时间">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="PostTypeName" DataField="PostTypeName" HeaderText="岗位类别">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="SL" DataField="SL" HeaderText="审批数量">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridTemplateColumn>
                        </Columns>
                        <HeaderStyle Font-Bold="True" />
                    </MasterTableView>
                    <FilterMenu EnableEmbeddedSkins="False">
                    </FilterMenu>
                </telerik:RadGrid>
            </div>
            <div style="width: 90%; margin: 5px auto; text-align: center; padding-top: 8px;">
                <asp:Button ID="ButtonOutput" runat="server" Text="导出列表" CssClass="bt_large" OnClick="ButtonOutput_Click" />
            </div>
        </div>
    </div>
</asp:Content>
