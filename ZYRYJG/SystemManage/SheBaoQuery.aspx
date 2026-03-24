<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SheBaoQuery.aspx.cs" Inherits="ZYRYJG.SystemManage.SheBaoQuery" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" />
    <link href="../Skins/Blue/Grid.Blue.css?v=1.008" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
    <link href="../layer/skin/layer.css" rel="stylesheet" />
    <script src="../layer/layer.js" type="text/javascript"></script>
    <link href="../css/timelimit.css?v=1.001" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Scripts/timelimit.js?v=1.003"></script>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Windows7">
        </telerik:RadWindowManager>
        <div class="div_out">
            <div class="dqts">
                <div style="float: left;">
                    当前位置 &gt;&gt;系统管理 &gt;&gt;<strong>社保查询</strong>
                </div>
            </div>
            <div class="content">
                <table class="cx" width="100%" border="0" align="center" cellspacing="1">
                    <tr>
                        <td width="12%" align="right" nowrap="nowrap">社会保障号码
                        </td>
                        <td align="left" width="30%">
                            <telerik:RadTextBox ID="RadTextBoxSBCode" runat="server" Width="97%" Skin="Default">
                            </telerik:RadTextBox>
                            <asp:RequiredFieldValidator ID="ValidatorRadTextBoxSBCode" runat="Server" ControlToValidate="RadTextBoxSBCode"
                                ErrorMessage="社会保障号码" Display="Dynamic">*社会保障号码 </asp:RequiredFieldValidator>
                        </td>
                        <td width="12%" align="right" nowrap="nowrap">缴费日期：
                        </td>
                        <td align="left" width="30%">
                            <telerik:RadDatePicker ID="RadDatePickerStart" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                Width="150px" />
                            <div class="RadPicker md">至</div>
                            <telerik:RadDatePicker ID="RadDatePickerEnd" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                Width="150px" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="Server" ControlToValidate="RadDatePickerStart"
                                ErrorMessage="请输入缴费开始日期" Display="Dynamic">*请输入缴费开始日期</asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="Server" ControlToValidate="RadDatePickerEnd"
                                ErrorMessage="请输入缴费结束日期" Display="Dynamic">*请输入缴费结束日期</asp:RequiredFieldValidator>
                        </td>

                        <td align="left" style="padding-left: 40px">
                            <asp:Button ID="ButtonSearch" runat="server" Text="查询社保" CssClass="button" OnClick="ButtonSearch_Click" />
                            <asp:Button ID="ButtonSearchSheBaoCode" runat="server" Text="查询社会保障号" CssClass="bt_large" OnClick="ButtonSearchSheBaoCode_Click" />
                        </td>
                    </tr>

                </table>
                <div id="divResult" runat="server" style ="margin:20px 20px">
                     <telerik:RadGrid ID="RadGrid1" AllowPaging="True"
                    SortingSettings-SortToolTip="单击进行排序" runat="server" AutoGenerateColumns="False"
                    AllowSorting="True" GridLines="None" CellPadding="0" Width="100%" Skin="Blue"
                    EnableAjaxSkinRendering="False" EnableEmbeddedSkins="False"  PagerStyle-AlwaysVisible="true"              >
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                    <MasterTableView  NoMasterRecordsText="没有可显示的记录">
                        <Columns>
                            <telerik:GridBoundColumn HeaderText="序号" UniqueName="RowNum" DataField="RowNum" AllowSorting="false">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="缴费年月" UniqueName="JFYF" DataField="JFYF">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="险种" UniqueName="XZName" DataField="XZName">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>     
                              <telerik:GridBoundColumn UniqueName="ENT_Name" DataField="ENT_Name" HeaderText="缴费单位">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>                       
                            <telerik:GridBoundColumn UniqueName="WorkerName" DataField="WorkerName" HeaderText="姓名">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="CertificateCode" DataField="certificateCode"
                                HeaderText="社会保障号码">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>                          
                           <telerik:GridBoundColumn HeaderText="写入日期" UniqueName="CJSJ" DataField="CJSJ"
                                DataFormatString="{0:yyyy-MM-dd}">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>                            
                        </Columns>
                        <HeaderStyle Font-Bold="True" />
                        <PagerTemplate>
                            <uc2:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                        </PagerTemplate>
                    </MasterTableView>
                    <SortingSettings SortToolTip="单击进行排序"></SortingSettings>
                    <StatusBarSettings LoadingText="正在读取数据" ReadyText="完成" />
                </telerik:RadGrid>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="DataAccess.SheBaoDAL"
                    DataObjectTypeName="Model.SheBaoMDL" SelectMethod="GetList" EnablePaging="true"
                    SelectCountMethod="SelectCount" MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex"
                    SortParameterName="orderBy">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                            DefaultValue="and ApplyStatus='已申请'" ConvertEmptyStringToNull="false" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                </div>
            </div>
        </div>
         <div id="winpop">
        </div>
    </form>
</body>
</html>
