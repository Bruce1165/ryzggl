<%@ Page Title="" Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="CompanyWorkCertInfoList.aspx.cs" Inherits="ZYRYJG.PersonnelFile.CompanyWorkCertInfoList" %>

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
                当前位置 &gt;&gt; 业务办理 &gt;&gt;
                企业业务办理&gt;&gt; <strong>企业人员证书列表</strong>
            </div>
        </div>
        <div class="content">
            <div class="DivContent" id="Td3">                  
                    业务说明：1、根据有关规定，停止<span style="color:red">造价员、专业管理人员</span>考核、变更和续期、电子证书打印工作。<br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2、专业管理人员证书和技能人员证书没有续期要求，“当前有效证书”均为在有效期内的证书。<br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3、个人电子证书仅可由持证人本人下载使用。
                </div>
            <div  runat="server" id="divBaseInfo" style="text-align: left; font-size: 12px; line-height: 100%; padding: 12px 12px; color: #000;font-size:14px;">
            </div>
            <table class="bar_cx">
                  <tr>
                    <td width="7%" align="right" nowrap="nowrap">证书编号：
                    </td>
                    <td width="18%" align="left">
                        <telerik:RadTextBox ID="RadTextBoxCertificateCode" runat="server" Width="95%" Skin="Default">
                        </telerik:RadTextBox>
                    </td>
                    <td align="right" nowrap="nowrap" width="7%">有效期至：
                    </td>
                    <td align="left" width="18%">
                        <telerik:RadDatePicker ID="RadDatePicker_ValidEndDate" MinDate="01/01/1900" runat="server"
                            Width="97%" />
                    </td>
               
                    <td align="right" nowrap="nowrap" width="7%">姓名：
                    </td>
                    <td align="left" width="18%">
                        <telerik:RadTextBox ID="RadTextBoxWorkerName" runat="server" Width="97%" Skin="Default">
                        </telerik:RadTextBox>
                    </td>
                    <td align="right" nowrap="nowrap"  width="7%">证件号码：
                    </td>
                    <td align="left" width="18%">
                        <telerik:RadTextBox ID="RadTextBoxWorkerCertificateCode" runat="server" Width="97%" Skin="Default">
                        </telerik:RadTextBox>
                    </td>

                </tr>
                <tr>
                    <td align="right" nowrap="nowrap" >岗位工种：
                    </td>
                    <td align="left" colspan="3">
                        <uc1:PostSelect ID="PostSelect1" runat="server" />
                    </td>
                    <td align="right" nowrap="nowrap" >证书状态：
                    </td>
                    <td align="left" >
                        <telerik:RadComboBox ID="RadComboBoxStatus" runat="server" Width="97%">
                            <Items>
                                <telerik:RadComboBoxItem Text="全部" Value="" />
                                <telerik:RadComboBoxItem Text="当前有效" Value="当前有效" Selected="true" />
                                <telerik:RadComboBoxItem Text="已过期" Value="已过期" />
                                <telerik:RadComboBoxItem Text="已离京" Value="离京变更" />
                                <telerik:RadComboBoxItem Text="已注销" Value="注销" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
               
                    <td colspan="4" align="left">
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
                            <telerik:GridBoundColumn UniqueName="PostTypeName" DataField="PostTypeName" HeaderText="岗位类别">
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" Wrap="false" />
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
                            <telerik:GridTemplateColumn UniqueName="Status" HeaderText="最近业务">
                                <ItemTemplate>
                                    <%# Eval("Status").ToString()%> <%# Eval("Remark") != null && Eval("Remark").ToString().Contains("超龄")==true?"(超龄)":""%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" Wrap="false" />
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
