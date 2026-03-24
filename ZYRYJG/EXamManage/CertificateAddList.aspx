<%@ Page Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="CertificateAddList.aspx.cs" Inherits="ZYRYJG.EXamManage.CertificateAddList" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../PostSelect.ascx" TagName="PostSelect" TagPrefix="uc1" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">

        <script type="text/javascript">
            function onRequestStart(sender, args) {

                if (args.get_eventTarget().indexOf("ButtonExportToExcel") >= 0) {
                    args.set_enableAjax(false);

                }
            }

        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindowManager ID="RadWindowManager1" ShowContentDuringLoad="false" VisibleStatusbar="false"
        ReloadOnShow="true" runat="server" Skin="Windows7" EnableShadow="true">
    </telerik:RadWindowManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1"
        EnableAJAX="true">
        <ClientEvents OnRequestStart="onRequestStart" />
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="PostSelect1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="PostSelect1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadGrid1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ButtonSearch">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true"
        Skin="Windows7" />
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    </telerik:RadCodeBlock>
    <div class="div_out">
        <div class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt; 考务管理 &gt;&gt;证书制作
                &gt;&gt; 证书补登记
            </div>
        </div>
        <div class="content">
            <table class="bar_cx" runat="server" id="DivSearch">
                <tr>
                    <td width="11%" align="right" nowrap="nowrap">
                        <div id="Post" runat="server">
                            岗位工种：
                        </div>
                    </td>
                    <td align="left" width="38%">
                        <uc1:PostSelect ID="PostSelect1" runat="server" />
                    </td>
                    <td width="11%" align="right" nowrap="nowrap">证书编号：
                    </td>
                    <td align="left" width="38%">
                        <telerik:RadTextBox ID="RadTxtCertificateCode" runat="server" Width="97%" Skin="Default">
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" nowrap="nowrap" width="11%">考生姓名：
                    </td>
                    <td align="left" width="38%">
                        <telerik:RadTextBox ID="RadTextBoxWorkerName" runat="server" Width="97%" Skin="Default"
                            MaxLength="40">
                        </telerik:RadTextBox>
                    </td>
                    <td width="11%" align="right" nowrap="nowrap">证件号码：
                    </td>
                    <td align="left">
                        <telerik:RadTextBox ID="RadTextBoxWorkerCertificateCode" runat="server" CssClass="texbox"
                            Skin="Default" Width="97%">
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" nowrap="nowrap" width="11%">创建人：
                    </td>
                    <td align="left" width="38%">
                        <telerik:RadTextBox ID="RadTextBoxApplyMan" runat="server" Width="97%" Skin="Default"
                            MaxLength="40">
                        </telerik:RadTextBox>
                    </td>
                    <td align="right" nowrap="nowrap">创建时间：
                    </td>
                    <td width="43%" align="left">
                        <telerik:RadDatePicker ID="RadDatePicker_StartDate" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                            Width="46%" />
                        <div class="RadPicker">至</div>
                        <telerik:RadDatePicker ID="RadDatePicker_EndDate" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                            Width="46%" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center">
                        <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
                    </td>
                </tr>
            </table>
            <div class="table_cx">
                <img alt="" src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                证书补登记历史记录列表
            </div>
            <div class="table_cx" style="padding-right: 40px; float: left; clear: right; padding-left: 20px;">
                <a href='CertificateAdd.aspx' style="color: #DC2804; font-weight: bold;">
                    <img alt="" src="../Images/jia.gif" style="width: 14px; height: 15px; margin-bottom: -2px; padding-right: 5px; border: none;" />新建补登记</a>
            </div>
            <div style="width: 98%; margin: 0 auto;">
                <telerik:RadGrid ID="RadGrid1" AllowPaging="True"
                    SortingSettings-SortToolTip="单击进行排序" runat="server" AutoGenerateColumns="False"
                    AllowSorting="True" GridLines="None" CellPadding="0" Width="100%" Skin="Blue"  PagerStyle-AlwaysVisible="true"
                    EnableAjaxSkinRendering="False" EnableEmbeddedSkins="False" OnExcelExportCellFormatting="RadGrid1_ExcelExportCellFormatting">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                    <MasterTableView CommandItemDisplay="None" NoMasterRecordsText="　没有可显示的记录">
                        <Columns>
                            <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                <HeaderStyle HorizontalAlign="Center" Font-Bold="true" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="PostName" DataField="PostName" HeaderText="岗位工种">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="CertificateCode" DataField="CertificateCode"
                                HeaderText="证书编号">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="WorkerName" DataField="WorkerName" HeaderText="姓名">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="WorkerCertificateCode" DataField="WorkerCertificateCode"
                                HeaderText="证件号码">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="UnitName" DataField="UnitName"
                                HeaderText="工作单位">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="sb" HeaderText="社保">
                                <ItemTemplate>
                                    <%# (((Eval("PostTypeID").ToString() == "1") || (Eval("PostTypeID").ToString() == "3"))
                                             && (Convert.ToDateTime(Eval("CREATETIME")).CompareTo(DateTime.Parse("2014-07-01")) >= 0)) ?
                                            string.Format("<span class=\"link_edit\" onclick='javascript:SetIfrmSrc(\"../PersonnelFile/RYSBView.aspx?o1={0}&o2={1}&o3={2}\");'><nobr>社保</nobr></span>", Eval("WorkerCertificateCode").ToString(), Eval("UnitCode").ToString(), Convert.ToDateTime(Eval("CREATETIME")).ToString()) : ""%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" Font-Bold="true" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                            </telerik:GridTemplateColumn>
                            <%--<telerik:GridBoundColumn UniqueName="ApplyMan" DataField="ApplyMan" HeaderText="创建人">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    </telerik:GridBoundColumn>--%>
                            <telerik:GridBoundColumn UniqueName="CreateTime" DataField="CreateTime" HtmlEncode="false"
                                DataFormatString="{0:yyyy-MM-dd}" HeaderText="创建时间">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="Status" HeaderText="审批状态">
                                <ItemTemplate>
                                    <%# Eval("Status").ToString() =="待审批"?"待审批":"已审批" %>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" Font-Bold="true" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridTemplateColumn>
                        </Columns>
                        <PagerTemplate>
                            <uc2:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                        </PagerTemplate>
                    </MasterTableView>
                    <ClientSettings>
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <SortingSettings SortToolTip="单击进行排序"></SortingSettings>
                    <StatusBarSettings LoadingText="正在读取数据" ReadyText="完成" />
                </telerik:RadGrid>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="DataAccess.CertificateDAL"
                    DataObjectTypeName="Model.CertificateOB" SelectMethod="GetList" EnablePaging="true"
                    SelectCountMethod="SelectCount" MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex"
                    SortParameterName="orderBy">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                            DefaultValue="and Remark='补登记'" ConvertEmptyStringToNull="false" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </div>
            <div style="width: 90%; margin: 5px auto; text-align: center; padding-top: 8px;">
                <asp:Button ID="ButtonOutput" runat="server" Text="导出查询结果" CssClass="bt_large" OnClick="ButtonOutput_Click" />
            </div>
        </div>
        <br />
    </div>
    <uc4:IframeView ID="IframeView" runat="server" />
</asp:Content>
