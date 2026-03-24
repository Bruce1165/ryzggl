<%@ Page Title="" Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="CertifEnterAccepted.aspx.cs" Inherits="ZYRYJG.CertifEnter.CertifEnterAccepted" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../PostSelect.ascx" TagName="PostSelect" TagPrefix="uc1" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<%@ Register Src="../CheckAll.ascx" TagName="CheckAll" TagPrefix="uc3" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            $(function () {
                //建委受理结果
                $("#<%= RadioButtonListJWAccept.ClientID%> input").each(function () {
                    $(this).click(function () {
                        var TextBoxGetResult = $("#<%= TextBoxGetResult.ClientID%>");
                            if ($(this).val() == "通过") {
                                TextBoxGetResult.val("通过");
                            }
                            else {
                                TextBoxGetResult.val("退回个人");
                            }
                        });
                    });
            });
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadWindowManager runat="server" RestrictionZoneID="offsetElement" ID="RadWindowManager1"
        EnableShadow="true" EnableEmbeddedScripts="true" Skin="Windows7" VisibleStatusbar="false">
    </telerik:RadWindowManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadGrid1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="PostSelect1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="PostSelect1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Windows7" />
    <style type="text/css">
        .tl, .flag_red, .flag_orange, .flag_blue, .flag_green, .flag_black, .lookout, .watch {
            color: #444;
            float: left;
            margin: 0 2px;
        }

        .pl20 {
            padding-left: 20px;
        }

        .flag_red {
            background: url(../Images/flag_red.png) no-repeat left center transparent;
        }

        .flag_orange {
            background: url(../Images/flag_purple.png) no-repeat left center transparent;
        }

        .flag_blue {
            background: url(../Images/flag_blue.png) no-repeat left center transparent;
        }

        .flag_green {
            background: url(../Images/flag_green.png) no-repeat left center transparent;
        }

        .flag_black {
            background: url(../Images/flag_black.png) no-repeat left center transparent;
        }

        .lookout {
            background: url(../Images/lookout.png) no-repeat left center transparent;
            background-size: 16px 16px;
        }

        .watch {
            background: url(../Images/watch.png) no-repeat left center transparent;
            background-size: 16px 16px;
        }

        .pointer {
            cursor: pointer;
            width: 16px;
            height: 16px;
        }

        .RadPicker2 {
            float: left;
            line-height: 27px;
            padding: 0px 1px;
            background-repeat: no-repeat;
        }

        .float {
            float: left;
            margin: 4px 12px;
            position: relative;
        }

        .absolute {
            position: relative;
            clear: both;
        }
    </style>
    <div class="div_out">
        <div class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt; 证书进京 &gt;&gt;
                <asp:Label ID="LabelPostType" runat="server" Text="三类人员"></asp:Label>
                &gt;&gt; <strong>进京受理</strong>
            </div>
        </div>
        <div class="content">
            <table class="bar_cx" id="DivSearch" runat="server">
                <tr>
                    <td align="right" nowrap="nowrap">姓名：
                    </td>
                    <td align="left" width="39%">
                        <telerik:RadTextBox ID="rdtxtWorkerName" runat="server" Width="97%" Skin="Default">
                        </telerik:RadTextBox>
                    </td>
                    <td align="right" width="11%" nowrap="nowrap">岗位工种：
                    </td>
                    <td align="left" width="39%">
                        <uc1:PostSelect ID="PostSelect1" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="right" nowrap="nowrap" width="11%">证件号码：
                    </td>
                    <td align="left" width="39%">
                        <telerik:RadTextBox ID="rdtxtZJHM" runat="server" Width="97%" Skin="Default">
                        </telerik:RadTextBox>
                    </td>
                    <td width="11%" align="right" nowrap="nowrap">证书编号：
                    </td>
                    <td width="39%" align="left">
                        <telerik:RadTextBox ID="rdtxtCertificateCode" runat="server" Width="95%" Skin="Default">
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" nowrap="nowrap">申请批号：
                    </td>
                    <td align="left">
                        <telerik:RadTextBox ID="rdtxtApplyCode" runat="server" Width="97%" Skin="Default">
                        </telerik:RadTextBox>
                    </td>
                    <td align="right" nowrap="nowrap">现单位名称：
                    </td>
                    <td align="left">
                        <telerik:RadTextBox ID="rdtxtQYMC" runat="server" Width="97%" Skin="Default">
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" nowrap="nowrap">受理状态：
                    </td>
                    <td align="left">
                        <asp:RadioButtonList ID="RadioButtonListApplyStatus" runat="server" RepeatDirection="Horizontal"
                            AutoPostBack="false" Width="150px">
                            <asp:ListItem Value="已申请" Selected="True">未受理</asp:ListItem>
                            <asp:ListItem Value="已受理">已受理</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td align="right" nowrap="nowrap">受理时间：
                    </td>
                    <td align="left">
                        <telerik:RadDatePicker ID="RadDatePicker_GetDateStart" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                            Width="46%" />
                        <div class="RadPicker">至</div>
                        <telerik:RadDatePicker ID="RadDatePicker_GetDateEnd" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                            Width="46%" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center">
                        <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="bt_large" OnClick="ButtonSearch_Click" />
                    </td>
                </tr>
            </table>
            <%-- <div class="table_cx">
                <img alt="" src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                <asp:Label ID="LabelTitle" runat="server" Text="待受理进京申请列表"></asp:Label>
            </div>--%>

            <div style="width: 98%; line-height: 20px; height: 20px; vertical-align: middle; text-align: left; padding: 0; margin: 4px 0;">
                    <div class="tl pl20">图例说明：</div>
                    <div class="flag_blue pl20">社保缺失，</div>
                    <div class="flag_black pl20">持证校验异常</div>
                    <div style="text-align: right; right: 100px; cursor: pointer; color: blue;">
                        <asp:Label ID="LabelTitle" runat="server" Text=""></asp:Label></div>

                </div>
            <div style="width: 98%; margin: 0 auto;">
                
                <telerik:RadGrid ID="RadGrid1" AllowPaging="True" SortingSettings-SortToolTip="单击进行排序" PagerStyle-AlwaysVisible="true"
                    runat="server" AutoGenerateColumns="False" AllowSorting="True" GridLines="None"
                    CellPadding="0" Width="100%" Skin="Blue" EnableAjaxSkinRendering="False" EnableEmbeddedSkins="False"
                    OnItemDataBound="RadGrid1_ItemDataBound" OnDeleteCommand="RadGrid1_DeleteCommand"
                    OnPageIndexChanged="RadGridAccept_PageIndexChanged" OnDataBound="RadGridAccept_DataBound">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ApplyID,UnitCode,AddPostID" NoMasterRecordsText="没有可显示的记录">
                        <Columns>
                            <telerik:GridTemplateColumn UniqueName="SelectAllColumn" HeaderText="Highlight <br/> ship name">
                                <HeaderTemplate>
                                    <uc3:CheckAll ID="CheckAll1" runat="server" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" onclick='checkBoxClick(this.checked);' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn HeaderText="序号" UniqueName="RowNum" DataField="RowNum" AllowSorting="false">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="申请日期" UniqueName="ApplyDate" DataField="ApplyDate"
                                DataFormatString="{0:yyyy-MM-dd}">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn HeaderText="单位审核日期" UniqueName="NewUnitCheckTime" DataField="NewUnitCheckTime"
                                DataFormatString="{0:yyyy-MM-dd}">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
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
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="UnitName" DataField="UnitName" HeaderText="现聘用单位名称">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="sb" HeaderText="校验图例">
                                <ItemTemplate>
                                  <%# Eval("SheBaoCheck") ==DBNull.Value?"<div class=\"flag_blue pointer\" onclick='javascript:alert(\"尚未比对!（当日提交数据后,次日进行比对）\")'>&nbsp;</div>": Eval("SheBaoCheck").ToString()=="1"?"":string.Format("<div class=\"flag_blue pointer\" onclick='javascript:SetIfrmSrc(\"../PersonnelFile/RYSBView.aspx?o1={0}&o2={1}&o3={2}\");'>&nbsp;</div>", Eval("WorkerCertificateCode"), Eval("UnitCode"), Eval("ApplyDate"))%>
                                     <%# Eval("ZACheckResult") ==DBNull.Value?"<div class=\"flag_black pointer\" onclick='javascript:alert(\"尚未比对!\")'>&nbsp;</div>": Eval("ZACheckResult").ToString()=="1"?"": string.Format("<div class=\"flag_black pointer\" onclick='javascript:alert(\"{0}\")'>&nbsp;</div>",Eval("ZACheckRemark"))%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" Font-Bold="true" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn UniqueName="ApplyStatus" DataField="ApplyStatus" HeaderText="状态">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="Detail" HeaderText="详细">
                                <ItemTemplate>
                                    <span class="link_edit" onclick='javascript:SetIfrmSrc("CertifEnterApplyEdit.aspx?t=<%# Utility.Cryptography.Encrypt(Eval("PostTypeID").ToString()) %>&o=<%# Utility.Cryptography.Encrypt(Eval("ApplyID").ToString()) %>");'>
                                        <nobr>详细</nobr>
                                    </span>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                            </telerik:GridTemplateColumn>
                            <%-- <telerik:GridButtonColumn UniqueName="Delete" HeaderText="删除" CommandName="Delete"
                                Text="&lt;span onclick=&quot;if(!confirm('您确定要删除么?'))return false; &quot; &gt;删除&lt;/span&gt;">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridButtonColumn>--%>
                            <telerik:GridTemplateColumn UniqueName="GetCode" HeaderText="受理通知单">
                                <ItemTemplate>
                                    <%# Eval("GetCode") == System.DBNull.Value ? "&nbsp;" : "<a href='CertifEnterAccepted.aspx?o=" + Eval("PostTypeID").ToString() + "&code=" + Server.UrlEncode(Eval("GetCode").ToString()) + "'><nobr style='color:blue;text-decoration: underline;'>" + Eval("GetCode").ToString() + "</nobr></a>"%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
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
            <table id="TableJWAccept" runat="server" visible="false" border="0" cellpadding="5" cellspacing="1" width="100%" class="table" style="text-align: center; margin: 10px auto">
                <tr class="GridLightBK">
                    <td colspan="2" class="barTitle">进京受理</td>
                </tr>
                <tr class="GridLightBK">
                    <td width="20%" align="right">处理结果：</td>
                    <td width="80%" align="left">
                        <asp:RadioButtonList ID="RadioButtonListJWAccept" runat="server" RepeatDirection="Vertical" TextAlign="right">
                            <asp:ListItem Text="通过" Value="通过" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="不通过" Value="不通过"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr class="GridLightBK">
                    <td width="20%" align="right">处理意见：</td>
                    <td width="80%" align="left">

                        <asp:TextBox ID="TextBoxGetResult" runat="server" TextMode="MultiLine" Rows="3" Width="99%" MaxLength="100" Text="通过"></asp:TextBox>
                    </td>
                </tr>
                <tr class="GridLightBK">
                    <td colspan="2" align="center">
                        <asp:Button ID="ButtonAccepte" runat="server" Text="确 定" CssClass="bt_large" OnClick="ButtonAccepte_Click" />
                    </td>
                </tr>
            </table>
            <div style="width: 90%; margin: 5px auto; text-align: center; padding-top: 8px;">
                <asp:Button ID="ButtonOutput"
                    runat="server" Text="导出受理通知单" CssClass="bt_maxlarge" Visible="false" OnClick="ButtonOutput_Click" />&nbsp;<asp:Button
                        ID="ButtonReturn" runat="server" Text="返 回" CssClass="bt_large" Visible="false"
                        OnClick="ButtonReturn_Click" />
            </div>
            <br />
        </div>
    </div>
    <uc4:IframeView ID="IframeView" runat="server" />
</asp:Content>
