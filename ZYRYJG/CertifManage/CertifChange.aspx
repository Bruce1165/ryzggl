<%@ Page Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="CertifChange.aspx.cs" Inherits="ZYRYJG.CertifManage.CertifChange" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<%@ Register Src="~/PostSelect.ascx" TagName="PostSelect" TagPrefix="uc1" %>
<%@ Register Src="../CheckAll.ascx" TagName="CheckAll" TagPrefix="uc3" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
        .rgCaption {
            background-color: #4D6082;
            color: #efefef;
            line-height: 30px;
        }

        #ctl00_ContentPlaceHolder1_RadAsyncUploadFacePhoto ul li div {
            width: 215px !important;
        }
    </style>
    <telerik:RadWindowManager runat="server" RestrictionZoneID="offsetElement" ID="RadWindowManager1"
        EnableShadow="true" EnableEmbeddedScripts="true" Skin="Windows7" VisibleStatusbar="false">
    </telerik:RadWindowManager>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function onRequestStart(sender, args) {
                if (args.get_eventTarget().indexOf("ButtonOutPut") >= 0
                    //|| args.get_eventTarget().indexOf("ButtonOutPut2") >= 0
                || args.get_eventTarget().indexOf("ButtonExportToExcel") >= 0
                    //|| args.get_eventTarget().indexOf("ButtonPrint2") >= 0
                || args.get_eventTarget().indexOf("ButtonPrint") >= 0) {
                    args.set_enableAjax(false);
                }
            }
            function OnClientSelectedIndexChanged(sender, eventArgs) {
                var item = eventArgs.get_item();
                if (item.get_text() == "离京变更") {
                    alert('离京变更后证书在京视为无效，无法使用此证书在京办理其他业务。');
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
            <telerik:AjaxSetting AjaxControlID="divMain">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divMain" LoadingPanelID="RadAjaxLoadingPanel1" />
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
    <div class="div_out">
        <div class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt;从业人员证书管理 &gt;&gt;<strong><asp:Label ID="LabelApply" runat="server" Text="证书变更"></asp:Label></strong>
            </div>
        </div>

        <div id="divMain" runat="server" style="width: 100%">
            <div id="DivCanApplyList" runat="server" style="width: 98%; margin: 5px auto;">
                <div class="DivTitleOn" onclick="DivOnOff(this,'Td3',event);" title="折叠">
                    申报说明
                </div>
                <div class="DivContent" id="Td3">
                    1、办事流程:个人网上申请并上传扫描件-企业网上审核确认-住建委网上审批（咨询电话：010-89150138）。<br />
                    2、身份证样式：须为18位二代身份证（带X字母的必须使用英文大写）。<br />
                    3、组织机构代码：9位数字或大写字母组合,带“-”的去掉“-”，社会统一信用代码中的第9位至第17位为企业的组织机构代码。<br />
                    4、不再提供纸质证书补办业务，请企业或个人自行下载电子证书。<br />
                    5、个人身份信息有误的，请携带相关材料到北京市政务服务中心现场办理（咨询电话：010-89150138）。（不包含“专业技术管理人员”）<br />
                    6、根据有关规定，停止造价员、关键岗位专业技术管理人员考核、变更和续期等相关业务。（咨询电话：010-55598091）<br />
                    7、专业管理人员证书和技能人员证书均无续期要求。
                </div>
                <div class="content">
                    <table class="bar_cx">
                        <tr>
                            <td>
                                <table width="98%" border="0" align="center" cellspacing="5">
                                    <tr>
                                        <td align="right" nowrap="nowrap" width="8%">姓名：
                                        </td>
                                        <td align="left" width="35%">
                                            <telerik:RadTextBox ID="rdtxtWorkerName" runat="server" Width="97%" Skin="Default">
                                            </telerik:RadTextBox>
                                        </td>
                                        <td width="12%" align="right" nowrap="nowrap">证书编号：
                                        </td>
                                        <td width="45%" align="left">
                                            <telerik:RadTextBox ID="rdtxtCertificateCode" runat="server" Width="97%" Skin="Default">
                                            </telerik:RadTextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table id="tableSearchParms" runat="server" width="98%" border="0" align="center"
                                    cellspacing="1">
                                    <tr>
                                        <td align="right" nowrap="nowrap" width="8%">证件号码：
                                        </td>
                                        <td align="left" width="35%">
                                            <telerik:RadTextBox ID="rdtxtZJHM" runat="server" Width="97%" Skin="Default">
                                            </telerik:RadTextBox>
                                        </td>
                                        <td align="right" width="12%" nowrap="nowrap">岗位工种：
                                        </td>
                                        <td align="left" width="45%">
                                            <uc1:PostSelect ID="PostSelect1" runat="server" />

                                        </td>
                                    </tr>
                                    <tr id="TrUnitQuery" runat="server">
                                        <td align="right" nowrap="nowrap" width="8%">企业名称：
                                        </td>
                                        <td align="left" width="35%">
                                            <telerik:RadTextBox ID="rdtxtQYMC" runat="server" Width="97%" Skin="Default">
                                            </telerik:RadTextBox>
                                        </td>
                                        <td align="right" nowrap="nowrap" width="12%">组织机构代码：
                                        </td>
                                        <td align="left" width="45%">
                                            <telerik:RadTextBox ID="rdtxtUnitCode" runat="server" Width="97%" Skin="Default">
                                            </telerik:RadTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" nowrap="nowrap" width="8%">证书状态：
                                        </td>
                                        <td align="left" width="35%">
                                            <asp:RadioButtonList ID="rbl" runat="server" RepeatDirection="Horizontal" AutoPostBack="false">
                                                <asp:ListItem Value="0" Selected="True">未过期</asp:ListItem>
                                                <asp:ListItem Value="1">已过期（含注销、离京）</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        <td align="right" nowrap="nowrap" width="12%">有效期至：
                                        </td>
                                        <td align="left" width="45%">
                                            <telerik:RadDatePicker ID="txtValidStartDate" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                                Width="46%" Style="float: left;" />
                                            <div class="RadPicker">至</div>
                                            <telerik:RadDatePicker ID="txtValidEndtDate" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                                Width="46%" Style="float: left;" />
                                        </td>
                                    </tr>
                                </table>
                                <%-- <table width="98%" border="0" align="center" cellspacing="1">
                                    <tr>
                                        <td align="right" nowrap="nowrap" width="8%">申请状态：
                                        </td>
                                        <td align="left" width="35%">
                                            <asp:RadioButtonList ID="RadioButtonListStatus" runat="server" RepeatDirection="Horizontal"
                                                AutoPostBack="false">
                                                <asp:ListItem Value="0" Selected="True">未申请</asp:ListItem>
                                                <asp:ListItem Value="1">已申请（待审查）</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        <td align="right"></td>
                                        <td align="left"></td>
                                    </tr>
                                </table>--%>
                                <table width="98%" border="0" align="center" cellspacing="5">
                                    <tr>
                                        <td colspan="4" align="center">
                                            <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <div class="table_cx">
                        <img src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                        证书列表<asp:Label ID="LabelTip" runat="server" Text="" ForeColor="Red"></asp:Label>
                    </div>
                    <div style="width: 98%; margin: 0 auto;">
                        <telerik:RadGrid ID="RadGridCertificate" AutoGenerateColumns="False" PagerStyle-AlwaysVisible="true"
                            runat="server" AllowAutomaticDeletes="True" AllowAutomaticInserts="True" AllowAutomaticUpdates="True"
                            AllowPaging="True" PageSize="10" AllowSorting="True" SortingSettings-SortToolTip="单击进行排序"
                            Skin="Blue" EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false" Width="100%"
                            GridLines="None" OnItemCommand="RadGridCertificate_ItemCommand" OnPageIndexChanged="RadGridCertificate_PageIndexChanged"
                            OnDataBound="RadGridCertificate_DataBound">
                            <ClientSettings EnableRowHoverStyle="true">
                            </ClientSettings>
                            <MasterTableView AllowMultiColumnSorting="True" DataKeyNames="CertificateChangeID,CertificateID,CertificateCode,UnitCode,PostID"
                                NoMasterRecordsText="　没有可显示的记录">
                                <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                                <Columns>
                                    <telerik:GridTemplateColumn UniqueName="SelectAllColumn" HeaderText="Highlight <br/> ship name" Display="false">
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
                                    <telerik:GridBoundColumn UniqueName="PostName" DataField="PostName" HeaderText="岗位工种">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="CertificateCode" DataField="certificateCode"
                                        HeaderText="证书编号">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn UniqueName="ValidEndDate" HeaderText="有效期至">
                                        <ItemTemplate>
                                            <%# Convert.ToDateTime(Eval("ValidEndDate")).ToString("yyyy.MM.dd") == "2050.01.01" ? "当前有效证书" : Convert.ToDateTime(Eval("ValidEndDate")).ToString("yyyy.MM.dd")%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
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
                                    <telerik:GridTemplateColumn UniqueName="Faceimage" HeaderText="照片">
                                        <ItemTemplate>
                                            <div style="position: relative;">
                                                <asp:Image ID="Image1" runat="server" Width="14" Height="18" ImageUrl='<%#  ZYRYJG.UIHelp.ShowFaceImeByFacePhotoAndWorkerCertificateCode(Eval("FACEPHOTO") == DBNull.Value ? "" : Eval("FACEPHOTO").ToString(), Eval("WorkerCertificateCode") == DBNull.Value ? "111" : Eval("WorkerCertificateCode").ToString())%>'
                                                    onmouseover="setImgSize(this,100,140,'absolute',event);" onmouseout="setImgSize(this,14,18,'relative',event);"></asp:Image>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn UniqueName="UnitName" DataField="UnitName" HeaderText="原企业名称">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="UnitCode" DataField="UnitCode" HeaderText="原机构代码">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="ApplyStatus" DataField="ApplyStatus" HeaderText="状态">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn UniqueName="Apply" HeaderText="申请">
                                        <ItemTemplate>
                                            <%# (Eval("CertificateChangeID").ToString() == "0") ? string.Format("<span class=\"link_edit\" onclick='javascript:SetIfrmSrc(\"Application.aspx?c={0}&t={1}\")';><nobr>申请</nobr></span>", Page.Server.UrlEncode(Utility.Cryptography.Encrypt(Eval("CertificateID").ToString())), Utility.Cryptography.Encrypt(ChangeType))
                                            : (Eval("ApplyManID").ToString() == "0") ? string.Format("<span class=\"link_edit\" onclick='javascript:SetIfrmSrc(\"CompanyNameChange.aspx?a={0}\")';><nobr>详细</nobr></span>", Page.Server.UrlEncode(Utility.Cryptography.Encrypt(Eval("CertificateChangeID").ToString())))
                                            : string.Format("<span class=\"link_edit\" onclick='javascript:SetIfrmSrc(\"Application.aspx?o={0}\")';><nobr>详细</nobr></span>", Page.Server.UrlEncode(Utility.Cryptography.Encrypt(Eval("CertificateChangeID").ToString())))%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridTemplateColumn UniqueName="sb" HeaderText="社保">
                                        <ItemTemplate>
                                            <%# Eval("CertificateChangeID").ToString() == "0" || Eval("PostTypeID").ToString() != "1" ? "" : string.Format("<span class=\"link_edit\" onclick='javascript:SetIfrmSrc(\"../PersonnelFile/RYSBView.aspx?o1={0}&o2={1}&o3={2}\");'><nobr>{3}</nobr></span>", Eval("NewWorkerCertificateCode").ToString(), Eval("NewUnitCode").ToString(), Convert.ToDateTime(Eval("ApplyDate")).ToString(), Eval("SheBaoCheck") == DBNull.Value ? "查看" : (Eval("SheBaoCheck").ToString() == "1") ? "符合" : "不符合")%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                                    </telerik:GridTemplateColumn>
                                </Columns>
                                <HeaderStyle Font-Bold="True" />
                                <PagerTemplate>
                                    <uc2:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                                </PagerTemplate>
                            </MasterTableView>
                        </telerik:RadGrid>
                        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="DataAccess.CertificateChangeDAL"
                            DataObjectTypeName="Model.CertificateOB" SelectMethod="GetListView" EnablePaging="true"
                            SelectCountMethod="SelectCountView" MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex"
                            SortParameterName="orderBy">
                            <SelectParameters>
                                <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                                    DefaultValue="" ConvertEmptyStringToNull="false" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </div>
                    <br />
                    <div style="width: 98%; margin: 0 auto;">
                        <asp:Button ID="ButtonEdit" runat="server" Text="同单位批量申请" CssClass="bt_maxlarge" OnClick="ButtonEdit_Click"
                            ToolTip="只适用于同一单位名称批量变更" />&nbsp;
                    <asp:Button ID="ButtonExit" Text="批量取消申请" runat="server" CssClass="bt_large" OnClick="ButtonExit_Click" Visible="false"
                        OnClientClick="javascript:if(confirm('是否真的要取消变更申请？')==false) return false;"></asp:Button>&nbsp;
                    </div>
                </div>
                <br />
            </div>
            <div id="DivApplyedList" runat="server" visible="false" class="table_border" style="width: 98%; margin: 5px auto;">
                <p class="table_cx">
                    <img alt="" src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                    证书变更申请列表
                </p>
                <div style="width: 98%; margin: 0 auto;">
                    <telerik:RadGrid ID="RadGridApplyed" DataSourceID="ObjectDataSourceApplyed" AllowPaging="True"
                        SortingSettings-SortToolTip="单击进行排序" runat="server" AutoGenerateColumns="False"
                        AllowSorting="True" PageSize="10" GridLines="None" CellPadding="0" Width="100%"
                        Skin="Blue" EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false" OnExcelExportCellFormatting="RadGridApplyed_ExcelExportCellFormatting">
                        <MasterTableView DataKeyNames="CertificateChangeID,ChangeType" NoMasterRecordsText="　没有可显示的记录">
                            <Columns>
                                <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Width="36px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="ChangeType" DataField="ChangeType" HeaderText="变更类型">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="12%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="岗位类别" UniqueName="PostTypeName" DataField="PostTypeName">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="12%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="岗位工种" UniqueName="PostName" DataField="PostName">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="12%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="CertificateCode" DataField="CertificateCode"
                                    HeaderText="证书编号">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="12%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn HeaderText="有效期">
                                    <ItemTemplate>
                                        <nobr><%# Convert.ToDateTime(Eval("ValidStartDate")).ToString("yyyy.MM.dd") + "-"%></nobr>
                                        <nobr><%#Convert.ToDateTime(Eval("ValidEndDate")).ToString("yyyy.MM.dd")%></nobr>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn UniqueName="WorkerName" DataField="WorkerName" HeaderText="姓名">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="12%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="WorkerCertificateCode" DataField="WorkerCertificateCode"
                                    HeaderText="证件号码">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="NewUnitName" DataField="NewUnitName" HeaderText="企业名称">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="20%" />
                                </telerik:GridBoundColumn>
                                <%--<telerik:GridTemplateColumn UniqueName="sb" HeaderText="社保">
                                <ItemTemplate>
                                    <%# string.Format("<span class=\"link_edit\" onclick='javascript:SetIfrmSrc(\"../PersonnelFile/RYSBView.aspx?o1={0}&o2={1}&o3={2}\");'><nobr>社保</nobr></span>", Eval("NewWorkerCertificateCode").ToString(), Eval("NewUnitCode").ToString(), Convert.ToDateTime(Eval("ApplyDate")).ToString())%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" Font-Bold="true" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                            </telerik:GridTemplateColumn>--%>
                                <telerik:GridTemplateColumn UniqueName="sb" HeaderText="社保">
                                    <ItemTemplate> 
                                        <%# Eval("CertificateChangeID").ToString() == "0" || Eval("PostTypeID").ToString() != "1" ? "" : string.Format("<span class=\"link_edit\" onclick='javascript:SetIfrmSrc(\"../PersonnelFile/RYSBView.aspx?o1={0}&o2={1}&o3={2}\");'><nobr>{3}</nobr></span>", Eval("NewWorkerCertificateCode").ToString(), Eval("NewUnitCode").ToString(), Convert.ToDateTime(Eval("ApplyDate")).ToString(), Eval("SheBaoCheck") == DBNull.Value ? "查看" : (Eval("SheBaoCheck").ToString() == "1") ? "符合" : "不符合")%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" Font-Bold="true" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                                </telerik:GridTemplateColumn>
                            </Columns>
                            <HeaderStyle Font-Bold="True" />
                            <PagerTemplate>
                                <uc2:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                            </PagerTemplate>
                        </MasterTableView>
                    </telerik:RadGrid>
                    <asp:ObjectDataSource ID="ObjectDataSourceApplyed" runat="server" TypeName="DataAccess.CertificateChangeDAL"
                        DataObjectTypeName="Model.CertificateChangeOB" SelectMethod="GetList" EnablePaging="true"
                        SelectCountMethod="SelectCount" MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex"
                        SortParameterName="orderBy">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                                DefaultValue="and Status='已申请'" ConvertEmptyStringToNull="false" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </div>
                <div style="width: 98%; margin: 8px auto; text-align: center;">
                    <asp:Button ID="ButtonOutPut" runat="server"
                            Text="批量导出申请表" ToolTip="导出成Word文件" CssClass="bt_maxlarge" OnClick="ButtonOutPut_Click"
                            OnClientClick="javascript:if(confirm('导出打印申请表内容不允许擅自修改，否则后果自负！是否真的导出？')==false) return false;" />&nbsp;
                <asp:Button ID="ButtonExportToExcel" CssClass="bt_maxlarge" Text="导出申请汇总表" ToolTip="导出成Excel列表"
                    OnClick="ButtonExportToExcel_Click" runat="server"></asp:Button>&nbsp;
                <asp:Button ID="ButtonRtn" CssClass="button" Text="返 回" OnClick="ButtonRtn_Click"
                    runat="server"></asp:Button>
                </div>
            </div>
            <div id="DivEdit" runat="server" visible="false" class="table_border" style="width: 98%; margin: 5px auto;">
                <div class="DivTitleOn" onclick="DivOnOff(this,'Div1',event);" title="折叠">
                    填报格式说明
                </div>
                <div class="DivContent" id="Div1">
                    1、组织机构代码：9位数字或大写字母组合（不带“-”横杠）；不知道的请登录 <a title="组织机构代码查询" href="https://www.cods.org.cn"
                        target="_blank" style="color: Blue; text-decoration: underline;">https://www.cods.org.cn</a>
                    网站，在“信息核查”栏目中查询；<br />
                </div>
                <p class="jbxxbt">
                    填写变更申请
                </p>
                <table cellpadding="5" cellspacing="1" border="0" width="96%" class="table" align="center">
                    <tr class="GridLightBK">
                        <td width="11%" align="right" nowrap="nowrap">
                            <span style="color: Red">*</span>变更类型：
                        </td>
                        <td width="39%" align="left">
                            <telerik:RadComboBox ID="RadTextBoxChangeType" runat="server" Width="97%" Skin="Default"
                                OnClientSelectedIndexChanged="OnClientSelectedIndexChanged">
                            </telerik:RadComboBox>
                        </td>
                        <td width="11%" align="right" nowrap="nowrap">申请日期：
                        </td>
                        <td>
                            <asp:Label ID="lblApplyDate" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td colspan="2" align="center">变更前
                        </td>
                        <td colspan="2" align="center">变更后
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td align="right" nowrap="nowrap">原单位名称：
                        </td>
                        <td>
                            <telerik:RadTextBox ID="RadTextBoxUnitName" runat="server" Width="95%" Skin="Default"
                                ReadOnly="true">
                            </telerik:RadTextBox>
                        </td>
                        <td align="right" nowrap="nowrap">
                            <span style="color: Red">*</span>现单位名称：
                        </td>
                        <td>
                            <telerik:RadTextBox ID="RadTextBoxNewUnitName" runat="server" Width="95%" Skin="Default">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td align="right" nowrap="nowrap">原单位组织机构代码：
                        </td>
                        <td>
                            <telerik:RadTextBox ID="RadTextBoxUnitCode" runat="server" Width="95%" Skin="Default"
                                ReadOnly="true">
                            </telerik:RadTextBox>
                        </td>
                        <td align="right">
                            <span style="color: Red">*</span>现单位组织机构代码：
                        </td>
                        <td>
                            <telerik:RadTextBox ID="RadTextBoxNewUnitCode" runat="server" Width="95%" Skin="Default"
                                MaxLength="9">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td align="right">联系方式：
                        </td>
                        <td>
                            <telerik:RadTextBox ID="RadTextBoxLinkWay" runat="server" Width="97%" Skin="Default">
                            </telerik:RadTextBox>
                        </td>
                        <td>&nbsp;
                        </td>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td align="right">原单位意见：
                        </td>
                        <td colspan="3">
                            <telerik:RadTextBox ID="RadTextBoxOldUnitAdvise" runat="server" Width="98%" Skin="Default"
                                TextMode="MultiLine" Height="60px">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td align="right">现单位意见：
                        </td>
                        <td colspan="3">
                            <telerik:RadTextBox ID="RadTextBoxNewUnitAdvise" runat="server" Width="98%" Skin="Default"
                                TextMode="MultiLine" Height="60px">
                            </telerik:RadTextBox>
                        </td>
                    </tr>

                </table>
                <div style="width: 96%; margin: 20px auto;" runat="server" id="divNoPhoto">
                    <table cellpadding="5" cellspacing="1" border="0" width="100%" class="table" align="center" style="background-color: #EFEFFE; font-size: 12px;">
                        <tr>
                            <td align="left">
                                <span id="SpanTip" runat="server" style="font-size: 12px;">批量上传照片：（格式要求：一寸jpg格式图片，最大为50K，宽高102
                                        X 140像素）</span> &nbsp;&nbsp;&nbsp;&nbsp;辅助工具下载：<a href="../Images/1寸照片生成器.exe" style="text-decoration: underline; color: Blue;"><img alt="" src="../Images/Soft_common.gif" style="border-width: 0;" />
                                            1寸照片生成器.exe</a>
                                <div style="color: Red; padding-left: 100px">
                                    （注意：图片名称必须使用证件号码，如“210504198805200015.jpg”，如果电脑系统隐藏的扩展名.jpg，图片命名不要添加.jpg）
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">

                                <div style="float: left; clear: left; line-height: 28px; width: 130px; text-align: left;">
                                </div>
                                <div style="float: left; width: 315px;">
                                    <telerik:RadAsyncUpload runat="server" ID="RadAsyncUploadFacePhoto" MultipleFileSelection="Automatic" Width="315px"
                                        AutoAddFileInputs="true" OverwriteExistingFiles="true" AllowedFileExtensions="jpg"
                                        MaxFileInputsCount="1" MaxFileSize="51200" Culture="(Default)"
                                        Skin="Hot" EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false" ControlObjectsVisibility="None"
                                        InitialFileInputsCount="1" TemporaryFileExpiration="04:00:00" OnFileUploaded="RadAsyncUploadFacePhoto_FileUploaded"
                                        OnValidatingFile="RadAsyncUploadFacePhoto_ValidatingFile" HttpHandlerUrl="~/EXamManage/CustomHandler.ashx"
                                        Enabled="true" EnableFileInputSkinning="false">
                                        <Localization Delete="" Remove="" Select="选择文件" />
                                    </telerik:RadAsyncUpload>
                                </div>
                                <div style="float: left; padding-left: 13px;">
                                    <asp:Button ID="ButtonUploadImg" runat="server" Text="上 传" CssClass="button" OnClick="ButtonUploadImg_Click" />
                                </div>
                            </td>
                        </tr>
                    </table>
                    <telerik:RadGrid ID="RadGridNoPhto" AllowPaging="false"
                        runat="server" AutoGenerateColumns="False"
                        AllowSorting="false" GridLines="None" CellPadding="0" Width="100%"
                        Skin="Blue" EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false">
                        <MasterTableView DataKeyNames="WorkerCertificateCode" NoMasterRecordsText="　没有可显示的记录" Caption="以下人员缺少照片，请首先上传一寸照片">
                            <Columns>
                                <%-- <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Width="36px" />
                            </telerik:GridBoundColumn>--%>

                                <telerik:GridBoundColumn UniqueName="CertificateCode" DataField="CertificateCode"
                                    HeaderText="证书编号">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>

                                <telerik:GridBoundColumn UniqueName="WorkerName" DataField="WorkerName" HeaderText="姓名">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="WorkerCertificateCode" DataField="WorkerCertificateCode"
                                    HeaderText="证件号码">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                            </Columns>
                            <HeaderStyle Font-Bold="True" />

                        </MasterTableView>
                    </telerik:RadGrid>
                    <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" TypeName="DataAccess.CertificateChangeDAL"
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
                <asp:Button ID="ButtonApply" runat="server" Text="保 存" CssClass="button" OnClick="ButtonApply_Click" />
                <br />
                <br />
            </div>

        </div>
    </div>
    <uc4:IframeView ID="IframeView" runat="server" />
</asp:Content>
