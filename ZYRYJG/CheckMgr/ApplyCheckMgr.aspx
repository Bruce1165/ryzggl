<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplyCheckMgr.aspx.cs" Inherits="ZYRYJG.CheckMgr.ApplyCheckMgr" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc1" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc2" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
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
        <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server">
        </telerik:RadStyleSheetManager>
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Windows7">
        </telerik:RadWindowManager>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1"
            EnableAJAX="true">
            <AjaxSettings>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true" Skin="Default" />
        <div class="div_out">
            <div class="dqts">
                <div style="float: left;">
                    当前位置 &gt;&gt;综合查询 &gt;&gt;<strong>业务抽查创建</strong>
                </div>
            </div>
            <div class="content">
                <table class="cx" width="98%" border="0" align="center" cellspacing="5">
                    <tr id="TrPerson" runat="server">
                        <td style="text-align: right;width:200px">过滤创建时间：
                        </td>
                        <td width="100px" style="text-align: left" nowrap="nowrap">
                           <telerik:RadComboBox ID="RadComboBoxYear" runat="server" Skin="Office2007" CausesValidation="False" AutoPostBack="true" 
                               OnSelectedIndexChanged="RadComboBoxYear_SelectedIndexChanged" Width="60px" ExpandAnimation-Duration="0">
                        </telerik:RadComboBox>
                        &nbsp;年&nbsp;
                                        <telerik:RadComboBox ID="RadComboBoxMonth" runat="server" Skin="Office2007" CausesValidation="False" AutoPostBack="true" 
                                            Width="60px" ExpandAnimation-Duration="0" OnSelectedIndexChanged="RadComboBoxMonth_SelectedIndexChanged">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="全部" Value="" />
                                                <telerik:RadComboBoxItem Text="1" Value="1" />
                                                <telerik:RadComboBoxItem Text="2" Value="2" />
                                                <telerik:RadComboBoxItem Text="3" Value="3" />
                                                <telerik:RadComboBoxItem Text="4" Value="4" />
                                                <telerik:RadComboBoxItem Text="5" Value="5" />
                                                <telerik:RadComboBoxItem Text="6" Value="6" />
                                                <telerik:RadComboBoxItem Text="7" Value="7" />
                                                <telerik:RadComboBoxItem Text="8" Value="8" />
                                                <telerik:RadComboBoxItem Text="9" Value="9" />
                                                <telerik:RadComboBoxItem Text="10" Value="10" />
                                                <telerik:RadComboBoxItem Text="11" Value="11" />
                                                <telerik:RadComboBoxItem Text="12" Value="12" />
                                            </Items>
                                        </telerik:RadComboBox>&nbsp;月&nbsp;
                        </td>
                        <td align="left" style="padding-left:100px">
                            <input id="ButtonNew" visible="false" runat="server" type="button" value="新建抽查任务" class="bt_large" onclick='javascript: SetIfrmSrc("ApplyCheckEdit.aspx"); ' />
                        </td>
                    </tr>
                </table>
                <div style="width: 99%; margin: 8px auto;">
                    <telerik:RadGrid ID="RadGridTask" runat="server"
                        GridLines="None" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" PageSize="15" OnItemCommand="RadGridTask_ItemCommand"
                        SortingSettings-SortToolTip="单击进行排序" Width="100%" Skin="Blue" EnableAjaxSkinRendering="False" EnableEmbeddedSkins="False" PagerStyle-AlwaysVisible="true">
                        <ClientSettings EnableRowHoverStyle="true">
                        </ClientSettings>
                        <HeaderContextMenu EnableEmbeddedSkins="False">
                        </HeaderContextMenu>
                        <MasterTableView NoMasterRecordsText=" 没有可显示的记录" CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Center"
                            DataKeyNames="TaskID">
                            <Columns>
                                <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="cjsj" DataField="cjsj" HeaderText="创建时间" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="BusRange" DataField="BusRange" HeaderText="抽查业务范围">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>                                    
                                 <telerik:GridTemplateColumn UniqueName="BusStartDate"  HeaderText="业务办结时间范围">
                                    <ItemTemplate>
                                     <%# Convert.ToDateTime(Eval("BusStartDate")).ToString("yyyy.MM.dd")%> - <%# Convert.ToDateTime(Eval("BusEndDate")).ToString("yyyy.MM.dd")%> 
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>                
                                    <telerik:GridBoundColumn UniqueName="CheckPer" DataField="CheckPer" HeaderText="抽查千分比例">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="ItemCount" DataField="ItemCount" HeaderText="抽查数据量">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                 <telerik:GridBoundColumn UniqueName="CheckCount" DataField="CheckCount" HeaderText="已审查数量">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                 <telerik:GridTemplateColumn UniqueName="Detail" HeaderText="详细">
                                    <ItemTemplate>
                                        <a class="link_edit" href='ApplyCheckDetail.aspx?o=<%# Utility.Cryptography.Encrypt(Eval("TaskID").ToString())%>'>详细</a>
                            
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" Font-Bold="true" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn UniqueName="Edit" HeaderText="修改" Visible="false">
                                    <ItemTemplate>
                                        <span class="link_edit" onclick='javascript:SetIfrmSrc("ApplyCheckEdit.aspx?o=<%# Utility.Cryptography.Encrypt(Eval("TaskID").ToString())%>");'>修改</span>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" Font-Bold="true" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                                </telerik:GridTemplateColumn>

                                <telerik:GridTemplateColumn UniqueName="Delete" Visible="false">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButtonDelete" CommandName="ButtonDelete" OnClientClick="return confirm('您确定要删除么?')"
                                            runat="server">删除</asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
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
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="Model.ApplyCheckTaskMDL"
                    SelectMethod="GetList" TypeName="DataAccess.ApplyCheckTaskDAL"
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
        <div id="winpop"></div>
    </form>
</body>
</html>
