<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ZCJLS.aspx.cs" Inherits="ZYRYJG.Unit.ZCJLS" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc1" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" />
    <link href="../Skins/Blue/Grid.Blue.css?v=1.008" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
    <script src="../Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
        <link href="../layer/skin/layer.css" rel="stylesheet" />
    <script src="../layer/layer.js" type="text/javascript"></script>
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
            <div id="div_top" class="dqts">
                <div style="float: left;">
                     当前位置 &gt;&gt;综合查询 &gt;&gt;人员证书查询 &gt;&gt;<strong>注册监理师</strong>
                </div>
            </div>
            <div class="content">
                <p class="jbxxbt">
                    注册监理师
                </p>
                <table class="cx" width="100%" border="0" align="center" cellspacing="1">
                    <tr id="TrPerson" runat="server">
                        <td width="12%" align="right" nowrap="nowrap">
                            <telerik:RadComboBox ID="RadComboBoxIten" runat="server">
                                <Items>
                                    <telerik:RadComboBoxItem Text="姓名" Value="姓名" />
                                    <telerik:RadComboBoxItem Text="证件号" Value="证件号" />
                                    <telerik:RadComboBoxItem Text="注册号" Value="注册号" />
                                    <telerik:RadComboBoxItem Text="执业资格证书编号" Value="执业资格证书编号" />
                                    <telerik:RadComboBoxItem Text="注册证书编号" Value="注册证书编号" />
                                    <telerik:RadComboBoxItem Text="聘用单位" Value="聘用单位" />
                                    <telerik:RadComboBoxItem Text="所在区县" Value="所在区县" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <td align="left" width="38%">
                            <telerik:RadTextBox ID="RadTextBoxValue" runat="server" Width="97%" Skin="Default">
                            </telerik:RadTextBox>

                        </td>

                        <td align="left" width="38%">
                            <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
                             &nbsp;
                            <asp:Button ID="ButtonExportToExcel" CssClass="bt_large" Text="导出查询结果" OnClick="ButtonExportToExcel_Click"
                                runat="server"></asp:Button>
                        </td>
                    </tr>
                </table>
                <div style="width: 98%; margin: 8px auto;">
                    <telerik:RadGrid ID="RadGridQY" runat="server"
                        GridLines="None" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" PageSize="15"
                        SortingSettings-SortToolTip="单击进行排序" Width="100%" Skin="Blue" EnableAjaxSkinRendering="False" EnableEmbeddedSkins="False" PagerStyle-AlwaysVisible="true">
                        <ClientSettings EnableRowHoverStyle="true">
                        </ClientSettings>
                        <HeaderContextMenu EnableEmbeddedSkins="False">
                        </HeaderContextMenu>
                        <MasterTableView NoMasterRecordsText=" 没有可显示的记录" CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Left">
                            <Columns>

                                <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="所在区县" DataField="所在区县" HeaderText="所在区县" Visible="false">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="姓名" DataField="姓名" HeaderText="姓名">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="证件号" DataField="证件号" HeaderText="证件号码">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="聘用单位" DataField="聘用单位" HeaderText="聘用单位">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="执业资格证书编号" DataField="执业资格证书编号" HeaderText="执业资格证书编号">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="注册号" DataField="注册号" HeaderText="注册号">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="注册证书编号" DataField="注册证书编号" HeaderText="注册证书编号">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                 <telerik:GridTemplateColumn UniqueName="注册专业" HeaderText="注册专业">
                                    <ItemTemplate>
                                        <%# Eval("注册专业1") %><%# Eval("注册专业2")==DBNull.Value || Eval("注册专业2").ToString().Trim()=="无"?"":"；"+Eval("注册专业2") %>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn UniqueName="发证日期" DataField="发证日期" HeaderText="发证日期" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="注册有效期" DataField="注册有效期" HeaderText="注册有效期" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>

                                <telerik:GridTemplateColumn UniqueName="Edit">
                                    <ItemTemplate>
                                        <span class="link_edit" onclick=' javascript:SetIfrmSrc("ZCJLSDetail.aspx?o=<%# Utility.Cryptography.Encrypt(Eval("证件号").ToString()) %>"); '>详细</span>
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
                </div>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="Model.COC_TOW_Person_BaseInfoMDL"
                    SelectMethod="GetList" TypeName="DataAccess.jcsjk_jlsDAL"
                    SelectCountMethod="SelectCount" EnablePaging="true"
                    MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex" SortParameterName="orderBy">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                            DefaultValue="" ConvertEmptyStringToNull="false" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </div>
        </div>
        <uc2:IframeView ID="IframeView" runat="server" />
        <div id="winpop">
        </div>
    </form>
</body>
</html>
