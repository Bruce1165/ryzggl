<%@ Page Title="" Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="CompareJZS.aspx.cs" Inherits="ZYRYJG.StAnManage.CompareJZS" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
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
            <telerik:AjaxSetting AjaxControlID="RadGrid1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ButtonSearch">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1" />
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
                从业人员业务统计 &gt;&gt; <strong>项目负责人（B本）比对建造师</strong>
            </div>
        </div>
        <div class="content">

            <div class="DivTitleOn" onclick="DivOnOff(this,'Td3',event);" title="折叠">
                统计说明
            </div>
            <div class="DivContent" id="Td3">
                <div style="line-height: 30px; text-align: left; color: Red;">比对项：建造师证书“人员身份证号码”和“单位组织机构代码”。</div>
                <telerik:RadGrid ID="RadGrid2" runat="server" GridLines="None" AllowPaging="false"
                    AllowSorting="false" AutoGenerateColumns="true" Width="400px" Skin="Blue" EnableAjaxSkinRendering="false"
                    EnableEmbeddedSkins="false">
                </telerik:RadGrid>
            </div>
            <div id="Divquery" runat="server">
                <table class="bar_cx">
                    <tr>
                        <td align="right" nowrap="nowrap" width="11%">证书编号：
                        </td>
                        <td align="left" width="39%">
                            <telerik:RadTextBox ID="RadTextBoxCertificateCode" runat="server" Width="97%" Skin="Default">
                            </telerik:RadTextBox>
                        </td>
                        <td align="right" nowrap="nowrap" width="11%">有效期至：
                        </td>
                        <td align="left" width="39%">
                            <telerik:RadDatePicker ID="RadDatePicker_ValidEndDate" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                Width="97%" />
                        </td>
                    </tr>
                    <tr>
                        <td width="11%" align="right" nowrap="nowrap">姓名：
                        </td>
                        <td width="39%" align="left">
                            <telerik:RadTextBox ID="RadTextBoxWorkerName" runat="server" Width="97%" Skin="Default">
                            </telerik:RadTextBox>
                        </td>
                        <td width="11%" align="right" nowrap="nowrap">证件号码：
                        </td>
                        <td align="left">
                            <telerik:RadTextBox ID="RadTextBoxWorkerCertificateCode" runat="server" Width="97%" Skin="Default">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap="nowrap">所在单位：
                        </td>
                        <td align="left">
                            <telerik:RadTextBox ID="RadTextBoxUnitName" runat="server" Width="97%" Skin="Default">
                            </telerik:RadTextBox>
                        </td>
                        <td align="right" nowrap="nowrap">组织机构代码：
                        </td>
                        <td align="left" width="39%">
                            <telerik:RadTextBox ID="RadTextBoxUnitCode" runat="server" Width="97%" Skin="Default"
                                MaxLength="9">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="table_cx">
                <img src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                建造师不匹配证书列表
            </div>
            <div style="width: 98%; margin: 0 auto;">
                <telerik:RadGrid ID="RadGrid1" runat="server" GridLines="None" AllowPaging="True"
                    PageSize="10" AllowSorting="True" AutoGenerateColumns="False" SortingSettings-SortToolTip="单击进行排序"
                    Width="100%" Skin="Blue" EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false">
                    <MasterTableView DataKeyNames="CertificateID" NoMasterRecordsText="　没有可显示的记录">
                        <Columns>
                            <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" Font-Bold="true" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="WorkerName" DataField="WorkerName" HeaderText="姓名">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="WorkerCertificateCode" DataField="WorkerCertificateCode"
                                HeaderText="证件号码">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="UnitName" DataField="UnitName" HeaderText="所在单位">
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="UnitCode" DataField="UnitCode" HeaderText="组织机构代码">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="CertificateCode" DataField="CertificateCode"
                                HeaderText="证书编号">
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="ConferDate" DataField="ConferDate" HeaderText="发证时间"
                                HtmlEncode="false" DataFormatString="{0:yyyy.MM.dd}">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="ValidEndDate" DataField="ValidEndDate" HeaderText="有效期至"
                                HtmlEncode="false" DataFormatString="{0:yyyy.MM.dd}">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                        </Columns>
                        <PagerTemplate>
                            <uc2:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                        </PagerTemplate>
                        <EditFormSettings>
                            <EditColumn UniqueName="EditCommandColumn1">
                            </EditColumn>
                        </EditFormSettings>
                    </MasterTableView>
                </telerik:RadGrid>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="Model.CertificateOB"
                    SelectMethod="GetListCompareJZS" TypeName="DataAccess.CertificateDAL" SelectCountMethod="SelectCountCompareJZS"
                    EnablePaging="true" MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex"
                    SortParameterName="orderBy">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                            DefaultValue="" ConvertEmptyStringToNull="false" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </div>
            <br />
        </div>
    </div>
</asp:Content>
