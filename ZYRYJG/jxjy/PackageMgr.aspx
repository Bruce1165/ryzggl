<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="PackageMgr.aspx.cs" Inherits="ZYRYJG.jxjy.PackageMgr" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc1" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<%@ Register Src="../PostSelect.ascx" TagName="PostSelect" TagPrefix="uc3" %>
<%@ Register Src="~/PostAllSelect.ascx" TagPrefix="uc4" TagName="PostAllSelect" %>

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
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="RadGridPackage">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGridPackage" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="ButtonSearch">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGridPackage" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="ButtonPublish">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGridPackage" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="ButtonUnPublish">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGridPackage" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Windows7">
        </telerik:RadAjaxLoadingPanel>
        <div class="div_out">
            <div id="div_top" class="dqts">
                <div style="float: left;">
                    当前位置 &gt;&gt;在线公益教育培训 &gt;&gt;<strong>培训计划管理</strong>
                </div>
            </div>
            <div class="content">
                <table class="cx" width="98%" border="0" align="center" cellspacing="5">
                    <tr>
                      
                         <td align="right" width="10%" nowrap="nowrap">
                           适用人员专业：
                        </td>
                        <td align="left" width="30%">   
                            <uc4:PostAllSelect runat="server" ID="PostSelect1" />
                        </td>                         
                  
                          <td align="right" width="10%" nowrap="nowrap">发布状态：
                        </td>
                        <td align="left" width="30%">
                            <asp:RadioButtonList ID="RadioButtonListPublishStatus" runat="server" RepeatDirection="Horizontal" AutoPostBack="false">
                                <asp:ListItem Value="" Selected="True">全部</asp:ListItem>
                                <asp:ListItem Value="已发布">已发布</asp:ListItem>
                                <asp:ListItem Value="未发布">未发布</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td align="left">
                            <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
                        </td>
                    </tr>
                </table>
               <div class="table_cx"><img src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                    培训计划列表
                </div>
                <div style="width: 99%; margin: 8px auto;">
                    <telerik:RadGrid ID="RadGridPackage" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="10"
                        OnDeleteCommand="RadGridPackage_DeleteCommand" EnableAjaxSkinRendering="false" OnItemDataBound="RadGridPackage_ItemDataBound"
                        EnableEmbeddedSkins="false" Skin="Blue" Width="100%" >
                        <ClientSettings EnableRowHoverStyle="true">
                        </ClientSettings>
                        <MasterTableView CommandItemDisplay="Top" DataKeyNames="PackageID,Status,PackageTitle" NoMasterRecordsText="　没有可显示的记录">
                            <CommandItemTemplate>
                                <div class="grid_CommandBar">
                                    <input type="button" value=" " class="rgAdd" onclick="javascript: SetIfrmSrc('PackageEdit.aspx');" />
                                    <nobr onclick="javascript:SetIfrmSrc('PackageEdit.aspx');" class="grid_CmdButton">添加</nobr>
                                </div>
                            </CommandItemTemplate>
                            <Columns>                              
                                <telerik:GridBoundColumn HeaderText="行号" UniqueName="RowNum" DataField="RowNum">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>                             
                                 <telerik:GridTemplateColumn HeaderText="适用人员专业" UniqueName="PostTypeName">
                                    <ItemTemplate>
                                        <%#Eval("PostTypeName")%><%# Eval("PostName")==DBNull.Value?"":Eval("PostName")%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" Font-Bold="true" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="要求课时" UniqueName="Period">
                                    <ItemTemplate>
                                        <%# Convert.ToInt32(Eval("Period"))/45==0?"":string.Format("{0}学时",Convert.ToString(Convert.ToInt32(Eval("Period"))/45))%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" Font-Bold="true" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                </telerik:GridTemplateColumn>
                                  <telerik:GridTemplateColumn HeaderText="课程数量" UniqueName="SourceCount">
                                    <ItemTemplate>                                      
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" Font-Bold="true" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn HeaderText="发布状态" UniqueName="Status" DataField="Status">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn UniqueName="Edit">
                                    <ItemTemplate>
                                        <span class="link_edit" onclick='javascript:SetIfrmSrc("PackageEdit.aspx?o=<%# Eval("PackageID") %>");'>编辑</span>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridButtonColumn UniqueName="Delete" ButtonType="LinkButton" CommandName="Delete"
                                    ConfirmText="确认删除？" ConfirmDialogType="RadWindow" Text="删除">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                                </telerik:GridButtonColumn>
                            </Columns>
                            <HeaderStyle Font-Bold="true" />
                            <PagerStyle AlwaysVisible="true" />
                            <PagerTemplate>
                                <uc2:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                            </PagerTemplate>
                        </MasterTableView>
                    </telerik:RadGrid>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="DataAccess.PackageDAL"
                        DataObjectTypeName="Model.PackageMDL" SelectMethod="GetList" EnablePaging="true"
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
        <div id="winpop">
        </div>
        <uc1:IframeView ID="IframeView" runat="server" />
    </form>
</body>
</html>
