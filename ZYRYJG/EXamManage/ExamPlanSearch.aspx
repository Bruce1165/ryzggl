<%@ Page Title="考试计划选择" Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="ExamPlanSearch.aspx.cs" Inherits="ZYRYJG.EXamManage.ExamPlanSearch" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../PostSelect.ascx" TagName="PostSelect" TagPrefix="uc1" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function returnToParent(examPlanID, examPlanName, PostTypeID, PostID, PostTypeName, PostName) {
                var oArg = new Object();
                oArg.ExamPlanID = examPlanID;
                oArg.ExamPlanName = examPlanName;
                oArg.PostTypeID = PostTypeID;
                oArg.PostID = PostID;
                oArg.PostTypeName = PostTypeName;
                oArg.PostName = PostName;
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
                    <telerik:AjaxUpdatedControl ControlID="RadGridExamPlan" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadGridExamPlan">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGridExamPlan" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="PostSelect2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="PostSelect2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanelExamPlanSearch" runat="server"
        Visible="true" Skin="Windows7" />
    <div class="content">
        <table class="bar_cx">
            <tr>
                <td align="right" width="11%" nowrap="nowrap">考试计划名称：
                </td>
                <td width="39%" align="left">
                    <telerik:RadTextBox ID="RadTextBoxExam_PlanName" runat="server" Width="95%" Skin="Default">
                    </telerik:RadTextBox>
                </td>
                <td width="11%" align="right" nowrap="nowrap">岗位工种：
                </td>
                <td width="39%" align="left">
                    <uc1:PostSelect ID="PostSelect2" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="right" nowrap="nowrap">考试时间：
                </td>
                <td align="left" colspan="3">
                    <telerik:RadComboBox ID="RadComboBoxYear" runat="server" Skin="Office2007" CausesValidation="False"
                        Width="60px" ExpandAnimation-Duration="0">
                    </telerik:RadComboBox>
                    &nbsp;年&nbsp;
                                <telerik:RadComboBox ID="RadComboBoxMonth" runat="server" Skin="Office2007" CausesValidation="False"
                                    Width="60px" ExpandAnimation-Duration="0">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="全部" Value="" />
                                        <telerik:RadComboBoxItem Text="1" Value="1" />
                                        <telerik:RadComboBoxItem Text="2" Value="2" />
                                        <telerik:RadComboBoxItem Text="3" Value="3" />
                                        <telerik:RadComboBoxItem Text="4" Value="4" />
                                        <telerik:RadComboBoxItem Text="5" Value="5" />
                                        <telerik:RadComboBoxItem Text="6" Value="6" />
                                        <telerik:RadComboBoxItem Text="7" Value="7" />
                                        <telerik:RadComboBoxItem Text="8" Value="8" />
                                        <telerik:RadComboBoxItem Text="9" Value="9" />
                                        <telerik:RadComboBoxItem Text="10" Value="10" />
                                        <telerik:RadComboBoxItem Text="11" Value="11" />
                                        <telerik:RadComboBoxItem Text="12" Value="12" />
                                    </Items>
                                </telerik:RadComboBox>
                    &nbsp;月&nbsp;&nbsp; （<asp:RadioButtonList ID="RadioButtonListStatus" runat="server"
                        RepeatDirection="Horizontal" AutoPostBack="false" RepeatLayout="Flow">
                        <asp:ListItem Value="0" Selected="True">全部</asp:ListItem>
                        <asp:ListItem Value="1">未考试</asp:ListItem>
                        <asp:ListItem Value="2">已考试</asp:ListItem>
                    </asp:RadioButtonList>
                    ） &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="ButtonSearch" runat="server"
                        Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
                </td>
            </tr>
        </table>
        <div class="table_cx" style="padding-top: 2px;">
            提示：单击一行选择一个考试计划。
        </div>
        <div style="width: 98%; margin: 0 auto;">
            <telerik:RadGrid ID="RadGridExamPlan" AutoGenerateColumns="False"
                runat="server" AllowPaging="True" PageSize="10" AllowSorting="True" SortingSettings-SortToolTip="单击进行排序"
                Skin="Blue" EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false" Width="100%"  PagerStyle-AlwaysVisible="true"
                GridLines="None" OnItemDataBound="RadGridExamPlan_ItemDataBound">
                <ClientSettings EnableRowHoverStyle="true">
                </ClientSettings>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="ExamPlanID,ExamPlanName,PostTypeID,PostID,PostTypeName,PostName"
                    NoMasterRecordsText="　没有可显示的记录">
                    <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                    <Columns>
                        <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                            <ItemStyle HorizontalAlign="Center" Width="26px" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="ExamPlanName" DataField="ExamPlanName" HeaderText="考试计划名称">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left"  />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="PostTypeID" DataField="PostTypeName" HeaderText="岗位类别">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="PostID" DataField="PostName" HeaderText="岗位工种">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="考试时间">
                            <ItemTemplate>
                                <%# Convert.ToDateTime(Eval("ExamStartDate")).ToString("yyyy.MM.dd") + "-" + Convert.ToDateTime(Eval("ExamEndDate")).ToString("yyyy.MM.dd")%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                        </telerik:GridTemplateColumn>
                    </Columns>
                    <HeaderStyle Font-Bold="True" />
                    <PagerTemplate>
                        <uc2:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                    </PagerTemplate>
                    <ItemStyle Height="16px" />
                    <AlternatingItemStyle Height="16px" />
                </MasterTableView>
            </telerik:RadGrid>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="DataAccess.ExamPlanDAL"
                DataObjectTypeName="Model.ExamPlanOB" SelectMethod="GetList" EnablePaging="true"
                SelectCountMethod="SelectCount" MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex"
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
