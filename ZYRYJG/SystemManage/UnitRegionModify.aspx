<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UnitRegionModify.aspx.cs"
    MasterPageFile="~/RadControls.Master" Inherits="ZYRYJG.SystemManage.UnitRegionModify" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- content start -->
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    </telerik:RadCodeBlock>
    <telerik:RadWindowManager ID="RadWindowManager1" ShowContentDuringLoad="false" VisibleStatusbar="false"
        ReloadOnShow="true" runat="server" Skin="Windows7" EnableShadow="true">
    </telerik:RadWindowManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1"
        EnableAJAX="true">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadGrid1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ButtonSearch">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true"
        Skin="Windows7">
    </telerik:RadAjaxLoadingPanel>
    <div class="div_out">
        <div class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt; 系统管理 &gt;&gt;<strong>特殊企业隶属维护</strong>
            </div>
        </div>
        <div class="content">
            <table class="bar_cx">
                <tr>
                    <td align="right" width="11%" nowrap="nowrap">企业名称：
                    </td>
                    <td nowrap="nowrap" align="left" width="39%">
                        <telerik:RadTextBox ID="RadTextBoxUnitName" runat="server" Width="95%" Skin="Default">
                        </telerik:RadTextBox>
                    </td>
                    <td align="left">
                        <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
                    </td>
                </tr>
            </table>
            <div class="table_cx">
                <img alt="" src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                考场信息列表
            </div>
            <div style="width: 98%; margin: 0 auto;">
                <telerik:RadGrid ID="RadGrid1" runat="server" GridLines="None"
                    ShowStatusBar="true" AllowPaging="True" PageSize="10" AllowSorting="True" AutoGenerateColumns="False"
                    SortingSettings-SortToolTip="单击进行排序" Width="100%"
                    Skin="Blue" EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false" OnInsertCommand="RadGrid1_InsertCommand"
                    OnUpdateCommand="RadGrid1_UpdateCommand" OnItemDataBound="RadGrid1_ItemDataBound"
                    AllowAutomaticDeletes="True" OnItemCommand="RadGrid1_ItemCommand">
                    <ClientSettings EnableRowHoverStyle="true">
                    </ClientSettings>
                    <MasterTableView EditMode="PopUp" CommandItemDisplay="Top" DataKeyNames="QYMC"
                        NoMasterRecordsText="　没有可显示的记录">
                        <Columns>
                            <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="QYMC" DataField="QYMC" HeaderText="企业名称">
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="XZDQBM" DataField="XZDQBM" HeaderText="隶属关系">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridEditCommandColumn UniqueName="Edit" EditText="编辑" HeaderText="编辑">
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridEditCommandColumn>
                            <telerik:GridTemplateColumn UniqueName="Delete" HeaderText="删除">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButtonDelete" CommandName="ButtonDelete" OnClientClick="return confirm('您确定要删除么?')"
                                        runat="server">删除</asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                            </telerik:GridTemplateColumn>
                        </Columns>
                        <CommandItemSettings AddNewRecordText="添加记录" ShowRefreshButton="false" />
                        <EditFormSettings InsertCaption="添加记录" CaptionFormatString="编辑: {0}" CaptionDataField="QYMC"
                            EditFormType="Template" PopUpSettings-Width="70%" PopUpSettings-Modal="true">
                            <EditColumn UniqueName="EditCommandColumn1">
                            </EditColumn>
                            <FormTemplate>
                                <table class="bar_cx" style="margin-top: 20px">
                                    <tr>
                                        <td align="right" width="11%" nowrap="nowrap">
                                            <span style="color: Red">*</span>企业名称：
                                        </td>
                                        <td align="left">
                                            <telerik:RadTextBox ID="RadTextBoxQYMC" runat="server" Width="90%" Skin="Default">
                                            </telerik:RadTextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="必填"
                                                ControlToValidate="RadTextBoxQYMC" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" nowrap="nowrap">
                                            <span style="color: Red">*</span>隶属关系：
                                        </td>
                                        <td align="left">
                                            <telerik:RadComboBox ID="RadComboBoxXZDQBM" runat="server" Width="90%">
                                                <Items>                                                    
                                                    <telerik:RadComboBoxItem Text="东城区" Value="东城区" />
                                                    <telerik:RadComboBoxItem Text="西城区" Value="西城区" />
                                                    <telerik:RadComboBoxItem Text="朝阳区" Value="朝阳区" />
                                                    <telerik:RadComboBoxItem Text="海淀区" Value="海淀区" />
                                                    <telerik:RadComboBoxItem Text="丰台区" Value="丰台区" />
                                                    <telerik:RadComboBoxItem Text="石景山区" Value="石景山区" />
                                                    <telerik:RadComboBoxItem Text="昌平区" Value="昌平区" />
                                                    <telerik:RadComboBoxItem Text="通州区" Value="通州区" />
                                                    <telerik:RadComboBoxItem Text="顺义区" Value="顺义区" />
                                                    <telerik:RadComboBoxItem Text="门头沟区" Value="门头沟区" />
                                                    <telerik:RadComboBoxItem Text="房山区" Value="房山区" />
                                                    <telerik:RadComboBoxItem Text="大兴区" Value="大兴区" />
                                                    <telerik:RadComboBoxItem Text="怀柔区" Value="怀柔区" />
                                                    <telerik:RadComboBoxItem Text="平谷区" Value="平谷区" />
                                                    <telerik:RadComboBoxItem Text="密云区" Value="密云" />
                                                    <telerik:RadComboBoxItem Text="延庆区" Value="延庆" />
                                                    <telerik:RadComboBoxItem Text="亦庄" Value="亦庄" Selected="true" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>                                   
                                </table>
                                <table style="width: 100%; padding-bottom: 20px;">
                                    <tr>
                                        <td align="center" colspan="2">
                                            <asp:Button ID="Button1" CssClass="button" Text='<%# (Container is GridEditFormInsertItem) ? "保存" : "更新" %>'
                                                runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'></asp:Button>&nbsp;
                                                    <asp:Button ID="Button2" CssClass="button" Text="取消" runat="server" CausesValidation="False"
                                                        CommandName="Cancel"></asp:Button>
                                        </td>
                                    </tr>
                                </table>
                            </FormTemplate>
                            <PopUpSettings Modal="True" Width="70%"></PopUpSettings>
                        </EditFormSettings>
                        <CommandItemStyle />
                        <PagerTemplate>
                            <uc2:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                        </PagerTemplate>
                    </MasterTableView>
                    <SortingSettings SortToolTip="单击进行排序"></SortingSettings>
                    <StatusBarSettings LoadingText="正在读取数据" ReadyText="完成" />
                </telerik:RadGrid>
            </div>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectCountMethod="SelectCount"
                EnablePaging="True" SortParameterName="orderBy" TypeName="DataAccess.jcsjk_QY_ZHXX_CITY_ERRORDAL"
                SelectMethod="GetList" DeleteMethod="Delete" DataObjectTypeName="Model.jcsjk_QY_ZHXX_CITY_ERRORMDL">
                <SelectParameters>
                    <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                        DefaultValue="" ConvertEmptyStringToNull="false" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </div>
        <br />
    </div>
</asp:Content>
