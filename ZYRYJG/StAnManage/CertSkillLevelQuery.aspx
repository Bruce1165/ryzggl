<%@ Page Title="" Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="CertSkillLevelQuery.aspx.cs" Inherits="ZYRYJG.StAnManage.CertSkillLevelQuery" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function onRequestStart(sender, args) {
                if (args.get_eventTarget().indexOf("ButtonOutput") >= 0) {
                    args.set_enableAjax(false);
                }
            }

        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <ClientEvents OnRequestStart="onRequestStart" />
        <AjaxSettings>
       <%--     <telerik:AjaxSetting AjaxControlID="ButtonSearch">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="p_TrainUnit" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="RadioButtonListPostType" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadioButtonListPostType">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="p_TrainUnit" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="ButtonSearch" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="RadioButtonListPostType" UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>--%>
           <%-- <telerik:AjaxSetting AjaxControlID="ButtonOutput">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                </UpdatedControls>
            </telerik:AjaxSetting>--%>
             <telerik:AjaxSetting AjaxControlID="Divquery">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Divquery" LoadingPanelID="RadAjaxLoadingPanel1"  />
                </UpdatedControls>
            </telerik:AjaxSetting>
            
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Windows7" />
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Windows7">
    </telerik:RadWindowManager>
    <div class="div_out">
        <div class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt; 统计分析 &gt;&gt;
                从业人员业务统计 &gt;&gt; <strong>证书技术等级统计</strong>
            </div>
        </div>
        <div class="content">
            <div class="DivTitleOn" onclick="DivOnOff(this,'Td3',event);" title="折叠">
                统计说明
            </div>
            <div class="DivContent" id="Td3">
                【证书技术等级统计】是按时间段（发证日期）统计证书的技术等级数量。<br />
                &nbsp;&nbsp;输入查询时间段，如“自 2010-1-1 至 2010-12-31 ” 即可查询全年考试情况，不输时间表示查询所有数据。
            </div>
            <div id="Divquery" runat="server">
                <table class="bar_cx">
                    <tr>
                        <td style="width: 450px; vertical-align:top">
                            <asp:RadioButtonList ID="RadioButtonListPostType" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="RadioButtonListPostType_SelectedIndexChanged">
                                <asp:ListItem Text="新版建设职业技能岗位" Value="4000" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="旧版建设职业技能岗位" Value="4"></asp:ListItem>
                            </asp:RadioButtonList>
                            <p id="p_TrainUnit" runat="server" style="display:none;">
                                <telerik:RadComboBox ID="RadComboBoxTrainUnit" runat="server" Width="360px" Label="培训机构" Font-Size="16px" style="margin:12px 4px">
                                    <Items>
                                    </Items>
                                </telerik:RadComboBox>
                            </p>
                        </td>
                        <td align="right" nowrap="nowrap" style="width: 120px;vertical-align:top">发证日期：自
                        </td>
                        <td align="left" style="vertical-align:top">
                            <telerik:RadDatePicker ID="RadDatePicker_StartDate" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                Width="180px" />
                            <div class="RadPicker"> 至 </div>
                            <telerik:RadDatePicker ID="RadDatePicker_EndDate" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                Width="180px" />
                            &nbsp;&nbsp;
                                        <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
                        </td>
                    </tr>
                </table>
           <%-- </div>
            <div style="width: 98%; margin: 0 auto; padding-top: 8px; border: none;">--%>
                <telerik:RadGrid ID="RadGrid1" AutoGenerateColumns="False" runat="server" Skin="Blue"
                    EnableAjaxSkinRendering="False" EnableEmbeddedSkins="False" Width="100%" GridLines="None"
                    BorderWidth="0" OnExcelExportCellFormatting="RadGrid1_ExcelExportCellFormatting">
                    <ClientSettings EnableRowHoverStyle="true">
                        <%-- <Scrolling AllowScroll="True" UseStaticHeaders="true" ScrollHeight="250px" />--%>
                    </ClientSettings>
                    <HeaderContextMenu EnableEmbeddedSkins="False">
                    </HeaderContextMenu>
                    <MasterTableView CommandItemDisplay="None" NoMasterRecordsText="　没有可显示的记录" Width="100%">
                        <Columns>
                            <telerik:GridBoundColumn UniqueName="PostTypeName" DataField="PostTypeName" HeaderText="岗位类别">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="PostName" DataField="PostName" HeaderText="岗位工种">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="初级工" DataField="初级工" HeaderText="初级工">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="中级工" DataField="中级工" HeaderText="中级工">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="高级工" DataField="高级工" HeaderText="高级工">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="技师" DataField="技师" HeaderText="技师">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="高级技师" DataField="高级技师" HeaderText="高级技师">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="无" DataField="无" HeaderText="无等级">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="小计" DataField="小计" HeaderText="小计">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                        </Columns>
                        <HeaderStyle Font-Bold="True" />
                    </MasterTableView>
                    <FilterMenu EnableEmbeddedSkins="False">
                    </FilterMenu>
                </telerik:RadGrid>
            </div>
            <div style="width: 90%; margin: 5px auto; text-align: center; padding-top: 8px;">
                <asp:Button ID="ButtonOutput" runat="server" Text="导出列表" CssClass="bt_large" OnClick="ButtonOutput_Click" />
            </div>
        </div>
        <br />
    </div>
</asp:Content>
