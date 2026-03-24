<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserMaintain.aspx.cs" Inherits="ZYRYJG.SystemManage.UserMaintain" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" />
    <link href="../layer/skin/layer.css" rel="stylesheet" />
    <script type="text/javascript" src="../Scripts/jquery-3.4.1.min.js"></script>
    <script src="../layer/layer.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Windows7">
        </telerik:RadWindowManager>
        <div class="div_out">
            <div class="table_border" style="width: 90%; margin: 5px auto; padding-bottom: 30px;">
                <div class="dqts">
                    <div style="float: left;">
                        当前位置 &gt;&gt;系统管理 &gt;&gt;<strong>修改密码</strong>
                    </div>
                </div>
                <div class="content">
                    <p class="jbxxbt">
                        修改密码
                    </p>
                    <div class="DivTitleOn" onclick="DivOnOff(this,'Td3',event);" title="折叠">
                        功能说明
                    </div>
                    <div class="DivContent" id="Td3">

                        本页面用于【人员资格公网管理登录】的修改密码，对【住建委政务专网门户】的用户密码无法修改，修改专网密码请登录专网门户进行修改。<br />
                        公网：一般用户企业、个人登录申办业务。<br />
                        专网：住建委及相关管理机构用户登录审批业务。（特殊情况管理人员可以使用公网进行登录进行审批）
                    </div>
                    <div style="width: 95%; margin: 10px auto; text-align: center;">
                        <table id="TableEdit" runat="server" width="95%" border="0" cellpadding="5" cellspacing="1"
                            class="table" align="center">
                            <tr class="GridLightBK">
                                <td width="30%" nowrap="nowrap" align="right">用户名称：
                                </td>
                                <td width="70%">
                                    <asp:TextBox ID="TextBoxUserName" runat="server" Width="60%" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td width="30%" nowrap="nowrap" align="right">登录密码：
                                </td>
                                <td width="70%">
                                    <asp:TextBox ID="TextBoxPassWord" runat="server" Width="60%" TextMode="Password"
                                        ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td width="30%" nowrap="nowrap" align="right">
                                    <font color="red">*</font>新密码：
                                </td>
                                <td width="70%">
                                    <asp:TextBox ID="TextBoxNewPassWord" runat="server" Width="60%" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="！"
                                        ControlToValidate="TextBoxNewPassWord"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td width="30%" nowrap="nowrap" align="right">
                                    <font color="red">*</font>重复密码：
                                </td>
                                <td width="70%">
                                    <asp:TextBox ID="TextBoxRePassWord" runat="server" Width="60%" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="！"
                                        ControlToValidate="TextBoxRePassWord"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="两次密码不一致！"
                                        ControlToValidate="TextBoxRePassWord" ControlToCompare="TextBoxNewPassWord"></asp:CompareValidator>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td colspan="2" align="center">
                                    <asp:Button ID="ButtonSave" runat="server" Text="保存" CssClass="button" OnClick="ButtonSave_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <div id="winpop">
        </div>
    </form>
</body>
</html>
