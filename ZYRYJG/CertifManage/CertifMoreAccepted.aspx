<%@ Page Title="" Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="CertifMoreAccepted.aspx.cs" Inherits="ZYRYJG.CertifManage.CertifMoreAccepted" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../PostSelect.ascx" TagName="PostSelect" TagPrefix="uc1" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    </telerik:RadCodeBlock>
    <telerik:RadWindowManager runat="server" RestrictionZoneID="offsetElement" ID="RadWindowManager1"
        EnableShadow="true" EnableEmbeddedScripts="true" Skin="Windows7" VisibleStatusbar="false">
    </telerik:RadWindowManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadGrid1">
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
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Windows7" />
    <div class="div_out">
        <div class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt; 证书管理 &gt;&gt;三类人员&gt;&gt; <strong>A本增发审核</strong>
            </div>
        </div>
        <div class="content">
            <table class="bar_cx" id="DivSearch" runat="server">
                <tr>
                    <td align="right" nowrap="nowrap">姓名：
                    </td>
                    <td align="left" width="39%">
                        <telerik:RadTextBox ID="rdtxtWorkerName" runat="server" Width="97%" Skin="Default"
                            >
                        </telerik:RadTextBox>
                    </td>

                    <td align="right" nowrap="nowrap" width="11%">证件号码：
                    </td>
                    <td align="left" width="39%">
                        <telerik:RadTextBox ID="rdtxtZJHM" runat="server" Width="97%" Skin="Default" >
                        </telerik:RadTextBox>
                    </td>

                </tr>

                <tr>
                    <td align="right" nowrap="nowrap">受理状态：
                    </td>
                    <td align="left">
                        <asp:RadioButtonList ID="RadioButtonListApplyStatus" runat="server" RepeatDirection="Horizontal"
                            AutoPostBack="false" Width="98%">
                             <asp:ListItem Value="" >全部</asp:ListItem>
                            <asp:ListItem Value="已申请" Selected="True">未审核</asp:ListItem>
                            <asp:ListItem Value="已审核">审核通过</asp:ListItem>
                            <asp:ListItem Value="退回修改">退回修改</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td align="right" nowrap="nowrap">受理时间：
                    </td>
                    <td align="left">
                        <telerik:RadDatePicker ID="RadDatePicker_GetDateStart" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                            Width="46%" />
                        <div class="RadPicker">至</div>
                        <telerik:RadDatePicker ID="RadDatePicker_GetDateEnd" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                            Width="46%" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center">
                        <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="bt_large" OnClick="ButtonSearch_Click" /> &nbsp;&nbsp;
                         <asp:Button ID="ButtonExportToExcel" CssClass="bt_maxlarge" Text="导出查询结果列表" OnClick="ButtonExportToExcel_Click"
                            runat="server"></asp:Button>
                    </td>
                </tr>
            </table>
            <div class="table_cx">
                <img alt="" src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                <asp:Label ID="LabelTitle" runat="server" Text="申请列表"></asp:Label>
            </div>
            <div style="width: 98%; margin: 0 auto;">
                <telerik:RadGrid ID="RadGrid1" AllowPaging="True" SortingSettings-SortToolTip="单击进行排序"  PagerStyle-AlwaysVisible="true"
                    runat="server" AutoGenerateColumns="False" AllowSorting="True" GridLines="None"
                    CellPadding="0" Width="100%" Skin="Blue" EnableAjaxSkinRendering="False" EnableEmbeddedSkins="False">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ApplyID" NoMasterRecordsText="没有可显示的记录">
                        <Columns>
                            <telerik:GridBoundColumn HeaderText="序号" UniqueName="RowNum" DataField="RowNum" AllowSorting="false">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="企业提交日期" UniqueName="NewUnitCheckTime" DataField="NewUnitCheckTime"
                                DataFormatString="{0:yyyy-MM-dd}">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="受理日期" UniqueName="CheckDate" DataField="CheckDate"
                                DataFormatString="{0:yyyy-MM-dd}">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            
                            <telerik:GridBoundColumn UniqueName="WorkerName" DataField="WorkerName" HeaderText="姓名">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="WorkerCertificateCode" DataField="WorkerCertificateCode"
                                HeaderText="身份证号">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="UnitName" DataField="UnitName" HeaderText="现有A本单位名称">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn UniqueName="UnitNameMore" DataField="UnitNameMore" HeaderText="申请增发A本单位名称">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>                           
                            <telerik:GridTemplateColumn UniqueName="ApplyStatus" HeaderText="状态">
                                <ItemTemplate>
                                    <%# formatStauts(Eval("ApplyStatus").ToString())%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false"  />
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn UniqueName="CertificateCodeMore" DataField="CertificateCodeMore"
                                HeaderText="增发A本证书编号">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                          
                             <telerik:GridTemplateColumn UniqueName="Detail" HeaderText="详细">
                                <ItemTemplate>
                                    <%# string.Format("<span class=\"link_edit\" onclick='javascript:SetIfrmSrc(\"CertifMoreCheck.aspx?o={0}\")'><nobr style='color:blue;text-decoration: underline;'>详细</nobr></span>", Utility.Cryptography.Encrypt(Eval("ApplyID").ToString()))%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                            </telerik:GridTemplateColumn>
                            
                        </Columns>
                        <HeaderStyle Font-Bold="True" />
                        <PagerTemplate>
                            <uc2:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                        </PagerTemplate>
                    </MasterTableView>
                    <SortingSettings SortToolTip="单击进行排序"></SortingSettings>
                    <StatusBarSettings LoadingText="正在读取数据" ReadyText="完成" />
                </telerik:RadGrid>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="DataAccess.CertificateMoreDAL"
                    DataObjectTypeName="Model.CertificateMoreMDL" SelectMethod="GetList" EnablePaging="true"
                    SelectCountMethod="SelectCount" MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex"
                    SortParameterName="orderBy">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                            ConvertEmptyStringToNull="false" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </div>

            <br />
        </div>
    </div>
     <uc4:IframeView ID="IframeView" runat="server" />
</asp:Content>
