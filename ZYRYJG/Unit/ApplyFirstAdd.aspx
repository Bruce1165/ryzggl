<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplyFirstAdd.aspx.cs" Inherits="ZYRYJG.Unit.ApplyFirstAdd" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../CheckAll.ascx" TagName="CheckAll" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Css/styleRed.css?v=1.0.126" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
    <script src="../Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
    <link href="../layer/skin/layer.css" rel="stylesheet" />
    <script src="../layer/layer.js" type="text/javascript"></script>
    <style type="text/css">
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

        .auto-style2 {
            margin-right: 40px;
            text-align: right;
        }

        .auto-style1 {
            width: 60px;
            font-size: 12px;
            background-color: #5C9DD3;
            border: 1px solid #2F8399;
            border-radius: 3px;
            color: white;
            cursor: pointer;
        }

        .auto-style3 {
            width: 100%;
            text-align: right;
        }

        .step1 {
        }
    </style>

</head>
<body style="min-height: 584px;">
    <form id="form1" runat="server" enctype="multipart/form-data">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Windows7">
        </telerik:RadWindowManager>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server"
            EnableAJAX="true">
            <AjaxSettings>
               <%-- <telerik:AjaxSetting AjaxControlID="RadioButtonListENT_ContractType">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadDatePickerENT_ContractENDTime" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="RadioButtonListENT_ContractType" />
                        <telerik:AjaxUpdatedControl ControlID="LabelJZSJ" />
                        <telerik:AjaxUpdatedControl ControlID="ValidatorENT_ContractENDTime" />

                    </UpdatedControls>
                </telerik:AjaxSetting>--%>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <div class="div_out">
            <div id="div_top" class="dqts">
                <div style="float: left;">
                    当前位置 &gt;&gt;二级建造师注册 &gt;&gt;<strong>初始注册</strong>
                </div>
            </div>
            <div class="content">
                <p class="jbxxbt">
                    初始注册
                </p>
                <div class="step">
                    <div class="stepLabel">办理进度：</div>
                    <div id="step_未申报" runat="server" class="stepItem lgray">个人填写></div>
                    <div id="step_待确认" runat="server" class="stepItem lgray">待单位审核></div>
                    <div id="step_已申报" runat="server" class="stepItem lgray">已申报></div>
                    <div id="step_已受理" runat="server" class="stepItem lgray">区级受理></div>
                    <div id="step_区县审查" runat="server" class="stepItem lgray">区级审核></div>
                    <div id="step_已上报" runat="server" class="stepItem lgray">汇总上报></div>
                    <div id="step_已审查" runat="server" class="stepItem lgray">市级审核></div>
                    <div id="step_已决定" runat="server" class="stepItem lgray">市级决定></div>
                    <div id="step_已公示" runat="server" class="stepItem lgray">公示></div>
                    <div id="step_已公告" runat="server" class="stepItem lgray">公告></div>
                    <div id="step_已放号" runat="server" class="stepItem lgray">注册号发放（办结）</div>
                    <div class="stepArrow">▶</div>
                </div>
                <div class="code">
                    <table id="Tablezjhm0" runat="server" border="0" cellpadding="5" cellspacing="1" visible="false" width="100%">
                        <tr class="GridLightBK">
                            <td>
                                <div style="line-height: 30px;">操作提示：请勾选您要注册专业</div>
                                <telerik:RadGrid ID="RadGridHZSB" runat="server" AutoGenerateColumns="False" CellPadding="0" GridLines="None" PagerStyle-AlwaysVisible="False" Width="100%" Skin="Default" EnableAjaxSkinRendering="true"
                                    EnableEmbeddedSkins="true">
                                    <ClientSettings EnableRowHoverStyle="true">
                                    </ClientSettings>
                                    <MasterTableView CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Left" DataKeyNames="XM,ZJHM,ZYLB,ZGZSBH,QDNF,BYXX,BYSJ,SXZY,ZGXL,QFSJ" NoMasterRecordsText=" 没有可显示的记录">
                                        <Columns>
                                            <telerik:GridTemplateColumn UniqueName="SelectAllColumn">
                                                <HeaderTemplate>
                                                    <uc3:CheckAll ID="CheckAll1" runat="server" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CheckBox1" runat="server" CssClass="ck" onclick="checkBoxClick(this.checked);" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="36" />
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="XM" HeaderText="姓名" UniqueName="XM">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ZJHM" HeaderText="身份证号" UniqueName="ZJHM">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ZYLB" HeaderText="专业类别" UniqueName="ZYLB">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ZGZSBH" HeaderText="资格证书编号/管理号" UniqueName="ZGZSBH">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="QFSJ" HeaderText="签发日期" UniqueName="QFSJ" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="left">
                                <div runat="server" id="Tablezjhm" visible="false">
                                    <div class="DivContent" id="Div1">
                                        1、直接点击"请选择一个单位"选择注册企业点击"创建申请表"。<br />
                                        2、输入企业的18位统一社会信用代码或者9位组织机构代码数字、大写字母组合（不带“-”横杠）点击"创建申请表"；不知道组织机构代码的的请登录 <a title="组织机构代码查询" href="https://www.cods.org.cn"
                                            target="_blank" style="color: Blue; text-decoration: underline;">https://www.cods.org.cn</a>
                                        网站，在“信息核查”栏目中查询；<br />
                                    </div>
                                    <div>第一步：选择注册单位</div>
                                    <div style="padding-left: 20px; line-height: 200%">
                                        方式1：<input id="ButtonSelectQY" type="button" value="请选择一个单位" class="bt_maxlarge" onclick="javascript: selectQY();" />
                                    </div>
                                    <div style="padding-left: 20px; line-height: 200%">
                                        方式2：输入统一社会信用代码机构代码:<telerik:RadTextBox ID="RadTextBoxFindENT_OrganizationsCode" Width="250px" runat="server"></telerik:RadTextBox>
                                        <div style="display: none;">
                                            企业名称:
                                     <telerik:RadTextBox ID="RadTextBoxUnitName" Width="250px" runat="server" AutoPostBack="true"></telerik:RadTextBox>
                                            <asp:RequiredFieldValidator ID="ValidatorCreditCode" runat="Server" ControlToValidate="RadTextBoxFindENT_OrganizationsCode"
                                                ErrorMessage="请输入单位组织机构代码" Display="Dynamic">*请输入单位组织机构代码</asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div style="line-height: 200%">
                                        第二步：&nbsp;&nbsp;<asp:Button ID="ButtonSaveZjh" runat="server" Text="创建申请表" CssClass="bt_maxlarge" CausesValidation="false" OnClick="ButtonSaveZjh_Click" />
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="divBase" runat="server" style="width: 100%; margin: 0; text-align: center; overflow: hidden;">
                    <p class="auto-style2">
                        申请编号：<asp:Label ID="LabelApplyCode" runat="server" Text=""></asp:Label>
                    </p>
                    <div style="width: 66%; float: left; clear: left">
                        <table runat="server" id="AddTable" width="100%" border="0" cellpadding="5" cellspacing="1" class="table" style="text-align: center;">
                            <tr class="GridLightBK">
                                <td colspan="4" class="barTitle">基本信息</td>
                            </tr>
                            <tr class="GridLightBK">
                                <td width="22%" align="right">人员类型</td>
                                <td width="28%" align="left">
                                    <telerik:RadComboBox ID="RadComboBoxArchitectType" runat="server" Width="98%">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="二级注册建造师" Value="二级" Selected="true" />
                                            <telerik:RadComboBoxItem Text="二级注册临时建造师 " Value="二级临时" Enabled="false" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td align="right">民族</td>
                                <td align="left">
                                    <telerik:RadComboBox ID="RadComboBoxNation" runat="server" Width="98%" NoWrap="true" DropDownCssClass="multipleRowsColumns" DropDownWidth="620px" Height="300px">
                                    </telerik:RadComboBox>
                                    <asp:CompareValidator ValueToCompare="请选择" Operator="NotEqual" ControlToValidate="RadComboBoxNation"
                                        ErrorMessage="必填" runat="server" ID="Comparevalidator4" ForeColor="Red" Display="Dynamic" />
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td align="right">证件号码</td>
                                <td align="left">
                                    <telerik:RadTextBox ID="RadTextBoxPSN_CertificateNO" runat="server" Width="98%" MaxLength="50"></telerik:RadTextBox>
                                </td>
                                <td width="22%" align="right">姓名</td>
                                <td width="28%" align="left">
                                    <telerik:RadTextBox ID="RadTextBoxPSN_Name" runat="server"></telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="ValidatorPSN_Name" runat="Server" ControlToValidate="RadTextBoxPSN_Name"
                                        ErrorMessage="请输入姓名" Display="Dynamic">*请输入姓名 </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td width="22%" align="right">性别</td>
                                <td width="28%" align="left">
                                    <telerik:RadComboBox ID="RadComboBoxPSN_Sex" runat="server" Width="100%">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="男" Value="男" />
                                            <telerik:RadComboBoxItem Text="女" Value="女" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td width="22%" align="right">出生年月</td>
                                <td width="28%" align="left">
                                    <telerik:RadDatePicker ID="RadDatePickerBirthday" runat="server" MinDate="1900-1-1" MaxDate="2050-1-1" Calendar-DayCellToolTipFormat="yyyy年MM月dd日">
                                    </telerik:RadDatePicker>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td colspan="4" class="barTitle">联系信息</td>
                            </tr>
                            <tr class="GridLightBK">
                                <td align="right">手机</td>
                                <td align="left">
                                    <telerik:RadTextBox ID="RadTextBoxPSN_MobilePhone" runat="server"></telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="ValidatorPSN_MobilePhone" runat="Server" ControlToValidate="RadTextBoxPSN_MobilePhone"
                                        ErrorMessage="请输入手机号码" Display="Dynamic">*请输入手机号码</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="SJH" runat="Server" Display="Dynamic" ControlToValidate="RadTextBoxPSN_MobilePhone" ValidationExpression="0?(13|14|15|16|17|18|19)[0-9]{9}" ErrorMessage="*手机号码错误">
                                    </asp:RegularExpressionValidator>
                                </td>
                                <td align="right">联系电话</td>
                                <td align="left">
                                    <telerik:RadTextBox ID="RadTextBoxPSN_Telephone" runat="server"></telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td align="right">电子邮箱</td>
                                <td align="left" colspan="3">
                                    <telerik:RadTextBox ID="RadTextBoxPSN_Email" runat="server" Width="98%"></telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="ValidatorPSN_Email" runat="Server" ControlToValidate="RadTextBoxPSN_Email"
                                        ErrorMessage="请输入电子邮箱" Display="Dynamic">*请输入电子邮箱</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="DZYX" runat="Server" Display="Dynamic" ControlToValidate="RadTextBoxPSN_Email" ValidationExpression="\w[-\w.+]*@([A-Za-z0-9][-A-Za-z0-9]+\.)+[A-Za-z]{2,14}" ErrorMessage="*邮箱地址错误">
                                    </asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td colspan="4" class="barTitle">学历信息</td>
                            </tr>
                            <tr class="GridLightBK">
                                <td align="right">毕业院校</td>
                                <td align="left" colspan="3">
                                    <telerik:RadTextBox ID="RadTextBoxSchool" runat="server" Width="98%"></telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="dValidatorSchool" runat="Server" ControlToValidate="RadTextBoxSchool"
                                        ErrorMessage="请输入毕业院校" Display="Dynamic">*请输入毕业院校</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td align="right">所学专业</td>
                                <td align="left">
                                    <telerik:RadTextBox ID="RadTextBoxMajor" runat="server" Width="98%"></telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="ValidatorMajor" runat="Server" ControlToValidate="RadTextBoxMajor"
                                        ErrorMessage="请输入所学专业" Display="Dynamic">*请输入所学专业 </asp:RequiredFieldValidator>
                                </td>
                                <td align="right">毕业（肄、结）时间</td>
                                <td align="left">
                                    <telerik:RadDatePicker ID="RadDatePickerGraduationTime" runat="server" MinDate="1900-1-1" MaxDate="2050-1-1" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"></telerik:RadDatePicker>
                                    <asp:RequiredFieldValidator ID="ValidatorGraduationTime" runat="Server" ControlToValidate="RadDatePickerGraduationTime"
                                        ErrorMessage="请输入姓名" Display="Dynamic">*请输入毕业（肄、结）时间</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td align="right">最高学历</td>
                                <td align="left">
                                    <telerik:RadComboBox ID="RadComboBoxXueLi" runat="server" Width="98%" NoWrap="true">
                                    </telerik:RadComboBox>
                                    <asp:CompareValidator ValueToCompare="请选择" Operator="NotEqual" ControlToValidate="RadComboBoxXueLi"
                                        ErrorMessage="必填" runat="server" ID="Comparevalidator3" ForeColor="Red" Display="Dynamic" />
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td colspan="4" class="barTitle">聘用企业信息</td>
                            </tr>
                            <tr class="GridLightBK">
                                <td align="right">企业名称</td>
                                <td align="left" colspan="3">
                                    <telerik:RadTextBox ID="RadTextBoxENT_Name" runat="server" ReadOnly="true" CssClass="ReadOnly" Width="98%"></telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td align="right">机构代码</td>
                                <td align="left" colspan="3">
                                    <telerik:RadTextBox ID="RadTextBoxENT_OrganizationsCode" runat="server" ReadOnly="true" CssClass="ReadOnly" Width="98%"></telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td align="right">工商注册地址</td>
                                <td align="left" colspan="3">
                                    <telerik:RadTextBox ID="RadTextBoxEND_Addess" runat="server" ReadOnly="true" CssClass="ReadOnly" Width="98%"></telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td align="right">法定代表人</td>
                                <td align="left">
                                    <telerik:RadTextBox ID="RadTextBoxFR" runat="server" ReadOnly="true" CssClass="ReadOnly" Width="98%"></telerik:RadTextBox>
                                </td>
                                <td align="right">企业性质</td>
                                <td align="left">
                                    <telerik:RadTextBox ID="RadTextBoxENT_Economic_Nature" runat="server" ReadOnly="true" CssClass="ReadOnly" Width="98%"></telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td align="right">企业类型</td>
                                <td align="left">
                                    <telerik:RadTextBox ID="RadTextBoxENT_Type" runat="server" ReadOnly="true" CssClass="ReadOnly" Width="98%"></telerik:RadTextBox>
                                </td>
                                <td align="right">资质类别</td>
                                <td align="left">
                                    <telerik:RadTextBox ID="RadTextBoxENT_Sort" runat="server" ReadOnly="true" CssClass="ReadOnly" Width="98%"></telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td align="right">资质等级</td>
                                <td align="left">
                                    <telerik:RadTextBox ID="RadTextBoxENT_Grade" runat="server" ReadOnly="true" CssClass="ReadOnly" Width="98%"></telerik:RadTextBox>
                                </td>
                                <td align="right">资质证书编号</td>
                                <td align="left">
                                    <telerik:RadTextBox ID="RadTextBoxENT_QualificationCertificateNo" runat="server" ReadOnly="true" CssClass="ReadOnly" Width="98%"></telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td align="right">联系人</td>
                                <td align="left">
                                    <telerik:RadTextBox ID="RadTextBoxLinkMan" runat="server" ReadOnly="true" CssClass="ReadOnly" Width="98%"></telerik:RadTextBox>
                                </td>
                                <td align="right">联系电话</td>
                                <td align="left">
                                    <telerik:RadTextBox ID="RadTextBoxENT_Telephone" runat="server" ReadOnly="true" CssClass="ReadOnly" Width="98%"></telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td align="right">通讯地址</td>
                                <td align="left" colspan="3">
                                    <telerik:RadTextBox ID="RadTextBoxENT_Correspondence" runat="server" ReadOnly="true" CssClass="ReadOnly" Width="98%"></telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td align="right">邮政编码</td>
                                <td align="left" colspan="3">
                                    <telerik:RadTextBox ID="RadTextBoxENT_Postcode" runat="server" ReadOnly="true" CssClass="ReadOnly" Width="98%"></telerik:RadTextBox>
                                </td>
                            </tr>
                            <%--<tr class="GridLightBK">
                                <td align="right">合同类型</td>
                                <td align="left" colspan="3">
                                    <asp:RadioButtonList ID="RadioButtonListENT_ContractType" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="RadioButtonListENT_ContractType_SelectedIndexChanged">
                                        <asp:ListItem Text="固定期限" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="无固定期限" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="以完成一定工作任务为期限" Value="3"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td align="right">劳动合同开始时间</td>
                                <td align="left">
                                    <telerik:RadDatePicker ID="RadDatePickerENT_ContractStartTime" runat="server" MinDate="1900-1-1" MaxDate="2090-1-1" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"></telerik:RadDatePicker>
                                    <asp:RequiredFieldValidator ID="ValidatorENT_ContractStartTime" runat="Server" ControlToValidate="RadDatePickerENT_ContractStartTime"
                                        ErrorMessage="请输入劳动合同开始时间" Display="Dynamic">*请输入劳动合同开始时间</asp:RequiredFieldValidator>
                                </td>
                                <td align="right">
                                    <asp:Label ID="LabelJZSJ" runat="server" Text="劳动合同结束时间"></asp:Label></td>
                                <td align="left">
                                    <telerik:RadDatePicker ID="RadDatePickerENT_ContractENDTime" runat="server" MinDate="1900-1-1" MaxDate="2090-1-1" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"></telerik:RadDatePicker>
                                    <asp:RequiredFieldValidator ID="ValidatorENT_ContractENDTime" runat="Server" ControlToValidate="RadDatePickerENT_ContractENDTime"
                                        ErrorMessage="请输入劳动合同结束时间" Display="Dynamic">*请输入劳动合同结束时间</asp:RequiredFieldValidator>
                                </td>
                            </tr>--%>
                            <tr class="GridLightBK">
                                <td colspan="4" class="barTitle">执业资格证书信息</td>
                            </tr>
                            <tr class="GridLightBK">
                                <td align="right">资格证书专业类别</td>
                                <td align="left">
                                    <asp:Label ID="LabelZhuanYe" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="right">申请注册专业</td>
                                <td align="left">
                                    <asp:Label ID="LabelPSN_RegisteProfession" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td align="right">证书编号/管理号</td>
                                <td align="left">
                                    <asp:Label ID="LabelZSBH" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="right">签发日期</td>
                                <td align="left">
                                    <asp:Label ID="LabelConferDate" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td colspan="4" style="color: #434343; text-align: center">继续教育情况（逾期注册）</td>
                            </tr>
                            <tr class="GridLightBK">
                                <td align="right">必修课（学时）</td>
                                <td align="left">
                                    <asp:Label ID="LabelBiXiu" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="right">选修课（学时）</td>
                                <td align="left">
                                    <asp:Label ID="LabelXuanXiu" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td colspan="4" class="barTitle">其它专业考试合格证明信息                                   
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td colspan="4" style="border-collapse: collapse; padding: 0!important; margin: 0!important;">
                                    <telerik:RadGrid ID="RadGridExamInfo" runat="server" ShowHeader="false" CellPadding="0" CellSpacing="0"
                                        GridLines="None" AllowPaging="False" AllowSorting="False" AutoGenerateColumns="False" OnDeleteCommand="RadGridExamInfo_DeleteCommand"
                                        Width="100%" EnableEmbeddedSkins="false" PagerStyle-AlwaysVisible="False">
                                        <ClientSettings EnableRowHoverStyle="False">
                                        </ClientSettings>
                                        <MasterTableView NoMasterRecordsText="" CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Left">
                                            <Columns>
                                                <telerik:GridTemplateColumn UniqueName="ExamInfo">
                                                    <ItemTemplate>
                                                        <table class="subtable" cellpadding="5" cellspacing="1" style="width: 100%; border-collapse: collapse; margin: 0; padding: 0;">
                                                            <tr class="GridLightBK">
                                                                <td align="right" style="width: 25%">考试合格证明专业类别</td>
                                                                <td align="left" style="width: 25%">
                                                                    <asp:Label ID="LabelKSZY" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td align="right" style="width: 25%">申请注册专业
                                                                </td>
                                                                <td align="left" style="width: 25%">
                                                                    <asp:Label ID="LabelSQZCZY" runat="server" Text=""></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr class="GridLightBK">
                                                                <td align="right" style="width: 25%">证书编号/管理号</td>
                                                                <td align="left" style="width: 25%">
                                                                    <asp:Label ID="LabelKSHGZMBH" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td align="right" style="width: 25%">签发日期</td>
                                                                <td align="left" style="width: 25%">
                                                                    <asp:Label ID="LabelKSQFRQ" runat="server" Text=""></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr class="GridLightBK">
                                                                <td colspan="4" style="color: #434343; text-align: center">继续教育情况（逾期注册）</td>
                                                            </tr>
                                                            <tr class="GridLightBK">
                                                                <td align="right" style="width: 25%">必修课（学时）</td>
                                                                <td align="left" style="width: 25%">
                                                                    <asp:Label ID="LabelBiXiu" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td align="right" style="width: 25%">选修课（学时）</td>
                                                                <td align="left">
                                                                    <asp:Label ID="LabelXuanXiu" runat="server" Text=""></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridTemplateColumn>
                                            </Columns>
                                            <HeaderStyle Font-Bold="True" />
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </td>
                            </tr>
                            <tr class="GridLightBK" id="trFuJanTitel" runat="server" visible="false">
                                <td colspan="4" class="barTitle">附件上传<span style="color: red">（所有电子证书扫描件要求与原件1:1比例正向扫描上传,信息清晰完整）</span>
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
                                        <span style="color: Red">* </span>一寸免冠照片：<span class="link" onclick="javascript:tips_pop('<%=string.Format("{0}/{1}", Model.EnumManager.FileDataType.一寸免冠照片, WorkerCertificateCode.Trim().Substring(WorkerCertificateCode.Trim().Length - 3, 3))%>','一寸免冠照片','','<%=ApplyID%>')">选择文件</span><span class="tishi">（要求：上传免冠照片扫描件原件、一寸jpg格式图片，名称不限，最大为50K，宽高110 X 140像素）</span>
                                    </div>
                                    <div class="fujian">
                                        <span style="color: Red">* </span>证件扫描件：<span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.证件扫描件%>','证件扫描件','','<%=ApplyID%>')">选择文件</span><span class="tishi">（要求：上传身份证扫描件原件、jpg格式图片,最大500K）</span>
                                    </div>
                                    <div class="fujian">
                                        学历证书扫描件：<span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.学历证书扫描件%>','学历证书扫描件','','<%=ApplyID%>')">选择文件</span><span class="tishi">（要求：上传学历证书扫描件原件、jpg格式图片,最大500K）</span>
                                    </div>
                                    <div class="fujian">
                                        劳动合同扫描件：<span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.劳动合同扫描件%>','劳动合同扫描件','','<%=ApplyID%>')">选择文件</span><span class="tishi">（<span style="color: Red">已退休人员需提供劳动合同或相关劳动关系证明。</span>要求：上传劳动合同扫描件原件、只上传重要页,最多上传5页、jpg格式图片,每页最大500K）</span>
                                    </div>
                                    <div class="fujian">
                                        执业资格证书或资格考试合格通知书扫描件：<span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.执业资格证书扫描件%>','执业资格证书或资格考试合格通知书扫描件','','<%=ApplyID%>')">选择文件</span><span class="tishi">（要求：上传执业资格证书扫描件原件、jpg格式图片,最大500K）</span>
                                    </div>
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
                                    <div class="fujian">
                                        继续教育承诺书扫描件：<span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.继续教育承诺书扫描件%>','继续教育承诺书扫描件','','<%=ApplyID%>')">选择文件</span><span class="tishi">（要求：逾期申请初始注册（自资格证书批准之日起三年内未提出初始注册申请）的，上传继续教育承诺书扫描件原件、jpg格式图片,最大500K）</span><a target="_blank" href="../Template/二建继续教育承诺书.docx">《二建继续教育承诺书模板》</a>

                                    </div>
                                    <div id="cns" class="fujian" visible="false" runat="server">
                                        新设立企业建造师注册承诺书：<span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.新设立企业建造师注册承诺书%>','新设立企业建造师注册承诺书','','<%=ApplyID%>')">选择文件</span><span class="tishi">（要求：请先下载新设立企业建造师注册承诺书，单位盖章签字后扫描上传）</span><a target="_blank" href="../Template/新设立企业建造师注册承诺书.docx">《新设立企业建造师注册承诺书》</a>
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <div id="divGR" visible="false" runat="server" style="width: 100%; padding-top: 20px; text-align: center; clear: both;">
                            <div id="divStepDesc" runat="server" style="line-height: 300%;">操作流程：个人申请保存-->点击导出打印(导出申请表)-->上传相关附件-->提交单位审核</div>
                            <asp:Button ID="ButtonSave" runat="server" Text="保 存" CssClass="bt_large" UseSubmitBehavior="false" OnClientClick="if(CheckClientValidate()==true){ this.value='正在提交';this.disabled=true;}" OnClick="ButtonSave_Click" />
                            &nbsp;&nbsp;<asp:Button ID="ButtonOutput" runat="server" Text="导出打印" CssClass="bt_large" OnClick="ButtonOutput_Click" Enabled="false" />
                            &nbsp;&nbsp;<asp:Button ID="ButtonUnit" runat="server" Text="提交单位确认" CssClass="bt_large" OnClick="ButtonUnit_Click" Enabled="false" OnClientClick="javascript:if(this.value=='提交单位确认'){if(confirm('修改申请表填报内容后，请先保存再提交单位确认，否则无法记录修改内容。\r\n\r\n确定要提交单位审核吗？')==false) return false;} else {if(confirm('确认要取消申报吗？')==false) return false;}" />
                            &nbsp;&nbsp;<asp:Button ID="ButtonDelete" runat="server" Text="删 除" CssClass="bt_large" OnClick="ButtonDelete_Click" Enabled="false" OnClientClick="javascript:if(!confirm('您确定要删除么?'))return false;" />
                        </div>
                        <div id="divCheckHistory" visible="true" runat="server" style="width: 100%; padding-top: 20px; text-align: center; clear: both;">
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
                            <div style="font-size: 20px">
                                <a href="https://zjw.beijing.gov.cn/bjjs/gcjs/kszczn/tg/ejjzs/index.shtml" target="_blank" style="color: red"><span id="blink">【最新】</span>北京市二级建造师注册审查公告文件</a>
                                <%--<span style="padding-left:50px"><a href="http://120.52.185.14/Upload/PolicyNews/北京市二级建造师注册申诉表（2021版）.doc" target="_blank">【北京市二级建造师注册申诉表（2021版）】</a></span>--%>
                            </div>
                            <div style="line-height: 300%;"><a href="http://120.52.185.14/Upload/PolicyNews/北京市二级建造师执业资格合格人员信息修改申请表（2025版）.doc?read=<%=ZYRYJG.UIHelp.GetReadParam() %>" target="_blank">【北京市二级建造师执业资格合格人员信息修改申请表（2025版）.doc】</a></div>
                            <div style="line-height: 180%; text-align: left; background-color: #efefef; border: 1px solid #e9e9e9; border-radius: 12px 12px; padding: 12px 20px; color: red; margin: 16px 16px">
                                <b>提示：</b>申报事项公告通过后，申请人可在 电子证书 下载栏目中生成该电子证书使用有效期范围，公告后24小时内未自行填写，系统将会自动生成最大时间范围的使用有效期。新电子证书生成后，申请人也可以根据自身需求再次调整证书使用有效期时间范围。
                            </div>
                        </div>
                        <div id="divUnit" runat="server" visible="false" style="padding-top: 20px; text-align: center; clear: both;" class="auto-style3">
                            <table id="Table6" runat="server" width="100%" border="0" cellpadding="5" cellspacing="1" class="table" style="text-align: center; margin: 10px auto">
                                <tr class="GridLightBK">
                                    <td colspan="2" class="barTitle">单位确认</td>
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

                                        <asp:TextBox ID="TextBoxOldUnitCheckRemark" runat="server" TextMode="MultiLine" Rows="3" Width="99%" MaxLength="100" Text="提交区县审核"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td colspan="2" align="center">
                                        <asp:Button ID="ButtonApply" runat="server" CssClass="bt_large" Text="确 定" OnClick="ButtonApply_Click" />&nbsp;&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="divQX" runat="server" visible="false" style="padding-top: 20px; text-align: center; clear: both;" class="auto-style3">
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
                        <div id="divCheck" runat="server" visible="false" style="width: 100%; padding-top: 20px; text-align: center; clear: both;">

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
                        <div id="divDecide" runat="server" visible="false" style="width: 100%; padding-top: 20px; text-align: center; clear: both;">
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
                        <div id="divSendBack" runat="server" visible="false" style="width: 98%; padding-top: 20px; text-align: center; clear: both;">
                            <table id="Table5" width="100%" border="0" cellpadding="5" cellspacing="1" class="table" style="text-align: center; margin: 10px auto">
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
                                                <telerik:RadComboBoxItem Text="区县审查" Value="区县审查" />
                                                <telerik:RadComboBoxItem Text="已上报" Value="已上报" />
                                                <telerik:RadComboBoxItem Text="已审查" Value="已审查" />
                                                <telerik:RadComboBoxItem Text="已决定" Value="已决定" />
                                                <telerik:RadComboBoxItem Text="已公告" Value="已公告" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td colspan="2" align="center">
                                        <asp:Button ID="ButtonSendBack" runat="server" CssClass="bt_large" Text="执行后退" OnClick="ButtonSendBack_Click" OnClientClick="javascript:if(!confirm('您确定要后退么?')) return false;" CausesValidation="false" />&nbsp;&nbsp;
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
                                            <div class="DivTitleOn" onclick="DivOnOff(this,'Div<%# Eval("DataType")%>',event);" title="折叠">
                                                <%# Eval("DataType")%>
                                            </div>
                                            <div class="DivContent" id="Div<%# Eval("DataType")%>" style="position: relative;">
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
                                                                    <asp:ImageButton runat="server" ID="ImageButton1" ImageUrl="../images/Cancel.gif" CommandName="Delete" Visible='<%#(IfExistRoleID("0") == true && ((ViewState["ApplyMDL"] as Model.ApplyMDL).ApplyStatus == Model.EnumManager.ApplyStatus.未申报 || (ViewState["ApplyMDL"] as Model.ApplyMDL).ApplyStatus == Model.EnumManager.ApplyStatus.已驳回)) ? true : false%>' OnClientClick="javascript:if(confirm('您确定要删除么?')==false) return false" />

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
            document.getElementById("blink").style.color = color[colornum];
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
            //单位审核结果
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
        });
        function checkBoxAllClick(checkBoxAllClientID) {
            if (checkBoxAllClientID == undefined) return;
            var ckall = document.getElementById(checkBoxAllClientID);
            if (ckall == null) return;
            var grid = ckall.parentNode;
            while (grid != null && grid != undefined && grid.nodeName != "div") {
                grid = grid.parentNode;
            }
            var ifSelect = ckall.checked;
            var Chks;
            if (grid == undefined)
                Chks = document.getElementsByTagName("input");
            else
                Chks = grid.getElementsByTagName("input");

            if (Chks.length) {
                for (i = 0; i < Chks.length; i++) {
                    if (Chks[i].type == "checkbox") {
                        Chks[i].checked = ifSelect;
                    }
                }
            }
            else if (Chks) {
                if (Chks.type == "checkbox") {
                    Chks.checked = ifSelect;
                }
            }
        }
        //选择企业
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

                        var RadTextBoxCreditCode = $find("<%= RadTextBoxFindENT_OrganizationsCode.ClientID%>");
                        var RadTextBoxUnitName = $find("<%= RadTextBoxUnitName.ClientID%>");
                        RadTextBoxCreditCode.set_value(data.ENT_OrganizationsCode);
                        RadTextBoxUnitName.set_value(data.ENT_Name);

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
        //选择文件
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
