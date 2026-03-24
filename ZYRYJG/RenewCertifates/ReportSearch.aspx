<%@ Page Title="续期初审汇总上报单选择" Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="ReportSearch.aspx.cs" Inherits="ZYRYJG.RenewCertifates.ReportSearch" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../PostSelect.ascx" TagName="PostSelect" TagPrefix="uc1" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript">
            function returnToParent(reportCode) {

                var oArg = new Object();
                oArg.ReportCode = reportCode;
                var oWnd = GetRadWindow();
                oWnd.close(oArg);
            }
            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow) oWindow = window.radWindow;
                else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
                return oWindow;
            }

        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManager ID="RadAjaxManagerExamPlanSearch" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanelExamPlanSearch"
        EnableAJAX="true">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ButtonSearch">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGridReport" />
                </UpdatedControls>
            </telerik:AjaxSetting>

        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanelExamPlanSearch" runat="server"
        Visible="true" Skin="Windows7" />
    <style type="text/css">
        a {
            margin: 2px;
            text-decoration: none !important;
        }
    </style>
    <div class="content">
        <table class="bar_cx">
            <tr>
                <td width="8%" align="right" nowrap="nowrap">复合状态：
                </td>
                <td align="left" width="25%">
                    <asp:RadioButtonList ID="RadioButtonCheckStatus" runat="server" RepeatDirection="Horizontal"
                        AutoPostBack="false" Width="300px">
                        <asp:ListItem Value="">全部</asp:ListItem>
                        <asp:ListItem Value="未复合" Selected="True">未复合</asp:ListItem>
                        <asp:ListItem Value="已复合">已复合</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td width="8%" align="right" nowrap="nowrap">岗位工种：
                </td>
                <td align="left" colspan="3">
                    <uc1:PostSelect ID="PostSelect1" runat="server" />
                </td>
            </tr>
            <tr>
                <td width="8%" align="right" nowrap="nowrap">汇总单位：
                </td>
                <td align="left" width="25%">
                    <telerik:RadComboBox ID="RadComboBoxReportMan" runat="server" Width="90%">
                        <Items>
                        </Items>
                    </telerik:RadComboBox>
                </td>
                <td width="8%" align="right" nowrap="nowrap">汇总批次号：
                </td>
                <td align="left" width="25%">
                    <telerik:RadTextBox ID="RadTextBoxReportCode" runat="server" Width="90%" Skin="Default" MaxLength="30"
                        >
                    </telerik:RadTextBox>
                </td>
                <td width="8%" align="right" nowrap="nowrap">汇总日期：
                </td>
                <td align="left" width="25%">
                    <telerik:RadDatePicker ID="RadDatePickerReportDateStart" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                        Width="46%" Style="float: left;" />
                    <div class="RadPicker">至</div>
                    <telerik:RadDatePicker ID="RadDatePickerReportDateEnd" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                        Width="46%" Style="float: left;" />
                </td>
            </tr>
            <tr>
                <td colspan="6" align="center">
                    <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
                </td>
            </tr>
        </table>
        <div class="table_cx" style="line-height: 20px;">
            提示：请选择一个汇总。
        </div>
        <div style="width: 98%; margin: 0 auto;">

            <telerik:RadGrid ID="RadGridReport" runat="server" AllowCustomPaging="true" GridLines="None"
                AllowPaging="True" PageSize="10" AllowSorting="True" AutoGenerateColumns="False" 
                SortingSettings-SortToolTip="单击进行排序" Width="100%" Skin="Blue" EnableAjaxSkinRendering="false"
                EnableEmbeddedSkins="false" OnItemDataBound="RadGridReport_ItemDataBound" PagerStyle-AlwaysVisible="true">
                <ClientSettings EnableRowHoverStyle="true">
                </ClientSettings>
                <MasterTableView CommandItemDisplay="None" AllowMultiColumnSorting="True" DataKeyNames="ReportCode,PostTypeID,CertCount,FIRSTCHECKUNITNAME,FirstCheckStartDate,FirstCheckEndDate,ReportDate" NoMasterRecordsText="　没有可显示的记录">
                    <Columns>
                        <telerik:GridTemplateColumn HeaderText="">
                            <ItemTemplate>
                                <span class="link_edit" onclick="returnToParent('<%# Eval("ReportCode").ToString() %>')">选择</span>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Wrap="false" Font-Bold="true" />
                            <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                        </telerik:GridBoundColumn>

                        <telerik:GridTemplateColumn UniqueName="PostTypeName" HeaderText="岗位类别">
                            <ItemTemplate>
                                <%# PostName(Eval("PostTypeID").ToString()) %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                            <ItemStyle HorizontalAlign="Left" Wrap="false" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn UniqueName="CertCount" DataField="CertCount" HeaderText="证书数量">
                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="FIRSTCHECKUNITNAME" DataField="FIRSTCHECKUNITNAME" HeaderText="初审单位">
                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn UniqueName="FirstCheckStartDate" HeaderText="初审时间">
                            <ItemTemplate>
                                <%# Convert.ToDateTime(Eval("FirstCheckStartDate")).ToString("yyyy-MM-dd") == Convert.ToDateTime(Eval("FirstCheckEndDate")).ToString("yyyy-MM-dd")? Convert.ToDateTime(Eval("FirstCheckStartDate")).ToString("yyyy.MM.dd"):string.Format("{0} - {1}",Convert.ToDateTime(Eval("FirstCheckStartDate")).ToString("yyyy.MM.dd"),Convert.ToDateTime(Eval("FirstCheckEndDate")).ToString("yyyy.MM.dd")) %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                            <ItemStyle HorizontalAlign="Left" Wrap="false" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn UniqueName="ReportCode" DataField="ReportCode" HeaderText="汇总批次号">
                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="ReportDate" DataField="ReportDate" HtmlEncode="false"
                            DataFormatString="{0:yyyy-MM-dd}" HeaderText="汇总时间">
                            <HeaderStyle HorizontalAlign="Center" Wrap="true" />
                            <ItemStyle HorizontalAlign="Center" Wrap="true" />
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn UniqueName="CertificateChangeID" HeaderText="汇总扫描件">
                            <ItemTemplate>
                                <%#showFJ(Eval("ReportCode").ToString()) %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Wrap="false" Font-Bold="true" />
                            <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn UniqueName="ReportStatus" DataField="ReportStatus" HeaderText="上报状态">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="CheckStatus" DataField="CheckStatus" HeaderText="审核状态">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>

                    </Columns>
                    <HeaderStyle Font-Bold="True" />
                    <EditFormSettings>
                        <EditColumn UniqueName="EditCommandColumn1">
                        </EditColumn>
                    </EditFormSettings>
                    <PagerTemplate>
                        <uc2:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                    </PagerTemplate>
                </MasterTableView>
            </telerik:RadGrid>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetReportList"
                TypeName="DataAccess.CertificateContinueDAL" SelectCountMethod="SelectReportCount"
                EnablePaging="true" MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex"
                SortParameterName="orderBy">
                <SelectParameters>
                    <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                        DefaultValue="" ConvertEmptyStringToNull="false" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </div>
    </div>
    <br />
</asp:Content>
