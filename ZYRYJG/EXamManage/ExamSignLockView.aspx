<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="ExamSignLockView.aspx.cs" Inherits="ZYRYJG.EXamManage.ExamSignLockView" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head2" runat="server">
    <title></title>
    <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" />
    <link href="../layer/skin/layer.css" rel="stylesheet" />
    <script src="../Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
    <script src="../layer/layer.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager2" runat="server">
        </telerik:RadScriptManager>
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

                });
            </script>
        </telerik:RadCodeBlock>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true"
            Skin="Windows7" />
        <telerik:RadWindowManager ID="RadWindowManager1" ShowContentDuringLoad="false" VisibleStatusbar="false"
            ReloadOnShow="true" runat="server" Skin="Windows7" EnableShadow="true">
        </telerik:RadWindowManager>
        <div class="div_out">
            <div id="div_top" class="dqts">
                <div style="float: left;">
                    当前位置 &gt;&gt; 考务管理 &gt;&gt;报名管理 &gt;&gt;<strong>违规申报锁定管理</strong>
                </div>
            </div>
            <div style="text-align: left; padding-left: 40px; line-height: 200%">
                报名批号:<asp:Label ID="LabelSignUpCode" runat="server" Text=""></asp:Label>

            </div>
            <div runat="server" id="main" style="width: 100%; margin: 0; text-align: center; overflow: hidden;">
                <%--    <div style="width: 66%; float: left; clear: left">--%>
                <div style="width: 66%; float: left; clear: left" runat="server" id="divExamSignUp">
                    <table border="0" cellpadding="5" cellspacing="1" class="table2" align="center">
                        <tr class="GridLightBK">
                            <td colspan="5" class="barTitle">报名信息</td>
                        </tr>
                        <tr class="GridLightBK">
                            <td nowrap="nowrap" align="right">姓名
                            </td>
                            <td>
                                <telerik:RadTextBox ID="RadTextBoxWorkerName" runat="server" Width="80%" Skin="Default">
                                </telerik:RadTextBox>
                                <asp:HiddenField ID="HiddenFieldPhone" runat="server" />
                                <asp:HiddenField ID="HiddenFieldBirthday" runat="server" />
                            </td>
                            <td align="right">性别
                            </td>
                            <td>
                                <asp:Label ID="LabelS_Sex" runat="server" Text=""></asp:Label>

                            </td>
                            <td width="110px" rowspan="3" align="center">
                                <img id="ImgCode" runat="server" height="140" width="110" alt="一寸照片" />
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td width="15%" nowrap="nowrap" align="right">证件类别
                            </td>
                            <td width="30%">
                                <asp:Label ID="LabelCertificateType" runat="server" Text=""></asp:Label>

                            </td>
                            <td width="15%" align="right">证件号码
                            </td>
                            <td>
                                <telerik:RadTextBox ID="RadTextBoxCertificateCode" runat="server" Width="95%" Skin="Default">
                                </telerik:RadTextBox>
                            </td>

                        </tr>

                        <tr class="GridLightBK">
                            <td align="right">出生日期
                            </td>
                            <td>
                                <asp:Label ID="LabelS_Birthday" runat="server" Text=""></asp:Label>

                            </td>
                            <td align="right">民族
                            </td>
                            <td>
                                <telerik:RadTextBox ID="RadTextBoxNation" runat="server" Width="95%" Skin="Default">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td nowrap="nowrap" align="right">文化程度
                            </td>
                            <td>
                                <telerik:RadTextBox ID="RadTextBoxS_CulturalLevel" runat="server" Width="95%" Skin="Default">
                                </telerik:RadTextBox>
                            </td>
                            <td align="right" nowrap="nowrap">
                                <span style="color: Red">* </span>联系电话
                            </td>
                            <td colspan="2">
                                <telerik:RadTextBox ID="RadTextBoxS_Phone" runat="server" Width="95%" Skin="Default">
                                </telerik:RadTextBox>
                            </td>
                        </tr>


                        <tr class="GridLightBK">
                            <td nowrap="nowrap" align="right" colspan="1">单位名称（全称）
                            </td>
                            <td colspan="4">
                                <telerik:RadTextBox ID="RadTextBoxUnitName" runat="server" Width="90%" Skin="Default">
                                </telerik:RadTextBox>
                            </td>
                        </tr>

                        <tr class="GridLightBK">
                            <td nowrap="nowrap" align="right">考试计划
                            </td>
                            <td colspan="4">
                                <telerik:RadTextBox ID="RadTextBoxExamPlanName" runat="server" Width="95%" Skin="Default"
                                    ReadOnly="true">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td nowrap="nowrap" align="right">申报岗位工种
                            </td>
                            <td >
                                <telerik:RadTextBox ID="RadTextBoxPostName" runat="server" Width="95%" Skin="Default"
                                    ReadOnly="true">
                                </telerik:RadTextBox>
                            </td>
                       
                            <td nowrap="nowrap" align="right">考试时间
                            </td>
                            <td colspan="2">
                                <asp:Label ID="LabelExamStart" runat="server" Text=""></asp:Label>
                                
                            </td>
                        </tr>
                          <tr class="GridLightBK">
                            <td colspan="5" class="barTitle"><span style="color: Red">* </span>锁定信息</td>
                        </tr>
                        <tr class="GridLightBK">
                            <td nowrap="nowrap" align="right">锁定日期
                            </td>
                            <td>
                                <asp:Label ID="LabelLockTime" runat="server" Text=""></asp:Label>

                            </td>
                            <td nowrap="nowrap" align="right">解锁日期
                            </td>
                            <td colspan="2">
                                 <telerik:RadDatePicker ID="RadDatePickerLockEndTime" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy-MM-dd"
                                        Width="98%" />
                            </td>
                        </tr>
                       <tr class="GridLightBK">
                            <td colspan="5" style="text-align:left">
                                锁定原因：
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td colspan="5">
                                <telerik:RadTextBox ID="RadTextBoxLockReason" runat="server" Width="98%" Skin="Default" Font-Size="16px"
                                    TextMode="MultiLine" Height="60px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td colspan="5">
                                <asp:Button ID="btnSave" runat="server" Text="保 存" CssClass="button" OnClick="btnSave_Click" />
                            </td>
                        </tr>
                    </table>          
                    <br />
                </div>
                <%-- </div>--%>
                <div id="divImg" style="width: 32%; float: left; clear: right; margin-left: 1%; overflow: auto; border: 1px solid #cccccc; margin-bottom: 200px">
                    <telerik:RadGrid ID="RadGridFile" runat="server"
                        GridLines="None" AllowPaging="false" AllowSorting="false" AutoGenerateColumns="False"
                        Width="100%" Skin="Default" EnableAjaxSkinRendering="false" PagerStyle-AlwaysVisible="true"
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
        <uc4:IframeView ID="IframeView" runat="server" />
    </form>
</body>
</html>

