<%@ Page Title="" Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="ScoreView.aspx.cs" Inherits="ZYRYJG.EXamManage.ScoreView" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../ExamPlanSelect.ascx" TagName="ExamPlanSelect" TagPrefix="uc1" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc4" %>
<%@ Register Src="../PostSelect.ascx" TagPrefix="uc3" TagName="PostSelect" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    </telerik:RadCodeBlock>
    <telerik:RadWindowManager ID="RadWindowManager1" ShowContentDuringLoad="false" VisibleStatusbar="false"
        runat="server" Skin="Windows7" EnableShadow="true" OnClientClose="OnClientClose">
    </telerik:RadWindowManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1"
        EnableAJAX="true">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadGridExamResult">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGridExamResult" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ButtonSearch">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGridExamResult" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="PostSelect2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="PostSelect2" LoadingPanelID="RadAjaxLoadingPanel1" />
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
                考场管理 &gt;&gt; <strong>查看考试结果</strong>
            </div>
        </div>
        <div class="content">
            <table class="bar_cx">
                <tr id="TrExamPlan" runat="server">
                    <td align="right" nowrap="nowrap" width="9%">考试计划：<span style="color: Red">(必选)</span>
                    </td>
                    <td align="left" colspan="5">
                        <uc1:ExamPlanSelect ID="ExamPlanSelect1" runat="server" />
                    </td>
                </tr>
                <tr id="TrPost" runat="server">
                    <td width="9%" align="right" nowrap="nowrap">岗位工种：
                    </td>
                    <td align="left" colspan="3">
                        <uc3:PostSelect ID="PostSelect2" runat="server" />
                    </td>
                    <td width="9%" align="right" nowrap="nowrap">考试时间：
                    </td>
                    <td width="24%" align="left">
                        <telerik:RadComboBox ID="RadComboBoxYear" runat="server" Width="60px">
                            <Items>
                            </Items>
                        </telerik:RadComboBox>
                        &nbsp;年-&nbsp;
                                        <telerik:RadComboBox ID="RadComboBoxMonth" runat="server" Width="60px">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="全部" Value="" Selected="true" />
                                                <telerik:RadComboBoxItem Text="1月" Value="01" />
                                                <telerik:RadComboBoxItem Text="2月" Value="02" />
                                                <telerik:RadComboBoxItem Text="3月" Value="03" />
                                                <telerik:RadComboBoxItem Text="4月" Value="04" />
                                                <telerik:RadComboBoxItem Text="5月" Value="05" />
                                                <telerik:RadComboBoxItem Text="6月" Value="06" />
                                                <telerik:RadComboBoxItem Text="7月" Value="07" />
                                                <telerik:RadComboBoxItem Text="8月" Value="08" />
                                                <telerik:RadComboBoxItem Text="9月" Value="09" />
                                                <telerik:RadComboBoxItem Text="10月" Value="10" />
                                                <telerik:RadComboBoxItem Text="11月" Value="11" />
                                                <telerik:RadComboBoxItem Text="12月" Value="12" />
                                            </Items>
                                        </telerik:RadComboBox>
                        &nbsp;月
                    </td>
                </tr>
                <tr id="TrPerson" runat="server" width="9%">
                    <td align="right" nowrap="nowrap">考生姓名：
                    </td>
                    <td align="left" width="24%">
                        <telerik:RadTextBox ID="RadTextBoxWorkerName" runat="server" Width="97%" Skin="Default">
                        </telerik:RadTextBox>
                    </td>
                    <td align="right" nowrap="nowrap" width="9%">证件号码：
                    </td>
                    <td align="left" width="24%">
                        <telerik:RadTextBox ID="RadTextWorkerCertificateCode" runat="server" Width="97%" Skin="Default">
                        </telerik:RadTextBox>
                    </td>


                    <td align="right" nowrap="nowrap" width="9%">准考证号：
                    </td>
                    <td align="left" width="24%">
                        <telerik:RadTextBox ID="RadTextBoxExamCardID" runat="server" Width="97%" Skin="Default">
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" nowrap="nowrap" width="9%">考试结果：
                    </td>
                    <td align="left" colspan="5">
                        <asp:RadioButtonList ID="RadioButtonListExamResult" runat="server" RepeatDirection="Horizontal" Width="200px">
                            <asp:ListItem Text="全部" Value="" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="合格" Value="合格"></asp:ListItem>
                            <asp:ListItem Text="不合格" Value="不合格"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" align="center">
                        <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
                    </td>
                </tr>
            </table>

            <div class="table_cx">
                <img alt="" src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                考试结果列表（单击列名可排序）
            </div>
            <div style="width: 98%; margin: 0 auto;">
                <telerik:RadGrid ID="RadGridExamResult" runat="server"
                    GridLines="None" AllowPaging="True" PageSize="10" AllowSorting="True" AutoGenerateColumns="False"
                    SortingSettings-SortToolTip="单击进行排序" Width="100%" Skin="Blue" EnableAjaxSkinRendering="false"
                    EnableEmbeddedSkins="false">
                    <ClientSettings EnableRowHoverStyle="true">
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ExamResultID" NoMasterRecordsText="　没有可显示的记录">
                        <Columns>
                            <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="考试时间" UniqueName="ExamStartTime" DataField="ExamStartTime"
                                DataFormatString="{0:yyyy-MM-dd}">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="ExamCardID" DataField="ExamCardID" HeaderText="准考证号">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <%--<telerik:GridBoundColumn UniqueName="PostTypeName" DataField="PostTypeName" HeaderText="岗位类别">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>--%>
                            <telerik:GridBoundColumn UniqueName="PostName" DataField="PostName" HeaderText="岗位工种">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="WorkerName" DataField="WorkerName" HeaderText="姓名">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="WORKERCERTIFICATECODE" DataField="WORKERCERTIFICATECODE"
                                HeaderText="证件号码">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="S_Phone" DataField="S_Phone" HeaderText="联系方式">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="UnitName" DataField="UnitName" HeaderText="工作单位">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="ExamResult" HeaderText="考试结果">
                                <ItemTemplate>
                                    <%# Eval("ExamResult").ToString() == "合格" ? "合格" : string.Format("{0}（{1}）", Eval("ExamResult").ToString(), Eval("SumScoreDetail").ToString())%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" Font-Bold="true" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn UniqueName="CERTIFICATECODE" DataField="CertificateCode"
                                HeaderText="证书编号">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                           <%-- <telerik:GridBoundColumn UniqueName="S_TRAINUNITNAME" DataField="S_TRAINUNITNAME"
                                HeaderText="领证地点">
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>--%>
                        </Columns>
                        <PagerTemplate>
                            <uc2:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                        </PagerTemplate>
                    </MasterTableView>
                </telerik:RadGrid>
                <div style="padding: 15px 15px;">
                    <asp:Button ID="ButtonPrint" runat="server" Text="导出名单(Excel)" CssClass="bt_maxlarge"
                        OnClick="ButtonPrint_Click" />
                </div>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="DataAccess.ExamResultDAL"
                    DataObjectTypeName="Model.ExamResultOB" SelectMethod="GetListView_ExamScore_New"
                    EnablePaging="true" SelectCountMethod="SelectCountView_ExamScore_New" MaximumRowsParameterName="maximumRows"
                    StartRowIndexParameterName="startRowIndex" SortParameterName="orderBy">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                            DefaultValue="" ConvertEmptyStringToNull="false" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </div>
        </div>
    </div>
    <uc4:IframeView ID="IframeView" runat="server" />
    <script language="javascript" type="text/javascript">
        function downloadCSS(url) {
            var elem = document.createElement("link");
            elem.rel = "stylesheet";
            elem.type = "text/css";
            elem.href = url;
            document.body.appendChild(elem);
        }
    </script>
</asp:Content>
