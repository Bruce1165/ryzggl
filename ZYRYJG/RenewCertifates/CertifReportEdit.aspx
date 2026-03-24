<%@ Page Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="CertifReportEdit.aspx.cs" Inherits="ZYRYJG.RenewCertifates.CertifReportEdit" %>

<%@ Register Src="../CheckAll.ascx" TagName="CheckAll" TagPrefix="uc3" %>
<%@ Register Src="../PostSelect.ascx" TagName="PostSelect" TagPrefix="uc1" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManager ID="RadAjaxManager2" runat="server" EnableAJAX="true">

        <AjaxSettings>

            <%-- <telerik:AjaxSetting AjaxControlID="RadGridAccept">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGridAccept" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>           
            <telerik:AjaxSetting AjaxControlID="ButtonAccept">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGridAccept" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ButtonSearch">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGridAccept" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="DivButtonCheck" />
                  </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="PostSelect1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="PostSelect1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>--%>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Windows7" />
    <telerik:RadWindowManager runat="server" RestrictionZoneID="offsetElement" ID="RadWindowManager1"
        EnableShadow="true" EnableEmbeddedScripts="true" Skin="Windows7" VisibleStatusbar="false">
    </telerik:RadWindowManager>
    <div class="div_out">
        <div id="div_top" class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt; 证书续期 &gt;&gt;
                <asp:Label ID="LabelAccepy" runat="server" Text="Label"></asp:Label>
                &gt;&gt; <strong>初审汇总上报</strong>
            </div>
        </div>
        <div class="content">
            <table class="bar_cx">
                <tr>
                    <td width="8%" align="right" nowrap="nowrap">岗位工种：
                    </td>
                    <td align="left" width="42%">
                        <uc1:PostSelect ID="PostSelect1" runat="server" />
                    </td>
                    <td width="8%" align="right" nowrap="nowrap">证书编号：
                    </td>
                    <td width="42%" align="left">

                        <telerik:RadTextBox ID="txtCertificateCode" runat="server" Width="33%" Skin="Default"
                            onkeydown="ButtonSearchClick(event); " Style="float: left;">
                        </telerik:RadTextBox>
                        <span style="float: left; padding: 0 4px; line-height: 22px;">有效期至：</span>
                        <telerik:RadDatePicker ID="txtValidEndtDate" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                            MinDate="01/01/1900" runat="server" Width="33%" Style="float: left;" />

                    </td>
                </tr>
                <tr>
                    <td align="right" nowrap="nowrap">姓名：
                    </td>
                    <td align="left">
                        <telerik:RadTextBox ID="txtWorkerName" runat="server" Width="97%" Skin="Default"
                            MaxLength="50" >
                        </telerik:RadTextBox>
                    </td>
                    <td align="right" nowrap="nowrap">证件号码：
                    </td>
                    <td align="left">
                        <telerik:RadTextBox ID="txtWorkerCertificateCode" runat="server" Width="97%" Skin="Default"
                            MaxLength="50" >
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" nowrap="nowrap">企业名称：
                    </td>
                    <td align="left">
                        <telerik:RadTextBox ID="txtUnitName" runat="server" Width="97%" Skin="Default" MaxLength="100"
                            >
                        </telerik:RadTextBox>
                    </td>
                    <td align="right" nowrap="nowrap">组织机构代码：
                    </td>
                    <td align="left">
                        <telerik:RadTextBox ID="RadTextBoxUnitCode" runat="server" Width="97%" Skin="Default"
                            MaxLength="9" >
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" nowrap="nowrap">申请批号：
                    </td>
                    <td align="left">
                        <telerik:RadTextBox ID="txtApplyCode" runat="server" Width="97%" Skin="Default" MaxLength="20"
                            >
                        </telerik:RadTextBox>
                    </td>
                    <td align="right" nowrap="nowrap">申请日期：
                    </td>
                    <td align="left">

                        <telerik:RadDatePicker ID="txtSApplyDate" MinDate="01/01/1900" runat="server" Width="46%" Style="float: left;" Calendar-DayCellToolTipFormat="yyyy年MM月dd日" />
                        <div class="RadPicker">至</div>
                        <telerik:RadDatePicker ID="txtEApplyDate" MinDate="01/01/1900" runat="server" Width="46%" Style="float: left;" Calendar-DayCellToolTipFormat="yyyy年MM月dd日" />
                    </td>
                </tr>
                <tr>
                    <td align="right" nowrap="nowrap">受理人：
                    </td>
                    <td align="left">
                        <telerik:RadTextBox ID="RadTextBoxGetMan" runat="server" Width="97%" Skin="Default" MaxLength="50"
                            >
                        </telerik:RadTextBox>
                    </td>
                    <td align="right" nowrap="nowrap">受理日期：
                    </td>
                    <td align="left">
                        <telerik:RadDatePicker ID="RadDatePickerGetDateStart" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                            Width="46%" Style="float: left;" />
                        <div class="RadPicker">至</div>
                        <telerik:RadDatePicker ID="RadDatePickerGetDateEnd" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                            Width="46%" Style="float: left;" />
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
                证书续期初审通过结果列表
            </div>
            <div style="width: 98%; margin: 0 auto;">
                <telerik:RadGrid ID="RadGridAccept" runat="server" AllowCustomPaging="true" GridLines="None"
                    AllowPaging="True" PageSize="10" AllowSorting="True" AutoGenerateColumns="False"
                    SortingSettings-SortToolTip="单击进行排序" Width="100%" Skin="Blue" EnableAjaxSkinRendering="false"
                    EnableEmbeddedSkins="false"
                    OnPageIndexChanged="RadGridAccept_PageIndexChanged" OnDataBound="RadGridAccept_DataBound">
                    <ClientSettings EnableRowHoverStyle="true">
                    </ClientSettings>
                    <MasterTableView NoMasterRecordsText="　没有可显示的记录"
                        DataKeyNames="CertificateContinueID">
                        <Columns>
                            <telerik:GridTemplateColumn UniqueName="TemplateColumn" HeaderText="Highlight <br/> ship name">
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" onclick='checkBoxClick(this.checked);' />
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <uc3:CheckAll ID="CheckAll1" runat="server" />
                                </HeaderTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" Width="30px" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="GetDate" DataField="GetDate" HtmlEncode="false"
                                DataFormatString="{0:yyyy-MM-dd}" HeaderText="受理日期">
                                <HeaderStyle HorizontalAlign="Center" Wrap="true" />
                                <ItemStyle HorizontalAlign="Center" Wrap="true" />
                            </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn UniqueName="GetMan" DataField="GetMan" HeaderText="受理人">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="PostName" DataField="PostName" HeaderText="岗位工种">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>

                            <telerik:GridTemplateColumn UniqueName="CertificateChangeID" HeaderText="证书编号">
                                <ItemTemplate>
                                    <span><%# Eval("CertificateCode")%></span>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" Font-Bold="true" />
                                <ItemStyle HorizontalAlign="Left" Wrap="false" />
                            </telerik:GridTemplateColumn>


                            <telerik:GridBoundColumn UniqueName="ValidEndDate" DataField="ValidEndDate" HtmlEncode="false"
                                DataFormatString="{0:yyyy-MM-dd}" HeaderText="有效期至">
                                <HeaderStyle HorizontalAlign="Center" Wrap="true" />
                                <ItemStyle HorizontalAlign="Center" Wrap="true" />
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn UniqueName="WorkerName" DataField="WorkerName" HeaderText="姓名">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="WorkerCertificateCode" DataField="WorkerCertificateCode"
                                HeaderText="证件号码">
                                <HeaderStyle HorizontalAlign="Center" Wrap="true" />
                                <ItemStyle HorizontalAlign="Center" Wrap="true" />
                            </telerik:GridBoundColumn>

                            <telerik:GridTemplateColumn UniqueName="UnitName" HeaderText="企业名称">
                                <ItemTemplate>
                                    <%# Eval("NEWUNITNAME")!= DBNull.Value && Eval("NEWUNITNAME").ToString() !=Eval("UNITNAME").ToString()?"<span style='color:red'>新单位：</span>"+Eval("NEWUNITNAME"):Eval("UNITNAME")%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridTemplateColumn>



                        </Columns>
                        <HeaderStyle Font-Bold="True" />
                        <EditFormSettings>
                            <EditColumn UniqueName="EditCommandColumn1">
                            </EditColumn>
                        </EditFormSettings>
                        <PagerTemplate>
                            <uc2:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                        </PagerTemplate>
                    </MasterTableView>
                </telerik:RadGrid>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetList"
                    TypeName="DataAccess.CertificateContinueDAL" SelectCountMethod="SelectCount"
                    EnablePaging="true" MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex"
                    SortParameterName="orderBy">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                            DefaultValue="" ConvertEmptyStringToNull="false" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </div>
            <div id="DivButtonCheck" runat="server" style="width: 90%; margin: 5px auto; text-align: center; padding-top: 8px;">
                <p>
                    提示：勾选要汇总的审批数据，保存后导出汇总单，签字盖章后扫描上传到系统中。
                </p>
                <asp:Button ID="ButtonAccept" runat="server" Text="保存汇总" CssClass="bt_large" OnClick="ButtonAccept_Click" />
                <input id="ButtonReturn" type="button" value="返 回" class="button" onclick="javascript: hideIfam();" />
            </div>
        </div>
        <br />
    </div>
</asp:Content>
