<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UnitWatchList.aspx.cs" Inherits="ZYRYJG.SystemManage.UnitWatch" %>

<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<%@ Register Src="../CheckAll.ascx" TagName="CheckAll" TagPrefix="uc3" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc4" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" />
    <link href="../Skins/Blue/Grid.Blue.css?v=1.008" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
    <link href="../layer/skin/layer.css" rel="stylesheet" />
    <script src="../layer/layer.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        </telerik:RadCodeBlock>
         <script type="text/javascript">
            function onRequestStart(sender, args) {
                if (args.get_eventTarget().indexOf("ButtonImport") >= 0) {
                    args.set_enableAjax(false);
                }
            }
        </script>
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadWindowManager ID="RadWindowManager1" ShowContentDuringLoad="false" VisibleStatusbar="false"
            ReloadOnShow="true" runat="server" Skin="Sunset" EnableShadow="true">
        </telerik:RadWindowManager>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
            <ClientEvents OnRequestStart="onRequestStart" />
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="DivMain">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="DivMain" />
                    </UpdatedControls>
                </telerik:AjaxSetting>

            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true"
            Skin="Sunset" />
        <div class="div_out">
            <div class="dqts">
                <div style="float: left;">
                    当前位置 &gt;&gt; 综合查询 &gt;&gt; 
                <strong>建造师重点核查企业</strong>
                </div>
            </div>
            <div class="content" runat="server" id="DivMain">
                <table class="cx" width="98%" border="0" align="center" cellspacing="5">
                    <tr>
                        <td align="right" nowrap="nowrap" width="8%">行政所属：
                        </td>
                        <td align="left" width="15%">
                            <telerik:RadComboBox ID="RadComboBoxENT_City" runat="server" Width="97%">
                                <Items>
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <td align="right" nowrap="nowrap" width="8%">企业名称：
                        </td>
                        <td align="left" width="15%">
                            <telerik:RadTextBox ID="RadTextBoxUnitName" runat="server" Width="97%" Skin="Default"
                                MaxLength="50">
                            </telerik:RadTextBox>
                        </td>

                        <td align="right" nowrap="nowrap" width="8%">核查期起：
                        </td>
                        <td align="left" width="15%">
                            <telerik:RadDatePicker ID="datePickerCJSJ" runat="server" Width="97%" DateInput-DateFormat="yyyy-MM-dd" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                DateInput-DisplayDateFormat="yyyy-MM-dd">
                            </telerik:RadDatePicker>
                        </td>
                        <td align="right" nowrap="nowrap" width="8%">核查期止：
                        </td>
                        <td align="left" width="15%">
                            <telerik:RadDatePicker ID="datePickerValidEnd" runat="server" Width="97%" DateInput-DateFormat="yyyy-MM-dd" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                DateInput-DisplayDateFormat="yyyy-MM-dd">
                            </telerik:RadDatePicker>
                        </td>
                        <td align="left">
                            <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />

                        </td>
                    </tr>
                     <tr>
                          <td align="center" colspan="9">
                              <asp:Button ID="ButtonSetEnd" runat="server" Text="废止查询结果" ToolTip="将核查有效期设置成当日" CssClass="bt_large" OnClick="ButtonSetEnd_Click" OnClientClick="javascript:if(confirm('请您确认是否将查询记过核查有效期设置成当日？')==false) return false;"/>
                            <asp:Button ID="ButtonDelSelect" runat="server" Text="删除查询结果" ToolTip="删除记录" CssClass="bt_large" OnClick="ButtonDelSelect_Click"  OnClientClick="javascript:if(confirm('确定要删除查询结果记录吗？')==false) return false;"/>

                        </td>
                          </tr>

                </table>
                <div class="table_cx">
                    <img alt="" src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                    企业列表
                </div>
                <div style="width: 98%; margin: 0 auto; padding-bottom: 10px;">
                    <telerik:RadGrid ID="RadGridExamPlan" runat="server" GridLines="None" AllowPaging="True"
                        AllowCustomPaging="true" PageSize="10" AllowSorting="True" AutoGenerateColumns="False"
                        SortingSettings-SortToolTip="单击进行排序" Width="100%" Skin="Blue" EnableAjaxSkinRendering="False"
                        EnableEmbeddedSkins="False" PagerStyle-AlwaysVisible="true" OnInsertCommand="RadGridExamPlan_InsertCommand"
                        OnUpdateCommand="RadGridExamPlan_UpdateCommand" OnItemDataBound="RadGridExamPlan_ItemDataBound"
                        OnDeleteCommand="RadGridExamPlan_DeleteCommand">
                        <ClientSettings EnableRowHoverStyle="true">
                        </ClientSettings>
                        <MasterTableView DataKeyNames="CreditCode" NoMasterRecordsText="　没有可显示的记录" EditMode="PopUp" CommandItemDisplay="Top">
                            <Columns>
                                <%--  <telerik:GridTemplateColumn UniqueName="SelectAllColumn" HeaderText="Highlight <br/> ship name" >
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" onclick='checkBoxClick(this.checked);' />
                                    </ItemTemplate>
                                    <HeaderTemplate>
                                        <uc3:CheckAll ID="CheckAll1" runat="server" />
                                    </HeaderTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridTemplateColumn>--%>

                                <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="行号" AllowSorting="false">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" Width="36" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="企业名称" DataField="UnitName" UniqueName="UnitName">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="社会统一信用代码" DataField="CreditCode" UniqueName="CreditCode">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="资质证书编号" DataField="UnitCertCode" UniqueName="UnitCertCode">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="行政所属" DataField="ENT_City" UniqueName="ENT_City">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="FaRen" HeaderText="法定代表人" UniqueName="FaRen">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="FRPhone" HeaderText="法人联系电话" UniqueName="FRPhone">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="注册地址" DataField="Address" UniqueName="Address">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="核查期起" DataField="CJSJ" UniqueName="CJSJ"
                                    HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="核查期止" DataField="ValidEnd" UniqueName="ValidEnd"
                                    HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridEditCommandColumn UniqueName="Edit" EditText="编辑">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridEditCommandColumn>
                                <telerik:GridButtonColumn UniqueName="Delete" CommandName="Delete" Text="&lt;span onclick=&quot;if(!confirm('您确定要删除么?'))return false; &quot; &gt;删除&lt;/span&gt;">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridButtonColumn>

                            </Columns>
                            <CommandItemSettings AddNewRecordText="添加企业" ShowRefreshButton="false" />
                            <EditFormSettings InsertCaption="添加企业" CaptionFormatString="编辑企业: {0}" CaptionDataField="UnitName"
                                EditFormType="Template" PopUpSettings-Modal="true" FormCaptionStyle-HorizontalAlign="Left">
                                <EditColumn UniqueName="EditCommandColumn1">
                                </EditColumn>
                                <FormTemplate>
                                    <div id="DivEdit" runat="server" style="width: 100%; margin: 0 auto">

                                        <table id="Table1" class="bar_cx" cellspacing="0" cellpadding="1" width="98%;">
                                            <tr>
                                                <td align="right" style="width: 12%">
                                                    <font style="color: Red">*</font>企业名称：
                                                </td>
                                                <td align="left" style="width: 37%">
                                                    <telerik:RadTextBox runat="server" ID="RadTextBoxUnitName" Width="80%" Skin="Default">
                                                    </telerik:RadTextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="必填"
                                                        ControlToValidate="RadTextBoxUnitName" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </td>
                                                <td align="right" style="width: 12%">
                                                    <font style="color: Red">*</font>社会统一信用代码：
                                                </td>
                                                <td align="left" style="width: 37%">
                                                    <telerik:RadTextBox runat="server" ID="RadTextBoxCreditCode" Width="80%" Skin="Default">
                                                    </telerik:RadTextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="必填"
                                                        ControlToValidate="RadTextBoxCreditCode" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">资质证书编号：
                                                </td>
                                                <td align="left">
                                                    <telerik:RadTextBox runat="server" ID="RadTextBoxUnitCertCode" Width="80%" Skin="Default">
                                                    </telerik:RadTextBox>

                                                </td>
                                                <td align="right">行政所属：
                                                </td>
                                                <td align="left">
                                                    <telerik:RadTextBox runat="server" ID="RadTextBoxENT_City" Width="80%" Skin="Default">
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">法定代表人：
                                                </td>
                                                <td align="left">
                                                    <telerik:RadTextBox runat="server" ID="RadTextBoxFaRen" Width="80%" Skin="Default">
                                                    </telerik:RadTextBox>
                                                </td>
                                                <td align="right">法人联系电话：
                                                </td>
                                                <td align="left">
                                                    <telerik:RadTextBox runat="server" ID="RadTextBoxFRPhone" Width="80%" Skin="Default">
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" nowrap="nowrap">注册地址：
                                                </td>
                                                <td align="left" colspan="3">
                                                    <telerik:RadTextBox runat="server" ID="RadTextBoxAddress" Width="95%" Skin="Default"
                                                        MaxLength="1000">
                                                    </telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <font style="color: Red">*</font> 核查期起：
                                                </td>
                                                <td align="left">
                                                    <telerik:RadDatePicker ID="RadDatePickerCJSJ" runat="server" Width="120px" Calendar-DayCellToolTipFormat="yyyy年MM月dd日">
                                                    </telerik:RadDatePicker>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="必填"
                                                        ControlToValidate="RadDatePickerCJSJ" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </td>
                                                <td align="right">
                                                    <font style="color: Red">*</font> 核查期止：
                                                </td>
                                                <td align="left">
                                                    <telerik:RadDatePicker ID="RadDatePickerValidEnd" runat="server" Width="120px" Calendar-DayCellToolTipFormat="yyyy年MM月dd日">
                                                    </telerik:RadDatePicker>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="必填"
                                                        ControlToValidate="RadDatePickerValidEnd" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </table>
                                        <table width="100%">
                                            <tr>
                                                <td style="text-align: center">
                                                    <div style="margin: 20px 0px 30px 0px; width: 80%">
                                                        <asp:Button ID="ButtonSave" CssClass="button" Text='<%# (Container is GridEditFormInsertItem) ? "保 存" : "更 新" %>'
                                                            runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'></asp:Button>&nbsp;
                                                            <asp:Button ID="Button2" CssClass="button" Text="取 消" runat="server" CausesValidation="False"
                                                                CommandName="Cancel"></asp:Button>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </FormTemplate>
                                <PopUpSettings Modal="True" Width="95%"></PopUpSettings>
                            </EditFormSettings>
                            <PagerTemplate>
                                <uc2:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                            </PagerTemplate>
                        </MasterTableView>
                    </telerik:RadGrid>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="DataAccess.UnitWatchDAL"
                        SelectMethod="GetList" EnablePaging="true" SelectCountMethod="SelectCount" MaximumRowsParameterName="maximumRows"
                        StartRowIndexParameterName="startRowIndex" SortParameterName="orderBy">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                                DefaultValue="" ConvertEmptyStringToNull="false" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <div style="margin: 12px 8px">
                        选择已经填写好的模板：<asp:FileUpload ID="FileUpload1" runat="server" />
                        <asp:Button ID="ButtonImport" runat="server" Text="导 入" CssClass="button" OnClick="ButtonImport_Click" />

                        <span style="padding-left: 100px"><a href="../Template/重点核查企业导入模板.xls">
                            <img src="../Images/xls.gif" title="下载模板" alt="下载模板" />
                            &nbsp;【点击下载重点核查企业导入模板】</a></span>
                    </div>
                </div>
            </div>
        </div>
        <uc4:IframeView ID="IframeView" runat="server" />
        <div id="winpop"></div>
    </form>
</body>
</html>
