<%@ Page Title="" Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true" CodeBehind="CashManage.aspx.cs" Inherits="ZYRYJG.SystemManage.CashManage" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="div_out">
        <div class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt; 系统管理 &gt;&gt; 系统配置 &gt;&gt;
                <strong>系统缓存管理</strong>
            </div>
        </div>
        <div class="table_border" style="width: 98%; margin: 5px auto;">
            <div class="content">
                <div class="jbxxbt">
                    系统缓存管理</div>

                <table id="TableEdit" runat="server" width="70%" border="0" cellpadding="5" cellspacing="1"
                    class="tableEdit" align="center">
                    <tr class="GridLightBK">
                        <td align="left" nowrap="nowrap">
                            系统缓存类型：
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td align="left" nowrap="nowrap">
                            <asp:CheckBoxList ID="CheckBoxListCash" runat="server" RepeatDirection="Horizontal" CellPadding="2" CellSpacing="2" RepeatColumns="5">
                                <asp:ListItem Text="物业企业资质库" Value="wyqy"></asp:ListItem>
                                <asp:ListItem Text="房地产开发企业资质库" Value="fdckfqy"></asp:ListItem>
                                <asp:ListItem Text="物业在岗无证人员库" Value="zgwz"></asp:ListItem>
                                <asp:ListItem Text="本地建造师" Value="本地建造师"></asp:ListItem>
                                <asp:ListItem Text="外地建造师" Value="外地建造师"></asp:ListItem>
                                <asp:ListItem Text="全部建造师" Value="全部建造师"></asp:ListItem>
                                 <asp:ListItem Text="特殊组织机构代码" Value="UnitCodeSet"></asp:ListItem>
                                  <asp:ListItem Text="续期时间配置" Value="reNewDateSpan"></asp:ListItem>
                                   <asp:ListItem Text="题库系统显示科目列表" Value="Exam_KeMu"></asp:ListItem>
                                    <asp:ListItem Text="角色访问权限" Value="RoleResourceUrlList"></asp:ListItem>
                                         <asp:ListItem Text="首页政策通知" Value="DataTableZCTZ"></asp:ListItem>        
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td align="left">
                            <asp:Button ID="ButtonClearChecked" runat="server" Text="清除勾选缓存" CssClass="bt_large"
                                OnClick="ButtonClearChecked_Click" />
                            &nbsp;
                            <asp:Button ID="ButtonClearAll" runat="server" Text="清除所有缓存" CssClass="bt_large"
                                OnClick="ButtonClearAll_Click" />
                        </td>
                    </tr>
                     <tr class="GridLightBK">
                        <td align="left">

                              <telerik:radtextbox ID="RadTextBoxLog" runat="server" Width="98%" Skin="Default" style="padding: 10px 10px;" 
                                            ReadOnly="true" TextMode="MultiLine" Rows="5" BackColor="#EDEDED">
                                           
                                        </telerik:radtextbox>
                        </td>
                    </tr>
                </table>
            <br /> <br /> <br />
            </div>
        </div>
    </div>
</asp:Content>
