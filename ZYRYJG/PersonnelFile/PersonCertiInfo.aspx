<%@ Page Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="PersonCertiInfo.aspx.cs" Inherits="ZYRYJG.PersonnelFile.PersonCertiInfo" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../PostSelect.ascx" TagName="PostSelect" TagPrefix="uc1" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    </telerik:RadCodeBlock>
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
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="PostSelect1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="PostSelect1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ButtonExportToExcel">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ButtonExportToExcel" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Windows7" />
    <telerik:RadWindowManager runat="server" RestrictionZoneID="offsetElement" ID="RadWindowManager1" Skin="Windows7"
        EnableShadow="true" EnableEmbeddedScripts="true">
    </telerik:RadWindowManager>
    <div class="div_out">
        <%--<div class="dqts">--%>
          <div id="div_top" class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt; 人员档案 &gt;&gt;
                <strong>人员证书信息</strong>
            </div>
        </div>
        <div class="content">
            <table class="bar_cx">
                <tr>
                    <td align="right" nowrap="nowrap" width="11%">岗位工种：
                    </td>
                    <td align="left" width="39%">
                        <uc1:PostSelect ID="PostSelect1" runat="server" />
                    </td>
                    <td align="right" nowrap="nowrap">证书状态：
                    </td>
                    <td align="left">
                        <telerik:RadComboBox ID="RadComboBoxStatus" runat="server" Width="97%" ToolTip="注意：各状态之间可能有重叠（如已过期中可能包括离京、注销），不能简单相加计算总数！">
                            <Items>
                                <telerik:RadComboBoxItem Text="全部" Value="" />
                                <telerik:RadComboBoxItem Text="当前有效（全部）" Value="当前有效" Selected="true" />
                                <telerik:RadComboBoxItem Text="当前有效（首次）" Value="首次" />
                                <telerik:RadComboBoxItem Text="当前有效（续期）" Value="续期" />
                                <telerik:RadComboBoxItem Text="当前有效（京内变更）" Value="京内变更" />
                                <telerik:RadComboBoxItem Text="当前有效（进京变更）" Value="进京变更" />
                                <telerik:RadComboBoxItem Text="当前有效（遗失补办）" Value="补办" />
                                <telerik:RadComboBoxItem Text="已过期" Value="已过期" />
                                <telerik:RadComboBoxItem Text="已离京" Value="离京变更" />
                                <telerik:RadComboBoxItem Text="已注销" Value="注销" />
                                <telerik:RadComboBoxItem Text="已注销（超龄注销）" Value="超龄注销" />
                                <telerik:RadComboBoxItem Text="待审批" Value="待审批" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" nowrap="nowrap" width="11%">证书编号：
                    </td>
                    <td align="left" width="39%">
                        <telerik:RadTextBox ID="RadTextBoxCertificateCode" runat="server" Width="97%" Skin="Default"
                            >
                        </telerik:RadTextBox>
                    </td>
                    <td align="right" nowrap="nowrap" width="11%">发证年度：
                    </td>
                    <td align="left" width="39%">
                        <span style="float: left;">
                            <telerik:RadNumericTextBox ID="RadNumericTextBoxConferData" runat="server" Width="100px" Style="float: left;" MaxLength="4" MaxValue="2050" MinValue="1950" DataType="Number" NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" ShowSpinButtons="true">
                            </telerik:RadNumericTextBox></span>
                        <span style="float: left; padding: 0 4px; line-height: 22px;">有效期至：</span>
                        <telerik:RadDatePicker ID="RadDatePickerFrom" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                            MinDate="01/01/1900" runat="server" Width="120px" Style="float: left;" />
                        <span style="float: left; padding: 0 4px; line-height: 22px;">至</span>
                        <telerik:RadDatePicker ID="RadDatePickerEnd" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                            MinDate="01/01/1900" runat="server" Width="120px" Style="float: left;" />
                    </td>
                </tr>
                <tr>
                    <td align="right" nowrap="nowrap">姓名：
                    </td>
                    <td align="left">
                        <telerik:RadTextBox ID="RadTextBoxWorkerName" runat="server" Width="200px" Skin="Default" Style="float: left;">
                        </telerik:RadTextBox>
                        <span style="float: left; padding: 0 4px 0 20px; line-height: 22px;">证件号码：</span>
                        <telerik:RadTextBox ID="RadTextBoxWorkerCertificateCode" runat="server" Width="200px" Skin="Default" Style="float: left;"></telerik:RadTextBox>
                    </td>
                    <td align="right" nowrap="nowrap">企业名称：
                    </td>
                    <td align="left">
                        <telerik:RadTextBox ID="RadTextBoxUnitName" runat="server" Width="200px" Skin="Default" Style="float: left;">
                        </telerik:RadTextBox>
                        <span style="float: left; padding: 0 4px 0 20px; line-height: 22px;">组织机构代码：</span>
                        <telerik:RadTextBox ID="RadTextBoxUnitCode" runat="server" Width="200px" Skin="Default" Style="float: left;"></telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" nowrap="nowrap">锁定状态：
                    </td>
                    <td align="left">
                        <asp:RadioButtonList ID="RadioButtonListLockStatus" runat="server" RepeatDirection="Horizontal" width="300px">
                            <asp:ListItem Text="全部" Value="全部" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="锁定中" Value="锁定中"></asp:ListItem>
                            <asp:ListItem Text="未锁定" Value="未锁定"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td align="right" nowrap="nowrap">打印时间：
                    </td>
                    <td width="43%" align="left">
                        <telerik:RadDatePicker ID="RadDatePicker_PrintStartDate" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                            Width="46%" />
                        <div class="RadPicker">至</div>
                        <telerik:RadDatePicker ID="RadDatePicker_PrintEndDate" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                            Width="46%" />
                    </td>
                </tr>
                <tr>
                    <td align="right" nowrap="nowrap">电子证书：
                    </td>
                    <td align="left">
                        <asp:CheckBox ID="CheckBoxOnlyShowDZZS" runat="server" Text="只显示有电子证书数据" />
                    </td>
                    <td align="right" nowrap="nowrap">电子证书生成时间：
                    </td>
                    <td width="43%" align="left">
                        <telerik:RadDatePicker ID="RadDatePickerDZZSBegin" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                            Width="46%" />
                        <div class="RadPicker">至</div>
                        <telerik:RadDatePicker ID="RadDatePickerDZZSEnd" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
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
                <img src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                人员证书信息列表
            </div>
            <div style="width: 98%; margin: 0 auto;">
                <telerik:RadGrid ID="RadGrid1" runat="server" GridLines="None"
                    AllowPaging="True" PageSize="10" AllowSorting="True" AutoGenerateColumns="False"
                    SortingSettings-SortToolTip="单击进行排序" Width="100%" Skin="Blue" EnableAjaxSkinRendering="false"
                    EnableEmbeddedSkins="false">
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
                            <telerik:GridBoundColumn UniqueName="PostTypeName" DataField="PostTypeName" HeaderText="岗位类别">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="PostName" DataField="PostName" HeaderText="岗位工种">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="SkillLevel" DataField="SkillLevel" HeaderText="等级" Visible="false">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="CertificateCode" HeaderText="证书编号">
                                <ItemTemplate>
                                    <span class="link_edit" onclick='javascript:SetIfrmSrc("../PersonnelFile/CertificateInfo.aspx?o=<%# Utility.Cryptography.Encrypt(Eval("CertificateID").ToString())%>");'>
                                        <nobr><%# Eval("CertificateCode").ToString()%></nobr>
                                    </span>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" Font-Bold="true" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                            </telerik:GridTemplateColumn>
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
                            <telerik:GridTemplateColumn UniqueName="SingUp" HeaderText="变更历史">
                                <ItemTemplate>
                                    <span class="link_edit" onclick='javascript:SetIfrmSrc("CertifHistory.aspx?o=<%# Eval("CertificateID")%>");'>变更历史</span>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" Font-Bold="true" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="CertificateCode" HeaderText="电子证书">
                                <ItemTemplate>                                   
                                    <%# (Convert.ToDateTime(Eval("ValidEndDate")).AddDays(1) < DateTime.Now || Eval("Status").ToString() == "注销" || Eval("Status").ToString() == "离京变更" || Eval("PostTypeID").ToString() == "3" || Eval("PostID").ToString() == "55" || Eval("PostID").ToString() == "159" || Eval("PostID").ToString() == "1009" || Eval("PostID").ToString() == "1021" || Eval("PostID").ToString() == "1024") ? "" : string.Format("<span class=\"link_edit\" onclick='javascript:SetIfrmSrc(\"../PersonnelFile/CertificatePdf.aspx?c={0}\");'><nobr>电子证书</nobr></span>", Page.Server.UrlEncode(Utility.Cryptography.Encrypt(Eval("CertificateID").ToString())))%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" Wrap="false" ForeColor="Blue" />
                            </telerik:GridTemplateColumn>
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
                    DeleteMethod="Delete" InsertMethod="Insert" SelectMethod="GetList" TypeName="DataAccess.CertificateDAL"
                    UpdateMethod="Update" SelectCountMethod="SelectCount" EnablePaging="true" MaximumRowsParameterName="maximumRows"
                    StartRowIndexParameterName="startRowIndex" SortParameterName="orderBy">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                            DefaultValue="" ConvertEmptyStringToNull="false" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </div>
            <div id="DivButtonOutput" runat="server" style="width: 90%; margin: 5px auto; text-align: center; padding-top: 8px;">
                <p>
                    提示：导出格式为csv格式（不带样式，不受最大行数限制，可用excel打开修改样式另存为其它样式）。
                </p>
                <asp:Button ID="ButtonExportToExcel" CssClass="bt_large" Text="导出查询结果" OnClick="ButtonExportToExcel_Click"
                    runat="server"></asp:Button>
            </div>
        </div>
        <br />
    </div>
    <uc4:IframeView ID="IframeView" runat="server" />
</asp:Content>
