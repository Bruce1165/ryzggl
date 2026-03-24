<%@ Page Title="" Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="CompanyWorkCertInfoHighcharts.aspx.cs" Inherits="ZYRYJG.PersonnelFile.CompanyWorkCertInfoHighcharts" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Import Namespace="DataAccess" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" src="../Scripts/jquery-1.8.3.min.js"></script>
    <script type="text/javascript" src="../Scripts/highcharts.js"></script>
    <script type="text/javascript" src="../Scripts/highcharts-3d.js"></script>
    <div class="div_out">
        <div id="div_top" class="dqts">
            <div id="divRoad" runat="server" style="float: left;">
                当前位置 &gt;&gt; 业务办理 &gt;&gt;企业业务办理&gt;&gt;证书过期预警
            </div>
        </div>
        <div class="content">
            <p class="jbxxbt">
                证书过期预警
            </p>
            <div style="margin: 8px 20px;">
                <telerik:RadGrid ID="RadGridCertificate" runat="server" GridLines="None"
                    AllowPaging="false" PageSize="10" AllowSorting="false" AutoGenerateColumns="False"
                    SortingSettings-SortToolTip="单击进行排序" Width="100%"
                    Skin="Blue" EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false">
                    <ClientSettings EnableRowHoverStyle="true">
                    </ClientSettings>
                    <MasterTableView NoMasterRecordsText="　没有可显示的记录">
                        <Columns>
                            <telerik:GridBoundColumn UniqueName="PostTypeName" DataField="PostTypeName" HeaderText="岗位类别">
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" Width="300px" />
                                <ItemStyle HorizontalAlign="Left" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="sl" DataField="sl" HeaderText="证书数量（本）">
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" Width="120px" />
                                <ItemStyle HorizontalAlign="Left" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="VALIDENDDATE" DataField="VALIDENDDATE" HeaderText="过期日期"
                                HtmlEncode="false" DataFormatString="{0:yyyy.MM.dd}">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" Width="120px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                    <StatusBarSettings LoadingText="正在读取数据" ReadyText="完成" />
                </telerik:RadGrid>
            </div>
            <div class="DivContent" runat="server" id="divBaseInfo" style="text-align: left; font-size: 16px; line-height: 150%; padding: 20px 20px; color: #404040">
            </div>
        </div>
    </div>
</asp:Content>
