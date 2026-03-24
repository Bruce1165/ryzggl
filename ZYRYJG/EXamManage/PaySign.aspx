<%@ Page Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="PaySign.aspx.cs" Inherits="ZYRYJG.EXamManage.PaySign" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../PostSelect.ascx" TagName="PostSelect" TagPrefix="uc1" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<%@ Register Src="../ExamPlanSelect.ascx" TagName="ExamPlanSelect" TagPrefix="uc1" %>
<%@ Register Src="../CheckAll.ascx" TagName="CheckAll" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function onRequestStart(sender, args) {

            //if (args.get_eventTarget().indexOf("ButtonExport") >= 0 || args.get_eventTarget().indexOf("ButtonPrint") >= 0) {
            //    args.set_enableAjax(false);

            //}
        }
    </script>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
        <ClientEvents OnRequestStart="onRequestStart" />
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="DivMain">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="DivMain" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Windows7" />
    <telerik:RadWindowManager runat="server" RestrictionZoneID="offsetElement" ID="RadWindowManager1"
        EnableShadow="true" EnableEmbeddedScripts="true" Skin="Windows7" VisibleStatusbar="false"
        OnClientClose="OnClientClose">
    </telerik:RadWindowManager>
    <div class="div_out">
        <div class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt; 考务管理 &gt;&gt;
                报名管理 &gt;&gt; <strong>缴费通知</strong>
            </div>
        </div>
        <div id="DivMain" runat="server" class="content">
            <%--<table class="bar_cx" id="DivSearch" runat="server">
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
                    <td width="11%" align="right" nowrap="nowrap">报名批号：
                    </td>
                    <td align="left" width="38%">
                        <telerik:RadTextBox ID="RadTxtSignUpCode" runat="server" Width="97%" Skin="Default"
                            >
                        </telerik:RadTextBox>
                    </td>
                    <td width="11%" align="right" nowrap="nowrap">审核批号：
                    </td>
                    <td align="left" width="38%">
                        <telerik:RadTextBox ID="RadTextBoxCheckCode" runat="server" Width="95%" Skin="Default"
                            >
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td width="11%" align="right" nowrap="nowrap">缴费通知状态：
                    </td>
                    <td align="left" width="38%">
                        <asp:RadioButtonList ID="RadioButtonListStatus" runat="server" RepeatDirection="Horizontal"
                            AutoPostBack="false" Width="200px">
                            <asp:ListItem Value="未通知" Selected="True">未通知</asp:ListItem>
                            <asp:ListItem Value="已通知">已通知</asp:ListItem>
                        </asp:RadioButtonList>
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
                考试报名列表
            </div>
            <div style="width: 98%; margin: 0 auto;">
                <telerik:RadGrid ID="RadGridCheckSign" runat="server" Width="100%" PageSize="10"
                    AllowSorting="True" AllowPaging="True" GridLines="None" AutoGenerateColumns="False"
                    Skin="Blue" EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false" OnItemCommand="RadGridCheckSign_ItemCommand"
                    OnPageIndexChanged="RadGridAccept_PageIndexChanged" OnDataBound="RadGridAccept_DataBound">
                    <MasterTableView DataKeyNames="ExamSignUpID,PayNoticeCode,TrainUnitID,ExamPlanID"
                        NoMasterRecordsText="　没有可显示的记录">
                        <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                        <Columns>
                            <telerik:GridTemplateColumn UniqueName="SelectAllColumn" HeaderText="Highlight <br/> ship name">
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" onclick='checkBoxClick(this.checked);' />
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <uc3:CheckAll ID="CheckAll1" runat="server" />
                                </HeaderTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn HeaderText="序号" UniqueName="RowNum" DataField="RowNum" AllowSorting="false">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="TrainUnitName" DataField="TrainUnitName" HeaderText="报名地点">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="WorkerName" DataField="WorkerName" HeaderText="姓名">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="CertificateCode" DataField="CertificateCode"
                                HeaderText="证件号码">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="UnitName" DataField="UnitName" HeaderText="单位名称">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="PostName" HeaderText="申报岗位工种">
                                <ItemTemplate>
                                    <nobr><%# Eval("PostName")%><%# Eval("PostID").ToString() == "12" && Eval("EXAMPLANNAME").ToString().Contains("暖通") ? "（暖通）":
                                                   Eval("PostID").ToString() == "12" && Eval("EXAMPLANNAME").ToString().Contains("电气") ? "（电气）" : ""%></nobr>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="考试时间">
                                <ItemTemplate>
                                    <nobr><%# Convert.ToDateTime(Eval("ExamStartDate")).CompareTo(Convert.ToDateTime(Eval("ExamEndDate")))==0?Convert.ToDateTime(Eval("ExamStartDate")).ToString("yyyy.MM.dd"):Convert.ToDateTime(Eval("ExamStartDate")).ToString("yyyy.MM.dd") + " - " + Convert.ToDateTime(Eval("ExamEndDate")).ToString("yyyy.MM.dd")%></nobr>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridButtonColumn UniqueName="PayNoticeCode" HeaderText="缴费通知批号" ButtonType="LinkButton"
                                DataTextField="PayNoticeCode" Display="false" CommandName="PayNoticeCode">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Font-Underline="true" ForeColor="Blue" />
                            </telerik:GridButtonColumn>
                        </Columns>
                        <HeaderStyle Font-Bold="True" />
                        <PagerTemplate>
                            <uc2:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                        </PagerTemplate>
                    </MasterTableView>
                    <SortingSettings SortToolTip="单击进行排序"></SortingSettings>
                    <StatusBarSettings LoadingText="正在读取数据" ReadyText="完成" />
                </telerik:RadGrid>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DeleteMethod="Delete"
                    InsertMethod="Insert" SelectMethod="GetList_New" TypeName="DataAccess.ExamSignUpDAL"
                    UpdateMethod="Update" SelectCountMethod="SelectCount_New" EnablePaging="true"
                    MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex"
                    SortParameterName="orderBy">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                            DefaultValue="" ConvertEmptyStringToNull="false" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </div>
            <div style="width: 95%; margin: 0 auto; text-align: center; padding-top: 8px;">
                <asp:Button ID="ButtonNotice" runat="server" Text="通知缴费" CssClass="bt_large" OnClick="ButtonNotice_Click" />
                <asp:Button ID="ButtonPrint" runat="server" Text="打印缴费通知单" CssClass="bt_maxlarge" Visible="false"
                    OnClick="ButtonPrint_Click" />&nbsp;
                        <asp:Button ID="ButtonExport" runat="server" Text="导出缴费通知单" CssClass="bt_maxlarge" Visible="false"
                            OnClick="ButtonExport_Click" />&nbsp;
                        <asp:Button ID="ButtonReturn" runat="server" Text="返 回" CssClass="button" Visible="false"
                            OnClick="ButtonReturn_Click" />
            </div>--%>
        </div>
    </div>
</asp:Content>
