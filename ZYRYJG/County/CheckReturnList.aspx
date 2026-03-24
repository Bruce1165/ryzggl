<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckReturnList.aspx.cs" Inherits="ZYRYJG.County.CheckReturnList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc1" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc2" %>
<%@ Register Src="../CheckAll.ascx" TagName="CheckAll" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
    <link href="../layer/skin/layer.css" rel="stylesheet" />
    <script src="../layer/layer.js" type="text/javascript"></script>
    <style type="text/css">
        .tl, .flag_red, .flag_orange, .flag_blue, .flag_green {
            color: #444;
             float: left;
            margin:0 2px;
        }
        .pl20 {
            padding-left: 20px;
        }
        .flag_red {
            background: url(../Images/flag_red.png) no-repeat left center transparent;
        }

        .flag_orange {
            background: url(../Images/flag_orange.png) no-repeat left center transparent;
        }

        .flag_blue {
            background: url(../Images/flag_blue.png) no-repeat left center transparent;
        }

        .flag_green {
            background: url(../Images/flag_green.png) no-repeat left center transparent;
        }

        .pointer {
            cursor: pointer;
            width: 16px;
            height: 16px;
        }
        .RadPicker2 {
            float: left;
            line-height: 27px;
            padding: 0px 1px;
            background-repeat: no-repeat;
        }
    </style>
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
            <div class="dqts">
                <div style="float: left;">
                    当前位置 &gt;&gt;业务管理 &gt;&gt;<strong>会审回传</strong>
                </div>
            </div>
            <div class="content">
                <table class="cx" border="0" align="center" cellspacing="1">
                    <tr id="Tr1" runat="server">
                        <td align="right" nowrap="nowrap">
                            <telerik:RadComboBox ID="RadComboBoxIten" runat="server" Width="80px">
                                <Items>                                   
                                    <telerik:RadComboBoxItem Text="姓名" Value="PSN_Name" />
                                    <telerik:RadComboBoxItem Text="证件号码" Value="PSN_CertificateNO" />
                                     <telerik:RadComboBoxItem Text="企业名称" Value="ENT_Name" />
                                    <telerik:RadComboBoxItem Text="注册号" Value="PSN_RegisterNO" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <td align="left">
                            <telerik:RadTextBox ID="RadTextBoxValue" runat="server" Skin="Default" Width="150px">
                            </telerik:RadTextBox>
                        </td>
                        <td id="zczxld" align="left" runat="server">
                            <div class="RadPicker md">会审时间：</div>
                            <telerik:RadDatePicker ID="RadDatePickerCheckDateStart" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                Width="100px" />
                            <div class="RadPicker md">至</div>
                            <telerik:RadDatePicker ID="RadDatePickerCheckDateEnd" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                Width="100px" />
                        </td>
                        <td align="right" nowrap="nowrap">注册类型：
                        </td>
                        <td align="left">
                            <telerik:RadComboBox ID="RadComboBoxApplyType" runat="server" Width="80px">
                                <Items>
                                    <telerik:RadComboBoxItem Text="全部" Value="" />
                                    <telerik:RadComboBoxItem Text="初始注册" Value="初始注册" />
                                    <telerik:RadComboBoxItem Text="重新注册" Value="重新注册" />
                                    <telerik:RadComboBoxItem Text="增项注册" Value="增项注册" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <td align="left">
                            <asp:RadioButtonList ID="RadioButtonListCityType" runat="server" RepeatDirection="Horizontal" TextAlign="right">
                                <asp:ListItem Text="未回传" Value="" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="已回传" Value="not"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td align="left">
                            <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
                        </td>
                    </tr>
                </table>
                <table style="width: 99%; line-height: 26px;" border="0" align="center" cellspacing="1">
                    <tr>
                        <td align="center">
                      <div style="width: 50%; float: left; line-height: 20px; height: 20px; vertical-align: middle; padding: 0">
                    <div class="tl pl20">图例说明：</div>
                    <div class="flag_red pl20">在施锁定，</div>
                    <div class="flag_orange pl20">重复注册，</div>
                    <div class="flag_blue pl20">社保缺失，</div>
                    <div class="flag_green pl20">新设立企业</div>
                    </div>
                            <div id="spanOutput" runat="server" style="width: 200px; float: right; text-align: left"></div>
                            <div style="width: 100px; float: right; text-align: right; vertical-align: bottom; margin: auto 12px; line-height: 26px;">
                                <asp:Button ID="ImageButtonOutput" runat="server"  OnClick="ImageButtonOutput_Click" Text="导出" Width="60px" Height="26px" BorderStyle="None" Style="background: url(../Images/xls.gif) no-repeat left center; padding-left: 18px; text-align: left" />
                            </div>
                        </td>
                    </tr>
                </table>
                <br />
                <br />
                <telerik:RadGrid ID="RadGridQY" runat="server"
                    GridLines="None" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                    SortingSettings-SortToolTip="单击进行排序" Width="99%" PagerStyle-AlwaysVisible="true">
                    <ClientSettings EnableRowHoverStyle="true">
                    </ClientSettings>
                    <HeaderContextMenu EnableEmbeddedSkins="False">
                    </HeaderContextMenu>
                    <MasterTableView NoMasterRecordsText=" 没有可显示的记录" CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Left"
                        DataKeyNames="ApplyID,PSN_ServerID">
<CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                        <Columns>
                            <telerik:GridTemplateColumn UniqueName="SelectAllColumn">
                                <HeaderTemplate>
                                    <uc3:CheckAll ID="CheckAll1" runat="server" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" CssClass="ck" runat="server" onclick='checkBoxClick(this.checked);' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="36" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="PSN_Name" DataField="PSN_Name" HeaderText="姓名">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="PSN_CertificateNO" HeaderText="证件号码">
                                <ItemTemplate>
                                    <%# Eval("PSN_CertificateNO") %>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn UniqueName="ENT_Name" DataField="ENT_Name" HeaderText="企业名称">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="PSN_RegisteProfession" DataField="PSN_RegisteProfession" HeaderText="注册专业">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="PSN_RegisterNo" DataField="PSN_RegisterNo" HeaderText="注册编号">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="ApplyTime" DataField="ApplyTime" HeaderText="申报日期" HtmlEncode="false" DataFormatString="{0:yyyy.MM.dd}">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                           
                            <telerik:GridBoundColumn UniqueName="ApplyType" DataField="ApplyType" HeaderText="申报事项">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn UniqueName="OtherDeptCheckDate" DataField="OtherDeptCheckDate" HeaderText="回传日期" HtmlEncode="false" DataFormatString="{0:yyyy.MM.dd}" Visible="False">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="CheckImg" HeaderText="校验图例">
                                <ItemTemplate>
                                    <%# (Eval("SheBaoCheck") !=DBNull.Value && Eval("SheBaoCheck").ToString()=="1" || (Eval("ApplyType").ToString() =="变更注册" && Eval("ApplyTypeSub").ToString() != "执业企业变更"))?"":string.Format("<div class=\"flag_blue pointer\" onclick='javascript:SetIfrmSrc(\"RYSBView.aspx?o1={0}&o2={1}&o3={2}\");'>&nbsp;</div>", Eval("PSN_CertificateNO"), Eval("ENT_OrganizationsCode"), Eval("ApplyTime")) %>
                                    <%# (Eval("CheckZSSD") !=DBNull.Value && Eval("CheckZSSD").ToString()=="0")?"":string.Format("<div class=\"flag_red pointer\" onclick='javascript:SetIfrmSrc(\"ZSSDDetail.aspx?z={0}\");'>&nbsp;</div>", Eval("PSN_RegisterNo")) %>
                                    <%# (Eval("CheckXSL") !=DBNull.Value &&Eval("CheckXSL").ToString()=="1")?"<div class=\"flag_green pointer\" onclick='javascript:alert(\"无企业资质\");'>&nbsp;</div>":""%>
                                    <%# (Eval("ApplyType").ToString() == "注销"? "" : Eval("CheckCFZC")== DBNull.Value ? "<div class=\"flag_orange pointer\" onclick='javascript:alert(\"尚未比对!\")'>&nbsp;</div>" : Eval("CheckCFZC").ToString() == "1" ? "<div class=\"flag_orange pointer\" onclick='javascript:alert(\"重复注册!\")'> &nbsp;</div>" : "")%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left"  />
                                <ItemStyle HorizontalAlign="Left" Wrap="false"  />
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn UniqueName="Edit" HeaderText="详细">
                                <ItemTemplate>
                                     <%--2020-02-12 南静修改参数传参加密,源代码为:a=<%# Eval("ApplyID") %>--%>
                                    <span class="link_edit" onclick='javascript:SetIfrmSrc("<%# GetApplyTypeUrl(Eval("ApplyType").ToString(),Eval("ApplyTypeSub").ToString()) %>?a=<%# Utility.Cryptography.Encrypt(Eval("ApplyID").ToString()) %>")'>详细</span>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                            </telerik:GridTemplateColumn>
                        </Columns>

<PagerStyle AlwaysVisible="True"></PagerStyle>

                        <HeaderStyle Font-Bold="True" />

<CommandItemStyle HorizontalAlign="Left"></CommandItemStyle>
                        <PagerTemplate>
                            <uc1:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                        </PagerTemplate>
                    </MasterTableView>

<PagerStyle AlwaysVisible="True"></PagerStyle>

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
            <div id="divQXCK" runat="server" style="width: 600px; margin: 0 auto; padding-top: 20px; text-align: center; clear: both;">

                <asp:Button ID="BttSave" runat="server" CssClass="bt_large" Text="批量回传" OnClick="BttSave_Click" />&nbsp;&nbsp;
                      
            </div>
        </div>
        <uc2:IframeView ID="IframeView" runat="server" />
    </form>
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

    </script>
</body>
</html>
