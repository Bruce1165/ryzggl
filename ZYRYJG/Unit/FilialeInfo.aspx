<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FilialeInfo.aspx.cs" Inherits="ZYRYJG.Unit.FilialeInfo" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" /> 
    <link href="../Skins/Blue/Grid.Blue.css?v=1.008" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
    <%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc1" %>
    <%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc2" %>
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
            <div  id="div_top" class="dqts">
                <div style="float: left;">
                    当前位置 &gt;&gt;信息查看 &gt;&gt;<strong>分公司信息</strong>
                </div>
            </div>
            <div class="content">
                <p class="jbxxbt">
                    分公司信息
                </p>
                <table class="cx" width="99%" border="0" align="center" cellspacing="1">
                    <tr id="TrPerson" runat="server">
                        <td width="100px" align="right" nowrap="nowrap">
                            <telerik:RadComboBox ID="RadComboBoxIten" runat="server">
                                <Items>
                                    <telerik:RadComboBoxItem Text="分支机构名称" Value="QYMC" />
                                      <telerik:RadComboBoxItem Text="组织机构代码" Value="ZZJGDM" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <td align="left" width="200px">
                            <telerik:RadTextBox ID="RadTextBoxValue" runat="server" Width="97%" Skin="Default">
                            </telerik:RadTextBox>
                        </td>
                        <td align="left">
                            <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
                        </td>
                    </tr>
                </table>
                  <telerik:RadGrid ID="RadGridQY" runat="server"
                            GridLines="None" AllowPaging="false" AllowSorting="true" AutoGenerateColumns="False"
                            SortingSettings-SortToolTip="单击进行排序" Width="100%"  Skin="Blue" EnableAjaxSkinRendering="False" EnableEmbeddedSkins="False" PagerStyle-AlwaysVisible="true">
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
                            <telerik:GridBoundColumn UniqueName="QYMC" DataField="QYMC" HeaderText="分支机构名称">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="ZZJGDM" DataField="ZZJGDM" HeaderText="组织机构代码">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="HYLZGX" DataField="HYLZGX" HeaderText="行业隶属关系">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="jz1" HeaderText="建造师一级">
                                <ItemTemplate>
                                  <a href='YJZJS.aspx?a=<%#Eval("ZZJGDM")%>'><%#Eval("jz1")%></a>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                 <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                            </telerik:GridTemplateColumn>
                              <telerik:GridTemplateColumn UniqueName="jz1L" HeaderText="建造师一级临时">
                                <ItemTemplate>
                                  <a href='YJZJS_LS.aspx?a=<%#Eval("ZZJGDM")%>'><%#Eval("jz1L")%></a>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                 <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                            </telerik:GridTemplateColumn>
                              <telerik:GridTemplateColumn UniqueName="jz2" HeaderText="建造师二级">
                                <ItemTemplate>
                                  <a href='EJZJS.aspx?a=<%#Eval("ZZJGDM")%>'><%#Eval("jz2")%></a>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                 <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                            </telerik:GridTemplateColumn>
                              <telerik:GridTemplateColumn UniqueName="jz2L" HeaderText="建造师二级临时">
                                <ItemTemplate>
                                  <a href='EJZJS_LS.aspx?a=<%#Eval("ZZJGDM")%>'><%#Eval("jz2L")%></a>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                 <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                            </telerik:GridTemplateColumn>
                              <telerik:GridTemplateColumn UniqueName="jls" HeaderText="监理师">
                                <ItemTemplate>
                                  <a href='ZCJLS.aspx?a=<%#Eval("ZZJGDM")%>'><%#Eval("jl")%></a>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                 <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                            </telerik:GridTemplateColumn>
                              <telerik:GridTemplateColumn UniqueName="zjs" HeaderText="造价师">
                                <ItemTemplate>
                                  <a href='ZCZJS.aspx?a=<%#Eval("ZZJGDM")%>'><%#Eval("zj")%></a>
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
                <br />
            </div>
        </div>
    </form>
</body>
</html>
