<%@ Page Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="CertifChangeConfirm.aspx.cs" Inherits="ZYRYJG.CertifManage.CertifChangeConfirm" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<%@ Register Src="../PostSelect.ascx" TagName="PostSelect" TagPrefix="uc1" %>
<%@ Register Src="../CheckAll.ascx" TagName="CheckAll" TagPrefix="uc3" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    </telerik:RadCodeBlock>
    <telerik:RadWindowManager runat="server" RestrictionZoneID="offsetElement" ID="RadWindowManager1"
        EnableShadow="true" EnableEmbeddedScripts="true" Skin="Windows7" VisibleStatusbar="false">
    </telerik:RadWindowManager>
    <%-- <script type="text/javascript">
        function onRequestStart(sender, args) {

            if (args.get_eventTarget().indexOf("ButtonExportExcel") >= 0) {
                args.set_enableAjax(false);

            }
        }

    
    </script>--%>

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" EnableAJAX="true">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadGrid1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default" />
    <div class="div_out">
        <div class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt; 证书管理 &gt;&gt;
                <asp:Label ID="LabelPostType" runat="server" Text="三类人员"></asp:Label>
                &gt;&gt; <strong>变更决定</strong>
            </div>
        </div>
        <div class="table_border" style="width: 98%; margin: 5px auto;">
            <div class="content">
                <table class="bar_cx" runat="server" id="DivSearch">
                    <tr>
                        <td align="right" nowrap="nowrap" width="11%">&nbsp;决定状态：
                        </td>
                        <td align="left">
                            <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>

                                    <td align="left" width="45%">
                                        <asp:RadioButtonList ID="RadioButtonListStatus" runat="server" RepeatDirection="Horizontal"
                                            AutoPostBack="false" Style="float: left;" Width="150px">
                                            <asp:ListItem Value="未决定" Selected="True">未决定</asp:ListItem>
                                            <asp:ListItem Value="已决定">已决定</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                    <td align="right" nowrap="nowrap" width="25%">变更类型：
                                    </td>
                                    <td align="left" width="30%">
                                        <telerik:RadComboBox ID="RadComboBoxChangeType" runat="server" Width="97%" Skin="Default">
                                            <Items>
                                                <telerik:RadComboBoxItem Value="" Text="全部" Selected="True" />
                                                <telerik:RadComboBoxItem Value="京内变更" Text="京内变更" />
                                                <telerik:RadComboBoxItem Value="离京变更" Text="离京变更" />
                                                <telerik:RadComboBoxItem Value="注销" Text="注销" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>

                                </tr>
                            </table>
                        </td>
                        <td align="right" width="11%" nowrap="nowrap">岗位工种：
                        </td>
                        <td align="left" width="39%">
                            <uc1:PostSelect ID="PostSelect1" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap="nowrap">姓 名：</td>

                        <td align="left" width="39%">
                            <telerik:RadTextBox ID="rdtxtWorkerName" runat="server" Width="95%" Skin="Default">
                            </telerik:RadTextBox>
                        </td>
                        <td align="right" nowrap="nowrap" width="11%">证件号码：
                        </td>
                        <td align="left" width="39%">
                            <telerik:RadTextBox ID="rdtxtZJHM" runat="server" Width="95%" Skin="Default">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap="nowrap">审核人：</td>
                        <td align="left" width="20%">
                            <telerik:RadTextBox ID="RadTextBoxGetMan" runat="server" Width="95%" Skin="Default">
                            </telerik:RadTextBox>
                        </td>
                        <td align="right" nowrap="nowrap" width="11%">审核时间：
                        </td>
                        <td align="left">
                            <telerik:RadDatePicker ID="RadDatePicker_GetDateStart" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                Width="45%" />
                            <div class="RadPicker">至</div>
                            <telerik:RadDatePicker ID="RadDatePicker_GetDateEnd" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                Width="45%" />
                        </td>
                    </tr>
                    <tr>
                        <td width="11%" align="right" nowrap="nowrap">证书编号：
                        </td>
                        <td width="39%" align="left">
                            <telerik:RadTextBox ID="rdtxtCertificateCode" runat="server" Width="95%" Skin="Default">
                            </telerik:RadTextBox>
                        </td>
                        <td align="right" nowrap="nowrap">原单位名称：
                        </td>
                        <td align="left">
                            <telerik:RadTextBox ID="rdtxtQYMC" runat="server" Width="95%" Skin="Default">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap="nowrap">新单位名称：
                        </td>
                        <td align="left">
                            <telerik:RadTextBox ID="RadTextBoxNewUnit" runat="server" Width="95%" Skin="Default">
                            </telerik:RadTextBox>
                        </td>
                        <td align="right" nowrap="nowrap">新单位机构代码：
                        </td>
                        <td align="left">
                            <telerik:RadTextBox ID="RadTextBoxUnitCode" runat="server" Width="95%" Skin="Default" MaxLength="18">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="4" nowrap="nowrap">
                            <asp:Button ID="btnSearch" runat="server" Text="查 询" CssClass="button" OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                </table>
                <div class="table_cx">
                    <img alt="" src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                    证书变更申请列表
                </div>
                <div style="width: 99%; margin: 0 auto;">
                    <telerik:RadGrid ID="RadGrid1" AllowPaging="True"
                        SortingSettings-SortToolTip="单击进行排序" runat="server" AutoGenerateColumns="False" PagerStyle-AlwaysVisible="true"
                        AllowSorting="True" PageSize="10" GridLines="None" CellPadding="0" Width="100%"
                        Skin="Blue" EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false"
                        OnPageIndexChanged="RadGridAccept_PageIndexChanged" OnDataBound="RadGridAccept_DataBound">
                        <ClientSettings EnableRowHoverStyle="true">
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                        <MasterTableView DataKeyNames="CertificateChangeID,PostID,ApplyCode,ChangeType" NoMasterRecordsText="没有可显示的记录">
                            <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                            <Columns>
                                <telerik:GridTemplateColumn UniqueName="SelectAllColumn" HeaderText="Highlight <br/> ship name">
                                    <HeaderTemplate>
                                        <uc3:CheckAll ID="CheckAll1" runat="server" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" onclick='checkBoxClick(this.checked);' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn HeaderText="序号" UniqueName="RowNum" DataField="RowNum" AllowSorting="false">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="申请日期" UniqueName="ApplyDate" DataField="ApplyDate" HtmlEncode="false" DataFormatString="{0:yyyy.MM.dd}">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="岗位工种" UniqueName="PostName" DataField="PostName">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>

                                <telerik:GridTemplateColumn UniqueName="CertificateChangeID" HeaderText="申请单">
                                    <ItemTemplate>
                                        <span class="link_edit" onclick='javascript:SetIfrmSrc("Application.aspx?o=<%# Page.Server.UrlEncode(Utility.Cryptography.Encrypt(Eval("CertificateChangeID").ToString())) %>");'>
                                            <nobr>申请单</nobr>
                                        </span>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn UniqueName="CertificateCode" HeaderText="证书编号" SortExpression="CertificateCode">
                                    <ItemTemplate>
                                        <span class="link_edit" onclick='javascript:SetIfrmSrc("../PersonnelFile/CertificateInfo.aspx?o=<%# Page.Server.UrlEncode(Utility.Cryptography.Encrypt(Eval("CertificateID").ToString())) %>");'>
                                            <nobr><%# Eval("CertificateCode").ToString()%></nobr>
                                        </span>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" Font-Bold="true" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn UniqueName="Output_CertificateCode" DataField="CertificateCode"
                                    HeaderText="证书编号" Visible="false">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="NewWorkerName" DataField="NewWorkerName" HeaderText="姓　名">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="NewWorkerCertificateCode" DataField="NewWorkerCertificateCode"
                                    HeaderText="证件号码">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="UnitName" DataField="UnitName" HeaderText="原单位名称">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="NewUnitName" DataField="NewUnitName" HeaderText="新单位名称">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="审核日期" UniqueName="CheckDate" DataField="CheckDate" HtmlEncode="false" DataFormatString="{0:yyyy.MM.dd}">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="决定日期" UniqueName="ConfrimDate" DataField="ConfrimDate" HtmlEncode="false" DataFormatString="{0:yyyy.MM.dd}">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
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
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="DataAccess.CertificateChangeDAL"
                        DataObjectTypeName="Model.CertificateChangeOB" SelectMethod="GetList" EnablePaging="true"
                        SelectCountMethod="SelectCount" MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex"
                        SortParameterName="orderBy">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                                DefaultValue="and Status='已申请'" ConvertEmptyStringToNull="false" />
                        </SelectParameters>

                    </asp:ObjectDataSource>
                </div>
                <br />

                <table id="TableConfirm" runat="server" width="100%" border="0" cellpadding="5" cellspacing="1" class="table" style="text-align: center; margin: 10px auto">
                    <tr class="GridLightBK">
                        <td colspan="2" class="barTitle">批量决定操作</td>
                    </tr>
                    <tr class="GridLightBK">
                        <td width="20%" align="right">决定结果：</td>
                        <td width="80%" align="left">
                            <asp:RadioButtonList ID="RadioButtonListDecide" runat="server" RepeatDirection="Horizontal" TextAlign="right" AutoPostBack="true" OnSelectedIndexChanged="RadioButtonListDecide_SelectedIndexChanged">
                                <asp:ListItem Text="通过" Value="通过" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="不通过" Value="不通过"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td width="20%" align="right">决定意见：</td>
                        <td width="80%" align="left">

                            <asp:TextBox ID="TextBoxConfrimResult" runat="server" TextMode="MultiLine" Rows="3" Width="99%" MaxLength="200" Text="通过"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td colspan="2" align="center">
                            <asp:Button ID="ButtonDecide" runat="server" CssClass="bt_large" Text="确 定" OnClick="ButtonDecide_Click" />
                        </td>
                    </tr>
                </table>
                <br />
            </div>
            <uc4:IframeView ID="IframeView" runat="server" />
        </div>
</asp:Content>
