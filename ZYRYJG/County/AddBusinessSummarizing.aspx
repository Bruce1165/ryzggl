<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddBusinessSummarizing.aspx.cs" Inherits="ZYRYJG.County.AddBusinessSummarizing" %>

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
                <div id="div_top" class="dqts">
                    <div style="float: left;">
                        <img alt="" src="../Images/ts.gif" width="17" height="11" />当前位置 &gt;&gt;业务管理 &gt;&gt;汇总上报 &gt;&gt;<strong>人员添加</strong>
                    </div>
                </div>
                <div class="content">
                    <p class="jbxxbt">
                        汇总上报信息
                    </p>
                    <table>
                        <tr>
                            <td style="text-align: right">上报所属事项</td>
                            <td>
                                <telerik:RadComboBox ID="RadComboBoxIfContinue1" runat="server">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="请选择" Value="" />
                                        <telerik:RadComboBoxItem Text="初始注册" Value="初始注册" />
                                        <telerik:RadComboBoxItem Text="重新注册" Value="重新注册" />
                                        <telerik:RadComboBoxItem Text="初始注册" Value="初始注册" />
                                        <telerik:RadComboBoxItem Text="重新注册" Value="重新注册" />
                                        <telerik:RadComboBoxItem Text="变更注册" Value="变更注册" />
                                        <telerik:RadComboBoxItem Text="延期（逾期）注册" Value="延期（逾期）注册" />
                                        <telerik:RadComboBoxItem Text="注销注册" Value="注销注册" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">上报时间</td>
                            <td>
                                <telerik:RadDatePicker ID="RadDatePicker1" runat="server" Width="180px"></telerik:RadDatePicker>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">批次编号</td>
                            <td>
                                <telerik:RadTextBox ID="RadTextBox3" runat="server" Width="150px" Text="HD115042011" Enabled="false"></telerik:RadTextBox></td>
                        </tr>
                        <tr>
                            <td style="text-align: right">人员数量</td>
                            <td>
                                <telerik:RadTextBox ID="RadTextBox4" runat="server" Width="150px" Enabled="false" ></telerik:RadTextBox></td>
                        </tr>
                    </table>
                    <div style="width: 95%; margin: 10px auto; text-align: center;">
                        <telerik:RadGrid ID="RadGridRYXX" runat="server"
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
                                    <telerik:GridBoundColumn UniqueName="AA" DataField="AA" HeaderText="姓名">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="BB" DataField="BB" HeaderText="证件号">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="CC" DataField="CC" HeaderText="组织机构代码">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="DD" DataField="DD" HeaderText="企业名称">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="EE" DataField="EE" HeaderText="注册专业">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="FF" DataField="FF" HeaderText="注册编号">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="GG" DataField="GG" HeaderText="证书编号">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    
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
                        <p style="text-align:right;width: 99%">
                            <asp:Button ID="BtnSave" runat="server" Text="提 交" CssClass="button"/>
                            &nbsp;&nbsp; 
                            <input id="Button1" type="button" class="button" value="选择人员" onclick='javascript: location("ChoiceBusinessSummarizing.aspx")' />
                        </p>
                    </div>

                </div>
            </div>
        </div>
    </form>
</body>
</html>
