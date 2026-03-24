<%@ Page Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="ZCJZSList.aspx.cs" Inherits="ZYRYJG.StAnManage.ZCJZSList" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../PostSelect.ascx" TagName="PostSelect" TagPrefix="uc1" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadWindowManager runat="server" RestrictionZoneID="offsetElement" ID="RadWindowManager1"
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
                综合查询 &gt;&gt; <strong>注册建造师查询</strong>
            </div>
        </div>
        <div class="DivTitleOn" onclick="DivOnOff(this,'Td3',event);" title="折叠">
            查询说明
        </div>
        <div class="DivContent" id="Td3">
            本注册建造师数据从住建委基础数据库获取，基础数据库数据来源于建筑部同步传输，之间数据每日同步存在时间差一天。
        </div>
        <div class="content">
            <table class="bar_cx">
                <tr>
                    <td align="right" width="11%" nowrap="nowrap">企业名称：
                    </td>
                    <td width="42%" align="left">
                        <telerik:RadTextBox ID="rdtxtQYMC" runat="server" Width="97%" Skin="Default" 
                            MaxLength="25">
                        </telerik:RadTextBox>
                    </td>
                    <td width="11%" align="right" nowrap="nowrap">组织机构代码：
                    </td>
                    <td width="36%" align="left">
                        <telerik:RadTextBox ID="RadTextBoxUNITCODE" runat="server" Width="97%" Skin="Default"
                             MaxLength="25">
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" width="11%" nowrap="nowrap">姓 名：
                    </td>
                    <td width="42%" align="left">
                        <telerik:RadTextBox ID="RadTextBoxXM" runat="server" Width="97%" Skin="Default" 
                            MaxLength="25">
                        </telerik:RadTextBox>
                    </td>
                    <td width="11%" align="right" nowrap="nowrap">证件号码：
                    </td>
                    <td width="36%" align="left">
                        <telerik:RadTextBox ID="RadTextBoxZJHM" runat="server" Width="97%" Skin="Default"
                             MaxLength="25">
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" width="11%" nowrap="nowrap">注册编号：
                    </td>
                    <td width="42%" align="left">
                        <telerik:RadTextBox ID="RadTextBoxZCBH" runat="server" Width="97%" Skin="Default"
                             MaxLength="25">
                        </telerik:RadTextBox>
                    </td>
                    <td width="11%" align="right" nowrap="nowrap">区域：
                    </td>
                    <td width="36%" align="left">
                        <asp:RadioButtonList ID="RadioButtonListQY" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="全部" Value="全部" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="本地" Value="本地"></asp:ListItem>
                            <asp:ListItem Text="外地（含中央在京）" Value="外地"></asp:ListItem>
                        </asp:RadioButtonList>
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
                注册建造师列表
            </div>
            <div style="width: 98%; margin: 0 auto;">
                <telerik:RadGrid ID="RadGrid1" AutoGenerateColumns="False"
                    runat="server" AllowAutomaticDeletes="True" AllowAutomaticInserts="True" AllowAutomaticUpdates="True"
                    AllowPaging="True" PageSize="10" AllowSorting="True" SortingSettings-SortToolTip="单击进行排序"
                    Skin="Blue" EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false" Width="100%"
                    GridLines="None">
                    <ClientSettings EnableRowHoverStyle="true">
                    </ClientSettings>
                    <MasterTableView AllowMultiColumnSorting="True"
                        NoMasterRecordsText="　没有可显示的记录">
                        <Columns>
                            <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="QY" DataField="QY" HeaderText="区域">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="ZCH" DataField="ZCH" HeaderText="注册编号">
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="XM" DataField="XM" HeaderText="姓名">
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="ZJHM" DataField="ZJHM" HeaderText="证件号码">
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="QYMC" DataField="QYMC" HeaderText="企业名称">
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="ZZJGDM" DataField="ZZJGDM" HeaderText="组织机构代码">
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                        </Columns>
                        <PagerTemplate>
                            <uc2:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                        </PagerTemplate>
                    </MasterTableView>
                    <SortingSettings SortToolTip="单击进行排序"></SortingSettings>
                    <StatusBarSettings LoadingText="正在读取数据" ReadyText="完成" />
                </telerik:RadGrid>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="DataAccess.RY_ZCJZS"
                    SelectMethod="GetList" EnablePaging="true" SelectCountMethod="SelectCount" MaximumRowsParameterName="maximumRows"
                    StartRowIndexParameterName="startRowIndex" SortParameterName="orderBy">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                            DefaultValue="" ConvertEmptyStringToNull="false" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <br />
            </div>
        </div>
        <br />
    </div>
    <uc4:IframeView ID="IframeView" runat="server" />
</asp:Content>
