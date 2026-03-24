<%@ Page Title="" Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="TrainUnitSet.aspx.cs" Inherits="ZYRYJG.Train.TrainUnitSet" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    </telerik:RadCodeBlock>
    <telerik:RadWindowManager ID="RadWindowManager1" ShowContentDuringLoad="false" VisibleStatusbar="false"
        ReloadOnShow="true" runat="server" Skin="Windows7" EnableShadow="true">
    </telerik:RadWindowManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1"
        EnableAJAX="true">
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true" />
    <div class="div_out">
        <div class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt; 系统管理 &gt;&gt;
                部门、用户、权限管理 &gt;&gt; <strong>培训点管理</strong>
            </div>
        </div>
        <div class="content">
            <div class="DivContent" style="color: #444; line-height: 180%">
                    <b>功能说明：</b> 
                    <div style="padding:8px 104px">该页面用于配置可以组织新版建设职业技能岗位考试的培训点。<br />
                        编号：3位数字，用于证书编号使用。<br />
                        机构代码：表示培训点登录系统使用的企业账号对应的社会统一信用代码。<br />
                        可创建考试计划工种：表示该培训点可组织那些岗位工种的考试。
                    </div>
                </div>
            <div style="width: 100%; margin: 15px auto;">
                <telerik:RadGrid ID="RadGrid1" runat="server" AutoGenerateColumns="False" AllowSorting="false"
                    GridLines="None" CellPadding="0" Width="98%" Skin="Blue" EnableAjaxSkinRendering="false"
                    EnableEmbeddedSkins="false" OnItemDataBound="RadGrid1_ItemDataBound" OnUpdateCommand="RadGrid1_UpdateCommand"
                    OnInsertCommand="RadGrid1_InsertCommand" OnDeleteCommand="RadGrid1_DeleteCommand" OnNeedDataSource="RadGrid1_NeedDataSource">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="UnitNo" EditMode="PopUp" CommandItemDisplay="Top" NoMasterRecordsText="没有可显示的记录">
                        <Columns>
                            <telerik:GridBoundColumn HeaderText="编号" UniqueName="UnitNo" DataField="UnitNo" AllowSorting="false">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="TrainUnitName" DataField="TrainUnitName" HeaderText="培训点名称">
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="UnitCode" DataField="UnitCode" HeaderText="机构代码"
                                AllowSorting="false">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="UseStatus" HeaderText="状态">
                                <ItemTemplate>
                                    <%# Eval("UseStatus").ToString()=="0"?"停用":"启用"%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridEditCommandColumn UniqueName="Edit" EditText="编辑" HeaderText="编辑">
                            </telerik:GridEditCommandColumn>
                            <telerik:GridButtonColumn UniqueName="Delete" HeaderText="删除" CommandName="Delete" Text="&lt;span onclick=&quot;if(!confirm('您确定要删除么?'))return false; &quot; &gt;删除&lt;/span&gt;">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridButtonColumn>
                        </Columns>
                        <CommandItemSettings AddNewRecordText="添加培训点" ShowRefreshButton="false" />
                        <EditFormSettings InsertCaption="添加培训点" CaptionFormatString="培训点设置"
                            EditFormType="Template" PopUpSettings-Modal="true" FormCaptionStyle-HorizontalAlign="Left">
                            <EditColumn UniqueName="EditCommandColumn1">
                            </EditColumn>
                            <FormTemplate>
                                <div id="DivEdit" runat="server" style="margin: 0 auto;">
                                    <br />
                                    <table class="bar_cx">
                                        <tr>
                                            <td align="right" style="width: 20%">编号：
                                            </td>
                                            <td align="left" style="width: 80%">
                                                <telerik:RadTextBox runat="server" ID="RadTextBoxUnitNo" Width="90%" Skin="Default" Text='<%# Eval("UnitNo") %>' MaxLength="3">
                                                </telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">培训点名称：
                                            </td>
                                            <td align="left">
                                                <telerik:RadTextBox runat="server" ID="RadTextBoxTrainUnitName" Width="90%" Skin="Default" Text='<%# Eval("TrainUnitName") %>' MaxLength="30">
                                                </telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">机构代码：
                                            </td>
                                            <td align="left">
                                                <telerik:RadTextBox runat="server" ID="RadTextBoxUnitCode" Width="90%" Skin="Default" Text='<%# Eval("UnitCode") %>' MaxLength="18">
                                                </telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">状态：
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioButtonListUseStatus" runat="server" RepeatDirection="Horizontal" Width="150px">
                                                    <asp:ListItem Text="启用" Value="1" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="停用" Value="0"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 40%; vertical-align: top;">可创建考试计划工种：
                                            </td>
                                            <td align="left">
                                                <asp:CheckBoxList ID="CheckBoxListPostID" runat="server" DataTextField="PostName"
                                                    DataValueField="PostID">
                                                </asp:CheckBoxList>
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
                            <PopUpSettings Modal="True" Width="800px"></PopUpSettings>
                        </EditFormSettings>
                        <HeaderStyle Font-Bold="True" />
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
        </div>
    </div>
</asp:Content>
