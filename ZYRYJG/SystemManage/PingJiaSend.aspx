<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PingJiaSend.aspx.cs" Inherits="ZYRYJG.SystemManage.PingJiaSend" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript" src="../Scripts/jquery-3.4.1.min.js"></script>
    <link href="../layer/skin/layer.css" rel="stylesheet" />
    <script src="../layer/layer.js" type="text/javascript"></script>
    <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" />
    <link href="../Skins/Blue/Grid.Blue.css?v=1.008" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:radstylesheetmanager id="RadStyleSheetManager1" runat="server">
        </telerik:radstylesheetmanager>
        <telerik:radscriptmanager id="RadScriptManager1" runat="server">
        </telerik:radscriptmanager>
        <telerik:radwindowmanager id="RadWindowManager1" runat="server" skin="Windows7">
        </telerik:radwindowmanager>
        <telerik:radajaxmanager id="RadAjaxManager1" runat="server" defaultloadingpanelid="RadAjaxLoadingPanel1"
            enableajax="true">
            <AjaxSettings>
               
            </AjaxSettings>
        </telerik:radajaxmanager>
        <telerik:radajaxloadingpanel id="RadAjaxLoadingPanel1" runat="server" visible="true" skin="Default" />
        <div class="div_out">
            <div class="dqts">
                <div style="float: left;">
                    当前位置 &gt;&gt; 系统管理 &gt;&gt; 
                <strong>历史数据评价推送（临时开放）</strong>
                </div>
            </div>
            <div class="content" runat="server" id="DivMain">
                <table class="cx" width="98%" border="0" align="center" cellspacing="5">
                    <tr>
                        <td align="right" nowrap="nowrap" width="8%">业务类型：
                        </td>
                        <td align="left" width="60%">
                            <asp:RadioButtonList ID="RadioButtonListDataType" runat="server" RepeatDirection="Horizontal"
                                AutoPostBack="false" Style="float: left;" >
                                <asp:ListItem Value="" Selected="True">全部</asp:ListItem>
                                <asp:ListItem Value="change">从业变更</asp:ListItem>
                                <asp:ListItem Value="exam">从业考核</asp:ListItem>
                                <asp:ListItem Value="continue">从业续期</asp:ListItem>
                                <asp:ListItem Value="JinJing">从业进京</asp:ListItem>
                                <asp:ListItem Value="CertMore">从业增发</asp:ListItem>
                                <asp:ListItem Value="apply">二建注册</asp:ListItem>
                                <asp:ListItem Value="applyZJS">二造注册</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td align="right" nowrap="nowrap" width="8%">状态：
                        </td>
                        <td align="left" width="20%">
                            <asp:RadioButtonList ID="RadioButtonListStatus" runat="server" RepeatDirection="Horizontal"
                                AutoPostBack="false" Style="float: left;" >
                                <asp:ListItem Value="" Selected="True">全部</asp:ListItem>
                                <asp:ListItem Value="未推送">未推送</asp:ListItem>
                                <asp:ListItem Value="已推送">已推送</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td align="left">
                            <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />

                        </td>
                    </tr>
                </table>
                <div class="table_cx">
                    <img alt="" src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                    企业列表
                </div>
                <div style="width: 98%; margin: 0 auto; padding-bottom: 10px;">

                    <telerik:radgrid id="RadGrid1" runat="server"
                        gridlines="None" allowpaging="True" allowsorting="True" autogeneratecolumns="False" pagesize="15"
                        sortingsettings-sorttooltip="单击进行排序" width="100%" skin="Blue" enableajaxskinrendering="False" enableembeddedskins="False" pagerstyle-alwaysvisible="true">
                        <ClientSettings EnableRowHoverStyle="true">
                        </ClientSettings>
                        <HeaderContextMenu EnableEmbeddedSkins="False">
                        </HeaderContextMenu>
                        <MasterTableView NoMasterRecordsText=" 没有可显示的记录" CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Left"
                            DataKeyNames="">
                            <Columns>
                                <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="DataType" DataField="DataType" HeaderText="DataType">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="DataID" DataField="DataID" HeaderText="DataID">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="ApplyType" DataField="ApplyType" HeaderText="ApplyType">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                               
                                <telerik:GridBoundColumn UniqueName="DoTime" DataField="DoTime" HeaderText="申报日期" HtmlEncode="false" DataFormatString="{0:yyyy.MM.dd HH:mm:ss}">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn UniqueName="progress" HeaderText="评价">
                                    <ItemTemplate>
                                          <%# string.Format("<span onclick=\"window.open('./pingjiaresult.aspx?t={0}&o={1}'); \" style=\"cursor:pointer;color:blue\">评价</span>", Page.Server.UrlEncode(Utility.Cryptography.Encrypt(Eval("DataType").ToString())), Page.Server.UrlEncode(Utility.Cryptography.Encrypt(Eval("DataID").ToString())))%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
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
                    </telerik:radgrid>

                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="Model.ApplyMDL"
                        SelectMethod="GetList_temp_ping" TypeName="DataAccess.ApplyDAL"
                        SelectCountMethod="SelectCount_temp_ping" EnablePaging="true"
                        MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex" SortParameterName="orderBy">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                                DefaultValue="" ConvertEmptyStringToNull="false" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
