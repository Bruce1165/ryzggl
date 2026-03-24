<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddUser.aspx.cs" Inherits="ZYRYJG.SystemManage.AddUser" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
    <%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc1" %>
    <%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc2" %>
    <style type="text/css">
        .link {
            border: none;
            color: blue;
            background-color: transparent;
            cursor: pointer;
            font-size: 12px;
        }
    </style>
     <script type="text/javascript">
         function Add(id)
         {
             layer.open({
                 type: 2,
                 title: ['添加用户', 'font-weight:bold;background: #5DA2EF;'],//标题
                 maxmin: true, //开启最大化最小化按钮,
                 area: ['400px', '200px'],
                 shadeClose: false, //点击遮罩关闭
                 content: 'AddUserRole.aspx?uid='+id ,//iframe的url
              
                
             });
          
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
            <div class="table_border" style="width: 99%; margin: 5px auto; padding-bottom: 30px;">
                <div class="dqts">
                    <div style="float: left;">
                        当前位置 &gt;&gt;系统管理 &gt;&gt;<strong>专网用户同步管理</strong>
                    </div>
                </div>
                <div class="content">
                    <div style="width: 95%; height: 100%; margin: 10px auto; text-align: center;">
                        <table width="98%">
                            <tr>
                                <td>
                                    用户名
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="RadTextBoxUser" runat="server" Width="97%" Skin="Default"></telerik:RadTextBox>
                                </td>
                                <td>
                                    证件号码
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="RadTextBoxzjhm" runat="server" Width="97%" Skin="Default"></telerik:RadTextBox>
                                </td>
                                   <td>
                                    同步状态
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="RadComboBoxZT" runat="server">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="全部" Value="全部" />
                                            <telerik:RadComboBoxItem Text="未同步" Value="未同步" />
                                            <telerik:RadComboBoxItem Text="已经同步" Value="已经同步" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                  <td>
                                     <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
                                </td>
                            </tr>
                        </table>
                        <br />
                         <telerik:RadGrid ID="RadGridAddUser" runat="server"
                            GridLines="None" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                            SortingSettings-SortToolTip="单击进行排序" Width="98%" Skin="Blue" EnableAjaxSkinRendering="False"
                            EnableEmbeddedSkins="False" PagerStyle-AlwaysVisible="true" >
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
                                    <telerik:GridBoundColumn UniqueName="USER_NAME" DataField="USER_NAME" HeaderText="姓名">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="SEX" DataField="SEX" HeaderText="姓名">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="ID_CARD_CODE" DataField="ID_CARD_CODE" HeaderText="证件号码">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="LOGIN_NAME" DataField="LOGIN_NAME" HeaderText="登陆名字">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="PASSWORD" DataField="PASSWORD" HeaderText="密码">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                   <telerik:GridBoundColumn UniqueName="STATUS" DataField="STATUS" HeaderText="状态">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="STATUS" DataField="STATUS" HeaderText="状态">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="LICENSE" DataField="LICENSE" HeaderText="同步状态">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                     <telerik:GridBoundColumn UniqueName="CREATOR" DataField="CREATOR" HeaderText="创建人">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                     <telerik:GridBoundColumn UniqueName="DATE_CREATE" DataField="DATE_CREATE" HeaderText="创建时间" HtmlEncode="false" DataFormatString="{0:yyyy.MM.dd}">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn UniqueName="Edit" HeaderText="操作">
                                        <ItemTemplate>
                                         <%--  <input id="Button1" runat="server" type="button" value="修改" onclick="return Add('<%#Eval("ID")%> ')" class="link"  visible='<%#Eval("Notice").ToString()=="已同步" %>' />     --%>
                                      <input id="ButtonAdd"  type="button" value="添加" class="link" onclick="return Add('<%#Eval("UserID")%>')"/>
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
                        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="Model.UserMDL"
                            SelectMethod="GetList" TypeName="DataAccess.UserDAL"
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
    </form>
</body>
</html>
