<%@ Page Title="" Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="RYSBView.aspx.cs" Inherits="ZYRYJG.PersonnelFile.RYSBView" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadWindowManager runat="server" RestrictionZoneID="offsetElement" ID="RadWindowManager1"
        EnableShadow="true" EnableEmbeddedScripts="true" Skin="Windows7" VisibleStatusbar="false">
    </telerik:RadWindowManager>
    <script type="text/javascript">
    </script>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" EnableAJAX="true">
        <AjaxSettings>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Windows7" />
    <div class="div_out">
        <div id="div_top" class="dqts">
            <div style="float: left;">
                当前位置&gt;&gt; <strong>社保缴费信息比对结果</strong>
            </div>
        </div>
        <div class="table_border" style="width: 98%; margin: 5px 0;">
            <div class="DivTitleOn" onclick="DivOnOff(this,'Td3',event);" title="折叠">
                比对说明
            </div>
            <div class="DivContent" id="Td3">
                1、从业人员：考试报名、证书京内变更、证书进京变更需要进行社保比对；<br />
                2、二级注册建造师、二级注册造价工程师：初始、重新、延续、执业企业变更注册需要进行社保比对；<br />
                3、社保缴费信息比对合格标准：<br />
                <p style="padding-left: 20px;margin:4px 0;">
                    （1）从业人员申请考试报名的，应显示申请人在申请单位已缴纳申请之日上个月社会保险的记录；<br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;从业人员申请证书京内变更、证书进京变更的，每月12日24:00前上报市住建委审核的，应显示申请人在申请单位已缴纳申请之日上上个月社会保险的记录；<br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;每月13日0:00后上报市住建委审核的，应显示申请人在申请单位已缴纳申请之日上个月社会保险的记录。
                </p>
                <p style="padding-left: 20px;margin:4px 0;">
                    （2）二级注册建造师、二级注册造价工程师申请初始、重新、延续、执业企业变更注册的，每月12日24:00前上报区或市住建委审核的，应显示申请人在申请单位已缴纳申请之日上上个月社会保险的记录；<br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;每月13日0:00后上报区或市住建委审核的，应显示申请人在申请单位已缴纳申请之日上个月社会保险的记录。
                </p>
                4、社保缴费信息由北京市社会保险基金管理中心提供，本系统不提供实时查询服务；申请人提交业务申请当天晚间本系统方能进行社保缴费信息比对。<br />
                5、根据目前社会保险缴纳数据共享获取规则，为不影响您办理业务,建议每月10日即完成社保缴纳。<br />
                6、企业已办理三证合一的，系统默认按企业社会统一信用代码和人员身份证号进行申报比对，请个人通过第三方渠道查询社保个人权益记录。如果企业仍按旧组织机构代码缴纳社保，则系统无法查询；<br />

            </div>
            <div id="main" runat="server" class="content" style="padding: 20px 5px 30px 5px; width: 97%">
                <p id="p_CheckUnit" runat="server"></p>

                <telerik:RadGrid ID="RadGrid1" AllowPaging="false" runat="server" AutoGenerateColumns="False" PagerStyle-AlwaysVisible="true"
                    AllowSorting="false" GridLines="None" CellPadding="0" Width="100%" Skin="Blue" EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false">
                    <ClientSettings EnableRowHoverStyle="false">
                        <Selecting AllowRowSelect="false" />
                    </ClientSettings>
                    <MasterTableView NoMasterRecordsText="没有可显示的记录">
                        <Columns>
                            <telerik:GridBoundColumn UniqueName="WorkerName" DataField="WorkerName" HeaderText="姓名">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="证件号码" UniqueName="CertificateCode" DataField="CertificateCode">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="ENT_Name" DataField="ENT_Name" HeaderText="社保缴费单位">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="CreditCode" DataField="CreditCode" HeaderText="社会统一信用代码">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="JFYF" DataField="JFYF" HeaderText="缴费月份">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="01" HeaderText="社会保险缴费情况">
                                <ItemTemplate>
                                    <%# Eval("01") != System.DBNull.Value ? "已缴" : "未缴"%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridTemplateColumn>

                            <telerik:GridBoundColumn UniqueName="CJSJ" DataField="CJSJ" HeaderText="申请比对日期">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>

                        </Columns>
                        <HeaderStyle Font-Bold="True" />
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
            <div id="divRtn" style="width: 50%; padding: 20px; 20px; margin: 0 auto; text-align: center;">
                <input id="ButtonReturn" type="button" value="返 回" class="bt_large" onclick="javascript: hideIfam();" />
            </div>
        </div>
    </div>
</asp:Content>
