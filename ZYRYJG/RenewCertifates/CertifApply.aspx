<%@ Page Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="CertifApply.aspx.cs" Inherits="ZYRYJG.RenewCertifates.CertifApply" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../PostSelect.ascx" TagName="PostSelect" TagPrefix="uc1" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<%@ Register Src="../CheckAll.ascx" TagName="CheckAll" TagPrefix="uc3" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
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
        <AjaxSettings>
            <%--  <telerik:AjaxSetting AjaxControlID="div_table">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="div_table" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
              <telerik:AjaxSetting AjaxControlID="IframeView">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="div_table" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>--%>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Windows7" />
    <telerik:RadWindowManager runat="server" RestrictionZoneID="offsetElement" ID="RadWindowManager1"
        ShowContentDuringLoad="false" VisibleStatusbar="false" ReloadOnShow="true" Skin="Windows7"
        EnableShadow="true" EnableEmbeddedScripts="true">
        <Windows>
            <telerik:RadWindow ID="RadWindow2" runat="server">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <div class="div_out">
        <div class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt;
                <asp:Label ID="LabelRoad" runat="server" Text="证书续期"></asp:Label>
                &gt;&gt;
                <asp:Label ID="LabelApply" runat="server" Text=""></asp:Label>&gt;&gt; <strong>申请</strong>
            </div>
        </div>
        <div class="table_border" runat="server" id="div_table">
            <div style="float: right; padding: 10px 30px 0px 25px;">
                <a id="A2" runat="server" href="~/Template/证书续期须知.docx" target="_blank"><font style="color: Blue; font-size: 16px; text-decoration: underline; background: url(../Images/light.gif) no-repeat left center; padding-left: 15px;">续期须知</font></a>
            </div>
            <div class="DivTitleOn" onclick="DivOnOff(this,'Td3',event);" title="折叠">
                申报说明
            </div>
            <div class="DivContent" id="Td3">
                1、安管人员安全生产考核证书、特种作业操作资格证书应在有效期满前提交续期申请，具体续期要求请关注市住建委网站通知，未在有效期满前提交续期申请的证书自动失效。<br />
                2、组织机构代码：9位数字或大写字母组合,带“-”的去掉“-”，社会统一信用代码中的第9位至第17位为企业的组织机构代码。<a title="统一社会信用代码查询" href="https://www.cods.org.cn"
                    target="_blank" style="color: Blue; text-decoration: underline;">【统一社会信用代码查询】</a><br />                
                3、证书续期办理流程：个人提交申请 > 聘用单位确认 > 市住建委审核，在市住建委审批前申请人可以进入详细页面进行修改或取消申请操作。<br />
                4、续期时间：证书有效期截止日期前90天的当日至有效期截止日期当日。<br />
                5、企业确认时间：企业确认的截止期限为证书有效期截止日期当日。
            </div>
            <div id="DivMain" class="content">
                <table class="bar_cx" id="tableSearch" runat="server">
                    <tr>
                        <td width="8%" align="right" nowrap="nowrap">岗位工种：
                        </td>
                        <td align="left" width="42%">
                            <uc1:PostSelect ID="PostSelect1" runat="server" OnPostTypeSelectChange="PostSelect1_OnPostTypeSelectChange" />
                        </td>
                        <td width="8%" align="right" nowrap="nowrap">证书编号：
                        </td>
                        <td width="42%" align="left">

                            <telerik:RadTextBox ID="RadTextBoxCertificateCode" runat="server" Width="33%" Skin="Default"
                                Style="float: left;">
                            </telerik:RadTextBox>
                            <span style="float: left; padding: 0 4px; line-height: 22px;">有效期至：</span>
                            <telerik:RadDatePicker ID="txtValidEndtDate" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                MinDate="01/01/1900" runat="server" Width="33%" Style="float: left;" />
                        </td>
                    </tr>
                    <tr id="TrPerson" runat="server">
                        <td align="right" nowrap="nowrap">姓 名：
                        </td>
                        <td align="left">
                            <telerik:RadTextBox ID="RadTextBoxWorkerName" runat="server" Width="97%" Skin="Default">
                            </telerik:RadTextBox>
                        </td>
                        <td align="right" nowrap="nowrap">证件号码：
                        </td>
                        <td align="left">
                            <telerik:RadTextBox ID="RadTextBoxWorkerCertificateCode" runat="server" Width="97%" Skin="Default"
                                ToolTip="带字母的注意大小写">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr id="TrUnit" runat="server">
                        <td align="right" nowrap="nowrap">
                            <asp:Label ID="LabelUnitName" runat="server" Text="企业名称："></asp:Label>

                        </td>
                        <td align="left">
                            <telerik:RadTextBox ID="RadTextBoxUnitName" runat="server" Width="97%" Skin="Default">
                            </telerik:RadTextBox>
                        </td>
                        <td align="right" nowrap="nowrap">

                            <asp:Label ID="LabelUnitCode" runat="server" Text=" 组织机构代码："></asp:Label>
                        </td>
                        <td align="left">
                            <telerik:RadTextBox ID="RadTextBoxUnitCode" runat="server" Width="97%" Skin="Default">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap="nowrap">申请状态：
                        </td>
                        <td align="left">
                            <asp:RadioButtonList ID="RadioButtonListApplyStatus" runat="server" RepeatDirection="Horizontal" Width="450px"
                                AutoPostBack="false">
                                <asp:ListItem Value="" Selected="True">全部</asp:ListItem>
                                <asp:ListItem Value="0">应续期（未申请）</asp:ListItem>
                                <asp:ListItem Value="1">已申请（在办中）</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>

                        <td align="right" nowrap="nowrap">申请批次号：
                        </td>
                        <td align="left">
                            <telerik:RadTextBox ID="RadTextBoxApplyCode" runat="server" Width="97%" Skin="Default">
                            </telerik:RadTextBox>
                        </td>

                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="bt_large" OnClick="ButtonSearch_Click" />
                        </td>
                    </tr>
                </table>
                <div class="table_cx">
                    <img alt="" src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                    证书续期申请列表                       
                </div>
                <telerik:RadGrid ID="RadGridApply" runat="server"
                    GridLines="None" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" PagerStyle-AlwaysVisible="true"
                    SortingSettings-SortToolTip="单击进行排序" Width="100%" Skin="Blue" EnableAjaxSkinRendering="False"
                    EnableEmbeddedSkins="False">
                    <ClientSettings EnableRowHoverStyle="true">
                    </ClientSettings>
                    <HeaderContextMenu EnableEmbeddedSkins="False">
                    </HeaderContextMenu>
                    <MasterTableView NoMasterRecordsText=" 没有可显示的记录"
                        DataKeyNames="CertificateID,CertificateContinueID,WorkerCertificateCode,ApplyStatus">
                        <Columns>

                            <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="ApplyCode" DataField="ApplyCode" HeaderText="申请批号">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="PostName" DataField="PostName" HeaderText="岗位工种">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="CertificateCode" DataField="certificateCode"
                                HeaderText="证书编号">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <%--<telerik:GridBoundColumn UniqueName="ConferDate" DataField="ConferDate" HtmlEncode="false"
                                DataFormatString="{0:yyyy-MM-dd}" HeaderText="发证日期">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>--%>
                            <telerik:GridBoundColumn UniqueName="ValidEndDate" DataField="ValidEndDate" HtmlEncode="false"
                                DataFormatString="{0:yyyy-MM-dd}" HeaderText="有效期至">
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
                            <telerik:GridTemplateColumn UniqueName="Faceimage" HeaderText="照片">
                                <ItemTemplate>
                                    <div style="position: relative;">
                                        <asp:Image ID="Image1" runat="server" Width="14" Height="18" ImageUrl='<%#  ZYRYJG.UIHelp.ShowFaceImeByFacePhotoAndWorkerCertificateCode(Eval("FACEPHOTO") == DBNull.Value?"":Eval("FACEPHOTO").ToString(),Eval("WorkerCertificateCode").ToString())  %>'
                                            onmouseover="setImgSize(this,100,140,'absolute',event);" onmouseout="setImgSize(this,14,18,'relative',event);"></asp:Image>
                                    </div>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridTemplateColumn>                         
                              <telerik:GridTemplateColumn UniqueName="NewUnitName" HeaderText="企业名称">
                                <ItemTemplate>
                                  <%# Eval("NewUnitName") == DBNull.Value?Eval("UnitName"):Eval("NewUnitName")%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridTemplateColumn>
                           <%-- <telerik:GridTemplateColumn UniqueName="sb" HeaderText="社保" Visible="false">
                                <ItemTemplate>
                                    <%# (((Eval("PostTypeID").ToString() == "1") || (Eval("PostTypeID").ToString() == "3"))
                                          && Eval("ApplyDate")!=DBNull.Value  && (Convert.ToDateTime(Eval("ApplyDate")).CompareTo(DateTime.Parse("2014-07-01")) >= 0))?
                                              string.Format("<span class=\"link_edit\" onclick='javascript:SetIfrmSrc(\"../PersonnelFile/RYSBView.aspx?o1={0}&o2={1}&o3={2}\");'><nobr>社保</nobr></span>", Eval("WorkerCertificateCode").ToString(), Eval("UnitCode").ToString(), Convert.ToDateTime(Eval("ApplyDate")).ToString()) : ""%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" Font-Bold="true" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                            </telerik:GridTemplateColumn>--%>
                            <%--<telerik:GridBoundColumn UniqueName="ApplyStatus" DataField="ApplyStatus" HeaderText="审批状态">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>--%>
                            <telerik:GridTemplateColumn UniqueName="ApplyStatus" HeaderText="审批状态">
                                <ItemTemplate>

                                    <%#  Eval("ApplyStatus") ==DBNull.Value?"未申请": Eval("ApplyStatus").ToString()!="已初审"?Eval("ApplyStatus"):  Eval("ReportMan")==DBNull.Value?"已申请":"已初审"%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="Apply" HeaderText="操作">
                                <ItemTemplate>
                                    <%# (Eval("CertificateContinueID").ToString() =="0")?
                                     (
                                     Convert.ToDateTime(Eval("ValidEndDate")).AddDays(-90) <DateTime.Now && Convert.ToDateTime(Eval("ValidEndDate")).AddDays(1) > DateTime.Now?
                                         string.Format("<span class=\"link_edit\" onclick='javascript:SetIfrmSrc(\"ApplyDetail.aspx?o={0}&o2={1}\")';><nobr>申请</nobr></span>" , Page.Server.UrlEncode(Utility.Cryptography.Encrypt(Eval("CertificateID").ToString())),Page.Server.UrlEncode(Utility.Cryptography.Encrypt(Eval("CertificateContinueID").ToString())))
                                         :string.Format("<span class=\"link_edit\" onclick=\"javascript:layer.alert('{0}',{{offset:'150px',icon:0,time:0,area: ['600px', 'auto']}});\">申请</span>",ViewState["Help"])
                                     )
                                     :string.Format("<span class=\"link_edit\" onclick='javascript:SetIfrmSrc(\"ApplyDetail.aspx?o={0}&o2={1}\")';><nobr>详细</nobr></span>" , Page.Server.UrlEncode(Utility.Cryptography.Encrypt(Eval("CertificateID").ToString())),Page.Server.UrlEncode(Utility.Cryptography.Encrypt(Eval("CertificateContinueID").ToString())))%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                            </telerik:GridTemplateColumn>
                        </Columns>
                        <HeaderStyle Font-Bold="True" />
                        <EditFormSettings>
                            <EditColumn UniqueName="EditCommandColumn1">
                            </EditColumn>
                        </EditFormSettings>
                        <PagerStyle AlwaysVisible="true" />
                        <PagerTemplate>
                            <uc2:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                        </PagerTemplate>
                    </MasterTableView>
                    <FilterMenu EnableEmbeddedSkins="False">
                    </FilterMenu>
                    <SortingSettings SortToolTip="单击进行排序"></SortingSettings>
                </telerik:RadGrid>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="Model.CertificateOB"
                    DeleteMethod="Delete" InsertMethod="Insert" SelectMethod="GetListView" TypeName="DataAccess.CertificateContinueDAL"
                    UpdateMethod="Update" SelectCountMethod="SelectCountView" EnablePaging="true"
                    MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex"
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
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <uc4:IframeView ID="IframeView" runat="server" />
      <div id="floadAD" class="floadAd">
            <a class="close" href="javascript:void();" style="color: #000000; text-align: right; float: right; clear: both; padding-bottom: 4px; font-size: 28px">×</a>
            <p class="item" style="line-height: 150%">
                <b>延长特种作业续期申请时间通知：</b>
                <p style="text-indent: 40px; line-height: 150%;text-align:left">
                   受疫情影响，未能在6月30日前提交证书续期申请的特种作业人员、请于2022年9月30日前办理续期。<br />
                </p>
            </p>
        </div>
     <script src="../Scripts/FloatMessage.js" type="text/javascript"></script>
        <style type="text/css">
        .floadAd {
            position: absolute;
            z-index: 999900;
            display: none;
            background-color: #80CFFD;
            padding: 5px 10px 8px 30px;
            border-radius: 6px 6px;
            font-size: 16px;
            width: 600px;
        }
        .floadAd .item {
            display: block;
            padding-right: 40px;
        }
        .floadAd .item img {
            vertical-align: bottom;
        }
    </style>
</asp:Content>
