<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CertificateMergeApply.aspx.cs" Inherits="ZYRYJG.CertifManage.CertificateMergeApply" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" />
    <link href="../layer/skin/layer.css" rel="stylesheet" />
    <script src="../Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
    <script src="../layer/layer.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
            <script type="text/javascript">
                $(function () {
                    //单位审核结果
                    $("#<%= RadioButtonListOldUnitCheckResult.ClientID%> input").each(function () {
                        $(this).click(function () {
                            var TextBoxOldUnitCheckRemark = $("#<%= TextBoxOldUnitCheckRemark.ClientID%>");

                        if ($(this).val() == "同意") {

                            TextBoxOldUnitCheckRemark.val("提交建委审核");
                        }
                        else {

                            TextBoxOldUnitCheckRemark.val("退回个人");

                        }
                    });

                });

                    //建委审核结果
                    $("#<%= RadioButtonListJWCheck.ClientID%> input").each(function () {
                        $(this).click(function () {
                            var TextBoxCheckResult = $("#<%= TextBoxCheckResult.ClientID%>");

                        if ($(this).val() == "通过") {

                            TextBoxCheckResult.val("通过");
                        }
                        else {

                            TextBoxCheckResult.val("退回个人");

                        }
                    });

                    });
                });

            //审核确认
            function JWSubmitTip() {
                var CheckSelect = $("input[name='RadioButtonListJWCheck']:checked").val();
                var CheckResult = $("#<%= TextBoxCheckResult.ClientID%>").val();
                        if (CheckSelect == "通过" && CheckResult != "通过") {
                            return confirm('您选择了审核通过，却修改了审核意见，您确定要提交审核意见么?');
                        }
                        if (CheckSelect == "不通过" && CheckResult == "通过") {
                            return confirm('您选择了审核不通过，却没有修改详细的审核意见，您确定要提交审核意见么?');
                        }
                        return true;
                    }
            </script>
        </telerik:RadCodeBlock>
        <telerik:RadWindowManager runat="server" RestrictionZoneID="offsetElement" ID="RadWindowManager1"
            EnableShadow="true" EnableEmbeddedScripts="true" Skin="Windows7" VisibleStatusbar="false">
        </telerik:RadWindowManager>
        <div class="div_out">
            <div id="div_top" class="dqts">
                <div style="float: left;">
                    当前位置 &gt;&gt; 从业人员证书管理 &gt;&gt;
              三类人C1C2证书合并
                </div>
            </div>
            <div class="step">
                <div class="stepLabel">办理进度：</div>
                <div id="step_填报中" runat="server" class="stepItem lgray">个人填写></div>
                <div id="step_待单位确认" runat="server" class="stepItem lgray">待单位确认></div>
                <div id="step_已申请" runat="server" class="stepItem lgray">待市建委审核></div>        
                <div id="step_已决定" runat="server" class="stepItem lgray">市建委决定></div>
                <div id="step_已办结" runat="server" class="stepItem lgray">住建部生成电子证书（办结，下载电子证书）</div>
                <div class="stepArrow">▶</div>
            </div>
            <div class="DivTitleOn" onclick="DivOnOff(this,'Td3',event);" title="折叠">
                业务说明
            </div>
            <div class="DivContent" id="Td3">
                1、办事流程：个人发起合并申请 > 企业确认 > 市建委审核 > 生成新的电子证书 > 个人下载电子证书。<br />
                2、存在B、C1、C2不在同一单位情况，不允许发起合并申请，必须先变更到同一家单位。<br />
                3、C1、C2证书存在未办结的变更或续期时，不允许发起合并申请，必须办结在途业务或取消在途申请。<br />
                4、合并申请审批通过后，生成新证书C3，有效截止日期取原C1、C2证书中最大有效截止日期。原C1、C2证书自动注销。<br />
                5、合并申请审批通过后1或2个工作日后，个人下载新版电子证书。<br />
            </div>
            <div class="content">

                <div id="DivEdit" runat="server" style="width: 100%; margin: 10px auto; text-align: center; overflow: hidden;">
                    <table cellpadding="5" cellspacing="1" border="0" width="99%" class="apply" align="center">
                        <tr class="GridLightBK" id="tr_PatchCheck" runat="server" visible="false">
                            <td colspan="5" style="text-align:center; color:red;font-weight:bolder;">
                                <asp:Label ID="LabelCheckCount" runat="server" Text="批量审核"></asp:Label></td>
                        </tr>
                        <tr class="GridLightBK">
                            <td colspan="5" class="barTitle">个人基本信息</td>
                        </tr>
                        <tr class="GridLightBK">
                            <td width="7%" nowrap="nowrap" style="text-align: right">姓名:
                            </td>
                            <td width="31%" style="text-align: left">
                                <asp:Label ID="LabelWorkerName" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="text-align: right" width="7%">性别：
                            </td>
                            <td style="text-align: left">
                                <asp:Label ID="LabelSex" runat="server" Text=""></asp:Label>
                            </td>
                            <td width="110px" rowspan="3" align="center">
                                <img id="ImgCode" runat="server" height="140" width="110" src="~/Images/photo_ry.jpg"
                                    alt="一寸照片" />
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td style="text-align: right">证件号码：
                            </td>
                            <td style="text-align: left">
                                <asp:Label ID="LabelWorkerCertificateCode" runat="server" Text=""></asp:Label>
                            </td>

                            <td style="text-align: right" width="7%">出生日期：
                            </td>
                            <td style="text-align: left">
                                <asp:Label ID="LabelBirthday" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                        </tr>
                        <tr class="GridLightBK">
                            <td style="text-align: right">单位名称：
                            </td>
                            <td style="text-align: left">
                                <asp:Label ID="LabelUnitName" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="text-align: right">机构代码：
                            </td>
                            <td style="text-align: left">
                                <asp:Label ID="LabelUnitCode" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td colspan="5" class="barTitle">C1证书信息</td>
                        </tr>
                        <tr class="GridLightBK">
                            <td style="text-align: right" nowrap="nowrap">岗位工种：
                            </td>
                            <td style="text-align: left">
                                <asp:Label ID="LabelPostName1" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="text-align: right">证书编号：
                            </td>
                            <td colspan="2" style="text-align: left">
                                <asp:Label ID="LabelCertificateCode1" runat="server" Text=""></asp:Label>
                            </td>

                        </tr>
                        <tr class="GridLightBK">
                            <td style="text-align: right" nowrap="nowrap">发证时间：
                            </td>
                            <td style="text-align: left">
                                <asp:Label ID="LabelConferDate1" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="text-align: right" nowrap="nowrap">有效期至：
                            </td>
                            <td colspan="2" style="text-align: left">
                                <asp:Label ID="LabelValidEndDate1" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td colspan="5" class="barTitle">C2证书信息</td>
                        </tr>
                        <tr class="GridLightBK">
                            <td style="text-align: right" nowrap="nowrap">岗位工种：
                            </td>
                            <td style="text-align: left">
                                <asp:Label ID="LabelPostName2" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="text-align: right">证书编号：
                            </td>
                            <td colspan="2" style="text-align: left">

                                <asp:Label ID="LabelCertificateCode2" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td style="text-align: right" nowrap="nowrap">发证时间：
                            </td>
                            <td style="text-align: left">
                                <asp:Label ID="LabelConferDate2" runat="server" Text=""></asp:Label>
                            </td>
                            <td style="text-align: right" nowrap="nowrap">有效期至：
                            </td>
                            <td colspan="2" style="text-align: left">
                                <asp:Label ID="LabelValidEndDate2" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        
                    </table>

                    <div id="divCheckHistory" runat="server" style="width: 100%; padding-top: 20px; text-align: center; clear: both;">
                        <table id="Table1" runat="server" width="100%" border="0" cellpadding="5" cellspacing="1" class="table" style="text-align: center; margin: 10px auto">
                            <tr class="GridLightBK">
                                <td class="barTitle">审办记录</td>
                            </tr>
                            <tr class="GridLightBK">
                                <td align="left" style="border-collapse: collapse;">
                                    <telerik:RadGrid ID="RadGridCheckHistory" runat="server" ShowHeader="true" CellPadding="0" CellSpacing="0"
                                        GridLines="None" AllowPaging="False" AllowSorting="False" AutoGenerateColumns="False"
                                        Width="100%" EnableEmbeddedSkins="false" PagerStyle-AlwaysVisible="False">
                                        <ClientSettings EnableRowHoverStyle="False">
                                        </ClientSettings>
                                        <MasterTableView NoMasterRecordsText="" CommandItemDisplay="None">
                                            <Columns>
                                                <telerik:GridBoundColumn HeaderText="序号" UniqueName="RowNo" DataField="RowNo">
                                                        <ItemStyle Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="流程" UniqueName="Action" DataField="Action">
                                                        <ItemStyle Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="办理人" UniqueName="ActionMan" DataField="ActionMan" Display="false">
                                                        <ItemStyle Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="办理时间" UniqueName="ActionData" DataField="ActionData" HtmlEncode="false" DataFormatString="{0:yyyy.MM.dd}">
                                                        <ItemStyle Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="办理结果" UniqueName="ActionResult" DataField="ActionResult">
                                                        <ItemStyle Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="办理意见" UniqueName="ActionRemark" DataField="ActionRemark">
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                            <HeaderStyle Font-Bold="True" BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1" Wrap="false" />
                                            <ItemStyle CssClass="subtable" />
                                            <AlternatingItemStyle CssClass="subtable" />
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </td>
                            </tr>
                        </table>
                    </div>

                    <div id="divCtl" runat="server" visible="false" style="width: 100%; margin: 10px auto 50px auto; text-align: center; overflow: hidden;">
                        <table style="width: 100%; padding-bottom: 20px;">
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:Button ID="btnSave" Text='提交单位审核' runat="server" OnClick="btnSave_Click" CssClass="button" Enabled="false"></asp:Button>&nbsp;
                                      
                                <asp:Button ID="ButtonDelete" Text="取消申报" runat="server" CssClass="button" Enabled="false"
                                    OnClick="ButtonDelete_Click"></asp:Button>&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                    <table id="TableUnitCheck" runat="server" visible="false" width="100%" border="0" cellpadding="5" cellspacing="1" class="table" style="text-align: center; margin: 10px auto">
                        <tr class="GridLightBK">
                            <td colspan="2" class="barTitle">单位审核</td>
                        </tr>
                        <tr class="GridLightBK">
                            <td width="20%" style="text-align: right">处理结果：</td>
                            <td width="80%" align="left">
                                <asp:RadioButtonList ID="RadioButtonListOldUnitCheckResult" runat="server" RepeatDirection="Horizontal" TextAlign="right">
                                    <asp:ListItem Text="同意" Value="同意" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="不同意" Value="不同意"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td width="20%" style="text-align: right">处理意见：</td>
                            <td width="80%" align="left">

                                <asp:TextBox ID="TextBoxOldUnitCheckRemark" runat="server" TextMode="MultiLine" Rows="3" Width="99%" MaxLength="100" Text="提交建委审核"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td colspan="2" align="center">
                                  <asp:CheckBox ID="CheckBoxAutoPrintUnit" runat="server" Text="后续申请都按照此意见审批处理" TextAlign="Left"  Visible="false"/>&nbsp;&nbsp;
                                <asp:Button ID="ButtonUnitCheck" runat="server" CssClass="bt_large" Text="确 定" OnClick="ButtonUnitCheck_Click" />&nbsp;&nbsp;
                                         
                            </td>
                        </tr>
                    </table>
                    <table id="TableJWCheck" runat="server" visible="false" border="0" cellpadding="5" cellspacing="1" width="100%" class="table" style="text-align: center; margin: 10px auto">
                        <tr class="GridLightBK">
                            <td colspan="2" class="barTitle">变更审核</td>
                        </tr>
                        <tr class="GridLightBK">
                            <td width="20%" align="right">处理结果：</td>
                            <td width="80%" align="left">
                                <asp:RadioButtonList ID="RadioButtonListJWCheck" runat="server" RepeatDirection="Horizontal" TextAlign="right">
                                    <asp:ListItem Text="通过" Value="通过" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="不通过" Value="不通过"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td width="20%" style="text-align: right">处理意见：</td>
                            <td width="80%" align="left">

                                <asp:TextBox ID="TextBoxCheckResult" runat="server" TextMode="MultiLine" Rows="3" Width="99%" MaxLength="200" Text="通过"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td colspan="2" align="center">
                                <asp:CheckBox ID="CheckBoxAutoPrint" runat="server" Text="后续申请都按照此意见审批处理" TextAlign="Left"  Visible="false"/>&nbsp;&nbsp;
                                <asp:Button ID="ButtonCheck" runat="server" CssClass="bt_large" Text="确 定" OnClick="ButtonCheck_Click" OnClientClick="if(JWSubmitTip()==false) return false;" />&nbsp;&nbsp;
                                         
                            </td>
                        </tr>
                    </table>
                </div>
                 <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Enabled="false" Interval="5000"
        EnableViewState="true">
    </asp:Timer>
            </div>

        </div>
    </form>
</body>
</html>
