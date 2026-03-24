<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonLostList.aspx.cs" Inherits="ZYRYJG.PersonLostList" %>

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
                    企业人员稳定性预警详细情况（建造师）
                </p>
                <div style="width: 100%; height: 100%; margin: 10px auto; text-align: center; border-collapse: collapse;">
                    <div style="width:95%; border:1px solid #dfdfdf; background-color:#F5F5FF; line-height:18px;color:#333; margin-bottom:12px; padding:12px 0 12px 20px; text-align:left; font-weight:bold">
                        1、本项功能统计企业建造师（一级、一级临时、二级、二级临时）流失情况。<br />
                        2、流入数量包含首次注册、变更单位转入。
                       流出数量包含主动注销、变更单位转出（过期不在统计之列）。
                        流失率=流出数量  /流入数量 。
                    </div>
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
                                     
                                 <telerik:GridBoundColumn UniqueName="CurCount" DataField="CurCount" HeaderText="当前数量">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left"   />
                                </telerik:GridBoundColumn>
                                  <telerik:GridBoundColumn UniqueName="GetCount" DataField="GetCount" HeaderText="流入数量">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left"   />
                                </telerik:GridBoundColumn>
                                  <telerik:GridBoundColumn UniqueName="LostCount" DataField="LostCount" HeaderText="流出数量">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left"   />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="LSV" DataField="LSV" HeaderText="流失率" DataFormatString="{0}%">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left"   />
                                </telerik:GridBoundColumn>
                            
                            </Columns>
                            <HeaderStyle Font-Bold="True" Wrap="false" />
                            <PagerTemplate>
                                <uc1:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                            </PagerTemplate>
                        </MasterTableView>
                        <HeaderStyle Wrap="false" />
                    </telerik:RadGrid>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
                        SelectMethod="GetList" TypeName="DataAccess.jcsjk_tj_qy_jzsDAL"
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
