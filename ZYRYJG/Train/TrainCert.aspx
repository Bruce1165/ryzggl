<%@ Page Title="" Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="TrainCert.aspx.cs" Inherits="ZYRYJG.Train.TrainCert" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../PostSelect.ascx" TagName="PostSelect" TagPrefix="uc1" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManager ID="RadAjaxManager2" runat="server" EnableAJAX="true">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadGridApply">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGridApply" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ButtonSearch">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGridApply" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="PostSelect1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="PostSelect1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ButtonExportToExcel">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ButtonExportToExcel" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Windows7" />
    <telerik:RadWindowManager runat="server" RestrictionZoneID="offsetElement" ID="RadWindowManager1"
        EnableShadow="true" EnableEmbeddedScripts="true">
    </telerik:RadWindowManager>
    <style type="text/css">
        .tl {
            line-height: 20px;
            padding-right: 4px;
            vertical-align: middle;
            width: 16px;
            height: 16px;
        }
    </style>
    <div class="div_out">
        <div class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt; 培训点业务 &gt;&gt;<strong>证书查看</strong>
            </div>
        </div>
        <div class="content">
            <div class="DivContent" id="Td3">                  
                    业务说明：1、这里只能查看参加本培训点培训考试取得的新版建设职业技能岗位证书。<br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2、证书没有续期要求，“当前有效证书”均为在有效期内的证书。<br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3、个人电子证书仅可由持证人本人下载使用。
                </div>
            <div  runat="server" id="divBaseInfo" style="text-align: left; font-size: 12px; line-height: 100%; padding: 12px 12px; color: #000;font-size:14px;">
            </div>
            <table class="bar_cx">
                  <tr>
                       <td width="12%"  align="right" nowrap="nowrap" >岗位工种：
                    </td>
                    <td width="38%" align="left" >
                        <uc1:PostSelect ID="PostSelect1" runat="server" />
                    </td>
                   
                    <td width="12%" align="right" nowrap="nowrap">证书编号：
                    </td>
                    <td width="38%" align="left">
                        <telerik:RadTextBox ID="RadTextBoxCertificateCode" runat="server" Width="95%" Skin="Default">
                        </telerik:RadTextBox>
                    </td>
                   </tr>
                <tr>
               
                    <td align="right" nowrap="nowrap">姓名：
                    </td>
                    <td align="left" >
                        <telerik:RadTextBox ID="RadTextBoxWorkerName" runat="server" Width="200px" Skin="Default">
                        </telerik:RadTextBox>
                        证件号码： 
                        <telerik:RadTextBox ID="RadTextBoxWorkerCertificateCode" runat="server" Width="200px" Skin="Default">
                        </telerik:RadTextBox>
                    </td>
                    <td align="right" nowrap="nowrap"  >
                        发证时间
                    </td>
                    <td align="left" >
                        <telerik:RadComboBox ID="RadComboBoxYear" runat="server" Skin="Office2007" CausesValidation="False"
                                        Width="60px" ExpandAnimation-Duration="0">
                                    </telerik:RadComboBox>
                                    &nbsp;年&nbsp;
                                        <telerik:RadComboBox ID="RadComboBoxMonth" runat="server" Skin="Office2007" CausesValidation="False"
                                            Width="60px" ExpandAnimation-Duration="0">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="全部" Value="" />
                                                <telerik:RadComboBoxItem Text="1" Value="1" />
                                                <telerik:RadComboBoxItem Text="2" Value="2" />
                                                <telerik:RadComboBoxItem Text="3" Value="3" />
                                                <telerik:RadComboBoxItem Text="4" Value="4" />
                                                <telerik:RadComboBoxItem Text="5" Value="5" />
                                                <telerik:RadComboBoxItem Text="6" Value="6" />
                                                <telerik:RadComboBoxItem Text="7" Value="7" />
                                                <telerik:RadComboBoxItem Text="8" Value="8" />
                                                <telerik:RadComboBoxItem Text="9" Value="9" />
                                                <telerik:RadComboBoxItem Text="10" Value="10" />
                                                <telerik:RadComboBoxItem Text="11" Value="11" />
                                                <telerik:RadComboBoxItem Text="12" Value="12" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    &nbsp;月
                    </td>

                </tr>
                <tr>
                  
                    <td colspan="4" align="center">
                        <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />&nbsp;&nbsp;
                         <asp:Button ID="ButtonExportToExcel" CssClass="bt_large" Text="导出查询结果" OnClick="ButtonExportToExcel_Click"
                    runat="server"></asp:Button>
                    </td>
                </tr>
            </table>
            <div class="table_cx">
                <img alt="" src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                证书列表【图例：<img class="tl" alt="" src="../Images/yx.png" />有效证书，<img class="tl" alt="" src="../Images/wx.png" />无效证书（过期、离京、注销）不能办理业务】
            </div>
            <div style="width: 98%; margin: 0 auto;">
                <telerik:RadGrid ID="RadGridApply" runat="server"
                    GridLines="None" AllowPaging="True" PageSize="10" AllowSorting="True" AutoGenerateColumns="False"
                    SortingSettings-SortToolTip="单击进行排序" Width="100%" Skin="Blue" EnableAjaxSkinRendering="false"
                    EnableEmbeddedSkins="false">
                    <MasterTableView NoMasterRecordsText=" 没有可显示的记录"
                        DataKeyNames="CertificateID">
                        <Columns>
                            <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="WorkerName" DataField="WorkerName" HeaderText="姓名">
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="WorkerCertificateCode" DataField="WorkerCertificateCode"
                                HeaderText="证件号码">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="PostName" DataField="PostName" HeaderText="岗位工种">
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="CertificateCode" HeaderText="证书编号">
                                <ItemTemplate>
                                   
                                    <span class="link_edit" onclick='javascript:SetIfrmSrc("../PersonnelFile/CertificateInfo.aspx?o=<%# Utility.Cryptography.Encrypt(Eval("CertificateID").ToString()) %>");'>
                                        <nobr><%# Eval("CertificateCode").ToString()%></nobr>
                                    </span>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" Font-Bold="true" />
                                <ItemStyle HorizontalAlign="Left" Wrap="false" ForeColor="Blue" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn UniqueName="ConferDate" DataField="ConferDate" HeaderText="发证时间"
                                HtmlEncode="false" DataFormatString="{0:yyyy.MM.dd}">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>                          
                             <telerik:GridTemplateColumn UniqueName="ValidEndDate" HeaderText="有效期至">
                                <ItemTemplate>
                                    <%# Convert.ToDateTime(Eval("ValidEndDate")).ToString("yyyy.MM.dd")=="2050.01.01"?"当前有效证书":Convert.ToDateTime(Eval("ValidEndDate")).ToString("yyyy.MM.dd")%> 
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridTemplateColumn>                          
                            <telerik:GridTemplateColumn UniqueName="Status" HeaderText="证书状态">
                                <ItemTemplate>
                                    <img alt="" class="tl" src='<%# (Convert.ToDateTime(Eval("ValidEndDate")) < DateTime.Now || Eval("Status").ToString()=="注销" || Eval("Status").ToString()=="离京变更" )?"../Images/wx.png":"../Images/yx.png"%>' />
                                    <%# (Convert.ToDateTime(Eval("ValidEndDate")) < DateTime.Now || Eval("Status").ToString()=="注销" || Eval("Status").ToString()=="离京变更" )?"无效":"有效"%>
                                    <%# Convert.ToDateTime(Eval("ValidEndDate")) < DateTime.Now?"(过期)": ""%>
                                    <%# Eval("Status").ToString()=="注销" ?"(注销)":""%>
                                    <%# Eval("Status").ToString()=="离京变更"?"(离京)":""%>
                                     <%# Eval("Remark") != null && Eval("Remark").ToString().Contains("超龄")==true?"(超龄)":""%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" Wrap="false" />
                            </telerik:GridTemplateColumn>
                           
                        </Columns>
                        <HeaderStyle Font-Bold="True" />
                        <EditFormSettings>
                            <EditColumn UniqueName="EditCommandColumn1">
                            </EditColumn>
                        </EditFormSettings>
                        <PagerTemplate>
                            <uc2:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                        </PagerTemplate>
                    </MasterTableView>
                    <SortingSettings SortToolTip="单击进行排序"></SortingSettings>
                    <StatusBarSettings LoadingText="正在读取数据" ReadyText="完成" />
                </telerik:RadGrid>
            </div>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="Model.CertificateOB"
                DeleteMethod="Delete" InsertMethod="Insert" SelectMethod="GetList" TypeName="DataAccess.CertificateDAL"
                UpdateMethod="Update" SelectCountMethod="SelectCount" EnablePaging="true" MaximumRowsParameterName="maximumRows"
                StartRowIndexParameterName="startRowIndex" SortParameterName="orderBy">
                <SelectParameters>
                    <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                        DefaultValue="" ConvertEmptyStringToNull="false" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <br />
        </div>
    </div>
    <uc4:IframeView ID="IframeView" runat="server" />
</asp:Content>
