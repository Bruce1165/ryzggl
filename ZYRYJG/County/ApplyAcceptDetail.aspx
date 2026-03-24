<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplyAcceptDetail.aspx.cs" Inherits="ZYRYJG.County.ApplyAcceptDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" /> 
    
    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Windows7">
        </telerik:RadWindowManager>
        <div class="div_out">
            <div class="table_border" style="width: 99%; margin: 5px auto; padding-bottom: 30px;">
                <div id="div_top" class="dqts">
                    <div style="float: left;">
                        当前位置 &gt;&gt;业务管理 &gt;&gt;收件复核
                    </div>
                </div>
                <div class="content">
                    <div class="jbxxbt">
                        收件复核
                    </div>
                    <div style="width: 100%; margin: 10px auto; text-align: center; clear: both;">
                        <div id="divInfo" runat="server" style="width: 98%; text-align: left; margin: 12px 0 12px 0;  font-size:14px;  font-weight:bold;padding-left:20px"></div>
                        <div style="width: 100%; clear: both; position: relative;">
                            <telerik:RadGrid ID="RadGridADDRY" runat="server" Height="300"
                                GridLines="None" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" PageSize="500"
                                SortingSettings-SortToolTip="单击进行排序" Width="100%" Skin="Default" EnableAjaxSkinRendering="true" 
                                EnableEmbeddedSkins="true" PagerStyle-AlwaysVisible="true">
                                <ClientSettings EnableRowHoverStyle="true">
                                    <Scrolling AllowScroll="True" UseStaticHeaders="true"></Scrolling>
                                </ClientSettings>
                                <HeaderContextMenu EnableEmbeddedSkins="False">
                                </HeaderContextMenu>
                                <MasterTableView NoMasterRecordsText=" 没有可显示的记录" DataKeyNames="ApplyID,ReportMan" CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Left">
                                    <Columns>
                                        <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                            <HeaderStyle HorizontalAlign="Left" Width="46" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="PSN_Name" DataField="PSN_Name" HeaderText="姓名">
                                            <HeaderStyle HorizontalAlign="Left" Width="92" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="PSN_CertificateNO" DataField="PSN_CertificateNO" HeaderText="证件号码">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="ENT_Name" DataField="ENT_Name" HeaderText="企业名称">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="PSN_RegisteProfession" DataField="PSN_RegisteProfession" HeaderText="注册专业">
                                            <HeaderStyle HorizontalAlign="Left" Width="92" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="PSN_RegisterNo" DataField="PSN_RegisterNo" HeaderText="注册编号">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="ApplyType" DataField="ApplyType" HeaderText="申报事项">
                                            <HeaderStyle HorizontalAlign="Left" Width="92" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="ApplyTime" DataField="ApplyTime" HeaderText="申请时间" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}">
                                            <HeaderStyle HorizontalAlign="Left" Width="92" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>

                                    </Columns>
                                    <HeaderStyle Font-Bold="True" Wrap="false" />
                                    <PagerTemplate>
                                        <uc1:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                                    </PagerTemplate>
                                </MasterTableView>
                                <FilterMenu EnableEmbeddedSkins="False">
                                </FilterMenu>
                                <SortingSettings SortToolTip="单击进行排序"></SortingSettings>
                            </telerik:RadGrid>
                            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="Model.ApplyMDL"
                                SelectMethod="GetList" TypeName="DataAccess.ApplyDAL"
                                SelectCountMethod="SelectCount" EnablePaging="true"
                                MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex" SortParameterName="orderBy">
                                <SelectParameters>
                                    <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                                        DefaultValue="" ConvertEmptyStringToNull="false" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </div>

                    </div>

                    <div style="clear: both; position: relative; width: 100%; text-align: center; margin: 8px 0">
                        <asp:Button ID="ButtonAccept" runat="server" Text="收 件" CssClass="bt_large" OnClick="ButtonAccept_Click" />
                        &nbsp;&nbsp; 
                             <input id="Button2" type="button" class="bt_large" value="返 回" onclick='javascript: hideIfam();' />

                    </div>

                </div>
            </div>
        </div>
    </form>
</body>
</html>
