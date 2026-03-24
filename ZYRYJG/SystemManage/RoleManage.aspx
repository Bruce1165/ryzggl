<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoleManage.aspx.cs" Inherits="ZYRYJG.SystemManage.RoleManage" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" />
    <link href="../Skins/Blue/Grid.Blue.css?v=1.008" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
    <link href="../layer/skin/layer.css" rel="stylesheet" />
    <script src="../layer/layer.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadWindowManager ID="Singleton" Skin="Windows7" Width="400" Height="430"
            VisibleStatusbar="false" Behaviors="Close,Move, Resize" runat="server">
        </telerik:RadWindowManager>
        <asp:HiddenField ID="HiddenField1" runat="server" />
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
        </telerik:RadAjaxLoadingPanel>
        <div style="margin-top: 5px;">
            <div style="width: 99%;" id="center_td">
                <p class="jbxxbt">
                    权限管理
                </p>
                <div id="div_main" runat="server" style="width: 98%; margin: 0 auto; padding-bottom: 30px;">
                    <div id="DivRoleList" runat="server">
                        <p class="table_cx">
                            <img src="../images/Soft_common.gif" style="margin-bottom: -2px; padding-right: 2px;"
                                alt="" />角色列表
                        </p>
                        <div style="width: 99%; margin: 0 auto; padding-bottom: 10px;">
                            <telerik:RadGrid ID="RadGrid1" runat="server" AutoGenerateColumns="False" GridLines="None" MasterTableView-AllowPaging="false"
                                AllowPaging="True" OnNeedDataSource="RadGrid1_NeedDataSource" Skin="Blue" EnableAjaxSkinRendering="false" DataKeyNames="RoleID"
                                EnableEmbeddedSkins="false" OnDetailTableDataBind="RadGrid1_DetailTableDataBind"
                                OnItemDataBound="RadGrid1_ItemDataBound" OnDeleteCommand="RadGrid1_DeleteCommand"
                                OnInsertCommand="RadGrid1_InsertCommand" OnUpdateCommand="RadGrid1_UpdateCommand">
                                <ClientSettings>
                                    <Selecting AllowRowSelect="True"></Selecting>
                                </ClientSettings>
                                <MasterTableView CommandItemDisplay="Top" DataKeyNames="RoleID" ClientDataKeyNames="RoleID" NoMasterRecordsText="　没有可显示的记录"
                                    EditMode="PopUp" CommandItemSettings-AddNewRecordText="添加" CommandItemSettings-RefreshText="刷新">
                                    <RowIndicatorColumn>
                                        <HeaderStyle Width="20px"></HeaderStyle>
                                    </RowIndicatorColumn>
                                    <ExpandCollapseColumn>
                                        <HeaderStyle Width="20px"></HeaderStyle>
                                    </ExpandCollapseColumn>
                                    <CommandItemSettings AddNewRecordText="添加" RefreshText="刷新"></CommandItemSettings>
                                    <DetailTables>                                        
                                        <telerik:GridTableView Width="100%" runat="server" CommandItemDisplay="None" DataKeyNames="RoleID"
                                            Name="RoleUser" NoDetailRecordsText=" 没有可显示的记录"  >                                            
                                            <Columns>
                                                 <telerik:GridTemplateColumn HeaderText="" UniqueName="TemplateColumn1">
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn DataField="OrganName" HeaderText="机构部门" UniqueName="OrganName">
                                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="RelUserName" HeaderText="姓名" UniqueName="RelUserName">
                                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn HeaderText="" UniqueName="TemplateColumn1">
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Width="60%" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridTemplateColumn>
                                                <%--  <telerik:GridTemplateColumn HeaderText="机构部门" UniqueName="TemplateColumn1">
                                        <ItemTemplate>
                                            <asp:Label ID="DeptOrgan" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn HeaderText="姓名" UniqueName="column1" DataField="RelUserName">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>--%>
                                            </Columns>
                                        </telerik:GridTableView>
                                    </DetailTables>
                                    <Columns>
                                        <telerik:GridTemplateColumn UniqueName="TemplateColumn" HeaderText="序号">
                                            <ItemTemplate>
                                                <asp:Label ID="numberLabel" runat="server" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="45px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridEditCommandColumn UniqueName="Edit" EditText="编辑">
                                            <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridEditCommandColumn>
                                        <telerik:GridBoundColumn DataField="RoleName" HeaderText="角色名称" UniqueName="RoleName">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Memo" HeaderText="角色备注" UniqueName="Memo">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>

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
                                                        <table width="95%" cellpadding="5" cellspacing="0" border="0">
                                                            <tr style="visibility: hidden">
                                                                <td width="20%" class="tousu_content" nowrap="nowrap">
                                                                    <font color="red">*</font>角色ID：
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtRoleID" runat="server" Text='<%# Eval("RoleID") %>'></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="20%" class="tousu_content" nowrap="nowrap">
                                                                    <font color="red">*</font> 角色名称：
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBoxRoleName" runat="server" Text='<%# Eval("RoleName") %>' CssClass="texbox"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBoxRoleName"
                                                                        ErrorMessage="！"></asp:RequiredFieldValidator>
                                                                    <asp:Label ID="lblRoleName" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="20%" class="tousu_content" nowrap="nowrap">角色备注：
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBoxRoleMemo" runat="server" TextMode="MultiLine" Text='<%# Eval("Memo") %>'
                                                                        Height="60px" CssClass="texbox" Width="98%"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="20%" class="tousu_content" nowrap="nowrap">
                                                                    <font color="red">*</font> 权限：
                                                                </td>
                                                                <td>
                                                                    <telerik:RadTreeView ID="RadTreeView1" runat="server" CheckBoxes="True" CheckChildNodes="True"
                                                                        TriStateCheckBoxes="True" Height="200px" Skin="Windows7">
                                                                    </telerik:RadTreeView>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" align="center">
                                                                    <asp:Button ID="Button2" runat="server" CssClass="button" Text='<%# (Container is GridEditFormInsertItem) ? "添加" : "修改" %>'
                                                                        CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>' />
                                                                    <asp:Button ID="Button1" runat="server" CssClass="button" Text="取消" CausesValidation="False"
                                                                        CommandName="Cancel" />
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
                                        <PopUpSettings Modal="True" Width="500px"></PopUpSettings>
                                    </EditFormSettings>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </div>
                    </div>
                    <div runat="server" id="DivMenuList">
                        <p class="table_cx">
                            <img src="../images/Soft_common.gif" style="margin-bottom: -2px; padding-right: 2px;"
                                alt="" />权限列表
                        </p>
                        <div style="width: 95%; margin: 0 auto; padding-bottom: 10px;">
                            <div style="width: 99%; margin: 5px auto; padding: 5px; font-size: 13px; color: #2b4875;">
                                <font style="color: red">当前角色：</font><asp:Label ID="lblRoleName" runat="server"></asp:Label>
                            </div>
                            <div style="width: 99%; margin: 0 auto;">
                                <asp:Label ID="MenuList" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>

                    <p class="table_cx">
                        <img src="../images/Soft_common.gif" style="margin-bottom: -2px; padding-right: 2px;"
                            alt="" />角色权限一览表
                    </p>
                    <div id="test">
                        <telerik:RadGrid ID="RadGridRoleResource" runat="server" AutoGenerateColumns="false" GridLines="None"
                            AllowPaging="False" EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false"
                            Skin="Blue" Width="100%" OnExcelExportCellFormatting="RadGridRoleResource_ExcelExportCellFormatting">
                            <%-- <ClientSettings EnableRowHoverStyle="true" Scrolling-AllowScroll="true" Scrolling-ScrollHeight="450" Scrolling-UseStaticHeaders="true">
                            </ClientSettings>--%>
                            <MasterTableView CommandItemDisplay="None" NoMasterRecordsText="　没有可显示的记录">
                                <Columns>
                                </Columns>
                                <HeaderStyle Font-Bold="true" />
                            </MasterTableView>
                        </telerik:RadGrid>
                    </div>
                    <div style="text-align: center; padding: 10px 0px;">
                        <asp:Button ID="ButtonExportToExcel" CssClass="bt_large" Text="导出列表Excel" OnClick="ButtonExportToExcel_Click"
                            runat="server"></asp:Button>
                    </div>
                </div>
            </div>
        </div>

    </form>
</body>
</html>
