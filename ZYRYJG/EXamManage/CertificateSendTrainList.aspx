<%@ Page Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="CertificateSendTrainList.aspx.cs" Inherits="ZYRYJG.EXamManage.CertificateSendTrainList" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../ExamPlanSelect.ascx" TagName="ExamPlanSelect" TagPrefix="uc1" %>
<%@ Register Src="../PostSelect.ascx" TagName="PostSelect" TagPrefix="uc4" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<%@ Register Src="../CheckAll.ascx" TagName="CheckAll" TagPrefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    </telerik:RadCodeBlock>

    <script type="text/javascript">
        function onRequestStart(sender, args) {

            if (args.get_eventTarget().indexOf("ButtonOutputSendTable") >= 0) {
                args.set_enableAjax(false);

            }
        }
    </script>

    <telerik:RadWindowManager runat="server" RestrictionZoneID="offsetElement" ID="RadWindowManager1"
        EnableShadow="true" EnableEmbeddedScripts="true" OnClientClose="OnClientClose"
        Skin="Windows7" VisibleStatusbar="false">
    </telerik:RadWindowManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" EnableAJAX="true">
        <ClientEvents OnRequestStart="onRequestStart" />
        <AjaxSettings>
            
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Windows7" Visible="true" />
    <div class="div_out">
        <div class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt; <strong>证书下发单位对照表查询</strong>
            </div>
        </div>
        <div class="content">
            <p style="color: blue;">如果按发证时间 + 岗位工种查询结果证书编号不连续，请使用考试名称定位考试。（出现原因：同月份同岗位工种存在2次考试【补考或拆分】）</p>
            <table id="tableSearch" runat="server" class="bar_cx">
                <tr>
                    <td align="right" nowrap="nowrap">考试名称：
                    </td>
                    <td align="left" colspan="5">
                        <uc1:ExamPlanSelect ID="ExamPlanSelect1" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="right" nowrap="nowrap">发证时间：
                    </td>
                    <td align="left">
                        <telerik:RadComboBox ID="RadComboBoxYear" runat="server" Skin="Office2007" CausesValidation="False"
                            Width="60px" ExpandAnimation-Duration="0">
                        </telerik:RadComboBox>
                        &nbsp;年&nbsp;
                                <telerik:RadComboBox ID="RadComboBoxMonth" runat="server" Skin="Office2007" CausesValidation="False"
                                    Width="60px" ExpandAnimation-Duration="0">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="全部" Value="" />
                                        <telerik:RadComboBoxItem Text="1" Value="1" />
                                        <telerik:RadComboBoxItem Text="2" Value="2" />
                                        <telerik:RadComboBoxItem Text="3" Value="3" />
                                        <telerik:RadComboBoxItem Text="4" Value="4" />
                                        <telerik:RadComboBoxItem Text="5" Value="5" />
                                        <telerik:RadComboBoxItem Text="6" Value="6" />
                                        <telerik:RadComboBoxItem Text="7" Value="7" />
                                        <telerik:RadComboBoxItem Text="8" Value="8" />
                                        <telerik:RadComboBoxItem Text="9" Value="9" />
                                        <telerik:RadComboBoxItem Text="10" Value="10" />
                                        <telerik:RadComboBoxItem Text="11" Value="11" />
                                        <telerik:RadComboBoxItem Text="12" Value="12" />
                                    </Items>
                                </telerik:RadComboBox>
                        &nbsp;月
                    </td>
                    <td align="right" nowrap="nowrap">审核日期：
                    </td>
                    <td align="left">
                        <telerik:RadDatePicker ID="RadDatePickerCheckDate" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                            Width="120px" />
                    </td>
                    <td align="left"></td>
                    <td></td>
                </tr>
                <tr>
                    <td align="right" nowrap="nowrap" style="padding-left: 20px;">
                        <div id="Post" runat="server">
                            岗位工种：
                        </div>
                    </td>

                    <td align="left">
                        <uc4:PostSelect ID="PostSelect1" runat="server" />
                    </td>
                    <td align="right" nowrap="nowrap">培训点名称：
                    </td>
                    <td align="left" >
                        <telerik:RadTextBox ID="RadTextBoxPeiXunUnitName" runat="server" Width="200px" Skin="Default"
                            MaxLength="100">
                        </telerik:RadTextBox>
                    </td>
                    <td align="left">&nbsp;&nbsp;<asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
                    </td>
                    <td></td>
                </tr>

            </table>
            <div class="table_cx">
                <img alt="" src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                证书列表
            </div>
            <div style="width: 98%; margin: 0 auto;">
                <telerik:RadGrid ID="RadGridTrainUnitCertificateCode" AutoGenerateColumns="False"
                    runat="server" AllowPaging="false"
                    PageSize="10" Skin="Blue" EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false" GridLines="None"
                    Width="100%" OnExcelExportCellFormatting="RadGridTrainUnitCertificateCode_ExcelExportCellFormatting">
                    <MasterTableView CommandItemDisplay="None" NoMasterRecordsText="　没有可显示的记录">
                        <Columns>
                            <telerik:GridBoundColumn UniqueName="TrainUnitName" DataField="TrainUnitName" HeaderText="报名点">
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="PostName" DataField="PostName" HeaderText="岗位工种">
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="CONFERDATE" DataField="CONFERDATE" HeaderText="发证日期" HtmlEncode="false" DataFormatString="{0:yyyy-MM-d}">
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn UniqueName="Count" DataField="Count"
                                HeaderText="证书数量">
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="min_code" DataField="min_code" HeaderText="最小证书编号">
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="max_code" DataField="max_code"
                                HeaderText="最大证书编号">
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>

                        </Columns>

                    </MasterTableView>
                </telerik:RadGrid>

            </div>
            <div style="padding: 20px;">
                <asp:Button ID="ButtonOutputSendTable" runat="server" Text="导出列表Excel" CssClass="bt_maxlarge"
                    OnClick="ButtonOutputSendTablel_Click" ToolTip="" />
            </div>
        </div>
    </div>
</asp:Content>
