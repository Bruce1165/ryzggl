<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TaskLE.aspx.cs" Inherits="ZYRYJG.City.TaskLE" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" />
    <link href="../Skins/Blue/Grid.Blue.css?v=1.008" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
    <link href="../layer/skin/layer.css" rel="stylesheet" />
    <script src="../layer/layer.js" type="text/javascript"></script>
    <style type="text/css">
        

        .float {
            float: left; margin:4px 4px;
            position:relative;
        }
        .absolute
        {
            position:relative;
            clear:both;
        }
        .pl50{padding-left:50px;}

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
            <script type="text/javascript">
                function openCheckWin() {
                    var divBase = document.getElementById("<%=DivBase.ClientID %>");
                    var divPop = document.getElementById("<%=TableTime.ClientID %>");
                    divBase.style.display = "none";
                    divPop.style.display = "block";
                }
                function Cancel() {
                    var divBase = document.getElementById("<%=DivBase.ClientID %>");
                    var divPop = document.getElementById("<%=TableTime.ClientID %>");
                    divBase.style.display = "block";
                    divPop.style.display = "none";
                }
                function check() {
                    var calendarStart = $find("<%= RadDatePickerStart.ClientID %>");
                    var startDate = calendarStart.get_textBox().value;
                    var calendarEnd = $find("<%= RadDatePickerEnd.ClientID %>");
                    var endDate = calendarEnd.get_textBox().value;
                    var startDate1 = startDate.replace(/-/g, "/");
                    var endDate1 = endDate.replace(/-/g, "/");
                    if (Date.parse(startDate1) - Date.parse(endDate1) > 0) {
                        alert("检查开始时间不能大于检查截止时间!");
                        return false;
                    }
                    return true;
                }
            </script>
        </telerik:RadCodeBlock>
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server">
        </telerik:RadStyleSheetManager>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" EnableAJAX="true">
            <AjaxSettings>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true"
            Skin="Windows7" />
        <telerik:RadWindowManager ID="RadWindowManager1" ShowContentDuringLoad="false" VisibleStatusbar="false"
            runat="server" Skin="Windows7" EnableShadow="true">
            <Windows>
                <telerik:RadWindow ID="RadWindow1" runat="server">
                </telerik:RadWindow>
            </Windows>
        </telerik:RadWindowManager>
        <div class="div_out">
            <div class="dqts">
                <div style="float: left;">
                    当前位置 &gt;&gt; 综合查询 &gt;&gt;<strong>检查结果填报填报</strong>
                </div>
            </div>

            <div id="DivBase" class="content" runat="server" style="width: 99%; display: block;">

                <div class="cx absolute">

                     <div  class="float" >
                        填报状态：                        
                    </div>
                    <div  class="float">
                        <asp:RadioButtonList ID="RadioButtonListCheckStatus" runat="server" Width="150px" RepeatDirection="Horizontal" TextAlign="right" CausesValidation="false" AutoPostBack="true" OnSelectedIndexChanged="RadioButtonListCheckStatus_SelectedIndexChanged">
                            <asp:ListItem Text="未填报" Value="未填报" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="已填报" Value="已填报"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div  class="float pl50">日期查询：</div>
                      <div  class="float">
                    <telerik:RadComboBox ID="RadComboBoxDateTimeItem" runat="server" Width="100px">
                        <Items>
                            <telerik:RadComboBoxItem Text="请选择属性" Value="" Selected="True" />
                            <telerik:RadComboBoxItem Text="摇号时间" Value="Yhsj" />
                            <telerik:RadComboBoxItem Text="检查时间" Value="Jcsjks" />
                        </Items>
                    </telerik:RadComboBox>
</div>
                      <div  class="float">起</div>
                    <telerik:RadDatePicker ID="RadDatePickerApplyTimeStart" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                        Width="120px" />
                     <div  class="float">至</div>
                    <telerik:RadDatePicker ID="RadDatePickerApplyTimeEnd" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                        Width="120px" />
                     <div  class="float pl50">文本查询：</div>
                      <div  class="float">
                    <telerik:RadComboBox ID="RadComboBoxIten" runat="server" Width="100px">
                        <Items>
                            <telerik:RadComboBoxItem Text="请选择属性" Value="" Selected="True" />
                            <telerik:RadComboBoxItem Text="所在单位" Value="Szdw" />
                            <telerik:RadComboBoxItem Text="人员类型" Value="Rylx" />
                            <telerik:RadComboBoxItem Text="抽查对象" Value="Ccdx" />
                            <telerik:RadComboBoxItem Text="证件号码" Value="Zjhm" />
                            <telerik:RadComboBoxItem Text="执法人" Value="Zfr" />
                            <telerik:RadComboBoxItem Text="检查结果" Value="Jcjg" />
                            <telerik:RadComboBoxItem Text="任务编号" Value="Yhrwbh" />
                            <telerik:RadComboBoxItem Text="检查单编号" Value="Zfjcbh" />
                        </Items>
                    </telerik:RadComboBox>
                          </div>
                    <telerik:RadTextBox ID="RadTextBoxValue" runat="server" Skin="Default" Width="180px">
                    </telerik:RadTextBox>
                     <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" CausesValidation="false" />
                </div>
               
                <div class="table_cx">
                    <img alt="" src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                    执法检查任务列表
                </div>
                <div style="width: 100%; margin: 0 auto;" runat="server" id="DivMain">
                    <telerik:RadGrid ID="RadGridTask" AutoGenerateColumns="False" runat="server"
                        AllowPaging="True" AllowCustomPaging="true" PageSize="10" AllowSorting="True"
                        SortingSettings-SortToolTip="单击进行排序" Skin="Blue" EnableAjaxSkinRendering="false"
                        EnableEmbeddedSkins="false" Width="100%" GridLines="None" >
                        <ClientSettings EnableRowHoverStyle="false">
                            <Selecting AllowRowSelect="false" />
                        </ClientSettings>
                        <MasterTableView CommandItemDisplay="None" NoMasterRecordsText="　没有可显示的记录">
                            <Columns>
                                <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                    <HeaderStyle HorizontalAlign="Left" Font-Bold="true" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="Yhrwbh" DataField="Yhrwbh" HeaderText="任务编号">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="Rwmc" DataField="Rwmc" HeaderText="任务名称">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="Yhsj" DataField="Yhsj" HeaderText="摇号时间" DataFormatString="{0:yy.MM.dd}">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="Ccdx" DataField="Ccdx" HeaderText="抽查对象">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="Zjhm" DataField="Zjhm"
                                    HeaderText="证件号码">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="Szdw" DataField="Szdw" HeaderText="所在单位">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="Rylx" DataField="Rylx" HeaderText="人员类型">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="Zfjcbh" DataField="Zfjcbh" HeaderText="检查单编号">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="Zfr" DataField="Zfr" HeaderText="执法人">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="Ccsx" DataField="Ccsx" HeaderText="抽查事项">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="Jcjg" DataField="Jcjg" HeaderText="检查结果">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn UniqueName="Jcsjks" HeaderText="检查时间" SortExpression="Jcsjks">
                                    <ItemTemplate>
                                        <%# Eval("Jcsjks")!=DBNull.Value ?Convert.ToDateTime(Eval("Jcsjks")).ToString("yy.MM.dd"):""%>
                                        <%# Eval("Jcsjjs")!=DBNull.Value && Convert.ToDateTime(Eval("Jcsjks")).ToString("yy.MM.dd")!=Convert.ToDateTime(Eval("Jcsjjs")).ToString("yy.MM.dd")?Convert.ToDateTime(Eval("Jcsjjs")).ToString(" - yy.MM.dd"):""%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridTemplateColumn>
                            </Columns>
                            <PagerTemplate>
                                <uc2:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                            </PagerTemplate>
                        </MasterTableView>
                        <ClientSettings>
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                        <SortingSettings SortToolTip="单击进行排序"></SortingSettings>
                        <StatusBarSettings LoadingText="正在读取数据" ReadyText="完成" />
                    </telerik:RadGrid>
                    <asp:ObjectDataSource ID="ObjectDataSourceRadGridTask" runat="server" TypeName="DataAccess.Gx_Task_ToZczxDAL"
                        DataObjectTypeName="Model.CertificateOB" SelectMethod="GetList"
                        EnablePaging="true" SelectCountMethod="SelectCount" MaximumRowsParameterName="maximumRows"
                        StartRowIndexParameterName="startRowIndex" SortParameterName="orderBy">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                                DefaultValue="" ConvertEmptyStringToNull="false" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <div style="width: 90%; margin: 5px auto; text-align: center; padding-top: 8px;">
                        <input id="BtnBH" type="button" value="任务结果填报" onclick="openCheckWin(); return false;"
                            class="bt_large" runat="server" />
                    </div>
                </div>
            </div>
            <table id="TableTime" runat="server" style="line-height: 30px; width: 500px; display: none; margin-top: 30px;">
                <tr>
                    <td style="width: 50%" align="right">检查开始时间：
                    </td>
                    <td align="left">
                        <telerik:RadDatePicker ID="RadDatePickerStart" MinDate="01/01/2020" runat="server" Width="120px" Calendar-DayCellToolTipFormat="yyyy年MM月dd日" />
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="必填"
                                                    ControlToValidate="RadDatePickerStart" Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right">检查截止时间：
                    </td>
                    <td align="left">
                        <telerik:RadDatePicker ID="RadDatePickerEnd" MinDate="01/01/2020" runat="server" Width="120px" Calendar-DayCellToolTipFormat="yyyy年MM月dd日" />
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="必填"
                                                    ControlToValidate="RadDatePickerEnd" Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right">检查结果：
                    </td>
                    <td align="left">
                        <telerik:RadComboBox ID="RadComboBoxResult" runat="server" Width="98%" NoWrap="true" >
                            <Items>
                                <telerik:RadComboBoxItem Text="请选择" Value="请选择" />
                                <telerik:RadComboBoxItem Text="未发现违法违规行为" Value="未发现违法违规行为" />
                                <telerik:RadComboBoxItem Text="责令改正 " Value="责令改正" />
                                <telerik:RadComboBoxItem Text="简易行政处罚 " Value="简易行政处罚" />
                                <telerik:RadComboBoxItem Text="一般行政处罚 " Value="一般行政处罚" />
                                <telerik:RadComboBoxItem Text="其他（写明处理结果名称）" Value="其他（写明处理结果名称）" />
                            </Items>
                        </telerik:RadComboBox>
                        <asp:CompareValidator ValueToCompare="请选择" Operator="NotEqual" ControlToValidate="RadComboBoxResult"
                            ErrorMessage="必填" runat="server" ID="Comparevalidator4" ForeColor="Red" Display="Dynamic" />
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2" style="padding: 20px 0px 100px 0px; text-align: center">
                        <asp:Button ID="ButtonOK" runat="server" Text="确 定" CssClass="button"
                            OnClientClick="javascript:if(check()==false) return false;" OnClick="ZSBH" />
                        <input id="Button1" type="button" value="取 消" class="button" onclick="javascript: Cancel();" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
