<%@ Page Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="CertifReport.aspx.cs" Inherits="ZYRYJG.RenewCertifates.CertifReport" %>

<%@ Register Src="../CheckAll.ascx" TagName="CheckAll" TagPrefix="uc3" %>
<%@ Register Src="../PostSelect.ascx" TagName="PostSelect" TagPrefix="uc1" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc4" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function onRequestStart(sender, args) {

                if (args.get_eventTarget().indexOf("ButtonExportToExcel") >= 0) {
                    args.set_enableAjax(false);

                }
            }
            function getEventObject(W3CEvent) {   //事件标准化函数
                return W3CEvent || window.event;
            }
            function getPointerPosition(e) {   //兼容浏览器的鼠标x,y获得函数
                e = e || getEventObject(e);
                var x = e.pageX || (e.clientX + (document.documentElement.scrollLeft || document.body.scrollLeft));
                var y = e.pageY || (e.clientY + (document.documentElement.scrollTop || document.body.scrollTop));

                return { 'x': x, 'y': y };
            }

            function setImgSize(img, imgWidth, timgHeight, position, e) {
                img.style.width = imgWidth + "px";
                img.style.height = timgHeight + "px";

                var pos = getPointerPosition(e);

                img.style.position = position;
                if (position == "absolute") {
                    img.style.top = -timgHeight + 20 + "px";
                    img.style.left = -imgWidth + 40 + "px";
                }
                else {
                    img.style.top = 0;
                    img.style.left = 0;
                }
            }

        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManager ID="RadAjaxManager2" runat="server" EnableAJAX="true">
        <ClientEvents OnRequestStart="onRequestStart" />
        <AjaxSettings>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Windows7" />
    <telerik:RadWindowManager runat="server" RestrictionZoneID="offsetElement" ID="RadWindowManager1"
        EnableShadow="true" EnableEmbeddedScripts="true" Skin="Windows7" VisibleStatusbar="false">
    </telerik:RadWindowManager>
    <style type="text/css">
        .btn_link {
            color: Blue;
            text-decoration: none;
            cursor: pointer;
            border: none;
            background-color: transparent;
            padding: 2px 2px !important;
            margin: 2px 2px !important;
        }

        .btn_add {
            background: url(../Skins/Blue/Grid/AddRecord.gif) no-repeat left center;
            margin: 4px;
            border: none;
            cursor: pointer;
            width: 16px;
        }

        .btn_edit {
            background: url(../images/jia.gif) no-repeat left center;
            margin: 4px;
            border: none;
            cursor: pointer;
            width: 16px;
        }

        .btn_close {
            background: url(../images/close.gif) no-repeat left center;
            margin: 4px;
            border: none;
            cursor: pointer;
            width: 16px;
        }

        .btn_view {
            background: url(../images/1034.gif) no-repeat left center;
            margin: 4px;
            border: none;
            cursor: pointer;
            width: 16px;
        }

        .btn_xls {
            background: url(../images/xls.gif) no-repeat left center;
            margin: 4px;
            border: none;
            cursor: pointer;
            width: 16px;
        }

        .btn_doc {
            background: url(../images/doc.gif) no-repeat left center;
            margin: 4px;
            border: none;
            cursor: pointer;
            width: 16px;
        }

        .btn_up {
            background: url(../images/upload.png) no-repeat left center;
            margin: 4px;
            border: none;
            cursor: pointer;
            width: 16px;
        }

        .btn_report {
            background: url(../images/report.png) no-repeat left center;
            margin: 4px;
            border: none;
            cursor: pointer;
            width: 16px;
        }

        .btn_cancel {
            background: url(../images/cando.gif) no-repeat left center;
            margin: 4px;
            border: none;
            cursor: pointer;
            width: 16px;
        }

        a {
            margin: 2px;
            text-decoration: none !important;
        }

        .float {
            padding: 10px;
        }
    </style>
    <div class="div_out">
        <div class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt; 证书续期 &gt;&gt;
                <asp:Label ID="LabelAccepy" runat="server" Text="Label"></asp:Label>
                &gt;&gt; <strong>续期初审汇总上报</strong>
            </div>
        </div>
        <div class="content">
            <div class="DivTitleOn" onclick="DivOnOff(this,'Td3',event);" title="折叠">
                汇总流程说明
            </div>
            <div class="DivContent" id="Td3">
                1、新增汇总表<span class="btn_add float">&nbsp;</span>：按查询条件筛选已初审的申请，保存汇总。<br />
                2、导出汇总签字表<span class="btn_doc float">&nbsp;</span>，签字、盖章，扫描成图片（jpg格式）备用。<br />
                3、导出汇总明细Excel<span class="btn_xls float">&nbsp;</span>，备用。<br />
                4、上传签字扫描件及明细<span class="btn_up float">&nbsp;</span><br />
                5、上报<span class="btn_report float">&nbsp;</span>
            </div>
            <table class="bar_cx">
                <tr>
                    <td width="8%" align="right" nowrap="nowrap">岗位工种：
                    </td>
                    <td align="left" width="42%">
                        <uc1:PostSelect ID="PostSelect1" runat="server" />
                    </td>
                    <td align="right" nowrap="nowrap">初审日期：
                    </td>
                    <td align="left">
                        <telerik:RadDatePicker ID="RadDatePickerCheckStartDate" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                            Width="46%" Style="float: left;" />
                        <div class="RadPicker">至</div>
                        <telerik:RadDatePicker ID="RadDatePickerCheckEndDate" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                            Width="46%" Style="float: left;" />
                    </td>
                </tr>
                <tr>
                    <td align="right" nowrap="nowrap">汇总批次号：
                    </td>
                    <td align="left">
                        <telerik:RadTextBox ID="RadTextBoxReportCode" runat="server" Width="80%" Skin="Default" MaxLength="100"
                            >
                        </telerik:RadTextBox>
                    </td>
                    <td align="right" nowrap="nowrap">汇总日期：
                    </td>
                    <td align="left">
                        <telerik:RadDatePicker ID="RadDatePickerReportDateStart" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                            Width="46%" Style="float: left;" />
                        <div class="RadPicker">至</div>
                        <telerik:RadDatePicker ID="RadDatePickerReportDateEnd" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                            Width="46%" Style="float: left;" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center">
                        <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
                    </td>
                </tr>
            </table>


            <div class="table_cx">
                <img src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                证书续期初审汇总列表
            </div>
            <div style="width: 98%; margin: 0 auto;">
                <telerik:RadGrid ID="RadGridReport" runat="server" AllowCustomPaging="true" GridLines="None" 
                    AllowPaging="True" PageSize="10" AllowSorting="True" AutoGenerateColumns="False"
                    SortingSettings-SortToolTip="单击进行排序" Width="100%" Skin="Blue" EnableAjaxSkinRendering="false"
                    EnableEmbeddedSkins="false" OnItemCommand="RadGridReport_ItemCommand" PagerStyle-AlwaysVisible="true">
                    <ClientSettings EnableRowHoverStyle="true">
                    </ClientSettings>
                    <MasterTableView CommandItemDisplay="Top" CommandItemStyle-HorizontalAlign="Left" AllowMultiColumnSorting="True" DataKeyNames="ReportCode,PostTypeID,CertCount,FIRSTCHECKUNITNAME,FirstCheckStartDate,FirstCheckEndDate,ReportDate" NoMasterRecordsText="　没有可显示的记录">
                        <CommandItemTemplate>
                            <div class="grid_CommandBar" style="line-height: 20px; padding-left: 10px;"  onclick="javascript:SetIfrmSrc('CertifReportEdit.aspx?o=<%=Request["o"]%>');" >
                                <input type="button" value=" " class="rgAdd"  />
                                <nobr  class="grid_CmdButton" style="cursor: pointer;">
                                       &nbsp;新增汇总表</nobr>
                            </div>
                        </CommandItemTemplate>
                        <Columns>
                            <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridBoundColumn>

                            <telerik:GridTemplateColumn UniqueName="PostTypeName" HeaderText="岗位类别">
                                <ItemTemplate>
                                    <%# PostName(Eval("PostTypeID").ToString()) %>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" Wrap="false" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn UniqueName="CertCount" DataField="CertCount" HeaderText="证书数量">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="FIRSTCHECKUNITNAME" DataField="FIRSTCHECKUNITNAME" HeaderText="初审单位">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="FirstCheckStartDate" HeaderText="初审时间">
                                <ItemTemplate>
                                    <%# Convert.ToDateTime(Eval("FirstCheckStartDate")).ToString("yyyy-MM-dd") == Convert.ToDateTime(Eval("FirstCheckEndDate")).ToString("yyyy-MM-dd")? Convert.ToDateTime(Eval("FirstCheckStartDate")).ToString("yyyy.MM.dd"):string.Format("{0} - {1}",Convert.ToDateTime(Eval("FirstCheckStartDate")).ToString("yyyy.MM.dd"),Convert.ToDateTime(Eval("FirstCheckEndDate")).ToString("yyyy.MM.dd")) %>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" Wrap="false" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn UniqueName="ReportCode" DataField="ReportCode" HeaderText="汇总批次号">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="ReportDate" DataField="ReportDate" HtmlEncode="false"
                                DataFormatString="{0:yyyy-MM-dd}" HeaderText="汇总时间">
                                <HeaderStyle HorizontalAlign="Center" Wrap="true" />
                                <ItemStyle HorizontalAlign="Center" Wrap="true" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="CertificateChangeID" HeaderText="汇总扫描件">
                                <ItemTemplate>
                                    <%#showFJ(Eval("ReportCode").ToString()) %>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" Font-Bold="true" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn UniqueName="ReportStatus" DataField="ReportStatus" HeaderText="上报状态">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="CheckStatus" DataField="CheckStatus" HeaderText="审核状态">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="Edit" HeaderText="操作<span style='font-weight:normal;color:#999'>(鼠标悬停有功能提示)</span>">
                                <ItemTemplate>
                                    <input id="Button1" runat="server" type="button" value="" class="btn_edit" title="查看汇总表" onclick='<%# string.Format("javascript:SetIfrmSrc(\"CertifReportEdit.aspx?o={0}&r={1}\");return false;",Eval("PostTypeID"),Eval("ReportCode"))%>' visible='<%#Eval("ReportStatus").ToString()=="未上报" %>' />
                                    <asp:Button ID="Button2" runat="server" ToolTip="删除汇总" CssClass="btn_close" CommandName="delete" OnClientClick="javascript:if(confirm('您确认要删除吗？')==false) return false;" Visible='<%#Eval("ReportStatus").ToString()=="未上报"%>' />
                                    <asp:Button ID="ButtonOutput" runat="server" ToolTip="导出汇总签字表（word）" Text="" CssClass="btn_doc" CommandName="OutPutReport" Visible='<%#Eval("ReportStatus").ToString()=="未上报" %>' />
                                    <asp:Button ID="ButtonOutputExcel" runat="server" ToolTip="导出汇总明细（excel）" Text="" CssClass="btn_xls" CommandName="OutPutReportExcel" Visible='<%#Eval("ReportStatus").ToString()=="未上报" %>' />
                                    <input id="Button4" runat="server" type="button" title="上传签字扫描件及明细" value="" class="btn_up" onclick='<%# string.Format("javascript:SetIfrmSrc(\"CertifReportUpload.aspx?r={0}\");return false;",Eval("ReportCode"))%>' visible='<%#Eval("ReportStatus").ToString()=="未上报" %>' />
                                    <asp:Button ID="Button3" runat="server" ToolTip="上报" CssClass="btn_report" CommandName="report" OnClientClick="javascript:if(confirm('您确认要上报吗？')==false) return false;" Visible='<%#Eval("ReportStatus").ToString()=="未上报" && IfUploadReportImg(Eval("ReportCode").ToString())==true %>' />
                                    <asp:Button ID="Button5" runat="server" ToolTip="取消上报" CssClass="btn_cancel" CommandName="Cancelreport" OnClientClick="javascript:if(confirm('您确认要取消上报吗？')==false) return false;" Visible='<%#Eval("ReportStatus").ToString()=="已上报" && Eval("CheckStatus").ToString()=="已初审"  %>' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" Wrap="false" ForeColor="Blue" />
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
                </telerik:RadGrid>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetReportList"
                    TypeName="DataAccess.CertificateContinueDAL" SelectCountMethod="SelectReportCount"
                    EnablePaging="true" MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex"
                    SortParameterName="orderBy">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                            DefaultValue="" ConvertEmptyStringToNull="false" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </div>
            <br />
        </div>
    </div>
    <uc4:IframeView ID="IframeView" runat="server" />

</asp:Content>
