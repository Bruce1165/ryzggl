<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExamInfoRight.aspx.cs"
    MasterPageFile="~/RadControls.Master" Inherits="ZYRYJG.EXamManage.ExamInfoRight" %>

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
                当前位置 &gt;&gt; 考务管理 &gt;&gt;
                考场管理 &gt;&gt; <strong>考场信息</strong>
            </div>
        </div>
        <div class="content">
            <table class="bar_cx">
                <tr>
                    <td align="right" width="11%" nowrap="nowrap">考点名称：
                    </td>
                    <td nowrap="nowrap" align="left" width="39%">
                        <telerik:RadTextBox ID="RadTxtExamPlaceName" runat="server" Width="95%" Skin="Default" >
                        </telerik:RadTextBox>
                    </td>
                    <td align="right" width="11%" nowrap="nowrap">考点地址：
                    </td>
                    <td nowrap="nowrap" align="left">
                        <telerik:RadTextBox ID="RadTxtExamPlaceAddress" runat="server" CssClass="texbox"
                            Width="95%" Skin="Default" >
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" width="11%" nowrap="nowrap">联系人：
                    </td>
                    <td nowrap="nowrap" align="left">
                        <telerik:RadTextBox ID="RadTxtLinkMan" runat="server" Width="95%" CssClass="texbox"
                            Skin="Default" >
                        </telerik:RadTextBox>
                    </td>
                    <td align="right" width="11%" nowrap="nowrap">联系电话：
                    </td>
                    <td nowrap="nowrap" align="left">
                        <telerik:RadTextBox ID="RadTxtPhone" runat="server" Width="95%" CssClass="texbox"
                            Skin="Default" >
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center">
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
                    <MasterTableView EditMode="PopUp" CommandItemDisplay="Top" DataKeyNames="ExamPlaceID"
                        NoMasterRecordsText="　没有可显示的记录">
                        <Columns>
                            <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="ExamPlaceName" DataField="ExamPlaceName" HeaderText="考点名称">
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn UniqueName="LinkMan" DataField="LinkMan" HeaderText="联系人">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="RoomNum" DataField="RoomNum" HeaderText="考场数量">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="ExamPersonNum" DataField="ExamPersonNum" HeaderText="容纳人数">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridEditCommandColumn UniqueName="Edit" EditText="编辑">
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridEditCommandColumn>

                            <telerik:GridTemplateColumn UniqueName="Delete">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButtonDelete" CommandName="ButtonDelete" OnClientClick="return confirm('您确定要删除么?')"
                                        runat="server">删除</asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                            </telerik:GridTemplateColumn>
                        </Columns>
                        <CommandItemSettings AddNewRecordText="添加考点" ShowRefreshButton="false" />
                        <EditFormSettings InsertCaption="添加考点" CaptionFormatString="编辑考点: {0}" CaptionDataField="ExamPlaceName"
                            EditFormType="Template" PopUpSettings-Width="70%" PopUpSettings-Modal="true">
                            <EditColumn UniqueName="EditCommandColumn1">
                            </EditColumn>
                            <FormTemplate>
                                <table class="bar_cx" style="margin-top: 20px">
                                    <tr>
                                        <td align="right" width="11%" nowrap="nowrap">
                                            <span style="color: Red">*</span>考点名称：
                                        </td>
                                        <td align="left">
                                            <telerik:RadTextBox ID="RadTextBoxExamPlaceName" runat="server" Width="90%" Skin="Default">
                                            </telerik:RadTextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="必填"
                                                ControlToValidate="RadTextBoxExamPlaceName" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" nowrap="nowrap">
                                            <span style="color: Red">*</span>考点地址：
                                        </td>
                                        <td align="left">
                                            <telerik:RadTextBox ID="RadTextBoxExamPlaceAddress" runat="server" Width="90%" Skin="Default"
                                                TextMode="MultiLine">
                                            </telerik:RadTextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="必填"
                                                ControlToValidate="RadTextBoxExamPlaceAddress" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" nowrap="nowrap">
                                            <span style="color: Red">*</span>联系人：
                                        </td>
                                        <td align="left">
                                            <telerik:RadTextBox ID="RadTextBoxLinkMan" runat="server" Width="90%" Skin="Default">
                                            </telerik:RadTextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="必填"
                                                ControlToValidate="RadTextBoxLinkMan" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" nowrap="nowrap">
                                            <span style="color: Red">*</span> 联系电话：
                                        </td>
                                        <td align="left">
                                            <telerik:RadTextBox ID="RadTextBoxPhone" runat="server" Width="90%" Skin="Default">
                                            </telerik:RadTextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="必填"
                                                ControlToValidate="RadTextBoxPhone" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" nowrap="nowrap">
                                            <span style="color: Red">*</span>考场数量：
                                        </td>
                                        <td align="left">
                                            <telerik:RadNumericTextBox ID="RadNumericTextBoxRoomNum" runat="server" MaxLength="5"
                                                Type="Number" NumberFormat-DecimalDigits="0" ShowSpinButtons="true" Width="100px"
                                                MinValue="0" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="必填"
                                                ControlToValidate="RadNumericTextBoxRoomNum" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" nowrap="nowrap">
                                            <span style="color: Red">*</span>容纳人数：
                                        </td>
                                        <td nowrap="nowrap" align="left">
                                            <telerik:RadNumericTextBox ID="RadNumericTextBoxExamPersonNum" runat="server" MaxLength="5"
                                                Type="Number" NumberFormat-DecimalDigits="0" ShowSpinButtons="true" Width="100px"
                                                MinValue="0" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="必填"
                                                ControlToValidate="RadNumericTextBoxExamPersonNum" Display="Dynamic"></asp:RequiredFieldValidator>
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
                EnablePaging="True" SortParameterName="orderBy" TypeName="DataAccess.ExamPlaceDAL"
                SelectMethod="GetList" DeleteMethod="Delete" DataObjectTypeName="Model.ExamPlaceOB">
                <SelectParameters>
                    <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                        DefaultValue="" ConvertEmptyStringToNull="false" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </div>
        <br />
    </div>
</asp:Content>
