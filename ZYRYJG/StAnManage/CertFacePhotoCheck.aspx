<%@ Page Title="" Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="CertFacePhotoCheck.aspx.cs" Inherits="ZYRYJG.StAnManage.CertFacePhotoCheck" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Windows7" />
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Windows7">
    </telerik:RadWindowManager>
    <div class="div_out">
        <div class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt; 统计分析 &gt;&gt;
               从业人员业务统计 &gt;&gt; <strong>证书一寸照片采集情况</strong>
            </div>
        </div>
        <div class="content">
            <div class="jbxxbt">
                证书一寸照片采集情况
            </div>
            <%-- <div class="DivTitleOn" onclick="DivOnOff(this,'Td3',event);" title="折叠">
                    统计说明
                </div>--%>
            <div style="width: 98%; margin: 0 auto; padding-top: 8px; border: none;">
                <telerik:RadGrid ID="RadGrid1" AutoGenerateColumns="False" runat="server" Skin="Blue"
                    EnableAjaxSkinRendering="False" EnableEmbeddedSkins="False" Width="99%" GridLines="None"
                    BorderWidth="0">
                    <HeaderContextMenu EnableEmbeddedSkins="False">
                    </HeaderContextMenu>
                    <MasterTableView CommandItemDisplay="None" NoMasterRecordsText="　没有可显示的记录" Width="100%">
                        <Columns>
                            <telerik:GridBoundColumn UniqueName="PostName" DataField="PostName" HeaderText="岗位类别">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn UniqueName="validCount" DataField="validCount" HeaderText="有效证书数量">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="checkCount" DataField="checkCount" HeaderText="已比对完成数量">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="havePhotoCount" DataField="havePhotoCount" HeaderText="存在照片数量">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="BL" DataField="BL" HeaderText="占比">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                        </Columns>
                        <HeaderStyle Font-Bold="True" />
                    </MasterTableView>
                    <FilterMenu EnableEmbeddedSkins="False">
                    </FilterMenu>
                </telerik:RadGrid>
            </div>
            <br />
        </div>
    </div>
</asp:Content>
