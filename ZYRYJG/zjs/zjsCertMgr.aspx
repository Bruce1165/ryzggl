<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="zjsCertMgr.aspx.cs" Inherits="ZYRYJG.zjs.zjsCertMgr" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc1" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc2" %>
<%@ Register Src="../CheckAll.ascx" TagName="CheckAll" TagPrefix="uc3" %>
<!DOCTYPE html html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
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
                <telerik:AjaxSetting AjaxControlID="divMain">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="divMain" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true" Skin="Default" />
        <div class="div_out">
            <div id="div_top" class="dqts">
                <div style="float: left;">
                    当前位置 &gt;&gt;二级造价工程师注册 &gt;&gt;<strong>注册证书管理</strong>
                </div>
            </div>
            <div class="content" id="divMain" runat="server">
                <table id="tableQuery" runat="server" class="cx" width="100%" border="0" align="center" cellspacing="3">
                    <tr>
                        <td style="text-align: right; width: 120px">证书状态：</td>
                        <td width="260px" align="left" nowrap="nowrap">

                            <asp:RadioButtonList ID="RadioButtonListPSN_RegisteType" runat="server" TextAlign="right" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="RadioButtonListPSN_RegisteType_SelectedIndexChanged">
                                <asp:ListItem Text="全部" Value="" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="未注销" Value="未注销"></asp:ListItem>
                                <asp:ListItem Text="已注销" Value="已注销"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td class="auto-style2" width="300px">

                            <div class="RadPicker md" style="width: 140px; text-align: right">
                                证书有效期：小于
                            </div>
                            <telerik:RadDatePicker ID="RadDatePickerGetDateStart" MinDate="01/01/1900" runat="server" Height="21px" align="left" Style="margin: auto auto" Calendar-DayCellToolTipFormat="yyyy年MM月dd日">
                                <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"></Calendar>
                                <DateInput DisplayDateFormat="yyyy/M/d" DateFormat="yyyy/M/d" Height="21px"></DateInput>
                                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                            </telerik:RadDatePicker>
                        </td>
                        <td align="right" width="170px">
                            <asp:CheckBox ID="CheckBoxPSN_Ag" runat="server" Text="超龄（大于70岁）" />
                        </td>
                        <td align="left" style="width: 150px; text-align: right" class="auto-style11">
                            <telerik:RadComboBox ID="RadComboBoxIten" runat="server" Width="119px" MaxHeight="200px">
                                <Items>
                                    <telerik:RadComboBoxItem Text="姓名" Value="PSN_Name" />
                                    <telerik:RadComboBoxItem Text="证件号码" Value="PSN_CertificateNO" />
                                    <telerik:RadComboBoxItem Text="单位名称" Value="ENT_Name" />
                                    <telerik:RadComboBoxItem Text="机构代码" Value="ENT_OrganizationsCode" />
                                    <telerik:RadComboBoxItem Text="注册号" Value="PSN_RegisterNO" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <td width="180px" align="right" nowrap="nowrap" aria-disabled="False" class="auto-style12">
                            <telerik:RadTextBox ID="RadTextBoxValue" runat="server" Width="97%" Skin="Default">
                            </telerik:RadTextBox>
                        </td>

                        <td align="left" style="padding-left: 40px" class="auto-style11">
                            <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" CausesValidation="false" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">预警类型：
                        </td>
                        <td align="left" colspan="6">
                        <%--    <asp:RadioButton ID="RadioButtonYuJing" runat="server" Text="全部" GroupName="q" Checked="true" />
                            &nbsp;<asp:RadioButton ID="CheckBoxNormalData" runat="server" Text="无预警数据" GroupName="q" />
                            &nbsp;<asp:RadioButton ID="CheckBoxYCZC" runat="server" Text="异常注册" GroupName="q" />
                            &nbsp;<asp:RadioButton ID="RadioButtonGuaZheng" runat="server" Text="未整改完成涉嫌挂证" GroupName="q" />--%>

                            <asp:RadioButtonList ID="RadioButtonListYuJingType" runat="server" TextAlign="right" RepeatDirection="Horizontal" Style="display: inline"
                                OnSelectedIndexChanged="RadioButtonListYuJingType_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Text="全部" Value="" Selected="True"></asp:ListItem>
                                 <asp:ListItem Text="无预警数据" Value="无预警数据" ></asp:ListItem>
                                <asp:ListItem Text="异常注册" Value="异常注册"></asp:ListItem>
                                <asp:ListItem Text="未整改完成涉嫌挂证" Value="未整改完成涉嫌挂证"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">批量操作类型：
                        </td>
                        <td align="left" colspan="6">
                            <asp:RadioButtonList ID="RadioButtonListAction" runat="server" TextAlign="right" RepeatDirection="Horizontal" Style="display: inline"
                                OnSelectedIndexChanged="RadioButtonListAction_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Text="手动锁定" Value="手动锁定" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="手动解锁" Value="手动解锁"></asp:ListItem>
                                <asp:ListItem Text="手动注销" Value="手动注销"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
                <div style="width: 99%; padding-top: 10px; text-align: center; clear: both; margin: auto">
                    <telerik:RadGrid ID="RadGridQY" runat="server" Skin="Blue" EnableAjaxSkinRendering="False"
                        EnableEmbeddedSkins="False" OnPageIndexChanged="RadGridQY_PageIndexChanged" OnDataBound="RadGridQY_DataBound"
                        GridLines="None" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" PageSize="10"
                        SortingSettings-SortToolTip="单击进行排序" Width="99%" PagerStyle-AlwaysVisible="true" >
                        <ClientSettings EnableRowHoverStyle="true">
                        </ClientSettings>
                        <HeaderContextMenu EnableEmbeddedSkins="False">
                        </HeaderContextMenu>
                        <MasterTableView NoMasterRecordsText=" 没有可显示的记录" CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Left"
                            DataKeyNames="PSN_ServerID" >
                            <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
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
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="PSN_Name" DataField="PSN_Name" HeaderText="姓名">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn UniqueName="PSN_CertificateNO" HeaderText="证件号码">
                                    <ItemTemplate>
                                        <%# Eval("PSN_CertificateNO") %>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn UniqueName="ENT_Name" DataField="ENT_Name" HeaderText="单位名称">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn UniqueName="PSN_RegisterNO" HeaderText="注册号">
                                    <ItemTemplate>
                                        <span class="link_edit" onclick='javascript:SetIfrmSrc("zjsCertInfo.aspx?o=<%# Utility.Cryptography.Encrypt(Eval("PSN_ServerID").ToString()) %>"); '><%#Eval("PSN_RegisterNO")%></span>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn UniqueName="PSN_RegisteProfession" DataField="PSN_RegisteProfession" HeaderText="注册专业">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="PSN_CertificateValidity" DataField="PSN_CertificateValidity" HeaderText="证书有效期" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn HeaderText="注销状态" UniqueName="PSN_RegisteType">
                                    <ItemTemplate>
                                        <%# Eval("PSN_RegisteType").ToString() =="07"?"已注销":"未注销"%>
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="false" />
                                </telerik:GridTemplateColumn>
                                 <telerik:GridBoundColumn UniqueName="LockRemark" DataField="LockRemark" HeaderText="异常说明" >
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                
                                <telerik:GridTemplateColumn UniqueName="Correct">
                                    <ItemTemplate>
                                        <span class="link_edit" onclick=' javascript:SetIfrmSrc("zjsCertModify.aspx?o=<%#  Utility.Cryptography.Encrypt(Eval("PSN_ServerID").ToString()) %>"); '>修正</span>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                                </telerik:GridTemplateColumn>

                            </Columns>
                            <PagerStyle AlwaysVisible="True"></PagerStyle>
                            <HeaderStyle Font-Bold="True" />
                            <CommandItemStyle HorizontalAlign="Left"></CommandItemStyle>
                            <PagerTemplate>
                                <uc1:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                            </PagerTemplate>
                        </MasterTableView>
                        <PagerStyle AlwaysVisible="True"></PagerStyle>
                        <FilterMenu EnableEmbeddedSkins="False">
                        </FilterMenu>
                        <SortingSettings SortToolTip="单击进行排序"></SortingSettings>
                    </telerik:RadGrid>
                </div>
                <div style="width: 99%; text-align: center; margin: 8px 0; text-align: right; padding-right: 40px">
                    <span id="spanOutput" runat="server" class="excel" style="padding-right: 20px; font-weight: bold"></span>
                    <asp:Button ID="ButtonOutput" runat="server" Text="导出查询结果" CssClass="bt_large" OnClick="ButtonOutput_Click" CausesValidation="false" />
                </div>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="Model.zjs_CertificateMDL"
                    SelectMethod="GetListView" TypeName="DataAccess.zjs_CertificateDAL"
                    SelectCountMethod="SelectCountView" EnablePaging="true"
                    MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex" SortParameterName="orderBy">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                            DefaultValue="" ConvertEmptyStringToNull="false" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <div id="divCancel" runat="server" style="width: 99%; text-align: center; clear: both; margin: auto">
                    <table id="TableEdit" runat="server" border="0" cellpadding="5" cellspacing="1" class="table" style="margin: 10px auto; width: 99%">
                        <tr class="GridLightBK">
                            <td colspan="2" class="barTitle">批量注销操作（请先勾选要注销的记录）</td>
                        </tr>
                        <tr class="GridLightBK">
                            <td width="10%" align="right">注销原因：</td>
                            <td width="90%" align="left" class="auto-style2">
                                <asp:RadioButtonList ID="RadioButtonListApplyStatus" runat="server" TextAlign="right" RepeatColumns="4" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="注册有效期满且未延续注册" Value="注册有效期满且未延续注册" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="企业资质已注销" Value="企业资质已注销"></asp:ListItem>
                                    <asp:ListItem Text="与原单位分歧" Value="与原单位分歧"></asp:ListItem>
                                    <asp:ListItem Text=" 聘用企业破产" Value=" 聘用企业破产"></asp:ListItem>
                                    <asp:ListItem Text="死亡或不具有完全民事行为能力" Value="死亡或不具有完全民事行为能力"></asp:ListItem>
                                    <asp:ListItem Text=" 聘用企业被吊销营业执照" Value=" 聘用企业被吊销营业执照"></asp:ListItem>
                                    <asp:ListItem Text="聘用企业被吊销相应资质证书" Value="聘用企业被吊销相应资质证书"></asp:ListItem>
                                    <asp:ListItem Text=" 聘用企业被吊销或者撤回资质证书" Value=" 聘用企业被吊销或者撤回资质证书"></asp:ListItem>
                                    <asp:ListItem Text="其他导致注册失效的情形" Value="其他导致注册失效的情形"></asp:ListItem>
                                    <asp:ListItem Text=" 已与聘用企业解除聘用合同关系" Value=" 已与聘用企业解除聘用合同关系"></asp:ListItem>
                                    <asp:ListItem Text="依法被吊销注册证书" Value="依法被吊销注册证书"></asp:ListItem>
                                    <asp:ListItem Text=" 年龄超过70周岁" Value="年龄超过70周岁"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td colspan="2" align="center" class="auto-style1">
                                <asp:Button ID="BtnCancel" runat="server" CssClass="bt_large" Text="批量注销" OnClick="BtnCancel_Click" OnClientClick="javascript:if(!confirm('您确定要批量证书注销么?'))return false;" />&nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="divLock" runat="server" style="width: 99%; padding-top: 10px; text-align: center; clear: both; margin: auto">
                    <table runat="server" id="TableDetail" cellpadding="5" cellspacing="1" border="0"
                        width="100%" class="table" align="center">
                        <tr class="GridLightBK">
                            <td colspan="2" class="barTitle">批量锁定/解锁操作（请先勾选要操作的记录）</td>
                        </tr>
                        <tr class="GridLightBK">
                            <td nowrap="nowrap" align="right">
                                <strong>
                                    <asp:Label ID="LabelLockDateTip" runat="server" Text="加锁日期"></asp:Label></strong>
                            </td>
                            <td align="left">
                                <asp:Label ID="LabelLockDate" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr class="GridLightBK" id="trLockEndTime">
                            <td align="right" nowrap="nowrap">
                                <strong>锁定截止日期</strong>
                            </td>
                            <td align="left">
                                <telerik:RadDatePicker ID="RadDatePickerLockEndTime" MinDate="01/01/2012" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                    Width="200px" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorLockEndTime" runat="server"
                                    ErrorMessage="必填" ControlToValidate="RadDatePickerLockEndTime" Display="Dynamic"></asp:RequiredFieldValidator>
                                <span style="color: #999999">（到期后锁定无效）</span>
                                <%--  锁定类型： <telerik:RadComboBox ID="RadComboBoxLockType" runat="server" Width="300px">
                                <Items>
                                    <telerik:RadComboBoxItem Text="其他" Value="其他" Selected="true"  />
                                </Items>
                            </telerik:RadComboBox>--%>
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td align="right" nowrap="nowrap" width="11%" valign="top">
                                <strong>
                                    <asp:Label ID="LabelLockRemark" runat="server" Text="锁定原因说明"></asp:Label></strong>
                            </td>
                            <td align="left">
                                <telerik:RadTextBox ID="RadTextBoxRemark" runat="server" Width="95%" Skin="Default"
                                    MaxLength="3000" TextMode="MultiLine" Rows="3">
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="必填"
                                    ControlToValidate="RadTextBoxRemark" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td colspan="2" style="text-align: center">
                                <asp:Button ID="ButtonLock" runat="server" Text="批量锁定" CssClass="button" OnClick="ButtonLock_Click" />

                                <asp:Button ID="ButtonUnLock" runat="server" Text="批量解锁" CssClass="button" OnClick="ButtonUnLock_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <uc2:IframeView ID="IframeView" runat="server" />
    </form>
    <script type="text/javascript">
        function checkBoxAllClick(checkBoxAllClientID) {
            if (checkBoxAllClientID == undefined) return;
            var ckall = document.getElementById(checkBoxAllClientID);
            if (ckall == null) return;
            var grid = ckall.parentNode;
            while (grid != null && grid != undefined && grid.nodeName != "div") {
                grid = grid.parentNode;
            }
            var ifSelect = ckall.checked;
            var Chks;
            if (grid == undefined)
                Chks = document.getElementsByTagName("input");
            else
                Chks = grid.getElementsByTagName("input");

            if (Chks.length) {
                for (i = 0; i < Chks.length; i++) {
                    if (Chks[i].type == "checkbox") {
                        Chks[i].checked = ifSelect;
                    }
                }
            }
            else if (Chks) {
                if (Chks.type == "checkbox") {
                    Chks.checked = ifSelect;
                }
            }
        }  </script>

</body>
</html>
