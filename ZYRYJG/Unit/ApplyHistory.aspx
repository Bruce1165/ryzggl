<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplyHistory.aspx.cs" Inherits="ZYRYJG.Unit.ApplyHistory" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc1" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

     <style type="text/css">
        .tl, .flag_red, .flag_orange, .flag_blue, .flag_green ,.flag_black  {
            color: #444;          
            float: left;
            margin:0 2px;           
        }

        .pl20 {
            padding-left: 20px;
        }

        .flag_red {
            background: url(../Images/flag_red.png) no-repeat left center transparent;
        }

        .flag_orange {
            background: url(../Images/flag_purple.png) no-repeat left center transparent;
        }

        .flag_blue {
            background: url(../Images/flag_blue.png) no-repeat left center transparent;
        }

        .flag_green {
            background: url(../Images/flag_green.png) no-repeat left center transparent;
        }
        .flag_black {
            background: url(../Images/flag_black.png) no-repeat left center transparent;
        }
        .pointer {
            cursor: pointer;
            width: 16px;
            height: 16px;
        }

    </style>
    <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" /> 
    <link href="../Skins/Blue/Grid.Blue.css?v=1.008" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
      <script src="../Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
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
                <telerik:AjaxSetting AjaxControlID="ButtonSearch">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGridQY" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="RadGridQY">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGridQY" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true" Skin="Default" />
        <div class="div_out">
            <div class="dqts">
                <div style="float: left;">
                    当前位置 &gt;&gt;查询统计 &gt;&gt;<strong>申报业务查询</strong>
                </div>
            </div>
            <div class="content">
                <p class="jbxxbt">
                    申报业务查询
                </p>
                <div style="width: 100%; margin: 2px auto; text-align: center;">
                    <table width="99%" border="0" align="center" cellspacing="1" style="padding-bottom: 8px">
                        <tr id="TrPerson" runat="server">
                            <td width="90px" align="right" nowrap="nowrap">
                                <telerik:RadComboBox ID="RadComboBoxIten" runat="server" Width="100%">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="姓名" Value="PSN_Name" />
                                        <telerik:RadComboBoxItem Text="证件号码" Value="PSN_CertificateNO" />
                                         <telerik:RadComboBoxItem Text="企业名称" Value="ENT_Name" />
                                        <telerik:RadComboBoxItem Text="注册号" Value="PSN_RegisterNO" />                                       
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td align="left" width="150px">
                                <telerik:RadTextBox ID="RadTextBoxValue" runat="server" Width="97%" Skin="Default">
                                </telerik:RadTextBox>
                            </td>
                            <td align="left" width="410px">
                                <div class="RadPicker md">&nbsp;申报日期：</div>
                                <telerik:RadDatePicker ID="RadDatePickerGetDateStart" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                    Width="150px" />
                                <div class="RadPicker md">至</div>
                                <telerik:RadDatePicker ID="RadDatePickerGetDateEnd" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                    Width="150px" />
                            </td>                       
                            <td align="left" >
                                &nbsp;申报事项：
                                 <telerik:RadComboBox ID="RadComboBoxPSN_RegisteType" runat="server" Width="120">
                                     <Items>
                                         <telerik:RadComboBoxItem Text="全部" Value="全部" />
                                         <telerik:RadComboBoxItem Text="初始注册" Value="初始注册" />
                                         <telerik:RadComboBoxItem Text="重新注册" Value="重新注册" />
                                         <telerik:RadComboBoxItem Text="增项注册" Value="增项注册" />
                                         <telerik:RadComboBoxItem Text="延续注册" Value="延期注册" />
                                          <telerik:RadComboBoxItem Text="执业企业变更" Value="执业企业变更" />
                                          <telerik:RadComboBoxItem Text="企业信息变更" Value="企业信息变更" />
                                         <telerik:RadComboBoxItem Text="个人信息变更" Value="个人信息变更" />
                                         <telerik:RadComboBoxItem Text="遗失补办" Value="遗失补办" />
                                         <telerik:RadComboBoxItem Text="注销" Value="注销" />
                                     </Items>
                                 </telerik:RadComboBox>
                                &nbsp;申报进度：
                                 <telerik:RadComboBox ID="RadComboBoxApplyStatus" runat="server" Width="80">
                                     <Items>
                                         <telerik:RadComboBoxItem Text="全部" Value="全部" />
                                         <telerik:RadComboBoxItem Text="未申报" Value="未申报" />
                                         <telerik:RadComboBoxItem Text="待确认" Value="待确认" />
                                         <telerik:RadComboBoxItem Text="已申报" Value="已申报" />
                                         <telerik:RadComboBoxItem Text="已受理" Value="已受理" />
                                         <telerik:RadComboBoxItem Text="已驳回" Value="已驳回" />
                                         <telerik:RadComboBoxItem Text="已办结" Value="已办结" />
                                     </Items>
                                 </telerik:RadComboBox>
                                &nbsp;<asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
                            </td>
                            <td></td>
                        </tr>
                    </table>
                     <div style="width: 90%; float: left; line-height: 20px; height: 20px; vertical-align: middle; padding: 0">
                    <div class="tl pl20">图例说明：</div>
                    <div class="flag_red pl20">在施锁定，</div>
                    <div class="flag_orange pl20">重复注册，</div>
                    <div class="flag_blue pl20">社保缺失，</div>
                    <div class="flag_green pl20">新设立企业，</div>
                    <div class="flag_black pl20">异常注册</div>
                    <div class="tl">（申报次日点击图例查看比对详细）</div>
                </div>
                <br />
                <br />
                    <telerik:RadGrid ID="RadGridQY" runat="server"
                        GridLines="None" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" PageSize="15"
                        SortingSettings-SortToolTip="单击进行排序" Width="100%"  Skin="Blue" EnableAjaxSkinRendering="False" EnableEmbeddedSkins="False" PagerStyle-AlwaysVisible="true">
                        <ClientSettings EnableRowHoverStyle="true">
                        </ClientSettings>
                        <HeaderContextMenu EnableEmbeddedSkins="False">
                        </HeaderContextMenu>
                        <MasterTableView NoMasterRecordsText=" 没有可显示的记录" CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Left"
                            DataKeyNames="ApplyID,PSN_ServerID">
                            <Columns>
                                <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
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
                                <telerik:GridBoundColumn UniqueName="PSN_RegisteProfession" DataField="PSN_RegisteProfession" HeaderText="注册专业">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="PSN_RegisterNo" DataField="PSN_RegisterNo" HeaderText="注册编号">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="ENT_Name" DataField="ENT_Name" HeaderText="企业名称">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="ApplyTime" DataField="ApplyTime" HeaderText="申报日期" HtmlEncode="false" DataFormatString="{0:yyyy.MM.dd}">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>                               
                                  <telerik:GridTemplateColumn UniqueName="ApplyType" HeaderText="正在申报事项">
                                    <ItemTemplate>
                                        <%# Eval("ApplyTypeSub")==DBNull.Value?Eval("ApplyType"):Eval("ApplyTypeSub")%></span>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn UniqueName="progress" HeaderText="进度">
                                    <ItemTemplate>
                                        <%# BindApplyStatus(Eval("ApplyStatus").ToString(),Eval("ApplyType").ToString(),Eval("ConfirmResult").ToString(),Eval("CodeMan"))%></span>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridTemplateColumn>
                                 <telerik:GridTemplateColumn UniqueName="CheckImg" HeaderText="校验图例">
                                <ItemTemplate>                                  
                                      <%# (Eval("ApplyType").ToString() =="注销"||Eval("ApplyType").ToString() =="遗失补办"||(Eval("ApplyType").ToString() =="变更注册" && Eval("ApplyTypeSub").ToString() != "执业企业变更"))?"":Eval("SheBaoCheck") ==DBNull.Value?"<div title=\"点击查看详细\" class=\"flag_blue pointer\" onclick='javascript:alert(\"尚未比对!（当日提交数据后,次日进行比对）\")'>&nbsp;</div>": Eval("SheBaoCheck").ToString()=="1"?"":string.Format("<div title=\"点击查看详细\" class=\"flag_blue pointer\" onclick='javascript:SetIfrmSrc(\"../County/RYSBView.aspx?o1={0}&o2={1}&o3={2}\");'>&nbsp;</div>", Eval("PSN_CertificateNO"), Eval("ENT_OrganizationsCode"), Eval("ApplyTime"))%>
                                      <%# (Eval("ApplyType").ToString() =="遗失补办"||Eval("ApplyType").ToString() =="增项注册")?"":Eval("CheckZSSD") ==DBNull.Value?"<div title=\"点击查看详细\" class=\"flag_red pointer\" onclick='javascript:alert(\"尚未比对!（当日提交数据后,次日进行比对）\")'>&nbsp;</div>": Eval("CheckZSSD").ToString()=="1"?string.Format("<div title=\"点击查看详细\" class=\"flag_red pointer\" onclick='javascript:SetIfrmSrc(\"../County/ZSSDDetail.aspx?z={0}\");'>&nbsp;</div>", Eval("PSN_RegisterNo")):""%>
                                      <%# (Eval("ApplyType").ToString() =="注销"||Eval("ApplyType").ToString() =="遗失补办"||Eval("ApplyType").ToString() =="增项注册")?"": Eval("CheckXSL") ==DBNull.Value?"<div title=\"点击查看详细\" class=\"flag_green pointer\" onclick='javascript:alert(\"尚未比对!（当日提交数据后,次日进行比对）\");'>&nbsp;</div>":Eval("CheckXSL").ToString()=="1"&&ZYRYJG.UIHelp.IfZSSD_ContinueChangeUnit(Eval("ApplyType").ToString(),Eval("PSN_RegisterNo").ToString(),Eval("ENT_Name").ToString())==true?"<div title=\"点击查看详细\" class=\"flag_green pointer\" onclick='javascript:alert(\"无企业资质\");'>&nbsp;</div>":""%>
                                     <%# (Eval("ApplyType").ToString() == "注销" ? "" : Eval("CheckCFZC")== DBNull.Value ? "<div title=\"点击查看详细\" class=\"flag_orange pointer\" onclick='javascript:alert(\"尚未比对!（当日提交数据后,次日进行比对）\")'>&nbsp;</div>" : Eval("CheckCFZC").ToString() == "1" ? "<div title=\"点击查看详细\" class=\"flag_orange pointer\" onclick='javascript:alert(\"重复注册!\")'> &nbsp;</div>" : "")%>
                                   <%# (Eval("CheckYCZC").ToString()=="1"? string.Format("<div title=\"点击查看详细\" class=\"flag_black pointer\" onclick='javascript:SetIfrmSrc(\"../County/CertLockView.aspx?o={0}\");'>&nbsp;</div>", Eval("PSN_CertificateNO")):"") %>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" Wrap="false"  />
                            </telerik:GridTemplateColumn>                                
                                <telerik:GridTemplateColumn UniqueName="Edit" HeaderText="操作">
                                    <ItemTemplate>
                                        <%--2020-02-12 南静修改参数传参加密,源代码为:o=<%# Eval("PSN_ServerID") %>&a=<%# Eval("ApplyID") %>--%>
                                        <span class="link_edit" onclick='javascript:SetIfrmSrc("<%# GetApplyTypeUrl(Eval("ApplyType").ToString(),Eval("ApplyTypeSub").ToString()) %>?o=<%# Utility.Cryptography.Encrypt(Eval("PSN_ServerID").ToString()) %>&a=<%# Utility.Cryptography.Encrypt(Eval("ApplyID").ToString()) %>")'>详细</span>
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
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="Model.ApplyMDL"
                        SelectMethod="GetList" TypeName="DataAccess.ApplyDAL"
                        SelectCountMethod="SelectCount" EnablePaging="true"
                        MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex" SortParameterName="orderBy">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                                DefaultValue="" ConvertEmptyStringToNull="false" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </div>
            </div>
        </div>
        <uc2:IframeView ID="IframeView" runat="server" />
    </form>
</body>
</html>
