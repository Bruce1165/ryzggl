<%@ Page Title="" Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="CertificateMergeList.aspx.cs" Inherits="ZYRYJG.CertifManage.CertificateMergeList" %>

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
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <%--<telerik:AjaxSetting AjaxControlID="RadGrid1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>--%>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default" />
    <div class="div_out">
        <div class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt;从业人员证书管理&gt;&gt;<strong>C1、C2证书合并审核</strong>
            </div>
        </div>
        <div class="DivTitleOn" onclick="DivOnOff(this,'Td3',event);" title="折叠">
            业务说明
        </div>
        <div class="DivContent" id="Td3">
            1、办事流程：个人发起合并申请 > 企业确认 > 市建委审核 > 生成新的电子证书 > 个人下载电子证书。<br />
            2、存在B、C1、C2不在同一单位情况，不允许发起合并申请，必须先变更到同一家单位。<br />
            3、C1、C2证书存在未办结的变更或续期时，不允许发起合并申请，必须办结在途业务或取消在途申请。<br />
            4、合并申请审批通过后，生成新证书C3，有效截止日期取原C1、C2证书中最大有效截止日期。原C1、C2证书自动注销。<br />
            5、合并申请审批通过后1或2个工作日后，个人下载新版电子证书。<br />
        </div>
        <table class="bar_cx" runat="server" id="tbCX">
            <tr>
                <td width="11%" align="right" nowrap="nowrap">姓 名：
                </td>
                <td align="left" width="38%">
                    <telerik:RadTextBox ID="RadTxtWorkerName" runat="server" Width="97%" Skin="Default">
                    </telerik:RadTextBox>
                </td>
                <td width="11%" align="right" nowrap="nowrap">身份证号：
                </td>
                <td align="left" width="38%">
                    <telerik:RadTextBox ID="RadTxtSFZHM" runat="server" Width="97%" Skin="Default">
                    </telerik:RadTextBox>
                </td>
            </tr>
            <tr>
                <td width="11%" align="right" nowrap="nowrap">证书编号：
                </td>
                <td align="left" width="38%">
                    <telerik:RadTextBox ID="RadTextBoxCertificateCode" runat="server" Width="97%" Skin="Default">
                    </telerik:RadTextBox>
                </td>
                <td width="11%" align="right" nowrap="nowrap">审核状态：
                </td>
                <td align="left" width="38%">
                     <telerik:radcombobox ID="RadComboBoxApplyStatus" runat="server" Width="97%" Skin="Default">
                                        <Items>
                                            <telerik:RadComboBoxItem Value="" Selected="True" Text="全部" />
                                            <telerik:RadComboBoxItem Value="待单位确认" Text="待单位确认" />
                                            <telerik:RadComboBoxItem Value="已申请" Text="已申请" />
                                            <telerik:RadComboBoxItem Value="已决定" Text="已决定" />
                                            
                                        </Items>
                                    </telerik:radcombobox>
                </td>
            </tr>
            <tr>
                <td colspan="4" align="center">
                    <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
                </td>
            </tr>
        </table>
        <div style="width: 98%; margin: 0 auto;">
            <telerik:RadGrid ID="RadGrid1" AllowPaging="True"
                SortingSettings-SortToolTip="单击进行排序" runat="server" AutoGenerateColumns="False" PagerStyle-AlwaysVisible="true"
                AllowSorting="True" GridLines="None" CellPadding="0" Width="100%" Skin="Blue" OnPageIndexChanged="RadGrid1_PageIndexChanged" OnDataBound="RadGrid1_DataBound"
                EnableAjaxSkinRendering="False" EnableEmbeddedSkins="False">
                <ClientSettings EnableRowHoverStyle="true">
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
                <MasterTableView DataKeyNames="ApplyID" NoMasterRecordsText="没有可显示的记录">
                    <Columns>
                         <telerik:GridTemplateColumn UniqueName="TemplateColumn" HeaderText="">
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" onclick='checkBoxClick(this.checked);' />
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <uc3:CheckAll ID="CheckAll1" runat="server" />
                                </HeaderTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderText="序号" UniqueName="RowNum" DataField="RowNum" AllowSorting="false">
                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="申请日期" UniqueName="ApplyDate" DataField="ApplyDate"
                            DataFormatString="{0:yyyy-MM-dd}">
                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="WorkerName" DataField="WorkerName" HeaderText="姓名">
                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="WorkerCertificateCode" DataField="WorkerCertificateCode"
                            HeaderText="身份证号">
                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="UnitName" DataField="UnitName" HeaderText="单位名称">
                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                       <%-- <telerik:GridBoundColumn UniqueName="CertificateCode1" DataField="CertificateCode1" HeaderText="C1证书编号">
                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>--%>
                        <telerik:GridTemplateColumn UniqueName="CertificateCode1" HeaderText="C1证书编号">
                                <ItemTemplate>
                                    <span class="link_edit" onclick='javascript:SetIfrmSrc("../PersonnelFile/CertificateInfo.aspx?o=<%# Utility.Cryptography.Encrypt(Eval("CertificateID1").ToString()) %>");'>
                                        <nobr><%# Eval("CertificateCode1")%></nobr>
                                    </span>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" Font-Bold="true" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                            </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderText="C1证书有效期" UniqueName="ValidEndDate1" DataField="ValidEndDate1"
                            DataFormatString="{0:yyyy-MM-dd}">
                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                       <%-- <telerik:GridBoundColumn UniqueName="CertificateCode2" DataField="CertificateCode2" HeaderText="C2证书编号">
                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>--%>
                         <telerik:GridTemplateColumn UniqueName="CertificateCode2" HeaderText="C2证书编号">
                                <ItemTemplate>
                                    <span class="link_edit" onclick='javascript:SetIfrmSrc("../PersonnelFile/CertificateInfo.aspx?o=<%# Utility.Cryptography.Encrypt(Eval("CertificateID2").ToString()) %>");'>
                                        <nobr><%# Eval("CertificateCode2")%></nobr>
                                    </span>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" Font-Bold="true" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                            </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderText="C2证书有效期" UniqueName="ValidEndDate2" DataField="ValidEndDate2"
                            DataFormatString="{0:yyyy-MM-dd}">
                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="ApplyStatus" DataField="ApplyStatus" HeaderText="审批状态">
                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                            <ItemStyle HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>


                        <telerik:GridTemplateColumn UniqueName="Detail" HeaderText="详细">
                            <ItemTemplate>
                                <%# string.Format("<span class=\"link_edit\" onclick='javascript:SetIfrmSrc(\"CertificateMergeApply.aspx?o={0}\")'><nobr style='color:blue;text-decoration: underline;'>详细</nobr></span>",Utility.Cryptography.Encrypt(Eval("ApplyID").ToString()))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                        </telerik:GridTemplateColumn>

                    </Columns>
                    <HeaderStyle Font-Bold="True" />
                    <PagerTemplate>
                        <uc2:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                    </PagerTemplate>
                </MasterTableView>
                <SortingSettings SortToolTip="单击进行排序"></SortingSettings>
                <StatusBarSettings LoadingText="正在读取数据" ReadyText="完成" />
            </telerik:RadGrid>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="DataAccess.CertificateMergeDAL"
                DataObjectTypeName="Model.CertificateMergeMDL" SelectMethod="GetList" EnablePaging="true"
                SelectCountMethod="SelectCount" MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex"
                SortParameterName="orderBy">
                <SelectParameters>
                    <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                        DefaultValue="and ApplyStatus='已申请'" ConvertEmptyStringToNull="false" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </div>
         <br />
           <div id="anniudiv" runat="server">
                    <asp:Button ID="ButtonCheck" runat="server" Text="批量审核" CssClass="bt_large" OnClick="ButtonCheck_Click" />
       
    </div>
    <uc4:IframeView ID="IframeView" runat="server" />
</asp:Content>
