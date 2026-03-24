<%@ Page Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="CertifChangeCheckConfirm.aspx.cs" Inherits="ZYRYJG.CertifManage.CertifChangeCheckConfirm" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<%@ Register Src="../PostSelect.ascx" TagName="PostSelect" TagPrefix="uc1" %>
<%@ Register Src="../CheckAll.ascx" TagName="CheckAll" TagPrefix="uc3" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <style type="text/css">
        .tl, .flag_red, .flag_orange, .flag_blue, .flag_green, .flag_black,.lookout,.watch,.enforce,.changeImg {
            color: #444;
            float: left;
            margin: 0 2px;
        }

        .pl20 {
            padding-left: 20px;
        }
        .changeImg {
            background: url(../Images/man.png) no-repeat left center transparent;
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
            background-size:16px 16px;
        }  
        
         .watch {
            background: url(../Images/watch.png) no-repeat left center transparent;
            background-size:16px 16px;
        }          

        .pointer {
            cursor: pointer;
            width: 16px;
            height: auto;
        }

        .RadPicker2 {
            float: left;
            line-height: 27px;
            padding: 0px 1px;
            background-repeat: no-repeat;
        }

        .float {
            float: left; margin:4px 12px;
            position:relative;
        }
        .absolute
        {
            position:relative;
            clear:both;
        }

    </style>
    <telerik:radcodeblock ID="RadCodeBlock1" runat="server">
    </telerik:radcodeblock>
    <telerik:radwindowmanager runat="server" RestrictionZoneID="offsetElement" ID="RadWindowManager1"
        EnableShadow="true" EnableEmbeddedScripts="true" Skin="Windows7" VisibleStatusbar="false">
    </telerik:radwindowmanager>
    <script type="text/javascript">
        function onRequestStart(sender, args) {

            if (args.get_eventTarget().indexOf("ButtonExportExcel") >= 0) {
                args.set_enableAjax(false);

            }
        }
    </script>
    <telerik:radajaxmanager ID="RadAjaxManager1" runat="server" EnableAJAX="true">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadGrid1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:radajaxmanager>
    <telerik:radajaxloadingpanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default" />
    <div class="div_out">
        <div class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt; 证书管理 &gt;&gt;
                <asp:Label ID="LabelPostType" runat="server" Text="三类人员"></asp:Label>
                &gt;&gt; <strong>变更审查</strong>
            </div>
        </div>
        <div class="table_cx" style="padding-right: 40px; float: right; clear: right; padding-left: 20px; margin-top:12px">
            <a id="Link_DelHistory" runat="server" href='DelChangeHistory.aspx' style="color: #DC2804; font-weight: bold;">
                <img alt="" src="../Images/jia.gif" style="width: 14px; height: 15px; margin-bottom: -2px; padding-right: 5px; border: none;" />查看删除历史记录</a>
        </div>
        <div class="content">
            <table class="bar_cx" runat="server" id="DivSearch">
                <tr>
                    <td align="right" nowrap="nowrap" width="11%">变更类型：
                    </td>
                    <td align="left">
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td align="left" width="30%">
                                    <telerik:radcombobox ID="RadComboBoxChangeType" runat="server" Width="97%" Skin="Default">
                                        <Items>
                                            <telerik:RadComboBoxItem Value="" Text="全部" Selected="True" />
                                            <telerik:RadComboBoxItem Value="京内变更" Text="京内变更" />
                                            <telerik:RadComboBoxItem Value="离京变更" Text="离京变更" />
                                            <telerik:RadComboBoxItem Value="注销" Text="注销" />                                            
                                        </Items>
                                    </telerik:radcombobox>
                                </td>
                                <td align="right" nowrap="nowrap" width="25%">&nbsp;处理方式：
                                </td>
                                <td align="left" width="45%">
                                    <telerik:radcombobox ID="RadComboBoxDealWay" runat="server" Width="97%" Skin="Default">
                                        <Items>
                                            <telerik:RadComboBoxItem Value="" Selected="True" Text="全部" />
                                            <telerik:RadComboBoxItem Value="证书信息修改" Text="证书信息修改" />
                                            <telerik:RadComboBoxItem Value="重新制作证书" Text="重新制作证书" />
                                            <telerik:RadComboBoxItem Value="收回原证书" Text="收回原证书" />
                                        </Items>
                                    </telerik:radcombobox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td align="right" width="11%" nowrap="nowrap">岗位工种：
                    </td>
                    <td align="left" width="39%">
                        <uc1:PostSelect ID="PostSelect1" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="right" nowrap="nowrap">申请人：
                    </td>
                    <td align="left">
                        <telerik:radtextbox ID="RadTextBoxApplyMan" runat="server" Width="97%" Skin="Default">
                        </telerik:radtextbox>
                    </td>
                    <td align="right" nowrap="nowrap">申请批号：
                    </td>
                    <td align="left">
                        <telerik:radtextbox ID="rdtxtApplyCode" runat="server" Width="200px" Skin="Default">
                        </telerik:radtextbox>（可扫条码）
                    </td>
                </tr>
                <tr>
                    <td align="right" nowrap="nowrap">受理状态：
                    </td>
                    <td align="left">
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td align="left" width="50%">
                                    <asp:RadioButtonList ID="RadioButtonListStatus" runat="server" RepeatDirection="Horizontal"
                                        AutoPostBack="false" Style="float: left;" Width="150px">
                                        <asp:ListItem Value="未审查" Selected="True">未审查</asp:ListItem>
                                        <asp:ListItem Value="已审查">已审查</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td align="right" nowrap="nowrap" width="15%">&nbsp;受理人：
                                </td>
                                <td align="left" width="35%">
                                    <telerik:radtextbox ID="RadTextBoxGetMan" runat="server" Width="90%" Skin="Default">
                                    </telerik:radtextbox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td align="right" nowrap="nowrap">受理时间：
                    </td>
                    <td align="left">
                        <telerik:raddatepicker ID="RadDatePicker_GetDateStart" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                            Width="46%" />
                        <div class="RadPicker">至</div>
                        <telerik:raddatepicker ID="RadDatePicker_GetDateEnd" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                            Width="46%" />
                    </td>
                </tr>
                <tr>
                    <td align="right" nowrap="nowrap" width="11%">姓 名：
                    </td>
                    <td align="left" width="39%">
                        <telerik:radtextbox ID="rdtxtWorkerName" runat="server" Width="97%" Skin="Default">
                        </telerik:radtextbox>
                    </td>
                    <td align="right" nowrap="nowrap" width="11%">证件号码：
                    </td>
                    <td align="left" width="39%">
                        <telerik:radtextbox ID="rdtxtZJHM" runat="server" Width="97%" Skin="Default">
                        </telerik:radtextbox>
                    </td>
                </tr>
                <tr>
                    <td align="right" nowrap="nowrap">原单位名称：
                    </td>
                    <td align="left">
                        <telerik:radtextbox ID="rdtxtQYMC" runat="server" Width="97%" Skin="Default">
                        </telerik:radtextbox>
                    </td>
                    <td align="right" nowrap="nowrap">新单位名称：
                    </td>
                    <td align="left">
                        <telerik:radtextbox ID="RadTextBoxNewUnit" runat="server" Width="97%" Skin="Default">
                        </telerik:radtextbox>
                    </td>
                </tr>
                <tr>
                    <td width="11%" align="right" nowrap="nowrap">证书编号：
                    </td>
                    <td width="39%" align="left">
                        <telerik:radtextbox ID="rdtxtCertificateCode" runat="server" Width="97%" Skin="Default">
                        </telerik:radtextbox>
                    </td>
                    <td align="right" nowrap="nowrap">新单位机构代码：
                    </td>
                    <td align="left">
                        <telerik:radtextbox ID="RadTextBoxUnitCode" runat="server" Width="97%" Skin="Default" MaxLength="18">
                        </telerik:radtextbox>
                    </td>
                </tr>
                <tr>
                    <td width="11%" align="right" nowrap="nowrap">
                    </td>
                    <td width="39%" align="left">
                        <%--<asp:CheckBox ID="CheckBoxViewUnUnitChecked" runat="server" Text="勾选查看企业没有确认的申请单（用于企业已注销等情况）" />--%>
                    </td>
                    <td align="right" nowrap="nowrap">
                         <asp:Button ID="btnSearch" runat="server" Text="查 询" CssClass="button" OnClick="btnSearch_Click" />
                    </td>
                    <td align="left">
                        
                    </td>
                </tr>
              
            </table>

             <div style="width: 98%; line-height: 20px; height: 20px; vertical-align: middle; padding: 0; margin:8px 0;">
                    <div class="tl pl20">图例说明：</div>
               <div class="changeImg pl20">变更照片，</div>
                    <div class="flag_blue pl20">社保缺失，</div>
                 <div class="enforce"><span style="color: Red;">强&nbsp;</span>申请强制执行</div>
                    
                  
                    
                </div>
            <div style="width: 98%; margin: 0 auto; overflow: auto;">
                <telerik:radgrid ID="RadGrid1" AllowPaging="True"
                    SortingSettings-SortToolTip="单击进行排序" runat="server" AutoGenerateColumns="False"  PagerStyle-AlwaysVisible="true"
                    AllowSorting="True" PageSize="10" GridLines="None" CellPadding="0" Width="99%"
                    Skin="Blue" EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false" OnExcelExportCellFormatting="RadGrid1_ExcelExportCellFormatting"
                    OnPageIndexChanged="RadGridAccept_PageIndexChanged" OnDataBound="RadGridAccept_DataBound">
                    <ClientSettings EnableRowHoverStyle="false">
                        <Selecting AllowRowSelect="false" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="CertificateChangeID,PostID,ApplyCode,ChangeType" NoMasterRecordsText="没有可显示的记录">
                        <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
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
                            <telerik:GridBoundColumn HeaderText="岗位工种" UniqueName="PostName" DataField="PostName">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="ChangeType" DataField="ChangeType" HeaderText="变更类型">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                              <telerik:GridTemplateColumn UniqueName="CheckImg" HeaderText="校验图例" >
                                <ItemTemplate>
                                     <%# (Eval("ChangeType").ToString() == "京内变更" && Eval("PostTypeID").ToString() == "1")?Eval("SheBaoCheck") ==DBNull.Value?"<div class=\"flag_blue pointer\" onclick='javascript:layer.alert(\"尚未比对!（当日提交数据后,次日进行比对）\")'>&nbsp;</div>": Eval("SheBaoCheck").ToString()=="1"?"":string.Format("<div class=\"flag_blue pointer\" onclick='javascript:SetIfrmSrc(\"../PersonnelFile/RYSBView.aspx?o1={0}&o2={1}&o3={2}\");'>&nbsp;</div>",  Eval("NewWorkerCertificateCode"), Eval("NewUnitCode"), Eval("ApplyDate")):""%>
                                    <%#  Eval("IfUpdatePhoto") != DBNull.Value && Eval("IfUpdatePhoto").ToString()=="1" ?"<div class=\"changeImg pointer\" onclick='javascript:layer.alert(\"变更了照片。\")'> &nbsp;</div>":""  %>
                                     <%#  (Eval("ChangeRemark") != System.DBNull.Value && Eval("ChangeRemark").ToString() == "申请强制执行") ? "<div class=\"enforce pointer\"  style=\"color: Red\" onclick='javascript:layer.alert(\"申请了强制执行。\")'>强</div>" : ""%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false"/>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn UniqueName="DealWay" DataField="DealWay" HeaderText="处理方式">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="NoticeCode" HeaderText="受理通知单" SortExpression="NoticeCode">
                                <ItemTemplate>
                                    <%# Eval("NoticeCode") == System.DBNull.Value ? "&nbsp;" : "<a href='CertifChangeCheckConfirm.aspx?o=" + Eval("PostTypeID").ToString() + "&code=" + Server.UrlEncode(Eval("NoticeCode").ToString()) + "'><nobr style='color:blue;text-decoration: underline;'>" + Eval("NoticeCode").ToString() + "</nobr></a>"%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn UniqueName="Output_NoticeCode" DataField="NoticeCode" HeaderText="受理通知单"
                                Visible="false">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="GetMan" DataField="GetMan" HeaderText="受理人">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="GetDate" DataField="GetDate" HeaderText="受理时间"
                                HtmlEncode="false" DataFormatString="{0:yyyy.MM.dd}" AllowSorting="false">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="CertificateChangeID" HeaderText="申请单">
                                <ItemTemplate>                                  
                                     <%# (Eval("CREATEPERSONID").ToString() =="0")?string.Format("<span class=\"link_edit\" onclick='javascript:SetIfrmSrc(\"CompanyNameChange.aspx?a={0}\")';><nobr>申请单</nobr></span>" , Page.Server.UrlEncode(Utility.Cryptography.Encrypt(Eval("CertificateChangeID").ToString())))
                                            :string.Format("<span class=\"link_edit\" onclick='javascript:SetIfrmSrc(\"Application.aspx?o={0}\")';><nobr>申请单</nobr></span>" , Page.Server.UrlEncode(Utility.Cryptography.Encrypt(Eval("CertificateChangeID").ToString())))%>
                                </ItemTemplate>
                               
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="CertificateCode" HeaderText="证书编号" SortExpression="CertificateCode">
                                <ItemTemplate>
                                    <span class="link_edit" onclick='javascript:SetIfrmSrc("../PersonnelFile/CertificateInfo.aspx?o=<%# Page.Server.UrlEncode(Utility.Cryptography.Encrypt(Eval("CertificateID").ToString())) %>");'>
                                        <nobr><%# Eval("CertificateCode").ToString()%></nobr>
                                    </span>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" Font-Bold="true" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn UniqueName="Output_CertificateCode" DataField="CertificateCode"
                                HeaderText="证书编号" Visible="false">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <%--<telerik:GridTemplateColumn UniqueName="Faceimage" HeaderText="换照片">
                                <ItemTemplate>
                                    <%#  Eval("IfUpdatePhoto") != DBNull.Value && Eval("IfUpdatePhoto").ToString()=="1" ?"√":""  %>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center"  Wrap="false" />
                            </telerik:GridTemplateColumn>--%>
                            <telerik:GridBoundColumn UniqueName="NewWorkerName" DataField="NewWorkerName" HeaderText="姓名">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="NewWorkerCertificateCode" DataField="NewWorkerCertificateCode"
                                HeaderText="证件号码">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="NewUnitName" DataField="NewUnitName" HeaderText="新单位名称">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>                        
                            <telerik:GridBoundColumn UniqueName="ApplyMan" DataField="ApplyMan" HeaderText="申请人">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                              <telerik:GridBoundColumn UniqueName="ApplyDate" DataField="ApplyDate" HeaderText="申请日期"
                                HtmlEncode="false" DataFormatString="{0:yyyy.MM.dd}">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>                            
                             <telerik:GridTemplateColumn UniqueName="OldUnitCheckTime" HeaderText="单位确认日期" SortExpression="NewUnitCheckTime" >
                                <ItemTemplate>
                                    <%# (Eval("UnitCode")== Eval("NewUnitCode"))? 
                                    (Eval("OldUnitCheckTime")==DBNull.Value?"": Convert.ToDateTime(Eval("OldUnitCheckTime")).ToString("yyyy.MM.dd"))
                                    : (Eval("OldUnitCheckTime")!=DBNull.Value && Eval("NewUnitCheckTime")!=DBNull.Value && Convert.ToDateTime(Eval("NewUnitCheckTime"))>Convert.ToDateTime(Eval("OldUnitCheckTime"))?
                                    Convert.ToDateTime(Eval("NewUnitCheckTime")).ToString("yyyy.MM.dd"):
                                    ((Eval("OldUnitCheckTime")!=DBNull.Value?Convert.ToDateTime(Eval("OldUnitCheckTime")).ToString("yyyy.MM.dd"):
                                    ((Eval("NewUnitCheckTime")!=DBNull.Value?Convert.ToDateTime(Eval("NewUnitCheckTime")).ToString("yyyy.MM.dd"):"")
                                    )))
                                    )%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" Font-Bold="true" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false"  />
                            </telerik:GridTemplateColumn>                          
                        </Columns>
                        <HeaderStyle Font-Bold="True" />
                        <PagerTemplate>
                            <uc2:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                        </PagerTemplate>
                    </MasterTableView>
                    <SortingSettings SortToolTip="单击进行排序"></SortingSettings>
                    <StatusBarSettings LoadingText="正在读取数据" ReadyText="完成" />
                </telerik:radgrid>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="DataAccess.CertificateChangeDAL"
                    DataObjectTypeName="Model.CertificateChangeOB" SelectMethod="GetList" EnablePaging="true"
                    SelectCountMethod="SelectCount" MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex"
                    SortParameterName="orderBy">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                            DefaultValue="and Status='已申请'" ConvertEmptyStringToNull="false" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </div>
            <br />
            <div id="DivOutput" runat="server" visible="false" style="display: inline; width: 100%">
                提示：请根据“受理人”、“受理时间”等查询条件查询后再导出，否则导出所有数据耗时较长。
                        <asp:Button ID="ButtonExportExcel" runat="server" Text="导出列表" CssClass="bt_large"
                            OnClick="ButtonExportExcel_Click" />
            </div>
            <div id="DivDealWay" runat="server" visible="false" style="float: left; display: inline; width: 100%">
                <span id="P_CheckConfirmKeyWord" runat="server" visible="false" style="float: left; color: Red; clear: both; text-align: left; width: 100%; padding-left: 20px; line-height: 20px;">注意：您只有审查决定变更基本字段（“姓名、企业名称、组织机构代码”）权限，对其它字段变更无效。<br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;对特殊字段“证件号码、性别、年龄”的变更只有注册中心人员审查决定才能生效。<br />
                </span><span style="float: left; clear: both; text-align: left; width: 100%; padding-left: 20px; line-height: 20px; color: #0A246A;">提示：1、证书处理方式选择“证书信息修改”时，点击审查通过将直接办结勾选数据；<br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2、审查通过类型为“重新制作证书”和“收回原证书”的将进入受理通知书打印界面；<br />
                </span>
                <hr />
                <div style="width: 100px; float: left; line-height: 30px; text-align: right;">
                    证书处理方式：
                </div>
                <div style="float: left;">
                    <asp:RadioButtonList ID="rdZSCLFS" runat="server" RepeatDirection="Horizontal" ValidationGroup="rdCLFS">
                        <asp:ListItem Value="证书信息修改">证书信息修改</asp:ListItem>
                        <asp:ListItem Value="重新制作证书">重新制作证书</asp:ListItem>
                        <asp:ListItem Value="收回原证书">收回原证书</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <div style="float: left; padding-left: 15px; line-height: 30px; clear: right;">
                    <asp:Button ID="ButtonConfirm" runat="server" Text="审查通过" CssClass="bt_large" OnClick="ButtonConfirm_Click" />
                    &nbsp;&nbsp;<asp:Button ID="ButtonDelete" runat="server" Text="删除无效申请" CssClass="bt_large"
                        OnClick="ButtonDelete_Click" />
                </div>
            </div>
            <br />
            <div style="width: 90%; margin: 5px auto; text-align: center; padding-top: 8px;">
                <asp:Button ID="btnPrint" runat="server" CssClass="bt_maxlarge" Text="打印受理通知书" OnClick="btnPrint_Click"
                    Visible="false" />
                &nbsp;&nbsp;<asp:Button ID="ButtonReturn" runat="server" Text="返 回" CssClass="button"
                    Visible="false" OnClick="ButtonReturn_Click" />
            </div>
            <br />
            <uc4:IframeView ID="IframeView" runat="server" />
        </div>
    </div>
</asp:Content>
