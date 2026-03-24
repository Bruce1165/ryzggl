<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="zjsApplyContinue.aspx.cs" Inherits="ZYRYJG.zjs.zjsApplyContinue" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" />
    <link href="../Skins/Blue/Grid.Blue.css?v=1.008" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
    <script src="../Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
    <link href="../layer/skin/layer.css" rel="stylesheet" />
    <script src="../layer/layer.js" type="text/javascript"></script>
    <style type="text/css">
        td {
            line-height: 200%;
        }

        .detailTable {
            width: 98%;
        }

        .infoHead {
            text-align: right;
            vertical-align: top;
            font-weight: bold;
            line-height: 200%;
        }

        .infoHeadC {
            text-align: center;
            vertical-align: middle;
            font-weight: bold;
            line-height: 200%;
        }

        .formItem_1 {
            text-align: left;
            vertical-align: top;
            line-height: 200%;
        }

            .formItem_1 input {
                border: none;
                line-height: 200%;
                width: 100%;
            }

            .formItem_1 td {
            }

        .formItem_2 {
            text-align: center;
            vertical-align: middle;
            line-height: 200%;
        }

            .formItem_2 input {
                border: none;
                line-height: 200%;
                width: 100%;
                text-align: center;
            }

        .la {
            font-weight: bold;
            text-align: center;
        }

        .readonly {
            border: none !important;
        }

        .tbzy td {
            border: 1px solid #efefef;
            line-height: 200%;
        }

        .barTitle {
            color: #434343;
            background-color: #E4E4E4;
            font-weight: bold;
            border-left: 5px solid #ff6a00;
            text-align: left;
        }

        .img {
            border: none;
            width: 0px;
        }

        .img200 {
            border: none;
            width: 200px;
        }

        .subtable td {
            border: 1px solid #cccccc;
            border-collapse: collapse;
        }

        .addrow {
            float: right;
            background: url(../images/jiah.gif) no-repeat center center;
            width: 15px;
            height: 15px;
            padding-right: 20px;
        }

        .auto-style1 {
            text-align: right;
            vertical-align: top;
            font-weight: bold;
            line-height: 200%;
            width: 15%;
            border: 1px solid #efefef;
        }

        .main {
            color: red;
        }

        .rgCaption {
            text-align: left;
            color: orangered;
        }
      
        @keyframes blink {         
          70% { padding-right:20px; } 
        } 
        .blinking-text {
          animation: blink 2s infinite;
        }

         @keyframes blink2 {         
          90% { padding-left:10px; } 
        } 
        .blinking-text2 {
          animation: blink2 2s infinite;
        }   
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Windows7">
        </telerik:RadWindowManager>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server"
            EnableAJAX="true">
            <AjaxSettings>               
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <div class="div_out">
            <div id="div_top" class="dqts">
                <div style="float: left;">
                    当前位置 &gt;&gt;二级造价工程师注册 &gt;&gt;<strong>延续注册</strong>
                </div>
            </div>
            <div class="content">
                <div class="step">
                    <div class="stepLabel">办理进度：</div>
                    <div id="step_未申报" runat="server" class="stepItem lgray">个人填写></div>
                    <div id="step_待确认" runat="server" class="stepItem lgray">待单位审核></div>
                    <div id="step_已申报" runat="server" class="stepItem lgray">单位申报></div>
                    <div id="step_已受理" runat="server" class="stepItem lgray">市级受理></div>
                    <div id="step_已审核" runat="server" class="stepItem lgray">市级审核></div>
                    <div id="step_已决定" runat="server" class="stepItem lgray">市级决定></div>
                    <div id="step_已公告" runat="server" class="stepItem lgray">公告（办结）</div>
                    <div class="stepArrow">▶</div>
                </div>
                <div style="width: 100%; margin: 10px auto; text-align: center; overflow: hidden;">
                    <div class="DivTitleOn" onclick="DivOnOff(this,'Td3',event);" title="折叠">
                        申请说明
                    </div>
                    <div class="DivContent" id="Td3">
                        申请延续注册的，近一个注册有效期内继续教育学时应不少于120学时，其中必修课（每年度15学时，合计60学时）、选修课（每年度15学时，合计60学时）。同时注册两个专业的，继续教育学时可重复计算。<br />
                        <span style="color: red;">注意:自2023.10.1号起,人员需按要求完成必修课,选修课继续教育学时才可发起延续注册业务</span>
                    </div>
                    <div class="code">申请编号：<asp:Label ID="LabelApplyCode" runat="server" Text=""></asp:Label></div>
                    <div style="width: 66%; float: left; clear: left">
                        <table runat="server" id="EditTable" class="detailTable">
                            <tr class="GridLightBK">
                                <td colspan="4" class="barTitle">个人基本信息</td>
                            </tr>
                            <tr>
                                <td class="infoHead">姓名：
                                </td>
                                <td class="formItem_1">
                                    <telerik:RadTextBox ID="RadTextBoxPSN_Name" runat="server" CssClass="textEdit" Width="98%" MaxLength="50"></telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorPSN_Name" runat="server" ControlToValidate="RadTextBoxPSN_Name"
                                        ErrorMessage="请输入####" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                                <td class="auto-style1">性别：
                                </td>
                                <td class="formItem_1">
                                    <telerik:RadTextBox ID="RadTextBoxPSN_Sex" runat="server" CssClass="textEdit" Width="98%" MaxLength="2"></telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="infoHead">出生日期：</td>
                                <td class="formItem_1">
                                    <telerik:RadDatePicker ID="RadDatePickerBirthday" runat="server" MinDate="1900-1-1" MaxDate="2050-1-1" Calendar-DayCellToolTipFormat="yyyy年MM月dd日">
                                    </telerik:RadDatePicker>
                                </td>
                                <td class="infoHead">民族：
                                </td>
                                <td class="formItem_1">
                                    <telerik:RadComboBox ID="RadComboBoxNation" runat="server" Width="98%" NoWrap="true" DropDownCssClass="multipleRowsColumns" DropDownWidth="620px" Height="300px">
                                    </telerik:RadComboBox>
                                    <asp:CompareValidator ValueToCompare="请选择" Operator="NotEqual" ControlToValidate="RadComboBoxNation"
                                        ErrorMessage="必填" runat="server" ID="Comparevalidator4" ForeColor="Red" Display="Dynamic" />
                                </td>
                            </tr>
                            <tr>
                                <td class="infoHead">证件类型：
                                </td>
                                <td class="formItem_1">
                                    <telerik:RadTextBox ID="RadTextBoxPSN_CertificateType" runat="server" CssClass="textEdit" Width="98%" MaxLength="20"></telerik:RadTextBox>
                                </td>
                                <td class="auto-style1">证件号码：
                                </td>
                                <td class="formItem_1">
                                    <telerik:RadTextBox ID="RadTextBoxPSN_CertificateNO" runat="server" CssClass="textEdit" Width="98%" MaxLength="30"></telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="infoHead">手机号码：
                                </td>
                                <td class="formItem_1">
                                    <telerik:RadTextBox ID="RadTextBoxPSN_MobilePhone" runat="server" CssClass="textEdit" Width="98%" MaxLength="100"></telerik:RadTextBox>
                                </td>
                                <td class="infoHead">电子邮箱：
                                </td>
                                <td class="formItem_1">
                                    <telerik:RadTextBox ID="RadTextBoxPSN_Email" runat="server" CssClass="textEdit" Width="98%" MaxLength="200"></telerik:RadTextBox>
                                </td>
                            </tr>


                            <tr class="GridLightBK">
                                <td colspan="4" class="barTitle">聘用单位信息</td>
                            </tr>
                            <tr>
                                <td class="infoHead" style="width: 20%">单位名称：
                                </td>
                                <td class="formItem_1" style="width: 30%">
                                    <telerik:RadTextBox ID="RadTextBoxENT_Name" runat="server" CssClass="textEdit" Width="98%" MaxLength="200"></telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorENT_Name" runat="server" ControlToValidate="RadTextBoxENT_Name"
                                        ErrorMessage="请输入单位名称" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                                <td class="infoHead" style="width: 20%">机构代码：
                                </td>
                                <td class="formItem_1" style="width: 30%">
                                    <telerik:RadTextBox ID="RadTextBoxENT_OrganizationsCode" runat="server" CssClass="textEdit" Width="98%" MaxLength="50"></telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style1">工商注册地：
                                </td>
                                <td class="formItem_1" colspan="3">
                                    <telerik:RadTextBox ID="RadTextBoxEND_Addess" runat="server" CssClass="textEdit" Width="98%" MaxLength="100"></telerik:RadTextBox>
                                </td>

                            </tr>
                            <tr>
                                <td class="auto-style1">所属区县：
                                </td>
                                <td class="formItem_1">
                                    <telerik:RadTextBox ID="RadTextBoxENT_City" runat="server" CssClass="textEdit" Width="98%" MaxLength="100"></telerik:RadTextBox>
                                </td>
                                <td class="auto-style1">单位法人：
                                </td>
                                <td class="formItem_1">
                                    <telerik:RadTextBox ID="RadTextBoxFR" runat="server" CssClass="textEdit" Width="98%" MaxLength="100"></telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style1">联系人：
                                </td>
                                <td class="formItem_1">
                                    <telerik:RadTextBox ID="RadTextBoxLinkMan" runat="server" CssClass="textEdit" Width="98%" MaxLength="100"></telerik:RadTextBox>
                                </td>
                                <td class="auto-style1">联系电话：
                                </td>
                                <td class="formItem_1">
                                    <telerik:RadTextBox ID="RadTextBoxENT_Telephone" runat="server" CssClass="textEdit" Width="98%" MaxLength="100"></telerik:RadTextBox>
                                </td>
                            </tr>                            
                            <tr class="GridLightBK">
                                <td colspan="4" class="barTitle">注册证书信息</td>
                            </tr>
                            <tr>
                                <td class="infoHeadC">注册编号
                                </td>
                                <td class="infoHeadC">注册专业
                                </td>
                                <td class="infoHeadC" colspan="2">注册有效期届满日期
                                </td>

                            </tr>
                            <tr>
                                <td class="formItem_2">
                                    <telerik:RadTextBox ID="RadTextBoxPSN_RegisterNo" runat="server" CssClass="textEdit" Width="98%" MaxLength="50"></telerik:RadTextBox>
                                </td>
                                <td class="formItem_2">
                                    <telerik:RadTextBox ID="RadTextBoxPSN_RegisteProfession" runat="server" CssClass="readonly" Width="98%" MaxLength="50" ReadOnly="true"></telerik:RadTextBox>
                                </td>
                                <td class="formItem_2" colspan="2">
                                    <telerik:RadTextBox ID="RadTextBoxPSN_CertificateValidity" runat="server" CssClass="readonly" Width="98%" MaxLength="50" ReadOnly="true"></telerik:RadTextBox>
                                </td>

                            </tr>
                            <tr class="GridLightBK">
                                <td colspan="4" class="barTitle">继续教育情况（延续注册）<div style="float: right; padding-right: 40px;" class="blinking-text"><a target="_blank" href="http://120.52.185.14/Register/NewsView.aspx?o=dCT+otPFLBJzV1c8WZgk4BJr1sENjmh6OOlmZ4aRoM4AXjPheR/rig==">【继续教育学习形式说明】</a></div>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td align="right" class="infoHead">必修课（学时）</td>
                                <td align="left" class="formItem_1">
                                    <asp:Label ID="LabelBiXiu" runat="server" Text=""></asp:Label>

                                </td>
                                <td align="left" colspan="2" class="formItem_1">
                                    <asp:Label ID="LabelBiXiuFinishCase" runat="server" Text="" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td align="right">选修课（学时）</td>
                                <td align="left" class="formItem_1">
                                    <asp:Label ID="LabelXuanXiu" runat="server" Text=""></asp:Label></td>
                                <td align="left" colspan="2" class="formItem_1">
                                    <asp:Label ID="LabelXuanXiuFinishCase" runat="server" Text="" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                            <tr class="GridLightBK" id="trXuanXiuDetail" runat="server" visible="false">
                                <td colspan="4" style="border-collapse: collapse">
                                    <telerik:RadGrid ID="RadGridXuanXiu" runat="server" AutoGenerateColumns="False" AllowSorting="false" HeaderStyle-BorderColor="#eee"
                                        GridLines="None" CellPadding="0" Width="98%" Skin="Blue" EnableAjaxSkinRendering="false"
                                        EnableEmbeddedSkins="false" OnItemDataBound="RadGridXuanXiu_ItemDataBound" OnInsertCommand="RadGridXuanXiu_InsertCommand" OnUpdateCommand="RadGridXuanXiu_UpdateCommand"
                                        OnDeleteCommand="RadGridXuanXiu_DeleteCommand" OnNeedDataSource="RadGridXuanXiu_NeedDataSource">
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
                                                <telerik:GridEditCommandColumn UniqueName="Edit" EditText="编辑" EditImageUrl="../Images/jia.gif" ButtonType="ImageButton">
                                                </telerik:GridEditCommandColumn>
                                                <telerik:GridButtonColumn UniqueName="Delete" CommandName="Delete" ConfirmText="您确定要删除么?" Text="&lt;span onclick=&quot;if(!confirm('您确定要删除么?'))return false; &quot; &gt;删除&lt;/span&gt;" ButtonType="ImageButton" ImageUrl="../Images/close.png">
                                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                                </telerik:GridButtonColumn>
                                            </Columns>
                                            <CommandItemSettings AddNewRecordText="<span style='color:blue' class='blinking-text2'>点击这里添加选修课培训记录</span>" ShowRefreshButton="false" />
                                            <EditFormSettings InsertCaption="添加选修课培训记录" CaptionFormatString="编辑选修课培训记录"
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
                                                                <td align="right">培训时间：
                                                                </td>
                                                                <td align="left">
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
                                                                <td align="right">培训方式：
                                                                </td>
                                                                <td align="left">
                                                                    <asp:RadioButtonList ID="RadioButtonListTrainWay" runat="server" RepeatDirection="Horizontal" Width="150px">
                                                                        <asp:ListItem Text="网络" Value="网络" Selected="true"></asp:ListItem>
                                                                        <asp:ListItem Text="现场" Value="现场"></asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">学时：
                                                                </td>
                                                                <td align="left">
                                                                    <telerik:RadNumericTextBox ID="RadNumericTextBoxPeriod" runat="server" MaxLength="5"
                                                                        Type="Number" NumberFormat-DecimalDigits="0" ShowSpinButtons="true" Width="70px" MinValue="1" MaxValue="100">
                                                                        <NumberFormat DecimalDigits="0" GroupSeparator=""></NumberFormat>
                                                                    </telerik:RadNumericTextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="必填"
                                                                        ControlToValidate="RadNumericTextBoxPeriod" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
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
                             <tr class="GridLightBK" id="LabelWorkResult" runat="server" visible="false">
                                <td colspan="4" class="barTitle">前一个注册期内的主要工作业绩（根据个人实际情况自行填写，没有业绩忽略本项）
                                </td>
                            </tr>
                             <tr class="GridLightBK" id="trWorkResult" runat="server" visible="false">
                                <td colspan="4" style="border-collapse: collapse">
                                    <telerik:RadGrid ID="RadGridWorkResult" runat="server" AutoGenerateColumns="False" AllowSorting="false" HeaderStyle-BorderColor="#eee"
                                        GridLines="None" CellPadding="0" Width="98%" Skin="Blue" EnableAjaxSkinRendering="false"
                                        EnableEmbeddedSkins="false" OnItemDataBound="RadGridWorkResult_ItemDataBound" OnInsertCommand="RadGridWorkResult_InsertCommand" OnUpdateCommand="RadGridWorkResult_UpdateCommand"
                                        OnDeleteCommand="RadGridWorkResult_DeleteCommand" OnNeedDataSource="RadGridWorkResult_NeedDataSource">
                                        <ClientSettings EnableRowHoverStyle="true">
                                            <Selecting AllowRowSelect="True" />
                                        </ClientSettings>
                                        <MasterTableView DataKeyNames="DetailID,ApplyID" EditMode="EditForms" CommandItemDisplay="Top" NoMasterRecordsText="没有可显示的记录">
                                            <Columns>
                                                <telerik:GridBoundColumn HeaderText="序号" UniqueName="DataNo" DataField="DataNo" AllowSorting="false">
                                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                              
                                                <telerik:GridTemplateColumn HeaderText="起止日期" UniqueName="DateStart">
                                                    <ItemTemplate>
                                                        <%# string.Format("{0}至{1}",Convert.ToDateTime(Eval("DateStart")).ToString("yyyy-MM-dd")
                                                        ,Convert.ToDateTime(Eval("DateEnd")).ToString("yyyy-MM-dd"))
                                                        %>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridTemplateColumn>

                                                <telerik:GridBoundColumn UniqueName="ProjectName" DataField="ProjectName" HeaderText="工程项目名称">
                                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="Job" DataField="Job" HeaderText="负责工作内容">
                                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="Cost" DataField="Cost" HeaderText="总造价（万元）">
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
                                            <CommandItemSettings AddNewRecordText="<span style='color:blue' class='blinking-text2'>点击这里添加工作业绩</span>" ShowRefreshButton="false" />
                                            <EditFormSettings InsertCaption="添加工作业绩" CaptionFormatString="编辑工作业绩"
                                                EditFormType="Template" PopUpSettings-Modal="false" FormCaptionStyle-HorizontalAlign="Left">
                                                <EditColumn UniqueName="EditCommandColumn1">
                                                </EditColumn>
                                                <FormTemplate>
                                                    <div id="DivEdit" runat="server" style="width: 100%; margin: 0 auto; background-color: #fefefe;">
                                                        <br />
                                                        <table class="bar_cx" style="border-collapse: collapse; width: 100%;">
                                                            <tr>
                                                                <td align="right" style="width: 20%;"><span style="color: Red">* </span>序号：
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
                                                                <td align="right"><span style="color: Red">* </span>起止日期：
                                                                </td>
                                                                <td align="left">
                                                                    <telerik:RadDatePicker ID="RadDatePickerDateStart" runat="server" Width="120px" Calendar-DayCellToolTipFormat="yyyy年MM月dd日">
                                                                    </telerik:RadDatePicker>
                                                                    <div style="float: left; padding: 0 12px">至</div>
                                                                    <telerik:RadDatePicker ID="RadDatePickerDateEnd" runat="server" Width="120px" Calendar-DayCellToolTipFormat="yyyy年MM月dd日">
                                                                    </telerik:RadDatePicker>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="必填"
                                                                        ControlToValidate="RadDatePickerDateStart" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="必填"
                                                                        ControlToValidate="RadDatePickerDateEnd" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                          
                                                           
                                                            <tr>
                                                                <td align="right"><span style="color: Red">* </span>工程项目名称：
                                                                </td>
                                                                <td align="left">
                                                                    <telerik:RadTextBox runat="server" ID="RadTextBoxProjectName" Width="95%" Skin="Default"
                                                                        MaxLength="100">
                                                                    </telerik:RadTextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="必填"
                                                                        ControlToValidate="RadTextBoxProjectName" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right"><span style="color: Red">* </span>负责工作内容：
                                                                </td>
                                                                <td align="left">
                                                                    <telerik:RadTextBox runat="server" ID="RadTextBoxJob" Width="95%" Skin="Default"
                                                                        MaxLength="200" TextMode="MultiLine" Rows="2">
                                                                    </telerik:RadTextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="必填"
                                                                        ControlToValidate="RadTextBoxJob" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                             <tr>
                                                                <td align="right"><span style="color: Red">* </span>总造价（万元）：
                                                                </td>
                                                                <td align="left">
                                                                    <telerik:RadNumericTextBox ID="RadNumericCost" runat="server" MaxLength="5"
                                                                        Type="Number" NumberFormat-DecimalDigits="0" ShowSpinButtons="true" Width="70px" MinValue="0" MaxValue="100000000">
                                                                        <NumberFormat DecimalDigits="0" GroupSeparator=""></NumberFormat>
                                                                    </telerik:RadNumericTextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="必填"
                                                                        ControlToValidate="RadNumericCost" Display="Dynamic"></asp:RequiredFieldValidator>
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
                                <td colspan="4" class="barTitle">附件上传<span style="color: red">(所有电子证书扫描件要求与原件1:1比例正向扫描上传,信息清晰完整) </span>
                                </td>
                            </tr>
                            <tr class="GridLightBK" id="trFuJan" runat="server" visible="false">
                                <td colspan="4">
                                    <div style="text-align: right; padding-right: 40px;">
                                        <span class="tishi">附件辅助工具下载：</span><a
                                            href="../Images/1寸照片生成器.exe" style="text-decoration: underline; color: Blue;"><img
                                                alt="" src="../Images/Soft_common.gif" style="border-width: 0;" />
                                            1寸照片生成器.exe</a>
                                    </div>
                                    <div class="fujian">
                                        <span style="color: Red">* </span>一寸免冠照片：<span class="link" onclick="javascript:tips_pop('<%=string.Format("{0}/{1}",Model.EnumManager.FileDataType.一寸免冠照片,RadTextBoxPSN_CertificateNO.Text.Trim().Substring(RadTextBoxPSN_CertificateNO.Text.Trim().Length - 3, 3))%>','一寸免冠照片','','<%=ApplyID%>')">选择文件</span><span class="tishi">（要求：上传免冠照片扫描件原件、一寸jpg格式图片，名称不限，最大为50K，宽高110 X 140像素）</span>
                                    </div>
                                    <div class="fujian">
                                        <span style="color: Red">* </span>劳动合同扫描件：<span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.劳动合同扫描件%>','劳动合同扫描件','','<%=ApplyID%>')">选择文件</span><span class="tishi">（要求：上传劳动合同扫描件原件、只上传重要页,最多上传5页、jpg格式图片,每页最大500K）</span>
                                    </div>
                                    <div class="fujian">
                                        <span style="color: Red">* </span>继续教育合格证明：<span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.继续教育证明扫描件%>','继续教育证明扫描件','','<%=ApplyID%>')">选择文件</span><span class="tishi">（要求：上传《近一个注册有效期内继续教育（选修课）完成情况》扫描件、jpg格式图片,最大500K）</span>
                                    </div>
                                    <%-- <div class="fujian">
                                        <span style="color: Red">* </span>继续教育承诺书扫描件：<span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.继续教育承诺书扫描件%>','继续教育承诺书扫描件','','<%=ApplyID%>')">选择文件</span><span class="tishi">（要求：上传继续教育承诺书扫描件原件、jpg格式图片,最大500K）</span><a target="_blank" href="../Template/二建继续教育承诺书.docx">《二建继续教育承诺书模板》</a>
                                    </div>--%>
                                    <div class="fujian">
                                        <span style="color: Red">* </span>申请表扫描件：<span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.申请表扫描件%>','申请表扫描件','','<%=ApplyID%>')">选择文件</span><span class="tishi">（要求：请在本页面导出打印申请表，单位盖章签字后扫描上传）</span>
                                    </div>
                                    <div class="fujian">
                                        社保权益记录及其他劳动关系证明材料：<span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.社保扫描件%>','社保扫描件','','<%=ApplyID%>')">选择文件</span>
                                        <span class="tishi">（社保校验结果为符合的，不用上传；不符合的，需上传相关社保权益记录或其他证明材料扫描件，jpg格式图片,最大500K。）<br />
                                            社保附件要求说明：<br />
                                            1、已在申请单位缴纳申请之日上一个月社会保险的，须提交《社会保险个人权益记录》；<br />
                                            2、已在申请单位本市或外省市分支机构缴纳申请之日上一个月社会保险的，须提交该单位分支机构的《营业执照》和《社会保险个人权益记录》；<br />
                                            3、 已办理正式退休手续的，须提交退休所在单位人事部门或社会保障部门颁发的《退休证》；<br />
                                            4、已办理正式退休手续，但男未满60周岁、女未满55周岁的，须提交退休所在单位人事部门或社会保障部门颁发的《退休证》和《城乡居民养老保险待遇核定表》。                                         
                                        </span>
                                    </div>
                                </td>
                            </tr>
                            
                        </table>
                        <div id="divGR" visible="false" runat="server" style="width: 100%; padding-top: 20px; text-align: center; clear: both;">
                            <div id="divStepDesc" runat="server" style="line-height: 300%;">操作流程：个人申请保存-->点击导出打印(导出申请表)-->上传相关附件-->提交单位审核</div>
                            <asp:Button ID="ButtonSave" runat="server" Text="保 存" CssClass="bt_large" UseSubmitBehavior="false" OnClientClick="if(CheckClientValidate()==true){this.value='正在提交';this.disabled=true;}" OnClick="ButtonSave_Click" Enabled="false" />
                            &nbsp;&nbsp;<asp:Button ID="ButtonOutput" runat="server" Text="导出打印" CssClass="bt_large" OnClick="ButtonOutput_Click" Enabled="false" />
                            &nbsp;&nbsp;<asp:Button ID="ButtonUnit" runat="server" type="button" Text="提交单位确认" CssClass="bt_large" OnClick="ButtonUnit_Click" Enabled="false" />
                            &nbsp;&nbsp;<asp:Button ID="ButtonDelete" runat="server" Text="删 除" CssClass="bt_large" OnClick="ButtonDelete_Click" Enabled="false" OnClientClick="javascript:if(!confirm('您确定要删除么?'))return false;" CausesValidation="false" />
                            <%--   &nbsp;&nbsp;<asp:Button ID="ButtonLoadFile" runat="server" Text="载入测试附件" CssClass="bt_large" OnClick="ButtonLoadFile_Click" Visible="false" />--%>
                        </div>
                        <div id="divCheckHistory" visible="true" runat="server" style="width: 98%; padding-top: 20px; text-align: center; clear: both;">
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
                                                <ItemStyle CssClass="subtable" />
                                                <AlternatingItemStyle CssClass="subtable" />
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="divUnit" runat="server" visible="false" style="padding-top: 20px; text-align: center; clear: both;" class="auto-style3">
                            <table id="Table6" runat="server" width="100%" border="0" cellpadding="5" cellspacing="1" class="table" style="text-align: center; margin: 10px auto">
                                <tr class="GridLightBK">
                                    <td colspan="2" class="barTitle">单位确认</td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td width="20%" align="right">处理结果：</td>
                                    <td width="80%" align="left">
                                        <asp:RadioButtonList ID="RadioButtonListOldUnitCheckResult" runat="server" RepeatDirection="Horizontal" TextAlign="right">
                                            <asp:ListItem Text="同意" Value="同意" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="不同意" Value="不同意"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td width="20%" align="right">处理意见：</td>
                                    <td width="80%" align="left">
                                        <asp:TextBox ID="TextBoxOldUnitCheckRemark" runat="server" TextMode="MultiLine" Rows="3" Width="99%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td colspan="2" align="center">
                                        <asp:Button ID="ButtonApply" runat="server" CssClass="bt_large" Text="确定" OnClick="ButtonApply_Click" />&nbsp;&nbsp;                                         
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="divQX" runat="server" visible="false" style="width: 98%; padding-top: 20px; text-align: center; clear: both;">
                            <table id="TableEdit" runat="server" width="100%" border="0" cellpadding="5" cellspacing="1" class="table" style="text-align: center; margin: 10px auto">
                                <tr class="GridLightBK">
                                    <td colspan="2" class="barTitle">受理操作</td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td width="20%" align="right">受理结果：</td>
                                    <td width="80%" align="left">
                                        <asp:RadioButtonList ID="RadioButtonListApplyStatus" runat="server" RepeatDirection="Horizontal" TextAlign="right">
                                            <asp:ListItem Text="通过" Value="通过" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="不通过" Value="不通过"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td width="20%" align="right">受理意见：</td>
                                    <td width="80%" align="left">
                                        <asp:TextBox ID="TextBoxApplyGetResult" runat="server" TextMode="MultiLine" Rows="3" Width="99%" Text="予以受理"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td colspan="2" align="center">
                                        <asp:Button ID="BtnSave" runat="server" CssClass="bt_large" Text="确认提交" OnClick="BtnSave_Click" />&nbsp;&nbsp;
                                            <input id="BtnReturn" type="button" class="bt_large" value="返 回" onclick='javascript: hideIfam()' />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="divQXCK" runat="server" visible="false" style="width: 98%; padding-top: 20px; text-align: center; clear: both;">
                            <table id="Table4" runat="server" width="100%" border="0" cellpadding="5" cellspacing="1" class="table" style="text-align: center; margin: 10px auto">
                                <tr class="GridLightBK">
                                    <td colspan="2" class="barTitle">审核操作</td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td width="20%" align="right">审核结果：</td>
                                    <td width="80%" align="left">
                                        <asp:RadioButtonList ID="RadioButtonListExamineResult" runat="server" RepeatDirection="Horizontal" TextAlign="right">
                                            <asp:ListItem Text="通过" Value="通过" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="不通过" Value="不通过"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td width="20%" align="right">审核意见：</td>
                                    <td width="80%" align="left">
                                        <asp:TextBox ID="TextBoxExamineRemark1" runat="server" TextMode="MultiLine" Rows="3" Width="99%" Text="允许通过"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td colspan="2" align="center">
                                        <asp:Button ID="BttSave" runat="server" CssClass="bt_large" Text="确认提交" OnClick="BttSave_Click" />&nbsp;&nbsp;
                                            <input id="BtnReturnck" type="button" class="bt_large" value="返 回" onclick='javascript: hideIfam()' />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="divDecide" runat="server" visible="false" style="width: 98%; padding-top: 20px; text-align: center; clear: both;">
                            <table id="Table3" runat="server" width="100%" border="0" cellpadding="5" cellspacing="1" class="table" style="text-align: center; margin: 10px auto">
                                <tr class="GridLightBK">
                                    <td colspan="2" class="barTitle">决定操作</td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td width="20%" align="right">决定结果：</td>
                                    <td width="80%" align="left">
                                        <asp:RadioButtonList ID="RadioButtonListDecide" runat="server" RepeatDirection="Horizontal" TextAlign="right">
                                            <asp:ListItem Text="通过" Value="通过" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="不通过" Value="不通过"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td colspan="2" align="center">
                                        <asp:Button ID="ButtonDecide" runat="server" CssClass="bt_large" Text="确认提交" OnClick="ButtonDecide_Click" />&nbsp;&nbsp;
                                            <input id="BtnReturn4" type="button" class="bt_large" value="返 回" onclick='javascript: hideIfam()' />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="divSendBack" runat="server" visible="false" style="width: 98%; padding-top: 20px; text-align: center; clear: both;">
                            <table id="Table2" runat="server" width="100%" border="0" cellpadding="5" cellspacing="1" class="table" style="text-align: center; margin: 10px auto">
                                <tr class="GridLightBK">
                                    <td colspan="2" class="barTitle">审核流程后退操作</td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td width="20%" align="right">请选择要后退到的节点：</td>
                                    <td width="80%" align="left">
                                        <telerik:RadComboBox ID="RadComboBoxReturnApplyStatus" runat="server" Width="80">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="已申报" Value="已申报" />
                                                <telerik:RadComboBoxItem Text="已受理" Value="已受理" />
                                                <telerik:RadComboBoxItem Text="已审核" Value="已审核" />
                                                <telerik:RadComboBoxItem Text="已决定" Value="已决定" />
                                                <telerik:RadComboBoxItem Text="已公告" Value="已公告" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td colspan="2" align="center">
                                        <asp:Button ID="ButtonSendBack" runat="server" CssClass="bt_large" Text="执行后退" OnClick="ButtonSendBack_Click" OnClientClick="javascript:if(!confirm('您确定要后退么?')) return false;" CausesValidation="false" />&nbsp;&nbsp;
                                            <input id="BtnReturn5" type="button" class="bt_large" value="返 回" onclick='javascript: hideIfam()' />
                                    </td>
                                </tr>
                            </table>
                        </div>
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
                                                                    <asp:ImageButton runat="server" ID="ImageButton1" ImageUrl="../images/Cancel.gif" CommandName="Delete" Visible='<%#(IfExistRoleID("0") == true && ((ViewState["zjs_ApplyMDL"] as Model.zjs_ApplyMDL).ApplyStatus == Model.EnumManager.ApplyStatus.未申报 || (ViewState["zjs_ApplyMDL"] as Model.zjs_ApplyMDL).ApplyStatus == Model.EnumManager.ApplyStatus.已驳回))?true:false%>' OnClientClick="javascript:if(confirm('您确定要删除么?')==false) return false" />
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
        <div id="winpop">
        </div>
    </form>
    <script type="text/javascript">
        var colornum = 0;
        function changeColor() {
            var color = "#fff|red";
            color = color.split("|");
            if (document.getElementById("blink")) document.getElementById("blink").style.color = color[colornum];
            if (colornum == 0) {
                colornum = 1
            } else {
                colornum = 0
            }
        }
        if (document.getElementById("blink")) {
            setInterval("changeColor()", 500);
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
            //变换受理结果
            $("#<%= RadioButtonListApplyStatus.ClientID%> input").each(function () {
                $(this).click(function () {
                    var TextBoxApplyGetResult = $("#<%= TextBoxApplyGetResult.ClientID%>");
                    if ($(this).val() == "通过") {
                        TextBoxApplyGetResult.val("予以受理");
                    }
                    else {
                        TextBoxApplyGetResult.val("不予受理");
                    }
                });
            });
            //变换审核结果
            $("#<%= RadioButtonListExamineResult.ClientID%> input").each(function () {
                $(this).click(function () {
                    var TextBoxApplyCheckRemark = $("#<%= TextBoxExamineRemark1.ClientID%>");
                    if ($(this).val() == "通过") {
                        TextBoxApplyCheckRemark.val("允许通过");
                    }
                    else {
                        TextBoxApplyCheckRemark.val("审核未通过");
                    }
                });
            });
        })
        function tips_pop(code, ftype, fsname, pid) {
            layer.open({
                type: 2,
                title: ['资料上传 - ' + ftype, 'font-weight:bold;background: #5DA2EF;'],//标题
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
        function CheckClientValidate() {
            Page_ClientValidate();
            if (Page_IsValid) {
                return true;
            } else {
                return false;
            }
        }
    </script>
</body>
</html>
