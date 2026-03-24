<%@ Page Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="CertifHistory.aspx.cs" Inherits="ZYRYJG.PersonnelFile.CertifHistory" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManager ID="RadAjaxManager2" runat="server" EnableAJAX="true">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadGrid1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server" />
    <telerik:RadWindowManager runat="server" RestrictionZoneID="offsetElement" ID="RadWindowManager1"
        EnableShadow="true" EnableEmbeddedScripts="true" Skin="Windows7">
    </telerik:RadWindowManager>
    <div class="div_out">
        <div id="div_top" class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt; <strong>证书变化历史</strong>
            </div>
        </div>
        <div class="table_border" style="width: 98%; margin: 5px auto;">
            <div class="content">
                <p class="jbxxbt">
                    证书变化历史</p>
                <div>
                    <div style="width: 98%; margin: 0 auto;">
                        <telerik:RadGrid ID="RadGrid1" runat="server" GridLines="None"
                            AllowPaging="True" PageSize="50" AllowSorting="False" AutoGenerateColumns="False"
                            AllowCustomPaging="true" SortingSettings-SortToolTip="单击进行排序" Width="100%" Skin="Blue"
                            EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false" OnDataBound="RadGrid1_DataBound">
                            <MasterTableView DataKeyNames="CreateTime">
                                <Columns>
                                    <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    </telerik:GridBoundColumn>                                  
                                    <telerik:GridBoundColumn UniqueName="PostName" DataField="PostName" HeaderText="岗位工种">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="CertificateCode" DataField="certificateCode"
                                        HeaderText="证书编号">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="ValidEndDate" DataField="ValidEndDate" HeaderText="有效期至"
                                        HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="WorkerName" DataField="WorkerName" HeaderText="姓名">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="WorkerCertificateCode" DataField="WorkerCertificateCode"
                                        HeaderText="证件号码">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="UnitName" DataField="UnitName" HeaderText="单位名称">
                                        <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="STATUS" DataField="STATUS" HeaderText="业务类型">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="ModifyTime" DataField="ModifyTime" HeaderText="决定日期"
                                        HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd HH:mm}">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                </Columns>
                                <PagerTemplate>
                                    <uc2:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                                </PagerTemplate>
                                <HeaderStyle Font-Bold="true" />
                            </MasterTableView>
                        </telerik:RadGrid>
                        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="Model.CertificateHistoryOB"
                            DeleteMethod="Delete" InsertMethod="Insert" SelectMethod="GetListView" TypeName="DataAccess.CertificateHistoryDAL"
                            UpdateMethod="Update" SelectCountMethod="SelectCountView" EnablePaging="true"
                            MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex"
                            SortParameterName="orderBy">
                            <SelectParameters>
                                <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                                    DefaultValue="" ConvertEmptyStringToNull="false" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </div>
                    <div style="width: 90%; margin: 5px auto; text-align: center; line-height:40px;">
                        <input id="ButtonReturn" type="button" value="返 回" class="bt_large" onclick="javascript:hideIfam();" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
