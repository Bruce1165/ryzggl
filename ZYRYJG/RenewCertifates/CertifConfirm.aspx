<%@ Page Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="CertifConfirm.aspx.cs" Inherits="ZYRYJG.RenewCertifates.CertifConfirm" %>

<%@ Register Src="../PostSelect.ascx" TagName="PostSelect" TagPrefix="uc1" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<%@ Register Src="../CheckAll.ascx" TagName="CheckAll" TagPrefix="uc3" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc4" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../css/timelimit.css?v=1.001" rel="stylesheet" type="text/css" />
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript" src="../Scripts/timelimit.js?v=1.003"></script>
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
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" EnableAJAX="true">
        <ClientEvents OnRequestStart="onRequestStart" />
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ButtonExportToExcel">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ButtonExportToExcel" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
             <telerik:AjaxSetting AjaxControlID="divMain">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divMain" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <%--<telerik:AjaxSetting AjaxControlID="ButtonSearch">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGridAccept" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="TableFirstCheck" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="PostSelect1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="PostSelect1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="TableFirstCheck">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="TableFirstCheck" />
                    <telerik:AjaxUpdatedControl ControlID="RadGridAccept" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="ButtonSearch" />
                </UpdatedControls>
            </telerik:AjaxSetting>--%>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Windows7" />
    <telerik:RadWindowManager runat="server" RestrictionZoneID="offsetElement" ID="RadWindowManager1"
        EnableShadow="true" EnableEmbeddedScripts="true" Skin="Windows7" VisibleStatusbar="false">
    </telerik:RadWindowManager>
    <div class="div_out">
        <div class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt; 从业人员证书管理 &gt;&gt;
                <asp:Label ID="LabelCheck" runat="server" Text="Label"></asp:Label>
                &gt;&gt; <strong>续期决定告知</strong>
            </div>
        </div>
        <div class="content" id="divMain" runat="server">
            <table class="bar_cx">
                    
                 
                    <tr>
                        <td align="right" nowrap="nowrap">姓名：
                        </td>
                        <td align="left">
                            <telerik:RadTextBox ID="RadTextBoxWorkerName" runat="server" Width="200px" Skin="Default" Style="float: left;" >
                            </telerik:RadTextBox>
                              <span style="float: left; padding: 0 4px 0 20px; line-height: 22px;">证件号码：</span> 
                            <telerik:RadTextBox ID="RadTextBoxWorkerCertificateCode" runat="server" Width="200px" Skin="Default" Style="float: left;" > </telerik:RadTextBox>
                        </td>
                        <td align="right" nowrap="nowrap">企业名称：
                        </td>
                        <td align="left">
                            <telerik:RadTextBox ID="RadTextBoxUnitName" runat="server" Width="200px" Skin="Default" Style="float: left;" >
                            </telerik:RadTextBox>
                             <span style="float: left; padding: 0 4px 0 20px; line-height: 22px;">组织机构代码：</span>   
                            <telerik:RadTextBox ID="RadTextBoxUnitCode" runat="server" Width="200px" Skin="Default" Style="float: left;"></telerik:RadTextBox>
                        </td>
                       
                    </tr>
                <tr>
                        <td align="right" nowrap="nowrap">证书编号：
                        </td>
                        <td nowrap="nowrap" align="left"> 
                             <telerik:RadTextBox ID="RadTextBoxCertificateCode" runat="server" Width="200px" Skin="Default"
                                onkeydown="ButtonSearchClick(event); " Style="float: left;">
                            </telerik:RadTextBox>
                             <span style="float: left; padding: 0 4px 0 20px; line-height: 22px;">发证年度：</span>
                             <telerik:RadNumericTextBox ID="RadNumericTextBoxConferData" runat="server" Width="100px" Style="float: left;" MaxLength="4" MaxValue="2050" MinValue="1950" DataType="Number" NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" ShowSpinButtons="true">
                            </telerik:RadNumericTextBox>
                        </td>
                        <td align="right" nowrap="nowrap">有效期截止：
                        </td>
                        <td align="left">
                            <span style="float: left; padding: 0 4px; line-height: 22px;">从</span>
                             <telerik:RadDatePicker ID="RadDatePickerFrom" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                MinDate="01/01/1900" runat="server" Width="200px" Style="float: left;" />
                           <span style="float: left; padding: 0 4px; line-height: 22px;">至</span>
                            <telerik:RadDatePicker ID="RadDatePickerEnd" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                MinDate="01/01/1900" runat="server" Width="200px" Style="float: left;" />
                        </td>
                    </tr>
                <tr>
                    <td align="right" nowrap="nowrap">审核批号：
                    </td>
                    <td align="left">
                        <telerik:RadTextBox ID="RadTextBoxGetCode" runat="server" Width="200px" Skin="Default" MaxLength="20" Style="float: left;"
                            >
                        </telerik:RadTextBox>
                     <span style="float: left; padding: 0 4px 0 20px; line-height: 22px;">审核日期：</span>   
                   
                        <telerik:RadDatePicker ID="RadDatePickerCheckDateStart" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                            Width="120px"  Style="float: left;"/>
                         <span style="float: left; padding: 0 4px; line-height: 22px;">至</span>
                        <telerik:RadDatePicker ID="RadDatePickerCheckDateEnd" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                            Width="120px" Style="float: left;" />
                    </td>
             
                    <td align="right" nowrap="nowrap">决定批号：
                    </td>
                    <td align="left">
                        <telerik:RadTextBox ID="RadTextBoxConfirmCode" runat="server" Width="200px" Skin="Default" MaxLength="20" Style="float: left;">
                        </telerik:RadTextBox>
                    <span style="float: left; padding: 0 4px 0 20px; line-height: 22px;">决定日期：</span> 
                    
                        <telerik:RadDatePicker ID="RadDatePickerConfirmDateStart" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                            Width="120px"  Style="float: left;"/>
                         <span style="float: left; padding: 0 4px; line-height: 22px;">至</span>
                        <telerik:RadDatePicker ID="RadDatePickerConfirmDateEnd" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                            Width="120px" Style="float: left;" />
                    </td>
                </tr>
                <tr>
                     <td width="8%" align="right" nowrap="nowrap">岗位工种：
                    </td>
                    <td align="left" width="42%">
                        <uc1:PostSelect ID="PostSelect1" runat="server" />
                    </td>      
                    <td align="right" nowrap="nowrap">初审单位：
                    </td>
                    <td align="left">
                        <telerik:RadTextBox ID="RadTextBoxFirstCheckUnitName" runat="server" Width="97%"
                            Skin="Default"  ToolTip="只输入关键字即可模糊查询">
                        </telerik:RadTextBox>
                    </td>
                   
                </tr>
                <tr>
                    <td align="right" nowrap="nowrap">决定状态：
                    </td>
                    <td align="left" >
                        <asp:RadioButtonList ID="RadioButtonListStatus" runat="server" RepeatDirection="Horizontal"
                            AutoPostBack="false" Style="float: left;width:auto;">
                             <asp:ListItem Value="">全部</asp:ListItem>
                            <asp:ListItem Value="未决定" Selected="True">未决定</asp:ListItem>
                             <asp:ListItem Value="退回修改">退回修改</asp:ListItem>
                            <asp:ListItem Value="决定通过">决定通过</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                     <td align="right" nowrap="nowrap">继续教育形式：
                        </td>
                        <td align="left">
                            <asp:RadioButtonList ID="RadioButtonListjxjyway" runat="server" RepeatDirection="Horizontal" Width="400px"
                                AutoPostBack="false">
                                <asp:ListItem Value="" Selected="True">全部</asp:ListItem>
                                <asp:ListItem Value="1">面授</asp:ListItem>
                                <asp:ListItem Value="2">网络教育</asp:ListItem>
                                <asp:ListItem Value="3">面授+网络教育</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                </tr>
                  <tr>
                              
                   
                    <td colspan="4" align="center">
                        <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />&nbsp;&nbsp;<asp:Button ID="ButtonExportToExcel" CssClass="bt_maxlarge" Text="导出查询结果列表" OnClick="ButtonExportToExcel_Click"
                    runat="server"></asp:Button><span style="color: #999">（提示：请首先根据特定的查询条件过滤后再导出查询结果集。） </span>
                    </td>
                </tr>
            </table>
            <div class="table_cx">
                <img src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                证书续期决定列表<span style="text-align:right; right:20px;cursor:pointer;color:blue;" onclick="showCheckListHelp()">《审核时限说明》</span>
            </div>
            <div style="width: 98%; margin: 0 auto;">
                <telerik:RadGrid ID="RadGridAccept" runat="server"
                    GridLines="None" AllowPaging="True" PageSize="10" AllowSorting="True" AutoGenerateColumns="False"  PagerStyle-AlwaysVisible="true"
                    SortingSettings-SortToolTip="单击进行排序" Width="100%" Skin="Blue" EnableAjaxSkinRendering="false"
                    EnableEmbeddedSkins="false" OnExcelExportCellFormatting="RadGrid1_ExcelExportCellFormatting"
                    OnPageIndexChanged="RadGridAccept_PageIndexChanged" OnDataBound="RadGridAccept_DataBound">
                    <ClientSettings EnableRowHoverStyle="true">
                    </ClientSettings>
                    <MasterTableView NoMasterRecordsText="　没有可显示的记录"
                        DataKeyNames="CertificateContinueID">
                        <Columns>
                            <telerik:GridTemplateColumn UniqueName="TemplateColumn" HeaderText="Highlight <br/> ship name">
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" onclick='checkBoxClick(this.checked);' />
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <uc3:CheckAll ID="CheckAll1" runat="server" />
                                </HeaderTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridBoundColumn>
                              <telerik:GridTemplateColumn UniqueName="jxjyway" HeaderText="继续教育形式">
                                    <ItemTemplate>                                        
                                            <%# ZYRYJG.UIHelp.GetJxjyType( Eval("jxjyway")) %>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" Font-Bold="true" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false"  />
                                </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn UniqueName="CheckDate" DataField="CheckDate" HtmlEncode="false"
                                DataFormatString="{0:yyyy-MM-dd}" HeaderText="审核日期">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridBoundColumn>
                             <telerik:GridTemplateColumn UniqueName="CheckLimit" HeaderText="审核时限(工作日)" >
                                <ItemTemplate>
                                     <%# formatCheckList(Eval("NewUnitCheckTime"),Eval("ReportDate"),Eval("Status"))%>                                
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn UniqueName="ConfirmDate" DataField="ConfirmDate" HtmlEncode="false"
                                DataFormatString="{0:yyyy-MM-dd}" HeaderText="决定日期">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="PostName" DataField="PostName" HeaderText="岗位工种">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="CertificateChangeID" HeaderText="证书编号">
                                <ItemTemplate>
                                    <span class="link_edit" onclick='javascript:SetIfrmSrc("ApplyDetail.aspx?o=<%# Page.Server.UrlEncode(Utility.Cryptography.Encrypt(Eval("CertificateID").ToString()))%>&o2=<%# Page.Server.UrlEncode(Utility.Cryptography.Encrypt(Eval("CertificateContinueID").ToString())) %>");'>
                                        <%# Eval("CertificateCode")%></span>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" Font-Bold="true" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                            </telerik:GridTemplateColumn> 
                            <telerik:GridBoundColumn UniqueName="ValidEndDate" DataField="ValidEndDate" HtmlEncode="false"
                                DataFormatString="{0:yyyy-MM-dd}" HeaderText="有效期至">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="Faceimage" HeaderText="照片">
                                <ItemTemplate>
                                    <div style="position: relative;">
                                        <asp:Image ID="Image1" runat="server" Width="14" Height="18" ImageUrl='<%# ZYRYJG.UIHelp.ShowFaceImeByFacePhotoAndWorkerCertificateCode(string.Format("{0}",Eval("FACEPHOTO")),string.Format("{0}",Eval("WorkerCertificateCode"))) %>'
                                            onmouseover="setImgSize(this,100,140,'absolute',event);" onmouseout="setImgSize(this,14,18,'relative',event);"></asp:Image>
                                    </div>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn UniqueName="WorkerName" DataField="WorkerName" HeaderText="姓名">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="WorkerCertificateCode" DataField="WorkerCertificateCode"
                                HeaderText="证件号码">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="UnitName" HeaderText="企业名称">
                                <ItemTemplate>
                                    <%# Eval("NEWUNITNAME")!= DBNull.Value && Eval("NEWUNITNAME").ToString() !=Eval("UNITNAME").ToString()?"<span style='color:red'>新单位：</span>"+Eval("NEWUNITNAME"):Eval("UNITNAME")%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridTemplateColumn>            
                            <telerik:GridBoundColumn UniqueName="CONFIRMCODE" DataField="CONFIRMCODE" HeaderText="决定批号">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
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
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetList"
                    TypeName="DataAccess.CertificateContinueDAL" SelectCountMethod="SelectCount"
                    EnablePaging="true" MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex"
                    SortParameterName="orderBy">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                            DefaultValue="" ConvertEmptyStringToNull="false" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </div>
            <div id="TableFirstCheck" runat="server"  style="width: 99%; padding-top: 20px; text-align: center; clear: both; margin: auto">
                <table id="TableEdit" runat="server"  width="100%" border="0" cellpadding="5" cellspacing="1" class="table" style="text-align: center; margin: 10px auto">
                    <tr class="GridLightBK">
                        <td colspan="2" class="barTitle">批量决定操作（请先勾选要决定操的记录）</td>
                    </tr>
                    <tr class="GridLightBK">
                        <td width="20%" align="right">处理结果：</td>
                        <td width="80%" align="left">
                            <asp:RadioButtonList ID="RadioButtonListFirstCheckResult" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="RadioButtonListFirstCheckResult_SelectedIndexChanged">
                                 <asp:ListItem Text="通过" Value="通过" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="不通过" Value="不通过"></asp:ListItem>
                            </asp:RadioButtonList>
                        
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td width="20%" align="right">处理意见：</td>
                        <td width="80%" align="left">

                            <asp:TextBox ID="TextBoxFirstCheckRemark" runat="server" TextMode="MultiLine" Rows="3" Width="99%" MaxLength="100" Text="决定通过"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td colspan="2" align="center">
                            <asp:Button ID="ButtonFirstCheck" runat="server" CssClass="bt_large" Text="确 定" OnClick="ButtonFirstCheck_Click" />&nbsp;&nbsp;
                                         
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <br />
    </div>
    <uc4:IframeView ID="IframeView" runat="server" />
</asp:Content>
