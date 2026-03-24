<%@ Page Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="TjMessageSend.aspx.cs" Inherits="ZYRYJG.StAnManage.TjMessageSend" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../PostSelect.ascx" TagName="PostSelect" TagPrefix="uc1" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadWindowManager runat="server" RestrictionZoneID="offsetElement" ID="RadWindowManager1"
        EnableShadow="true" EnableEmbeddedScripts="true">
    </telerik:RadWindowManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" EnableAJAX="true">
        <AjaxSettings>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Windows7" />
    <div class="div_out">
        <div class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt; 系统管理 &gt;&gt;
                 <strong>系统发送短信统计</strong>
            </div>
        </div>
        <div class="content">            
            <div class="table_cx">
                 <table class="bar_cx">
                    <tr>
                        <td align="right" nowrap="nowrap" style="width: 180px;">统计时间范围：自
                        </td>
                        <td align="left">
                            <telerik:RadDatePicker ID="RadDatePicker_StartDate" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                Width="180px" />
                            <div class="RadPicker">至</div>
                            <telerik:RadDatePicker ID="RadDatePicker_EndDate" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                Width="180px" />
                            &nbsp;&nbsp;
                                        <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click"
                                            />
                        </td>
                    </tr>
                </table>
                <div class="table_cx">
                    <img src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                    统计结果
                </div>
                <div style="width: 98%; margin: 0 auto;">
                    <telerik:RadGrid ID="RadGridSendCount" runat="server" GridLines="None" AllowPaging="false"
                        AllowSorting="false" AutoGenerateColumns="False"
                        Width="100%" Skin="Blue" EnableAjaxSkinRendering="False" EnableEmbeddedSkins="False"
                        >
                        <ClientSettings EnableRowHoverStyle="true">
                        </ClientSettings>
                        <HeaderContextMenu EnableEmbeddedSkins="False">
                        </HeaderContextMenu>
                        <MasterTableView AllowMultiColumnSorting="True" NoMasterRecordsText="没有可显示的记录">
                            <Columns>       
                                  <telerik:GridTemplateColumn HeaderText="发送时间">
                                <ItemTemplate>
                                    <nobr><%# Eval("SendStart") %><%# Eval("SendEnd")==DBNull.Value?"": Convert.ToDateTime(Eval("SendEnd")).ToString("～yyyy-MM-dd HH")%></nobr>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridTemplateColumn>                        
                                <telerik:GridBoundColumn UniqueName="fszt" DataField="发送状态" HeaderText="发送状态">
                                    <HeaderStyle HorizontalAlign="Left"  Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>                               
                                <telerik:GridBoundColumn UniqueName="infoCount" DataField="数量" HeaderText="短信数量">
                                    <HeaderStyle HorizontalAlign="Left"  Wrap="false"  />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                 <telerik:GridBoundColumn UniqueName="dxnr" DataField="内容" HeaderText="短信内容">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>          
                            </Columns>
                            <HeaderStyle Font-Bold="true" />

                        </MasterTableView>
                        <SortingSettings SortToolTip="单击进行排序"></SortingSettings>
                        <StatusBarSettings LoadingText="正在读取数据" ReadyText="完成" />
                    </telerik:RadGrid>
                     <br />
                    <telerik:RadGrid ID="RadGrid1" runat="server" GridLines="None" AllowPaging="false"
                        AllowSorting="false" AutoGenerateColumns="False"
                        Width="100%" Skin="Blue" EnableAjaxSkinRendering="False" EnableEmbeddedSkins="False"
                        OnExcelExportCellFormatting="RadGrid1_ExcelExportCellFormatting">
                        <ClientSettings EnableRowHoverStyle="true">
                        </ClientSettings>
                        <HeaderContextMenu EnableEmbeddedSkins="False">
                        </HeaderContextMenu>
                        <MasterTableView AllowMultiColumnSorting="True" NoMasterRecordsText="没有可显示的记录">

                            <Columns>

                                <telerik:GridBoundColumn UniqueName="业务类型" DataField="业务类型" HeaderText="业务类型">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>                               
                                <telerik:GridBoundColumn UniqueName="数量" DataField="数量" HeaderText="短信数量">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>

                            </Columns>
                            <HeaderStyle Font-Bold="true" />

                        </MasterTableView>
                        <SortingSettings SortToolTip="单击进行排序"></SortingSettings>
                        <StatusBarSettings LoadingText="正在读取数据" ReadyText="完成" />
                    </telerik:RadGrid>

                    <br />
                    <asp:Button ID="ButtonExportToExcel" CssClass="bt_maxlarge" Text="导出查询结果列表" OnClick="ButtonExportToExcel_Click"
                        runat="server"></asp:Button>
                </div>
            </div>
            <br />
        </div>
    </div>
</asp:Content>
