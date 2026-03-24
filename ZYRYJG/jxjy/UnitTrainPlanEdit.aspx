<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="UnitTrainPlanEdit.aspx.cs" Inherits="ZYRYJG.jxjy.UnitTrainPlanEdit" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../PostSelect.ascx" TagName="PostSelect" TagPrefix="uc1" %>
<%@ Register Src="~/PostAllSelect.ascx" TagPrefix="uc2" TagName="PostAllSelect" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" />
    <link href="../Skins/Blue/Grid.Blue.css?v=1.008" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
    <script src="../Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
    <link href="../layer/skin/layer.css" rel="stylesheet" />
    <script src="../layer/layer.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Windows7">
        </telerik:RadWindowManager>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="PostSelect1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="PostSelect1" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="RadGridSource">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="DivTip" UpdatePanelRenderMode="Inline" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="HiddenFieldSelectPreid" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="RadComboBoxPackageYear">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGridSource" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="DivTip" UpdatePanelRenderMode="Inline" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="HiddenFieldSelectPreid" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true"
            Skin="Windows7" />
        <div class="div_out">
            <div id="div_top" class="dqts">
                <div style="float: left;">
                    当前位置 &gt;&gt;在线公益教育培训 &gt;&gt;<strong>企业培训计划管理</strong>
                </div>
            </div>
            <div class="content">
                <table width="95%" border="0" cellpadding="5" cellspacing="1" class="table" align="center">
                    <tr class="GridLightBK">
                        <td align="right">
                            <span style="color: Red">* </span>培训专业方向：
                        </td>
                        <td align="left">

                            <uc2:PostAllSelect runat="server" ID="PostSelect1" />
                        </td>
                        <td align="left" colspan="2">（专业类别不允许为空；专业允许为空，表示课程适用于改专业类别下所有专业。）</td>
                    </tr>
                    <tr class="GridLightBK">
                        <td align="right">
                            <span style="color: Red">* </span>要求完成课时：
                        </td>
                        <td align="left">
                            <telerik:RadNumericTextBox ID="RadNumericTextBoxPeriodHour" runat="server" MaxLength="3" Type="Number" NumberFormat-DecimalDigits="0" ShowSpinButtons="true" Width="120px" MinValue="0">
                                <NumberFormat DecimalDigits="0"></NumberFormat>
                            </telerik:RadNumericTextBox>
                            学时（说明：45分钟为1学时）
                            <asp:HiddenField ID="HiddenFieldSelectPreid" runat="server" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="Dynamic" ErrorMessage="必填" ControlToValidate="RadNumericTextBoxPeriodHour"></asp:RequiredFieldValidator>
                        </td>
                        <td width="15%" align="right">
                            <span style="color: Red">* </span>发布状态：
                        </td>
                        <td width="35%" align="left">
                            <asp:RadioButtonList ID="RadioButtonListPublishStatus" runat="server" RepeatDirection="Horizontal" AutoPostBack="false">
                                <asp:ListItem Value="已发布" Selected="True">已发布</asp:ListItem>
                                <asp:ListItem Value="未发布">未发布</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td align="right">课程简介：
                        </td>
                        <td align="left" colspan="3">
                            <telerik:RadTextBox ID="RadTextBoxDescription" runat="server" Width="95%" Skin="Default" Rows="4" TextMode="MultiLine" MaxLength="2000">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td colspan="2" align="left" style="padding-left: 20px;">
                            <span style="color: Red">* </span>√ 请在下面列表中勾选培训计划包含的课程
                             <telerik:RadComboBox ID="RadComboBoxPackageYear" runat="server" AutoPostBack="true" Visible="false"
                                CausesValidation="false" OnSelectedIndexChanged="RadComboBoxPackageYear_SelectedIndexChanged">
                            </telerik:RadComboBox>
                        </td>
                       
                        <td align="left" colspan="2" >
                           请在下面人员列表中勾选要参加培训的人员名单
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td colspan="2" valign="top"  align="center">
                            <telerik:RadGrid ID="RadGridSource" runat="server" AutoGenerateColumns="False" AllowPaging="False" EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false" Skin="Blue" Width="95%">
                                <ClientSettings EnableRowHoverStyle="true">
                                </ClientSettings>
                                <MasterTableView CommandItemDisplay="None" DataKeyNames="SourceID,ShowPeriod" NoMasterRecordsText="　没有可显示的记录">
                                    <Columns>
                                        <telerik:GridTemplateColumn UniqueName="SelectAllColumn" HeaderText="">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBox1" runat="server" OnCheckedChanged="CheckBox1_CheckedChanged"
                                                    AutoPostBack="true" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn HeaderText="行号" UniqueName="RowNum" DataField="RowNum">
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="年度" UniqueName="SourceYear" DataField="SourceYear">
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="课程名称" UniqueName="SourceName" DataField="SourceName">
                                            <HeaderStyle HorizontalAlign="Left" Wrap="false" Width="50%" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="学时" UniqueName="ShowPeriod" DataField="ShowPeriod">
                                            <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>

                                    </Columns>
                                    <FooterStyle Wrap="false" />
                                    <HeaderStyle Font-Bold="true" />
                                </MasterTableView>
                            </telerik:RadGrid>
                        </td>
                        <td colspan="2"  valign="top" align="center">
                            <telerik:RadGrid ID="RadGridworker" AutoGenerateColumns="False"
                                runat="server" AllowPaging="True" PageSize="35" AllowSorting="True" SortingSettings-SortToolTip="单击进行排序"
                                Skin="Blue" EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false" Width="95%"
                                GridLines="None">
                                <ClientSettings EnableRowHoverStyle="true">
                                </ClientSettings>
                                <MasterTableView CommandItemDisplay="None" AllowMultiColumnSorting="True" DataKeyNames="WORKERID"
                                    NoMasterRecordsText="　没有可显示的记录">

                                    <Columns>
                                        <telerik:GridTemplateColumn UniqueName="SelectAllColumn" HeaderText="">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBox1" runat="server" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                            <HeaderStyle HorizontalAlign="Center" Wrap="true" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="true" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="WorkerName" DataField="WorkerName" HeaderText="姓名">
                                            <HeaderStyle HorizontalAlign="Center" Wrap="true" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="true" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="SEX" DataField="SEX" HeaderText="性别">
                                            <HeaderStyle HorizontalAlign="Center" Wrap="true" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="true" />
                                        </telerik:GridBoundColumn>


                                        <telerik:GridTemplateColumn UniqueName="BIRTHDAY" HeaderText="年龄">
                                            <ItemTemplate>
                                                <%#Eval("BIRTHDAY")==DBNull.Value || ((DateTime)Eval("BIRTHDAY")).Year == 1900 ? "" : Convert.ToString(DateTime.Now.Year -((DateTime)Eval("BIRTHDAY")).Year)%>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" Font-Bold="true" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn UniqueName="CERTIFICATECODE" DataField="CERTIFICATECODE"
                                            HeaderText="证件号码">
                                            <HeaderStyle HorizontalAlign="Center" Wrap="true" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="CULTURALLEVEL" DataField="CULTURALLEVEL" HeaderText="文化程度">
                                            <HeaderStyle HorizontalAlign="Center" Wrap="true" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="true" />
                                        </telerik:GridBoundColumn>

                                    </Columns>
                                    <PagerTemplate>
                                        <uc3:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                                    </PagerTemplate>
                                </MasterTableView>
                                <SortingSettings SortToolTip="单击进行排序"></SortingSettings>
                                <StatusBarSettings LoadingText="正在读取数据" ReadyText="完成" />
                            </telerik:RadGrid>
                            <asp:ObjectDataSource ID="ObjectDataSourceWorker" runat="server" TypeName="DataAccess.WorkerDAL"
                                DataObjectTypeName="Model.WorkerOB" SelectMethod="GetList" EnablePaging="true"
                                SelectCountMethod="SelectCount" MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex"
                                SortParameterName="orderBy">
                                <SelectParameters>
                                    <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                                        DefaultValue="" ConvertEmptyStringToNull="false" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="DivTip" runat="server" style="text-align: center; padding-left: 20px; font-size: 16px; line-height: 30px">
            </div>
            <div style="padding-top: 10px; vertical-align: middle; padding-bottom: 20px; text-align: center">
                <asp:Button ID="ButtonSave" runat="server" Text="保 存" CssClass="button" OnClick="ButtonSave_Click" />&nbsp;&nbsp;
                <input id="ButtonReturn" type="button" value="返 回" class="button" onclick="javascript: parent.refreshGrid(); hideIfam();" />
            </div>
        </div>
        <div id="winpop">
        </div>
    </form>
</body>
</html>
