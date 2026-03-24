<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Acceptance.aspx.cs" Inherits="ZYRYJG.County.Acceptance" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc1" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc2" %>
<%@ Register Src="../CheckAll.ascx" TagName="CheckAll" TagPrefix="uc3" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
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
                        当前位置 &gt;&gt;业务管理 &gt;&gt;<strong>受理单打印</strong>
                    </div>
                </div>
                <div class="content">
                    
                    
                    <div style="width: 100%; margin: 10px auto; text-align: center; clear: both;">
                        <table id="tableSearch" runat="server" width="98%" border="0" align="center" cellspacing="1">
                            <tr id="TrPerson" runat="server">
                                <td align="right" width="65px" style="font-weight:bold;">注册类型：
                                </td>
                                <td align="left" width="100px">
                                    <telerik:RadComboBox ID="RadComboBoxIfContinue1" runat="server" Width="100">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="请选择" Value="" />
                                            <telerik:RadComboBoxItem Text="初始注册" Value="初始注册" />
                                            <telerik:RadComboBoxItem Text="重新注册" Value="重新注册" />
                                            <telerik:RadComboBoxItem Text="增项注册" Value="增项注册" />
                                            <telerik:RadComboBoxItem Text="延续注册" Value="延期注册" />
                                            <telerik:RadComboBoxItem Text="遗失补办" Value="遗失补办" />
                                            <telerik:RadComboBoxItem Text="注销" Value="注销" />
                                            <telerik:RadComboBoxItem Text="执业企业变更" Value="执业企业变更" />
                                            <telerik:RadComboBoxItem Text="个人信息变更" Value="个人信息变更" />
                                            <telerik:RadComboBoxItem Text="企业信息变更" Value="企业信息变更" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td align="right" width="65px" style="font-weight:bold;">注册级：
                                </td>
                                <td align="left" width="150px">
                                    <asp:RadioButtonList ID="RadioButtonListPSN_Level" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Text="二级" Value="二级" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="二级临时" Value="二级临时"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td width="65px" align="right" nowrap="nowrap">
                                    <telerik:RadComboBox ID="RadComboBoxIten" runat="server" Width="100">
                                        <Items>                                            
                                            <telerik:RadComboBoxItem Text="姓名" Value="PSN_Name" />
                                            <telerik:RadComboBoxItem Text="证件号码" Value="PSN_CertificateNO" />                                            
                                            <telerik:RadComboBoxItem Text="企业名称" Value="ENT_Name" />
                                            <telerik:RadComboBoxItem Text="注册号" Value="PSN_RegisterNO" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td align="left" width="200px">
                                    <telerik:RadTextBox ID="RadTextBoxValue" runat="server" Width="97%" Skin="Default">
                                    </telerik:RadTextBox>
                                </td>
                                
                                <td align="left">
                                    <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
                                </td>
                                <td align="right"></td>
                            </tr>
                        </table>
                      
                        <div style="width: 100%; clear: both; position: relative;">
                            <telerik:RadGrid ID="RadGridADDRY" runat="server" Height="300"
                                GridLines="None" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" PageSize="500"
                                SortingSettings-SortToolTip="单击进行排序" Width="100%" Skin="Default" EnableAjaxSkinRendering="true"
                                EnableEmbeddedSkins="true" PagerStyle-AlwaysVisible="true" OnDataBound="RadGridADDRY_DataBound">
                                <ClientSettings EnableRowHoverStyle="true">
                                    <Scrolling AllowScroll="True" UseStaticHeaders="true"></Scrolling>
                                </ClientSettings>
                                <HeaderContextMenu EnableEmbeddedSkins="False">
                                </HeaderContextMenu>
                                <MasterTableView NoMasterRecordsText=" 没有可显示的记录" DataKeyNames="ApplyID,ReportMan,ReportDate" CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Left">
                                    <Columns>
                                        <telerik:GridTemplateColumn UniqueName="SelectAllColumn">
                                            <HeaderTemplate>
                                                <uc3:CheckAll ID="CheckAll1" runat="server" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBox1" CssClass="ck" runat="server" onclick='checkBoxClick(this.checked);' Checked='<%#ViewState["ReportCode"]==null||ViewState["ReportCode"].ToString() !=Eval("ReportCode").ToString()?false:true %>' />
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
                                        <telerik:GridBoundColumn UniqueName="ApplyTime" DataField="ApplyTime" HeaderText="申请时间" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}">
                                            <HeaderStyle HorizontalAlign="Left" Width="92" />
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
                        <asp:Button ID="BtnSave" runat="server" Text="打 印 " CssClass="bt_large" OnClick="BtnSave_Click" />
                        &nbsp;&nbsp; 
                        <%=string.IsNullOrEmpty(Request["o1"]) == true && ViewState["ReportCode"] != null?"<input id=\"Button2\" type=\"button\" class=\"bt_large\" value=\"新 建\" onclick='javascript: document.location=\"ChoiceBusinessSummarizing.aspx?invoke=\" +  Math.random();' />":""%>
                        <%=string.IsNullOrEmpty(Request["o1"]) == false?"<input id=\"Button2\" type=\"button\" class=\"bt_large\" value=\"返 回\" onclick='javascript: hideIfam();' />":""%>
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
