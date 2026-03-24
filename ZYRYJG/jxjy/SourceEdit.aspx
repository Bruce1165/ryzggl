<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="SourceEdit.aspx.cs" Inherits="ZYRYJG.jxjy.SourceEdit" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/SelectImg.ascx" TagPrefix="uc1" TagName="SelectImg" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
    <script src="../Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
    <link href="../layer/skin/layer.css" rel="stylesheet" />
    <script src="../layer/layer.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>      
        <telerik:RadWindowManager ID="RadWindowManager1" ShowContentDuringLoad="false" VisibleStatusbar="false"  OnClientClose="OnClientClose"
            ReloadOnShow="true" runat="server" Skin="Windows7" EnableShadow="true">
            <AlertTemplate>
                <div class="alertText">
                    {1}
                </div>
                <div class="confrimButton">
                    <input onclick="$find('{0}').close();" class="button" id="ButtonOK" type="button"
                        value="确 定" />
                </div>
            </AlertTemplate>
            <ConfirmTemplate>
                <div class="confrimText">
                    {1}
                </div>
                <div class="confrimButton">
                    <input onclick="$find('{0}').close(true);" class="button" id="ButtonOK" type="button"
                        value="确 定" />&nbsp;&nbsp;
                <input onclick="$find('{0}').close(false);" class="button" id="ButtonCancel" type="button"
                    value="取 消" />
                </div>
            </ConfirmTemplate>
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
            <div id="div_top" class="dqts">
                <div style="float: left;">
                    当前位置 &gt;&gt;在线公益教育培训 &gt;&gt;<strong>培训课程编辑</strong>
                </div>
            </div>
            <div class="content">

                <table id="TableEdit" runat="server" width="95%" border="0" cellpadding="5" cellspacing="1"
                    class="table" align="center">
                    <tr class="GridLightBK">
                        <td width="12%" nowrap="nowrap" align="right">
                            <span style="color: Red">* </span>
                            <asp:Label ID="Label_Source" runat="server" Text="课程名称"></asp:Label>
                            ：
                        </td>
                        <td width="38%">
                            <telerik:RadTextBox ID="RadTextBoxSourceName" runat="server" Width="95%" Skin="Default"
                                MaxLength="100">
                            </telerik:RadTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorSourceName" runat="server"
                                Display="Dynamic" ErrorMessage="必填" ControlToValidate="RadTextBoxSourceName"></asp:RequiredFieldValidator>
                        </td>
                        <td width="12%" nowrap="nowrap" align="right">
                           <span style="color: Red">* </span>年度：
                        </td>
                        <td width="38%">                            
                                 <telerik:RadNumericTextBox ID="RadNumericTextBoxSourceYear" runat="server" MaxLength="4"
                                     Type="Number" NumberFormat-DecimalDigits="0" ShowSpinButtons="true" Width="80px"
                                     MinValue="2021" MaxValue="2050">
                                     <NumberFormat DecimalDigits="0"></NumberFormat>
                                 </telerik:RadNumericTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="Dynamic"
                                ErrorMessage="必填" ControlToValidate="RadNumericTextBoxSourceYear"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td width="12%" align="right">
                            <span style="color: Red">* </span>学 时：
                        </td>
                        <td width="38%">
                            <telerik:RadNumericTextBox ID="RadNumericTextBoxPeriodHour" runat="server" MaxLength="3"
                                Type="Number" NumberFormat-DecimalDigits="0" ShowSpinButtons="true" Width="60px"
                                MinValue="0">
                                <NumberFormat DecimalDigits="0"></NumberFormat>
                            </telerik:RadNumericTextBox>
                            时 -
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic"
                                ErrorMessage="必填" ControlToValidate="RadNumericTextBoxPeriodHour"></asp:RequiredFieldValidator>
                            <telerik:RadNumericTextBox ID="RadNumericTextBoxMinute" runat="server" MaxLength="2"
                                Type="Number" NumberFormat-DecimalDigits="0" ShowSpinButtons="true" Width="80px"
                                MinValue="0" MaxValue="59">
                                <NumberFormat DecimalDigits="0"></NumberFormat>
                            </telerik:RadNumericTextBox>
                            分
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic"
                                ErrorMessage="必填" ControlToValidate="RadNumericTextBoxMinute"></asp:RequiredFieldValidator>
                        </td>
                        <td width="12%" align="right">
                            <span style="color: Red">* </span>排序ID：
                        </td>
                        <td width="38%">
                            <telerik:RadNumericTextBox ID="RadNumericTextBoxSortID" runat="server" MaxLength="8"
                                Type="Number" NumberFormat-DecimalDigits="0" ShowSpinButtons="true" Width="80px"
                                MinValue="0">
                                <NumberFormat DecimalDigits="0"></NumberFormat>
                            </telerik:RadNumericTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic"
                                ErrorMessage="必填" ControlToValidate="RadNumericTextBoxSortID"></asp:RequiredFieldValidator>
                          
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td width="12%" align="right">
                            对外显示学时：
                        </td>                       
                        <td  align="left" colspan="3">
                            <asp:Label ID="LabelShowPeriod" runat="server" Text=""></asp:Label><span  style="color:#999">（说明：此学时为对学员开放显示学时，非课件真实长度）</span>
                        </td>
                      
                    </tr>
                    <tr class="GridLightBK" id="Tr1" runat="server">
                        <td width="12%" align="right">
                            <span style="color: Red">* </span>授课教师：
                        </td>
                        <td width="38%">
                            <telerik:RadTextBox ID="RadTextBoxTeacher" runat="server" Width="95%" Skin="Default"
                                MaxLength="50">
                            </telerik:RadTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="必填"
                                Display="Dynamic" ControlToValidate="RadTextBoxTeacher"></asp:RequiredFieldValidator>
                        </td>
                        <td width="12%" align="right">工作单位：
                        </td>
                        <td width="38%">
                            <telerik:RadTextBox ID="RadTextBoxWorkUnit" runat="server" Width="95%" Skin="Default"
                                MaxLength="100">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr class="GridLightBK" id="Tr2" runat="server">
                        <td align="right" width="12%" nowrap="nowrap">
                            <span style="color: Red">* </span>状 态：
                        </td>
                        <td width="38%">
                            <asp:RadioButtonList ID="RadioButtonListStatus" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="启用" Value="启用" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="停用" Value="停用"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td width="12%" align="right">
                            <span style="color: Red">* </span>课程类型：
                        </td>
                        <td width="38%">
                            <asp:RadioButtonList ID="RadioButtonListSourceType" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="必修" Value="必修" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="选修" Value="选修"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                   
                     <tr class="GridLightBK" id="TrBarType" runat="server">
                        <td width="12%" align="right" nowrap="nowrap">所属栏目：
                        </td>
                        <td colspan="3">
                              <telerik:RadComboBox ID="RadComboBoxBarType" runat="server">
                                <Items>
                                    <telerik:RadComboBoxItem Text="请选择" Value="" />
                                     <telerik:RadComboBoxItem Text="工匠讲堂" Value="工匠讲堂" />
                                      <telerik:RadComboBoxItem Text="首都建设云课堂" Value="首都建设云课堂" />
                                    <telerik:RadComboBoxItem Text="行业推荐精品课程" Value="行业推荐精品课程" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr class="GridLightBK" id="Tr4" runat="server">
                        <td width="12%" align="right" nowrap="nowrap">课程简介：
                        </td>
                        <td colspan="3">
                            <telerik:RadTextBox ID="RadTextBoxDescription" runat="server" Width="95%" Skin="Default"
                                TextMode="MultiLine" Rows="3" MaxLength="2000">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                     <tr class="GridLightBK" id="TrSourceImg" runat="server">
                        <td width="12%" align="right" nowrap="nowrap">背景图片：
                        </td>
                        <td colspan="3">
                            <uc1:SelectImg runat="server" ID="SelectImg1" />
                        </td>
                    </tr>
                    <tr class="GridLightBK" id="Tr5" runat="server">
                        <td width="12%" align="right" nowrap="nowrap">课件地址：
                        </td>
                        <td>
                            <telerik:RadTextBox ID="RadTextBoxSourceWareUrl" runat="server" Width="300px" Skin="Default"
                                MaxLength="500">
                            </telerik:RadTextBox>

                        </td>
                        <td width="12%" align="right" nowrap="nowrap">SDK ID：
                        </td>
                        <td width="38%">
                            <telerik:RadTextBox ID="RadTextBoxSDKID" runat="server" Width="300px" Skin="Default"
                                MaxLength="50">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="width: 95%; margin: 10px auto; text-align: center;">
                <asp:Button ID="ButtonSave" runat="server" Text="保 存" CssClass="button" OnClick="ButtonSave_Click" />&nbsp;&nbsp;
                <input id="ButtonReturn" type="button" value="返 回" class="button" onclick="javascript: hideIfam();" />
            </div>
        </div>
    </form>
</body>
</html>
