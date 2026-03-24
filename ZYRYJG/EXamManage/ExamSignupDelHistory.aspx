<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExamSignupDelHistory.aspx.cs"
    MasterPageFile="~/RadControls.Master" Inherits="ZYRYJG.EXamManage.ExamSignupDelHistory" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../ExamPlanSelect.ascx" TagName="ExamPlanSelect" TagPrefix="uc1" %>
<%@ Register Src="../PostSelect.ascx" TagName="PostSelect" TagPrefix="uc1" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function onRequestStart(sender, args) {

                if (args.get_eventTarget().indexOf("ButtonExport") >= 0) {
                    args.set_enableAjax(false);
                }
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" EnableAJAX="true" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
        <ClientEvents OnRequestStart="onRequestStart" />
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadGrid1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ButtonSearch">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="DivList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ButtonExport">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ButtonExport" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Windows7" />
    <telerik:RadWindowManager runat="server" RestrictionZoneID="offsetElement" ID="RadWindowManager1"
        EnableShadow="true" EnableEmbeddedScripts="true" OnClientClose="OnClientClose"
        Skin="Windows7" VisibleStatusbar="false">
    </telerik:RadWindowManager>
    <div class="div_out">
        <div class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt; 考务管理 &gt;&gt;
                报名管理 &gt;&gt; 查看报名情况 &gt;&gt;<strong>查看考试报名删除历史</strong>
            </div>
        </div>
        <div class="content">
            <table class="bar_cx">
                <tr>
                    <td align="right" width="11%" nowrap="nowrap">考试计划：
                    </td>
                    <td align="left" colspan="3">
                        <uc1:ExamPlanSelect ID="ExamPlanSelect1" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="right" width="11%" nowrap="nowrap">培训(报名)点：
                    </td>
                    <td align="left" width="38%">
                        <telerik:RadTextBox ID="RadTextBoxTrainUnitID" runat="server" Width="97%" Skin="Default"
                            >
                        </telerik:RadTextBox>
                    </td>
                    <td width="11%" align="right" nowrap="nowrap">单位名称：
                    </td>
                    <td align="left" width="38%">
                        <telerik:RadTextBox ID="RadTxtUnitName" runat="server" Width="95%" Skin="Default"
                            >
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" width="11%" nowrap="nowrap">姓名：
                    </td>
                    <td align="left" width="38%">
                        <telerik:RadTextBox ID="RadTxtWorkerName" runat="server" Width="97%" Skin="Default"
                            >
                        </telerik:RadTextBox>
                    </td>
                    <td width="11%" align="right" nowrap="nowrap">证件号码：
                    </td>
                    <td align="left" width="38%">
                        <telerik:RadTextBox ID="RadTxtCertificateCode" runat="server" Width="95%" Skin="Default"
                            >
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" width="11%" nowrap="nowrap">报名批号：
                    </td>
                    <td align="left" width="38%">
                        <telerik:RadTextBox ID="RadTextBoxSignUpCode" runat="server" Width="97%" Skin="Default"
                            >
                        </telerik:RadTextBox>
                    </td>
                    <td width="11%" align="right" nowrap="nowrap">报名状态：
                    </td>
                    <td align="left" width="38%">
                        <telerik:RadComboBox ID="RadComboBoxStatus" runat="server" Skin="Office2007" CausesValidation="False"
                            ExpandAnimation-Duration="0">
                            <Items>
                                <telerik:RadComboBoxItem Text="全部" Value="" Selected="true" />
                                <telerik:RadComboBoxItem Text="待初审" Value="待初审" />
                                <telerik:RadComboBoxItem Text="已初审" Value="已初审" />
                                <telerik:RadComboBoxItem Text="已受理" Value="已受理" />
                                <telerik:RadComboBoxItem Text="已审核" Value="已审核" />
                                  <telerik:RadComboBoxItem Text="审核确认" Value="已缴费" />
                              </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" nowrap="nowrap">报名时间：
                    </td>
                    <td width="39%" align="left">
                        <telerik:RadDatePicker ID="RadDatePicker_SignUpStartDate" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                            Width="46%" />
                        <div class="RadPicker">至</div>
                        <telerik:RadDatePicker ID="RadDatePicker_SignUpEndDate" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                            Width="46%" />
                    </td>
                    <tr>
                        <td colspan="4" align="center">
                            <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
                        </td>
                    </tr>
                </tr>
            </table>

            <div id="DivList" runat="server">
                <div class="table_cx">
                    <img alt="" src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                    被删除的考试报名列表
                </div>
                <div style="width: 98%; margin: 0 auto; padding-bottom: 10px;">
                    <telerik:RadGrid ID="RadGrid1" runat="server" Width="100%" PagerStyle-AlwaysVisible="true"
                        PageSize="10" AllowSorting="True" AllowPaging="True" GridLines="None" AutoGenerateColumns="False"
                        Skin="Blue" EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false" OnExcelExportCellFormatting="RadGrid1_ExcelExportCellFormatting">
                        <ClientSettings EnableRowHoverStyle="true">
                            <Scrolling AllowScroll="true" ScrollHeight="380" />
                        </ClientSettings>
                        <MasterTableView DataKeyNames="ExamSignUpID" NoMasterRecordsText="　没有可显示的记录">
                            <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                            <Columns>
                                <telerik:GridBoundColumn HeaderText="序号" UniqueName="RowNum" DataField="RowNum" AllowSorting="false">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="DeleteMan" DataField="DeleteMan" HeaderText="删除人">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="DeleteTime" DataField="DeleteTime" HeaderText="删除时间"
                                    HtmlEncode="false" DataFormatString="{0:yy.MM.dd HH:mm}">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn HeaderText="考试时间">
                                    <ItemTemplate>
                                        <nobr><%# Convert.ToDateTime(Eval("ExamStartDate")).CompareTo(Convert.ToDateTime(Eval("ExamEndDate")))==0?Convert.ToDateTime(Eval("ExamStartDate")).ToString("yy.MM.dd"):Convert.ToDateTime(Eval("ExamStartDate")).ToString("yy.MM.dd") + " - " + Convert.ToDateTime(Eval("ExamEndDate")).ToString("yy.MM.dd")%></nobr>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn UniqueName="PostTypeName" DataField="PostTypeName" HeaderText="岗位类别">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="PostName" DataField="PostName" HeaderText="岗位工种">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>                                
                                 <telerik:GridTemplateColumn UniqueName="Status" HeaderText="报名状态">
                                    <ItemTemplate>
                                        <nobr><%# Eval("Status").ToString()=="已缴费" ? "审核确认":Eval("Status")%></nobr>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn UniqueName="WorkerName" DataField="WorkerName" HeaderText="姓名">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="CertificateCode" DataField="CertificateCode"
                                    HeaderText="证件号码">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="UnitName" DataField="UnitName" HeaderText="单位名称">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="SignUpDate" DataField="SignUpDate" HeaderText="报名时间"
                                    HtmlEncode="false" DataFormatString="{0:yy.MM.dd}">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="CheckMan" DataField="CheckMan" HeaderText="审核人">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="CheckDate" DataField="CheckDate" HeaderText="审核时间"
                                    HtmlEncode="false" DataFormatString="{0:yy.MM.dd}">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>
                            </Columns>
                            <HeaderStyle Font-Bold="True" />
                            <PagerTemplate>
                                <uc2:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                            </PagerTemplate>
                        </MasterTableView>
                        <SortingSettings SortToolTip="单击进行排序"></SortingSettings>
                        <StatusBarSettings LoadingText="正在读取数据" ReadyText="完成" />
                    </telerik:RadGrid>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server"
                        SelectMethod="GetList" TypeName="DataAccess.ExamSignUp_DelDAL"
                        SelectCountMethod="SelectCount" EnablePaging="true" MaximumRowsParameterName="maximumRows"
                        StartRowIndexParameterName="startRowIndex" SortParameterName="orderBy">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                                DefaultValue="" ConvertEmptyStringToNull="false" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                </div>
                <div style="width: 95%; margin: 0 auto; text-align: center; padding-bottom: 20px;">
                    <asp:Button ID="ButtonExport" runat="server" Text="导出查询结果" CssClass="bt_large" OnClick="ButtonExport_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
