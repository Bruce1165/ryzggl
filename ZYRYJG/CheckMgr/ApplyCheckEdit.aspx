<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplyCheckEdit.aspx.cs" Inherits="ZYRYJG.CheckMgr.ApplyCheckEdit" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc1" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc2" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
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
        <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server">
        </telerik:RadStyleSheetManager>
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Windows7">
        </telerik:RadWindowManager>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1"
            EnableAJAX="true">
            <AjaxSettings>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true" Skin="Default" />
        <div class="div_out">
            <div id="div_top" class="dqts">
                <div style="float: left;">
                    当前位置 &gt;&gt;综合查询 &gt;&gt;<strong>业务抽查创建</strong>
                </div>
            </div>
            <div class="content">
                <table class="detailTable" cellpadding="5" cellspacing="1" style="margin: 12px auto;">
                    <tr class="GridLightBK">
                        <td colspan="4" class="barTitle">抽查参数设置</td>
                    </tr>
                    <tr>
                        <td class="infoHead" style="width: 150px" rowspan="4">抽查业务范围：
                        </td>
                        <td class="formItem_2" rowspan="4">
                            <asp:CheckBoxList ID="CheckBoxListBusRange" runat="server">
                                <asp:ListItem Text="二建注册建造师" Value="1"></asp:ListItem>
                                <asp:ListItem Text="二级注册造价工程师" Value="2"></asp:ListItem>
                                <asp:ListItem Text="安全生产管理人员" Value="3"></asp:ListItem>
                                <asp:ListItem Text="特种作业人员" Value="4"></asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                        <td class="infoHead" style="width: 150px">业务办结日期：</td>
                        <td class="formItem_1">
                            <div class="RadPicker md">自</div><telerik:RadDatePicker ID="RadDatePickerStart" MinDate="01/01/2010" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                    Width="130px" />
                                <div class="RadPicker md">至</div>
                                <telerik:RadDatePicker ID="RadDatePickerEnd" MaxDate="01/01/2050" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"  
                                    Width="130px" />
                        </td>

                    </tr>
                    <tr>
                        <td class="infoHead">抽查千分比例：
                        </td>
                        <td class="formItem_1">
                            <telerik:RadNumericTextBox ID="RadNumericTextBoxCheckPer" runat="server" MaxLength="3"
                                Type="Number" NumberFormat-DecimalDigits="0" ShowSpinButtons="true" Width="50px"
                                Value="5">
                                <NumberFormat DecimalDigits="0"></NumberFormat>
                            </telerik:RadNumericTextBox>‰
                        </td>
                       
                    </tr>
                     <tr>
                        <td class="infoHead">抽查时间：
                        </td>
                        <td class="formItem_1">    
                            <asp:Label ID="Labelcjsj" runat="server" Text=""></asp:Label>                        
                        </td>
                     </tr>
                     <tr>
                       <td class="infoHead">抽查结果记录数：
                        </td>
                        <td class="formItem_1">    
                            <asp:Label ID="LabelItemCount" runat="server" Text=""></asp:Label>                        
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center;line-height:200%" colspan="4">
                            <asp:Button ID="ButtonSave" runat="server" Text="开始抽查" CssClass="bt_large" OnClick="ButtonSave_Click" />&nbsp;
                            <asp:CheckBox ID="CheckBoxIfDelHis" runat="server" Text="覆盖上次抽查结果（覆盖将删除抽查记录及抽查审核结果）" />
                        </td>                                    
                    </tr>                    
                </table>
                <div style="width: 98%; margin: 12px auto; padding-bottom: 140px;">
                    <telerik:RadGrid ID="RadGridApplyCheckItem" runat="server"
                        GridLines="None" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" PageSize="15"
                        SortingSettings-SortToolTip="单击进行排序" Width="100%" Skin="Blue" EnableAjaxSkinRendering="False" EnableEmbeddedSkins="False" PagerStyle-AlwaysVisible="true">
                        <ClientSettings EnableRowHoverStyle="true">
                        </ClientSettings>
                        <HeaderContextMenu EnableEmbeddedSkins="False">
                        </HeaderContextMenu>
                        <MasterTableView NoMasterRecordsText=" 没有可显示的记录" CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Center"
                            DataKeyNames="TaskItemID">
                            <Columns>
                                <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="CheckType" DataField="CheckType" HeaderText="监管类型">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>

                                <telerik:GridBoundColumn UniqueName="WorkerName" DataField="WorkerName" HeaderText="人员姓名">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                 <telerik:GridBoundColumn UniqueName="IDCardType" DataField="IDCardType" HeaderText="证书类别">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="IDCard" DataField="IDCard" HeaderText="证件号码">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="CertificateCode" DataField="CertificateCode" HeaderText="证书编号">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="PostTypeName" DataField="PostTypeName" HeaderText="证书类别">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                 <telerik:GridTemplateColumn UniqueName="BusTypeID" HeaderText="证书类别">
                                    <ItemTemplate>
                                        <%# GetBusTypeName(Convert.ToInt32(Eval("BusTypeID")))%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" Font-Bold="true" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn UniqueName="ApplyType" DataField="ApplyType" HeaderText="业务类别">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="ApplyFinishTime" DataField="ApplyFinishTime" HeaderText="业务办结日期" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
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
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="Model.ApplyCheckTaskItemMDL"
                    SelectMethod="GetList" TypeName="DataAccess.ApplyCheckTaskItemDAL"
                    SelectCountMethod="SelectCount" EnablePaging="true"
                    MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex" SortParameterName="orderBy">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                            DefaultValue="" ConvertEmptyStringToNull="false" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </div>
        </div>
    </form>
</body>
</html>
