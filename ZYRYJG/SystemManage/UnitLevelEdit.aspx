<%@ Page Title="" Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true" CodeBehind="UnitLevelEdit.aspx.cs" Inherits="ZYRYJG.SystemManage.UnitLevelEdit" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    </telerik:RadCodeBlock>
    <telerik:RadWindowManager ID="RadWindowManager1" ShowContentDuringLoad="false" VisibleStatusbar="false"
        ReloadOnShow="true" runat="server" Skin="Windows7" EnableShadow="true">
    </telerik:RadWindowManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1"
        EnableAJAX="true">
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true" />
    <style type="text/css">
        .LSGX label {
            margin: 0 20px 0 0;
            width: 250px;
            display: inline-block;
            line-height: 14px;
        }

        .LSGX input {
            line-height: 14px;
            padding: 0 0;
            margin: 1px 2px;
        }

        .LSGX p {
            line-height: 14px;
        }
    </style>
    <div class="div_out">
        <div id="div_top" class="dqts">
            <div id="divRoad" runat="server" style="float: left;">
                当前位置 &gt;&gt; 企业隶属管理维护
            </div>
        </div>
        <div class="table_border" style="width: 98%; margin: 5px auto; min-height:300px">
            <div class="content">                
                <div id="DivFindUnit" runat="server" visible="false" style="margin:20px 0px">
                    请输入要调入企业组织机构代码（社会统一信用代码9到17位）： <asp:TextBox ID="TextBoxFindUnitCode" runat="server" Text='' Width="150px"  MaxLength="9"></asp:TextBox> <asp:Button ID="ButtonFindUnit" runat="server" Text="查 询" CssClass="button"
                                OnClick="ButtonFindUnit_Click" />
                      
                </div>
                <table id="tableLSGX" runat="server"  width="98%" cellpadding="0" cellspacing="0" border="0" align="center" style="margin-top: 10px; ">
                    <tr style="background-color: #EFEFFE; height: 30px; line-height: 200%;">
                        <td class="tousu_content" nowrap="nowrap" style="width: 15%;font-size:16px">企业名称：</td>
                        <td align="left" style="width: 35%">
                            <asp:TextBox ID="TextBoxUnitName" runat="server" Text='' Width="97%" ReadOnly="true" BorderStyle="None" BackColor="Transparent" Font-Size="16px"></asp:TextBox>
                        </td>

                        <td class="tousu_content" nowrap="nowrap" style="width: 15%;font-size:16px">组织机构代码：</td>
                        <td align="left" style="width: 35%">
                            <asp:TextBox ID="TextBoxUnitCode" runat="server" Text='' Width="97%" ReadOnly="true" BorderStyle="None" BackColor="Transparent" Font-Size="16px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr >
                        <td class="tousu_content" nowrap="nowrap" colspan="4" style="text-align: left; line-height: 30px;font-size:16px">请选择一个隶属机构：</td>
                    </tr>
                    <tr id="trLSJGList" runat="server">
                        <td align="left" id="tdLSJG" runat="server" colspan="4" class="LSGX"></td>
                    </tr>
                      <tr id="trImport" runat="server" visible="false">
                        <td align="left"  colspan="4" >
                            <asp:Label ID="LabelCurDept" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <asp:Button ID="btnSave" runat="server" Text="保 存" CssClass="button"
                                OnClick="btnSave_Click" />

                            <input id="ButtonReturn" type="button" value="返 回" class="button" onclick="javascript: hideIfam();" style="margin-left: 40px" />
                        </td>
                    </tr>
                </table>
                <br />
            </div>
        </div>
    </div>
</asp:Content>
