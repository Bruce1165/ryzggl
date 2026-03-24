<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="ExamSignView.aspx.cs" Inherits="ZYRYJG.EXamManage.ExamSignView" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head2" runat="server">
    <title></title>
    <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" />
    <link href="../Skins/Blue/Grid.Blue.css?v=1.008" rel="stylesheet" type="text/css" />
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

                    //建委审核结果
                    $("#<%= RadioButtonListJWCheck.ClientID%> input").each(function () {
                        $(this).click(function () {
                            var TextBoxJWCheckResult = $("#<%= TextBoxJWCheckResult.ClientID%>");
                            var tabke_Lock = $("#<%= tabke_Lock.ClientID%>");
                            if ($(this).val() == "通过") {

                                TextBoxJWCheckResult.val("通过");
                                tabke_Lock.hide();
                            }
                            else {

                                TextBoxJWCheckResult.val("退回个人");
                                tabke_Lock.show();
                            }
                        });

                    });
                    //建委受理结果
                    $("#<%= RadioButtonListJWAccept.ClientID%> input").each(function () {
                        $(this).click(function () {
                            var TextBoxJWAcceptResult = $("#<%= TextBoxJWAcceptResult.ClientID%>");
                            var tabkeAccept_Lock = $("#<%= tabkeAccept_Lock.ClientID%>");
                            if ($(this).val() == "通过") {

                                TextBoxJWAcceptResult.val("通过");
                                tabkeAccept_Lock.hide();
                            }
                            else {

                                TextBoxJWAcceptResult.val("退回个人");
                                tabkeAccept_Lock.show();
                            }
                        });

                    });

                    //单位审核结果
                    $("#<%= RadioButtonListOldUnitCheckResult.ClientID%> input").each(function () {
                        $(this).click(unitClick);
                        <%--$(this).click(function () {
                            var TextBoxOldUnitCheckRemark = $("#<%= TextBoxOldUnitCheckRemark.ClientID%>");
                            alert($(this).val());
                            if ($(this).val() == "同意") {

                                TextBoxOldUnitCheckRemark.val("提交建委审核");
                            }
                            else if ($(this).val() == "通过") {

                                TextBoxOldUnitCheckRemark.val("通过");
                            }
                            else {

                                TextBoxOldUnitCheckRemark.val("退回个人");

                            }
                        });--%>

                    });
                    //建委审核确认结果
                    $("#<%= RadioButtonListPayConfirm.ClientID%> input").each(function () {
                        $(this).click(function () {
                            var TextBoxPayConfirmResult = $("#<%= TextBoxPayConfirmResult.ClientID%>");

                        if ($(this).val() == "通过") {
                            TextBoxPayConfirmResult.val("通过");
                        }
                        else {

                            TextBoxPayConfirmResult.val("退回个人");
                        }
                    });

                });
                });

                function unitClick() {
                    var TextBoxOldUnitCheckRemark = $("#<%= TextBoxOldUnitCheckRemark.ClientID%>");
                    tdUnitCheck = $("#<%= tdUnitCheck.ClientID%>");
                    if ($(this).val() == "同意" || $(this).val() == "通过") {

                        if (tdUnitCheck.text() == "培训点审核")
                            TextBoxOldUnitCheckRemark.val("通过");
                        else
                            TextBoxOldUnitCheckRemark.val("提交建委审核");
                    }
                    else {

                        TextBoxOldUnitCheckRemark.val("退回个人");

                    }
                }
               
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
                    当前位置 &gt;&gt; <strong>查看报名表</strong>
                </div>
            </div>
            <div style="text-align: left; padding-left: 40px; line-height: 200%">
                报名批号:<asp:Label ID="LabelSignUpCode" runat="server" Text=""></asp:Label>

            </div>
            <div runat="server" id="main" style="width: 100%; margin: 0; text-align: center; overflow: hidden;">
                <div style="width: 66%; float: left; clear: left" runat="server" id="divExamSignUp">
                    <table id="tableUnit" runat="server" border="0" cellpadding="5" cellspacing="1" class="table2" align="center" style="width: 100%">
                        <tr class="GridLightBK">
                            <td colspan="5" class="barTitle">所在单位信息</td>
                        </tr>
                        <tr class="GridLightBK">
                            <td nowrap="nowrap" align="right" colspan="2" width="35%">
                                <span style="color: Red">* </span>社会统一信用代码/组织机构代码组织机构代码
                            </td>
                            <td colspan="3">
                                <telerik:RadTextBox ID="RadTextBoxUnitCode" runat="server" Width="82%" Skin="Default"
                                    MaxLength="9">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td nowrap="nowrap" align="right" colspan="2">单位名称（全称）
                            </td>
                            <td colspan="3">
                                <telerik:RadTextBox ID="RadTextBoxUnitName" runat="server" Width="90%" Skin="Default">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr class="GridLightBK" runat="server" id="trJob" visible="false">
                            <td nowrap="nowrap" align="right" colspan="2">职务
                            </td>
                            <td colspan="3">
                                <asp:Label ID="LabelJob" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td nowrap="nowrap" align="right" colspan="2">合同类型
                            </td>
                            <td colspan="3">
                                <asp:Label ID="LabelENT_ContractType" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td nowrap="nowrap" align="right" colspan="2">合同时间
                            </td>
                            <td colspan="3">
                                <asp:Label ID="LabelENT_ContractStartTime" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table id="tableTrainUnit" runat="server" border="0" cellpadding="5" cellspacing="1" class="table2" align="center" style="width: 100%" visible="false">
                        <tr class="GridLightBK">
                            <td colspan="5" class="barTitle">培训考核机构</td>
                        </tr>
                        <tr class="GridLightBK">
                            <td width="10%" nowrap="nowrap" align="right"></td>
                            <td nowrap="nowrap" align="left" colspan="4">
                                <asp:Label ID="LabelTrainUnit" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table border="0" cellpadding="5" cellspacing="1" class="table2" align="center" style="width: 100%">
                        <tr class="GridLightBK">
                            <td colspan="5" class="barTitle">人员基本信息</td>
                        </tr>
                        <tr class="GridLightBK">
                            <td width="15%" nowrap="nowrap" align="right">证件类别
                            </td>
                            <td width="20%">
                                <asp:Label ID="LabelCertificateType" runat="server" Text=""></asp:Label>

                            </td>
                            <td width="15%" align="right">证件号码
                            </td>
                            <td>
                                <telerik:RadTextBox ID="RadTextBoxCertificateCode" runat="server" Width="95%" Skin="Default">
                                </telerik:RadTextBox>
                            </td>
                            <td width="110px" rowspan="3" align="center">
                                <img id="ImgCode" runat="server" height="140" width="110" alt="一寸照片" />
                            </td>
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
                            <td align="right" nowrap="nowrap">政治面貌
                            </td>
                            <td colspan="2">
                                <telerik:RadTextBox ID="RadTextBoxPoliticalBackground" runat="server" Width="95%"
                                    Skin="Default">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td align="right">技术职称<br />
                                或技术等级
                            </td>
                            <td>
                                <telerik:RadTextBox ID="RadTextBoxSKILLLEVEL" runat="server" Width="95%" Skin="Default">
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
                            <td colspan="5" class="barTitle">报名信息</td>
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
                            <td colspan="4">
                                <telerik:RadTextBox ID="RadTextBoxPostName" runat="server" Width="95%" Skin="Default"
                                    ReadOnly="true">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td nowrap="nowrap" align="right">从事本岗位工作的时间
                            </td>
                            <td>
                                <asp:Label ID="LabelWorkStartDate" runat="server" Text=""></asp:Label>

                            </td>
                            <td nowrap="nowrap" align="right">从事工作年限
                            </td>
                            <td colspan="2">
                                <telerik:RadNumericTextBox ID="RadNumericTextBoxWorkYearNumer" runat="server" MaxLength="3"
                                    Type="Number" NumberFormat-DecimalDigits="0" ShowSpinButtons="false" Width="95%"
                                    MinValue="0">
                                    <NumberFormat DecimalDigits="0"></NumberFormat>
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td colspan="5">
                                <div style="line-height: 150%; text-align: left; padding-left: 20px" id="divCheckPlan" runat="server"></div>

                                <asp:HiddenField ID="HiddenFieldApplyMan" runat="server" />
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td colspan="5" class="barTitle"><span style="color: Red">* </span>个人简历</td>
                        </tr>
                        <tr class="GridLightBK">
                            <td colspan="5">
                                <telerik:RadTextBox ID="RadTextBoxPersonDetail" runat="server" Width="98%" Skin="Default" Font-Size="16px"
                                    TextMode="MultiLine" Height="60px">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr class="GridLightBK" id="trExamSafeTrainHead" runat="server" visible="false">
                            <td colspan="5" class="barTitle"><span style="color: Red">* </span>考前安全操作培训</td>
                        </tr>
                         <tr class="GridLightBK" id="trExamSafeTrain1" runat="server" visible="false">
                            <td align="right">培训方式
                            </td>
                            <td colspan="4"><asp:Label ID="LabelSafeTrainType" runat="server" Text=""></asp:Label> </td>
                        </tr>
                        <tr class="GridLightBK" id="trExamSafeTrain2" runat="server" visible="false">
                            <td align="right">培训机构名称
                            </td>
                            <td>
                                <telerik:RadTextBox ID="RadTextBoxSafeTrainUnit" runat="server" Width="95%" Skin="Default"
                                    MaxLength="128">
                                </telerik:RadTextBox>
                            </td>
                            <td align="right" nowrap="nowrap">办学许可证编号
                            </td>
                            <td colspan="2">
                                <telerik:RadTextBox ID="RadTextBoxSafeTrainUnitCode" runat="server" Width="95%" Skin="Default"
                                    MaxLength="64">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr class="GridLightBK" id="trExamSafeTrain3" runat="server" visible="false">
                            <td align="right">办学许可证有效期
                            </td>
                            <td> <asp:Label ID="LabelSafeTrainUnitValidEndDate" runat="server" Text=""></asp:Label>                                
                            </td>
                            <td align="right" nowrap="nowrap">办学许可证发证机关
                            </td>
                            <td colspan="2">
                                <telerik:RadTextBox ID="RadTextBoxSafeTrainUnitOfDept" runat="server" Width="95%" Skin="Default"
                                    MaxLength="128">
                                </telerik:RadTextBox>
                            </td>
                        </tr>
                        <tr class="GridLightBK" id="trExamSafeTrainDetail" runat="server" visible="false">
                            <td colspan="5">
                                <telerik:RadGrid ID="RadGridAnQuanPX" runat="server" AutoGenerateColumns="False" AllowSorting="false" HeaderStyle-BorderColor="#eee"
                                    GridLines="None" CellPadding="0" Width="98%" Skin="Blue" EnableAjaxSkinRendering="false"
                                    EnableEmbeddedSkins="false">
                                    <ClientSettings EnableRowHoverStyle="false">
                                        <Selecting AllowRowSelect="false" />
                                    </ClientSettings>
                                    <MasterTableView  CommandItemDisplay="None" NoMasterRecordsText="没有可显示的记录">
                                        <Columns>
                                            <telerik:GridTemplateColumn HeaderText="培训时间" UniqueName="TrainDateStart">
                                                <ItemTemplate>
                                                    <%# string.Format("{0}至{1}",Eval("TrainDateStart")==DBNull.Value?"":Convert.ToDateTime(Eval("TrainDateStart")).ToString("yyyy-MM-dd")
                                                        ,Eval("TrainDateEnd")==DBNull.Value?"":Convert.ToDateTime(Eval("TrainDateEnd")).ToString("yyyy-MM-dd"))
                                                    %>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn UniqueName="TrainType" DataField="TrainType" HeaderText="培训类型">
                                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="TrainName" DataField="TrainName" HeaderText="培训内容">
                                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="TrainWay" DataField="TrainWay" HeaderText="培训方式">
                                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="Period" DataField="Period" HeaderText="学时">
                                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                        <HeaderStyle Font-Bold="True" />
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td colspan="5" class="barTitle"><span style="color: Red">* </span>本人承诺</td>
                        </tr>
                        <tr class="GridLightBK">
                            <td colspan="5">
                                <asp:CheckBox ID="CheckBoxPromise" runat="server" Enabled="false" />
                                本人承诺所提供的个人信息和证明材料真实准确, 对因提供有关信息, 证件不实或违反有关规定造成的后果, 责任自负。
                            </td>
                        </tr>
                        <tr id="trDataCheckHead" class="GridLightBK" runat="server">
                            <td colspan="5" class="barTitle">数据校验</td>
                        </tr>
                        <tr id="trDataChcekRow" class="GridLightBK" runat="server">
                            <td colspan="5">
                                <div runat="server" id="divSheBao" style="margin: 0 auto; padding-left: 20px; text-align: left; line-height: 200%;"></div>
                                <div runat="server" id="divFR" style="margin: 0 auto; padding-left: 20px; text-align: left; line-height: 200%;"></div>
                                <div runat="server" id="divZACheckResult" style="margin: 0 auto; padding-left: 20px; text-align: left; line-height: 200%;"></div>

                            </td>
                        </tr>
                    </table>
                    <div id="divCheckHistory" runat="server" style="margin: 0 auto; padding-top: 20px; text-align: center; clear: both;">
                        <table id="Table1" runat="server" border="0" cellpadding="5" cellspacing="1" class="table" style="text-align: center; width: 100%; margin: 10px auto">
                            <tr class="GridLightBK">
                                <td class="barTitle">审办记录</td>
                            </tr>
                            <tr class="GridLightBK">
                                <td align="left" style="border-collapse: collapse;">
                                    <telerik:RadGrid ID="RadGridCheckHistory" runat="server" ShowHeader="true" CellPadding="0" CellSpacing="0"
                                        GridLines="None" AllowPaging="False" AllowSorting="False" AutoGenerateColumns="False"
                                        Width="100%" EnableEmbeddedSkins="false" PagerStyle-AlwaysVisible="False" Skin="Default">
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
                                                <telerik:GridBoundColumn HeaderText="办理时间" UniqueName="ActionData" DataField="ActionData">
                                                        <ItemStyle Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="办理结果" UniqueName="ActionResult" DataField="ActionResult">
                                                        <ItemStyle Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="办理意见" UniqueName="ActionRemark" DataField="ActionRemark">
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                            <HeaderStyle Font-Bold="True" BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1" Wrap="false" />
                                            <ItemStyle CssClass="subtable" Font-Size="16px" />
                                            <AlternatingItemStyle CssClass="subtable" Font-Size="16px" />
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="divUnit" visible="false" runat="server" style="padding-top: 20px; text-align: center; clear: both;" class="auto-style3">
                         <div class="DivContent" >
                           <div id="divPostType1Tip" runat="server" visible="false" >
                                <p class="bold">重要提示：</p>
	                            <p style="padding-left:64px">本次业务申请最终审批结果以住房和城乡建设部生成电子证书为准，在未生成电子证书前，申请人应满足以下条件：<br />
	                            1.申请法人A本证书（职务为法定代表人）的，应为该单位营业执照上载明的法定代表人。<br />
                                2.不允许持有多本非法人A本（职务不是法定代表人）。<br />
	                            3.申请项目负责人B本证书的，其一、二级建造师注册单位应与其现聘用单位一致。<br />
	                            4.同时持有多本安管人员ABC证书的，其项目负责人B本、专职安管人员C本应与其一本法人A本证书工作单位一致。<br />
	                            5.同时持有安管人员BC本证书的，其项目负责人B本应与其专职安管人员C本证书工作单位一致。<br />
                                6.有C3不能再报考C1和C2，有C1或C2不能报考C3。<br />
                                7.本次业务申请最终审批结果以住房和城乡建设部生成电子证书为准，在未生成电子证书前，请不要办理营业执照法定代表人变更或一、二级建造师变更、重新、注销注册等业务，以免无法正常生成电子证书，影响后续业务办理。<br />
	                            </p>                              
                            </div> 
                        </div>
                        <table id="Table6" runat="server" width="100%" border="0" cellpadding="5" cellspacing="1" class="table" style="text-align: center; margin: 10px auto">
                            <tr class="GridLightBK">
                                <td colspan="2" class="barTitle" runat="server" id="tdUnitCheck">单位审核</td>
                            </tr>
                            <tr class="GridLightBK">
                                <td width="20%" align="right">处理结果：</td>
                                <td width="80%" align="left">
                                    <asp:RadioButtonList ID="RadioButtonListOldUnitCheckResult" runat="server" RepeatDirection="Vertical" TextAlign="right">
                                        <asp:ListItem Text="同意" Value="同意" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="不同意" Value="不同意"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td width="20%" align="right">处理意见：</td>
                                <td width="80%" align="left">

                                    <asp:TextBox ID="TextBoxOldUnitCheckRemark" runat="server" TextMode="MultiLine" Rows="3" Width="99%" MaxLength="100" Text="提交建委审核"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td colspan="2" align="center">
                                    <asp:Button ID="ButtonUnitCheck" runat="server" CssClass="bt_large" Text="确 定" OnClick="ButtonUnitCheck_Click" />&nbsp;&nbsp;
                                         
                                </td>
                            </tr>
                        </table>
                        <div id="DivExamConfirm" runat="server" style="line-height: 30px; width: 680px; display: none; margin-bottom: 30px; position: relative; top: -300px; left: 150px; background-color: #dedede; padding: 20px 50px 50px 50px; border-left: 4px solid #eee; border-top: 4px solid #eee; border-right: 4px solid #999; border-bottom: 4px solid #999; color: #000">
                            <p style="font-size: 30px; font-weight: bold; text-align: center;">系统提示</p>
                            <p style="text-align: left;" id="p_ExamConvfirmDesc" runat="server">
                            </p>
                            <p style="text-align: center;">
                                <asp:Button ID="ButtonYes" runat="server" Text="是" CssClass="button  btn_no" OnClick="ButtonYes_Click" CausesValidation="false" Enabled="false" />
                                &nbsp;&nbsp; 
                <asp:Button ID="ButtonNo" runat="server" Text="否" CssClass="button  btn_no" OnClick="ButtonNo_Click" CausesValidation="false" Enabled="false" />
                                <span style="padding-left: 20px; font-size: 30px; color: red; font-weight: bold" id="spanCount">8</span>
                            </p>
                        </div>
                    </div>
                    <div id="divCancel" visible="false" runat="server" style="padding-top: 20px; text-align: center; clear: both;">
                        <asp:Button ID="ButtonCancel" runat="server" Text="取消申报" CssClass="bt_large" OnClick="ButtonCancel_Click" OnClientClick="javascript:if(confirm('取消申报将退回到个人，确认要取消申报吗？')==false) return false;" />
                    </div>
                    <table id="TableJWAccept" runat="server" visible="false" border="0" cellpadding="5" cellspacing="1" width="100%" class="table" style="text-align: center; margin: 10px auto">
                        <tr class="GridLightBK">
                            <td colspan="2" class="barTitle">报名受理</td>
                        </tr>
                        <tr class="GridLightBK">
                            <td width="20%" align="right">受理结果：</td>
                            <td width="80%" align="left">
                                <asp:RadioButtonList ID="RadioButtonListJWAccept" runat="server" RepeatDirection="Vertical" TextAlign="right">
                                    <asp:ListItem Text="通过" Value="通过" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="不通过" Value="不通过"></asp:ListItem>
                                </asp:RadioButtonList>

                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td width="20%" align="right" valign="top">受理意见：</td>
                            <td width="80%" align="left">
                                <asp:TextBox ID="TextBoxJWAcceptResult" runat="server" TextMode="MultiLine" Rows="3" Width="99%" MaxLength="1000" Text="通过"></asp:TextBox>
                                <table id="tabkeAccept_Lock" width="100%" runat="server" style="display: none;">
                                    <tr class="GridLightBK">
                                        <td align="left">
                                            <asp:CheckBox ID="CheckBoxAcceptLock" runat="server" Text="加入违规申报人员库（锁定一年，不允许报名）" /><asp:Label ID="LabelAcceptLockTimeSpan" runat="server" Text=""></asp:Label></td>
                                    </tr>
                                    <tr class="GridLightBK">
                                        <td align="left">锁定原因：</td>
                                    </tr>
                                    <tr class="GridLightBK">
                                        <td align="left">
                                            <asp:TextBox ID="TextBoxAcceptLockReasion" runat="server" TextMode="MultiLine" Rows="3" Width="99%" MaxLength="2000" Text=""></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td colspan="2" align="center" style="padding-bottom: 80px">
                                <asp:Button ID="ButtonAccept" runat="server" CssClass="bt_large" Text="确 定" OnClick="ButtonAccept_Click" />&nbsp;&nbsp;
                                         
                            </td>
                        </tr>
                    </table>
                    <table id="TableJWCheck" runat="server" visible="false" border="0" cellpadding="5" cellspacing="1" width="100%" class="table" style="text-align: center; margin: 10px auto">
                        <tr class="GridLightBK">
                            <td colspan="2" class="barTitle">报名审核</td>
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
                            <td width="20%" align="right" valign="top">处理意见：</td>
                            <td width="80%" align="left">
                                <asp:TextBox ID="TextBoxJWCheckResult" runat="server" TextMode="MultiLine" Rows="3" Width="99%" MaxLength="1000" Text="通过"></asp:TextBox>
                                <table id="tabke_Lock" width="100%" runat="server" style="display: none;">
                                    <tr class="GridLightBK">
                                        <td align="left">
                                            <asp:CheckBox ID="CheckBoxLock" runat="server" Text="加入违规申报人员库（锁定一年，不允许报名）" /><asp:Label ID="LabelLockTimeSpan" runat="server" Text=""></asp:Label></td>
                                    </tr>
                                    <tr class="GridLightBK">
                                        <td align="left">锁定原因：</td>
                                    </tr>
                                    <tr class="GridLightBK">
                                        <td align="left">
                                            <asp:TextBox ID="TextBoxLockReasion" runat="server" TextMode="MultiLine" Rows="3" Width="99%" MaxLength="2000" Text=""></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td colspan="2" align="center" style="padding-bottom: 80px">
                                <asp:Button ID="ButtonCheck" runat="server" CssClass="bt_large" Text="确 定" OnClick="ButtonCheck_Click" />&nbsp;&nbsp;
                                         
                            </td>
                        </tr>
                    </table>
                    <table id="TablePayConfirm" runat="server" visible="false" border="0" cellpadding="5" cellspacing="1" width="100%" class="table" style="text-align: center; margin: 10px auto">
                        <tr class="GridLightBK">
                            <td colspan="2" class="barTitle">报名审核确认</td>
                        </tr>
                        <tr class="GridLightBK">
                            <td width="20%" align="right">审核确认结果：</td>
                            <td width="80%" align="left">
                                <asp:RadioButtonList ID="RadioButtonListPayConfirm" runat="server" RepeatDirection="Vertical" TextAlign="right">
                                    <asp:ListItem Text="通过" Value="通过" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="不通过" Value="不通过"></asp:ListItem>
                                </asp:RadioButtonList>

                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td width="20%" align="right" valign="top">审核确认意见：</td>
                            <td width="80%" align="left">
                                <asp:TextBox ID="TextBoxPayConfirmResult" runat="server" TextMode="MultiLine" Rows="3" Width="99%" MaxLength="1000"  Text="通过"></asp:TextBox>

                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td colspan="2" align="center" style="padding-bottom: 80px">
                                <asp:Button ID="ButtonPayConfirmRult" runat="server" CssClass="bt_large" Text="确 定" OnClick="ButtonPayConfirmRult_Click" />&nbsp;&nbsp;
                                         
                            </td>
                        </tr>
                    </table>
                    <br />
                </div>
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

