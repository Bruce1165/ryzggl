<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QYZZWDBList.aspx.cs" Inherits="ZYRYJG.QYZZWDBList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc1" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" /> 
    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
    <script type="text/javascript" src="../Scripts/echarts-all.js"></script>
    <script type="text/javascript" src="../Scripts/jquery-3.4.1.min.js"></script>
    <link href="../layer/skin/layer.css" rel="stylesheet" />
    <script src="../layer/layer.js" type="text/javascript"></script>
</head>
<body style="padding-bottom: 20px">
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
                    当前位置 &gt;&gt;人员全执业生命周期 &gt;&gt;<strong>企业监控</strong>
                </div>
            </div>
            <div class="content">
                <p class="jbxxbt">
                    资质未达标企业预警
                </p>
                <div style="width: 100%; height: 100%; margin: 10px auto; text-align: center; border-collapse: collapse;">
                    <table width="98%" border="0" align="center" cellspacing="1">
                        <tr id="TrPerson" runat="server">
                            <td width="150px" align="right" nowrap="nowrap">
                                <telerik:RadComboBox ID="RadComboBoxIten" runat="server" Width="100%">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="企业名称" Value="QYMC" />
                                        <telerik:RadComboBoxItem Text="机构代码" Value="ZZJGDM" />
                                        <telerik:RadComboBoxItem Text="所在区县" Value="RegionName" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td align="left" width="300px">
                                <telerik:RadTextBox ID="RadTextBoxValue" runat="server" Width="97%" Skin="Default">
                                </telerik:RadTextBox>
                            </td>                           
                            <td align="left">                          
                                <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
                                &nbsp; &nbsp;<asp:Button ID="ButtonExport" runat="server" Text="导 出" CssClass="button" OnClick="ButtonExport_Click"/><span id="spanOutput" runat="server" class="excel" style="padding-right:40px; font-weight:bold"></span>
                            
                            </td>
                        </tr>
                    </table>
                    <telerik:RadGrid ID="RadGridQY" runat="server"
                        GridLines="None" AllowPaging="true" AllowSorting="false" AutoGenerateColumns="False" PageSize="10"
                        SortingSettings-SortToolTip="单击进行排序" Width="98%" Skin="Default" EnableAjaxSkinRendering="true"
                        EnableEmbeddedSkins="true" PagerStyle-AlwaysVisible="true" >
                        <ClientSettings EnableRowHoverStyle="true" Selecting-AllowRowSelect="true">
                        </ClientSettings>
                        <HeaderContextMenu EnableEmbeddedSkins="False">
                        </HeaderContextMenu>
                        <MasterTableView NoMasterRecordsText=" 没有可显示的记录" CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Left"
                           >
                            <Columns>
                                <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                    <HeaderStyle HorizontalAlign="Right"  Wrap="false" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="ZZJGDM" DataField="ZZJGDM" HeaderText="机构代码">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                  <telerik:GridBoundColumn UniqueName="QYMC" DataField="QYMC" HeaderText="企业名称">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left"  />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="RegionName" DataField="RegionName" HeaderText="所在区县">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                               <telerik:GridTemplateColumn UniqueName="ZCZJ"  HeaderText="注册资本金">
                                   <ItemTemplate>
                                       <%# Eval("ZCZJ") %><%# Eval("ZCBZ") %>
                                   </ItemTemplate>
                               </telerik:GridTemplateColumn>
                              
                              <telerik:GridBoundColumn UniqueName="FDDBR" DataField="FDDBR" HeaderText="法定代表人">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                 <telerik:GridBoundColumn UniqueName="LXDH" DataField="LXDH" HeaderText="联系电话">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                              
                                <telerik:GridBoundColumn UniqueName="ZCDZ" DataField="ZCDZ" HeaderText="注册地址">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left"   />
                                </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="Edit" HeaderText="详细">
                                    <ItemTemplate>
                                        <span class="link_edit" onclick='javascript:SetIfrmSrc("QYDetail.aspx?o=<%# Eval("ZZJGDM") %>")'>详细</span>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                                </telerik:GridTemplateColumn>
                            </Columns>
                            <HeaderStyle Font-Bold="True" Wrap="false" />
                            <PagerTemplate>
                                <uc1:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                            </PagerTemplate>
                        </MasterTableView>
                        <HeaderStyle Wrap="false" />
                    </telerik:RadGrid>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
                        SelectMethod="GetList" TypeName="DataAccess.jcsjk_tj_qy_zzwdbDAL"
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
        <uc2:IframeView ID="IframeView" runat="server" />
    </form>
</body>
</html>
