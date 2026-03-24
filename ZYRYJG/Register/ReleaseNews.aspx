<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReleaseNews.aspx.cs" ValidateRequest="false" Inherits="ZYRYJG.Register.ReleaseNews" %>

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
    <link href="../Skins/Blue/Grid.Blue.css?v=1.008" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
    <script src="../ueditor/ueditor.config.js" type="text/javascript"></script>
    <script src="../ueditor/ueditor.all.min.js" type="text/javascript"></script>
    <script src="../ueditor/lang/zh-cn/zh-cn.js" type="text/javascript"></script>
     <link href="../layer/skin/layer.css" rel="stylesheet" />
    <script src="../layer/layer.js" type="text/javascript"></script>
    <script type="text/javascript">
        function save() {
            document.getElementById("HiddenField1").value = ue.getContentTxt();
        }
    </script>
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
            <div style="margin: 5px auto; padding-bottom: 30px;">
                <div id="div_top" class="dqts">
                    <div style="float: left;">
                        当前位置 &gt;&gt;业务受理 &gt;&gt;<strong>政策通知</strong>
                    </div>
                </div>
                <div class="content">

                    <table class="cx" width="100%" border="0" align="center" cellspacing="1">
                        <tr id="TrPerson" runat="server">
                            <td width="5%" align="right" nowrap="nowrap">标题：
                            </td>

                            <td align="left" width="20%">
                                <telerik:RadTextBox ID="RadTextBoxValue" runat="server" Width="97%" Skin="Default" >
                                </telerik:RadTextBox>
                            </td>

                            <td width="10%" align="right" nowrap="nowrap">发布状态：
                            </td>

                            <td align="left" width="10%">
                                <telerik:RadComboBox ID="RadComboBox_Release" runat="server" Width="80px" OnSelectedIndexChanged="RadComboBox_Release_SelectedIndexChanged" AutoPostBack="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="全部" Value="" Selected="true" />
                                        <telerik:RadComboBoxItem Text="已发布" Value="已发布"  />
                                        <telerik:RadComboBoxItem Text="未发布" Value="未发布" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                             <td align="center" width="80px" nowrap="nowrap">
                                <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
                            </td>
                            <td align="center" width="80px" nowrap="nowrap">
                                <input id="ButtonRenew" type="button" value="添 加" class="button" onclick='javascript: SetIfrmSrc("ReleaseNew.aspx"); ' /></td>

                            <td width="80px" align="center" nowrap="nowrap">
                                <asp:Button ID="ButtonRelease" runat="server" Text="发 布" CssClass="button" OnClick="ButtonRelease_Click" />
                            </td>
                            <td align="center" width="80px" nowrap="nowrap"><asp:Button ID="ButtonDelete" runat="server" Text="删 除" CssClass="button" OnClick="ButtonDelete_Click" OnClientClick="javascript:if(!confirm('您确定要删除么?'))return false;" />
                            </td>
                           
                            <td align="left" style="padding-left: 40px">&nbsp;</td>
                        </tr>
                    </table>
                    <div style="width: 99%; margin: 8px auto;">
                        <telerik:RadGrid ID="RadGridQY" runat="server" Skin="Blue" EnableAjaxSkinRendering="False" EnableEmbeddedSkins="False"
                            GridLines="None" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" PageSize="15"
                            SortingSettings-SortToolTip="单击进行排序" Width="100%" PagerStyle-AlwaysVisible="true">

                            <ClientSettings EnableRowHoverStyle="true">
                            </ClientSettings>
                            <HeaderContextMenu EnableEmbeddedSkins="False">
                            </HeaderContextMenu>
                            <MasterTableView NoMasterRecordsText=" 没有可显示的记录" CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Left"
                                DataKeyNames="ID">
                                <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                                <Columns>

                                    <telerik:GridTemplateColumn UniqueName="SelectAllColumn">
                                        <HeaderTemplate>
                                            <uc3:CheckAll ID="CheckAll1" runat="server" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBox1" CssClass="ck" runat="server" onclick='checkBoxClick(this.checked);' />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridBoundColumn UniqueName="ID" DataField="ID" HeaderText="ID" Visible="False">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn DataField="Title" HeaderText="标题" SortExpression="Title" UniqueName="Title">
                                        <ItemTemplate>
                                            <%# Eval("Title") %>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
<%--
                                    <telerik:GridTemplateColumn DataField="Content" HeaderText="内容" SortExpression="Content" UniqueName="Content">
                                        <ItemTemplate>
                                            <%# Eval("Content").ToString().Trim().Length > 15 ? Eval("Content").ToString().Substring(0, 15).Trim() + "..." : Eval("Content").ToString().Trim() %>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>--%>
                                    <telerik:GridBoundColumn UniqueName="column3" DataField="FileUrl" HeaderText="地址" Visible="False">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>

                                    <telerik:GridDateTimeColumn DataField="GetDateTime" HeaderText="发布时间" UniqueName="GetDateTime" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridDateTimeColumn>

                                    <telerik:GridTemplateColumn UniqueName="States" DataField="States" HeaderText="发布状态">
                                        <ItemTemplate>
                                            <%# Eval("States").ToString()=="1" ?"已发布":"未发布" %>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />

                                    </telerik:GridTemplateColumn>
                                     <telerik:GridTemplateColumn UniqueName="View" DataField="View" HeaderText="预览">
                                        <ItemTemplate>
                                            <span class="link_edit" onclick='javascript:SetIfrmSrc("NewsView.aspx?o=<%#Utility.Cryptography.Encrypt( Eval("ID").ToString()) %>");'>预览</span>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />

                                    </telerik:GridTemplateColumn>

                                   
                                    <telerik:GridTemplateColumn UniqueName="Edit">
                                        <ItemTemplate>
                                            <span class="link_edit" onclick=' javascript:SetIfrmSrc("../Register/ReleaseNew.aspx?m=<%# Eval("ID") %>"); '>修改</span>
                                            <%--<span class="link_edit" onclick=' javascript:SetIfrmSrc("../Register/ReleaseNew.aspx?m=<%# Eval("ID") %>"); '>添加</span>--%>
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

                        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="Model.PolicyNewsMDL"
                            SelectMethod="GetList" TypeName="DataAccess.PolicyNewsDAL"
                            SelectCountMethod="SelectCount" EnablePaging="true"
                            MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex" SortParameterName="orderBy">
                            <SelectParameters>
                                <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                                    DefaultValue="" ConvertEmptyStringToNull="false" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </div>
                </div>

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
        }  </script>
</body>
</html>
