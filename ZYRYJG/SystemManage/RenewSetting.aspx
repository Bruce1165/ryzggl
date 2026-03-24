<%@ Page Title="" Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="RenewSetting.aspx.cs" Inherits="ZYRYJG.SystemManage.RenewSetting" %>

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
                系统维护 &gt;&gt; <strong>续期时间设置</strong>
            </div>
        </div>
        <div class="content">
            <div style="width: 100%; margin: 15px auto;">
                <telerik:RadGrid ID="RadGrid1" runat="server" AutoGenerateColumns="False" AllowSorting="false"
                    GridLines="None" CellPadding="0" Width="98%" Skin="Blue" EnableAjaxSkinRendering="false"
                    EnableEmbeddedSkins="false" OnItemDataBound="RadGrid1_ItemDataBound" OnUpdateCommand="RadGrid1_UpdateCommand"
                    OnNeedDataSource="RadGrid1_NeedDataSource">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="TypeID,TypeName,TypeValue" EditMode="PopUp" NoMasterRecordsText="没有可显示的记录">
                        <Columns>
                            <telerik:GridBoundColumn HeaderText="序号" UniqueName="RowNum" DataField="RowNum" AllowSorting="false">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="TypeName" DataField="TypeName" HeaderText="配置类型名称">
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="ValidTo" DataField="ValidTo" HeaderText="证书到期时间（月-日）"
                                HtmlEncode="false" DataFormatString="{0:MM月dd日}" AllowSorting="false">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="RenewMonth" DataField="RenewMonth" HeaderText="续期开放时间段（月份）">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridEditCommandColumn UniqueName="Edit" EditText="设置">
                            </telerik:GridEditCommandColumn>
                        </Columns>
                        <EditFormSettings InsertCaption="添加考试计划" CaptionFormatString="续期时间设置"
                            EditFormType="Template" PopUpSettings-Modal="true" FormCaptionStyle-HorizontalAlign="Left">
                            <EditColumn UniqueName="EditCommandColumn1">
                            </EditColumn>
                            <FormTemplate>
                                <div id="DivEdit" runat="server" style="width: 500px; margin: 0 auto;">
                                    <br />
                                    <table class="bar_cx">
                                        <tr>
                                            <td align="right" style="width: 40%;">配置类型名称：
                                            </td>
                                            <td align="left" style="width: 60%">
                                                <asp:Label ID="LabelTypeName" runat="server" Text='<%# Eval("TypeName") %>'></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 40%">证书到期时间：
                                            </td>
                                            <td align="left" style="width: 60%">
                                                <asp:Label ID="LabelValidTo" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 40%">续期开放时间段：
                                            </td>
                                            <td align="left">从&nbsp;<telerik:RadComboBox ID="RadComboBoxMonthStart" runat="server" Skin="Office2007"
                                                CausesValidation="False" Width="60px" ExpandAnimation-Duration="0">
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="1" Value="1" />
                                                    <telerik:RadComboBoxItem Text="2" Value="2" />
                                                    <telerik:RadComboBoxItem Text="3" Value="3" />
                                                    <telerik:RadComboBoxItem Text="4" Value="4" />
                                                    <telerik:RadComboBoxItem Text="5" Value="5" />
                                                    <telerik:RadComboBoxItem Text="6" Value="6" />
                                                    <telerik:RadComboBoxItem Text="7" Value="7" />
                                                    <telerik:RadComboBoxItem Text="8" Value="8" />
                                                    <telerik:RadComboBoxItem Text="9" Value="9" />
                                                    <telerik:RadComboBoxItem Text="10" Value="10" />
                                                    <telerik:RadComboBoxItem Text="11" Value="11" />
                                                    <telerik:RadComboBoxItem Text="12" Value="12" />
                                                </Items>
                                            </telerik:RadComboBox>
                                                &nbsp;月 至
                                                            <telerik:RadComboBox ID="RadComboBoxMonthEnd" runat="server" Skin="Office2007" CausesValidation="False"
                                                                Width="60px" ExpandAnimation-Duration="0">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="1" Value="1" />
                                                                    <telerik:RadComboBoxItem Text="2" Value="2" />
                                                                    <telerik:RadComboBoxItem Text="3" Value="3" />
                                                                    <telerik:RadComboBoxItem Text="4" Value="4" />
                                                                    <telerik:RadComboBoxItem Text="5" Value="5" />
                                                                    <telerik:RadComboBoxItem Text="6" Value="6" />
                                                                    <telerik:RadComboBoxItem Text="7" Value="7" />
                                                                    <telerik:RadComboBoxItem Text="8" Value="8" />
                                                                    <telerik:RadComboBoxItem Text="9" Value="9" />
                                                                    <telerik:RadComboBoxItem Text="10" Value="10" />
                                                                    <telerik:RadComboBoxItem Text="11" Value="11" />
                                                                    <telerik:RadComboBoxItem Text="12" Value="12" />
                                                                    <telerik:RadComboBoxItem Text="明年1" Value="13" />
                                                                    <telerik:RadComboBoxItem Text="明年2" Value="14" />
                                                                    <telerik:RadComboBoxItem Text="明年3" Value="15" />
                                                                    <telerik:RadComboBoxItem Text="明年4" Value="16" />
                                                                    <telerik:RadComboBoxItem Text="明年5" Value="17" />
                                                                    <telerik:RadComboBoxItem Text="明年6" Value="18" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                &nbsp;月
                                            </td>
                                        </tr>
                                    </table>
                                    <div style="padding: 20px 100px 30px 0px; text-align:right">
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
            </div>
        </div>
    </div>
</asp:Content>
