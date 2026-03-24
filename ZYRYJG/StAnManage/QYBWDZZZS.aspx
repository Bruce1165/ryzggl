<%@ Page Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="QYBWDZZZS.aspx.cs" Inherits="ZYRYJG.StAnManage.QYBWDZZZS" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
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
                当前位置 &gt;&gt;综合查询 &gt;&gt;企业信息查询&gt;&gt; <strong>企业资质查询</strong>
            </div>
        </div>

        <div class="DivTitleOn" onclick="DivOnOff(this,'Td3',event);" title="折叠">
            查询说明
        </div>
        <div class="DivContent" id="Td3">
            资质比对顺序：&nbsp;&nbsp;
         1、建筑施工企业&nbsp;&nbsp;
         2、中央在京&nbsp;&nbsp;
         3、外地进京&nbsp;&nbsp;
         4、起重机械租赁企业&nbsp;&nbsp;
         5、设计施工一体化
        </div>
        <div class="content">
            <table class="bar_cx">
                <tr>
                    <td align="right" width="11%" nowrap="nowrap">企业名称：
                    </td>
                    <td width="42%" align="left">
                        <telerik:RadTextBox ID="rdtxtQYMC" runat="server" Width="97%" Skin="Default" >
                        </telerik:RadTextBox>
                    </td>
                    <td width="11%" align="right" nowrap="nowrap">企业代码：
                    </td>
                    <td width="36%" align="left">
                        <telerik:RadTextBox ID="RadTextBoxUNITCODE" runat="server" Width="97%" Skin="Default"
                            >
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" width="11%" nowrap="nowrap">资质类型：
                    </td>
                    <td width="42%" align="left">
                        <telerik:RadComboBox ID="RadComboBoxType" runat="server" Width="97%">
                            <Items>
                                <telerik:RadComboBoxItem Text="全部" Value="" Selected="true" />
                                <telerik:RadComboBoxItem Text="建筑施工企业" Value="建筑施工企业" />
                                <telerik:RadComboBoxItem Text="中央在京" Value="中央在京" />
                                <telerik:RadComboBoxItem Text="外地进京" Value="外地进京" />
                                <telerik:RadComboBoxItem Text="起重机械租赁企业" Value="起重机械租赁企业" />
                                <telerik:RadComboBoxItem Text="设计施工一体化" Value="设计施工一体化" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                    <td width="11%" align="right" nowrap="nowrap"></td>
                    <td width="36%" align="left"></td>
                </tr>
                <tr>
                    <td colspan="4" align="center">
                        <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
                    </td>
                </tr>
            </table>
            <div class="table_cx">
                <img src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                企业持证人员列表
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
                            <telerik:GridBoundColumn UniqueName="QYLB" DataField="QYLB" HeaderText="资质类别">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
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
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="DataAccess.UnitDAL"
                    DataObjectTypeName="Model.UnitMDL" SelectMethod="GetList_QY_BWDZZZS" EnablePaging="true"
                    SelectCountMethod="SelectCount_QY_BWDZZZS" MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex"
                    SortParameterName="orderBy">
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
