<%@ Page Language="C#" Title="" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="ApplyProcess.aspx.cs" Inherits="ZYRYJG.PersonnelFile.ApplyProcess" %>

<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" EnableAJAX="true">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadGrid1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Windows7" />
    <telerik:RadWindowManager runat="server" RestrictionZoneID="offsetElement" ID="RadWindowManager1"
        EnableShadow="true" EnableEmbeddedScripts="true" Skin="Windows7">
    </telerik:RadWindowManager>
    <style type="text/css">
        .f-red {
            color: red;
            padding: 0 4px;
        }

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
                当前位置 &gt;&gt; 我的信息 &gt;&gt;
                <strong>最近办理业务进度</strong>
            </div>
        </div>
        <div class="content">
            <%-- <div class="DivContent" id="Td3">
                业务说明：
            </div>--%>
            <div style="width: 98%; margin: 0 auto;">
                <div class="table_cx" style="margin-top: 20px;">
                    <img alt="" src="../Images/date.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                    最近办理业务历史<span style="color:red">（注意：安全生产三类人员和特种作业人员证书业务市建委决定后，需要建设部核准后才能生成电子证书，核准未通过请按提示整改后，在电子证书下载页面申请重新校验）</span>
                </div>
                <telerik:RadGrid ID="RadGridProcess" runat="server" GridLines="None"
                    AllowPaging="false" AllowSorting="false" AutoGenerateColumns="False"
                    SortingSettings-SortToolTip="单击进行排序" Width="100%"
                    Skin="Blue" EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false">
                    <ClientSettings EnableRowHoverStyle="true">
                    </ClientSettings>
                    <MasterTableView DataKeyNames="DataID" NoMasterRecordsText="　没有可显示的记录">
                        <Columns>
                            <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="PostName" DataField="PostName" HeaderText="岗位工种">
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="PostName" DataField="CERTIFICATECODE" HeaderText="证书编号">
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" Wrap="false" />
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn UniqueName="ApplyDate" DataField="ApplyDate" HeaderText="申请日期"
                                HtmlEncode="false" DataFormatString="{0:yyyy.MM.dd}">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="ItemType" DataField="ItemType" HeaderText="业务类型">
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="UnitCheckDate" DataField="UnitCheckDate" HeaderText="企业确认日期"
                                HtmlEncode="false" DataFormatString="{0:yyyy.MM.dd}">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="UnitRsult" DataField="UnitRsult" HeaderText="企业确认结果">
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="FirstCheckDate" DataField="AcceptDate" HeaderText="受理日期"
                                HtmlEncode="false" DataFormatString="{0:yyyy.MM.dd}">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="FirstCheckResult" DataField="AcceptResult" HeaderText="受理结果">
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="CHECKDATE" DataField="CHECKDATE" HeaderText="审核日期"
                                HtmlEncode="false" DataFormatString="{0:yyyy.MM.dd}">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="CHECKRESULT" DataField="CHECKRESULT" HeaderText="审核结果">
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="CONFRIMDATE" DataField="CONFRIMDATE" HeaderText="决定日期"
                                HtmlEncode="false" DataFormatString="{0:yyyy.MM.dd}">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="CONFRIMRESULT" DataField="CONFRIMRESULT" HeaderText="决定结果">
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="NOTICEDATE" DataField="NOTICEDATE" HeaderText="告知日期"
                                HtmlEncode="false" DataFormatString="{0:yyyy.MM.dd}">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="NOTICERESULT" DataField="NOTICERESULT" HeaderText="告知结果">
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <%--<telerik:GridTemplateColumn UniqueName="DataID" HeaderText="评价">
                                <ItemTemplate>
                                    <%# (Eval("ApplyDate")!=DBNull.Value && Convert.ToDateTime(Eval("ApplyDate")).AddDays(30)>DateTime.Now)?string.Format("<a href=\"./Appraise.aspx?t={0}&o={1}\" target=\"_blank\" style=\"color: Blue; text-decoration: underline;\">评价</a>",Utility.Cryptography.Encrypt(Eval("ItemCode").ToString()), Utility.Cryptography.Encrypt(Eval("DataID").ToString())):"" %>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridTemplateColumn>--%>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings>
                    </ClientSettings>
                    <StatusBarSettings LoadingText="正在读取数据" ReadyText="完成" />
                </telerik:RadGrid>
            </div>
            <br />
        </div>
    </div>
    <uc4:IframeView ID="IframeView" runat="server" />
</asp:Content>
