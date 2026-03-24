<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CertMgr.aspx.cs" Inherits="ZYRYJG.SystemManage.CertMgr" %>

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
    <style type="text/css">
        .auto-style1 {
            height: 37px;
        }

        .font {
            font-size: 13px;
        }


        .auto-style2 {
            height: 25px;
        }


        .auto-style3 {
            height: 50px;
        }


        .auto-style6 {
            font-size: 13px;
            height: 8px;
            width: 90px;
        }


        .auto-style11 {
            font-size: 13px;
            height: 25px;
        }

        .auto-style12 {
            font-size: 13px;
            height: 25px;
        }


        .auto-style13 {
            font-size: 13px;
            height: 25px;
            width: 90px;
        }
    </style>
    <style type="text/css">
        .tl, .flag_red, .flag_orange, .flag_blue, .flag_green, .flag_black {
            color: #444;
            float: left;
            margin: 0 2px;
        }

        .pl20 {
            padding-left: 20px;
        }

        .flag_red {
            background: url(../Images/flag_red.png) no-repeat left center transparent;
        }

        .flag_orange {
            background: url(../Images/flag_orange.png) no-repeat left center transparent;
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
                <%--           <telerik:AjaxSetting AjaxControlID="RadioButtonList2">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="tableQuery" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>--%>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true" Skin="Default" />
        <div class="div_out">
            <div id="div_top" class="dqts">
                <div style="float: left;">
                    当前位置 &gt;&gt;建造师注册管理 &gt;&gt;<strong>注册证书管理</strong>
                </div>
            </div>
            <div class="content">
                <table id="tableQuery" runat="server" class="cx" width="100%" border="0" align="center" cellspacing="3">
                    <tr id="TrPerson" runat="server">
                        <td align="right" style="width: 200px;">
                            <asp:Label ID="LabelCustomer" runat="server" Text="自定义查询字段："></asp:Label>
                            <asp:Label ID="lbl_DateQuery" runat="server" Text="证书或专业有效期 <"></asp:Label>
                        </td>
                        <td align="left" width="130px" class="auto-style11">
                            <telerik:RadComboBox ID="RadComboBoxIten" runat="server" Width="119px" MaxHeight="200px">
                                <Items>
                                    <telerik:RadComboBoxItem Text="姓名" Value="PSN_Name" />
                                    <telerik:RadComboBoxItem Text="证件号码" Value="PSN_CertificateNO" />
                                    <telerik:RadComboBoxItem Text="企业名称" Value="ENT_Name" />
                                    <telerik:RadComboBoxItem Text="机构代码" Value="ENT_OrganizationsCode" />
                                    <telerik:RadComboBoxItem Text="注册号" Value="PSN_RegisterNO" />
                                </Items>
                            </telerik:RadComboBox>



                            <telerik:RadDatePicker ID="RadDatePickerGetDateStart" MinDate="01/01/1900" runat="server" Height="21px" align="left" Style="margin: auto auto" Calendar-DayCellToolTipFormat="yyyy年MM月dd日">
                                <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"></Calendar>
                                <DateInput DisplayDateFormat="yyyy/M/d" DateFormat="yyyy/M/d" Height="21px"></DateInput>
                                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                            </telerik:RadDatePicker>
                        </td>
                        <td align="left" nowrap="nowrap" aria-disabled="False">
                            <telerik:RadTextBox ID="RadTextBoxValue" runat="server" Width="250px" Skin="Default">
                            </telerik:RadTextBox>

                            &nbsp;&nbsp;
                            <asp:Label ID="lbl_Query" runat="server" Text="注销状态："></asp:Label>
                            <telerik:RadComboBox ID="RadComboBoxPSN_RegisteType" runat="server" Width="85px">
                                <Items>
                                    <telerik:RadComboBoxItem Text="全部" Value="" Selected="true" />
                                    <telerik:RadComboBoxItem Text="未注销" Value="未注销" />
                                    <telerik:RadComboBoxItem Text="已注销" Value="已注销" />
                                </Items>
                            </telerik:RadComboBox>
                            &nbsp;&nbsp;
                            <asp:Label ID="lbl_Query1" runat="server" Text="注销年龄："></asp:Label>
                            <telerik:RadComboBox ID="RadComboBoxPSN_Age" runat="server" Width="85px">
                                <Items>
                                    <telerik:RadComboBoxItem Text="全部" Value="" Selected="true" />
                                    <telerik:RadComboBoxItem Text="大于65岁" Value="大于65岁" />
                                    <telerik:RadComboBoxItem Text="大于60岁" Value="大于60岁" />
                                </Items>
                            </telerik:RadComboBox>
                            &nbsp;&nbsp;
                             <asp:Label ID="lbl_Query3" runat="server" Text="企业资质："></asp:Label>
                            <telerik:RadComboBox ID="RadComboBoxENT_Qyzz" runat="server" Width="85px">
                                <Items>
                                    <telerik:RadComboBoxItem Text="全部" Value="" Selected="true" />
                                    <telerik:RadComboBoxItem Text="资质已注销" Value="资质已注销" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">预警类型：
                        </td>
                        <td align="left" colspan="2">
                            <asp:RadioButton ID="RadioButtonAll" runat="server" Text="全部" GroupName="q" Checked="true" />
                            &nbsp;<asp:RadioButton ID="CheckBoxNormalData" runat="server" Text="无预警数据" GroupName="q" />
                            &nbsp;<asp:RadioButton ID="CheckBoxZSSD" runat="server" Text="在施锁定" GroupName="q" />
                            &nbsp;<asp:RadioButton ID="CheckBoxYCZC" runat="server" Text="异常注册" GroupName="q" />
                            &nbsp;<asp:RadioButton ID="RadioButtonGuaZheng" runat="server" Text="未整改完成涉嫌挂证" GroupName="q" />
                            &nbsp;<asp:RadioButton ID="RadioButtonRepeat" runat="server" Text="一年两次重新注册" GroupName="q" />

                        </td>
                    </tr>
                    <tr>
                        <td align="right">批量操作类型：
                        </td>
                        <td align="left" colspan="2">
                            <asp:RadioButtonList ID="RadioButtonList2" runat="server" TextAlign="right" RepeatDirection="Horizontal" Style="display: inline"
                                OnSelectedIndexChanged="RadioButtonList2_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Text="手动锁定" Value="手动锁定" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="手动解锁" Value="手动解锁"></asp:ListItem>
                                <asp:ListItem Text="手动注销" Value="手动注销"></asp:ListItem>
                                <asp:ListItem Text="证书过期注销" Value="证书过期"></asp:ListItem>
                                <asp:ListItem Text="专业过期注销" Value="专业过期"></asp:ListItem>
                            </asp:RadioButtonList>

                            <asp:Button ID="ButtonSearch" runat="server" Text="查 询" OnClick="ButtonSearch_Click" CssClass="button" Style="margin-left: 100px" CausesValidation="false" />
                        </td>
                    </tr>
                </table>
                <div style="line-height: 20px; height: 20px; vertical-align: middle; margin: 8px">
                    <div class="tl pl20">图例说明：</div>
                    <div class="flag_red pl20">在施锁定，</div>
                    <div class="flag_black pl20">异常注册</div>
                </div>
                <telerik:RadGrid ID="RadGridQY" runat="server" Skin="Blue" EnableAjaxSkinRendering="False"
                    EnableEmbeddedSkins="False" OnPageIndexChanged="RadGridQY_PageIndexChanged" OnDataBound="RadGridQY_DataBound"
                    GridLines="None" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" PageSize="10"
                    SortingSettings-SortToolTip="单击进行排序" Width="100%" PagerStyle-AlwaysVisible="true" >
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
                            <telerik:GridBoundColumn UniqueName="ENT_Name" DataField="ENT_Name" HeaderText="企业名称">
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="PSN_RegisterNO" HeaderText="注册号">
                                <ItemTemplate>
                                    <span class="link_edit" onclick=' javascript:SetIfrmSrc("../Unit/EJZJSDetail.aspx?o=<%# Utility.Cryptography.Encrypt(Eval("PSN_ServerID").ToString()) %>"); '><%# Eval("PSN_RegisterNO") %></span>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" Wrap="false" ForeColor="Blue" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn UniqueName="ProfessionWithValid" DataField="ProfessionWithValid" HeaderText="注册专业及有效期">
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                             <telerik:GridTemplateColumn HeaderText="注销状态" UniqueName="PSN_RegisteType">
                                <ItemTemplate>
                                    <%# Eval("PSN_RegisteType").ToString() =="07"?"已注销":"未注销"%>                                  
                                </ItemTemplate>
                                <HeaderStyle Wrap="false" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="异常状态预警" UniqueName="TemplateColumn">
                                <ItemTemplate>
                                    <%# Eval("SDZT") !=DBNull.Value && Eval("SDZT").ToString()=="1"?string.Format("<div  title=\"在施锁定\" class=\"flag_red pointer\" onclick='javascript:SetIfrmSrc(\"../County/ZSSDDetail.aspx?z={0}\");'>&nbsp;</div>", Eval("PSN_RegisterNo")):""%>
                                    <%# Eval("LockID") !=DBNull.Value?string.Format("<div title=\"异常注册\" class=\"flag_black pointer\" onclick='javascript:layer.alert(\"{0}\");'>&nbsp;</div>", Eval("LockRemark")):""%>
                                </ItemTemplate>
                                <HeaderStyle Wrap="false" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="Correct">
                                <ItemTemplate>
                                    <span class="link_edit" onclick=' javascript:SetIfrmSrc("../SystemManage/CertModify.aspx?o=<%# Eval("PSN_ServerID") %>"); '>修正</span>
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
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="Model.COC_TOW_Person_BaseInfoMDL"
                    SelectMethod="GetListWithCertificate" TypeName="DataAccess.COC_TOW_Person_BaseInfoDAL"
                    SelectCountMethod="SelectCountWithCertificate" EnablePaging="true"
                    MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex" SortParameterName="orderBy">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                            DefaultValue="" ConvertEmptyStringToNull="false" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </div>
            <div style="width: 99%; text-align: center; margin: 8px 0; text-align: right; padding-right: 40px">
                <span id="spanOutput" runat="server" class="excel" style="padding-right: 20px; font-weight: bold"></span>
                <asp:Button ID="ButtonOutput" runat="server" Text="导出查询结果" CssClass="bt_large" OnClick="ButtonOutput_Click" CausesValidation="false" />
            </div>
            <div id="divCancel" runat="server" style="width: 99%; padding-top: 10px; text-align: center; clear: both; margin: auto">
                <table id="TableEdit" runat="server" border="0" cellpadding="5" cellspacing="1" class="table" style="margin: 10px auto; width: 99%">
                    <tr class="GridLightBK">
                        <td colspan="2" class="barTitle">批量注销操作（请先勾选要注销的记录）</td>
                    </tr>
                    <tr class="GridLightBK">
                        <td width="10%" align="right">注销原因：</td>
                        <td width="90%" align="left" class="auto-style2">
                            <asp:RadioButtonList ID="RadioButtonListApplyStatus" runat="server" TextAlign="right" CssClass="font" RepeatColumns="3" RepeatDirection="Horizontal">
                                <asp:ListItem Text="注册有效期满且未延续注册" Value="注册有效期满且未延续注册" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="企业资质已注销" Value="企业资质已注销"></asp:ListItem>
                                <asp:ListItem Text="注册专业过期" Value="注册专业过期"></asp:ListItem>
                                <asp:ListItem Text="与原单位分歧" Value="与原单位分歧"></asp:ListItem>
                                <asp:ListItem Text=" 聘用企业破产" Value=" 聘用企业破产"></asp:ListItem>
                                <asp:ListItem Text="死亡或不具有完全民事行为能力" Value="死亡或不具有完全民事行为能力"></asp:ListItem>
                                <asp:ListItem Text=" 聘用企业被吊销营业执照" Value=" 聘用企业被吊销营业执照"></asp:ListItem>
                                <asp:ListItem Text="聘用企业被吊销相应资质证书" Value="聘用企业被吊销相应资质证书"></asp:ListItem>
                                <asp:ListItem Text=" 聘用企业被吊销或者撤回资质证书" Value=" 聘用企业被吊销或者撤回资质证书"></asp:ListItem>
                                <asp:ListItem Text="其他导致注册失效的情形" Value="其他导致注册失效的情形"></asp:ListItem>
                                <asp:ListItem Text=" 已与聘用企业解除聘用合同关系" Value=" 已与聘用企业解除聘用合同关系"></asp:ListItem>
                                <asp:ListItem Text=" 有效期过期" Value=" 有效期过期"></asp:ListItem>
                                <asp:ListItem Text="依法被吊销注册证书" Value="依法被吊销注册证书"></asp:ListItem>
                                <asp:ListItem Text=" 年龄超过65周岁" Value="年龄超过65周岁"></asp:ListItem>
                                <asp:ListItem Text=" 年龄超过60周岁" Value="年龄超过60周岁"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <%--     <td width="10%" align="right">注销意见：</td>--%>
                        <%--       <td width="90%" align="left">

                            <asp:TextBox ID="TextBoxApplyGetResult" runat="server" TextMode="MultiLine" Rows="3" Width="99%" Text="予以注销"></asp:TextBox>
                        </td>--%>
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
                            <strong><asp:Label ID="LabelLockDateTip" runat="server" Text="加锁日期"></asp:Label></strong>
                        </td>
                        <td align="left" >
                            <asp:Label ID="LabelLockDate" runat="server" Text=""></asp:Label>
                        </td>
                         </tr>
                    <tr class="GridLightBK" id="trLockEndTime">
                        <td align="right" nowrap="nowrap">
                            <strong>锁定截止日期</strong>
                        </td>
                        <td align="left" >
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
                            <strong><asp:Label ID="LabelLockRemark" runat="server" Text="锁定原因说明"></asp:Label></strong>
                        </td>
                        <td align="left" >
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
