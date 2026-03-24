<%@ Page Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="PersonInfo.aspx.cs" Inherits="ZYRYJG.PersonnelFile.PersonInfo" %>

<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc1" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" EnableAJAX="true">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ButtonSearch">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="Button2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divserch" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Windows7" />
    <div class="div_out">
        <div class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt; 综合查询 &gt;&gt;
                <strong>人员基本信息查询</strong>
            </div>
        </div>
        <div class="content">
            <table class="bar_cx">
                <tr>
                    <td width="12%" align="right" nowrap="nowrap">
                         <telerik:RadComboBox ID="RadComboBoxIten" runat="server">
                                <Items>
                                    <telerik:RadComboBoxItem Text="证件号码" Value="CERTIFICATECODE" />
                                    <telerik:RadComboBoxItem Text="姓名" Value="WorkerName" />                                    
                                    <telerik:RadComboBoxItem Text="联系电话" Value="PHONE" />

                                </Items>
                            </telerik:RadComboBox>
                    </td>
                    <td width="38%" nowrap="nowrap">
                         <telerik:RadTextBox ID="RadTextBoxValue" runat="server" Width="97%" Skin="Default" MaxLength="20">
                            </telerik:RadTextBox>
                    </td>
                    <td width="12%" align="right" nowrap="nowrap">年龄：
                    </td>
                    <td nowrap="nowrap">
                        <telerik:RadNumericTextBox ID="RadNumericTextBoxBirthdayFrom" runat="server" Width="100px"  MaxLength="2" MaxValue="200" MinValue="0" DataType="Number" NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" ShowSpinButtons="true">
                            </telerik:RadNumericTextBox>至
                         <telerik:RadNumericTextBox ID="RadNumericTextBoxBirthdayTo" runat="server" Width="100px"  MaxLength="3" MaxValue="200" MinValue="0" DataType="Number" NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" ShowSpinButtons="true">
                            </telerik:RadNumericTextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" nowrap="nowrap">锁定状态：
                    </td>
                    <td align="left">
                        <asp:RadioButtonList ID="RadioButtonListLockStatus" runat="server" RepeatDirection="Horizontal" Width="280px">
                            <asp:ListItem Text="全部" Value="全部" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="锁定中" Value="锁定中"></asp:ListItem>
                            <asp:ListItem Text="未锁定" Value="未锁定"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td align="right" nowrap="nowrap">锁定原因：
                    </td>
                    <td align="left">
                        <asp:RadioButtonList ID="RadioButtonListLockRemark" runat="server" RepeatDirection="Horizontal" Width="200px">
                            <asp:ListItem Text="全部" Value="全部" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="缺考" Value="缺考"></asp:ListItem>
                            <asp:ListItem Text="其他" Value="其他"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
            
                <tr>
                    <td colspan="4" align="center">
                        <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
                    </td>
                </tr>
            </table>
            <div class="table_cx">
                <img src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                人员基本信息列表
            </div>
            <div style="width: 98%; margin: 0 auto;">
                <telerik:RadGrid ID="RadGrid1" AutoGenerateColumns="False"
                    runat="server" AllowPaging="True" PageSize="10" AllowSorting="True" SortingSettings-SortToolTip="单击进行排序"
                    Skin="Blue" EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false" Width="100%"
                    GridLines="None">
                    <ClientSettings EnableRowHoverStyle="true">
                    </ClientSettings>
                    <MasterTableView CommandItemDisplay="None" AllowMultiColumnSorting="True" DataKeyNames="WORKERID"
                        NoMasterRecordsText="　没有可显示的记录">
                        <CommandItemTemplate>
                            <div class="grid_CommandBar">
                                <input type="button" value=" " class="rgAdd" onclick="javascript: SetIfrmSrc('PersonInfoEdit.aspx');" />
                                <nobr onclick="javascript:SetIfrmSrc('PersonInfoEdit.aspx');" class="grid_CmdButton">
                                        添加</nobr>
                            </div>
                        </CommandItemTemplate>
                        <Columns>
                            <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                <HeaderStyle HorizontalAlign="Center" Wrap="true" />
                                <ItemStyle HorizontalAlign="Center" Wrap="true" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="WorkerName" DataField="WorkerName" HeaderText="姓名">
                                <HeaderStyle HorizontalAlign="Center" Wrap="true" />
                                <ItemStyle HorizontalAlign="Center" Wrap="true" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="CERTIFICATETYPE" DataField="CERTIFICATETYPE"
                                HeaderText="证件类型">
                                <HeaderStyle HorizontalAlign="Center" Wrap="true" />
                                <ItemStyle HorizontalAlign="Center" Wrap="true" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="CERTIFICATECODE" DataField="CERTIFICATECODE"
                                HeaderText="证件号码">
                                <HeaderStyle HorizontalAlign="Center" Wrap="true" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="SEX" DataField="SEX" HeaderText="性别">
                                <HeaderStyle HorizontalAlign="Center" Wrap="true" />
                                <ItemStyle HorizontalAlign="Center" Wrap="true" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="BIRTHDAY" HeaderText="出生日期">
                                <ItemTemplate>
                                    <%#Eval("BIRTHDAY")==DBNull.Value || ((DateTime)Eval("BIRTHDAY")).Year == 1900 ? "" : ((DateTime)Eval("BIRTHDAY")).ToString("yyyy.MM.dd")%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" Font-Bold="true" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn UniqueName="CULTURALLEVEL" DataField="CULTURALLEVEL" HeaderText="文化程度">
                                <HeaderStyle HorizontalAlign="Center" Wrap="true" />
                                <ItemStyle HorizontalAlign="Center" Wrap="true" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="Edit" Visible="false">
                                <ItemTemplate>
                                    <span class="link_edit" onclick='javascript:SetIfrmSrc("PersonInfoEdit.aspx?o=<%# Utility.Cryptography.Encrypt(Eval("WORKERID").ToString()) %>");'>编辑</span>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                            </telerik:GridTemplateColumn>
                        </Columns>
                        <PagerTemplate>
                            <uc2:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                        </PagerTemplate>
                    </MasterTableView>
                    <SortingSettings SortToolTip="单击进行排序"></SortingSettings>
                    <StatusBarSettings LoadingText="正在读取数据" ReadyText="完成" />
                </telerik:RadGrid>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="DataAccess.WorkerDAL"
                    DataObjectTypeName="Model.WorkerOB" SelectMethod="GetList" EnablePaging="true"
                    SelectCountMethod="SelectCount" MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex"
                    SortParameterName="orderBy">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                            DefaultValue="" ConvertEmptyStringToNull="false" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </div>
        </div>
        <br />
    </div>
    <uc1:IframeView ID="IframeView" runat="server" />
</asp:Content>
