<%@ Page Title="" Language="C#" MasterPageFile="~/RadControls.master" AutoEventWireup="true" CodeFile="FinishList.aspx.cs" Inherits="Student_FinishList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc1" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadComboBoxPostType">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGridFinish" LoadingPanelID="RadAjaxLoadingPanel1" />
                     <telerik:AjaxUpdatedControl ControlID="RadGridSource" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadGridSource">
                <UpdatedControls>
                     <telerik:AjaxUpdatedControl ControlID="RadGridSource" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default" />
    <div class="div_main" style="padding: 8px 8px;">
        <div id="div_top" class="div_mainTop">
            <div class="div_road">
            </div>
        </div>
        <div class="div_fun">
            学习成果
        </div>
        <div class="content" style="text-align: center;">
            <div runat="server" id="divCX" class="cx" style="width: 98%; margin-bottom: 20px; clear: both;">
                <b>筛选专业：</b>&nbsp;<telerik:RadComboBox ID="RadComboBoxPostType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RadComboBoxPostType_SelectedIndexChanged">
                    <Items></Items>
                </telerik:RadComboBox>
            </div>
            <telerik:RadGrid ID="RadGridFinish" runat="server" AutoGenerateColumns="False" PageSize="10"
                EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false" Skin="Blue" MasterTableView-HorizontalAlign="Center">
                <ClientSettings EnableRowHoverStyle="false">
                </ClientSettings>
                <MasterTableView CommandItemDisplay="None" NoMasterRecordsText="　没有可展示的学习成果">
                    <Columns>
                        <telerik:GridBoundColumn HeaderText="年度" UniqueName="ND" DataField="ND">
                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="完成听课学时" UniqueName="FinishPeriod" DataField="FinishPeriod">
                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="测试通过学时" UniqueName="TestPeriod" DataField="TestPeriod">
                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>

                    </Columns>
                    <HeaderStyle Font-Bold="true" />
                </MasterTableView>
            </telerik:RadGrid>
            <div class="table_cx" style="margin-top:20px">
                <b>学习历史</b> <span style="color:darkred">（历史记录可以自行删除，删除后可以重新学习，按新年度记录学时。）</span>
            </div>
            <telerik:RadGrid ID="RadGridSource" AutoGenerateColumns="False"
                runat="server" AllowPaging="True" PageSize="10" AllowSorting="True" SortingSettings-SortToolTip="单击进行排序"
                Skin="Blue" EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false" Width="100%" PagerStyle-AlwaysVisible="true"
                GridLines="None" OnDeleteCommand="RadGridSource_DeleteCommand" >
                <ClientSettings EnableRowHoverStyle="true">
                </ClientSettings>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="SourceID,SourceName"
                    NoMasterRecordsText="　没有可显示的记录">
                    <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                    <Columns>
                        <telerik:GridBoundColumn HeaderText="学习时间" UniqueName="LearnTime" DataField="LearnTime"
                                    HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn HeaderText="课程名称" UniqueName="SourceName" DataField="SourceName">
                            <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="课件时长" UniqueName="Period">
                            <ItemTemplate>
                                <%# Convert.ToInt32(Eval("Period")) / 60 == 0 ? "" : string.Format("{0}小时", Convert.ToString(Convert.ToInt32(Eval("Period")) / 60))%><%# string.Format("{0}分", Convert.ToString(Convert.ToInt32(Eval("Period")) % 60))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Right" Wrap="false" />
                            <ItemStyle HorizontalAlign="Right" Wrap="false" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="学习比例" UniqueName="FinishPeriod">
                            <ItemTemplate>
                                <%# Eval("FinishPeriod")== DBNull.Value?0: Convert.ToInt32(Eval("FinishPeriod")) *10 /Convert.ToInt32(Eval("Period")) /6 %>%
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Right" Wrap="false" />
                            <ItemStyle HorizontalAlign="Right" Wrap="false" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="测试状态" UniqueName="FinishPeriod">
                            <ItemTemplate>
                                <%# Eval("StudyStatus")!= DBNull.Value && Convert.ToInt32(Eval("StudyStatus"))==1?"已测试":"" %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Right" Wrap="false" />
                            <ItemStyle HorizontalAlign="Right" Wrap="false" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderText="适用专业" UniqueName="PostTypeName" DataField="PostTypeName">
                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                        </telerik:GridBoundColumn>
                      <telerik:GridButtonColumn UniqueName="Delete" ButtonType="LinkButton" CommandName="Delete"
                                            ConfirmText="确定要删除吗？" ConfirmDialogType="RadWindow" Text="删除">
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                                        </telerik:GridButtonColumn>
                    </Columns>
                    <HeaderStyle Font-Bold="True" />
                    <PagerTemplate>
                        <uc2:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                    </PagerTemplate>
                    <ItemStyle Height="16px" />
                    <AlternatingItemStyle Height="16px" />
                </MasterTableView>
            </telerik:RadGrid>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="DataAccess.FinishSourceWareDAL"
                DataObjectTypeName="Model.FinishSourceWareOB" SelectMethod="GetListView" EnablePaging="true"
                SelectCountMethod="SelectCountView" MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex"
                SortParameterName="orderBy">
                <SelectParameters>
                    <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                        DefaultValue="" ConvertEmptyStringToNull="false" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </div>

    </div>
</asp:Content>

