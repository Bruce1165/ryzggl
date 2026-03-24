<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Notification.aspx.cs" Inherits="ZYRYJG.County.Notification" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" />
     <link href="../Skins/Blue/Grid.Blue.css?v=1.008" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
    <link href="../layer/skin/layer.css" rel="stylesheet" />
    <script src="../layer/layer.js" type="text/javascript"></script>
    <%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc1" %>
    <%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc2" %>
    <style type="text/css">
        .link {
            border: none;
            color: blue;
            background-color: transparent;
            cursor: pointer;
            font-size: 12px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server">
        </telerik:RadStyleSheetManager>
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Windows7">
        </telerik:RadWindowManager>
        <div class="div_out">
            <div  style="width: 99%; margin: 5px auto; padding-bottom: 30px;">
                <div id="div_top" class="dqts">
                    <div style="float: left;">
                        当前位置 &gt;&gt;建造师注册管理 &gt;&gt;<strong>发送注册办结通知</strong>
                    </div>
                </div>
                <div class="content">
                    <p class="jbxxbt">
                        发送注册办结通知
                    </p>
                    <div style="width: 95%; height: 100%; margin: 10px auto; text-align: center;">
                        <table width="98%" border="0" align="center" cellspacing="1">
                            <tr id="TrPerson" runat="server">
                                <td style="width: 100px" align="left" nowrap="nowrap">
                                    <telerik:RadComboBox ID="RadComboBoxIten" runat="server">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="全部" Value="全部" />
                                            <telerik:RadComboBoxItem Text="初始注册" Value="初始注册" />
                                            <telerik:RadComboBoxItem Text="重新注册" Value="重新注册" />
                                            <telerik:RadComboBoxItem Text="增项注册" Value="增项注册" />
                                            <telerik:RadComboBoxItem Text="延续注册" Value="延期注册" />
                                            <%--<telerik:RadComboBoxItem Text="变更注册" Value="变更注册" />--%>
                                            <%-- <telerik:RadComboBoxItem Text="遗失补办" Value="遗失补办" />--%>
                                            <%-- <telerik:RadComboBoxItem Text="注销" Value="注销" />--%>
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td style="width: 100px" align="right">公告号：
                                </td>
                                <td style="width: 150px" align="left">
                                    <telerik:RadTextBox ID="RadTextBoxValue" runat="server" Width="97%" Skin="Default">
                                    </telerik:RadTextBox>
                                </td>
                                <td align="left">
                                    <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
                                </td>
                                <td align="left">
                                    <asp:RadioButtonList ID="RadioButtonListReportStatus" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="RadioButtonListReportStatus_SelectedIndexChanged">
                                        <asp:ListItem Text="未发送" Value="未发送" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="已发送" Value="已发送"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                        <telerik:RadGrid ID="RadGridFSTZ" runat="server"
                            GridLines="None" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                            SortingSettings-SortToolTip="单击进行排序" Width="98%" Skin="Blue" EnableAjaxSkinRendering="False"
                            EnableEmbeddedSkins="False" PagerStyle-AlwaysVisible="true" OnItemCommand="RadGridFSTZ_ItemCommand">
                            <ClientSettings EnableRowHoverStyle="true">
                            </ClientSettings>
                            <HeaderContextMenu EnableEmbeddedSkins="False">
                            </HeaderContextMenu>
                            <MasterTableView NoMasterRecordsText=" 没有可显示的记录" DataKeyNames="NoticeCode,ApplyType" CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Left">
                                <Columns>
                                    <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="NoticeCode" DataField="NoticeCode" HeaderText="公告号">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="ApplyType" DataField="ApplyType" HeaderText="申请类型">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="NUM" DataField="NUM" HeaderText="人数">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="GetDateTime" DataField="GetDateTime" HeaderText="发送时间" HtmlEncode="false" DataFormatString="{0:yyyy.MM.dd}">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn UniqueName="Edit" HeaderText="操作">
                                        <ItemTemplate>
                                            <asp:Button ID="BtnTop" runat="server" Text="发送通知" Visible='<%#Eval("GetDateTime").ToString()=="" %>' CssClass="link" CommandName="Top" OnClientClick="javascript:if(confirm('您确认此操作？')==false) return false;" />
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
                        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="Model.ApplyMDL"
                            SelectMethod="GetNotificationList" TypeName="DataAccess.ApplyDAL"
                            SelectCountMethod="SelectNotificationCount" EnablePaging="true"
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
    </form>
</body>
</html>
