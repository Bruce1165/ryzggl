<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UnitList.aspx.cs" Inherits="ZYRYJG.Unit.UnitList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc1" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" />
    <link href="../Skins/Blue/Grid.Blue.css?v=1.008" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
    <link href="../layer/skin/layer.css" rel="stylesheet" />
    <script src="../layer/layer.js" type="text/javascript"></script>
    <style type="text/css">
        .watch {
            color: #444;
            float: left;
            margin: 3px 3px;
            background: url(../Images/watch.png) no-repeat left center transparent;
            background-size: 16px 16px;
        }

        .pointer {
            cursor: pointer;
            width: 16px;
            height: 16px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Windows7">
        </telerik:RadWindowManager>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1"
            EnableAJAX="true">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="ButtonSearch">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGridQY" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="RadGridQY">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGridQY" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true" Skin="Default" />
        <div class="div_out">
            <div style="width: 99%; margin: 5px auto; padding-bottom: 30px;">
                <div class="dqts">
                    <div style="float: left;">
                        当前位置 &gt;&gt;综合查询 &gt;&gt;企业信息查询&gt;&gt;<strong>企业基本信息查询</strong>
                    </div>
                </div>
                <div class="content">
                    <table width="98%" border="0" align="center" cellspacing="1">
                        <tr id="TrPerson" runat="server">
                            <td width="8%" align="right" nowrap="nowrap">机构代码：
                            </td>
                            <td align="left" width="20%">
                                <telerik:RadTextBox ID="RadTextBoxENT_OrganizationsCode" runat="server" Width="90%" Skin="Default">
                                </telerik:RadTextBox>
                            </td>
                            <td width="8%" align="right" nowrap="nowrap">企业名称：
                            </td>
                            <td align="left" width="20%">
                                <telerik:RadTextBox ID="RadTextBoxENT_Name" runat="server" Width="97%" Skin="Default">
                                </telerik:RadTextBox>
                            </td>
                            <td align="right" nowrap="nowrap" width="8%">所属区县：
                        </td>
                        <td align="left" width="20%">
                            <telerik:RadComboBox ID="RadComboBoxENT_City" runat="server" >
                                <Items>
                                    <telerik:RadComboBoxItem Text="请选择" Value="" />
                                    <telerik:RadComboBoxItem Text="东城区" Value="东城区" />
                                    <telerik:RadComboBoxItem Text="西城区" Value="西城区" />
                                    <telerik:RadComboBoxItem Text="朝阳区" Value="朝阳区" />
                                    <telerik:RadComboBoxItem Text="海淀区" Value="海淀区" />
                                    <telerik:RadComboBoxItem Text="丰台区" Value="丰台区" />
                                    <telerik:RadComboBoxItem Text="石景山区" Value="石景山区" />
                                    <telerik:RadComboBoxItem Text="昌平区" Value="昌平区" />
                                    <telerik:RadComboBoxItem Text="通州区" Value="通州区" />
                                    <telerik:RadComboBoxItem Text="顺义区" Value="顺义区" />
                                    <telerik:RadComboBoxItem Text="门头沟区" Value="门头沟区" />
                                    <telerik:RadComboBoxItem Text="房山区" Value="房山区" />
                                    <telerik:RadComboBoxItem Text="大兴区" Value="大兴区" />
                                    <telerik:RadComboBoxItem Text="怀柔区" Value="怀柔区" />
                                    <telerik:RadComboBoxItem Text="平谷区" Value="平谷区" />
                                    <telerik:RadComboBoxItem Text="密云区" Value="密云区" />
                                    <telerik:RadComboBoxItem Text="延庆区" Value="延庆区" />
                                    <telerik:RadComboBoxItem Text="亦庄" Value="亦庄" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                            <td align="left">
                                <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
                            </td>
                        </tr>
                    </table>
                    <div style="width: 98%; margin: 8px auto;">
                        <telerik:RadGrid ID="RadGridQY" runat="server"
                            GridLines="None" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                            SortingSettings-SortToolTip="单击进行排序" Width="100%" Skin="Blue" EnableAjaxSkinRendering="False"
                            EnableEmbeddedSkins="False" PagerStyle-AlwaysVisible="true">
                            <ClientSettings EnableRowHoverStyle="true">
                            </ClientSettings>
                            <HeaderContextMenu EnableEmbeddedSkins="False">
                            </HeaderContextMenu>
                            <MasterTableView NoMasterRecordsText=" 没有可显示的记录" CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Left"
                                DataKeyNames="UnitID">
                                <Columns>
                                    <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="ENT_OrganizationsCode" DataField="ENT_OrganizationsCode" HeaderText="机构代码">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn UniqueName="ENT_Name" HeaderText="企业名称" SortExpression="ENT_Name">
                                        <ItemTemplate>
                                            <%# (CheckUnitWatch(Eval("ENT_OrganizationsCode").ToString()) == true ? "<div class=\"watch pointer\" onclick='javascript:alert(\"重点核查企业!\")' title=\"重点核查企业\"> &nbsp;</div>" : "")%>
                                            <span class="link_edit" onclick=' javascript:SetIfrmSrc("UnitDetail.aspx?o=<%# Utility.Cryptography.Encrypt(Eval("UnitID").ToString()) %>"); '><%# Eval("ENT_Name")%></span>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridBoundColumn UniqueName="ENT_City" DataField="ENT_City" HeaderText="所属区县">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn UniqueName="ENT_Contact" DataField="ENT_Contact" HeaderText="联系人">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="ENT_Telephone" DataField="ENT_Telephone" HeaderText="联系电话">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="ENT_Type" DataField="ENT_Type" HeaderText="企业类型">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn UniqueName="Edit" HeaderText="建造师流动情况">
                                        <ItemTemplate>
                                            <%# CheckUnitWatch(Eval("ENT_OrganizationsCode").ToString()) == true ?string.Format("<span class=\"link_edit\" onclick='javascript:SetIfrmSrc(\"JZSChangeUnitHis.aspx?o={0}\"); '>建造师流动情况</span>", Utility.Cryptography.Encrypt(Eval("ENT_OrganizationsCode").ToString()) ):""%>
                                        </ItemTemplate>
                                        <headerstyle horizontalalign="Center" wrap="false" />
                                            <itemstyle horizontalalign="Center" wrap="false" forecolor="Blue" />
                                            </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn UniqueName="PeopleInfo" HeaderText="人员信息">
                                        <itemtemplate>                                          
                                            <span class="link_edit" onclick='javascript:SetIfrmSrc("PeopleList.aspx?p=<%# Utility.Cryptography.Encrypt(Eval("ENT_OrganizationsCode").ToString())%>");'
                                            >三师人员</span>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                                    </telerik:GridTemplateColumn>
                                </Columns>
                                <HeaderStyle Font-Bold="True" />
                                <PagerTemplate>
                                    <uc1:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                                </PagerTemplate>
                            </MasterTableView>
                            <FilterMenu EnableEmbeddedSkins="False">
                            </FilterMenu>
                            <SortingSettings SortToolTip="单击进行排序"></SortingSettings>
                        </telerik:RadGrid>

                        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="Model.UnitMDL"
                            SelectMethod="GetList" TypeName="DataAccess.UnitDAL"
                            SelectCountMethod="SelectCount" EnablePaging="true"
                            MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex" SortParameterName="orderBy">
                            <SelectParameters>
                                <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                                    DefaultValue="" ConvertEmptyStringToNull="false" />
                            </SelectParameters>
                        </asp:ObjectDataSource>

                    </div>
                </div>
            </div>
        </div>
        <uc2:IframeView ID="IframeView" runat="server" />
    </form>
</body>
</html>
