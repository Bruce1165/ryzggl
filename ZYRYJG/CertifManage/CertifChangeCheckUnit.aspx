<%@ Page Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="CertifChangeCheckUnit.aspx.cs" Inherits="ZYRYJG.CertifManage.CertifChangeCheckUnit" %>

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
    <script type="text/javascript">

    </script>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" EnableAJAX="true">
        <AjaxSettings>
             <telerik:AjaxSetting AjaxControlID="btnSearch">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
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
                当前位置 &gt;&gt; 证书管理 &gt;&gt; <strong>证书变更审查</strong>
            </div>
        </div>
        <div class="content">
            <table class="bar_cx" runat="server" id="DivSearch">
                <tr>
                    <td align="right" nowrap="nowrap" width="11%">变更类型：
                    </td>
                    <td align="left">
                        <telerik:RadComboBox ID="RadComboBoxChangeType" runat="server" Width="97%" Skin="Default">
                            <Items>
                                <telerik:RadComboBoxItem Value="" Text="全部" Selected="True" />
                                <telerik:RadComboBoxItem Value="京内变更" Text="京内变更" />
                                <telerik:RadComboBoxItem Value="离京变更" Text="离京变更" />
                                <telerik:RadComboBoxItem Value="注销" Text="注销" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                    <td align="right" nowrap="nowrap" width="11%">&nbsp;处理方式：
                    </td>
                    <td align="left" width="39%">
                        <telerik:RadComboBox ID="RadComboBoxDealWay" runat="server" Width="97%" Skin="Default">
                            <Items>
                                <telerik:RadComboBoxItem Value="" Selected="True" Text="全部" />
                                <telerik:RadComboBoxItem Value="审核通过" Text="审核通过" />
                                <telerik:RadComboBoxItem Value="退回修改" Text="退回修改" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>

                <tr>
                    <td align="right" nowrap="nowrap">受理状态：
                    </td>
                    <td align="left">
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td align="left" width="50%">
                                    <asp:RadioButtonList ID="RadioButtonListStatus" runat="server" RepeatDirection="Horizontal"
                                        AutoPostBack="false" Style="float: left;" Width="150px">
                                        <asp:ListItem Value="未审查" Selected="True">未审查</asp:ListItem>
                                        <asp:ListItem Value="已审查">已审查</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>

                            </tr>
                        </table>
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
                    <td align="right" nowrap="nowrap" width="11%">姓 名：
                    </td>
                    <td align="left" width="39%">
                        <telerik:RadTextBox ID="rdtxtWorkerName" runat="server" Width="97%" Skin="Default">
                        </telerik:RadTextBox>
                    </td>
                    <td align="right" nowrap="nowrap" width="11%">证件号码：
                    </td>
                    <td align="left" width="39%">
                        <telerik:RadTextBox ID="rdtxtZJHM" runat="server" Width="97%" Skin="Default">
                        </telerik:RadTextBox>
                    </td>
                </tr>

                <tr>
                    <td width="11%" align="right" nowrap="nowrap">证书编号：
                    </td>
                    <td width="39%" align="left">
                        <telerik:RadTextBox ID="rdtxtCertificateCode" runat="server" Width="97%" Skin="Default">
                        </telerik:RadTextBox>
                    </td>
                    <td align="right" width="11%" nowrap="nowrap">岗位工种：
                    </td>
                    <td align="left" width="39%">
                        <uc1:PostSelect ID="PostSelect1" runat="server" />
                    </td>
                </tr>

                <tr>
                    <td colspan="4" align="center">
                        <asp:Button ID="btnSearch" runat="server" Text="查 询" CssClass="button" OnClick="btnSearch_Click" />
                    </td>
                </tr>
            </table>

            <div class="table_cx">
                <img alt="" src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                申请列表
            </div>
            <div style="width: 98%; margin: 0 auto; overflow: auto;">
                <telerik:RadGrid ID="RadGrid1" AllowPaging="True"
                    SortingSettings-SortToolTip="单击进行排序" runat="server" AutoGenerateColumns="False" PagerStyle-AlwaysVisible="true"
                    AllowSorting="True" PageSize="10" GridLines="None" CellPadding="0" Width="99%"
                    Skin="Blue" EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false" 
                    OnPageIndexChanged="RadGridAccept_PageIndexChanged" OnDataBound="RadGridAccept_DataBound">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="CertificateChangeID,ChangeType,UnitCode,NewUnitCode" NoMasterRecordsText="没有可显示的记录">
                        <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                        <Columns>
                            <telerik:GridTemplateColumn UniqueName="SelectAllColumn" HeaderText="Highlight <br/> ship name" Display="false">
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
                           
                            <telerik:GridBoundColumn UniqueName="ChangeType" DataField="ChangeType" HeaderText="变更类型">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                          
                            <telerik:GridTemplateColumn UniqueName="CertificateChangeID" HeaderText="申请单">
                                <ItemTemplate>
                                    <span class="link_edit" onclick='javascript:SetIfrmSrc("Application.aspx?o=<%# Utility.Cryptography.Encrypt(Eval("CertificateChangeID").ToString()) %>");'>
                                        <nobr>申请单</nobr>
                                    </span>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                            </telerik:GridTemplateColumn>
                             <telerik:GridBoundColumn HeaderText="岗位工种" UniqueName="PostName" DataField="PostName">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                           
                            <telerik:GridTemplateColumn UniqueName="CertificateCode" HeaderText="证书编号" SortExpression="CertificateCode">
                                <ItemTemplate>
                                    <span class="link_edit" onclick='javascript:SetIfrmSrc("../PersonnelFile/CertificateInfo.aspx?o=<%# Utility.Cryptography.Encrypt(Eval("CertificateID").ToString()) %>");'>
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
                           
                            <telerik:GridBoundColumn UniqueName="NewWorkerName" DataField="NewWorkerName" HeaderText="姓名">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="NewWorkerCertificateCode" DataField="NewWorkerCertificateCode"
                                HeaderText="证件号码">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                             <%--<telerik:GridTemplateColumn UniqueName="CheckInfo" HeaderText="审核结果" SortExpression="OldUnitCheckTime">
                                <ItemTemplate>
                                    <%# Eval("ChangeType").ToString() == "京内变更" ? string.Format("原单位：{0}<br />审核时间：{1}<br />审核结果：{2}<br />新单位：{3}<br />审核时间：{4}<br />审核结果：{5}",Eval("UnitName"),(Eval("OldUnitCheckTime")==DBNull.Value?"":Convert.ToDateTime(Eval("OldUnitCheckTime")).ToString("yyyy.MM.dd")),Eval("OldUnitAdvise"),Eval("NewUnitName"),(Eval("NewUnitCheckTime")==DBNull.Value?"":Convert.ToDateTime(Eval("NewUnitCheckTime")).ToString("yyyy.MM.dd")),Eval("NewUnitAdvise")): string.Format("单位名称：{0}<br />审核时间：{1}<br />审核结果：{2}",Eval("UnitName"),(Eval("OldUnitCheckTime")==DBNull.Value?"":Convert.ToDateTime(Eval("OldUnitCheckTime")).ToString("yyyy.MM.dd")),Eval("OldUnitAdvise"))%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridTemplateColumn> --%>
                            <telerik:GridTemplateColumn UniqueName="CheckInfo" HeaderText="审核结果" SortExpression="OldUnitCheckTime">
                                <ItemTemplate>
                                    <%# Eval("ChangeType").ToString() == "京内变更" ? string.Format("新单位：{0}<br />审核时间：{1}<br />审核结果：{2}",Eval("NewUnitName"),(Eval("NewUnitCheckTime")==DBNull.Value?"":Convert.ToDateTime(Eval("NewUnitCheckTime")).ToString("yyyy.MM.dd")),Eval("NewUnitAdvise")): string.Format("单位名称：{0}<br />审核时间：{1}<br />审核结果：{2}",Eval("UnitName"),(Eval("OldUnitCheckTime")==DBNull.Value?"":Convert.ToDateTime(Eval("OldUnitCheckTime")).ToString("yyyy.MM.dd")),Eval("OldUnitAdvise"))%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridTemplateColumn> 
                            <telerik:GridTemplateColumn UniqueName="sb" HeaderText="社保">
                                <ItemTemplate>
                                    <%# (Eval("ChangeType").ToString() == "京内变更" || Eval("ChangeType").ToString() == "补办")? string.Format("<span class=\"link_edit\" onclick='javascript:SetIfrmSrc(\"../PersonnelFile/RYSBView.aspx?o1={0}&o2={1}&o3={2}\");'><nobr>{3}</nobr></span>", Eval("NewWorkerCertificateCode").ToString(), Eval("NewUnitCode").ToString(), Convert.ToDateTime(Eval("ApplyDate")).ToString(),(Eval("SheBaoCheck")==DBNull.Value)?"查看": Eval("SheBaoCheck").ToString()=="1"?"符合":"不符合" ):""%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" Font-Bold="true" />
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

           <%-- <div style=" line-height: 30px; ">
                <asp:Button ID="ButtonConfirm" runat="server" Text="审查通过" CssClass="bt_large" OnClick="ButtonConfirm_Click" />
                &nbsp;&nbsp;<asp:Button ID="ButtonDelete" runat="server" Text="删除无效申请" CssClass="bt_large"
                    OnClick="ButtonDelete_Click" />
            </div>--%>
            <uc4:IframeView ID="IframeView" runat="server" />
        </div>
    </div>
</asp:Content>
