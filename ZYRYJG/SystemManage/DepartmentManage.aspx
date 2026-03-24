<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DepartmentManage.aspx.cs"
    Inherits="ZYRYJG.SystemManage.DepartmentManage" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" /> 
    <script type="text/javascript">
        function hightlyAdapterPages(tdID, iHeight) {
            var centertdHeight = document.documentElement.clientHeight - iHeight;
            document.getElementById(tdID).height = centertdHeight;
        }
        function hightlyAdapterPages1(tdID, iHeight) {
            var centertdHeight = document.documentElement.clientHeight - iHeight;
            document.getElementById(tdID).style.minHeight = centertdHeight + "px";
            document.getElementById(tdID).style.height = centertdHeight + "px";
        }
        function hightlyAdapterPages2(tdID, iHeight) {
            var centertdHeight = document.documentElement.clientHeight - iHeight;
            document.getElementById(tdID).style.minHeight = centertdHeight + "px";
        }
        function CheckBrowser() {
            var cb = "Unknown";
            if (window.ActiveXObject) {
                cb = "IE";
            } else if (navigator.userAgent.toLowerCase().indexOf("firefox") != -1) {
                cb = "Firefox";
            } else if ((typeof document.implementation != "undefined") && (typeof document.implementation.createDocument != "undefined") && (typeof HTMLDocument != "undefined")) {
                cb = "Mozilla";
            } else if (navigator.userAgent.toLowerCase().indexOf("opera") != -1) {
                cb = "Opera";
            }
            return cb;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <div style="margin-top: 5px;">
            <div class="table_border" style="width: 99%;">
                <p class="jbxxbt">
                    部门管理
                </p>
                <div style="width: 98%; margin: 0 auto; padding-bottom:30px;">
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
                                <table width="95%" border="0" align="center" cellpadding="5" cellspacing="1" class="table">
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
                                        <td class="tousu_content">机构描述：
                                        </td>
                                        <td colspan="3">
                                            <asp:Label ID="lblOrganDescription" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="GridLightBK">
                                        <td class="tousu_content">机构性质：
                                        </td>
                                        <td>
                                            <asp:Label ID="lblOrganNature" runat="server"></asp:Label>
                                        </td>
                                        <td class="tousu_content">机构地址：
                                        </td>
                                        <td>
                                            <asp:Label ID="lblOrganAddress" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="GridLightBK">
                                        <td class="tousu_content">联系电话：
                                        </td>
                                        <td>
                                            <asp:Label ID="lblOrganTelphone" runat="server"></asp:Label>
                                        </td>
                                        <td class="tousu_content">行政编码：
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
                            alt="" />部门列表
                    </p>
                    <div style="width: 95%; margin: 0 auto; padding-bottom: 10px;">
                        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
                        </telerik:RadAjaxLoadingPanel>
                        <telerik:RadGrid ID="RadGrid1" runat="server" AllowPaging="True" AllowSorting="True"
                            AutoGenerateColumns="False" GridLines="None" Skin="Windows7" OnInsertCommand="RadGrid1_InsertCommand"
                            OnItemDataBound="RadGrid1_ItemDataBound" OnNeedDataSource="RadGrid1_NeedDataSource"
                            OnDeleteCommand="RadGrid1_DeleteCommand" OnUpdateCommand="RadGrid1_UpdateCommand">
                            <MasterTableView NoMasterRecordsText="" CommandItemDisplay="None" EditMode="PopUp"
                                DataKeyNames="DeptID" CommandItemSettings-AddNewRecordText="添加" CommandItemSettings-RefreshText="刷新"
                                EditFormSettings-EditColumn-CancelText="取消" EditFormSettings-EditColumn-EditText="修改"
                                EditFormSettings-EditColumn-UpdateText="更新" EditFormSettings-EditColumn-InsertText="保存">
                                <CommandItemSettings AddNewRecordText="添加" RefreshText="刷新"></CommandItemSettings>
                                <Columns>
                                    <telerik:GridTemplateColumn HeaderText="序号" UniqueName="TemplateColumn">
                                        <ItemTemplate>
                                            <asp:Label ID="numberLabel" runat="server" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="DeptID" HeaderText="部门ID" UniqueName="DeptID"
                                        Visible="false" ReadOnly="true">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="pDeptID" HeaderText="父级部门" UniqueName="pDeptID"
                                        ReadOnly="true" Visible="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="OrganID" HeaderText="机构ID" UniqueName="OrganID"
                                        ReadOnly="true" Visible="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="DeptName" HeaderText="部门名称" UniqueName="DeptName">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                  <%--  <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                        <HeaderStyle Width="4%" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridEditCommandColumn>
                                    <telerik:GridButtonColumn UniqueName="column" CommandName="Delete" ButtonType="ImageButton"
                                        ConfirmText="确认删除？" ConfirmDialogType="RadWindow" Text="dfdfdfd">
                                        <HeaderStyle Width="4%" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridButtonColumn>--%>
                                </Columns>
                                <EditFormSettings EditFormType="Template">
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
                                                <div style="width: 96%; margin: 0 auto; padding: 5px;">
                                                    <table width="95%" align="center" cellpadding="5" cellspacing="0" border="0">
                                                        <tr>
                                                            <td width="20%" class="tousu_content" nowrap="nowrap">
                                                                <font color="red">*</font>部门名称：
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtDeptName" runat="server" Text='<%# Eval("DeptName") %>' CssClass="texbox"
                                                                    Width="80%"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtDeptName"
                                                                    runat="server" ErrorMessage="！"></asp:RequiredFieldValidator>
                                                                <asp:TextBox ID="txtpDeptID" runat="server" Text='<%# Eval("pDeptID") %>' Visible="false"
                                                                    Width="80%" CssClass="texbox"></asp:TextBox>
                                                                <asp:TextBox ID="txtOrganID" runat="server" Text='<%# Eval("OrganID") %>' Visible="false"
                                                                    Width="80%" CssClass="texbox"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2" align="center">
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
                                </EditFormSettings>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </div>
                </div>
            </div>
        </div>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="RadGrid1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Windows7">
        </telerik:RadWindowManager>
    </form>
</body>
</html>