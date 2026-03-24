<%@ Page Title="" Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="CertifMoreCheck.aspx.cs" Inherits="ZYRYJG.CertifManage.CertifMoreCheck" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadWindowManager ID="RadWindowManager1" ShowContentDuringLoad="false" VisibleStatusbar="false"
        ReloadOnShow="true" runat="server" Skin="Windows7" EnableShadow="true">
    </telerik:RadWindowManager>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            $(function () {
                var imgWid = 0;
                var imgHei = 0; //变量初始化
                var big = 2.5;//放大倍数
                $(".img200").hover(function () {

                    $(this).stop(true, true);
                    var imgWid2 = 0; var imgHei2 = 0;//局部变量
                    imgWid = $(this).width();
                    imgHei = $(this).height();
                    imgWid2 = imgWid * big;
                    imgHei2 = imgHei * big;

                    $("#divImg").css({ "float": "right", "overflow": "visible" });
                    $(this).animate({ "width": imgWid2, "height": imgHei2, "margin-left": -imgWid * (big - 1), "position": "absolute", "z-index": 999 });
                }, function () {
                    $("#divImg").css({ "float": "right", "overflow": "auto" });
                    $(this).stop().animate({ "width": imgWid, "height": imgHei, "margin-left": 0, "position": "relative", "float": "none" });
                });

                $(".img200").click(function () {
                    var nw = window.open($(this)[0].src, "_blank", 'resizable=yes');
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

                //建委决定结果
                $("#<%= RadioButtonListJWConfirm.ClientID%> input").each(function () {
                    $(this).click(function () {
                        var TextBoxConfirmResult = $("#<%= TextBoxConfirmResult.ClientID%>");

                        if ($(this).val() == "通过") {

                            TextBoxConfirmResult.val("通过");
                        }
                        else {

                            TextBoxConfirmResult.val("退回个人");

                        }
                    });

                });
            });

            //审核确认
            function JWSubmitTip() {
                var CheckSelect = $("input[name='RadioButtonListJWConfirm']:checked").val();
                var CheckResult = $("#<%= TextBoxConfirmResult.ClientID%>").val();
                if (CheckSelect == "通过" && CheckResult != "通过") {
                    return confirm('您选择了决定通过，却修改了决定意见，您确定要提交决定意见么?');
                }
                if (CheckSelect == "不通过" && CheckResult == "通过") {
                    return confirm('您选择了决定不通过，却没有修改详细的决定意见，您确定要提交决定意见么?');
                }
                return true;
            }


        </script>
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
                当前位置 &gt;&gt; 证书管理 &gt;&gt;三类人员 &gt;&gt; <strong>
                    <asp:Label ID="LabelCheckTitle" runat="server" Text="A本增发审核"></asp:Label></strong>
            </div>
        </div>
        <div class="content">
            <div class="step">
                <div class="stepLabel">办理进度：</div>
                <div id="step_填报中" runat="server" class="stepItem lgray">个人填写></div>
                <div id="step_待单位确认" runat="server" class="stepItem lgray">待单位确认></div>
                <div id="step_已申请" runat="server" class="stepItem lgray">待市建委审核></div>
                <div id="step_已审核" runat="server" class="stepItem lgray">市建委审核></div>               
                <div id="step_已决定" runat="server" class="stepItem lgray">市建委决定></div>
                <div id="step_已办结" runat="server" class="stepItem lgray">住建部生成电子证书（办结，下载电子证书）</div>
                <div class="stepArrow">▶</div>
            </div>
            <div id="DivEdit" runat="server" style="width: 100%; margin: 10px auto; text-align: center; overflow: hidden;">
                <div style="width: 66%; float: left; clear: left">
                    <table id="TableEdit" runat="server" width="99%" border="0" cellpadding="5" cellspacing="1"
                        class="table2" align="center">
                        <tr class="GridLightBK" id="tr_PatchCheck" runat="server" visible="false">
                            <td colspan="6" style="text-align: center; color: red; font-weight: bolder;">
                                <asp:Label ID="LabelCheckCount" runat="server" Text="批量决定"></asp:Label></td>
                        </tr>
                        <tr class="GridLightBK">
                            <td colspan="6" class="barTitle">申请表</td>
                        </tr>
                        <tr class="GridLightBK">
                            <td width="16%" nowrap="nowrap" align="right">申请日期：
                            </td>
                            <td colspan="4">
                                <asp:Label ID="LabelApplyDate" runat="server" Text=""></asp:Label>
                            </td>
                            <td rowspan="5" align="center" style="width: 110px;">
                                <img id="ImgCode" runat="server" height="140" width="110" alt="照片" src="~/Images/photo_ry.jpg" />
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td align="right">身份证号：
                            </td>
                            <td colspan="4">
                                <asp:Label ID="LabelWorkerCertificateCode" runat="server" Text=""></asp:Label>
                            </td>

                        </tr>
                        <tr class="GridLightBK">
                            <td nowrap="nowrap" align="right">姓 名：
                            </td>
                            <td colspan="4">
                                <asp:Label ID="LabelWorkerName" runat="server" Text=""></asp:Label>


                            </td>
                        </tr>
                        <tr class="GridLightBK">

                            <td align="right">性别：
                            </td>
                            <td width="16%">
                                <asp:Label ID="LabelSex" runat="server" Text=""></asp:Label>

                            </td>
                            <td align="right" width="16%">出生日期：
                            </td>
                            <td colspan="2" width="32%">

                                <asp:Label ID="LabelBirthday" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td nowrap="nowrap" align="right">
                                <span style="color: Red">* </span>联系电话：
                            </td>
                            <td colspan="4">
                                <telerik:RadTextBox ID="RadTextBoxPeoplePhone" runat="server" Width="80%" Skin="Default" MaxLength="25" ReadOnly="true">
                                </telerik:RadTextBox>

                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td colspan="3" align="center" style="font-weight: bold">现有证书信息
                            </td>
                            <td colspan="3" align="center" style="font-weight: bold">增发证书信息
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td nowrap="nowrap" align="right" width="16%">单位全称：
                            </td>
                            <td colspan="2">
                                <asp:Label ID="LabelUnitName" runat="server" Text=""></asp:Label>


                            </td>

                            <td nowrap="nowrap" align="right" width="16%">&nbsp;<span style="color: Red">* </span>单位全称：
                            </td>
                            <td colspan="2">
                                <telerik:RadTextBox ID="RadTextBoxUnitNameMore" runat="server" Width="90%" Skin="Default" MaxLength="100" ReadOnly="true">
                                </telerik:RadTextBox>

                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td nowrap="nowrap" align="right">组织机构代码（9位）：
                            </td>
                            <td colspan="2">
                                <asp:Label ID="LabelUnitCode" runat="server" Text=""></asp:Label>

                            </td>
                            <td nowrap="nowrap" align="right">&nbsp;<span style="color: Red">* </span>组织机构代码（9位）：
                            </td>
                            <td colspan="2">
                                <telerik:RadTextBox ID="RadTextBoxUnitCodeMore" runat="server" Width="90%" Skin="Default"
                                    MaxLength="9">
                                </telerik:RadTextBox>

                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td width="10%" align="right">证书编号：
                            </td>
                            <td colspan="2">
                                <span class="link_edit" onclick='javascript:SetIfrmSrc("../PersonnelFile/CertificateInfo.aspx?o=<%=ViewState["CertificateMoreMDL"]==null?"":Utility.Cryptography.Encrypt(((Model.CertificateMoreMDL)ViewState["CertificateMoreMDL"]).CertificateID.ToString()) %>");'>
                                    <%=ViewState["CertificateMoreMDL"]==null?"":((Model.CertificateMoreMDL)ViewState["CertificateMoreMDL"]).CertificateCode%>
                                </span>
                            </td>
                            <td align="right">证书编号：
                            </td>
                            <td colspan="2">
                                <asp:Label ID="LabelCertificateCodeMore" runat="server" Text=""></asp:Label>

                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td align="right" nowrap="nowrap">发证日期：
                            </td>
                            <td colspan="2">
                                <asp:Label ID="LabelValidStartDate" runat="server" Text=""></asp:Label>

                            </td>
                            <td align="right" nowrap="nowrap">发证日期：
                            </td>
                            <td align="left" colspan="2">
                                <asp:Label ID="LabelValidStartDateMore" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr class="GridLightBK">

                            <td align="right" nowrap="nowrap">有效期至：
                            </td>
                            <td colspan="2" align="left">
                                <asp:Label ID="LabelValidEndDate" runat="server" Text=""></asp:Label>

                            </td>
                            <td align="right" nowrap="nowrap">有效期至：
                            </td>
                            <td align="left" colspan="2">
                                <asp:Label ID="LabelValidEndDateMore" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <%--<tr class="GridLightBK">

                            <td align="right" nowrap="nowrap">审核状态：
                            </td>
                            <td align="left" colspan="2">
                                <asp:Label ID="LabelApplyStatus" runat="server" Text=""></asp:Label>

                            </td>
                            <td align="right" nowrap="nowrap">审核结论：
                            </td>
                            <td align="left" colspan="2">
                                <asp:Label ID="LabelCheckAdvise" runat="server" Text=""></asp:Label>

                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td align="right" nowrap="nowrap">审核时间：
                            </td>
                            <td align="left" colspan="2">
                                <asp:Label ID="LabelCheckDate" runat="server" Text=""></asp:Label>

                            </td>
                            <td align="right" nowrap="nowrap">审核人：
                            </td>
                            <td align="left" colspan="2">
                                <asp:Label ID="LabelCheckMan" runat="server" Text=""></asp:Label>

                            </td>
                        </tr>--%>
                    </table>
                    <div id="divCheckHistory" runat="server" style="width: 99%; padding-top: 20px; text-align: center; clear: both;">
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
                    <table id="TableJWCheck" runat="server" visible="false" border="0" cellpadding="5" cellspacing="1" width="99%" class="table" style="text-align: center; margin: 10px auto">
                        <tr class="GridLightBK">
                            <td colspan="2" class="barTitle">增发审核</td>
                        </tr>
                        <tr class="GridLightBK">
                            <td width="20%" align="right">处理结果：</td>
                            <td width="80%" align="left">
                                <asp:RadioButtonList ID="RadioButtonListJWCheck" runat="server" RepeatDirection="Vertical" TextAlign="right">
                                    <asp:ListItem Text="通过" Value="通过" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="不通过" Value="不通过"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td width="20%" align="right">处理意见：</td>
                            <td width="80%" align="left">

                                <asp:TextBox ID="TextBoxCheckResult" runat="server" TextMode="MultiLine" Rows="3" Width="99%" MaxLength="100" Text="通过"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td colspan="2" align="center">
                                <asp:Button ID="ButtonCheck" runat="server" CssClass="bt_large" Text="确 定" OnClick="ButtonCheck_Click" />&nbsp;&nbsp;
                                         
                            </td>
                        </tr>
                    </table>
                    <table id="TableJWConfirm" runat="server" visible="false" border="0" cellpadding="5" cellspacing="1" width="99%" class="table" style="text-align: center; margin: 10px auto">
                        <tr class="GridLightBK">
                            <td colspan="2" class="barTitle">增发决定</td>
                        </tr>
                        <tr class="GridLightBK">
                            <td width="20%" align="right">处理结果：</td>
                            <td width="80%" align="left">
                                <asp:RadioButtonList ID="RadioButtonListJWConfirm" runat="server" RepeatDirection="Vertical" TextAlign="right">
                                    <asp:ListItem Text="通过" Value="通过" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="不通过" Value="不通过"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td width="20%" align="right">处理意见：</td>
                            <td width="80%" align="left">

                                <asp:TextBox ID="TextBoxConfirmResult" runat="server" TextMode="MultiLine" Rows="3" Width="99%" MaxLength="100" Text="通过"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td colspan="2" align="center">
                                <asp:CheckBox ID="CheckBoxAutoConfirm" runat="server" Text="后续申请都按照此意见自动审批处理" TextAlign="Left" Visible="false" />&nbsp;&nbsp;
                                <asp:Button ID="ButtonConfirm" runat="server" CssClass="bt_large" Text="确 定" OnClick="ButtonConfirm_Click" OnClientClick="if(JWSubmitTip()==false) return false;" />&nbsp;&nbsp;
                                         
                            </td>
                        </tr>
                    </table>
                    <%-- <table style="width: 100%; padding-bottom: 20px;">
                        <tr>
                            <td align="center" colspan="2">
                                <asp:Button ID="ButtonSave" Text='审核通过(发证)' runat="server" CssClass="bt_large" OnClick="ButtonSave_Click"></asp:Button>&nbsp;
                                <asp:Button ID="ButtonDelete" Text='审核不通过' runat="server" CssClass="bt_large" OnClick="ButtonDelete_Click"
                                    Visible="false"></asp:Button>&nbsp;
                               
                             <asp:Button ID="ButtonRreturn" runat="server" Text="返 回" CssClass="bt_large" OnClick="ButtonRreturn_Click" />

                                <br />
                            </td>
                        </tr>
                    </table>--%>
                </div>

                <div id="divImg" style="width: 32%; float: left; clear: right; margin-left: 1%; overflow: auto; border: 1px solid #cccccc; margin-bottom: 200px">
                    <telerik:RadGrid ID="RadGridFile" runat="server"
                        GridLines="None" AllowPaging="false" AllowSorting="false" AutoGenerateColumns="False"
                        Width="100%" Skin="Default" EnableAjaxSkinRendering="false"
                        EnableEmbeddedSkins="false" OnItemDataBound="RadGridFile_ItemDataBound">
                        <ClientSettings EnableRowHoverStyle="false">
                        </ClientSettings>
                        <MasterTableView NoMasterRecordsText=" 没有相关附件" GridLines="None" CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Left"
                            DataKeyNames="ApplyID,FileName,FileUrl">
                            <Columns>
                                <telerik:GridTemplateColumn UniqueName="ApplyFile" HeaderText="附件">
                                    <ItemTemplate>
                                        <div class="DivTitleOn" onclick="DivOnOff(this,'Div<%# Eval("DataType") %>',event);" title="折叠">
                                            <%# Eval("DataType") %>
                                        </div>
                                        <div class="DivContent" id="Div<%# Eval("DataType") %>" style="position: relative;">
                                            <telerik:RadGrid ID="RadGrid1" runat="server" ShowHeader="false"
                                                GridLines="None" AllowPaging="false" AllowSorting="false" AutoGenerateColumns="False"
                                                Width="100%" Skin="Default" EnableAjaxSkinRendering="false"
                                                EnableEmbeddedSkins="false">
                                                <ClientSettings EnableRowHoverStyle="false">
                                                </ClientSettings>
                                                <MasterTableView NoMasterRecordsText="" GridLines="None" CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Left"
                                                    DataKeyNames="ApplyID,FileID">
                                                    <Columns>
                                                        <telerik:GridTemplateColumn UniqueName="ApplyFile" HeaderText="附件">
                                                            <ItemTemplate>
                                                                <img class="img200" alt="图片" src='<%# ZYRYJG.UIHelp.ShowFile(Eval("FileUrl").ToString())%>' />

                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" Height="30px" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </telerik:GridTemplateColumn>

                                                    </Columns>
                                                    <HeaderStyle BackColor="#E4E4E4" Height="22px" Font-Bold="true" />
                                                </MasterTableView>
                                            </telerik:RadGrid>
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" Height="30px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridTemplateColumn>
                            </Columns>
                            <HeaderStyle BackColor="#E4E4E4" Height="22px" Font-Bold="true" />
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
            </div>
        </div>
    </div>
    <uc4:IframeView ID="IframeView" runat="server" />
    <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Enabled="false" Interval="2000"
        EnableViewState="true">
    </asp:Timer>
</asp:Content>
