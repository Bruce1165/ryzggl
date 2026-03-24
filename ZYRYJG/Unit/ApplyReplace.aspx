<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplyReplace.aspx.cs" Inherits="ZYRYJG.Unit.ApplyReplace" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" />

    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
    <script src="../Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
    <link href="../layer/skin/layer.css" rel="stylesheet" />
    <script src="../layer/layer.js" type="text/javascript"></script>
    <style type="text/css">
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

        td {
            line-height: 200%;
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
                    当前位置 &gt;&gt;事项申报 &gt;&gt;<strong>遗失（污损）补办</strong>
                </div>
            </div>
            <div class="content">
                <p class="jbxxbt">
                    遗失（污损）补办
                </p>
                <div class="step">
                    <div class="stepLabel">办理进度：</div>
                    <div id="step_未申报" runat="server" class="stepItem lgray">个人填写></div>
                     <div id="step_待确认" runat="server" class="stepItem lgray">待单位确认></div>
                    <div id="step_已申报" runat="server" class="stepItem lgray">单位申报></div>
                    <div id="step_已受理" runat="server" class="stepItem lgray">区县受理></div>
                    <div id="step_区县审查" runat="server" class="stepItem lgray">区县审核></div>
                    <div id="step_已上报" runat="server" class="stepItem lgray">汇总上报></div>
                    <div id="step_已审查" runat="server" class="stepItem lgray">市建委审核></div>
                    <div id="step_已决定" runat="server" class="stepItem lgray">市建委决定></div>
                    <div id="step_已公示" runat="server" class="stepItem lgray">公示></div>
                    <div id="step_已公告" runat="server" class="stepItem lgray">公告（办结）</div>
                    <div class="stepArrow">▶</div>
                </div>
                <div style="width: 100%; margin: 10px auto; text-align: center; overflow: hidden;">
                    <div class="code">申请编号：<asp:Label ID="LabelApplyCode" runat="server" Text=""></asp:Label></div>
                    <div style="width: 66%; float: left; clear: left">
                        <table runat="server" id="EditTable" class="detailTable">
                            <tr class="GridLightBK">
                                <td colspan="4" class="barTitle">基本信息</td>
                            </tr>
                            <tr>
                                <td class="infoHead w20">企业名称：
                                </td>
                                <td colspan="3" class="formItem_1 w40">
                                    <telerik:RadTextBox ID="RadTextBoxENT_Name" runat="server" CssClass="textEdit" Width="98%" MaxLength="200"></telerik:RadTextBox>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorENT_Name" runat="server" ControlToValidate="RadTextBoxENT_Name"
                                        ErrorMessage="请输入企业名称" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="infoHead">机构代码（或信用代码）：
                                </td>
                                <td class="formItem_1">
                                    <telerik:RadTextBox ID="RadTextBoxENT_OrganizationsCode" runat="server" CssClass="textEdit" Width="98%" MaxLength="50"></telerik:RadTextBox>
                                </td>
                                <td class="infoHead">所在区县：
                                </td>
                                <td class="formItem_1">
                                    <telerik:RadTextBox ID="RadTextBoxENT_City" runat="server" CssClass="textEdit" Width="98%" MaxLength="100"></telerik:RadTextBox>
                                </td>

                            </tr>
                            <tr>
                                <td class="infoHead">联系人：
                                </td>
                                <td class="formItem_1">
                                    <telerik:RadTextBox ID="RadTextBoxLinkMan" runat="server" Width="99%"></telerik:RadTextBox>
                                </td>
                                <td class="infoHead">联系电话：
                                </td>
                                <td class="formItem_1">
                                    <telerik:RadTextBox ID="RadTextBoxENT_Telephone" runat="server" CssClass="textEdit" Width="98%" MaxLength="100"></telerik:RadTextBox>
                                </td>

                            </tr>
                            <tr>
                                <td class="infoHead">姓名：
                                </td>
                                <td class="formItem_1">
                                    <telerik:RadTextBox ID="RadTextBoxPSN_Name" runat="server" CssClass="textEdit" Width="98%" MaxLength="50"></telerik:RadTextBox>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorPSN_Name" runat="server" ControlToValidate="RadTextBoxPSN_Name"
                                        ErrorMessage="请输入####" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                                <td class="infoHead">性别：
                                </td>
                                <td class="formItem_1">
                                    <telerik:RadTextBox ID="RadTextBoxPSN_Sex" runat="server" CssClass="textEdit" Width="98%" MaxLength="2"></telerik:RadTextBox>
                                </td>

                            </tr>
                            <tr>
                                <td class="infoHead">证件类型：
                                </td>
                                <td class="formItem_1">
                                    <telerik:RadTextBox ID="RadTextBoxPSN_CertificateType" runat="server" CssClass="textEdit" Width="98%" MaxLength="20"></telerik:RadTextBox>
                                </td>
                                <td class="infoHead">证件号码：
                                </td>
                                <td class="formItem_1">
                                    <telerik:RadTextBox ID="RadTextBoxPSN_CertificateNO" runat="server" CssClass="textEdit" Width="98%" MaxLength="30"></telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="infoHead">手机：
                                </td>
                                <td class="formItem_1">
                                    <telerik:RadTextBox ID="RadTextBoxPSN_MobilePhone" runat="server" CssClass="textEdit" Width="98%" MaxLength="100"></telerik:RadTextBox>
                                </td>
                                <td class="infoHead">邮箱：
                                </td>
                                <td class="formItem_1">
                                    <telerik:RadTextBox ID="RadTextBoxPSN_Email" runat="server" CssClass="textEdit" Width="98%" MaxLength="200"></telerik:RadTextBox>
                                </td>

                            </tr>
                            <tr>
                                <td class="infoHead">注册号：
                                </td>
                                <td class="formItem_1">
                                    <telerik:RadTextBox ID="RadTextBoxPSN_RegisterNO" runat="server" CssClass="textEdit" Width="98%" MaxLength="50"></telerik:RadTextBox>
                                </td>
                                <td class="infoHead">注册证书编号：
                                </td>
                                <td class="formItem_1">
                                    <telerik:RadTextBox ID="RadTextBoxPSN_RegisterCertificateNo" runat="server" CssClass="textEdit" Width="98%" MaxLength="50"></telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="infoHead">已注册专业：
                                </td>
                                <td class="formItem_1 w80" colspan="3" style="padding: 0; border-collapse: collapse;">
                                    <table width="100%" style="border-collapse: collapse;">
                                        <tr>
                                            <td class="formItem_1 la" style="width: 40%">专业
                                            </td>
                                            <td class="formItem_1 la" style="width: 60%">有效期</td>
                                        </tr>
                                        <tr>
                                            <td class="formItem_1">
                                                <telerik:RadTextBox ID="RadTextBoxPSN_RegisteProfession1" runat="server" CssClass="readonly" Width="98%" MaxLength="50" ReadOnly="true"></telerik:RadTextBox>
                                            </td>
                                            <td class="formItem_1">
                                                <telerik:RadTextBox ID="RadTextBoxPSN_CertificateValidity1" runat="server" CssClass="readonly" Width="98%" MaxLength="50" ReadOnly="true"></telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="formItem_1">
                                                <telerik:RadTextBox ID="RadTextBoxPSN_RegisteProfession2" runat="server" CssClass="readonly" Width="98%" MaxLength="50" ReadOnly="true"></telerik:RadTextBox>
                                            </td>
                                            <td class="formItem_1">
                                                <telerik:RadTextBox ID="RadTextBoxPSN_CertificateValidity2" runat="server" CssClass="readonly" Width="98%" MaxLength="50" ReadOnly="true"></telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="formItem_1">
                                                <telerik:RadTextBox ID="RadTextBoxPSN_RegisteProfession3" runat="server" CssClass="readonly" Width="98%" MaxLength="50" ReadOnly="true"></telerik:RadTextBox>

                                            </td>
                                            <td class="formItem_1">
                                                <telerik:RadTextBox ID="RadTextBoxPSN_CertificateValidity3" runat="server" CssClass="readonly" Width="98%" MaxLength="50" ReadOnly="true"></telerik:RadTextBox>
                                            </td>
                                        </tr>
                                    </table>

                                </td>

                            </tr>
                            <tr class="GridLightBK">
                                <td colspan="4" class="barTitle">补办信息</td>
                            </tr>
                            <tr>
                                <td class="infoHead">申请补办或更换内容：
                                </td>
                                <td colspan="3" align="left" class="formItem_2 w80">
                                    <asp:RadioButtonList ID="RadioButtonListReplaceType" runat="server" RepeatDirection="Horizontal" TextAlign="right">
                                        <asp:ListItem Text="注册证书" Value="注册证书" Selected="True"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td class="infoHead">补办原因：
                                </td>
                                <td colspan="3" align="left" class="formItem_2 w80">
                                    <asp:RadioButtonList ID="RadioButtonListReplaceReason" runat="server" RepeatDirection="Horizontal" TextAlign="right">
                                        <asp:ListItem Text="遗失" Value="遗失补办" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="污损" Value="污损补办"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td class="infoHead">有效期至：
                                </td>
                                <td colspan="3" align="left" class="formItem_2 w80">
                                    <telerik:RadTextBox ID="RadTextBoxDisnableDate" runat="server"></telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr class="GridLightBK" id="trFuJanTitel" runat="server" visible="false">
                                        <td colspan="4" class="barTitle">附件上传                                   
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
                                                一寸免冠照片：<span class="link" onclick="javascript:tips_pop('<%=string.Format("{0}/{1}",Model.EnumManager.FileDataType.一寸免冠照片,RadTextBoxPSN_CertificateNO.Text.Trim().Substring(RadTextBoxPSN_CertificateNO.Text.Trim().Length - 3, 3))%>','一寸免冠照片','','<%=ApplyID%>')">选择文件</span><span class="tishi">（要求：上传免冠照片扫描件原件、一寸jpg格式图片，名称不限，最大为50K，宽高110 X 140像素）</span>
                                            </div>

                                            <div class="fujian">
                                                证件扫描件：<span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.证件扫描件%>','证件扫描件','','<%=ApplyID%>')">选择文件</span><span class="tishi">（要求：上传身份证扫描件原件、jpg格式图片,最大500K）</span>
                                            </div>
                                            <div class="fujian">
                                                学历证书扫描件：<span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.学历证书扫描件%>','学历证书扫描件','','<%=ApplyID%>')">选择文件</span><span class="tishi">（要求：上传学历证书扫描件原件、jpg格式图片,最大500K）</span>
                                            </div>
                                            <%--<div class="fujian">
                                                遗失声明扫描件：<span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.遗失声明扫描件%>','遗失声明扫描件','','<%=ApplyID%>')">选择文件</span><span class="tishi">（要求：上传遗失声明扫描件原件、jpg格式图片,最大500K）</span>
                                            </div>--%>
                                            <div class="fujian">
                                                劳动合同扫描件：<span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.劳动合同扫描件%>','劳动合同扫描件','','<%=ApplyID%>')">选择文件</span><span class="tishi">（要求：上传劳动合同扫描件原件、只上传重要页,最多上传5页、jpg格式图片,每页最大500K）</span>
                                            </div>
                                            <div class="fujian">
                                                执业资格证书或资格考试合格通知书扫描件：<span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.执业资格证书扫描件%>','执业资格证书或资格考试合格通知书扫描件','','<%=ApplyID%>')">选择文件</span><span class="tishi">（要求：上传执业资格证书扫描件原件、jpg格式图片,最大500K）</span>
                                            </div>
                                            <div class="fujian">
                                                社保扫描件：<span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.社保扫描件%>','社保扫描件','','<%=ApplyID%>')">选择文件</span><span class="tishi">（要求：上传社保扫描件原件、非在本单位参保需上传相关社保证明）</span>
                                            </div>
                                            <div class="fujian">
                                                申请表扫描件：<span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.申请表扫描件%>','申请表扫描件','','<%=ApplyID%>')">选择文件</span><span class="tishi">（请在本页面导出打印申请表，单位盖章签字后扫描上传）</span>
                                            </div>
                                        </td>
                                    </tr>
                        </table>
                       <%-- <div id="divQY" visible="false" runat="server" style="width: 100%; padding-top: 20px; text-align: center; clear: both;">
                            <asp:Button ID="ButtonSave" runat="server" Text="保 存" CssClass="bt_large" UseSubmitBehavior="false" OnClientClick="if(CheckClientValidate()==true){this.value='正在提交';this.disabled=true;}" onclick="ButtonSave_Click" Enabled="false" /> 
                            &nbsp;&nbsp;<asp:Button ID="ButtonOutput" runat="server" Text="导出打印" CssClass="bt_large" OnClick="ButtonOutput_Click" Enabled="false" />
                            &nbsp;&nbsp;<asp:Button ID="ButtonApply" runat="server" Text="申 报" CssClass="bt_large" OnClick="ButtonApply_Click" Enabled="false" />
                            &nbsp;&nbsp;<asp:Button ID="ButtonDelete" runat="server" Text="删 除" CssClass="bt_large" OnClick="ButtonDelete_Click" Enabled="false" OnClientClick="javascript:if(!confirm('您确定要删除么?'))return false;" />
                        </div>--%>
                        <div id="divGR" visible="false" runat="server" style="width: 100%; padding-top: 20px; text-align: center; clear: both;">
                                <asp:Button ID="ButtonSave" runat="server" Text="保 存" CssClass="bt_large" UseSubmitBehavior="false" OnClientClick="if(CheckClientValidate()==true){this.value='正在提交';this.disabled=true;}" OnClick="ButtonSave_Click" Enabled="false" />
                                &nbsp;&nbsp;<asp:Button ID="ButtonOutput" runat="server" Text="导出打印" CssClass="bt_large" OnClick="ButtonOutput_Click" Enabled="false" />
                                  &nbsp;&nbsp;<asp:Button ID="ButtonUnit" runat="server" Text="提交单位确认" CssClass="bt_large" OnClick="ButtonUnit_Click" Enabled="false" />
                                &nbsp;&nbsp;<asp:Button ID="ButtonDelete" runat="server" Text="删 除" CssClass="bt_large" OnClick="ButtonDelete_Click" Enabled="false" OnClientClick="javascript:if(!confirm('您确定要删除么?'))return false;" CausesValidation="false" />
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
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn HeaderText="流程" UniqueName="Action" DataField="Action">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn HeaderText="办理人" UniqueName="ActionMan" DataField="ActionMan"  Display="false">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn HeaderText="办理时间" UniqueName="ActionData" DataField="ActionData">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn HeaderText="办理结果" UniqueName="ActionResult" DataField="ActionResult">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn HeaderText="办理意见" UniqueName="ActionRemark" DataField="ActionRemark">
                                                    </telerik:GridBoundColumn>
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
                                            <input id="BtnReturn" type="button" class="bt_large" value="取消提交" onclick='javascript: hideIfam()' />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="divQXCK" runat="server" visible="false" style="width: 98%; padding-top: 20px; text-align: center; clear: both;">
                            <table id="Table4" runat="server" width="100%" border="0" cellpadding="5" cellspacing="1" class="table" style="text-align: center; margin: 10px auto">
                                <tr class="GridLightBK">
                                    <td colspan="2" class="barTitle">区县审查操作</td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td width="20%" align="right">审查结果：</td>
                                    <td width="80%" align="left">
                                        <asp:RadioButtonList ID="RadioButtonListExamineResult" runat="server" RepeatDirection="Horizontal" TextAlign="right">
                                            <asp:ListItem Text="通过" Value="通过" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="不通过" Value="不通过"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td width="20%" align="right">审查意见：</td>
                                    <td width="80%" align="left">
                                        <asp:TextBox ID="TextBoxExamineRemark1" runat="server" TextMode="MultiLine" Rows="3" Width="99%" Text="允许通过"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td colspan="2" align="center">
                                        <asp:Button ID="BttSave" runat="server" CssClass="bt_large" Text="确认提交" OnClick="BttSave_Click" />&nbsp;&nbsp;
                                            <input id="BtnReturnck" type="button" class="bt_large" value="取消提交" onclick='javascript: hideIfam()' />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="divCheck" runat="server" visible="false" style="width: 98%; padding-top: 20px; text-align: center; clear: both;">

                            <table id="Table2" runat="server" width="100%" border="0" cellpadding="5" cellspacing="1" class="table" style="text-align: center; margin: 10px auto">
                                <tr class="GridLightBK">
                                    <td colspan="2" class="barTitle">审查操作</td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td width="20%" align="right">审查结果：</td>
                                    <td width="80%" align="left">
                                        <asp:RadioButtonList ID="RadioButtonListCheckResult" runat="server" RepeatDirection="Horizontal" TextAlign="right">
                                            <asp:ListItem Text="通过" Value="通过" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="不通过" Value="不通过"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td width="20%" align="right">审查意见：</td>
                                    <td width="80%" align="left">

                                        <asp:TextBox ID="TextBoxApplyCheckRemark" runat="server" TextMode="MultiLine" Rows="3" Width="99%" Text="允许通过"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td colspan="2" align="center">
                                        <asp:Button ID="ButtonCheck" runat="server" CssClass="bt_large" Text="确认提交" OnClick="ButtonCheck_Click" />&nbsp;&nbsp;
                                            <input id="BtnReturn3" type="button" class="bt_large" value="取消提交" onclick='javascript: hideIfam()' />
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
                                            <input id="BtnReturn4" type="button" class="bt_large" value="取消提交" onclick='javascript: hideIfam()' />
                                    </td>
                                </tr>
                            </table>

                        </div>
                        <div id="divViewAction" runat="server" visible="false" style="width: 100%; padding-top: 20px; text-align: center; clear: both;">
                            <input id="BtnReturn2" type="button" class="bt_large" value="返 回" onclick='javascript: hideIfam()' />
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
                                                                        <asp:ImageButton runat="server" ID="ImageButton1" ImageUrl="../images/Cancel.gif" CommandName="Delete" Visible='<%#(IfExistRoleID("0") == true && ((ViewState["ApplyMDL"] as Model.ApplyMDL).ApplyStatus == Model.EnumManager.ApplyStatus.未申报 || (ViewState["ApplyMDL"] as Model.ApplyMDL).ApplyStatus == Model.EnumManager.ApplyStatus.已驳回))?true:false%>' OnClientClick="javascript:if(confirm('您确定要删除么?')==false) return false" />

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

                //单位审核结果  南静  2019-10-28  添加
                $("#<%= RadioButtonListOldUnitCheckResult.ClientID%> input").each(function () {
                    $(this).click(function () {
                        var TextBoxOldUnitCheckRemark = $("#<%= TextBoxOldUnitCheckRemark.ClientID%>");

                    if ($(this).val() == "同意") {

                        TextBoxOldUnitCheckRemark.val("提交区县审核");
                    }
                    else {

                        TextBoxOldUnitCheckRemark.val("退回个人");

                    }
                });

            });




                //变换审核结果
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
                //变换区县审核
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
                //变换审核结果
                $("#<%= RadioButtonListCheckResult.ClientID%> input").each(function () {
                    $(this).click(function () {
                        var TextBoxApplyCheckRemark = $("#<%= TextBoxApplyCheckRemark.ClientID%>");

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
