<%@ Page Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true" CodeBehind="CertifCheckUnit.aspx.cs" Inherits="ZYRYJG.RenewCertifates.CertifCheckUnit" %>


<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<%@ Register Src="../PostSelect.ascx" TagName="PostSelect" TagPrefix="uc1" %>
<%@ Register Src="../CheckAll.ascx" TagName="CheckAll" TagPrefix="uc3" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    </telerik:RadCodeBlock>
    <telerik:RadWindowManager runat="server" RestrictionZoneID="offsetElement" ID="RadWindowManager1"
        EnableShadow="true" EnableEmbeddedScripts="true" Skin="Windows7" VisibleStatusbar="false">
    </telerik:RadWindowManager>
    <script type="text/javascript">

    </script>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" EnableAJAX="true">
        <AjaxSettings>
             <telerik:AjaxSetting AjaxControlID="RadGridAccept">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGridAccept" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
              <telerik:AjaxSetting AjaxControlID="ButtonSearch">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGridAccept" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="divQX" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="PostSelect1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="PostSelect1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
               <telerik:AjaxSetting AjaxControlID="divQX">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divQX"  />
                    <telerik:AjaxUpdatedControl ControlID="RadGridAccept" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="ButtonSearch" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default" />
    <div class="div_out">
        <div class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt; 证书管理 &gt;&gt; <strong>企业证书续期申请确认</strong>
            </div>
        </div>
        <div class="content">
            <table class="bar_cx">
                    <tr>
                        <td width="8%" align="right" nowrap="nowrap">岗位工种：
                        </td>
                        <td align="left" width="42%">
                            <uc1:PostSelect ID="PostSelect1" runat="server" />
                        </td>
                        <td width="8%" align="right" nowrap="nowrap">证书编号：
                        </td>
                        <td width="42%" align="left">
                            <telerik:RadTextBox ID="RadTextBoxCertificateCode" runat="server" Width="33%" Skin="Default"
                                onkeydown="ButtonSearchClick(event); " Style="float: left;">
                            </telerik:RadTextBox>
                            <span style="float: left; padding: 0 4px; line-height: 22px;">有效期至：</span>
                            <telerik:RadDatePicker ID="txtValidEndtDate" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                MinDate="01/01/1900" runat="server" Width="33%" Style="float: left;" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap="nowrap">姓名：
                        </td>
                        <td align="left">
                            <telerik:RadTextBox ID="RadTextBoxWorkerName" runat="server" Width="97%" Skin="Default"
                                MaxLength="50" >
                            </telerik:RadTextBox>
                        </td>
                        <td align="right" nowrap="nowrap">证件号码：
                        </td>
                        <td align="left">
                            <telerik:RadTextBox ID="RadTextBoxWorkerCertificateCode" runat="server" Width="97%" Skin="Default"
                                MaxLength="50" >
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap="nowrap">受理状态：
                        </td>
                        <td align="left">
                            <asp:RadioButtonList ID="RadioButtonListStatus" runat="server" RepeatDirection="Horizontal" Width="400px"
                                AutoPostBack="false">
                                <asp:ListItem Value="" Selected="True">全部</asp:ListItem>
                                <asp:ListItem Value="待单位确认">未受理</asp:ListItem>
                                <asp:ListItem Value="已审查">审查通过</asp:ListItem>
                                <asp:ListItem Value="退回修改">退回修改</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td align="right" nowrap="nowrap">企业受理日期：
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
                        <td align="center" colspan="2">
                            <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="bt_large" OnClick="ButtonSearch_Click" />
                        </td>
                    </tr>
                </table>

            <div class="table_cx">
                    <img src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                    证书续期申请列表
                </div>
                <div style="width: 98%; margin: 0 auto;">
                    <telerik:RadGrid ID="RadGridAccept" runat="server"
                        GridLines="None" AllowPaging="True" PageSize="10" AllowSorting="True" AutoGenerateColumns="False" PagerStyle-AlwaysVisible="true"
                        SortingSettings-SortToolTip="单击进行排序" Width="100%" Skin="Blue" EnableAjaxSkinRendering="false"
                        EnableEmbeddedSkins="false" OnPageIndexChanged="RadGridAccept_PageIndexChanged"
                        OnDataBound="RadGridAccept_DataBound">
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
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridTemplateColumn>
                         
                                 <telerik:GridTemplateColumn UniqueName="jxjyway" HeaderText="继续教育形式">
                                    <ItemTemplate>                                        
                                            <%# ZYRYJG.UIHelp.GetJxjyType( Eval("jxjyway")) %>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" Font-Bold="true" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false"  />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="ApplyDate" DataField="ApplyDate" HtmlEncode="false"
                                    DataFormatString="{0:yyyy-MM-dd}" HeaderText="申请日期">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="NewUnitCheckTime" DataField="NewUnitCheckTime" HtmlEncode="false"
                                    DataFormatString="{0:yyyy-MM-dd}" HeaderText="企业受理日期">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn UniqueName="Status" HeaderText="企业受理状态">
                                    <ItemTemplate>
                                        <span class='<%# (Eval("Status").ToString()=="待单位确认")?"statusWaitUnitCehck":(Eval("Status").ToString()=="退回修改")?"statusSendBack":"CheckPass"%>'>
                                            <%# (Eval("Status").ToString()=="待单位确认")?"待单位确认":
                                    (Eval("Status").ToString()=="退回修改")?"退回修改":"审查通过"%>
                                        </span>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" Font-Bold="true" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn UniqueName="PostTypeName" DataField="PostTypeName" HeaderText="岗位类别">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
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
                                            <asp:Image ID="Image1" runat="server" Width="14" Height="18" ImageUrl='<%#  ZYRYJG.UIHelp.ShowFaceImeByFacePhotoAndWorkerCertificateCode(Eval("FACEPHOTO") == DBNull.Value?"":Eval("FACEPHOTO").ToString(),Eval("WorkerCertificateCode") == DBNull.Value?"111":Eval("WorkerCertificateCode").ToString())  %>'
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
                              <%--  <telerik:GridTemplateColumn UniqueName="sb" HeaderText="社保">
                                    <ItemTemplate>
                                        <%# (Convert.ToDateTime(Eval("ApplyDate")).CompareTo(DateTime.Parse("2014-07-01")) >= 0)? string.Format("<span class=\"link_edit\" onclick='javascript:SetIfrmSrc(\"../PersonnelFile/RYSBView.aspx?o1={0}&o2={1}&o3={2}\");'><nobr>{3}</nobr></span>", Eval("WorkerCertificateCode").ToString(), Eval("UnitCode").ToString(), Convert.ToDateTime(Eval("ApplyDate")).ToString(),(Eval("SheBaoCheck")==DBNull.Value)?"查看": Eval("SheBaoCheck").ToString()=="1"?"符合":"不符合" ):""%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" Font-Bold="true" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                                </telerik:GridTemplateColumn>--%>

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
                <div id="divQX" runat="server" visible="false" style="width: 99%; padding-top: 20px; text-align: center; clear: both; margin: auto">
                    <table id="TableEdit" runat="server" border="0" cellpadding="5" cellspacing="1" class="table" style="margin: 10px auto; width: 99%">
                        <tr class="GridLightBK">
                            <td colspan="2" class="barTitle">批量受理操作（请先勾选要受理的记录）</td>
                        </tr>
                        <tr class="GridLightBK">
                            <td width="20%" align="right">受理结果：</td>
                            <td width="80%" align="left">
                                <asp:RadioButtonList ID="RadioButtonListApplyStatus" runat="server" RepeatDirection="Horizontal" TextAlign="right" AutoPostBack="true" OnSelectedIndexChanged="RadioButtonListApplyStatus_SelectedIndexChanged">
                                    <asp:ListItem Text="通过" Value="通过" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="不通过" Value="不通过"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td align="right">受理意见：</td>
                            <td align="left">

                                <asp:TextBox ID="TextBoxApplyGetResult" runat="server" TextMode="MultiLine" Rows="3" Width="99%" Text="提交建委审核"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td width="20%" align="right"></td>
                            <td align="left">
                                <asp:Button ID="BtnSave" runat="server" CssClass="bt_large" Text="确认提交" OnClick="BtnSave_Click" />&nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                    
                </div>

            </div>
            <br />
        </div>
        <uc4:IframeView ID="IframeView" runat="server" />
        <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
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
</asp:Content>
