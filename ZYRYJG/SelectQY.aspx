<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectQY.aspx.cs" Inherits="ZYRYJG.SelectQY" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="./css/Style3.css?v=1.0.1" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="./Scripts/jquery-3.4.1.min.js"></script>
        <link href="./layer/skin/layer.css" rel="stylesheet" />
    <script src="./layer/layer.js" type="text/javascript"></script>
    <script>
        function setdata(ENT_Name, ENT_OrganizationsCode, CreditCode) {
            document.getElementById("HiddenENT_Name").value = ENT_Name;
            document.getElementById("HiddenENT_OrganizationsCode").value = ENT_OrganizationsCode;
            document.getElementById("HiddenCreditCode").value = CreditCode;
        }

        function callbackdata() {
            var data = {
                ENT_Name: document.getElementById("HiddenENT_Name").value ,
                ENT_OrganizationsCode: document.getElementById("HiddenENT_OrganizationsCode").value ,
                CreditCode: document.getElementById("HiddenCreditCode").value
            };
            return data;
        }
       
    </script>
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
                <telerik:AjaxSetting AjaxControlID="ButtonSearch">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGridQY" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="RadGridQY">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGridQY" LoadingPanelID="RadAjaxLoadingPanel1" />

                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true" Skin="Default" />
        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">          
        </telerik:RadCodeBlock>
        <div class="content">
            <p class="jbxxbt">
                企业信息查询
            </p>
            <div style="width: 95%; margin: 10px auto; text-align: center;">
                <table width="98%" border="0" align="center" cellspacing="1">
                    <tr id="TrPerson" runat="server">
                        <td width="8%" align="right" nowrap="nowrap">机构代码：
                        </td>
                        <td align="left" width="32%">
                            <telerik:RadTextBox ID="RadTextBoxENT_OrganizationsCode" runat="server" Width="90%" Skin="Default">
                            </telerik:RadTextBox>
                        </td>
                        <td width="8%" align="right" nowrap="nowrap">企业名称：
                        </td>
                        <td align="left" width="42%">
                            <telerik:RadTextBox ID="RadTextBoxENT_Name" runat="server" Width="97%" Skin="Default">
                            </telerik:RadTextBox>
                        </td>
                        <td align="left">
                            <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
                        </td>
                    </tr>
                </table>
                <telerik:RadGrid ID="RadGridQY" runat="server"
                    GridLines="None" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" OnItemDataBound="RadGridQY_ItemDataBound"
                    SortingSettings-SortToolTip="单击进行排序" Width="98%" Skin="Default" EnableAjaxSkinRendering="true"
                    EnableEmbeddedSkins="true" PagerStyle-AlwaysVisible="true" >
                    <ClientSettings EnableRowHoverStyle="true" Selecting-AllowRowSelect="true">
                    </ClientSettings>
                    <HeaderContextMenu EnableEmbeddedSkins="False">
                    </HeaderContextMenu>
                    <MasterTableView NoMasterRecordsText=" <span style='color:red'>没有查到可用的企业信息，请检查输入是否正确；或者企业从未登录过本系统，请通知企业登录办事大厅系统并点击268系统进行工商验证,完善信息。</span>" CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Left"
                        DataKeyNames="ENT_Name,ENT_OrganizationsCode,CreditCode">
                        <Columns>
                            <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="ENT_Name" DataField="ENT_Name" HeaderText="企业名称">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="ENT_OrganizationsCode" DataField="ENT_OrganizationsCode" HeaderText="机构代码">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="CreditCode" DataField="CreditCode" HeaderText="社会统一信用代码">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="ENT_City" DataField="ENT_City" HeaderText="所属区县">
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
                </telerik:RadGrid>

                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="Model.UnitMDL"
                    SelectMethod="GetList" TypeName="DataAccess.UnitDAL"
                    SelectCountMethod="SelectCount" EnablePaging="true"
                    MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex" SortParameterName="orderBy">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                            DefaultValue="" ConvertEmptyStringToNull="false" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </div>
        </div>
        <input id="HiddenENT_Name" type="hidden" />
        <input id="HiddenENT_OrganizationsCode" type="hidden" />
        <input id="HiddenCreditCode" type="hidden" />
    </form>
</body>
</html>
