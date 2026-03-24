<%@ Page Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="CertifStatusList.aspx.cs" Inherits="ZYRYJG.RenewCertifates.CertifStatusList" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../CheckAll.ascx" TagName="CheckAll" TagPrefix="uc3" %>
<%@ Register Src="../PostSelect.ascx" TagName="PostSelect" TagPrefix="uc1" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" EnableAJAX="true">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadGridCheck">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGridCheck" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ButtonSearch">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGridCheck" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="PostSelect1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="PostSelect1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Windows7" />
    <telerik:RadWindowManager runat="server" RestrictionZoneID="offsetElement" ID="RadWindowManager1"
        EnableShadow="true" EnableEmbeddedScripts="true" Skin="Windows7" VisibleStatusbar="false">
    </telerik:RadWindowManager>
    <div class="div_out">
        <div class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt; 证书续期 &gt;&gt;
                <asp:Label ID="LabelCheck" runat="server" Text="Label"></asp:Label>&gt;&gt; <strong>查看续期状态</strong>
            </div>
        </div>
        <div class="table_cx" style="padding-right: 40px; float: left; clear: right; padding-left: 20px;">
            <a id="Link_DelHistory" runat="server" href='DelHistory.aspx' style="color: #DC2804; font-weight: bold;">
                <img alt="" src="../Images/jia.gif" style="width: 14px; height: 15px; margin-bottom: -2px; padding-right: 5px; border: none;" />查看删除历史记录</a>
        </div>
        <div class="table_border" style="width: 98%; margin: 5px auto;">
            <div class="content">
                <table class="bar_cx">
                    <tr>
                        <td width="8%" align="right" nowrap="nowrap">岗位工种：
                        </td>
                        <td align="left" width="42%">
                            <uc1:PostSelect ID="PostSelect1" runat="server" />
                        </td>
                        <td width="8%" align="right" nowrap="nowrap">
                        </td>
                        <td width="42%" align="left">
                        </td>
                    </tr>
                     <tr>
                        <td align="right" nowrap="nowrap">证书编号：
                        </td>
                        <td nowrap="nowrap" align="left"> 
                             <telerik:RadTextBox ID="RadTextBoxCertificateCode" runat="server" Width="33%" Skin="Default"
                                onkeydown="ButtonSearchClick(event); " Style="float: left;">
                            </telerik:RadTextBox>
                             <span style="float: left; padding: 0 4px 0 20px; line-height: 22px;">发证年度：</span>
                             <telerik:RadNumericTextBox ID="RadNumericTextBoxConferData" runat="server" Width="100px" Style="float: left;" MaxLength="4" MaxValue="2050" MinValue="1950" DataType="Number" NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" ShowSpinButtons="true">
                            </telerik:RadNumericTextBox>
                        </td>
                        <td align="right" nowrap="nowrap">有效期截止：
                        </td>
                        <td align="left">
                            <span style="float: left; padding: 0 4px; line-height: 22px;">从</span>
                             <telerik:RadDatePicker ID="RadDatePickerFrom" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                MinDate="01/01/1900" runat="server" Width="33%" Style="float: left;" />
                           <span style="float: left; padding: 0 4px; line-height: 22px;">至</span>
                            <telerik:RadDatePicker ID="RadDatePickerEnd" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                MinDate="01/01/1900" runat="server" Width="33%" Style="float: left;" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap="nowrap">姓名：
                        </td>
                        <td align="left">
                            <telerik:RadTextBox ID="RadTextBoxWorkerName" runat="server" Width="33%" Skin="Default" Style="float: left;" >
                            </telerik:RadTextBox>
                              <span style="float: left; padding: 0 4px 0 20px; line-height: 22px;">证件号码：</span> 
                            <telerik:RadTextBox ID="RadTextBoxWorkerCertificateCode" runat="server" Width="33%" Skin="Default" Style="float: left;" > </telerik:RadTextBox>
                        </td>
                        <td align="right" nowrap="nowrap">企业名称：
                        </td>
                        <td align="left">
                            <telerik:RadTextBox ID="RadTextBoxUnitName" runat="server" Width="33%" Skin="Default" Style="float: left;" >
                            </telerik:RadTextBox>
                             <span style="float: left; padding: 0 4px 0 20px; line-height: 22px;">组织机构代码：</span>   
                            <telerik:RadTextBox ID="RadTextBoxUnitCode" runat="server" Width="33%" Skin="Default" Style="float: left;"></telerik:RadTextBox>
                        </td>                       
                    </tr>                   
                    <tr>
                         <td align="right" nowrap="nowrap">继续教育形式：
                        </td>
                        <td align="left">
                            <asp:RadioButtonList ID="RadioButtonListjxjyway" runat="server" RepeatDirection="Horizontal" Width="400px"
                                AutoPostBack="false">
                                <asp:ListItem Value="" Selected="True">全部</asp:ListItem>
                                <asp:ListItem Value="1">面授</asp:ListItem>
                                <asp:ListItem Value="2">网络教育</asp:ListItem>
                                <asp:ListItem Value="3">面授+网络教育</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td align="right" width="11%" nowrap="nowrap">初审单位：
                        </td>
                        <td align="left" width="39%">
                            <telerik:RadTextBox ID="RadTextBoxFirstCheckUnitName" runat="server" Width="97%"
                                Skin="Default"  ToolTip="只输入关键字即可模糊查询">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap="nowrap" width="11%">续期状态：
                        </td>
                        <td align="left" width="39%">
                            <asp:RadioButtonList ID="rbl" runat="server" RepeatDirection="Horizontal" AutoPostBack="false" Width="400px">
                                <asp:ListItem Value="" Selected="True">全部</asp:ListItem>
                                <asp:ListItem Value="已申请">已申请</asp:ListItem>
                                <asp:ListItem Value="已初审">已初审</asp:ListItem>
                                <asp:ListItem Value="已审核">已审核</asp:ListItem>
                                <asp:ListItem Value="已决定">已决定</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td width="11%" align="right" nowrap="nowrap">续期结果：
                        </td>
                        <td width="39%" align="left">
                            <asp:RadioButtonList ID="RadioButtonListResult" runat="server" RepeatDirection="Horizontal"
                                AutoPostBack="false" Width="500px">
                                <asp:ListItem Value="" Selected="True">全部</asp:ListItem>
                                <asp:ListItem Value="不予受理">不予受理</asp:ListItem>
                                <asp:ListItem Value="决定通过">通过</asp:ListItem>
                                <asp:ListItem Value="决定不通过">不通过</asp:ListItem>
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
                    <img src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                    证书续期初审列表
                </div>
                <div id="DivGrid" style="width: 98%; margin: 0 auto; overflow: auto;" runat="server">
                    <telerik:RadGrid ID="RadGridCheck" runat="server"
                        GridLines="None" AllowPaging="True" PageSize="10" AllowSorting="True" AutoGenerateColumns="False"  PagerStyle-AlwaysVisible="true"
                        SortingSettings-SortToolTip="单击进行排序" Width="100%" Skin="Blue" EnableAjaxSkinRendering="false"
                        EnableEmbeddedSkins="false" OnExcelExportCellFormatting="RadGridCheck_ExcelExportCellFormatting">
                        <ClientSettings EnableRowHoverStyle="true">
                        </ClientSettings>
                        <MasterTableView NoMasterRecordsText="　没有可显示的记录"
                            DataKeyNames="CertificateContinueID">
                            <Columns>
                                <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn UniqueName="jxjyway" HeaderText="继续教育形式">
                                    <ItemTemplate>                                        
                                            <%# ZYRYJG.UIHelp.GetJxjyType( Eval("jxjyway")) %>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" Font-Bold="true" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false"  />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn UniqueName="PostName" DataField="PostName" HeaderText="岗位工种">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn UniqueName="CertificateChangeID" HeaderText="证书编号">
                                <ItemTemplate>
                                    <span class="link_edit" onclick='javascript:SetIfrmSrc("ApplyDetail.aspx?o=<%# Utility.Cryptography.Encrypt(Eval("CertificateID").ToString())%>&o2=<%# Utility.Cryptography.Encrypt(Eval("CertificateContinueID").ToString()) %>");'>
                                        <%# Eval("CertificateCode")%></span>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" Font-Bold="true" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                            </telerik:GridTemplateColumn> 
                                <telerik:GridBoundColumn UniqueName="ValidEndDate" DataField="ValidEndDate" HtmlEncode="false"
                                    DataFormatString="{0:yyyy-MM-dd}" HeaderText="有效期至">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
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
                                <telerik:GridBoundColumn UniqueName="UnitName" DataField="UnitName" HeaderText="企业名称">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="UnitCode" DataField="UnitCode" HeaderText="机构代码">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="FirstCheckUnitName" DataField="FirstCheckUnitName"
                                    HeaderText="初审单位">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="Status" DataField="Status" HeaderText="审核状态">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn UniqueName="CheckResult" HeaderText="审核结论">
                                    <ItemTemplate>
                                        <%# Eval("Status").ToString()==Model.EnumManager.CertificateContinueStatus.Applyed?"未审核":
                                                Eval("Status").ToString()==Model.EnumManager.CertificateContinueStatus.Accepted?Eval("GetResult"):
                                                Eval("Status").ToString()==Model.EnumManager.CertificateContinueStatus.Checked?Eval("CheckResult"):
                                                Eval("ConfirmResult") %>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn UniqueName="CheckDate" HeaderText="审核时间">
                                    <ItemTemplate>
                                        <%# Eval("Status").ToString()==Model.EnumManager.CertificateContinueStatus.Applyed?"":
                                                Eval("Status").ToString()==Model.EnumManager.CertificateContinueStatus.Accepted?Convert.ToDateTime(Eval("GetDate")).ToString("yyyy-MM-dd"):
                                                Eval("Status").ToString()==Model.EnumManager.CertificateContinueStatus.Checked?Convert.ToDateTime(Eval("CheckDate")).ToString("yyyy-MM-dd"):
                                                Eval("ConfirmDate")==DBNull.Value?"" :Convert.ToDateTime(Eval("ConfirmDate")).ToString("yyyy-MM-dd") %>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                </telerik:GridTemplateColumn>
                            </Columns>
                            <HeaderStyle Font-Bold="True" />
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
                    <div style="width: 90%; margin: 5px auto; text-align: center; padding-top: 8px;">
                        <asp:Button ID="ButtonApplyPrint" runat="server" Text="打 印" CssClass="button" Visible="false"
                            OnClick="ButtonApplyPrint_Click" />
                        <asp:Button ID="ButtonExport" runat="server" Text="导出续期决定" CssClass="bt_large" Visible="false"
                            OnClick="ButtonExport_Click" />
                    </div>
                </div>
                <br />
            </div>
        </div>
    </div>
    <uc4:IframeView ID="IframeView" runat="server" />
</asp:Content>
