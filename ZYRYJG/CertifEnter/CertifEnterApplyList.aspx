<%@ Page Title="" Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="CertifEnterApplyList.aspx.cs" Inherits="ZYRYJG.CertifEnter.CertifEnterApplyList" %>

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
                当前位置 &gt;&gt;
                <asp:Label ID="LabelRoad" runat="server" Text="证书进京"></asp:Label>
                &gt;&gt;
                <asp:Label ID="LabelPostType" runat="server" Text="三类人员"></asp:Label>
                &gt;&gt; <strong>进京申请</strong>
            </div>
        </div>
         <div class="step">
                    <div class="stepLabel">办理流程：</div>
                    <div id="step_填报中" runat="server" class="stepItem lgray">个人填报></div>
                    <div id="step_待单位确认" runat="server" class="stepItem lgray">单位确认></div>
                    <div id="step_已申请" runat="server" class="stepItem lgray">提交审核></div>
                    <div id="step_已受理" runat="server" class="stepItem lgray">市级受理></div>
                    <div id="step_已审核" runat="server" class="stepItem lgray">市级审核></div>
                    <div id="step_已编号" runat="server" class="stepItem lgray">证书编号></div>
                    <div id="step_证书已审核" runat="server" class="stepItem lgray">证书已审核（办结，下载电子证书）</div>
                    <div class="stepArrow">▶</div>
                </div>
        <div class="content">
            <table class="bar_cx">
                <tr>
                    <td width="11%" align="right" nowrap="nowrap">证书编号：
                    </td>
                    <td align="left" width="200px">
                        <telerik:RadTextBox ID="RadTextBoxCertificateCode" runat="server" Width="97%" Skin="Default" >
                        </telerik:RadTextBox>
                    </td>
                    <td width="100px" align="right" nowrap="nowrap">申请编号：
                    </td>
                    <td align="left" width="200px">
                        <telerik:RadTextBox ID="RadTextBoxApplyCode" runat="server" Width="97%" Skin="Default" >
                        </telerik:RadTextBox>
                    </td>
                    <td align="center" width="350px" id="td_QYQuergyParm" runat="server" visible="false">
                        <asp:RadioButtonList ID="RadioButtonListQYCheckStatus" runat="server" RepeatDirection="Horizontal" Width="300px" TextAlign="Right">
                            <asp:ListItem Text="待单位确认" Value="待单位确认" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="单位已确认" Value="单位已确认"></asp:ListItem>
                            <asp:ListItem Text="全部" Value="全部"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td align="left" style="padding-left:20px">
                        <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
                    </td>
                </tr>
            </table>
            <div class="table_cx" style="margin-top: 12px" >
                <img alt="" src="../Images/jglb.png" width="15" height="15" />
                进京申请列表
            </div>
            <div id="div_New" runat="server" visible="false"  style="margin: 12px 12px;padding-right: 40px; float: left; clear: right; padding-left: 20px;">
                <span class="link_edit" onclick="javascript:SetIfrmSrc('CertifEnterApplyEdit.aspx?t=<%=ViewState["PostTypeID"]==null?"":Utility.Cryptography.Encrypt(ViewState["PostTypeID"].ToString()) %>')"
                    style="color: #DC2804; font-weight: bold;">
                    <img alt="" src="../Images/jia.gif" style="width: 14px; height: 15px; margin-bottom: -2px; padding-right: 5px; border: none;" />发起申请</span>
              
            </div>
            <div style="width: 98%; margin: 0 auto;">
                <telerik:RadGrid ID="RadGrid1" AllowPaging="True"
                    SortingSettings-SortToolTip="单击进行排序" runat="server" AutoGenerateColumns="False"
                    AllowSorting="True" GridLines="None" CellPadding="0" Width="100%" Skin="Blue"
                    EnableAjaxSkinRendering="False" EnableEmbeddedSkins="False"  PagerStyle-AlwaysVisible="true"
                    OnItemDataBound="RadGrid1_ItemDataBound">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ApplyID,AddPostID" NoMasterRecordsText="没有可显示的记录">
                        <Columns>
                            <telerik:GridBoundColumn HeaderText="序号" UniqueName="RowNum" DataField="RowNum" AllowSorting="false">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="申请日期" UniqueName="ApplyDate" DataField="ApplyDate"
                                DataFormatString="{0:yyyy-MM-dd}">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="申请编号" UniqueName="ApplyCode" DataField="ApplyCode">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="岗位工种" UniqueName="PostName" DataField="PostName">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="CertificateCode" DataField="certificateCode"
                                HeaderText="证书编号">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="WorkerName" DataField="WorkerName" HeaderText="姓名">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="UnitName" DataField="UnitName" HeaderText="现聘用单位名称">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="ApplyStatus" DataField="ApplyStatus" HeaderText="状态">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="Detail" HeaderText="详细">
                                <ItemTemplate>
                                    <%# string.Format("<span class=\"link_edit\" onclick='javascript:SetIfrmSrc(\"CertifEnterApplyEdit.aspx?t={0}&o={1}\")'><nobr style='color:blue;text-decoration: underline;'>详细</nobr></span>",Page.Server.UrlEncode(Utility.Cryptography.Encrypt(Eval("PostTypeID").ToString())),Page.Server.UrlEncode(Utility.Cryptography.Encrypt(Eval("ApplyID").ToString())))%>
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
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="DataAccess.CertificateEnterApplyDAL"
                    DataObjectTypeName="Model.CertificateChangeOB" SelectMethod="GetListView" EnablePaging="true"
                    SelectCountMethod="SelectViewCount" MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex"
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
