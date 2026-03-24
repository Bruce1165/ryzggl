<%@ Page Title="" Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="CertifMoreApplyList.aspx.cs" Inherits="ZYRYJG.CertifManage.CertifMoreApplyList" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<%@ Register Src="../PostSelect.ascx" TagName="PostSelect" TagPrefix="uc1" %>
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
                当前位置 &gt;&gt;证书管理&gt;&gt;三类人员&gt;&gt; <strong>法人增发A证申请</strong>
            </div>
        </div>
        <div class="content">
            <div class="DivTitleOn" onclick="DivOnOff(this,'Td3',event);" title="折叠">
                业务说明
            </div>
            <div class="DivContent" id="Td3">
                1、办事流程:个人网上申请并上传扫描件-企业网上审核确认-住建委网上审批（咨询电话:010-89150138）。<br />
                2、身份证样式：须为18位二代身份证（带X字母的必须使用英文大写）。<br />
                3、组织机构代码：9位数字或大写字母组合,带“-”的去掉“-”，社会统一信用代码中的第9位至第17位为企业的组织机构代码。<br />
                4、不再提供纸质证书补办业务，请企业或个人自行下载电子证书。<br />
                5、A本增发需要增发企业网上审核确认。<br />
                6、同时担任本市两家（或两家以上）建筑施工企业法人，已取得其中一家建筑施工企业A本的,可申请A本增发。<br />
                7、增发A本的发证日期与有效期同原A本保持一致。
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
                    <td colspan="4" align="center">
                        <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
                    </td>
                </tr>
            </table>

            <div id="div_New" runat="server" visible="false" style="margin: 12px 12px; padding-right: 40px; float: left; clear: right; padding-left: 20px;">
                <span class="link_edit" onclick="javascript:SetIfrmSrc('CertifMoreApply.aspx?t=<%=ViewState["PostTypeID"]==null?"":Utility.Cryptography.Encrypt(ViewState["PostTypeID"].ToString()) %>')"
                    style="color: #DC2804; font-weight: bold;">
                    <img alt="" src="../Images/jia.gif" style="width: 14px; height: 15px; margin-bottom: -2px; padding-right: 5px; border: none;" />发起申请</span>
            </div>
            <div style="width: 98%; margin: 0 auto;">
                <telerik:RadGrid ID="RadGrid1" AllowPaging="True"
                    SortingSettings-SortToolTip="单击进行排序" runat="server" AutoGenerateColumns="False" PagerStyle-AlwaysVisible="true"
                    AllowSorting="True" GridLines="None" CellPadding="0" Width="100%" Skin="Blue"
                    EnableAjaxSkinRendering="False" EnableEmbeddedSkins="False">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ApplyID" NoMasterRecordsText="没有可显示的记录">
                        <Columns>
                            <telerik:GridBoundColumn HeaderText="序号" UniqueName="RowNum" DataField="RowNum" AllowSorting="false">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="申请日期" UniqueName="CreateTime" DataField="CreateTime"
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
                            <telerik:GridBoundColumn UniqueName="UnitName" DataField="UnitName" HeaderText="现有A本单位名称">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn UniqueName="UnitNameMore" DataField="UnitNameMore" HeaderText="申请增发A本单位名称">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="ApplyStatus" DataField="ApplyStatus" HeaderText="状态">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="CertificateCodeMore" DataField="CertificateCodeMore"
                                HeaderText="增发A本证书编号">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>

                            <telerik:GridTemplateColumn UniqueName="Detail" HeaderText="详细">
                                <ItemTemplate>
                                    <%# string.Format("<span class=\"link_edit\" onclick='javascript:SetIfrmSrc(\"CertifMoreApply.aspx?o={0}\")'><nobr style='color:blue;text-decoration: underline;'>详细</nobr></span>",Utility.Cryptography.Encrypt(Eval("ApplyID").ToString()))%>
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
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="DataAccess.CertificateMoreDAL"
                    DataObjectTypeName="Model.CertificateMoreMDL" SelectMethod="GetList" EnablePaging="true"
                    SelectCountMethod="SelectCount" MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex"
                    SortParameterName="orderBy">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                            DefaultValue="and ApplyStatus='已申请'" ConvertEmptyStringToNull="false" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </div>
            <br />
        </div>
    </div>
    <uc4:IframeView ID="IframeView" runat="server" />
</asp:Content>
