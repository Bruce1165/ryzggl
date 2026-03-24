<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PublicityLook.aspx.cs" Inherits="ZYRYJG.County.PublicityLook" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

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
            <div style="width: 99%; margin: 5px auto; padding-bottom: 30px;">
                <div id="div_top" class="dqts">
                    <div style="float: left;">
                        当前位置 &gt;&gt;建造师注册管理 &gt;&gt;<strong>公示</strong>
                    </div>
                </div>
                <div class="content">                    
                    <div style="width: 95%; height: 100%; margin: 10px auto; text-align: center;">
                        <table>
                            <tr>
                                <td>公示批次号：
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="RadTextBoxPublicCode" runat="server" Skin="Telerik"></telerik:RadTextBox>
                                </td>
                                <td>
                                    <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
                                </td>
                                <td style="width: 30%" align="right">
                                  <%--  <input id="Button1" type="button" class="button" value="添 加" onclick='javascript: SetIfrmSrc("PublicityChoice.aspx");'  />--%>
                                </td>
                            </tr>
                        </table>
                        <div style="width: 98%; text-align: center; clear: both;">

                            <telerik:RadGrid ID="RadGridHZSB" runat="server"
                                GridLines="None" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" PageSize="15"
                                Skin="Blue" EnableAjaxSkinRendering="False" EnableEmbeddedSkins="False"
                                SortingSettings-SortToolTip="单击进行排序" Width="100%" OnItemCommand="RadGridHZSB_ItemCommand" PagerStyle-AlwaysVisible="true" >
                                <ClientSettings EnableRowHoverStyle="true">
                                </ClientSettings>
                                <MasterTableView NoMasterRecordsText=" 没有可显示的记录" DataKeyNames="PublicCode,ApplyType" CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Left">
<CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                                    <Columns>
                                        <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="PublicCode" DataField="PublicCode" HeaderText="公示批次号">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="ApplyType" DataField="ApplyType" HeaderText="事项名称">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName=" Num" DataField=" Num" HeaderText="人员数量">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="public" DataField="public" HeaderText="上报状态">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="等级" UniqueName="PSN_Level" DataField="PSN_Level">
                                             <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="审核结果" UniqueName="ConfirmResult" DataField="ConfirmResult">
                                             <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn UniqueName="Edit" HeaderText="操作">
                                            <ItemTemplate>
                                                <a class="link" style="color: #0000ff" target="_blank" href="<%# ZYRYJG.UIHelp.ShowFile(string.Format("~/Upload/ReportXls/Excel{0}.xls",Eval("PublicCode")))%>"  title="点击下载或鼠标右键另存">导出</a>
                                                <input id="Button1" runat="server" type="button" value="修改" class="link" onclick='<%# string.Format("javascript:SetIfrmSrc(\"PublicityChoice.aspx?01={0}&02={1}&03=xg&04={2}&05={3}\");return false;",Eval("PublicCode"),Eval("ApplyType"),Eval("PSN_Level"),Eval("ConfirmResult"))%>' visible='<%#Eval("public").ToString()=="未公示" %>' />
                                        <asp:Button ID="Button3" runat="server" Text="公示" CssClass="link" CommandName="report" OnClientClick="javascript:if(confirm('您确认要公示吗？')==false) return false;" Visible='<%#Eval("public").ToString()=="未公示" %> ' />
                                            <asp:Button ID="Button5" runat="server" Text="取消" CssClass="link" CommandName="Cancelreport" OnClientClick="javascript:if(confirm('您确认要取消重新选择吗？')==false) return false;" Visible='<%#Eval("public").ToString()=="未公示" %> ' />
                                                                                                                                        
                                         <input id="Button6" runat="server" type="button" value="详细" class="link" onclick='<%# string.Format("javascript:SetIfrmSrc(\"PublicityChoice.aspx?01={0}&02={1}&03=xx\");return false;",Eval("PublicCode"),Eval("ApplyType"))%>' visible='<%#Eval("public").ToString()=="公示中" %>' />
                                                 <%--<input id="Button4" runat="server" type="button" value="申述" class="link" onclick='<%# string.Format("javascript:SetIfrmSrc(\"PublicityModify.aspx?01={0}&02={1}&03=xx\");return false;",Eval("PublicCode"),Eval("ApplyType"))%>' visible='<%#Eval("public").ToString()=="公示中" && Eval("ConfirmResult").ToString()=="不通过"%>' />--%>
                                                <input id="Button4" runat="server" type="button" value="申述" class="link" onclick='<%# string.Format("javascript:SetIfrmSrc(\"PublicityModify.aspx?01={0}&02={1}&03={2}\");return false;",Eval("PublicCode"),Eval("ApplyType"),Eval("ConfirmResult"))%>' visible='<%#Eval("public").ToString()=="公示中"%>' />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Left" Wrap="false" ForeColor="Blue" />
                                        </telerik:GridTemplateColumn>
                                    </Columns>

<PagerStyle AlwaysVisible="True"></PagerStyle>

                                    <HeaderStyle Font-Bold="True" />

<CommandItemStyle HorizontalAlign="Left"></CommandItemStyle>
                                    <PagerTemplate>
                                        <uc1:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                                    </PagerTemplate>
                                </MasterTableView>

<PagerStyle AlwaysVisible="True"></PagerStyle>

                                <FilterMenu EnableEmbeddedSkins="False">
                                </FilterMenu>
                                <SortingSettings SortToolTip="单击进行排序"></SortingSettings>
                            </telerik:RadGrid>
                            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="Model.ApplyMDL"
                                SelectMethod="GetPublicList" TypeName="DataAccess.ApplyDAL"
                                SelectCountMethod="SelectPublicCount" EnablePaging="true"
                                MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex" SortParameterName="orderBy" >
                                <SelectParameters>
                                    <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                                        DefaultValue="" ConvertEmptyStringToNull="false" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <uc2:IframeView ID="IframeView" runat="server" />
    </form>
</body>
</html>
