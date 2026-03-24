<%@ Page Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="BlackListManage.aspx.cs" Inherits="ZYRYJG.EXamManage.BlackListManage" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../PostSelect.ascx" TagName="PostSelect" TagPrefix="uc1" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    </telerik:RadCodeBlock>
    <telerik:RadWindowManager runat="server" RestrictionZoneID="offsetElement" ID="RadWindowManager1"
        EnableShadow="true" EnableEmbeddedScripts="true" Skin="Sunset" VisibleStatusbar="false">
    </telerik:RadWindowManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" EnableAJAX="true">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ButtonSearch">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGridUnit" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Sunset" />
    <div class="div_out">
        <div class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt; 考务管理 &gt;&gt;
                报名管理 &gt;&gt; <strong>报考黑名单</strong>
            </div>
        </div>
        <div class="table_border" style="width: 98%; margin: 5px auto;">
            <div class="content">
                <div class="jbxxbt">
                    报考黑名单</div>
                <div>
                    <div class="table_cx">
                        <img src="../Images/1034.gif" width="11" height="11" style="margin-bottom: -2px;
                            padding-right: 2px;" />
                        查询条件</div>
                    <div class="blue_center" style="width: 98%; margin: 0 auto;">
                        <div>
                            <b class="subxtop"><b class="subxb1"></b><b class="subxb2"></b><b class="subxb3"></b>
                                <b class="subxb4"></b></b>
                        </div>
                        <div class="subxboxcontent" id="divserch" runat="server">
                            <table width="95%" border="0" align="center" cellspacing="2">
                                <tr>
                                    <td width="11%" align="right" nowrap="nowrap">
                                        岗位工种：
                                    </td>
                                    <td align="left" width="38%">
                                        <uc1:PostSelect ID="PostSelect1" runat="server" />
                                    </td>
                                    <td width="11%" align="right" nowrap="nowrap">
                                        黑名单类型：
                                    </td>
                                    <td align="left" width="38%">
                                        <telerik:RadComboBox ID="RadComboBoxBlackType" runat="server" Skin="Office2007" CausesValidation="False"
                                            ExpandAnimation-Duration="0">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="全部" Value="" Selected="true" />
                                                <telerik:RadComboBoxItem Text="替人考试" Value="替人考试" />
                                                <telerik:RadComboBoxItem Text="虚假申报" Value="虚假申报" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" nowrap="nowrap">
                                        生效时间：
                                    </td>
                                    <td width="39%" align="left">
                                        <telerik:RadDatePicker ID="RadDatePicker_StartDate" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                            Width="46%" ToolTip="不选择时间表示从无穷小" />
                                        <div class="RadPicker">至</div>
                                        <telerik:RadDatePicker ID="RadDatePicker_EndDate" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                            Width="46%" ToolTip="不选择时间表示到无穷大" />
                                    </td>
                                    <td width="11%" align="right" nowrap="nowrap">
                                        报名状态：
                                    </td>
                                    <td align="left" width="38%">
                                        <telerik:RadComboBox ID="RadComboBoxBlackStatus" runat="server" Skin="Office2007"
                                            CausesValidation="False" ExpandAnimation-Duration="0">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="全部" Value="" Selected="true" />
                                                <telerik:RadComboBoxItem Text="有效" Value="有效" />
                                                <telerik:RadComboBoxItem Text="失效" Value="失效" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" width="11%" nowrap="nowrap">
                                        姓名：
                                    </td>
                                    <td align="left" width="38%">
                                        <telerik:RadTextBox ID="RadTxtWorkerName" runat="server" Width="97%" Skin="Default"
                                            >
                                        </telerik:RadTextBox>
                                    </td>
                                    <td width="11%" align="right" nowrap="nowrap">
                                        证件号码：
                                    </td>
                                    <td align="left" width="38%">
                                        <telerik:RadTextBox ID="RadTxtCertificateCode" runat="server" Width="95%" Skin="Default"
                                            >
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" width="11%" nowrap="nowrap">
                                        单位名称：
                                    </td>
                                    <td width="42%" align="left">
                                        <telerik:RadTextBox ID="RadTextBoxUnitName" runat="server" Width="97%" Skin="Default"
                                            >
                                        </telerik:RadTextBox>
                                    </td>
                                    <td align="right" width="11%" nowrap="nowrap">
                                        培训点：
                                    </td>
                                    <td align="left" width="38%">
                                        <telerik:RadTextBox ID="RadTextBoxTrainUnitName" runat="server" Width="97%" Skin="Default"
                                            >
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" align="center">
                                        <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div>
                            <b class="subxbottom"></b><b class="subxb4"></b><b class="subxb3"></b><b class="subxb2">
                            </b><b class="subxb1"></b>
                        </div>
                    </div>
                </div>
                <div>
                    <div class="table_cx">
                        <img src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px;
                            padding-right: 2px;" />
                        黑名单列表</div>
                    <div style="width: 98%; margin: 0 auto;">
                        <telerik:RadGrid ID="RadGridMain" AutoGenerateColumns="False" runat="server" AllowPaging="True"
                            PageSize="10" AllowSorting="True" SortingSettings-SortToolTip="单击进行排序" Skin="Blue"
                            EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false" Width="100%" GridLines="None"  PagerStyle-AlwaysVisible="true"
                            OnItemCommand="RadGridUnit_ItemCommand">
                            <ClientSettings EnableRowHoverStyle="true">
                            </ClientSettings>
                            <MasterTableView AllowMultiColumnSorting="True" DataKeyNames="BlackListID" NoMasterRecordsText="　没有可显示的记录"  CommandItemDisplay="Top"
                            EditMode="PopUp">
                                <CommandItemTemplate>
                                <div class="grid_CommandBar">
                                    &nbsp;
                                    <nobr class="grid_CmdButton">                        
                                    <span onclick="javascript:SetIfrmSrc('BlackListEdit.aspx');" style="cursor:pointer;">
                                               <input id="Button2" type="button" value="" class="rgAdd" />&nbsp;添加黑名单
                                               </span>                                            
                                        </nobr>
                                </div>
                            </CommandItemTemplate>
                            <CommandItemStyle Height="30" HorizontalAlign="Left" />
                                <Columns>
                                    <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="行号" AllowSorting="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="BlackType" DataField="BlackType" HeaderText="黑名单类型">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="PostTypeName" DataField="PostTypeName" HeaderText="岗位类别">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="PostName" DataField="PostName" HeaderText="岗位工种">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="WorkerName" DataField="WorkerName" HeaderText="姓名">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="CertificateCode" DataField="CertificateCode"
                                        HeaderText="证件号码">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="UnitName" DataField="UnitName" HeaderText="单位名称">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="TrainUnitName" DataField="TrainUnitName" HeaderText="培训点">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="BlackStatus" DataField="BlackStatus" HeaderText="状态">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="StartTime" DataField="StartTime" HeaderText="生效时间"
                                        HtmlEncode="false" DataFormatString="{0:yyyy.MM.dd}">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn UniqueName="View">
                                        <ItemTemplate>
                                            <span class="link_edit" onclick='javascript:SetIfrmSrc("BlackListEdit.aspx?o=<%# Eval("BlackListID") %>&v=1");'>
                                                详细</span>
                                        </ItemTemplate>
                                        <ItemStyle ForeColor="Blue" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn UniqueName="Edit">
                                        <ItemTemplate>
                                            <span class="link_edit" onclick='javascript:SetIfrmSrc("BlackListEdit.aspx?o=<%# Eval("BlackListID") %>");'>
                                                编辑</span>
                                        </ItemTemplate>
                                        <ItemStyle ForeColor="Blue" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridButtonColumn UniqueName="Delete" HeaderText="" CommandName="Delete"
                                        Text="&lt;span onclick=&quot;if(!confirm('您确定要删除么?'))return false; &quot; &gt;删除&lt;/span&gt;">
                                    </telerik:GridButtonColumn>
                                </Columns>
                                <PagerTemplate>
                                    <uc2:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                                </PagerTemplate>
                                <PagerStyle AlwaysVisible="true" />
                                <EditItemStyle HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                            </MasterTableView>
                            <SortingSettings SortToolTip="单击进行排序"></SortingSettings>
                            <StatusBarSettings LoadingText="正在读取数据" ReadyText="完成" />
                        </telerik:RadGrid>
                        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="DataAccess.BlackListDAL"
                            DataObjectTypeName="Model.BlackListOB" SelectMethod="GetList" EnablePaging="true"
                            SelectCountMethod="SelectCount" MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex"
                            SortParameterName="orderBy">
                            <SelectParameters>
                                <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                                    DefaultValue="" ConvertEmptyStringToNull="false" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </div>
                </div>
            </div>
            <br />
        </div>
    </div>
    <uc4:IframeView ID="IframeView" runat="server" />
</asp:Content>
