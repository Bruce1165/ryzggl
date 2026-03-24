<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CertPrintMgr.aspx.cs" Inherits="ZYRYJG.County.CertPrintMgr" %>

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
    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
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
                        当前位置 &gt;&gt;建造师注册管理 &gt;&gt;<strong>证书打印</strong>
                    </div>
                </div>
                <div class="content">

                    <div style="width: 100%; margin: 10px auto; text-align: center;">
                        <table width="98%" border="0" align="center" cellspacing="1" style="margin: 6px 0">
                            <tr>
                                <td width="64px" align="right" nowrap="nowrap">打印类别：
                                </td>
                                <td align="left" width="74px">
                                    <telerik:RadComboBox ID="RadComboBoxPrintType" runat="server" Width="64">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="防伪条" Value="防伪条" />
                                            <telerik:RadComboBoxItem Text="证书" Value="证书" Selected="true" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                   <td width="74px" align="right" nowrap="nowrap">证书等级：
                                </td>
                                <td align="left" width="90px">
                                    <telerik:RadComboBox ID="RadComboBoxPSN_Level" runat="server" Width="80">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="二级" Value="二级" Selected="true" />
                                            <telerik:RadComboBoxItem Text="二级临时" Value="二级临时" />

                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                               <%-- <td width="64px" align="right" nowrap="nowrap">
                                </td>--%>
                                <td align="left"  colspan="5">                                   
                                    证书号段：<asp:TextBox ID="TextBoxPSN_RegisterCertificateNo_from" runat="server" Width="70px" MaxLength="8"></asp:TextBox> 至 <asp:TextBox ID="TextBoxPSN_RegisterCertificateNo_to" runat="server" Width="70px" MaxLength="8"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                              
                                <td width="64px" align="right" nowrap="nowrap">申报事项：
                                </td>
                                <td align="left" width="90px">
                                    <telerik:RadComboBox ID="RadComboBoxPSN_RegisteType" runat="server" Width="80">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="全部" Value="全部" />
                                            <telerik:RadComboBoxItem Text="初始注册" Value="01" />
                                            <telerik:RadComboBoxItem Text="重新注册" Value="05" />
                                            <telerik:RadComboBoxItem Text="增项注册" Value="04" />
                                            <telerik:RadComboBoxItem Text="延续注册" Value="03" />
                                            <telerik:RadComboBoxItem Text="变更注册" Value="02" />
                                            <telerik:RadComboBoxItem Text="遗失补办" Value="06" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td width="64px" align="right" nowrap="nowrap">审核日期：
                                </td>
                                <td align="left" width="110px">
                                    <telerik:RadDatePicker ID="RadDatePickerGetDateStart" MinDate="1950-1-1" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                        Width="100px" />
                                </td>
                                <td width="80px" align="right" nowrap="nowrap">
                                    <telerik:RadComboBox ID="RadComboBoxIten" runat="server" Width="80px">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="姓名" Value="PSN_Name" />
                                            <telerik:RadComboBoxItem Text="身份证号" Value="PSN_CertificateNO" />
                                            <telerik:RadComboBoxItem Text="企业名称" Value="ENT_Name" />
                                            <telerik:RadComboBoxItem Text="注册号" Value="PSN_RegisterNO" />
                                            <telerik:RadComboBoxItem Text="证书编号" Value="PSN_RegisterCertificateNo" />
                                            <telerik:RadComboBoxItem Text="公告批次号" Value="NoticeCode" />
                                             <telerik:RadComboBoxItem Text="监管区县" Value="ENT_City" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td align="left" width="180px">                                   
                                    <asp:TextBox ID="TextBoxValue" runat="server" Width="99%"></asp:TextBox>
                                </td>
                                <td align="left" style="padding-left:20px">
                                    <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
                                </td>

                            </tr>
                        </table>
                        <telerik:RadGrid ID="RadGridQY" runat="server" PageSize="12"
                            GridLines="None" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                            SortingSettings-SortToolTip="单击进行排序" Width="100%" PagerStyle-AlwaysVisible="true">
                            <ClientSettings EnableRowHoverStyle="true">
                            </ClientSettings>
                            <HeaderContextMenu EnableEmbeddedSkins="False">
                            </HeaderContextMenu>
                            <MasterTableView NoMasterRecordsText=" 没有可显示的记录" CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Left"
                                DataKeyNames="PSN_ServerID">
                                <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
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
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="PSN_Name" DataField="PSN_Name" HeaderText="姓名">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="ENT_City" DataField="ENT_City" HeaderText="区县">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="ENT_Name" DataField="ENT_Name" HeaderText="企业名称">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="PSN_RegisterNo" DataField="PSN_RegisterNo" HeaderText="注册号">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn UniqueName="PSN_RegisterCertificateNo" DataField="PSN_RegisterCertificateNo" HeaderText="证书编号">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="PSN_RegisteProfession" DataField="PSN_RegisteProfession" HeaderText="注册专业">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="申报事项" UniqueName="RegisteType">
                                        <ItemTemplate>
                                            <%#  ShowRegType(Eval("PSN_RegisteType").ToString()) %>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn UniqueName="PSN_RegistePermissionDate" DataField="PSN_RegistePermissionDate" HeaderText="审核日期" HtmlEncode="false" DataFormatString="{0:yyyy.MM.dd}">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>

                                    <telerik:GridTemplateColumn HeaderText="打印状态" UniqueName="TemplateColumn">
                                        <ItemTemplate>
                                            <%#Eval("PrintTime")== DBNull.Value || Convert.ToDateTime(Eval("PSN_RegistePermissionDate"))> Convert.ToDateTime(Eval("PrintTime"))?"未打印":"已打印"
                                            %>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridTemplateColumn UniqueName="PrintCert" HeaderText="">
                                        <ItemTemplate>
                                            <%-- 初始注册，变更注册，延期注册，重新注册，遗失补办--%>
                                            <%# "01，02，03，05，06".Contains(Eval("PSN_RegisteType").ToString()) == true?string.Format(" <span class=\"link_edit\"  onclick='javascript:SetIfrmSrc(\"CertPrint.aspx?o={0}\")'>打印证书</span>",Eval("PSN_ServerID")):""%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Left" Wrap="false" ForeColor="Blue" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn UniqueName="PrintCodeBar" HeaderText="">
                                        <ItemTemplate>
                                            <%-- 变更注册，延期注册，增项--%>
                                            <%# "02，03，04".Contains(Eval("PSN_RegisteType").ToString()) == true?string.Format(" <span class=\"link_edit\"  onclick='javascript:SetIfrmSrc(\"CertCodeBarPrint.aspx?o={0}\")'>打印防伪条</span>",Eval("PSN_ServerID")):""%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Left" Wrap="false" ForeColor="Blue" />
                                    </telerik:GridTemplateColumn>
                                </Columns>

                                <PagerStyle AlwaysVisible="True"></PagerStyle>

                                <HeaderStyle Font-Bold="True" Wrap="false" />

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
                        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="Model.COC_TOW_Person_BaseInfoMDL"
                            SelectMethod="GetList" TypeName="DataAccess.COC_TOW_Person_BaseInfoDAL"
                            SelectCountMethod="SelectCount" EnablePaging="true"
                            MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex" SortParameterName="orderBy">
                            <SelectParameters>
                                <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                                    DefaultValue="" ConvertEmptyStringToNull="false" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </div>
                    <div style="width: 100%; margin: 10px auto; text-align: center;">
                        请先勾选要打印的记录
                        <asp:Button ID="ButtonPrint" runat="server" Text="批量打印" OnClick="ButtonPrint_Click" CssClass="bt_large" />
                    </div>
                </div>
            </div>
        </div>
        <uc2:IframeView ID="IframeView" runat="server" />
    </form>
    <script type="text/javascript">
        //grid行选择
        function checkBoxClick(checked) {
            if (checked == true) return;
            var ckall = document.getElementById(CheckBoxAllClientID);
            if (ckall == null) return;
            if (ckall.checked == true) ckall.checked = false;
        }

        //grid全选
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
