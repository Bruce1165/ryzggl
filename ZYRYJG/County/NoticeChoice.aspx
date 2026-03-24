<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NoticeChoice.aspx.cs" Inherits="ZYRYJG.County.NoticeChoice" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc1" %>
<%@ Register Src="../CheckAll.ascx" TagName="CheckAll" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" />
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
        <div class="div_out">
            <div class="table_border" style="width: 99%; margin: 5px auto; padding-bottom: 30px;">
                <div id="div_top" class="dqts">
                    <div style="float: left;">
                        当前位置 &gt;&gt;业务管理 &gt;&gt;<strong>选择公告</strong>
                    </div>
                </div>
                <div class="content">
                    <p class="jbxxbt">
                        选择公告
                    </p>
                    <div style="width: 95%; height: 100%; margin: 10px auto; text-align: center;">
                        <table id="tableSearch" runat="server">
                            <tr>
                                <td>
                                    <telerik:RadComboBox ID="RadComboBoxIfContinue1" runat="server">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="初始注册" Value="初始注册" />
                                            <telerik:RadComboBoxItem Text="重新注册" Value="重新注册" />
                                            <telerik:RadComboBoxItem Text="增项注册" Value="增项注册" />
                                            <telerik:RadComboBoxItem Text="延续注册" Value="延期注册" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td>&nbsp; &nbsp;决定时间
                                </td>
                                <td>
                                    <telerik:RadDatePicker ID="RadDatePickerGetDateStart" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"></telerik:RadDatePicker>
                                </td>
                                <td>至
                                </td>
                                <td>
                                    <telerik:RadDatePicker ID="RadDatePickerGetDateEnd" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"></telerik:RadDatePicker>
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="RadioButtonListReportStatus" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Text="通过" Value="通过" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="不通过" Value="不通过"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td>
                                    <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
                                </td>
                            </tr>
                        </table>
                       <%-- <div style="width: 98%; text-align: left; margin: 12px 0 12px 0; color: blue;">请勾选要公告的人员，点击“立即公告”上报到建委，点击“保存”先保存勾选结果，以后公告。</div>--%>
                        <div style="width: 100%; clear: both; position: relative;">
                            <telerik:RadGrid ID="RadGridADDRY" runat="server" Height="300"
                                GridLines="None" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" PageSize="2000"
                                SortingSettings-SortToolTip="单击进行排序" Width="100%" Skin="Default" EnableAjaxSkinRendering="true" OnDataBound="RadGridADDRY_DataBound"
                                EnableEmbeddedSkins="true" PagerStyle-AlwaysVisible="true">
                                <ClientSettings EnableRowHoverStyle="true">
                                    <Scrolling AllowScroll="True" UseStaticHeaders="true"></Scrolling>
                                </ClientSettings>
                                <HeaderContextMenu EnableEmbeddedSkins="False">
                                </HeaderContextMenu>
                                <MasterTableView NoMasterRecordsText=" 没有可显示的记录" DataKeyNames="ApplyID,NoticeMan,ConfirmResult,ApplyType" CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Left">
                                    <Columns>
                                        <telerik:GridTemplateColumn UniqueName="SelectAllColumn">
                                            <HeaderTemplate>
                                                <uc3:CheckAll ID="CheckAll1" runat="server" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBox1" CssClass="ck" runat="server" onclick='checkBoxClick(this.checked);' Checked='<%#Eval("NoticeCode")==DBNull.Value?false:true %>' />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="36" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                            <HeaderStyle HorizontalAlign="Left" Width="46" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="PSN_Name" DataField="PSN_Name" HeaderText="姓名">
                                            <HeaderStyle HorizontalAlign="Left" Width="92" />
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
                                        <telerik:GridBoundColumn UniqueName="PSN_RegisteProfession" DataField="PSN_RegisteProfession" HeaderText="注册专业">
                                            <HeaderStyle HorizontalAlign="Left" Width="92" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="PSN_RegisterNo" DataField="PSN_RegisterNo" HeaderText="注册编号">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="ApplyType" DataField="ApplyType" HeaderText="申报事项">
                                            <HeaderStyle HorizontalAlign="Left" Width="92" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                         <telerik:GridBoundColumn UniqueName="ConfirmDate" DataField="ConfirmDate" HeaderText="决定时间" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}">
                                            <HeaderStyle HorizontalAlign="Left" Width="80"  />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                          <telerik:GridBoundColumn UniqueName="ConfirmResult" DataField="ConfirmResult" HeaderText="决定结果" >
                                            <HeaderStyle HorizontalAlign="Left" Width="60"  />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>

                                    </Columns>
                                    <HeaderStyle Font-Bold="True" Wrap="false" />
                                    <PagerTemplate>
                                        <uc1:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                                    </PagerTemplate>
                                </MasterTableView>
                                <FilterMenu EnableEmbeddedSkins="False">
                                </FilterMenu>
                                <SortingSettings SortToolTip="单击进行排序"></SortingSettings>
                            </telerik:RadGrid>
                            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="Model.ApplyMDL"
                                SelectMethod="GetList" TypeName="DataAccess.ApplyDAL"
                                SelectCountMethod="SelectCount" EnablePaging="true"
                                MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex" SortParameterName="orderBy">
                                <SelectParameters>
                                    <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                                        DefaultValue="" ConvertEmptyStringToNull="false" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </div>
                    </div>
                    <div id="divMessage" runat="server" style="clear: both; position: relative; width: 100%; text-align: center; margin: 8px 0; line-height: 150%; color: blue;">
                    </div>
                    <div style="clear: both; position: relative; width: 100%; text-align: center; margin: 8px 0">
                        
                        <asp:Button ID="BtnSave" runat="server" Text="保 存" CssClass="bt_large" OnClick="BtnSave_Click" />
                      <%--  &nbsp;&nbsp; 
                            <asp:Button ID="ButtonReport" runat="server" Text="立即公告" CssClass="bt_large" OnClick="ButtonReport_Click" />--%>
                        &nbsp;&nbsp; 
                             <input id="Button2" type="button" class="bt_large" value="返 回" onclick='javascript: hideIfam();' />
                    </div>
                </div>
            </div>
        </div>
        <script type="text/javascript">
            function checkBoxAllClick(checkBoxAllClientID) {
                if (checkBoxAllClientID == undefined) return;
                var ckall = document.getElementById(checkBoxAllClientID);
                if (ckall == null) return;
                var grid = ckall.parentNode;
                while (grid != null && grid != undefined && grid.nodeName != "div") {
                    grid = grid.parentNode;
                }
                var ifSelect = ckall.checked;
                var Chks;
                if (grid == undefined)
                    Chks = document.getElementsByTagName("input");
                else
                    Chks = grid.getElementsByTagName("input");

                if (Chks.length) {
                    for (i = 0; i < Chks.length; i++) {
                        if (Chks[i].type == "checkbox") {
                            Chks[i].checked = ifSelect;
                        }
                    }
                }
                else if (Chks) {
                    if (Chks.type == "checkbox") {
                        Chks.checked = ifSelect;
                    }
                }


            }

            function GetSelectCount() {
                var o = $("#<%= divMessage.ClientID%>");

                o.html("");
                var selectCount = 0;
                var Chks = $(".ck input");
                if (Chks.length) {

                    for (i = 0; i < Chks.length; i++) {

                        if (Chks[i].type == "checkbox" && Chks[i].checked == true) {
                            selectCount += 1;
                        }
                    }
                }

                o.html("您一共选择了" + selectCount + "人");

            }

            $(function () {
                $("input").each(
                    function () {
                        $(this).click(function () { GetSelectCount(); });
                    }
                    );
            });
        </script>
    </form>
</body>
</html>
