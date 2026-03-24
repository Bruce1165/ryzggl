<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MissExamDetail.aspx.cs" Inherits="ZYRYJG.StAnManage.MissExamDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" />
    <link href="../Skins/Blue/Grid.Blue.css?v=1.008" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Scripts/jquery-3.4.1.min.js"></script>
    <script type="text/javascript" src="../Scripts/Public.js?v=1.011"></script>
    <script type="text/javascript" src="../layer/layer.js"></script>
    <link href="layer/skin/layer.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .infoHead {
            width: 15%;
            text-align: right;
            vertical-align: top;
            font-weight: bold;
            line-height: 150%;
            border-collapse: collapse;
        }

        .formItem_1 {
            width: 35%;
            text-align: left;
            vertical-align: top;
        }

        .formItem_2 {
            text-align: left;
            vertical-align: top;
        }

        .formItem_3 {
            text-align: center;
            vertical-align: middle;
            width: 110px;
        }

        .formItem_1 input, .formItem_2 input {
            border: none;
            line-height: 150%;
        }

        .barTitle {
            color: #434343;
            background-color: #E4E4E4;
            font-weight: bold;
            border-left: 5px solid #ff6a00;
            text-align: left;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Windows7">
        </telerik:RadWindowManager>
        <div class="div_out">
            <div id="div_top" class="dqts">
                <div style="float: left;">
                    当前位置 &gt;&gt; 考务管理 &gt;&gt;
                考试管理 &gt;&gt; <strong>缺考锁定记录</strong>
                </div>
            </div>
            <div class="content">
                <table runat="server" id="EditTable" width="100%" border="0" cellpadding="5" cellspacing="0" class="table" style="text-align: center;">
                    <tr class="GridLightBK">
                        <td colspan="5" class="barTitle">锁定记录</td>
                    </tr>
                    <tr class="GridLightBK">
                        <td class="infoHead">姓名：</td>
                        <td class="formItem_1">
                            <asp:TextBox ID="TextBoxWorkerName" runat="server" CssClass="textEdit" MaxLength="100"></asp:TextBox>
                        </td>
                        <td class="infoHead">证件号码：
                        </td>
                        <td class="formItem_2">
                            <asp:TextBox ID="TextBoxWorkerCertificateCode" runat="server" CssClass="textEdit" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td class="infoHead">锁定日期：</td>
                        <td class="formItem_1">
                            <telerik:RadDatePicker ID="RadDatePickerLockStartDate" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日" />
                        </td>
                        <td class="infoHead">解锁日期：</td>
                        <td class="formItem_2">
                            <telerik:RadDatePicker ID="RadDatePickerLockEndDate" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日" /> <asp:Button ID="ButtonSave" runat="server" Text="保 存" CssClass="button" OnClick="ButtonSave_Click" Visible="false" />
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td colspan="4" align="center">
                           
                        </td>
                    </tr>
                </table>
                <table runat="server" id="Table1" width="100%" border="0" cellpadding="5" cellspacing="0" class="table" style="text-align: center;">
                    <tr class="GridLightBK">
                        <td colspan="4" class="barTitle">缺考记录</td>
                    </tr>
                    <tr class="GridLightBK">
                        <td colspan="4" class="formItem_1">
                            <telerik:RadGrid ID="RadGridExam" runat="server"
                                GridLines="None" AllowPaging="false" AllowSorting="false" AutoGenerateColumns="False" PageSize="15"
                                Width="100%" Skin="Blue" EnableAjaxSkinRendering="false"
                                EnableEmbeddedSkins="false" PagerStyle-AlwaysVisible="true">
                                <ClientSettings EnableRowHoverStyle="true">
                                </ClientSettings>
                                <HeaderContextMenu EnableEmbeddedSkins="False">
                                </HeaderContextMenu>
                                <MasterTableView NoMasterRecordsText=" 没有可显示的记录" CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Left">
                                    <Columns>
                                        <telerik:GridBoundColumn UniqueName="UnitName" DataField="UnitName" HeaderText="单位名称">
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="PostName" DataField="PostName" HeaderText="岗位工种">
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="考试时间" UniqueName="ExamStartDate" DataField="ExamStartDate"
                                            DataFormatString="{0:yyyy-MM-dd}">
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="ExamCardID" DataField="ExamCardID" HeaderText="准考证号">
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn UniqueName="ExamResult" HeaderText="考试结果">
                                            <ItemTemplate>
                                                <%# Eval("ExamResult").ToString() == "合格" ? "合格" : string.Format("{0}（{1}）", Eval("ExamResult").ToString(), Eval("SumScoreDetail").ToString())%>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" Wrap="false" Font-Bold="true" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                    <HeaderStyle Font-Bold="True" />
                                </MasterTableView>

                            </telerik:RadGrid>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
