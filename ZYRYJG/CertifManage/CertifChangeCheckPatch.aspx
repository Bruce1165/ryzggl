<%@ Page Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="CertifChangeCheckPatch.aspx.cs" Inherits="ZYRYJG.CertifManage.CertifChangeCheckPatch" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<%@ Register Src="../PostSelect.ascx" TagName="PostSelect" TagPrefix="uc1" %>
<%@ Register Src="../CheckAll.ascx" TagName="CheckAll" TagPrefix="uc3" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    </telerik:RadCodeBlock>
    <telerik:RadWindowManager runat="server" RestrictionZoneID="offsetElement" ID="RadWindowManager1"
        EnableShadow="true" EnableEmbeddedScripts="true" Skin="Windows7" VisibleStatusbar="false">
    </telerik:RadWindowManager>
    <script type="text/javascript">
        function onRequestStart(sender, args) {

            if (args.get_eventTarget().indexOf("ButtonExportExcel") >= 0) {
                args.set_enableAjax(false);

            }
        }
    </script>

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" EnableAJAX="true">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadGrid1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default" />
    <div class="div_out">
        <div class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt; 证书管理 &gt;&gt;
                <asp:Label ID="LabelPostType" runat="server" Text="三类人员"></asp:Label>
                &gt;&gt; <strong>批量审查决定（社保）</strong>
            </div>
        </div>
        <div class="table_border" style="width: 98%; margin: 5px auto;">
            <div class="content">
                <div class="DivTitleOn" onclick="DivOnOff(this,'Td3',event);" title="折叠">
                    审批说明
                </div>
                <div class="DivContent" id="Td3">
                    1、本页面功能用于审批（只变更单位名称或组织机构代码，无需打证，不用到现场提交纸质材料）的京内变更申请。<br />
                    2、本页面只显示变更单位（新单位）与社保信息一致的记录。社保不一致或需要重新制证的申请（如遗失补办）需要到现场办理。<br />
                    3、审批人员定期（建议每日）登陆该页面，核查申请是否有误，无误进行批量审核操作。<br />
                </div>
                <div class="jbxxbt">
                    批量审查决定（社保）
                </div>
                <table class="bar_cx" runat="server" id="DivSearch">
                    <tr>

                        <td align="right" width="11%" nowrap="nowrap">岗位工种：
                        </td>
                        <td align="left" width="39%">
                            <uc1:PostSelect ID="PostSelect1" runat="server" />
                        </td>
                        <td width="11%" align="right" nowrap="nowrap">证书编号：
                        </td>
                        <td width="39%" align="left">
                            <telerik:RadTextBox ID="rdtxtCertificateCode" runat="server" Width="97%" Skin="Default">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap="nowrap">申请人：
                        </td>
                        <td align="left">
                            <telerik:RadTextBox ID="RadTextBoxApplyMan" runat="server" Width="97%" Skin="Default">
                            </telerik:RadTextBox>
                        </td>
                        <td align="right" nowrap="nowrap">申请批号：
                        </td>
                        <td align="left">
                            <telerik:RadTextBox ID="rdtxtApplyCode" runat="server" Width="97%" Skin="Default">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap="nowrap" width="11%">姓 名：
                        </td>
                        <td align="left" width="39%">
                            <telerik:RadTextBox ID="rdtxtWorkerName" runat="server" Width="97%" Skin="Default">
                            </telerik:RadTextBox>
                        </td>
                        <td align="right" nowrap="nowrap" width="11%">证件号码：
                        </td>
                        <td align="left" width="39%">
                            <telerik:RadTextBox ID="rdtxtZJHM" runat="server" Width="97%" Skin="Default">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap="nowrap">原单位名称：
                        </td>
                        <td align="left">
                            <telerik:RadTextBox ID="rdtxtQYMC" runat="server" Width="97%" Skin="Default">
                            </telerik:RadTextBox>
                        </td>
                        <td align="right" nowrap="nowrap">新单位名称：
                        </td>
                        <td align="left">
                            <telerik:RadTextBox ID="RadTextBoxNewUnit" runat="server" Width="97%" Skin="Default">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <asp:Button ID="btnSearch" runat="server" Text="查 询" CssClass="button" OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                </table>
                <div class="table_cx">
                    <img alt="" src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                    证书管理决定列表
                </div>
                <div style="width: 98%; margin: 0 auto; overflow: auto;">
                    <telerik:RadGrid ID="RadGrid1" AllowPaging="True"
                        SortingSettings-SortToolTip="单击进行排序" runat="server" AutoGenerateColumns="False"  PagerStyle-AlwaysVisible="true"
                        AllowSorting="True" PageSize="10" GridLines="None" CellPadding="0" Width="100%"
                        Skin="Blue" EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false"
                        OnPageIndexChanged="RadGridAccept_PageIndexChanged" OnDataBound="RadGridAccept_DataBound">
                        <ClientSettings EnableRowHoverStyle="true">
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                        <MasterTableView DataKeyNames="CertificateChangeID,PostID,ApplyCode,ChangeType" NoMasterRecordsText="没有可显示的记录">
                            <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                            <Columns>
                                <telerik:GridTemplateColumn UniqueName="SelectAllColumn" HeaderText="Highlight <br/> ship name">
                                    <HeaderTemplate>
                                        <uc3:CheckAll ID="CheckAll1" runat="server" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" onclick='checkBoxClick(this.checked);' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn HeaderText="序号" UniqueName="RowNum" DataField="RowNum" AllowSorting="false">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="申请日期" UniqueName="ApplyDate" DataField="ApplyDate" HtmlEncode="false" DataFormatString="{0:yyyy.MM.dd}">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="岗位工种" UniqueName="PostName" DataField="PostName">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>

                                <telerik:GridTemplateColumn UniqueName="CertificateChangeID" HeaderText="申请单">
                                    <ItemTemplate>
                                        <span class="link_edit" onclick='javascript:SetIfrmSrc("Application.aspx?o=<%# Utility.Cryptography.Encrypt(Eval("CertificateChangeID").ToString()) %>");'>
                                            <nobr>申请单</nobr>
                                        </span>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn UniqueName="CertificateCode" HeaderText="证书编号" SortExpression="CertificateCode">
                                    <ItemTemplate>
                                        <span class="link_edit" onclick='javascript:SetIfrmSrc("../PersonnelFile/CertificateInfo.aspx?o=<%# Utility.Cryptography.Encrypt(Eval("CertificateID").ToString()) %>");'>
                                            <nobr><%# Eval("CertificateCode").ToString()%></nobr>
                                        </span>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" Font-Bold="true" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn UniqueName="Output_CertificateCode" DataField="CertificateCode"
                                    HeaderText="证书编号" Visible="false">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="NewWorkerName" DataField="NewWorkerName" HeaderText="姓　名">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="NewWorkerCertificateCode" DataField="NewWorkerCertificateCode"
                                    HeaderText="证件号码">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="UnitName" DataField="UnitName" HeaderText="原单位名称">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="NewUnitName" DataField="NewUnitName" HeaderText="新单位名称">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>

                                <telerik:GridTemplateColumn UniqueName="sb" HeaderText="社保">
                                    <ItemTemplate>
                                        <%# string.Format("<span class=\"link_edit\" onclick='javascript:SetIfrmSrc(\"../PersonnelFile/RYSBView.aspx?o1={0}&o2={1}&o3={2}\");'><nobr>{3}</nobr></span>", Eval("NewWorkerCertificateCode").ToString(), Eval("NewUnitCode").ToString(), Convert.ToDateTime(Eval("ApplyDate")).ToString(),(Eval("SheBaoCheck")!=DBNull.Value && Eval("SheBaoCheck").ToString()=="1")?"符合":"" )%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" Font-Bold="true" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn UniqueName="ApplyMan" DataField="ApplyMan" HeaderText="申请人">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="PrintVer" DataField="PrintVer" HeaderText="版本">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>

                            </Columns>
                            <HeaderStyle Font-Bold="True" />
                            <PagerTemplate>
                                <uc2:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                            </PagerTemplate>
                        </MasterTableView>
                        <SortingSettings SortToolTip="单击进行排序"></SortingSettings>
                        <StatusBarSettings LoadingText="正在读取数据" ReadyText="完成" />
                    </telerik:RadGrid>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="DataAccess.CertificateChangeDAL"
                        DataObjectTypeName="Model.CertificateChangeOB" SelectMethod="GetList" EnablePaging="true"
                        SelectCountMethod="SelectCount" MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex"
                        SortParameterName="orderBy">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                                DefaultValue="and Status='已申请'" ConvertEmptyStringToNull="false" />
                        </SelectParameters>

                    </asp:ObjectDataSource>
                </div>
                <br />
                <div id="DivDealWay" runat="server" style="width: 100%">
                    <asp:Button ID="ButtonConfirm" runat="server" Text="审查通过" CssClass="bt_large" OnClick="ButtonConfirm_Click" />
                </div>
                <br />
            </div>
            <uc4:IframeView ID="IframeView" runat="server" />
        </div>
</asp:Content>
