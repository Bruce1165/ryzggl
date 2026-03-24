<%@ Page Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="CertifEnterConfirm.aspx.cs" Inherits="ZYRYJG.CertifEnter.CertifEnterConfirm" %>

<%@ Register Src="../PostSelect.ascx" TagName="PostSelect" TagPrefix="uc1" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<%@ Register Src="../CheckAll.ascx" TagName="CheckAll" TagPrefix="uc3" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc4" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    </telerik:RadCodeBlock>
    <telerik:RadWindowManager ID="RadWindowManager1" ShowContentDuringLoad="false" VisibleStatusbar="false"
        ReloadOnShow="true" runat="server" Skin="Windows7" EnableShadow="true">
    </telerik:RadWindowManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" EnableAJAX="true">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadGrid1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ButtonSearch">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="ButtonCheck" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ButtonCheck">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="PostSelect1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="PostSelect1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true"
        Skin="Windows7" />
    <div class="div_out">
        <div class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt; 证书进京 &gt;&gt;
                <asp:Label ID="LabelPostType" runat="server" Text="三类人员"></asp:Label>
                &gt;&gt; <strong>进京决定</strong>
            </div>
        </div>
        <div class="content">
            <table class="bar_cx">
                <tr>
                    <td align="right" nowrap="nowrap" width="11%">编号批次：
                    </td>
                    <td align="left">
                        <telerik:RadTextBox ID="rdtxtApplyCode" runat="server" Width="97%" Skin="Default"
                            >
                        </telerik:RadTextBox>
                    </td>
                    <td align="right" width="11%" nowrap="nowrap">岗位工种：
                    </td>
                    <td align="left" width="39%">
                        <uc1:PostSelect ID="PostSelect1" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="right" nowrap="nowrap">姓名：
                    </td>
                    <td align="left" width="39%">
                        <telerik:RadTextBox ID="rdtxtWorkerName" runat="server" Width="97%" Skin="Default"
                            >
                        </telerik:RadTextBox>
                    </td>
                    <td align="right" nowrap="nowrap">单位名称：
                    </td>
                    <td align="left">
                        <telerik:RadTextBox ID="rdtxtQYMC" runat="server" Width="97%" Skin="Default" >
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
                待审批证书列表
            </div>
            <div style="width: 98%; margin: 0 auto;">
                <telerik:RadGrid ID="RadGrid1" AutoGenerateColumns="False" runat="server" AllowPaging="True"
                    PageSize="10" AllowSorting="True" SortingSettings-SortToolTip="单击进行排序" Skin="Blue"
                    EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false" Width="100%" GridLines="None"  PagerStyle-AlwaysVisible="true"
                    OnPageIndexChanged="RadGridAccept_PageIndexChanged" OnDataBound="RadGridAccept_DataBound">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                    <MasterTableView CommandItemDisplay="None" DataKeyNames="CertificateID" NoMasterRecordsText="　没有可显示的记录">
                        <Columns>
                            <telerik:GridTemplateColumn UniqueName="SelectAllColumn" HeaderText="Highlight <br/> ship name">
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" onclick='checkBoxClick(this.checked);' />
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <uc3:CheckAll ID="CheckAll1" runat="server" />
                                </HeaderTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                <HeaderStyle HorizontalAlign="Center" Font-Bold="true" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="WorkerName" DataField="WorkerName" HeaderText="姓名">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="UnitName" DataField="UnitName" HeaderText="单位名称">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
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
                            <telerik:GridTemplateColumn HeaderText="有效期">
                                <ItemTemplate>
                                    <%# Convert.ToDateTime(Eval("ValidStartDate")) == DateTime.MinValue?"":Convert.ToDateTime(Eval("ValidStartDate")).ToString("yyyy.MM.dd") + "-" + Convert.ToDateTime(Eval("ValidEndDate")).ToString("yyyy.MM.dd")%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Font-Bold="true" Wrap="false" />
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
                    DataObjectTypeName="Model.ExamResultOB" SelectMethod="GetList" EnablePaging="true"
                    SelectCountMethod="SelectCount" MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex"
                    SortParameterName="orderBy">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                            DefaultValue="" ConvertEmptyStringToNull="false" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <br />
                <asp:Button ID="ButtonCheck" runat="server" Text="审批通过" CssClass="bt_large" OnClick="ButtonCheck_Click" />
            </div>
        </div>
    </div>
    <uc4:IframeView ID="IframeView" runat="server" />
</asp:Content>
