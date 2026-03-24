<%@ Page Title="" Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="ScorePublish.aspx.cs" Inherits="ZYRYJG.EXamManage.ScorePublish" %>

<%@ Register Src="../ExamPlanSelect.ascx" TagName="ExamPlanSelect" TagPrefix="uc1" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    </telerik:RadCodeBlock>
    <script type="text/javascript">
        function onRequestStart(sender, args) {

            if (args.get_eventTarget().indexOf("ButtonPrint") >= 0) {
                args.set_enableAjax(false);

            }
        }
    </script>
    <telerik:RadWindowManager ID="RadWindowManager1" VisibleStatusbar="false"
        runat="server" Skin="Windows7" EnableShadow="true" OnClientClose="OnClientClose">
    </telerik:RadWindowManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
        <ClientEvents OnRequestStart="onRequestStart" />
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ButtonSearch">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="DivMain" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ButtonPrint">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="DivMain" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="BtnPublish">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="DivMain" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true"
        Skin="Windows7" />
    <div class="div_out">
        <div class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt; 考务管理 &gt;&gt;
                考场管理 &gt;&gt; <strong>成绩公告</strong>
            </div>
        </div>
        <div class="content">
            <table class="bar_cx">
                <tr>
                    <td align="right" nowrap="nowrap" width="11%">考试名称：
                    </td>
                    <td align="left" colspan="3">
                        <uc1:ExamPlanSelect ID="ExamPlanSelect1" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="right" nowrap="nowrap">考生姓名：
                    </td>
                    <td align="left" width="39%">
                        <telerik:RadTextBox ID="RadTextBoxWorkerName" runat="server" Width="97%" Skin="Default"
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
                    <td align="right" nowrap="nowrap">准考证号：
                    </td>
                    <td align="left" width="39%">
                        <telerik:RadTextBox ID="RadTextBoxExamCardID" runat="server" Width="97%" Skin="Default"
                            >
                        </telerik:RadTextBox>
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
                考试通过的考生名单（单击列名可排序）
            </div>
            <div style="width: 98%; margin: 0 auto;" runat="server" id="DivMain">
                <telerik:RadGrid ID="RadGridExamResult" runat="server" PagerStyle-AlwaysVisible="true"
                    GridLines="None" AllowPaging="True" AllowCustomPaging="true" PageSize="10" AllowSorting="True"
                    AutoGenerateColumns="False" SortingSettings-SortToolTip="单击进行排序" Width="100%"
                    Skin="Blue" EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false" OnDataBound="RadGridExamResult_DataBound">
                    <ClientSettings EnableRowHoverStyle="true">
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ExamResultID,Status" NoMasterRecordsText="　没有可显示的记录">
                        <Columns>
                            <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="PostTypeName" DataField="PostTypeName" HeaderText="岗位类别">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="PostName" DataField="PostName" HeaderText="岗位工种">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="ExamCardID" DataField="ExamCardID" HeaderText="准考证号">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="WorkerName" DataField="WorkerName" HeaderText="考生姓名">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="WorkerCertificateCode" DataField="WorkerCertificateCode"
                                HeaderText="证件号码">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="UnitName" DataField="UnitName" HeaderText="工作单位">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="Status" DataField="Status" HeaderText="公告状态">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="S_TrainUnitName" DataField="S_TrainUnitName" HeaderText="报名地点">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                        </Columns>
                        <PagerTemplate>
                            <uc2:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                        </PagerTemplate>
                    </MasterTableView>
                </telerik:RadGrid>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="DataAccess.ExamResultDAL"
                    DataObjectTypeName="Model.ExamResultOB" SelectMethod="GetListView_ExamScore_New"
                    EnablePaging="true" SelectCountMethod="SelectCountView_ExamScore_New" MaximumRowsParameterName="maximumRows"
                    StartRowIndexParameterName="startRowIndex" SortParameterName="orderBy">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                            DefaultValue="" ConvertEmptyStringToNull="false" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <div style="width: 90%; margin: 5px auto; text-align: center; padding-top: 8px;">
                    <%-- <asp:CheckBox ID="CheckBox2" Text="是否打印所有数据" runat="server"></asp:CheckBox>--%>
                    <asp:Button ID="ButtonPrint" runat="server" Text="导出通过考生名单" CssClass="bt_maxlarge" OnClick="ButtonPrint_Click"
                        Enabled="False" />
                    <asp:Button ID="BtnPublish" runat="server" Text="公告" CssClass="bt_large" OnClick="BtnPublish_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
