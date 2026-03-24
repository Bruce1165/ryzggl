<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LockList.aspx.cs" Inherits="ZYRYJG.County.LockList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc1" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc2" %>
<%@ Register Src="../CheckAll.ascx" TagName="CheckAll" TagPrefix="uc3" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" />
    <link href="../Skins/Blue/Grid.Blue.css?v=1.008" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
    <link href="../layer/skin/layer.css" rel="stylesheet" />
    <script src="../layer/layer.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Windows7">
        </telerik:RadWindowManager>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1"
            EnableAJAX="true">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="divmian">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="divmian" LoadingPanelID="RadAjaxLoadingPanel1" />

                    </UpdatedControls>
                </telerik:AjaxSetting>
           
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true" Skin="Default" />
        <div class="div_out">
            <div id="div_top" class="dqts">
                <div style="float: left; ">
                    当前位置 &gt;&gt;建造师注册管理 &gt;&gt;<strong>注册证书锁定与解锁</strong>
                </div>
            </div>
            <div id="divmian" runat="server" style="width: 99%; margin: 5px auto; padding-bottom: 30px;">
                <div class="content">
                    <table class="cx" width="100%" border="0" align="center" cellspacing="1">
                        <tr id="TrPerson" runat="server">
                            <td width="12%" align="right" nowrap="nowrap">
                                <telerik:RadComboBox ID="RadComboBoxIten" runat="server">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="姓名" Value="PSN_Name" />
                                        <telerik:RadComboBoxItem Text="证件号码" Value="PSN_CertificateNO" />
                                        <telerik:RadComboBoxItem Text="企业名称" Value="ENT_Name" />
                                        <telerik:RadComboBoxItem Text="证书编号" Value="PSN_RegisterCertificateNo" />
                                        <telerik:RadComboBoxItem Text="类型" Value="LX" />
                                        <%-- <telerik:RadComboBoxItem Text="中标企业" Value="ZBQY" />--%>
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td align="left" width="30%">
                                <telerik:RadTextBox ID="RadTextBoxValue" runat="server" Width="97%" Skin="Default">
                                </telerik:RadTextBox>

                            </td>
                            <td width="80px" align="right" nowrap="nowrap">锁定状态：
                            </td>
                            <td align="left" width="80px">
                                <telerik:RadComboBox ID="RadComboBoxSDZT" runat="server" Width="80px">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="全部" Value="" />
                                        <telerik:RadComboBoxItem Text="加锁" Value="加锁" Selected="true" />
                                        <telerik:RadComboBoxItem Text="解锁" Value="解锁" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td width="80px" align="right" nowrap="nowrap">多次重注：
                            </td>
                            <td align="left" width="70px">
                                <telerik:RadComboBox ID="RadComboBoxDCCZ" runat="server" Width="80px">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="否" Value="" Selected="true" />
                                        <telerik:RadComboBoxItem Text="是" Value="是" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td align="left" style="padding-left: 40px">
                                <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
                            </td>
                        </tr>

                    </table>
                    <div style="width: 50%; float: left; line-height: 20px; height: 20px; vertical-align: middle; padding: 0">
                        <div class="tl pl20">&nbsp;批量加锁说明：类型：<b style="color: red">二级建造师</b>、锁定状态：<b style="color: red">解锁</b>、多次重注：<b style="color: red">是</b></div>
                    </div>
                    <br />
                    <br />
                    <telerik:RadGrid ID="RadGridQY" runat="server"
                        GridLines="None" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" PageSize="15" Skin="Blue" EnableAjaxSkinRendering="False"
                        EnableEmbeddedSkins="False"
                        SortingSettings-SortToolTip="单击进行排序" Width="100%" PagerStyle-AlwaysVisible="true">
                        <ClientSettings EnableRowHoverStyle="true">
                        </ClientSettings>
                        <HeaderContextMenu EnableEmbeddedSkins="False">
                        </HeaderContextMenu>
                        <MasterTableView NoMasterRecordsText=" 没有可显示的记录" CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Left"
                            DataKeyNames="Fid">
                            <Columns>
                                <telerik:GridTemplateColumn UniqueName="SelectAllColumn">
                                    <HeaderTemplate>
                                        <uc3:CheckAll ID="CheckAll1" runat="server" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" CssClass="ck" runat="server" onclick='checkBoxClick(this.checked);' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="36" />
                                </telerik:GridTemplateColumn>


                                <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="PSN_Name" DataField="PSN_Name" HeaderText="姓名">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn UniqueName="PSN_CertificateNO" HeaderText="证件号码">
                                    <ItemTemplate>
                                        <%# Eval("PSN_CertificateNO") %>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn UniqueName="ENT_Name" DataField="ENT_Name" HeaderText="企业名称">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="PSN_RegisterCertificateNo" DataField="PSN_RegisterCertificateNo" HeaderText="证书编号">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <%--                         <telerik:GridBoundColumn UniqueName="PSN_RegisterNO" DataField="PSN_RegisterNO" HeaderText="注册号">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>--%>
                                <telerik:GridBoundColumn UniqueName="PSN_RegisteProfession" DataField="PSN_RegisteProfession" HeaderText="注册专业">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="PSN_CertificateValidity" DataField="PSN_CertificateValidity" HeaderText="有效期" DataFormatString="{0:yyyy-MM-dd}">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="LX" DataField="LX" HeaderText="类型">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>

                                <telerik:GridTemplateColumn UniqueName="Edit">
                                    <ItemTemplate>

                                        <%--<span class="link_edit" onclick=' javascript:SetIfrmSrc("EJZJSDetail.aspx?p=<%# Eval("Fid")%>"); '>详细</span>--%>
                                        <span class="link_edit" onclick='<%# Eval("LockStates").ToString()=="加锁"&&DateTime.Parse(Eval("LockEndTime").ToString())>DateTime.Now ?string.Format("javascript:SetIfrmSrc(\"UnLockDetailed.aspx?id={0}&&name={1}&&numno={2}&&lx={3}&&sfzh={4}\");",Eval("Fid"),Eval("PSN_Name"),Eval("PSN_RegisterCertificateNo"),Eval("LX"),Eval("PSN_CertificateNO")):string.Format("javascript:SetIfrmSrc(\"LockDetailed.aspx?id={0}&&name={1}&&numno={2}&&lx={3}&&sfzh={4}\");",Eval("Fid"),Eval("PSN_Name"),Eval("PSN_RegisterCertificateNo"),Eval("LX"),Eval("PSN_CertificateNO")) %>' /><%# Eval("LockStates").ToString()=="加锁"&&DateTime.Parse(Eval("LockEndTime").ToString())>DateTime.Now ?"解锁":"加锁"%></span>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                                </telerik:GridTemplateColumn>
                            </Columns>
                            <HeaderStyle Font-Bold="True" />
                            <PagerTemplate>
                                <uc1:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                            </PagerTemplate>
                        </MasterTableView>
                        <FilterMenu EnableEmbeddedSkins="False">
                        </FilterMenu>
                        <SortingSettings SortToolTip="单击进行排序"></SortingSettings>
                    </telerik:RadGrid>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="Model.COC_TOW_Person_BaseInfoMDL"
                        SelectMethod="GetLockList" TypeName="DataAccess.COC_TOW_Person_BaseInfoDAL"
                        SelectCountMethod="GetCountLockList" EnablePaging="true"
                        MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex" SortParameterName="orderBy">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                                DefaultValue="" ConvertEmptyStringToNull="false" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </div>
                <div id="divJS" runat="server" style="width: 99%; padding-top: 20px; text-align: center; clear: both; margin: auto">
                    <table id="TableEdit" runat="server" border="0" cellpadding="5" cellspacing="1" class="table" style="margin: 10px auto; width: 99%">
                        <tr class="GridLightBK">
                            <td colspan="2" class="barTitle">批量加锁操作（请先勾选要加锁的记录）</td>
                        </tr>
                        <tr class="GridLightBK">
                            <td width="20%" align="right">锁定截止时间：</td>
                            <td width="80%" align="left">
                                <telerik:RadDatePicker ID="RadDatePickerGetDateEnd" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                    Width="120px" />
                                (到期后锁定无效)
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td width="20%" align="right">加锁原因：</td>
                            <td width="80%" align="left">

                                <asp:TextBox ID="TextBoxApplyGetResult" runat="server" TextMode="MultiLine" Rows="3" Width="99%" Text="注册状态异常"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td colspan="2" align="center">
                                <asp:Button ID="BtnSave" runat="server" CssClass="bt_large" Text="确认加锁" OnClick="BtnSave_Click" />&nbsp;&nbsp;
                                           <%-- <input id="BtnReturn" type="button" class="bt_large" value="取消提交" onclick='javascript: hideIfam()' />--%>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <uc2:IframeView ID="IframeView" runat="server" />
    </form>
</body>
</html>
