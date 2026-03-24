<%@ Page Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="DelHistory.aspx.cs" Inherits="ZYRYJG.RenewCertifates.DelHistory" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
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
                <asp:Label ID="LabelCheck" runat="server" Text="Label"></asp:Label>&gt;&gt; <strong>查看删除历史</strong>
            </div>
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
                        <td width="8%" align="right" nowrap="nowrap">证书编号：
                        </td>
                        <td width="42%" align="left">
                            <telerik:RadTextBox ID="txtCertificateCode" runat="server" Width="33%" Skin="Default"
                                onkeydown="ButtonSearchClick(event); " Style="float: left;">
                            </telerik:RadTextBox>
                            <span style="float: left; padding: 0 4px; line-height: 22px;">申请批号：</span>                            
                            <telerik:RadTextBox ID="RadTextBoxApplyCode" runat="server" Width="33%" Skin="Default">
                                        </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap="nowrap">姓名：
                        </td>
                        <td align="left">
                            <telerik:RadTextBox ID="txtWorkerName" runat="server" Width="97%" Skin="Default"
                                >
                            </telerik:RadTextBox>
                        </td>
                        <td align="right" nowrap="nowrap">证件号码：
                        </td>
                        <td align="left">
                            <telerik:RadTextBox ID="txtWorkerCertificateCode" runat="server" Width="97%" Skin="Default"
                                >
                            </telerik:RadTextBox>
                        </td>
                    </tr>                 
                    <tr>
                        <td align="right" nowrap="nowrap" width="11%">续期状态：
                        </td>
                        <td align="left" width="39%">
                            <asp:RadioButtonList ID="rbl" runat="server" RepeatDirection="Horizontal" AutoPostBack="false" Width="300px">
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
                                AutoPostBack="false" Width="300px">
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
                    证书续期申请表删除列表
                </div>
                <div id="DivGrid" style="width: 98%; margin: 0 auto; overflow: auto;" runat="server">
                    <telerik:RadGrid ID="RadGridCheck" runat="server"
                        GridLines="None" AllowPaging="True" PageSize="10" AllowSorting="True" AutoGenerateColumns="False"
                        SortingSettings-SortToolTip="单击进行排序" Width="100%" Skin="Blue" EnableAjaxSkinRendering="false"  PagerStyle-AlwaysVisible="true"
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
                                 <telerik:GridBoundColumn UniqueName="APPLYMAN" DataField="APPLYMAN" HeaderText="申请人" AllowSorting="false">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                </telerik:GridBoundColumn>
                                
                                  <telerik:GridBoundColumn UniqueName="ApplyDate" DataField="ApplyDate" HeaderText="申请日期"
                                    HtmlEncode="false" DataFormatString="{0:yyyy.MM.dd}">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="ApplyCode" DataField="ApplyCode" HeaderText="申请批次号">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="PostName" DataField="PostName" HeaderText="岗位工种">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn UniqueName="CertificateChangeID" HeaderText="证书编号">
                                    <ItemTemplate>
                                        
                                        <span class="link_edit" onclick='javascript:SetIfrmSrc("../PersonnelFile/CertificateInfo.aspx?o=<%# Utility.Cryptography.Encrypt(Eval("CertificateID").ToString()) %>");'>
                                        <nobr><%# Eval("CertificateCode").ToString()%></nobr>
                                    </span>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" Font-Bold="true" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                                </telerik:GridTemplateColumn>
                              
                                <telerik:GridBoundColumn UniqueName="WorkerName" DataField="WorkerName" HeaderText="姓名">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="WorkerCertificateCode" DataField="WorkerCertificateCode"
                                    HeaderText="证件号码">
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
                                                Eval("ConfirmDate") == DBNull.Value?"":Convert.ToDateTime(Eval("ConfirmDate")).ToString("yyyy-MM-dd") %>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                </telerik:GridTemplateColumn>
                         
                                <telerik:GridBoundColumn UniqueName="DelTime" DataField="DelTime" HeaderText="删除日期"
                                    HtmlEncode="false" DataFormatString="{0:yyyy.MM.dd}">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>
                            </Columns>
                            <HeaderStyle Font-Bold="True" />
                            <PagerTemplate>
                                <uc2:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                            </PagerTemplate>
                        </MasterTableView>
                    </telerik:RadGrid>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetDeleteList"
                        TypeName="DataAccess.CertificateContinueDAL" SelectCountMethod="SelectDeleteCount"
                        EnablePaging="true" MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex"
                        SortParameterName="orderBy">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                                DefaultValue="" ConvertEmptyStringToNull="false" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <div style="width: 90%; margin: 5px auto; text-align: center; padding-top: 8px;">
                       
                        <asp:Button ID="ButtonExport" runat="server" Text="导 出" CssClass="bt_large" 
                            OnClick="ButtonExport_Click" />
                    </div>
                </div>
                <br />
            </div>
        </div>
    </div>
    <uc4:IframeView ID="IframeView" runat="server" />
</asp:Content>
