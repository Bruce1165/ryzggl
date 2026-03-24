<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RandomCheckAdd.aspx.cs" Inherits="ZYRYJG.CheckMgr.RandomCheckAdd" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc1" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc2" %>
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
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1"
            EnableAJAX="true">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="divContent">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="divContent" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>

            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true" Skin="Default" />
        <div class="div_out">
            <div id="div_top" class="dqts">
                <div style="float: left;">
                    当前位置 &gt;&gt;综合查询 &gt;&gt;<strong>检查对象人员筛查</strong>
                </div>
            </div>

            <div class="content" id="divContent" runat="server">
                <div class="DivTitleOn" onclick="DivOnOff(this,'Td3',event);" title="折叠">
                    统计说明
                </div>
                <div class="DivContent" id="Td3">
                    1、有效期失效的剔除，注销的剔除；<br />
                    2、上年度最后一笔业务，和当年最后一笔业务，对比工作单位，一致的留下；<br />
                    3、上年度最后一笔业务，和当年最后一笔业务，对比工作单位，不一致的剔除；<br />
                    4、上年度最后一笔业务，但当年没有发生业务，留下；<br />
                    5、统计证书范围：二级注册建造师、二级造价工程师、安全生产考核三类人员、建筑施工特种作业；<br />
                    6、统计业务范围：初始、重新、变更、延续、增项。
                </div>
                <table class="cx" width="100%" border="0" align="center" cellspacing="1">
                    <tr>
                        <td align="left" nowrap="nowrap">
                            <div style="float: left;">申请日期：</div>

                            <telerik:RadDatePicker ID="RadDatePicker_ApplyStartDate" MinDate="01/01/1900" runat="server" Skin="Default" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                Width="150px" Style="float: left;" />
                            <div style="float: left;">&nbsp;至&nbsp;</div>
                            <telerik:RadDatePicker ID="RadDatePicker_ApplyEndDate" MinDate="01/01/1900" runat="server" Skin="Default" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                Width="150px" Style="float: left;" />
                            &nbsp;&nbsp;
                            <div style="float: left;">办结日期：</div>

                            <telerik:RadDatePicker ID="RadDatePicker_NoticeStartDate" MinDate="01/01/1900" runat="server" Skin="Default" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                Width="150px" Style="float: left;" />
                            <div style="float: left;">&nbsp;至&nbsp;</div>
                            <telerik:RadDatePicker ID="RadDatePicker_NoticeEndDate" MinDate="01/01/1900" runat="server" Skin="Default" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                Width="150px" Style="float: left;" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" nowrap="nowrap">人员类型：
                       
                            <telerik:RadComboBox ID="RadComboBoxPersonType" runat="server" Width="150px" OnSelectedIndexChanged="RadComboBoxPersonType_SelectedIndexChanged" AutoPostBack="true">
                                <Items>
                                    <telerik:RadComboBoxItem Text="二级注册建造师" Value="二级注册建造师" />
                                    <telerik:RadComboBoxItem Text="二级造价工程师" Value="二级造价工程师" />
                                    <telerik:RadComboBoxItem Text="安全生产考核三类人员" Value="安全生产考核三类人员" />
                                    <telerik:RadComboBoxItem Text="建筑施工特种作业" Value="建筑施工特种作业" />
                                </Items>
                            </telerik:RadComboBox>
                            &nbsp;&nbsp;
                            业务类型：
                            <telerik:RadComboBox ID="RadComboBoxPSN_RegisteType" runat="server" Width="80px">
                                <Items>
                                    <telerik:RadComboBoxItem Text="全部" Value="" Selected="true" />
                                    <telerik:RadComboBoxItem Text="初始注册" Value="初始注册" />
                                    <telerik:RadComboBoxItem Text="重新注册" Value="重新注册" />
                                    <telerik:RadComboBoxItem Text="变更注册" Value="变更注册" />
                                    <telerik:RadComboBoxItem Text="增项注册" Value="增项注册" />
                                    <telerik:RadComboBoxItem Text="延续注册" Value="延期注册" />
                                </Items>
                            </telerik:RadComboBox>
                            <telerik:RadComboBox ID="RadComboBox_CongYeApplyType" runat="server" Width="80px" Visible="false">
                                <Items>
                                    <telerik:RadComboBoxItem Text="全部" Value="" Selected="true" />
                                    <telerik:RadComboBoxItem Text="考试发证" Value="考试发证" />
                                    <telerik:RadComboBoxItem Text="企业变更" Value="企业变更" />
                                    <telerik:RadComboBoxItem Text="证书续期" Value="证书续期" />
                                    <telerik:RadComboBoxItem Text="证书进京" Value="证书进京" />
                                </Items>
                            </telerik:RadComboBox>
                            <telerik:RadComboBox ID="RadComboBoxEJZJGCS" runat="server" Width="80px" Visible="false">
                                <Items>
                                    <telerik:RadComboBoxItem Text="全部" Value="" Selected="true" />
                                    <telerik:RadComboBoxItem Text="初始注册" Value="初始注册" />
                                    <telerik:RadComboBoxItem Text="变更注册" Value="变更注册" />
                                    <telerik:RadComboBoxItem Text="延续注册" Value="延续注册" />
                                </Items>
                            </telerik:RadComboBox>
                            &nbsp;&nbsp;
                       自定义查询：
                                <telerik:RadComboBox ID="RadComboBoxIten" runat="server" Width="120px">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="姓名" Value="PSN_Name" />
                                        <telerik:RadComboBoxItem Text="证件号码" Value="PSN_CertificateNO" />
                                        <telerik:RadComboBoxItem Text="单位名称" Value="ENT_Name" />
                                        <telerik:RadComboBoxItem Text="注册号/证书编号" Value="PSN_RegisterNO" />
                                        <telerik:RadComboBoxItem Text="专业" Value="ProfessionWithValid" />
                                    </Items>
                                </telerik:RadComboBox>
                            <telerik:RadTextBox ID="RadTextBoxValue" runat="server" Width="200px" Skin="Default">
                            </telerik:RadTextBox>
                            &nbsp; &nbsp;
                              <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />

                        </td>
                    </tr>

                </table>
                <div style="width: 98%; margin: 8px auto;">
                    <telerik:RadGrid ID="RadGridQY" runat="server"
                        GridLines="None" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" PageSize="10" Skin="Blue" EnableAjaxSkinRendering="False" EnableEmbeddedSkins="False"
                        SortingSettings-SortToolTip="单击进行排序" Width="100%" PagerStyle-AlwaysVisible="true">
                        <ClientSettings EnableRowHoverStyle="true">
                        </ClientSettings>
                        <HeaderContextMenu EnableEmbeddedSkins="False">
                        </HeaderContextMenu>
                        <MasterTableView NoMasterRecordsText=" 没有可显示的记录" CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Left">
                            <Columns>
                                <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="PSN_Name" DataField="PSN_Name" HeaderText="姓名">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="PSN_CertificateNO" DataField="PSN_CertificateNO" HeaderText="证件号码">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="ENT_Name" DataField="ENT_Name" HeaderText="企业名称">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="PSN_RegisterNO" DataField="PSN_RegisterNO" HeaderText="注册号">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="ProfessionWithValid" DataField="ProfessionWithValid" HeaderText="注册专业及有效期">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>

                                <telerik:GridBoundColumn UniqueName="ApplyType" DataField="ApplyType" HeaderText="申报事项">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="ApplyTime" DataField="ApplyTime" HeaderText="申报日期" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="NoticeDate" DataField="NoticeDate" HeaderText="办结日期" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                            </Columns>
                            <HeaderStyle Font-Bold="True" />
                            <PagerTemplate>
                                <uc1:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                            </PagerTemplate>
                        </MasterTableView>
                        <FilterMenu EnableEmbeddedSkins="False">
                        </FilterMenu>
                        <SortingSettings SortToolTip="单击进行排序"></SortingSettings>
                    </telerik:RadGrid>
                </div>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="Model.COC_TOW_Person_BaseInfoMDL"
                    SelectMethod="GetListCheck" TypeName="DataAccess.COC_TOW_Person_BaseInfoDAL"
                    SelectCountMethod="SelectCountCheck" EnablePaging="true"
                    MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex" SortParameterName="orderBy">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                            DefaultValue="" ConvertEmptyStringToNull="false" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" DataObjectTypeName="Model.CertificateOB"
                    SelectMethod="GetListCheck" TypeName="DataAccess.CertificateDAL"
                    SelectCountMethod="SelectCountCheck" EnablePaging="true"
                    MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex" SortParameterName="orderBy">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                            DefaultValue="" ConvertEmptyStringToNull="false" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" DataObjectTypeName="Model.zjs_CertificateMDL"
                    SelectMethod="GetListCheck" TypeName="DataAccess.zjs_CertificateDAL"
                    SelectCountMethod="SelectCountCheck" EnablePaging="true"
                    MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex" SortParameterName="orderBy">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                            DefaultValue="" ConvertEmptyStringToNull="false" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </div>
            <div style="width: 98%; margin: 8px auto;text-align:center">
                <asp:Button ID="ButtonSave" CssClass="bt_large" Text="保存筛选结果" OnClick="ButtonSave_Click"
                    runat="server"></asp:Button>
            </div>
        </div>
        <uc2:IframeView ID="IframeView" runat="server" />
        <div id="winpop">
        </div>
    </form>
</body>
</html>
