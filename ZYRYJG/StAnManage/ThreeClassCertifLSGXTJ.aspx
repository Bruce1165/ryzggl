<%@ Page Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="ThreeClassCertifLSGXTJ.aspx.cs" Inherits="ZYRYJG.StAnManage.ThreeClassCertifLSGXTJ" %>

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
                当前位置 &gt;&gt; 统计分析 &gt;&gt;
                综合查询 &gt;&gt; <strong>三类人员续期归属统计</strong>
            </div>
        </div>

        <div class="content">
            <p class="jbxxbt">
                三类人员续期归属统计
            </p>
            <div class="DivTitleOn" onclick="DivOnOff(this,'Td3',event);" title="折叠">
                数据说明
            </div>
            <div class="DivContent" id="Td3">
                1、当隶属关系有重叠时，优先顺序为：1、外地进京；2、市属集团总公司；3、中央驻京单位；4、区县建委；5、起重机械租赁企业。<br />
                2、区县建委、外地进京、起重机械租赁企业隶属每日同步于住建委基础数据库；市属集团总公司、中央驻京单位自行维护子单位隶属关系。
                <br />

            </div>
            <div class="table_cx">

                <div class="table_cx">
                    <img src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                    统计结果
                </div>
                <div style="width: 98%; margin: 0 auto;">
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

                                <telerik:GridBoundColumn UniqueName="lsgxtype" DataField="lsgxtype" HeaderText="机构类型">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="lsgx" DataField="lsgx" HeaderText="续期初审机构">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="CertCount" DataField="CertCount" HeaderText="今年应续期证书数量">
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
