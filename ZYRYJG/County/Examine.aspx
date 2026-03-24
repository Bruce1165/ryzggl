<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Examine.aspx.cs" Inherits="ZYRYJG.County.Examine" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc1" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

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
    <style type="text/css">
        .red{
            color:red;
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
            <div class="table_border" style="width: 99%; margin: 5px auto; padding-bottom: 30px;">
                <div id="div_top" class="dqts">
                    <div style="float: left;">
                        当前位置 &gt;&gt;业务管理 &gt;&gt;<strong>企业信息变更</strong>
                    </div>
                </div>
                <div class="content">
                    <p class="jbxxbt">
                        企业信息变更
                    </p>
                    <div style="width: 100px; float: right; text-align: right; vertical-align: bottom; margin: auto 12px; line-height: 26px;">
                        <asp:Button ID="ImageButtonOutput" runat="server" Visible="false" Text="导出" Width="60px" Height="26px" BorderStyle="None" Style="background: url(../Images/xls.gif) no-repeat left center; padding-left: 18px; text-align: left" OnClick="ImageButtonOutput_Click" />
                    </div>
                    <div id="spanOutput" runat="server" style="width: 200px; float: right; text-align: right; margin-top: 10px"></div>
                    <br />
                    <br />
                    <div style="width: 99%; height: 100%; margin: 10px auto; text-align: center;">
                        <telerik:RadGrid ID="RadGridQY" runat="server"
                            GridLines="None" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false"
                            SortingSettings-SortToolTip="单击进行排序" Width="100%" Skin="Blue" EnableAjaxSkinRendering="false"
                            EnableEmbeddedSkins="false" PagerStyle-AlwaysVisible="true">
                            <ClientSettings EnableRowHoverStyle="true">
                            </ClientSettings>
                            <HeaderContextMenu EnableEmbeddedSkins="False">
                            </HeaderContextMenu>
                            <MasterTableView NoMasterRecordsText=" 没有可显示的记录" CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Left"
                                DataKeyNames="ENT_OrganizationsCode">
                                <Columns>
                                    <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>                                   
                                    <telerik:GridBoundColumn UniqueName="ENT_OrganizationsCode" DataField="ENT_OrganizationsCode" HeaderText="组织机构代码">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>                         
                                    <telerik:GridTemplateColumn UniqueName="ENT_NameFrom" HeaderText="企业名称">
                                        <ItemTemplate>
                                            <div>变更前：<%# Eval("ENT_NameFrom") %></div>
                                            <div <%# ZYRYJG.UIHelp.FormatKuoHao(Eval("ENT_NameFrom").ToString())  !=ZYRYJG.UIHelp.FormatKuoHao(Eval("ENT_NameTo").ToString()) ?"class='red'":"" %>>变更后：<%# Eval("ENT_NameTo") %></div>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Left" Wrap="false"  />
                                    </telerik:GridTemplateColumn>                               
                                     <telerik:GridBoundColumn UniqueName="Num" DataField="Num" HeaderText="人数">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn UniqueName="FromEND_Addess" HeaderText="工商注册地址">
                                        <ItemTemplate>
                                            <div>变更前：<%# Eval("FromEND_Addess") %></div>
                                            <div <%# Eval("FromEND_Addess").ToString()  !=Eval("ToEND_Addess").ToString() ?"class='red'":"" %>>变更后：<%# Eval("ToEND_Addess") %></div>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Left" Wrap="false"  />
                                    </telerik:GridTemplateColumn>                           
                                    <telerik:GridTemplateColumn UniqueName="FromENT_City" HeaderText="监管区县">
                                        <ItemTemplate>
                                            <div>变更前：<%# Eval("FromENT_City") %></div>
                                            <div <%# Eval("FromENT_City").ToString() !=Eval("ToENT_City").ToString() ?"class='red'":"" %>>变更后：<%# Eval("ToENT_City") %></div>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Left" Wrap="false"  />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn UniqueName="Edit" HeaderText="详细">
                                        <ItemTemplate>
                                            <span class="link_edit" onclick='javascript:SetIfrmSrc("../Unit/ApplyChangeEnterprise.aspx?zzjgdm=<%# Utility.Cryptography.Encrypt(Eval("ENT_OrganizationsCode").ToString())%>")'>详细</span>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                                    </telerik:GridTemplateColumn>
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
                        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="Model.ApplyMDL"
                            SelectMethod="GetApplyChangeList" TypeName="DataAccess.ApplyChangeDAL"
                            SelectCountMethod="SelectApplyChangeCount" EnablePaging="true"
                            MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex" SortParameterName="orderBy">
                            <SelectParameters>
                                <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                                    DefaultValue="" ConvertEmptyStringToNull="false" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                        <div id="divQX" runat="server" visible="false" style="width: 100%; padding-top: 20px; text-align: center; clear: both;">

                            <table id="TableEdit" runat="server" width="100%" border="0" cellpadding="5" cellspacing="1" class="table" style="text-align: center; margin: 10px auto">
                                <tr class="GridLightBK">
                                    <td colspan="2" class="barTitle">受理操作</td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td width="20%" align="right">受理结果：</td>
                                    <td width="80%" align="left">
                                        <asp:RadioButtonList ID="RadioButtonListApplyStatus" runat="server" RepeatDirection="Horizontal" TextAlign="right">
                                            <asp:ListItem Text="通过" Value="通过" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="不通过" Value="不通过"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td width="20%" align="right">受理意见：</td>
                                    <td width="80%" align="left">

                                        <asp:TextBox ID="TextBoxApplyGetResult" runat="server" TextMode="MultiLine" Rows="3" Width="99%" Text="予以受理"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td colspan="2" align="center">
                                        <asp:Button ID="BtnSave" runat="server" CssClass="bt_large" Text="确认提交" OnClick="BtnSave_Click" />&nbsp;&nbsp;
                                            <input id="BtnReturn" type="button" class="bt_large" value="取消提交" onclick='javascript: hideIfam()' />
                                    </td>
                                </tr>
                            </table>

                        </div>

                    </div>
                </div>
            </div>
        </div>
        <uc2:IframeView ID="IframeView" runat="server" />
    </form>
</body>
</html>
