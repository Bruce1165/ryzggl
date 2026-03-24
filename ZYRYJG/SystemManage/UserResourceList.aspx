<%@ Page Language="C#"  MasterPageFile="~/RadControls.Master" AutoEventWireup="true" CodeBehind="UserResourceList.aspx.cs" Inherits="ZYRYJG.SystemManage.UserResourceList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadWindowManager ID="Singleton" Skin="Windows7" Width="400" Height="430" VisibleStatusbar="false"
            Behaviors="Close,Move, Resize" runat="server">
        </telerik:RadWindowManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Windows7">
        </telerik:RadAjaxLoadingPanel>
        <div class="div_out" style="padding-top: 6px;">
            <div class="table_border" style="width: 98%; margin: 0px auto;">
                <div class="content">
                    <div class="jbxxbt">
                        机构用户权限一览表
                    </div>
                    <div id="test">
                        <telerik:RadGrid ID="RadGrid1" runat="server" AutoGenerateColumns="false" GridLines="None"
                            AllowPaging="False" EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false"
                            Skin="Blue" Width="100%" OnExcelExportCellFormatting="RadGrid1_ExcelExportCellFormatting">
                            <ClientSettings EnableRowHoverStyle="true" Scrolling-AllowScroll="true" Scrolling-ScrollHeight="450" Scrolling-UseStaticHeaders="true">
                            </ClientSettings>
                            <MasterTableView CommandItemDisplay="None" NoMasterRecordsText="　没有可显示的记录">
                                <Columns>
                                </Columns>
                                <HeaderStyle Font-Bold="true" />
                            </MasterTableView>
                        </telerik:RadGrid>
                    </div>
                    <div style="text-align: center; padding: 10px 0px;">
                        <asp:Button ID="ButtonExportToExcel" CssClass="bt_large" Text="导出列表Excel" OnClick="ButtonExportToExcel_Click"
                            runat="server"></asp:Button>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>