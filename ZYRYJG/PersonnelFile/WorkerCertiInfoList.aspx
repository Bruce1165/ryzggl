<%@ Page Language="C#" Title="" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="WorkerCertiInfoList.aspx.cs" Inherits="ZYRYJG.PersonnelFile.WorkerCertiInfoList" %>

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
                当前位置 &gt;&gt; 电子证书下载 &gt;&gt;
                <strong>电子证书下载</strong>
            </div>
        </div>
        <div class="content">
            <div class="DivContent" id="Td3">
                业务说明：1、根据有关规定，停止<span style="color: red">造价员、专业管理人员</span>考核、变更和续期、电子证书打印工作。<br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2、专业管理人员证书和技能人员证书没有续期要求，“当前有效证书”均为在有效期内的证书。
            </div>
            <p class="jbxxbt">
                从业人员个人证书列表
            </p>
            <div style="width: 98%; margin: 0 auto;">
                <div id="div_ZYTJ" runat="server" style="width: 100%" visible="false">
                    <div class="table_cx">
                        <img alt="" src="../Images/time.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                        重要提示
                    </div>
                    <div style="margin: 12px auto;" class="DivContent">
                        <div runat="server" id="divMessage" style="margin: 12px auto; line-height: 22px;">
                        </div>
                        <telerik:RadGrid ID="RadGridContinueTimeSpan" runat="server" AutoGenerateColumns="False" AllowSorting="false" PageSize="100"
                            GridLines="None" CellPadding="0" Width="100%" Skin="Blue" EnableAjaxSkinRendering="false"
                            EnableEmbeddedSkins="false" OnItemDataBound="RadGridContinueTimeSpan_ItemDataBound">
                            <ClientSettings EnableRowHoverStyle="true">
                                <Selecting AllowRowSelect="True" />
                            </ClientSettings>
                            <MasterTableView NoMasterRecordsText="没有可显示的记录" DataKeyNames="TypeID,TypeName,TypeValue">
                                <Columns>
                                    <telerik:GridBoundColumn HeaderText="序号" UniqueName="RowNum" DataField="RowNum" AllowSorting="false">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="TypeName" DataField="TypeName" HeaderText="配置类型名称">
                                        <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="ValidTo" DataField="ValidTo" HeaderText="证书到期时间（月-日）"
                                        HtmlEncode="false" DataFormatString="{0:MM月dd日}" AllowSorting="false">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="RenewMonth" DataField="RenewMonth" HeaderText="续期开放时间段（月份）">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                </Columns>
                                <HeaderStyle Font-Bold="True" />
                            </MasterTableView>
                        </telerik:RadGrid>
                    </div>
                </div>
                <div class="table_cx" style="margin-top: 20px;margin-bottom: 6px">
                    <img alt="" src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                    个人持证信息列表 【图例：<img class="tl" alt="" src="../Images/yx.png" />有效证书，<img class="tl" alt="" src="../Images/wx.png" />无效证书（过期、离京、注销）不能办理业务】
                </div>
                <telerik:RadGrid ID="RadGrid1" runat="server" GridLines="None"
                    AllowPaging="True" PageSize="100" AllowSorting="True" AutoGenerateColumns="False"
                    SortingSettings-SortToolTip="单击进行排序" Width="100%"
                    Skin="Blue" EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false" >
                    <ClientSettings EnableRowHoverStyle="true">
                    </ClientSettings>
                    <MasterTableView DataKeyNames="CertificateID" NoMasterRecordsText="　没有可显示的记录">
                        <Columns>
                            <telerik:GridBoundColumn UniqueName="PostTypeName" DataField="PostTypeName" HeaderText="岗位类别">
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="PostName" DataField="PostName" HeaderText="岗位工种">
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="CertificateCode" HeaderText="证书编号">
                                <ItemTemplate>
                                    <%# Convert.ToInt32(Eval("PostTypeID"))>0?string.Format("<span class=\"link_edit\"  onclick='javascript:SetIfrmSrc(\"../PersonnelFile/CertificateInfo.aspx?o={0}\");'><nobr>{1}</nobr></span>",Utility.Cryptography.Encrypt(Eval("CertificateID").ToString()),Eval("CertificateCode")):""
                                    %>
                                     <%# Convert.ToInt32(Eval("PostTypeID"))==0?string.Format("<span class=\"link_edit\"  onclick='javascript:SetIfrmSrc(\"../Unit/EJZJSDetail.aspx?o={0}\");'><nobr>{1}</nobr></span>",Utility.Cryptography.Encrypt(Eval("CertificateID").ToString()),Eval("CertificateCode")):""
                                    %>
                                      <%# Convert.ToInt32(Eval("PostTypeID"))==-1?string.Format("<span class=\"link_edit\"  onclick='javascript:SetIfrmSrc(\"../zjs/zjsCertInfo.aspx?o={0}\");'><nobr>{1}</nobr></span>",Utility.Cryptography.Encrypt(Eval("CertificateID").ToString()),Eval("CertificateCode")):""
                                    %>
                                     <%# Convert.ToInt32(Eval("PostTypeID"))<-1?Eval("CertificateCode"):""
                                    %>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" Wrap="false" ForeColor="Blue" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="ValidEndDate" HeaderText="有效期至">
                                <ItemTemplate>
                                    <%# Convert.ToDateTime(Eval("ValidEndDate")).ToString("yyyy.MM.dd")=="2050.01.01"?"当前有效证书":Convert.ToDateTime(Eval("ValidEndDate")).ToString("yyyy.MM.dd")%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridTemplateColumn>             
                             <telerik:GridTemplateColumn UniqueName="UnitName" HeaderText="所在单位">
                                <ItemTemplate>
                                    <%# Eval("PostTypeid").ToString()=="5"?"": Eval("UnitName")%> 
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" Wrap="false" />
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn UniqueName="Status" HeaderText="最近业务">
                                <ItemTemplate>
                                    <%# Eval("Status").ToString()%> <%# Eval("Remark") != null && Eval("Remark").ToString().Contains("超龄")==true?"(超龄)":""%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" Wrap="false" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="Status" HeaderText="证书状态">
                                <ItemTemplate>
                                    <img alt="" class="tl" src='<%# (Eval("POSTTYPENAME").ToString() != "一级建造师" && Eval("POSTTYPENAME").ToString() != "一级临时建造师"&&(Convert.ToDateTime(Eval("ValidEndDate")).AddDays(1) < DateTime.Now || Eval("Status").ToString()=="注销" || Eval("Status").ToString()=="离京变更" ))?"../Images/wx.png":"../Images/yx.png"%>' />
                                    <%# (Eval("POSTTYPENAME").ToString() != "一级建造师" && Eval("POSTTYPENAME").ToString() != "一级临时建造师"&&(Convert.ToDateTime(Eval("ValidEndDate")).AddDays(1) < DateTime.Now || Eval("Status").ToString()=="注销" || Eval("Status").ToString()=="离京变更" ))?"无效":"有效"%>
                                    <%# (Eval("POSTTYPENAME").ToString() != "一级建造师" && Eval("POSTTYPENAME").ToString() != "一级临时建造师"&&Convert.ToDateTime(Eval("ValidEndDate")).AddDays(1) < DateTime.Now)?"(过期)": ""%>
                                    <%# Eval("Status").ToString()=="注销" ?"(注销)":""%>
                                    <%# Eval("Status").ToString()=="离京变更"?"(离京)":""%>
                                    <%# Eval("Remark") != null && Eval("Remark").ToString().Contains("超龄")==true?"(超龄)":""%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" Wrap="false" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="CertificateCode" HeaderText="电子证书">
                                <ItemTemplate>
                                    <%# Eval("Status").ToString()=="离京变更"?string.Format("<span class=\"link_edit\" onclick='SetIfrmSrc(\"../CertifManage/CertChangeTestify.aspx?o={0}\");'><nobr>离京证明</nobr></span>",Utility.Cryptography.Encrypt(Eval("CertificateID").ToString())):((Convert.ToDateTime(Eval("ValidEndDate")).AddDays(1) < DateTime.Now || Eval("Status").ToString()=="注销" ||  Eval("PostTypeID").ToString()=="3"|| Eval("PostID").ToString()=="55" || Eval("PostID").ToString()=="159"|| Eval("PostID").ToString()=="1009"|| Eval("PostID").ToString()=="1021"|| Eval("PostID").ToString()=="1024" )?"":string.Format("<span class=\"link_edit\" onclick='javascript:SetIfrmSrc(\"../PersonnelFile/CertificatePdf.aspx?c={0}&t={1}\");'><nobr>电子证书</nobr></span>", Page.Server.UrlEncode(Utility.Cryptography.Encrypt(Eval("CertificateID").ToString())),Eval("PostTypeID")))%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" Wrap="false" ForeColor="Blue" />
                            </telerik:GridTemplateColumn>
                        </Columns>
                        <HeaderStyle Font-Bold="true" />
                        <PagerTemplate>
                            <uc2:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                        </PagerTemplate>
                        <EditFormSettings>
                            <EditColumn UniqueName="EditCommandColumn1">
                            </EditColumn>
                        </EditFormSettings>
                    </MasterTableView>
                    <ClientSettings>
                    </ClientSettings>
                    <StatusBarSettings LoadingText="正在读取数据" ReadyText="完成" />
                </telerik:RadGrid>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="Model.CertificateOB"
                    SelectMethod="GetList" TypeName="DataAccess.CertificateDAL"
                    SelectCountMethod="SelectCount" EnablePaging="true" MaximumRowsParameterName="maximumRows"
                    StartRowIndexParameterName="startRowIndex" SortParameterName="orderBy">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                            DefaultValue="" ConvertEmptyStringToNull="false" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <div class="table_cx" style="margin-top: 20px; display: none">
                    <img alt="" src="../Images/date.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                    最近办理业务进度
                </div>
                <telerik:RadGrid ID="RadGridProcess" runat="server" GridLines="None" Visible="false"
                    AllowPaging="false" PageSize="10" AllowSorting="false" AutoGenerateColumns="False"
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
                            <telerik:GridBoundColumn UniqueName="ApplyDate" DataField="ApplyDate" HeaderText="申请日期"
                                HtmlEncode="false" DataFormatString="{0:yyyy.MM.dd}">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="ItemType" DataField="ItemType" HeaderText="业务类型">
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="FirstCheckDate" DataField="FirstCheckDate" HeaderText="办理进度"
                                HtmlEncode="false" DataFormatString="{0:yyyy.MM.dd}">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="FirstCheckResult" DataField="FirstCheckResult" HeaderText="">
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="CHECKDATE" DataField="CHECKDATE" HeaderText=""
                                HtmlEncode="false" DataFormatString="{0:yyyy.MM.dd}">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="CHECKRESULT" DataField="CHECKRESULT" HeaderText="">
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="CONFRIMDATE" DataField="CONFRIMDATE" HeaderText=""
                                HtmlEncode="false" DataFormatString="{0:yyyy.MM.dd}">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="CONFRIMRESULT" DataField="CONFRIMRESULT" HeaderText="">
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="NOTICEDATE" DataField="NOTICEDATE" HeaderText=""
                                HtmlEncode="false" DataFormatString="{0:yyyy.MM.dd}">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="NOTICERESULT" DataField="NOTICERESULT" HeaderText="">
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" Wrap="false" />
                            </telerik:GridBoundColumn>
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
