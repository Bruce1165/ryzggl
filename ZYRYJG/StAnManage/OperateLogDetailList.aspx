<%@ Page Title="" Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="OperateLogDetailList.aspx.cs" Inherits="ZYRYJG.StAnManage.OperateLogDetailList" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function RowDblClick(sender, eventArgs) {
                sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" EnableAJAX="true">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadGridExamPlan">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGridExamPlan" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ButtonSearch">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGridExamPlan" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="PostSelect2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="PostSelect2" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true"
        Skin="Sunset" />
    <telerik:RadWindowManager runat="server" RestrictionZoneID="offsetElement" ID="RadWindowManager1"
        EnableShadow="true" EnableEmbeddedScripts="true">
    </telerik:RadWindowManager>
    <div class="div_out">
        <div class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt; 统计分析 &gt;&gt;
                统计报表 &gt;&gt; <strong>系统操作日志查询</strong>
            </div>
        </div>
         
        <div class="content">
             <p class="jbxxbt">
                    系统操作日志查询</p>
            <div id="Divquery" runat="server">
                <table class="bar_cx">
                    <tr>
                        <td align="right" width="10%" nowrap="nowrap">访问服务ID：
                        </td>
                        <td width="40%" align="left">
                            <telerik:RadTextBox ID="txtServerId" runat="server" Width="90%" Skin="Default">
                            </telerik:RadTextBox>
                        </td>
                        <td align="right" width="10%" nowrap="nowrap">调用的方法名称：
                        </td>
                        <td width="40%" align="left">
                            <telerik:RadTextBox ID="txtCallingMethodName" runat="server" Width="90%" Skin="Default">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap="nowrap">访问时间：
                        </td>
                        <td width="40%" align="left">
                            <telerik:RadDatePicker ID="RadDatePicker_AccessDateStartDate" MinDate="01/01/1900" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                runat="server" Width="44%" />
                            <div class="RadPicker">至</div>
                            <telerik:RadDatePicker ID="RadDatePicker_AccessDateEndDate" MinDate="01/01/1900" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                runat="server" Width="44%" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
                            &nbsp;&nbsp;&nbsp;
                                        <input id="Button1" type="button" value="返 回" class="button" onclick="javascript: location.href = 'OperateLogList.aspx';" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="table_cx">
                <img alt="" src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                系统操作日志列表
            </div>
            <div style="width: 98%; margin: 0 auto;">
                <telerik:RadGrid ID="RadGridInterFaceLogDetail"
                    AutoGenerateColumns="False" runat="server" AllowAutomaticDeletes="True" AllowPaging="True"
                    PageSize="10" AllowSorting="True" SortingSettings-SortToolTip="单击进行排序" Skin="Blue"
                    EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false" Width="100%" GridLines="None">
                    <ClientSettings EnableRowHoverStyle="true">
                    </ClientSettings>
                    <HeaderContextMenu EnableEmbeddedSkins="False">
                    </HeaderContextMenu>
                    <MasterTableView CommandItemDisplay="None" DataKeyNames="ID" NoMasterRecordsText="　没有可显示的记录">
                        <Columns>
                            <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="ACCESSDATE" DataField="ACCESSDATE" HeaderText="访问日期">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="ACCESSTIME" DataField="ACCESSTIME" HeaderText="访问时间">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="SERVERID" DataField="SERVERID" HeaderText="访问服务ID">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="CALLINGMETHODNAME" DataField="CALLINGMETHODNAME"
                                HeaderText="调用的方法名称">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="PARAMETERDATA" DataField="PARAMETERDATA" HeaderText="调用方法所输入的参数">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="METHODDESCRIPTION" DataField="METHODDESCRIPTION"
                                HeaderText="描述信息">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                        </Columns>
                        <EditFormSettings>
                            <EditColumn InsertImageUrl="Update.gif" UpdateImageUrl="Update.gif" EditImageUrl="Edit.gif"
                                CancelImageUrl="Cancel.gif">
                            </EditColumn>
                        </EditFormSettings>
                        <HeaderStyle Font-Bold="True" />
                        <PagerTemplate>
                            <uc2:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                        </PagerTemplate>
                    </MasterTableView>
                    <FilterMenu EnableEmbeddedSkins="False">
                    </FilterMenu>
                    <SortingSettings SortToolTip="单击进行排序"></SortingSettings>
                    <StatusBarSettings LoadingText="正在读取数据" ReadyText="完成" />
                </telerik:RadGrid>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="DataAccess.InterFaceLogDAL"
                    SelectMethod="GetDetailList" EnablePaging="true" SelectCountMethod="SelectDetailPageCount"
                    MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex"
                    SortParameterName="orderBy">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                            DefaultValue="" ConvertEmptyStringToNull="false" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </div>
        </div>
        <br />
    </div>
</asp:Content>
