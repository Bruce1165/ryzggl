<%@ Page Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="QYZZCheck.aspx.cs" Inherits="ZYRYJG.StAnManage.QYZZCheck" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../PostSelect.ascx" TagName="PostSelect" TagPrefix="uc1" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadWindowManager runat="server" RestrictionZoneID="offsetElement" ID="RadWindowManager1"
        EnableShadow="true" EnableEmbeddedScripts="true">
    </telerik:RadWindowManager>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function onRequestStart(sender, args) {
                if (args.get_eventTarget().indexOf("ButtonOutput") >= 0) {
                    args.set_enableAjax(false);
                }
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" EnableAJAX="true">
        <ClientEvents OnRequestStart="onRequestStart" />
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadTabStrip1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="RadTabStrip1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadioButtonListSZD">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="RadTabStrip1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ButtonOutput">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ButtonOutput" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Windows7" />
    <div class="div_out">
        <div class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt; 统计分析 &gt;&gt;
                综合查询 &gt;&gt; <strong>企业资质人员检查统计</strong>
            </div>
        </div>
        <div class="DivTitleOn" onclick="DivOnOff(this,'Td3',event);" title="折叠">
            查询说明
        </div>
        <div class="DivContent" id="Td3">
            本页面用于统计满足企业资质人员数量要求的企业数量。<br />
            企业资质数据来源于基础数据库（建筑施工企业资质、中央在京备案资质、设计施工一体化资质）。<br />
            企业存在多个资质的按：总包、专包、一体化、分包先后顺序取第一个最高等级资质作为该企业资质进行统计<br />
            其中：管理符合表示管理人员（专职安全生产管理人员、造价员、专业技术管理人员岗位）数量符合资质要求。<br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;工人符合表示技术人员（特种作业【初级】、职业技能岗位【初级、中级、高级、技师、高级技师】）数量符合资质要求。<br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;分包统计初级及以上人员数量；其他序列（总、专、一体化）统计中级及以上人员数量。<br />
            注意：部分数据没有所属区县（只在“全部”筛选中计算）
        </div>
        <div class="content">
            <p class="jbxxbt">
                企业资质人员检查统计
            </p>
            <div class="table_cx">
                <img src="../Images/1034.gif" width="11" height="11" style="margin-bottom: -2px; padding-right: 2px;" />
                区县
            </div>
            <table class="bar_cx">
                <tr>
                    <td align="left">
                        <asp:RadioButtonList ID="RadioButtonListSZD" runat="server" RepeatColumns="11" RepeatDirection="Horizontal"
                            AutoPostBack="True" OnSelectedIndexChanged="RadioButtonListSZD_SelectedIndexChanged">
                            <asp:ListItem Text="全部" Value="%" />
                            <asp:ListItem Text="北京市 " Value="北京市" />
                            <asp:ListItem Text="昌平区" Value="昌平区" />
                            <asp:ListItem Text="朝阳区 " Value="朝阳区" />
                            <asp:ListItem Text="崇文区" Value="崇文区" />
                            <asp:ListItem Text="大兴区" Value="大兴区" />
                            <asp:ListItem Text="东城区" Value="东城区" />
                            <asp:ListItem Text="房山区" Value="房山区" />
                            <asp:ListItem Text="丰台区" Value="丰台区" />
                            <asp:ListItem Text="海淀区" Value="海淀区" />
                            <asp:ListItem Text="怀柔区" Value="怀柔区" />
                            <asp:ListItem Text="门头沟区" Value="门头沟区" />
                            <asp:ListItem Text="密云县" Value="密云县" />
                            <asp:ListItem Text="平谷区" Value="平谷区" />
                            <asp:ListItem Text="石景山区" Value="石景山区" />
                            <asp:ListItem Text="顺义区" Value="顺义区" />
                            <asp:ListItem Text="通州区" Value="通州区" />
                            <asp:ListItem Text="西城区" Value="西城区" />
                            <asp:ListItem Text="宣武区" Value="宣武区" />
                            <asp:ListItem Text="延庆县" Value="延庆县" />
                            <asp:ListItem Text="开发区 " Value="北京市经济技术开发区" />
                        </asp:RadioButtonList>
                    </td>
                </tr>
            </table>

            <div class="table_cx">
                <img src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                企业资质人员数量配备表
            </div>
            <div style="width: 98%; margin: 0 auto;">
                <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1"
                    SelectedIndex="0" Skin="Outlook" OnTabClick="RadTabStrip1_TabClick">
                    <Tabs>
                        <telerik:RadTab runat="server" Text="施工总承包" PageViewID="RadPageView1" Selected="True">
                        </telerik:RadTab>
                        <telerik:RadTab runat="server" Text="专业承包" PageViewID="RadPageView2">
                        </telerik:RadTab>
                        <telerik:RadTab runat="server" Text="设计施工一体" PageViewID="RadPageView3">
                        </telerik:RadTab>
                        <telerik:RadTab runat="server" Text="劳务分包" PageViewID="RadPageView4">
                        </telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
                <telerik:RadGrid ID="RadGrid1" AutoGenerateColumns="False" runat="server" AllowAutomaticDeletes="True"
                    AllowAutomaticInserts="True" AllowAutomaticUpdates="True" AllowPaging="false"
                    PageSize="10" AllowSorting="false" SortingSettings-SortToolTip="单击进行排序" Skin="Blue"
                    EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false" Width="100%" GridLines="None"
                    OnExcelExportCellFormatting="RadGrid1_ExcelExportCellFormatting">
                    <ClientSettings EnableRowHoverStyle="true">
                    </ClientSettings>
                    <MasterTableView AllowMultiColumnSorting="false" NoMasterRecordsText="　没有可显示的记录">
                        <Columns>
                        </Columns>
                        <PagerTemplate>
                            <uc2:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                        </PagerTemplate>
                    </MasterTableView>
                    <SortingSettings SortToolTip="单击进行排序"></SortingSettings>
                    <StatusBarSettings LoadingText="正在读取数据" ReadyText="完成" />
                </telerik:RadGrid>
                <div style="width: 90%; margin: 5px auto; text-align: center; padding-top: 8px;">
                    <asp:Button ID="ButtonOutput" runat="server" Text="导出列表" CssClass="bt_large" OnClick="ButtonOutput_Click" />
                </div>
                <br />
            </div>
        </div>
    </div>
    <uc4:IframeView ID="IframeView" runat="server" />
</asp:Content>
