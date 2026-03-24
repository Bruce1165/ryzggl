<%@ Page Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="CertifChangeStatusList.aspx.cs" Inherits="ZYRYJG.CertifManage.CertifChangeStatusList1" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<%@ Register Src="../PostSelect.ascx" TagName="PostSelect" TagPrefix="uc1" %>
<%@ Register Src="../CheckAll.ascx" TagName="CheckAll" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    </telerik:RadCodeBlock>
    <telerik:RadWindowManager runat="server" RestrictionZoneID="offsetElement" ID="RadWindowManager1"
        EnableShadow="true" EnableEmbeddedScripts="true" Skin="Windows7" VisibleStatusbar="false">
    </telerik:RadWindowManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" EnableAJAX="true">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadGrid1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearch">
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
                当前位置 &gt;&gt; 证书管理 &gt;&gt;
                三类人员 &gt;&gt; <strong>证书变更状态</strong>
            </div>
        </div>
        <table class="bar_cx" runat="server" id="DivSearch">
            <tr>
                <td align="right" nowrap="nowrap" width="11%">姓名：
                </td>
                <td align="left" width="39%">
                    <telerik:RadTextBox ID="rdtxtWorkerName" runat="server" Width="97%" Skin="Default">
                    </telerik:RadTextBox>
                </td>
                <td align="right" width="11%" nowrap="nowrap">岗位工种：
                </td>
                <td align="left" width="39%">
                    <uc1:PostSelect ID="PostSelect1" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="right" nowrap="nowrap">证件号码：
                </td>
                <td align="left">
                    <telerik:RadTextBox ID="rdtxtZJHM" runat="server" Width="97%" Skin="Default">
                    </telerik:RadTextBox>
                </td>
                <td width="11%" align="right" nowrap="nowrap">证书编号：
                </td>
                <td align="left">
                    <telerik:RadTextBox ID="rdtxtCertificateCode" runat="server" Width="95%" Skin="Default">
                    </telerik:RadTextBox>
                </td>
            </tr>
            <tr>
                <td align="right" nowrap="nowrap">企业名称：
                </td>
                <td align="left">
                    <telerik:RadTextBox ID="rdtxtQYMC" runat="server" Width="97%" Skin="Default">
                    </telerik:RadTextBox>
                </td>
                <td align="right" nowrap="nowrap">证书有效期：
                </td>
                <td align="left">
                    <telerik:RadDatePicker ID="txtValidStartDate" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                        Width="46%" />
                    <div class="RadPicker">至</div>
                    <telerik:RadDatePicker ID="txtValidEndtDate" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                        Width="46%" />
                </td>
            </tr>
            <tr>
                <td align="right" nowrap="nowrap" width="11%">变更状态：
                </td>
                <td align="left" colspan="3">
                    <asp:RadioButtonList ID="rbl" runat="server" RepeatDirection="Horizontal" AutoPostBack="false">
                        <asp:ListItem Value="0">全部</asp:ListItem>
                        <asp:ListItem Value="1">已申请</asp:ListItem>
                        <asp:ListItem Value="2">已受理</asp:ListItem>
                        <asp:ListItem Value="3">已审核</asp:ListItem>
                        <asp:ListItem Value="4">已决定</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td colspan="4" align="center">
                    <asp:Button ID="btnSearch" runat="server" Text="查 询" CssClass="button" OnClick="btnSearch_Click" />
                </td>
            </tr>
        </table>
        <p class="table_cx">
            <img alt="" src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
            证书管理告知列表
        </p>
        <div style="width: 98%; margin: 0 auto;">
            <telerik:RadGrid ID="RadGrid1" AllowPaging="True"
                SortingSettings-SortToolTip="单击进行排序" runat="server" AutoGenerateColumns="False"  PagerStyle-AlwaysVisible="true"
                AllowSorting="True" PageSize="10" GridLines="None" CellPadding="0"
                Width="100%" Skin="Blue" EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false"
                OnDataBound="RadGrid1_DataBound">
                <ClientSettings EnableRowHoverStyle="true">
                </ClientSettings>
                <MasterTableView DataKeyNames="CertificateChangeID" NoMasterRecordsText=" 没有可显示的记录">
                    <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                    <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                    <Columns>
                        <telerik:GridBoundColumn HeaderText="序号" UniqueName="RowNum" DataField="RowNum" AllowSorting="false">
                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="岗位类别" UniqueName="PostTypeID" DataField="PostTypeID">
                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="岗位工种" UniqueName="PostID" DataField="PostID">
                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="CertificateCode" DataField="certificateCode"
                            HeaderText="证书编号">
                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="有效期">
                            <ItemTemplate>
                                <nobr><%# Convert.ToDateTime(Eval("ValidStartDate")).ToString("yyyy.MM.dd") + "-"%></nobr>
                                <nobr><%#Convert.ToDateTime(Eval("ValidEndDate")).ToString("yyyy.MM.dd")%></nobr>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn UniqueName="WorkerName" DataField="WorkerName" HeaderText="姓名">
                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="WorkerCertificateCode" DataField="WorkerCertificateCode"
                            HeaderText="证件号码">
                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="NewUnitName" DataField="NewUnitName" HeaderText="企业名称">
                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                    </Columns>
                    <HeaderStyle Font-Bold="True" />
                    <PagerTemplate>
                        <uc2:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                    </PagerTemplate>
                </MasterTableView>
                <SortingSettings SortToolTip="单击进行排序"></SortingSettings>
                <StatusBarSettings LoadingText="正在读取数据" ReadyText="完成" />
            </telerik:RadGrid>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="DataAccess.CertificateChangeDAL"
                DataObjectTypeName="Model.CertificateChangeOB" SelectMethod="GetList" EnablePaging="true"
                SelectCountMethod="SelectCount" MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex"
                SortParameterName="orderBy">
                <SelectParameters>
                    <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                        DefaultValue="" ConvertEmptyStringToNull="false" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </div>
        <div style="clear: both">
        </div>
        <br />
    </div>
</asp:Content>
