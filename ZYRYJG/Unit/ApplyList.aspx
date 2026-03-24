<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplyList.aspx.cs" Inherits="ZYRYJG.Unit.ApplyList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc1" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc2" %>
<%@ Register Src="~/myhelp.ascx" TagPrefix="uc3" TagName="myhelp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" />
    <link href="../Skins/Blue/Grid.Blue.css?v=1.008" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
    <script src="../Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
        <link href="../layer/skin/layer.css" rel="stylesheet" />
    <script src="../layer/layer.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server">
        </telerik:RadStyleSheetManager>
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Windows7">
        </telerik:RadWindowManager>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1"
            EnableAJAX="true">
            <AjaxSettings>
                <%-- <telerik:AjaxSetting AjaxControlID="ButtonSearch">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGridQY" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="RadGridQY">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGridQY" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>--%>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true" Skin="Default" />
        <div class="div_out">
            <div class="dqts">
                <div style="float: left;">
                    当前位置 &gt;&gt;二级建造师注册 &gt;&gt;<strong><asp:Label ID="LabelPath" runat="server" Text=""></asp:Label></strong>
                </div>
                <uc3:myhelp runat="server" ID="myhelp" PageID="常见问题及处理方法最终版.htm" />
                <%--<div style="float: right; padding-right: 20px">
                    <img src="/Images/question3.png" alt="" height="14px" width="16px" />
                    <a href="/Template/常见问题及处理方法最终版.docx" style="color: #F00; font-weight: bold; font-size: 15px">常见问题</a>
                </div>--%>
            </div>
            <div class="content">
                <p class="jbxxbt">
                    <asp:Label ID="LabelTitle" runat="server" Text=""></asp:Label>
                </p>
                <table class="cx" width="98%" border="0" align="center" cellspacing="5">
                    <tr id="TrPerson" runat="server">
                        <td width="150px" align="right" nowrap="nowrap">
                            <telerik:RadComboBox ID="RadComboBoxIten" runat="server" Width="100%">
                                <Items>
                                    <telerik:RadComboBoxItem Text="姓名" Value="PSN_Name" />
                                    <telerik:RadComboBoxItem Text="证件号码" Value="PSN_CertificateNO" />
                                    <telerik:RadComboBoxItem Text="注册号" Value="PSN_RegisterNO" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <td align="left" width="400px">
                            <telerik:RadTextBox ID="RadTextBoxValue" runat="server" Width="97%" Skin="Default">
                            </telerik:RadTextBox>
                        </td>
                        <td>

                            <div class="RadPicker md">申报日期：</div>
                            <telerik:RadDatePicker ID="RadDatePickerGetDateStart" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                Width="100px" />
                            <div class="RadPicker md">至</div>
                            <telerik:RadDatePicker ID="RadDatePickerGetDateEnd" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                Width="100px" />
                             </td>
                       
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <asp:RadioButtonList ID="RadioButtonListApplyStatus" runat="server" RepeatDirection="Horizontal" TextAlign="right">
                                <asp:ListItem Text="全部" Value="全部" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="未填写" Value="未填写"></asp:ListItem>
                                <asp:ListItem Text="待确认" Value="待确认"></asp:ListItem>
                                <asp:ListItem Text="未申报" Value="未申报"></asp:ListItem>
                                <asp:ListItem Text="已申报" Value="已申报"></asp:ListItem>
                                <asp:ListItem Text="已受理" Value="已受理"></asp:ListItem>
                                <asp:ListItem Text="已驳回" Value="已驳回"></asp:ListItem>

                            </asp:RadioButtonList>
                        </td>
                        <td align="left">
                            <asp:Button ID="ButtonSearch" runat="server" Text="查询在办业务" CssClass="bt_large" OnClick="ButtonSearch_Click" />
                            &nbsp;<input id="ButtonRenew" runat="server" visible="false" type="button" value="发起重新注册" class="bt_large" onclick='javascript: SetIfrmSrc("ApplyRenew.aspx"); ' />
                        </td>
                    </tr>
                </table>
                <div id="div_continueTip" runat="server" visible="false" style="width: 99%; margin: 8px auto; color: red;"><%= string.Format(">续期申请开放时间为有效期届满前{0}天至{1}天，请自行注意证书有效期，逾期不许提交申请。", Model.EnumManager.ContinueTime.开始时间, Model.EnumManager.ContinueTime.结束时间) %></div>
                <div id="div30DayTip" runat="server" visible="false" style="width: 99%; margin: 8px auto; color: red;">>过期前30天内只能办理注销，不受理其他注册业务，请注意证书有效期截止日期，提前进行申请。</div>
                <div id="div_applyDeleteTime" runat="server" visible="false" style="width: 99%; margin: 8px auto; color: red;"><%= string.Format(">请在提交网上申请后，联系聘用企业进行审核上报。{0}天内企业未在网上进行审核上报的，系统将自动删除此次业务申请。", Model.EnumManager.ApplyDeleteTime.时间间隔) %></div>
                <div id="div_use" runat="server"  style="width: 99%; margin: 8px auto; color: red;">>申报事项公告通过后，申请人可在 电子证书 下载栏目中生成该电子证书使用有效期范围，公告后24小时内未自行填写，系统将会自动生成最大时间范围的使用有效期。<br />&nbsp;&nbsp;新电子证书生成后，申请人也可以根据自身需求再次调整证书使用有效期时间范围。</div>
                <div style="width: 99%; margin: 8px auto;">
                    <telerik:RadGrid ID="RadGridQY" runat="server"
                        GridLines="None" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" PageSize="15"
                        SortingSettings-SortToolTip="单击进行排序" Width="100%" Skin="Blue" EnableAjaxSkinRendering="False" EnableEmbeddedSkins="False" PagerStyle-AlwaysVisible="true">
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
                                <telerik:GridBoundColumn UniqueName="PSN_CertificateNO" DataField="PSN_CertificateNO" HeaderText="证件号码">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn UniqueName="PSN_RegisteProfession" HeaderText="注册专业">
                                    <ItemTemplate>
                                        <%# Eval("PSN_RegisteProfession")== DBNull.Value? Eval("PSN_RegisteProfession"):Eval("PSN_RegisteProfession") %>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridTemplateColumn>

                                <telerik:GridBoundColumn UniqueName="PSN_RegisterNO" DataField="PSN_RegisterNO" HeaderText="注册号">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn UniqueName="PSN_CertificateValidity" HeaderText="有效期">
                                    <ItemTemplate>
                                        <%# Eval("PSN_CertificateValidity")== DBNull.Value? "":
                                        ((Convert.ToDateTime(Eval("PSN_CertificateValidity"))-DateTime.Now).Days +1) <=0?
                                        string.Format("{0}（过期）",Convert.ToDateTime(Eval("PSN_CertificateValidity")).ToString("yyyy.MM.dd"))
                                        :string.Format("{0}（{1}天）",Convert.ToDateTime(Eval("PSN_CertificateValidity")).ToString("yyyy.MM.dd"),(Convert.ToDateTime(Eval("PSN_CertificateValidity"))-DateTime.Now).Days +1
                                        ) %>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridTemplateColumn>
                                <%--   <telerik:GridBoundColumn UniqueName="ProfessionList" DataField="ProfessionList" HeaderText="多专业有效期">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>--%>
                                <telerik:GridTemplateColumn UniqueName="ENT_Name" HeaderText="企业名称">
                                    <ItemTemplate>
                                        <%# Eval("ENT_Name")== DBNull.Value? Eval("ENT_Name"):Eval("ENT_Name") %>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn UniqueName="ApplyTime" DataField="ApplyTime" HeaderText="申报日期" HtmlEncode="false" DataFormatString="{0:yyyy.MM.dd}">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn UniqueName="progress" HeaderText="进度">
                                    <ItemTemplate>
                                        <%# Eval("ApplyStatus")==DBNull.Value?"未填写": BindApplyStatus(Eval("ApplyStatus").ToString())%></span>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn UniqueName="Edit" HeaderText="操作">
                                    <ItemTemplate>
                                       <%--2020-02-12 南静修改参数传参加密,源代码为:o=<%# Eval("PSN_ServerID").ToString() %>&a=<%# Eval("ApplyID") %>--%>
                                        <span class="link_edit" onclick='javascript:SetIfrmSrc("<%# GetApplyTypeUrl( Eval("ApplyID")==DBNull.Value?ApplyType:Eval("ApplyType").ToString()) %>?o=<%# Utility.Cryptography.Encrypt(Eval("PSN_ServerID").ToString()) %>&a=<%# Eval("ApplyID")==DBNull.Value?"":Utility.Cryptography.Encrypt(Eval("ApplyID").ToString()) %>")'>
                                            <%# formatApplyShow(ApplyType,  Eval("ApplyID")==DBNull.Value?"": Eval("ApplyID").ToString(),  Eval("ApplyStatus").ToString(),  Eval("ProfessionList").ToString())%>
                                        </span>
                                        <%-- <span class="link_edit" onclick='javascript:SetIfrmSrc("<%# GetApplyTypeUrl( Eval("ApplyID")==DBNull.Value?ApplyType:Eval("ApplyType").ToString()) %>?o=<%# Eval("PSN_ServerID") %>&a=<%# Eval("ApplyID") %>")'><%# (ApplyType=="延期注册" && Eval("ApplyID")==DBNull.Value && (Convert.ToDateTime(Eval("PSN_CertificateValidity")) < DateTime.Now.AddDays( Model.EnumManager.ContinueTime.结束时间)|| Convert.ToDateTime(Eval("PSN_CertificateValidity")) > DateTime.Now.AddDays( Model.EnumManager.ContinueTime.开始时间)))?"":( Eval("ApplyID")==DBNull.Value||Eval("ApplyStatus").ToString()=="未申报"?"申报":"详细")%></span>--%>
                                        <span class="link_edit" onclick='javascript:SetIfrmSrc("<%# GetApplyTypeUrl( Eval("ApplyID")==DBNull.Value?ApplyType:Eval("ApplyType").ToString()) %>?o=<%# Utility.Cryptography.Encrypt(Eval("PSN_ServerID").ToString()) %>&a=<%# Eval("ApplyID")==DBNull.Value?"":Utility.Cryptography.Encrypt(Eval("ApplyID").ToString()) %>")'><%# Eval("ApplyID")==DBNull.Value?"":Eval("ApplyStatus").ToString()=="未申报"||Eval("ApplyStatus").ToString() == "已驳回"?"提交":"撤销"%></span>
                                        <%--<%# Eval("ApplyStatus").ToString() == "未申报"||Eval("ApplyStatus").ToString() == "已驳回"?string.Format("<span class=\"link_edit\">提交</span>",Eval("ApplyID")):Eval("ApplyStatus").ToString() == "已申报"?string.Format("<span class=\"link_edit\" onclick=' javascript:SetIfrmSrc(\"ApplyFirstAdd.aspx?a={0}\"); '>撤销</span>",Eval("ApplyID")):"" %> --%>
                                        <span class="link_edit" onclick='javascript:SetIfrmSrc("<%# GetApplyTypeUrl( Eval("ApplyID")==DBNull.Value?ApplyType:Eval("ApplyType").ToString()) %>?o=<%# Utility.Cryptography.Encrypt(Eval("PSN_ServerID").ToString()) %>&a=<%# Eval("ApplyID")==DBNull.Value?"":Utility.Cryptography.Encrypt(Eval("ApplyID").ToString()) %>")'><%# Eval("ApplyID")==DBNull.Value?"":"打印"%></span>
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
                </div>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="Model.ApplyMDL"
                    SelectMethod="GetListApplyView" TypeName="DataAccess.ApplyDAL"
                    SelectCountMethod="SelectCountApplyView" EnablePaging="true"
                    MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex" SortParameterName="orderBy">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                            DefaultValue="" ConvertEmptyStringToNull="false" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </div>
        </div>
        <uc2:IframeView ID="IframeView" runat="server" />
        <div id="winpop">
        </div>
    </form>
</body>
</html>
