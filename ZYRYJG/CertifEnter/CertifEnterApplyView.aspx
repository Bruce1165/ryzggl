<%@ Page Title="" Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="CertifEnterApplyView.aspx.cs" Inherits="ZYRYJG.CertifEnter.CertifEnterApplyView" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../PostSelect.ascx" TagName="PostSelect" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadWindowManager ID="RadWindowManager1" ShowContentDuringLoad="false" VisibleStatusbar="false"
        ReloadOnShow="true" runat="server" Skin="Windows7" EnableShadow="true">
    </telerik:RadWindowManager>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true"
        Skin="Windows7" />
    <div class="div_out">
       <%--<div id="div_top" class="dqts">
            <div id="divRoad" runat="server" style="float: left;">
               
            </div>
        </div>--%>
        <div class="dqts">
            <div id="divRoad" style="float: left;">
                当前位置 &gt;&gt; 证书进京 &gt;&gt;<strong>证书进京申请表</strong>
            </div>
        </div>
        <div class="table_border word" >           
            <div class="content">
                <p class="jbxxbt">
                    证书进京申请表
                </p>
                <div style="width: 95%; margin: 10px auto; text-align: center;">
                    <table cellpadding="5" cellspacing="1" border="0" width="95%" align="center">
                        <tr>
                            <td align="left">
                                申请日期：
                                <asp:Label ID="LabelApplyDate" runat="server" Text=""></asp:Label>
                            </td>
                            <td align="right">
                                申请批次号：
                                <asp:Label ID="LabelApplyCode" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table id="TableEdit" runat="server" width="95%" border="0" cellpadding="5" cellspacing="1"
                        class="table2" align="center">
                        <tr class="GridLightBK">
                            <td width="10%" nowrap="nowrap" align="center">
                                姓 名
                            </td>
                            <td width="38%">
                                <telerik:RadTextBox ID="RadTextBoxWorkerName" runat="server" Width="80%" Skin="Default">
                                </telerik:RadTextBox>
                               
                            </td>
                            <td align="center" width="10%">
                                性别
                            </td>
                            <td width="28%">
                                <asp:RadioButton ID="RadioButtonMan" runat="server" Text="男" GroupName="1" Checked="true"
                                    Enabled="false" />&nbsp;<asp:RadioButton ID="RadioButtonWoman" Text="女" GroupName="1"
                                        runat="server" Enabled="false" />
                            </td>
                            <td rowspan="4" align="center" style="width: 110px;">
                                <img id="ImgCode" runat="server" height="140" width="110" alt="照片" src="" />
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td width="10%" align="center">
                                身份证号
                            </td>
                            <td width="38%">
                                <telerik:RadTextBox ID="RadTextBoxWorkerCertificateCode" runat="server" Width="80%"
                                    Skin="Default">
                                </telerik:RadTextBox>
                           
                            </td>
                            <td width="10%" align="center">
                                出生日期
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="RadDatePickerBirthday" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                    Width="98%" Enabled="false" />
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td width="10%" nowrap="nowrap" align="center">
                                &nbsp;原聘用单位全称
                            </td>
                            <td colspan="3">
                                <telerik:RadTextBox ID="RadTextBoxOldUnitName" runat="server" Width="90%" Skin="Default">
                                </telerik:RadTextBox>
                                                           </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td width="10%" nowrap="nowrap" align="center">
                                &nbsp;现聘用单位全称
                            </td>
                            <td colspan="3">
                                <telerik:RadTextBox ID="RadTextBoxUnitName" runat="server" Width="90%" Skin="Default">
                                </telerik:RadTextBox>
                              
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td width="10%" nowrap="nowrap" align="center">
                                现聘用单位<br />
                                组织机构代码（9位）
                            </td>
                            <td width="38%">
                                <telerik:RadTextBox ID="RadTextBoxUnitCode" runat="server" Width="82%" Skin="Default"
                                    MaxLength="9">
                                </telerik:RadTextBox>
                              
                            </td>
                            <td align="center" width="10%" nowrap="nowrap">
                                联系电话
                            </td>
                            <td colspan="2" width="38%">
                                <telerik:RadTextBox ID="RadTextBoxPhone" runat="server" Width="95%" Skin="Default">
                                </telerik:RadTextBox>
                               
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td width="10%" align="right" nowrap="nowrap">
                                岗位工种：
                            </td>
                            <td colspan="4">
                                <uc1:PostSelect ID="PostSelect1" runat="server" />
                                <div style="float: left; clear: right; padding-left: 10px;">
                                    <asp:CheckBox ID="CheckBoxAddItem" runat="server" Text="" Style="display: none;" /></div>
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td width="10%" align="right" nowrap="nowrap">
                                发证机关：
                            </td>
                            <td width="38%">
                                <telerik:RadTextBox ID="RadTextBoxConferUnit" runat="server" Width="97%" Skin="Default">
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="必填"
                                    ControlToValidate="RadTextBoxConferUnit" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                            <td width="10%" align="right" nowrap="nowrap">
                                发证日期：
                            </td>
                            <td width="38%" colspan="2">
                                <telerik:RadDatePicker ID="RadDatePickerConferDate" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                    Width="97%">
                                    <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x">
                                    </Calendar>
                                    <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                    <DateInput DisplayDateFormat="yyyy-M-d" DateFormat="yyyy-M-d">
                                    </DateInput>
                                </telerik:RadDatePicker>
                               
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td width="10%" align="right">
                                证书编号：
                            </td>
                            <td width="38%">
                                <telerik:RadTextBox ID="RadTextBoxCertificateCode" runat="server" Width="97%" Skin="Default">
                                </telerik:RadTextBox>
                              
                            </td>
                            <td width="10%" align="right" nowrap="nowrap">
                                证书有效期：
                            </td>
                            <td width="38%" colspan="2" align="left">
                                <div class="RadPicker">自</div>
                                <telerik:RadDatePicker ID="RadDatePickerValidStartDate" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                    Width="40%">
                                    <Calendar runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                                        ViewSelectorText="x">
                                    </Calendar>
                                    <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                    <DateInput DisplayDateFormat="yyyy-M-d" DateFormat="yyyy-M-d">
                                    </DateInput>
                                </telerik:RadDatePicker>
                              
                                <div class="RadPicker">至</div>
                                <telerik:RadDatePicker ID="RadDatePickerValidEndDate" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                    Width="40%">
                                    <Calendar runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                                        ViewSelectorText="x">
                                    </Calendar>
                                    <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                    <DateInput DisplayDateFormat="yyyy-M-d" DateFormat="yyyy-M-d">
                                    </DateInput>
                                </telerik:RadDatePicker>
                              
                            </td>
                        </tr>
                    </table>
                    <br />
                    <hr />
                    <table style="width: 100%; padding-bottom: 20px;">
                        <tr>
                            <td align="center" colspan="2">
                                <input id="ButtonReturn" type="button" value="返 回" class="button" onclick="javascript:hideIfam();" />
                                <br />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
