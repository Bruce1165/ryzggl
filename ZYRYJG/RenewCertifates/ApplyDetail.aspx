<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="ApplyDetail.aspx.cs" Inherits="ZYRYJG.RenewCertifates.ApplyDetail" %>


<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc4" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" />
    <link href="../Skins/Blue/Grid.Blue.css?v=1.008" rel="stylesheet" type="text/css" />
    <link href="../layer/skin/layer.css" rel="stylesheet" />
    <link href="../Skins/Hot/Upload.hot.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/Public.js?v=1.0.1" type="text/javascript"></script>
    <script src="../layer/layer.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>

        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <ClientEvents OnRequestStart="onRequestStart" />
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="divmain">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="divmain" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true"
            Skin="Windows7" />
        <telerik:RadWindowManager ID="RadWindowManager1" ShowContentDuringLoad="false" VisibleStatusbar="false"
            ReloadOnShow="true" runat="server" Skin="Windows7" EnableShadow="true">
        </telerik:RadWindowManager>
        <style type="text/css">
            .disenable {
                cursor: not-allowed !important;
            }

            .tablewhite td {
                border: none;
                line-height: 150%;
            }

            .zhifa {
                margin: 12px 12px;
                /*border:1px solid #ddd;
                border-radius:12px 12px;*/
                border: none;
                background-color: #fcfefc;
            }

                .zhifa .rr {
                    text-align: right;
                    background-color: #f6f6f6;
                }

                .zhifa .rl {
                    text-align: left;
                }

                .zhifa .nr {
                    padding: 20px 20px;
                    line-height: 180%;
                    color: red;
                }

                .zhifa p {
                    text-indent: 32px;
                }
        </style>
        <div class="div_out" >
            <div id="div_top" class="dqts">
                <div style="float: left;">
                    当前位置 &gt;&gt; 证书续期 &gt;&gt;
                <strong>续期申请</strong>
                </div>
            </div>

            <div class="table_border" style="margin: 5px auto;" runat="server" id="divmain">
                <div class="step">
                    <div class="stepLabel">办理进度：</div>
                    <div id="step_填报中" runat="server" class="stepItem lgray">个人填写></div>
                    <div id="step_待单位确认" runat="server" class="stepItem lgray">待单位确认></div>
                    <div id="step_已申请" runat="server" class="stepItem lgray">单位申报></div>
                    <div id="step_已初审" runat="server" class="stepItem lgray">区县（或集团）初审></div>
                    <div id="step_已上报" runat="server" class="stepItem lgray">区县（或集团）汇总上报></div>
                    <div id="step_已审查" runat="server" class="stepItem lgray">市级审核></div>
                    <div id="step_已决定" runat="server" class="stepItem lgray">市级决定></div>
                    <div id="step_已办结" runat="server" class="stepItem lgray">住建部生成电子证书（办结，下载电子证书）</div>
                    <div class="stepArrow">▶</div>
                </div>
                <div class="DivTitleOn" onclick="DivOnOff(this,'Td3',event);" title="折叠">
                    申请说明
                </div>
                <div class="DivContent" id="Td3">
                    <div id="spanTZZY" runat="server" visible="false">
                        自2023年8月2日起，建筑施工特种作业操作资格证书延期复核工作按<a title="京建发〔2024〕317号" href="http://120.52.185.14/Register/NewsView.aspx?o=Bcw6peY5SnhZ9ieYD1atEAd70CSdVzLXJb/+LkwNP1y2nwbD8Q489w==" target="_blank" style="color: Blue; text-decoration: underline;">《关于加强北京市建筑施工特种作业操作资格证书延期复核管理工作的通知》（京建发〔2023〕262号）</a>的有关规定执行。<br />
                        <div style="margin-left: 32px">
                            1、办理流程：个人申请 > 聘用单位确认 > 市住建委审核决定；在市住建委审核前，申请人可以进入详细页面进行取消申请操作。<br />
                            2、个人申请时间：申请人应在证书有效期届满前90日至有效期届满当日提交延期复核申请；未按规定时间提交延期复核申请或不符合延期复核条件的，证书自动失效。<br />
                            3、聘用单位确认时间：聘用单位应在证书有效期届满前，对个人申请进行初审确认。<br />
                            4、安全生产教育：<br />
                            <div style="margin-left: 32px">
                                ①申请人应当完成所在聘用单位组织的每年不少于24学时的年度安全生产教育培训并考核合格；<br />
                                ②申请人应当在证书有效期届满前，进入<a href="../jxjy/MyTrain.aspx?o=grkj" target="_blank">建设行业从业人员公益培训平台</a>，完成市住建委组织的不少于8学时的继续教育并考核合格。
                            </div>
                        </div>
                    </div>
                    <div id="spanSLR" runat="server" visible="false">
                        自2024年10月1日起，建筑施工企业“安管人员”安全生产考核合格证书延续工作按<a title="京建发〔2024〕317号" href="http://120.52.185.14/Register/NewsView.aspx?o=wTJcVcrB6B/GAR/1ZZEdCKq4E8t+fbKfe31wp4dKRjErdnx5/9lvpQ==" target="_blank" style="color: Blue; text-decoration: underline;">《关于加强北京市建筑施工企业主要负责人、项目负责人和专职安全生产管理人员安全生产考核合格证书延续管理工作的通知》（京建发〔2024〕317号）</a>的有关规定执行。<br />
                        <div style="margin-left: 32px">
                            1、办理流程：个人申请 > 聘用单位确认 > 各区住建委等初审单位审核 > 市住建委复核决定；在各区住建委等初审单位审核前，申请人可以进入详细页面进行取消申请操作。<br />
                            2、个人申请时间：申请人应在证书有效期届满前90日至有效期届满当日提交延续申请；未按规定时间提交延续申请或不符合延续条件的，证书自动失效。<br />
                            3、聘用单位确认时间：聘用单位应在证书有效期届满前，对个人申请进行初审确认。<br />
                            4、安全生产教育：<br />
                            <div style="margin-left: 32px">
                                ①申请人应当完成所在聘用单位组织的每年不少于20学时的企业年度安全生产教育和培训并考核合格；<br />
                                ②申请人应当在证书有效期届满前，完成<a href="../Template/各区县对外办公电话.htm?v=1.0" target="_blank" style="text-decoration: underline;">&nbsp;&nbsp;各区住建委等初审单位&nbsp;&nbsp;</a>组织的不少于16学时、以现场面授为主的证书延续继续教育并考核合格；<br />
                                ③申请人应当在证书有效期届满前，进入<a href="../jxjy/MyTrain.aspx?o=grkj" target="_blank">建设行业从业人员公益培训平台</a>，完成市住建委组织的不少于8学时的证书延续专项继续教育并考核合格。
                            </div>
                        </div>
                    </div>
                </div>
                <div class="content" id="div_content">
                    <div id="DivEdit" runat="server" style="width: 100%; margin: 10px auto; text-align: center; overflow: hidden;">
                        <div style="width: 66%; float: left; clear: left">
                            <div style="float: left; padding-left: 30px; width: 40%;">
                                申请时间:<asp:Label ID="LabelApplyDate" runat="server" Text=""></asp:Label>
                            </div>
                            <div style="float: right; padding-right: 30px; width: 40%;">
                                申请批号:<asp:Label ID="LabelApplyCode" runat="server" Text=""></asp:Label>
                            </div>
                            <table border="0" cellpadding="5" cellspacing="1" class="table" align="center" style="margin: 10px 10px">
                                <tr class="GridLightBK">
                                    <td colspan="7" class="barTitle">申请表</td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td width="7%" nowrap="nowrap" align="center">姓名
                                    </td>
                                    <td width="31%">
                                        <asp:Label ID="LabelWorkerName" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td align="center" width="9%">
                                        <nobr>性别</nobr>
                                    </td>
                                    <td width="9%">
                                        <asp:Label ID="LabelSex" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td align="center" width="7%">出生日期
                                    </td>
                                    <td width="19%">
                                        <asp:Label ID="LabelBirthday" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td width="110px" rowspan="4" align="center">
                                        <img id="ImgCode" runat="server" height="140" width="110" src="~/Images/photo_ry.jpg"
                                            alt="一寸照片" />
                                    </td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td align="center">证件号码
                                    </td>
                                    <td colspan="3">
                                        <asp:Label ID="LabelWorkerCertificateCode" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td align="center" nowrap="nowrap">
                                        <span style="color: Red">* </span>联系电话
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="RadTextBoxPhone" runat="server" Width="120px" Skin="Default"
                                            MaxLength="50">
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorPhone" runat="server" ErrorMessage="必填"
                                            ControlToValidate="RadTextBoxPhone" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td nowrap="nowrap" align="center">岗位工种
                                    </td>
                                    <td colspan="3">
                                        <asp:Label ID="LabelPostName" runat="server" Text=""></asp:Label>
                                        <asp:HiddenField ID="HiddenFieldPostTypeName" runat="server" />
                                    </td>
                                    <td nowrap="nowrap" align="center">&nbsp;&nbsp;&nbsp;&nbsp;文化程度&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <asp:Label ID="LabelCulturalLevel" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr class="GridLightBK" id="TableUploadPhoto" runat="server">
                                    <td colspan="6">
                                        <table width="99%" class="tablewhite">
                                            <tr>
                                                <td style="padding-left: 20px; line-height: 20px; text-align: left" colspan="3">
                                                    <span style="color: Red">* </span><span id="SpanTip" runat="server" style="font-size: 16px;">附件-照片：（格式要求：近期彩色标准1寸，半身免冠证件照，照片底色背景为白色；一寸jpg格式图片，名称不限，最大为50K，宽高110 X 140像素）</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center">辅助工具下载：<a
                                                    href="../Images/1寸照片生成器.exe" style="text-decoration: underline; color: Blue;"><img
                                                        alt="" src="../Images/Soft_common.gif" style="border-width: 0;" />
                                                    1寸照片生成器.exe</a></td>
                                                <td align="right" style="font-size: 13px; font-weight: bold;">上传照片：
                                                </td>
                                                <td align="left" style="width: 120px">
                                                    <telerik:RadUpload ID="RadUploadFacePhoto" runat="server" ControlObjectsVisibility="None"
                                                        Height="23px" MaxFileInputsCount="1" OverwriteExistingFiles="True" Skin="Hot"
                                                        EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false" ReadOnlyFileInputs="False"
                                                        Width="300px" OnClientFileSelected="showImg">
                                                        <Localization Select="选 择" />
                                                    </telerik:RadUpload>
                                                </td>

                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td align="center">技术职称<br />
                                        或技术等级
                                    </td>
                                    <td colspan="3">
                                        <asp:Label ID="LabelSKILLLEVEL" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td nowrap="nowrap" align="center">从事本岗位<br />
                                        工作的时间
                                    </td>
                                    <td>
                                        <asp:Label ID="LabelWorkStartDate" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr class="GridLightBK" id="trSelectUnit" runat="server" style="display: none">
                                    <td nowrap="nowrap" align="right" colspan="7">选择一个聘用单位，并由聘用单位对续期申请进行审核确认。<input id="ButtonSelectQY" type="button" value="请选择一个单位" class="bt_maxlarge" onclick="javascript: selectQY();" />
                                    </td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td nowrap="nowrap" align="center">

                                        <asp:Label ID="LabelUnit" runat="server" Text="聘用单位名称："></asp:Label>
                                    </td>
                                    <td colspan="3">
                                        <telerik:RadTextBox ID="RadTextBoxUnitName" runat="server" Width="97%" Skin="Default"
                                            BackColor="Transparent" BorderStyle="None">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td nowrap="nowrap" align="center">
                                        <span style="color: Red">* </span>
                                        <asp:Label ID="LabelUnitCode" runat="server" Text=" 组织机构代码："></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <telerik:RadTextBox ID="RadTextBoxUnitCode" runat="server" Width="200px" Skin="Default" BorderStyle="None"
                                            MaxLength="18">
                                        </telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="必填"
                                            ControlToValidate="RadTextBoxUnitCode" Display="Dynamic"></asp:RequiredFieldValidator>

                                        <asp:HiddenField ID="HiddenFieldOldUnitCode" runat="server" />
                                    </td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td nowrap="nowrap" align="center">证书编号
                                    </td>
                                    <td colspan="6">
                                        <asp:Label ID="LabelCertificateCode" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td nowrap="nowrap" align="center">发证时间
                                    </td>
                                    <td colspan="3">
                                        <asp:Label ID="LabelConferDate" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td nowrap="nowrap" align="center">有效期至
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="LabelValidDataTo" runat="server" Text=""></asp:Label><asp:HiddenField
                                            ID="HiddenFieldApplyMan" runat="server" />
                                    </td>
                                </tr>
                                <tr class="GridLightBK" id="trGongYiPXTitle" runat="server" visible="false">
                                    <td colspan="7" class="barTitle">证书延期复核继续教育学习记录</td>
                                </tr>
                                <tr class="GridLightBK" id="trGongYiPX" runat="server" visible="false">
                                    <td colspan="7">
                                        <div id="div_GongYiPX" runat="server" style="color: red"></div>
                                    </td>
                                </tr>
                                <tr class="GridLightBK" id="trAnQuanPXTitle" runat="server" visible="false">
                                    <td colspan="7" class="barTitle">年度安全生产教育培训记录</td>
                                </tr>
                                <tr class="GridLightBK" id="trAnQuanPX" runat="server" visible="false">
                                    <td colspan="7" style="border-collapse: collapse">
                                        <div id="divAnQuanPXFinishCase" runat="server" style="color: red"></div>
                                        <telerik:RadGrid ID="RadGridAnQuanPX" runat="server" AutoGenerateColumns="False" AllowSorting="false" HeaderStyle-BorderColor="#eee"
                                            GridLines="None" CellPadding="0" Width="98%" Skin="Blue" EnableAjaxSkinRendering="false"
                                            EnableEmbeddedSkins="false" OnItemDataBound="RadGridAnQuanPX_ItemDataBound" OnInsertCommand="RadGridAnQuanPX_InsertCommand" OnUpdateCommand="RadGridAnQuanPX_UpdateCommand"
                                            OnDeleteCommand="RadGridAnQuanPX_DeleteCommand" OnNeedDataSource="RadGridAnQuanPX_NeedDataSource">
                                            <ClientSettings EnableRowHoverStyle="true">
                                                <Selecting AllowRowSelect="True" />
                                            </ClientSettings>
                                            <MasterTableView DataKeyNames="DetailID,BaseID" EditMode="EditForms" CommandItemDisplay="Top" NoMasterRecordsText="没有可显示的记录">
                                                <Columns>
                                                    <telerik:GridBoundColumn HeaderText="序号" UniqueName="DataNo" DataField="DataNo" AllowSorting="false">
                                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridTemplateColumn HeaderText="年度" UniqueName="year">
                                                        <ItemTemplate>
                                                            <%# Eval("TrainDateStart")==DBNull.Value?"": Convert.ToDateTime(Eval("TrainDateStart")).ToString("yyyy")  %>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="时间" UniqueName="TrainDateStart">
                                                        <ItemTemplate>
                                                            <%# string.Format("{0}至{1}",Eval("TrainDateStart")==DBNull.Value?"":Convert.ToDateTime(Eval("TrainDateStart")).ToString("yyyy-MM-dd")
                                                        ,Eval("TrainDateEnd")==DBNull.Value?"":Convert.ToDateTime(Eval("TrainDateEnd")).ToString("yyyy-MM-dd"))
                                                            %>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn UniqueName="TrainName" DataField="TrainName" HeaderText="培训内容">
                                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn UniqueName="TrainWay" DataField="TrainWay" HeaderText="培训方式">
                                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn UniqueName="TrainUnit" DataField="TrainUnit" HeaderText="培训单位">
                                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn UniqueName="Period" DataField="Period" HeaderText="学时">
                                                        <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn UniqueName="ExamResult" DataField="ExamResult" HeaderText="考核结果">
                                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridEditCommandColumn UniqueName="Edit" EditText="编辑" EditImageUrl="../Images/jia.gif" ButtonType="ImageButton">
                                                    </telerik:GridEditCommandColumn>
                                                    <telerik:GridButtonColumn UniqueName="Delete" CommandName="Delete" ConfirmText="您确定要删除么?" Text="&lt;span onclick=&quot;if(!confirm('您确定要删除么?'))return false; &quot; &gt;删除&lt;/span&gt;" ButtonType="ImageButton" ImageUrl="../Images/close.png">
                                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                                    </telerik:GridButtonColumn>
                                                </Columns>
                                                <CommandItemSettings AddNewRecordText="点击这里添加培训记录" ShowRefreshButton="false" />
                                                                                            
                                                <EditFormSettings InsertCaption="添加培训记录" CaptionFormatString="编辑培训记录"
                                                    EditFormType="Template" PopUpSettings-Modal="false" FormCaptionStyle-HorizontalAlign="Left">
                                                    <EditColumn UniqueName="EditCommandColumn1">
                                                    </EditColumn>
                                                    <FormTemplate>
                                                        <div id="DivEdit" runat="server" style="width: 100%; margin: 0 auto; background-color: #fefefe;">
                                                            <br />
                                                            <table class="bar_cx" style="border-collapse: collapse; width: 100%;">
                                                                <tr>
                                                                    <td align="right" style="width: 20%;">序号：
                                                                    </td>
                                                                    <td align="left" style="width: 80%">
                                                                        <telerik:RadNumericTextBox ID="RadNumericTextBoxDataNo" runat="server" MaxLength="5"
                                                                            Type="Number" NumberFormat-DecimalDigits="0" ShowSpinButtons="true" Width="70px" MinValue="1" MaxValue="1000">
                                                                            <NumberFormat DecimalDigits="0" GroupSeparator=""></NumberFormat>
                                                                        </telerik:RadNumericTextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="必填"
                                                                            ControlToValidate="RadNumericTextBoxDataNo" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" style="width: 40%">培训时间：
                                                                    </td>
                                                                    <td align="left" style="width: 60%">
                                                                        <telerik:RadDatePicker ID="RadDatePickerTrainDateStart" runat="server" Width="120px" Calendar-DayCellToolTipFormat="yyyy年MM月dd日">
                                                                        </telerik:RadDatePicker>
                                                                        <div style="float: left; padding: 0 12px">至</div>
                                                                        <telerik:RadDatePicker ID="RadDatePickerTrainDateEnd" runat="server" Width="120px" Calendar-DayCellToolTipFormat="yyyy年MM月dd日">
                                                                        </telerik:RadDatePicker>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="必填"
                                                                            ControlToValidate="RadDatePickerTrainDateStart" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="必填"
                                                                            ControlToValidate="RadDatePickerTrainDateEnd" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" style="width: 40%;">培训方式：
                                                                    </td>
                                                                    <td align="left" style="width: 60%">
                                                                        <asp:RadioButtonList ID="RadioButtonListTrainWay" runat="server" RepeatDirection="Horizontal" Width="150px">
                                                                            <asp:ListItem Text="网络" Value="网络" Selected="true"></asp:ListItem>
                                                                            <asp:ListItem Text="现场" Value="现场"></asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" style="width: 40%;">学时：
                                                                    </td>
                                                                    <td align="left" style="width: 60%">
                                                                        <telerik:RadNumericTextBox ID="RadNumericTextBoxPeriod" runat="server" MaxLength="5"
                                                                            Type="Number" NumberFormat-DecimalDigits="0" ShowSpinButtons="true" Width="70px" MinValue="1" MaxValue="100">
                                                                            <NumberFormat DecimalDigits="0" GroupSeparator=""></NumberFormat>
                                                                        </telerik:RadNumericTextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="必填"
                                                                            ControlToValidate="RadNumericTextBoxPeriod" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr id="trExamResult" runat="server">
                                                                    <td align="right" style="width: 40%;">考核结果：
                                                                    </td>
                                                                    <td align="left" style="width: 60%">
                                                                        <asp:RadioButtonList ID="RadioButtonListExamResult" runat="server" RepeatDirection="Horizontal" Width="150px">
                                                                            <asp:ListItem Text="合格" Value="合格" Selected="true"></asp:ListItem>
                                                                            <asp:ListItem Text="不合格" Value="不合格"></asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                </tr>
                                                                <tr id="trTrainUnit" runat="server">
                                                                    <td align="right">培训单位：
                                                                    </td>
                                                                    <td align="left">
                                                                        <telerik:RadTextBox runat="server" ID="RadTextBoxTrainUnit" Width="95%" Skin="Default"
                                                                            MaxLength="64">
                                                                        </telerik:RadTextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="必填"
                                                                            ControlToValidate="RadTextBoxTrainUnit" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right">培训内容：
                                                                    </td>
                                                                    <td align="left">
                                                                        <telerik:RadTextBox runat="server" ID="RadTextBoxTrainName" Width="95%" Skin="Default"
                                                                            MaxLength="1000" TextMode="MultiLine" Rows="2">
                                                                        </telerik:RadTextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="必填"
                                                                            ControlToValidate="RadTextBoxTrainName" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <div style="padding: 20px 100px 30px 0px; text-align: right">
                                                                <asp:Button ID="ButtonSave" CssClass="button" Text='<%# (Container is GridEditFormInsertItem) ? "保 存" : "更 新" %>'
                                                                    runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'></asp:Button>&nbsp;
                                        <asp:Button ID="Button2" CssClass="button" Text="取 消" runat="server" CausesValidation="False"
                                            CommandName="Cancel"></asp:Button>
                                                            </div>
                                                        </div>
                                                    </FormTemplate>
                                                    <PopUpSettings Modal="True" Width="500px"></PopUpSettings>
                                                </EditFormSettings>
                                                <HeaderStyle Font-Bold="True" />
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </td>
                                </tr>
                                <tr class="GridLightBK" id="trFuJanTitel" runat="server" visible="false">
                                    <td colspan="7" class="barTitle">附件上传</td>
                                </tr>
                                <tr class="GridLightBK" id="trFuJan" runat="server" visible="false">
                                    <td align="left" style="line-height: 20px;" colspan="7">
                                        <div class="DivContent">
                                            <p>附件要求说明：</p>
                                            <p runat="server" id="p_PostTyppe1" visible="false">
                                                1.续期申请表1份（须加盖聘用单位公章）；<br />
                                                2.身份证正反面1份；<br />
                                                3.继续教育合格材料1份。<br />
                                                4.年度安全教育培训记录。
                                            </p>
                                            <p runat="server" id="p_PostTyppe2" visible="false">
                                                1.续期申请表1份（须加盖聘用单位公章）；<br />
                                                2. 近3个月内二级乙等以上医院体检合格材料1份；<br />
                                                3.个人健康承若（下载模板：<a href="../Template/个人健康承诺.docx">【个人健康承诺模板.docx】</a>）；<br />
                                                4.年度安全教育培训记录。
                                            </p>
                                        </div>
                                        <div class="fujian">
                                            续期申请表扫描件：<span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.续期申请表扫描件%>','续期申请表扫描件','','<%=ApplyID%>')">选择文件</span><span class="tishi">（请打印申请表，聘用单位盖章签字后扫描上传，jpg格式图片最大500K）</span>
                                        </div>
                                        <div class="fujian" runat="server" id="div_FJ_TiJian" visible="false">
                                            体检合格证明：<span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.体检合格证明%>','体检合格证明','','<%=ApplyID%>')">选择文件</span><span class="tishi">（要求：上传体检合格证明扫描件原件、jpg格式图片,最大500K）</span>
                                        </div>
                                        <div class="fujian" runat="server" id="div_FJ_ShenFenZheng" visible="false">
                                            身份证扫描件：<span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.证件扫描件%>','证件扫描件','','<%=ApplyID%>')">选择文件</span><span class="tishi">（要求：上传身份证正反面扫描件原件、jpg格式图片，jpg格式图片最大500K）</span>
                                        </div>
                                        <div class="fujian" runat="server" id="div_FJ_BuLiang" visible="false">
                                            安全教育培训和无违章及不良作业记录证明：<span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.安全教育培训和无违章及不良作业记录证明%>','安全教育培训和无违章及不良作业记录证明','','<%=ApplyID%>')">选择文件</span><span class="tishi">（要求：jpg格式图片最大500K）</span>
                                        </div>
                                        <div class="fujian" runat="server" id="div_FJ_jxjy" visible="false">
                                            继续教育证明扫描件：<span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.继续教育证明扫描件%>','继续教育证明扫描件','','<%=ApplyID%>')">选择文件</span><span class="tishi">（要求：上传16学时继续教育证明扫描件、jpg格式图片,最大500K）</span>
                                        </div>
                                        <div class="fujian" runat="server" id="div_SheBao" visible="false">
                                            个人社保权益记录：<span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.社保扫描件%>','社保扫描件','','<%=ApplyID%>')">选择文件</span><span class="tishi">（要求：上传个人社保权益记录，jpg格式图片最大500K）</span>
                                        </div>
                                        <div class="fujian" runat="server" id="div_grjkcn" visible="false">
                                            个人健康承诺扫描件：<span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.个人健康承诺%>','个人健康承诺','','<%=ApplyID%>')">选择文件</span><span class="tishi">（要求：上传填写好的个人健康承诺扫描件，jpg格式图片,最大500K）</span>
                                        </div>
                                        <div class="fujian" runat="server" id="div_SafeTrain" visible="false">
                                            年度安全教育培训记录：<span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.年度安全教育培训记录%>','年度安全教育培训记录','','<%=ApplyID%>')">选择文件</span><span class="tishi">（要求：随申请表导出打印，个人签字、聘用单位盖章后扫描上传，jpg格式图片,最大500K）</span>
                                        </div>
                                    </td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td colspan="7" class="barTitle">初审单位</td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td style="text-align: center">初审单位</td>
                                    <td colspan="6">
                                        <telerik:RadComboBox ID="RadComboBoxFirstCheckUnit" runat="server" NoWrap="true" Width="400px">
                                        </telerik:RadComboBox>
                                        <p>
                                            <asp:Label ID="LabelNoFirstCheckUnitMessage" runat="server" Text="提示：初审单位由系统自动根据企业资质信息中的隶属关系自动匹配，无法正确匹配初审单位的请联系注册中心进行匹配。" Visible="false" ForeColor="Red"></asp:Label>
                                        </p>
                                    </td>
                                </tr>
                                <tr class="GridLightBK" id="trjxjyway" runat="server">
                                    <td colspan="7">
                                        <div style="float: left; padding-left: 20px;">完成初审单位组织的16学时证书延续继续教育学习形式：</div>
                                        <div style="float: left">
                                            <asp:RadioButtonList ID="RadioButtonListjxjyway" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Text="面授" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="网络教育" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="面授+网络教育" Value="3"></asp:ListItem>
                                            </asp:RadioButtonList>

                                        </div>
                                    </td>
                                </tr>
                            </table>
                            <div id="divCtl" runat="server" visible="false" style="padding: 5px; margin: 10px 10px; text-align: center;">
                                <div id="divStepDesc" runat="server" style="line-height: 300%;">操作流程：个人申请保存-->点击导出打印(导出申请表)-->上传相关附件-->提交单位审核</div>
                                <asp:Button ID="btnSave" runat="server" Text="保 存" CssClass="bt_large" OnClick="btnSave_Click" Enabled="false" />&nbsp;
                                 <asp:Button ID="ButtonDelete" Text="删 除" runat="server" CssClass="button" Enabled="false"
                                     OnClick="ButtonDelete_Click"></asp:Button>&nbsp;
                                <asp:Button ID="ButtonExit" Text="取消申请" runat="server" CssClass="bt_large" Enabled="false" CausesValidation="false" OnClientClick="return myconfirm(this);"
                                    OnClick="ButtonExit_Click"></asp:Button>&nbsp;                                                             
                                <asp:Button ID="ButtonExport" runat="server" Text="导出打印" CssClass="bt_large disenable" OnClick="ButtonExport_Click"
                                    Enabled="False" />
                            </div>
                            <div runat="server" id="divSheBao" style="width: 95%; padding-left: 20px; text-align: left; line-height: 40px;"></div>
                            <div id="divZhiFa" runat="server" visible="false" style="width: 100%; padding-top: 20px; text-align: center; clear: both;">
                                <table id="Table2" runat="server" width="100%" border="0" cellpadding="5" cellspacing="1" class="table" style="text-align: center; margin: 10px auto">
                                    <tr class="GridLightBK">
                                        <td class="barTitle">执法处罚记录</td>
                                    </tr>
                                    <tr class="GridLightBK">
                                        <td align="left" style="border-collapse: collapse;">
                                            <telerik:RadGrid ID="RadGridZhiFa" runat="server" ShowHeader="true" CellPadding="0" CellSpacing="0"
                                                GridLines="None" AllowPaging="False" AllowSorting="False" AutoGenerateColumns="False"
                                                Width="100%" EnableEmbeddedSkins="false" PagerStyle-AlwaysVisible="False">
                                                <ClientSettings EnableRowHoverStyle="False">
                                                </ClientSettings>
                                                <MasterTableView NoMasterRecordsText="" CommandItemDisplay="None" ShowHeader="false">
                                                    <Columns>
                                                        <telerik:GridTemplateColumn UniqueName="CertificateChangeID" HeaderText="证书编号">
                                                            <ItemTemplate>
                                                                <table cellpadding="5" cellspacing="1" class="zhifa">
                                                                    <tr class="GridLightBK">
                                                                        <td class="rr">执法机关</td>
                                                                        <td class="rl"><%#Eval("Jgmc")%></td>
                                                                        <td class="rr">检查类型</td>
                                                                        <td class="rl"><%#Eval("ZFJC_Type")%></td>
                                                                        <td class="rr">文书编号</td>
                                                                        <td class="rl"><%#Eval("WhDeputy")%></td>
                                                                    </tr>
                                                                    <tr class="GridLightBK">
                                                                        <td class="rr" class="rr">受检企业</td>
                                                                        <td class="rl"><%#Eval("Dsr_gzdw")%></td>
                                                                        <td class="rr">受检人员</td>
                                                                        <td class="rl"><%#Eval("Dsr_Xm")%></td>
                                                                        <td class="rr">证件号码</td>
                                                                        <td class="rl"><%#Eval("Dsr_gr_Zjhm")%></td>
                                                                    </tr>
                                                                    <tr class="GridLightBK">
                                                                        <td colspan="6" class="nr">
                                                                            <p><%#Eval("Wfss")%></p>
                                                                            <p><%#Eval("cfcs")==DBNull.Value?Eval("Xzcfjd"):Eval("cfcs")%></p>

                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                    </Columns>
                                                    <HeaderStyle Font-Bold="True" BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1" />
                                                    <ItemStyle CssClass="subtable" />
                                                    <AlternatingItemStyle CssClass="subtable" />
                                                </MasterTableView>
                                            </telerik:RadGrid>
                                        </td>
                                    </tr>
                                </table>
                            </div>
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
                            <table id="TableUnitCheck" runat="server" visible="false" width="100%" border="0" cellpadding="5" cellspacing="1" class="table" style="text-align: center; margin: 10px auto">
                                <tr class="GridLightBK">
                                    <td colspan="2" class="barTitle">单位审核</td>
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
                            <table id="TableFirstCheck" runat="server" visible="false" width="100%" border="0" cellpadding="5" cellspacing="1" class="table" style="text-align: center; margin: 10px auto">
                                <tr class="GridLightBK">
                                    <td colspan="2" class="barTitle">续期初审</td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td width="20%" align="right">处理结果：</td>
                                    <td width="80%" align="left">
                                        <asp:RadioButtonList ID="RadioButtonListFirstCheckResult" runat="server" RepeatDirection="Vertical" TextAlign="right">
                                            <asp:ListItem Text="通过" Value="通过" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="不通过" Value="不通过"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td width="20%" align="right">处理意见：</td>
                                    <td width="80%" align="left">
                                        <asp:TextBox ID="TextBoxFirstCheckRemark" runat="server" TextMode="MultiLine" Rows="3" Width="99%" MaxLength="100" Text="初审通过"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td colspan="2" align="center">
                                        <asp:Button ID="ButtonFirstCheck" runat="server" CssClass="bt_large" Text="确 定" OnClick="ButtonFirstCheck_Click" />&nbsp;&nbsp;
                                    </td>
                                </tr>
                            </table>
                            <table id="TableJWCheck" runat="server" visible="false" border="0" cellpadding="5" cellspacing="1" width="100%" class="table" style="text-align: center; margin: 10px auto">
                                <tr class="GridLightBK">
                                    <td colspan="2" class="barTitle">建委审核</td>
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
                                        <asp:TextBox ID="TextBoxCheckResult" runat="server" TextMode="MultiLine" Rows="3" Width="99%" MaxLength="200" Text="通过"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td colspan="2" align="center">
                                        <asp:Button ID="ButtonCheck" runat="server" CssClass="bt_large" Text="确 定" OnClick="ButtonCheck_Click" />&nbsp;&nbsp;
                                    </td>
                                </tr>
                            </table>
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
                                                        Width="100%" Skin="Default" EnableAjaxSkinRendering="false" OnDeleteCommand="RadGridFile_DeleteCommand"
                                                        EnableEmbeddedSkins="false">
                                                        <ClientSettings EnableRowHoverStyle="false">
                                                        </ClientSettings>
                                                        <MasterTableView NoMasterRecordsText="" GridLines="None" CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Left"
                                                            DataKeyNames="ApplyID,FileID">
                                                            <Columns>
                                                                <telerik:GridTemplateColumn UniqueName="ApplyFile" HeaderText="附件">
                                                                    <ItemTemplate>
                                                                        <img class="img200" alt="图片" src='<%# ZYRYJG.UIHelp.ShowFile(Eval("FileUrl").ToString())%>' />
                                                                        <asp:ImageButton runat="server" ID="ImageButton1" ImageUrl="../images/Cancel.gif" CommandName="Delete" Visible='<%#(IfExistRoleID("0") == true && ((ViewState["CertificateContinueOB"] as Model.CertificateContinueOB).Status == Model.EnumManager.CertificateContinueStatus.NewSave||(ViewState["CertificateContinueOB"] as Model.CertificateContinueOB).Status == Model.EnumManager.CertificateContinueStatus.SendBack))?true:false%>' OnClientClick="javascript:if(confirm('您确定要删除么?')==false) return false" />

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
        </div>
        <div id="winpop"></div>
        <uc4:IframeView ID="IframeView" runat="server" />

        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
            <script type="text/javascript">

                function myconfirm(send) {
                    if (posttypeid == 1) {
                        if (send.value == '提交单位审核') {
                            var rect = send.getBoundingClientRect();
                            layer.confirm('本次业务申请最终审批结果以住房和城乡建设部生成电子证书为准，在未生成电子证书前，请不要办理营业执照法定代表人变更或一、二级建造师变更、重新、注销注册等业务，以免无法正常生成电子证书，影响后续业务办理。', { title: '提示', btn: ['已知悉上述要求'], offset: rect.top - 400, area: ['600px', '250px'] }, function (index) { layer.close(index); __doPostBack('ButtonExit', ''); });

                            return false;
                        }
                        else {
                            __doPostBack('ButtonExit', '');
                            return false;
                        }
                    }
                    else
                    {
                        __doPostBack('ButtonExit', '');
                        return false;
                    }
                    
                }

                //选择文件
                function tips_pop(code, ftype, fsname, pid) {
                    layer.open({
                        type: 2,
                        title: ['资料上传 - ' + ftype, 'font-weight:bold;'],//标题
                        maxmin: true, //开启最大化最小化按钮,
                        offset: $(parent.document).scrollTop() + 20 + 'px',
                        area: ['800px', '500px'],
                        shadeClose: false, //点击遮罩关闭
                        content: '../uploader/Upload.aspx?o=' + code + '&t=' + ftype + '&s=' + fsname + '&a=' + pid,
                        cancel: function (index, layero) {
                            __doPostBack('refreshFile', '');
                            layer.close(index);
                            return false;
                        }
                    });
                    var MsgPop = document.getElementById("winpop");//获取窗口这个对象,即ID为winpop的对象
                    MsgPop.style.display = "block";
                    MsgPop.style.height = "400px";//高度增加4个象素
                }

                function onRequestStart(sender, args) {
                    if (args.get_eventTarget().indexOf('<%=ButtonExport.UniqueID %>') >= 0
                        || args.get_eventTarget().indexOf("btnSave") >= 0
              ) {
                        args.set_enableAjax(false);
                    }
                }

                function showImg(radUpload, eventArgs) {
                    var input = eventArgs.get_fileInputField();
                    var inputs = radUpload.getFileInputs();
                    var ImgCode = document.getElementById("<%=ImgCode.ClientID %>");
                    for (i = 0; i < inputs.length; i++) {
                        ImgCode.src = inputs[i].value;
                        break;
                    }
                }
                function SelectMeOnly(objRadioButton, grdName) {

                    var i, obj;
                    for (i = 0; i < document.all.length; i++) {
                        obj = document.all(i);

                        if (obj.type == "radio") {
                            if (obj.id.indexOf(grdName) > 0) {
                                if (objRadioButton.id == obj.id)
                                    obj.checked = true;
                                else
                                    obj.checked = false;
                            }
                        }
                    }
                }

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

                    //单位审核结果
                    $("#<%= RadioButtonListOldUnitCheckResult.ClientID%> input").each(function () {
                        $(this).click(function () {
                            var TextBoxOldUnitCheckRemark = $("#<%= TextBoxOldUnitCheckRemark.ClientID%>");

                            if ($(this).val() == "同意") {

                                TextBoxOldUnitCheckRemark.val("提交初审单位审核");
                            }
                            else {

                                TextBoxOldUnitCheckRemark.val("退回个人");

                            }
                        });

                    });
                    //初审审核结果
                    $("#<%= RadioButtonListFirstCheckResult.ClientID%> input").each(function () {
                        $(this).click(function () {
                            var TextBoxFirstCheckRemark = $("#<%= TextBoxFirstCheckRemark.ClientID%>");

                            if ($(this).val() == "通过") {

                                TextBoxFirstCheckRemark.val("初审通过");
                            }
                            else {

                                TextBoxFirstCheckRemark.val("退回修改");

                            }
                        });

                    });
                });

                function CheckClientValidate() {
                    Page_ClientValidate();
                    if (Page_IsValid) {
                        return true;
                    } else {
                        return false;
                    }
                }

                //选择文件
                function selectQY() {

                    layer.open({
                        type: 2,
                        title: ['选择一个企业 ', 'font-weight:bold;'],//标题
                        maxmin: true, //开启最大化最小化按钮,
                        offset: $(parent.document).scrollTop() + 20 + 'px',
                        area: ['800px', '500px'],
                        shadeClose: false, //点击遮罩关闭
                        content: '../SelectQY.aspx',
                        btn: ['确定', '关闭'],
                        yes: function (index, oArg) {
                            //获取选择的row,并加载到页面
                            var data = window["layui-layer-iframe" + index].callbackdata();
                            if (data) {
                                var RadTextBoxNewUnitCode = $find("<%= RadTextBoxUnitCode.ClientID%>");
                                var RadTextBoxNewUnitName = $find("<%= RadTextBoxUnitName.ClientID%>");
                                RadTextBoxNewUnitCode.set_value(data.ENT_OrganizationsCode);
                                RadTextBoxNewUnitName.set_value(data.ENT_Name)
                                layer.close(index);
                                return false;
                            } else {
                                layer.msg('请选择一个企业', { icon: 0 });
                            }
                        },
                        cancel: function (index, layero) {
                            layer.close(index);
                            return false;
                        }
                    });
                    var MsgPop = document.getElementById("winpop");//获取窗口这个对象,即ID为winpop的对象
                    MsgPop.style.display = "block";
                    MsgPop.style.height = "400px";//高度增加4个象素
                }

            </script>
        </telerik:RadCodeBlock>
    </form>
</body>
</html>
