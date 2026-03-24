<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BusinessAccomplish.aspx.cs" Inherits="ZYRYJG.County.BusinessAccomplish" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../css/Style3.css" rel="stylesheet" type="text/css" />
    <link href="../css/Grid.Blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Public.js" type="text/javascript"></script>
    <%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc1" %>
    <%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc2" %>


</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server">
        </telerik:RadStyleSheetManager>
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Sunset">
        </telerik:RadWindowManager>
        <div class="div_out">
            <div class="table_border" style="width: 99%; margin: 5px auto; padding-bottom: 30px;">
                <div class="dqts">
                    <div style="float: left;">
                        <img alt="" src="../Images/ts.gif" width="17" height="11" />当前位置 &gt;&gt;业务管理 &gt;&gt;<strong>已办业务</strong>
                    </div>
                </div>
                <div class="content">
                    <p class="jbxxbt">
                        已办业务
                    </p>
                    <div style="width: 95%; margin: 10px auto; text-align: center;">
                        <table width="98%" border="0" align="center" cellspacing="1">
                            <tr id="TrPerson" runat="server">
                                <td width="100px" align="right" nowrap="nowrap">
                                    <telerik:RadComboBox ID="RadComboBoxIten" runat="server">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="业务流水号" Value="ApplyCode" />
                                            <telerik:RadComboBoxItem Text="姓名" Value="PSN_Name" />
                                            <telerik:RadComboBoxItem Text="证件号" Value="PSN_CertificateNO" />
                                            <telerik:RadComboBoxItem Text="注册编号" Value="PSN_RegisterNO" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td align="left" width="200px">
                                    <telerik:RadTextBox ID="RadTextBoxValue" runat="server" Width="97%" Skin="Default">
                                    </telerik:RadTextBox>
                                </td>
                                <td align="left">
                                    <asp:Button ID="ButtonSearch" runat="server" Height="25px" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
                                </td>
                            </tr>
                        </table>
                        <telerik:RadGrid ID="RadGridYBYW" runat="server"
                            GridLines="None" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                            SortingSettings-SortToolTip="单击进行排序" Width="98%" Skin="Default" EnableAjaxSkinRendering="true"
                            EnableEmbeddedSkins="true" PagerStyle-AlwaysVisible="true">
                            <ClientSettings EnableRowHoverStyle="true">
                            </ClientSettings>
                            <HeaderContextMenu EnableEmbeddedSkins="False">
                            </HeaderContextMenu>
                            <MasterTableView NoMasterRecordsText=" 没有可显示的记录" CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Left">
                                <Columns>
                                    <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="ApplyCode" DataField="ApplyCode" HeaderText="业务流水号">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <%-- <telerik:GridBoundColumn UniqueName="BB" DataField="BB" HeaderText="人员类型">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>--%>
                                    <telerik:GridBoundColumn UniqueName="PSN_Name" DataField="PSN_Name" HeaderText="姓名">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn UniqueName="PSN_CertificateNO" HeaderText="证件号码">
                                        <ItemTemplate>
                                            <%# Eval("PSN_CertificateType") %>：<%# Eval("PSN_CertificateNO") %>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn UniqueName="ENT_Name" DataField="ENT_Name" HeaderText="企业名称">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="PSN_RegisteProfession" DataField="PSN_RegisteProfession" HeaderText="注册专业">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="PSN_RegisterNO" DataField="PSN_RegisterNO" HeaderText="注册编号">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="ApplyType" DataField="ApplyType" HeaderText="申报事项">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="ApplyStatus" DataField="ApplyStatus" HeaderText="办理阶段">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <%--    <telerik:GridBoundColumn UniqueName="GetDateTime" DataField="GetDateTime" HeaderText="办理时间">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>--%>
                                    <%--<telerik:GridBoundColumn UniqueName="GetMan" DataField="GetMan" HeaderText="办理人">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>--%>

                                    <telerik:GridTemplateColumn UniqueName="Edit" HeaderText="操作">
                                        <ItemTemplate>
                                            <span class="link_edit" onclick='javascript:SetIfrmSrc("BusinessAccomplishDetail.aspx?a=<%# Eval("ApplyID") %>"); '>预览</span>
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
            </div>
        </div>
        <uc2:IframeView ID="IframeView" runat="server" />
    </form>
</body>
</html>
