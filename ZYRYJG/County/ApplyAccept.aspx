<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplyAccept.aspx.cs" Inherits="ZYRYJG.County.ApplyAccept" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" /> 
    
    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
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
            <div class="table_border" style="width: 99%; margin: 5px auto; padding-bottom: 30px;">
                <div class="dqts">
                    <div style="float: left;">
                        当前位置 &gt;&gt;业务管理 &gt;&gt;<strong>收件复核</strong>
                    </div>
                </div>
                <div class="content">
                    <p class="jbxxbt">
                        收件复核
                    </p>

                    <table width="98%" border="0" align="center" cellspacing="1">
                        <tr id="TrPerson" runat="server">
                            <td align="right" width="100px">所属区县：</td>
                            <td width="100px" align="left" nowrap="nowrap">
                                <telerik:RadComboBox ID="RadComboBoxENT_City" runat="server">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="全部" Value="" />
                                        <telerik:RadComboBoxItem Text="东城区" Value="东城区" />
                                        <telerik:RadComboBoxItem Text="西城区" Value="西城区" />
                                        <telerik:RadComboBoxItem Text="朝阳区" Value="朝阳区" />
                                        <telerik:RadComboBoxItem Text="海淀区" Value="海淀区" />
                                        <telerik:RadComboBoxItem Text="丰台区" Value="丰台区" />
                                        <telerik:RadComboBoxItem Text="石景山区" Value="石景山区" />
                                        <telerik:RadComboBoxItem Text="昌平区" Value="昌平区" />
                                        <telerik:RadComboBoxItem Text="通州区" Value="通州区" />
                                        <telerik:RadComboBoxItem Text="顺义区" Value="顺义区" />
                                        <telerik:RadComboBoxItem Text="门头沟区" Value="门头沟区" />
                                        <telerik:RadComboBoxItem Text="房山区" Value="房山区" />
                                        <telerik:RadComboBoxItem Text="大兴区" Value="大兴区" />
                                        <telerik:RadComboBoxItem Text="怀柔区" Value="怀柔区" />
                                        <telerik:RadComboBoxItem Text="平谷区" Value="平谷区" />
                                        <telerik:RadComboBoxItem Text="密云区" Value="密云" />
                                        <telerik:RadComboBoxItem Text="延庆区" Value="延庆" />
                                        <telerik:RadComboBoxItem Text="亦庄" Value="亦庄" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td align="right" width="100px">申报事项：                                    
                            </td>
                            <td width="140px" align="left" nowrap="nowrap">

                                <telerik:RadComboBox ID="RadComboBoxApplyType" runat="server">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="全部" Value="" />
                                        <telerik:RadComboBoxItem Text="初始注册" Value="初始注册" />
                                        <telerik:RadComboBoxItem Text="变更注册" Value="变更注册" />
                                        <telerik:RadComboBoxItem Text="延续注册" Value="延期注册" />
                                        <telerik:RadComboBoxItem Text="增项注册" Value="增项注册" />
                                        <telerik:RadComboBoxItem Text="重新注册" Value="重新注册" />
                                        <telerik:RadComboBoxItem Text="遗失补办" Value="遗失补办" />
                                        <telerik:RadComboBoxItem Text="注销" Value="注销" />
                                    </Items>
                                </telerik:RadComboBox>

                            </td>
                            <td align="left" width="100px" style="padding-left:20px">
                                <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="bt_large" OnClick="ButtonSearch_Click" />
                            </td>
                            <td></td>
                        </tr>

                    </table>
                    <div style="width: 98%; text-align: center; clear: both;">

                        <telerik:RadGrid ID="RadGridHZSB" runat="server"
                            GridLines="None" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" PageSize="20"
                            SortingSettings-SortToolTip="单击进行排序" Width="100%" Skin="Default" EnableAjaxSkinRendering="true"
                            EnableEmbeddedSkins="true" PagerStyle-AlwaysVisible="true">
                            <ClientSettings EnableRowHoverStyle="true">
                            </ClientSettings>
                            <MasterTableView NoMasterRecordsText=" 没有可显示的记录" DataKeyNames="ReportDate,ApplyType,ENT_City,ReportCode" CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Left">
                                <Columns>
                                    <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                      <telerik:GridBoundColumn UniqueName="ReportCode" DataField="ReportCode" HeaderText="上报编号">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    
                                    <telerik:GridBoundColumn UniqueName="ReportDate" DataField="ReportDate" HeaderText="上报日期" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="ENT_City" DataField="ENT_City" HeaderText="所属区县">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="ApplyType" DataField="ApplyType" HeaderText="事项名称">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn UniqueName="ManCount" DataField="ManCount" HeaderText="人员数量">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn UniqueName="Edit" HeaderText="操作">
                                        <ItemTemplate>
                                            <input id="Button1" runat="server" type="button" value="收件复核" class="link" onclick='<%# string.Format("javascript:SetIfrmSrc(\"ApplyAcceptDetail.aspx?o1={0}&o2={1}&o3={2}&o4={3}&o5={4}\");return false;",Eval("ENT_City"),Eval("ReportDate"),Eval("ApplyType"),Eval("ManCount"),Eval("ReportCode"))%>' />
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
                            SelectMethod="GetReportList" TypeName="DataAccess.ApplyDAL"
                            SelectCountMethod="SelectReportCount" EnablePaging="true"
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
