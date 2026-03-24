<%@ Page Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="ChaiQianUnitLimit.aspx.cs" Inherits="ZYRYJG.StAnManage.ChaiQianUnitLimit" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../PostSelect.ascx" TagName="PostSelect" TagPrefix="uc1" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadWindowManager runat="server" RestrictionZoneID="offsetElement" ID="RadWindowManager1" Skin="Windows7"
        EnableShadow="true" EnableEmbeddedScripts="true">
    </telerik:RadWindowManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" EnableAJAX="true">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadGrid1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ButtonSearch">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="PostSelect1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="PostSelect1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Windows7" />
    <div class="div_out">
        <div class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt; 统计分析 &gt;&gt;
                综合查询 &gt;&gt; <strong>拆迁企业资质查询</strong>
            </div>
        </div>
        <div class="content">
            <table class="bar_cx">
                <tr>
                    <td align="right" nowrap="nowrap" style="width: 120px;">企业名称：
                    </td>
                    <td align="left" style="width: 260px;">
                        <telerik:RadTextBox ID="rdtxtQYMC" runat="server" Width="250px" Skin="Default" >
                        </telerik:RadTextBox>
                    </td>
                    <td align="right" nowrap="nowrap" style="width: 120px;">组织机构代码：
                    </td>
                    <td align="left" style="width: 190px;">
                        <telerik:RadTextBox ID="RadTextBoxUNITCODE" runat="server" Width="180px" Skin="Default"
                            >
                        </telerik:RadTextBox>
                    </td>
                    <td align="left">
                        <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
                    </td>
                </tr>
            </table>
            <div class="table_cx">
                <img src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                拆迁企业列表
            </div>
            <div style="width: 98%; margin: 0 auto;">
                <telerik:RadGrid ID="RadGrid1" AutoGenerateColumns="False" runat="server" AllowAutomaticDeletes="True"
                    AllowCustomPaging="true" AllowPaging="True" PageSize="10" AllowSorting="True"
                    SortingSettings-SortToolTip="单击进行排序" Skin="Blue" EnableAjaxSkinRendering="false"
                    EnableEmbeddedSkins="false" Width="100%" GridLines="None" OnPageIndexChanged="RadGrid1_PageIndexChanged">
                    <ClientSettings EnableRowHoverStyle="true">
                    </ClientSettings>
                    <MasterTableView AllowMultiColumnSorting="True" NoMasterRecordsText="　没有可显示的记录">
                        <Columns>
                            <telerik:GridBoundColumn UniqueName="GSMC" DataField="GSMC" HeaderText="企业名称">
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="ZZJGDM" DataField="ZZJGDM" HeaderText="组织机构代码">
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="ZZZS" DataField="ZZZS" HeaderText="资质证书">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="ZZDJ" DataField="ZZDJ" HeaderText="资质等级">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="ZDRSYQ" DataField="ZDRSYQ" HeaderText="最低人数要求">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                        </Columns>
                        <PagerTemplate>
                            <uc2:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                        </PagerTemplate>
                    </MasterTableView>
                    <SortingSettings SortToolTip="单击进行排序"></SortingSettings>
                    <StatusBarSettings LoadingText="正在读取数据" ReadyText="完成" />
                </telerik:RadGrid>
                <br />
            </div>
            <br />
        </div>
    </div>
</asp:Content>
