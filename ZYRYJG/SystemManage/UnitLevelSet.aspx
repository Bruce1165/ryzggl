<%@ Page Title="" Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="UnitLevelSet.aspx.cs" Inherits="ZYRYJG.SystemManage.UnitLevelSet" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    </telerik:RadCodeBlock>
    <telerik:RadWindowManager ID="RadWindowManager1" ShowContentDuringLoad="false" VisibleStatusbar="false"
        ReloadOnShow="true" runat="server" Skin="Windows7" EnableShadow="true">
    </telerik:RadWindowManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1"
        EnableAJAX="true">
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
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true" Skin="Windows7" />
    <div class="div_out">
        <div class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt; 系统管理 &gt;&gt;
                系统维护 &gt;&gt; <strong>企业隶属管理维护</strong>
            </div>
        </div>
        <div class="content" style="width: 99%; margin: 2px auto;">

            <div class="jbxxbt">
                企业隶属管理维护
            </div>
            <div id="SearchDiv" runat="server">
                <div class="table_cx" style="padding-bottom: 8px;">
                    <img alt="" src="../Images/1034.gif" width="11" height="11" style="margin-bottom: -2px; padding-right: 2px;" />查询条件
                </div>
                <table class="cx" width="98%" border="0" align="center" cellspacing="5">
                  

                            <tr>
                                <td align="right" nowrap="nowrap" width="20%">企业名称
                                </td>
                                <td align="left" width="30%">
                                    <telerik:RadTextBox ID="RadTextBoxUnitName" runat="server" Width="97%" Skin="Default" MaxLength="25">
                                    </telerik:RadTextBox>
                                </td>
                                <td align="right" nowrap="nowrap" width="20%">组织机构代码
                                </td>
                                <td align="left" width="30%">
                                    <telerik:RadTextBox ID="RadTextBoxUnitCode" runat="server" Width="97%" Skin="Default" MaxLength="9">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" nowrap="nowrap">隶属机构类型：
                                </td>
                                <td align="left">
                                    <telerik:RadComboBox ID="RadComboBoxLSGX" runat="server" Width="150px">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="全部" Value="" Selected="true" />
                                            <telerik:RadComboBoxItem Text="无隶属关系" Value="无隶属关系" />
                                            <telerik:RadComboBoxItem Text="区县" Value="区县" />
                                            <telerik:RadComboBoxItem Text="市属集团总公司" Value="市属集团总公司" />
                                            <telerik:RadComboBoxItem Text="中央驻京单位" Value="中央驻京单位" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td align="right" nowrap="nowrap">隶属机构名称：
                                </td>
                                <td align="left">
                                    <telerik:RadTextBox ID="RadTextBoxUserName" runat="server" Width="97%" Skin="Default" MaxLength="25">
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
            <div style="width: 100%; margin: 15px auto;">
                <telerik:RadGrid ID="RadGrid1" runat="server" AutoGenerateColumns="False" AllowSorting="false" AllowPaging="true" AllowCustomPaging="true"
                    PageSize="10"
                    GridLines="None" CellPadding="0" Width="98%" Skin="Blue" EnableAjaxSkinRendering="false"
                    EnableEmbeddedSkins="false" OnItemCommand="RadGrid1_ItemCommand">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="UNITCODE,ID,USERID" NoMasterRecordsText="没有可显示的记录">
                        <CommandItemTemplate>
                            <div class="grid_CommandBar" style="line-height: 20px; padding-left: 10px;">
                                <input type="button" value=" " class="rgAdd" onclick="javascript: SetIfrmSrc('UnitLevelEdit.aspx');" />
                                <nobr onclick="javascript:SetIfrmSrc('UnitLevelEdit.aspx');" class="grid_CmdButton" style="cursor: pointer;">
                                       &nbsp;添加隶属企业</nobr>
                            </div>
                        </CommandItemTemplate>
                        <Columns>
                            <telerik:GridBoundColumn HeaderText="序号" UniqueName="rn" DataField="rn" AllowSorting="false">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="UNITNAME" DataField="UNITNAME" HeaderText="企业名称">
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="UNITCODE" DataField="UNITCODE" HeaderText="组织机构代码">
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="RELUSERNAME" DataField="RELUSERNAME" HeaderText="隶属机构">
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="jcsjk_lsgx" DataField="jcsjk_lsgx" HeaderText="基础库中隶属信息" Visible="false">
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="Edit" HeaderText="设置">
                                <ItemTemplate>
                                    <span class="link_edit" onclick='javascript:SetIfrmSrc("UnitLevelEdit.aspx?o=<%# Eval("ID")%>&u=<%# Eval("UNITCODE")%>&n=<%# Eval("UNITNAME")%>");'>设置</span>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" Font-Bold="true" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridButtonColumn UniqueName="Delete" CommandName="Delete" HeaderText="解除隶属关系"
                                Text="&lt;span onclick=&quot;if(!confirm('您确定要解除隶属关系么?'))return false; &quot; &gt;解除隶属关系&lt;/span&gt;">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridButtonColumn>
                        </Columns>

                        <HeaderStyle Font-Bold="True" />
                        <PagerTemplate>
                            <uc2:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                        </PagerTemplate>
                    </MasterTableView>
                </telerik:RadGrid>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="Model.QY_LSGXOB"
                    SelectMethod="GetListView" TypeName="DataAccess.QY_LSGXDAL"
                    SelectCountMethod="SelectCountView" EnablePaging="true" MaximumRowsParameterName="maximumRows"
                    StartRowIndexParameterName="startRowIndex" SortParameterName="orderBy">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                            DefaultValue="" ConvertEmptyStringToNull="false" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </div>
        </div>
    </div>
    <uc4:IframeView ID="IframeView" runat="server" />
</asp:Content>
