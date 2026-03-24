<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserManage.aspx.cs" Inherits="ZYRYJG.SystemManage.UserManage" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
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

        <%--<telerik:RadWindowManager ID="Singleton" Skin="Windows7" Width="400" Height="430"
            VisibleStatusbar="false" Behaviors="Close,Move, Resize" runat="server">
        </telerik:RadWindowManager>--%>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="RadGrid1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
        </telerik:RadAjaxLoadingPanel>
        <div style="margin-top: 5px;">
            <div class="table_border" style="width: 99%;">
                <p class="jbxxbt">
                    用户管理
                </p>
                <div style="width: 98%; margin: 0 auto; padding-bottom: 30px;">
                    <p class="table_cx">
                        <img src="../images/Soft_common.gif" style="margin-bottom: -2px; padding-right: 2px;"
                            alt="" />机构信息
                    </p>
                    <div class="blue_center" style="width: 95%; margin: 0 auto;">
                        <div>
                            <b class="subxtop"><b class="subxb1"></b><b class="subxb2"></b><b class="subxb3"></b>
                                <b class="subxb4"></b></b>
                        </div>
                        <div class="subxboxcontent">
                            <div style="width: 98%; margin: 0 auto; padding: 5px;">
                                <table width="100%" border="0" align="center" cellpadding="5" cellspacing="1" class="table">
                                    <tr class="GridLightBK">
                                        <td style="width: 15%;" nowrap="nowrap" class="tousu_content">机构名称：
                                        </td>
                                        <td width="35%">
                                            <asp:Label ID="lblOrganName" runat="server"></asp:Label>
                                        </td>
                                        <td style="width: 15%;" nowrap="nowrap" class="tousu_content">机构编码：
                                        </td>
                                        <td width="35%">
                                            <asp:Label ID="lblOrganCoding" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="GridLightBK">
                                        <td style="width: 15%;" nowrap="nowrap" class="tousu_content">机构描述：
                                        </td>
                                        <td colspan="3">
                                            <asp:Label ID="lblOrganDescription" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="GridLightBK">
                                        <td style="width: 15%;" nowrap="nowrap" class="tousu_content">机构性质：
                                        </td>
                                        <td>
                                            <asp:Label ID="lblOrganNature" runat="server"></asp:Label>
                                        </td>
                                        <td style="width: 15%;" nowrap="nowrap" class="tousu_content">机构地址：
                                        </td>
                                        <td>
                                            <asp:Label ID="lblOrganAddress" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="GridLightBK">
                                        <td style="width: 15%;" nowrap="nowrap" class="tousu_content">联系电话：
                                        </td>
                                        <td>
                                            <asp:Label ID="lblOrganTelphone" runat="server"></asp:Label>
                                        </td>
                                        <td style="width: 15%;" nowrap="nowrap" class="tousu_content">行政编码：
                                        </td>
                                        <td>
                                            <asp:Label ID="lblOrganCode" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div>
                            <b class="subxbottom"></b><b class="subxb4"></b><b class="subxb3"></b><b class="subxb2"></b><b class="subxb1"></b>
                        </div>
                    </div>
                    <p class="table_cx">
                        <img src="../images/Soft_common.gif" style="margin-bottom: -2px; padding-right: 2px;"
                            alt="" />用户列表
                    </p>
                    <div id="Div_UserResourceList" runat="server" style="float: right; padding: 0 40px; clear: right; line-height: 30px;">
                    </div>
                    <div style="width: 95%; margin: 0 auto; padding-bottom: 10px;">
                        <telerik:RadGrid ID="RadGrid1" runat="server" AutoGenerateColumns="False" GridLines="None"
                            Skin="Windows7" AllowPaging="True" AllowMultiRowSelection="True" OnDeleteCommand="RadGrid1_DeleteCommand"
                            OnInsertCommand="RadGrid1_InsertCommand" OnItemDataBound="RadGrid1_ItemDataBound"
                            OnNeedDataSource="RadGrid1_NeedDataSource" OnUpdateCommand="RadGrid1_UpdateCommand" PagerStyle-PageSizeLabelText="每页行数" PagerStyle-AlwaysVisible="true">
                            <ClientSettings>
                                <Selecting AllowRowSelect="True"></Selecting>
                            </ClientSettings>
                            <MasterTableView NoMasterRecordsText="" CommandItemDisplay="Top" DataKeyNames="UserID"
                                ClientDataKeyNames="UserID" EditMode="PopUp" CommandItemSettings-AddNewRecordText="添加"
                                CommandItemSettings-RefreshText="刷新">
                                <RowIndicatorColumn>
                                    <HeaderStyle Width="20px"></HeaderStyle>
                                </RowIndicatorColumn>
                                <ExpandCollapseColumn>
                                    <HeaderStyle Width="20px"></HeaderStyle>
                                </ExpandCollapseColumn>
                                <CommandItemSettings AddNewRecordText="添加" RefreshText="刷新"></CommandItemSettings>
                                <Columns>
                                    <telerik:GridTemplateColumn UniqueName="TemplateColumn" HeaderText="序号">
                                        <ItemTemplate>
                                            <asp:Label ID="numberLabel" runat="server" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="45px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="机构部门" UniqueName="TemplateColumn1">
                                        <ItemTemplate>
                                            <asp:Label ID="DeptOrgan" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn HeaderText="登录账号" UniqueName="column" DataField="UserName">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderText="姓名" UniqueName="column1" DataField="RelUserName">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="角色" UniqueName="TemplateColumn1">
                                        <ItemTemplate>
                                            <asp:Label ID="Role" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                    <%-- <telerik:GridBoundColumn HeaderText="身份证号码" UniqueName="column2" DataField="License">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>--%>
                                    <telerik:GridEditCommandColumn UniqueName="Edit" EditText="编辑">
                                        <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridEditCommandColumn>
                                    <telerik:GridButtonColumn UniqueName="column2" CommandName="Delete"
                                        ConfirmText="确认删除？" ConfirmDialogType="RadWindow" Text="删除">
                                        <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridButtonColumn>
                                </Columns>
                                <EditFormSettings EditFormType="Template" PopUpSettings-Modal="true">
                                    <EditColumn UniqueName="EditCommandColumn1">
                                    </EditColumn>
                                    <FormTemplate>
                                        <br />
                                        <div class="blue_center" style="width: 95%; margin: 0 auto;">
                                            <div>
                                                <b class="subxtop"><b class="subxb1"></b><b class="subxb2"></b><b class="subxb3"></b>
                                                    <b class="subxb4"></b></b>
                                            </div>
                                            <div class="subxboxcontent">
                                                <div style="width: 98%; margin: 0 auto; padding: 5px;">
                                                    <table width="98%" cellpadding="3" cellspacing="0" border="0">
                                                        <tr style="display: none;">
                                                            <td width="10%" class="tousu_content" nowrap="nowrap">
                                                                <font color="red">*</font> 用户ID：
                                                            </td>
                                                            <td width="45%">
                                                                <asp:TextBox ID="txtUserId" runat="server" Text='<%# Eval("UserID") %>' CssClass="texbox"
                                                                    Width="150"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="10%" class="tousu_content" nowrap="nowrap">
                                                                <font color="red">*</font> 登录账号：
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtUserName" runat="server" Text='<%# Eval("UserName") %>' CssClass="texbox"
                                                                    Width="150"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtUserName"
                                                                    runat="server" ErrorMessage="！"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td rowspan="8" valign="top" width="50%" align="left">
                                                                <div style="width: 90%; padding: 10px; float: left;">
                                                                    <p>
                                                                        角色<font color="red">*</font>
                                                                    </p>
                                                                    <telerik:RadTreeView ID="RadTreeView1" runat="server" CheckBoxes="true" TriStateCheckBoxes="false"
                                                                        Height="230px" Skin="Windows7">
                                                                    </telerik:RadTreeView>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="10%" class="tousu_content" nowrap="nowrap">
                                                                <font color="red">*</font> 登录密码：
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtUserPwd" runat="server" Text='<%# Eval("UserPwd") %>' value='<%# Eval("UserPwd") %>'
                                                                    TextMode="Password" CssClass="texbox" Width="150"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtUserPwd"
                                                                    runat="server" ErrorMessage="！"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="10%" class="tousu_content" nowrap="nowrap">
                                                                <font color="red">*</font> 重复密码：
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtDolUserPwd" runat="server" Text='<%# Eval("UserPwd") %>' value='<%# Eval("UserPwd") %>'
                                                                    TextMode="Password" CssClass="texbox" Width="150"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtDolUserPwd"
                                                                    runat="server" ErrorMessage="！"></asp:RequiredFieldValidator>
                                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="两次密码不一致！"
                                                                    ControlToCompare="txtUserPwd" ControlToValidate="txtDolUserPwd"></asp:CompareValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="10%" class="tousu_content" nowrap="nowrap"><font color="red">*</font> 真实名称：
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtRelUserName" runat="server" CssClass="texbox" Width="150" Text='<%# Eval("RelUserName") %>'></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtRelUserName"
                                                                    runat="server" ErrorMessage="！"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="10%" class="tousu_content" nowrap="nowrap">
                                                                <font color="red">*</font> 身份证号：
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtLicense" runat="server" Text='<%# Eval("License") %>' CssClass="texbox"
                                                                    Width="150"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtLicense"
                                                                    runat="server" ErrorMessage="！"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="格式不正确！" Enabled='<%# (_OrganID==242||_OrganID==245||_OrganID==246||_OrganID==247||_OrganID==249)?false:true%>'
                                                                    ValidationExpression="\d{17}[\d|X]|\d{15}" ControlToValidate="txtLicense"></asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="10%" class="tousu_content" nowrap="nowrap">联系电话：
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtTelphone" runat="server" Text='<%# Eval("Telphone") %>' CssClass="texbox"
                                                                    Width="150"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="10%" class="tousu_content" nowrap="nowrap">手机：
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtMobile" runat="server" Text='<%# Eval("Mobile") %>' CssClass="texbox"
                                                                    Width="150"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="10%" class="tousu_content" nowrap="nowrap">执法证号：
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtCode" runat="server" Text='<%# Eval("Code") %>' CssClass="texbox"
                                                                    Width="150"></asp:TextBox>

                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3" style="text-align: center;">
                                                                <asp:Button ID="Button2" runat="server" CssClass="button" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'
                                                                    Text='<%# (Container is GridEditFormInsertItem) ? "添加" : "修改" %>' />
                                                                <asp:Button ID="Button1" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                    Text="取消" CssClass="button" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                            <div>
                                                <b class="subxbottom"></b><b class="subxb4"></b><b class="subxb3"></b><b class="subxb2"></b><b class="subxb1"></b>
                                            </div>
                                        </div>
                                        <br />
                                    </FormTemplate>
                                    <PopUpSettings ScrollBars="None" Width="500px"></PopUpSettings>
                                </EditFormSettings>
                                <PagerTemplate>
                                    <uc2:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                                </PagerTemplate>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </div>
                </div>
            </div>
        </div>
        <div id="winpop">
        </div>
    </form>
</body>
</html>
