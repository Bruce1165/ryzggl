<%@ Page Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="FRList.aspx.cs" Inherits="ZYRYJG.PersonnelFile.FRList" %>

<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" EnableAJAX="true">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ButtonSearch">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadGrid1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Windows7" />
    <div class="div_out">
        <div class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt;综合查询 &gt;&gt;企业信息查询&gt;&gt;<strong>企业法人查询</strong>
            </div>
        </div>
        <div class="content">
            <table class="bar_cx">
                 <tr id="TrPerson" runat="server">
                            <td width="12%" align="right" nowrap="nowrap">机构代码/社会统一代码：
                            </td>
                            <td align="left" width="38%">
                                <telerik:RadTextBox ID="RadTextBoxENT_OrganizationsCode" runat="server" Width="97%" Skin="Default">
                                </telerik:RadTextBox>
                            </td>
                            <td width="12%" align="right" nowrap="nowrap">企业名称：
                            </td>
                            <td align="left" >
                                <telerik:RadTextBox ID="RadTextBoxENT_Name" runat="server" Width="97%" Skin="Default">
                                </telerik:RadTextBox>
                            </td>
                          
                     
                     </tr>
                <tr>
                    <td width="12%" align="right" nowrap="nowrap">法人姓名：
                    </td>
                    <td width="38%" nowrap="nowrap">
                        <telerik:RadTextBox ID="rdtxtWorkerName" runat="server" Width="97%" >
                        </telerik:RadTextBox>
                    </td>
                    <td width="12%" align="right" nowrap="nowrap">证件号码：
                    </td>
                    <td nowrap="nowrap">
                        <telerik:RadTextBox ID="rdtxtZJHM" runat="server" Width="97%" >
                        </telerik:RadTextBox>
                    </td>
                </tr>              
                <tr>
                    <td colspan="4" align="center">
                        <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
                    </td>
                </tr>
            </table>
            <div class="table_cx">
                <img src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                企业法人信息列表<span style="color:red">（已停止更新，请改为查询工商信息中的法人信息）</span>
            </div>
            <div style="width: 98%; margin: 0 auto;">
                <telerik:RadGrid ID="RadGrid1" AutoGenerateColumns="False"
                    runat="server" AllowPaging="True" PageSize="10" AllowSorting="True" SortingSettings-SortToolTip="单击进行排序"
                    Skin="Blue" EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false" Width="100%"
                    GridLines="None">
                    <ClientSettings EnableRowHoverStyle="true">
                    </ClientSettings>
                    <MasterTableView CommandItemDisplay="None" AllowMultiColumnSorting="True" 
                        NoMasterRecordsText="　没有可显示的记录">
                        <Columns>
                              <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                <HeaderStyle HorizontalAlign="Center" Wrap="true" />
                                <ItemStyle HorizontalAlign="Center" Wrap="true" />
                            </telerik:GridBoundColumn>
                               <telerik:GridBoundColumn UniqueName="qymc" DataField="qymc" HeaderText="企业名称">
                                <HeaderStyle HorizontalAlign="Left" Wrap="true" />
                                <ItemStyle HorizontalAlign="Left" Wrap="true" />
                            </telerik:GridBoundColumn>
                         <telerik:GridBoundColumn UniqueName="zzjgdm" DataField="zzjgdm" HeaderText="组织机构代码">
                                <HeaderStyle HorizontalAlign="Left" Wrap="true" />
                                <ItemStyle HorizontalAlign="Left" Wrap="true" />
                            </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn UniqueName="tyshxydm" DataField="tyshxydm" HeaderText="社会统一信用代码">
                                <HeaderStyle HorizontalAlign="Left" Wrap="true" />
                                <ItemStyle HorizontalAlign="Left" Wrap="true" />
                            </telerik:GridBoundColumn>
                          
                            <telerik:GridBoundColumn UniqueName="fddbr" DataField="fddbr" HeaderText="法人姓名">
                                <HeaderStyle HorizontalAlign="Center" Wrap="true" />
                                <ItemStyle HorizontalAlign="Center" Wrap="true" />
                            </telerik:GridBoundColumn>
                           
                            <telerik:GridBoundColumn UniqueName="zjhm" DataField="zjhm"
                                HeaderText="证件号码">
                                <HeaderStyle HorizontalAlign="Center" Wrap="true" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                         
                            <telerik:GridBoundColumn UniqueName="dataSource" DataField="dataSource" HeaderText="数据来源">
                                <HeaderStyle HorizontalAlign="Center" Wrap="true" />
                                <ItemStyle HorizontalAlign="Center" Wrap="true" />
                            </telerik:GridBoundColumn>
                           
                        </Columns>
                        <PagerTemplate>
                            <uc2:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                        </PagerTemplate>
                    </MasterTableView>
                    <SortingSettings SortToolTip="单击进行排序"></SortingSettings>
                    <StatusBarSettings LoadingText="正在读取数据" ReadyText="完成" />
                </telerik:RadGrid>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="DataAccess.View_FRDAL"
                    DataObjectTypeName="Model.WorkerOB" SelectMethod="GetList" EnablePaging="true"
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
    </div>
</asp:Content>
