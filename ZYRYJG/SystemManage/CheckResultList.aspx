<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckResultList.aspx.cs" Inherits="ZYRYJG.SystemManage.CheckResultList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" />
    <link href="../Skins/Blue/Grid.Blue.css?v=1.008" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
    <link href="../layer/skin/layer.css" rel="stylesheet" />
    <script src="../layer/layer.js" type="text/javascript"></script>
    <%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc1" %>
    <%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc2" %>

    <style type="text/css">
        .link {
            border: none;
            color: blue;
            background-color: transparent;
            cursor: pointer;
            font-size: 16px;

        }

        .Tab50 {
            height: 40px;
            line-height: 40px;
            width: 50%;
            background-color: #F0F0F0;
            border: none;
            margin: 0;
            padding: 0;
            font-size: 16px;
            color:#000000;
            cursor:pointer;
        }

        .TabCur {
            background-color: #FFFFFF !important;
             color:red !important;
        }

        .sm {
            width: 98%;
            margin: 12px auto;
            line-height: 200%;
            font-size: 16px;
            color: #444444;
            text-align: center;
            background-color: #F6F8FF;
            border: 1px solid #F0F8FF;
            border-radius: 5px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server">
        </telerik:RadStyleSheetManager>
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Windows7">
        </telerik:RadWindowManager>
        <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">

            <script type="text/javascript">
                function onRequestStart(sender, args) {

                    if (args.get_eventTarget().indexOf("ButtonImportShebao") >= 0) {
                        args.set_enableAjax(false);

                    }
                    if (args.get_eventTarget().indexOf("ButtonAdd") >= 0) {
                        args.set_enableAjax(false);

                    }
                    if (args.get_eventTarget().indexOf("ButtonButtonExportSheBao") >= 0) {
                        args.set_enableAjax(false);

                    }
                    if (args.get_eventTarget().indexOf("ButtonExport") >= 0) {
                        args.set_enableAjax(false);

                    }
                }

            </script>

        </telerik:RadCodeBlock>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1"
            EnableAJAX="true" ClientEvents-OnRequestStart="onRequestStart">
            <AjaxSettings>                
                <telerik:AjaxSetting AjaxControlID="div_Main">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="div_Main" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true" Skin="Default" />
        <div class="div_out">
            <div id="div_top" class="dqts">
                <div style="float: left;">
                    当前位置 &gt;&gt;综合查询 &gt;&gt;<strong>注册异常人员名单</strong>
                </div>
            </div>
            <div class="sm">以下数据由全国建筑市场公共服务平台提供，定期更新。请企业自主查询，及时完成自查自纠工作。<br />
                <a target="_blank" href="../Template/打击职业资格“挂证”等违法违规行为常见问题及统一解释.docx">下载：【打击职业资格“挂证”等违法违规行为常见问题及统一解释（2019.04.14）.docx】</a><br />
                 <a target="_blank" href="../Template/京建发〔2019〕151号 附件1.xlsx">下载：【京建发〔2019〕151号 附件1.xlsx】</a>
            </div>
            <div runat="server" id="div_Main" class="table_border" style="width: 99%; margin: 5px auto; padding-bottom: 30px;">

                <div style="width: 100%; margin: 0 0; padding: 0 0; text-align: left; background-color: #F0F0F0; border: none;">
                    <asp:Button ID="ButtonCheckSheBao" runat="server" Text=">> 社保存疑" OnClick="ButtonCheckSheBao_Click" CssClass="Tab50 TabCur" /><asp:Button ID="ButtonCheckCFZC" runat="server" Text=">> 重复注册" OnClick="ButtonCheckCFZC_Click" CssClass="Tab50" />
                </div>
                <div class="content">
                    <div id="div_CheckCFZC" runat="server" style="width: 98%; height: 100%; margin: 10px auto; text-align: center;" visible="false">
                        <table>
                             <tr id="trtrClassCFZC" runat="server" visible="false">
                                <td colspan="3" align="left">集团企业：
                                    <telerik:RadComboBox ID="RadComboBoxJT_CFZC" runat="server" Width="120">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="请选择" Value="" Selected="true" />
                                            <telerik:RadComboBoxItem Text="城建集团" Value="北京城建集团有限责任公司" />
                                            <telerik:RadComboBoxItem Text="城乡集团" Value="北京城乡建设集团有限责任公司" />
                                            <telerik:RadComboBoxItem Text="建工集团" Value="北京建工集团有限责任公司" />
                                            <telerik:RadComboBoxItem Text="市政集团" Value="北京市政建设集团有限责任公司" />
                                            <telerik:RadComboBoxItem Text="市政路桥集团" Value="北京市政路桥管理养护集团有限公司" />
                                            <telerik:RadComboBoxItem Text="新兴建设集团" Value="北京中关村开发建设股份有限公司" />
                                            <telerik:RadComboBoxItem Text="中关村建设" Value="北京住总集团有限责任公司" />
                                            <telerik:RadComboBoxItem Text="中建二局" Value="中国建筑第二工程局有限公司" />
                                            <telerik:RadComboBoxItem Text="中建一局" Value="中国建筑一局（集团）有限公司" />
                                            <telerik:RadComboBoxItem Text="住总集团" Value="中国新兴建设开发总公司" />
                                        </Items>

                                    </telerik:RadComboBox>
                                    区县：
                                    <telerik:RadComboBox ID="RadComboBoxQX_CFZC" runat="server" Width="120">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="请选择" Value="" Selected="true" />
                                            <telerik:RadComboBoxItem Text="东城区" Value="东城" />
                                            <telerik:RadComboBoxItem Text="西城区" Value="西城" />
                                            <telerik:RadComboBoxItem Text="崇文区" Value="崇文" />
                                            <telerik:RadComboBoxItem Text="宣武区" Value="宣武" />
                                            <telerik:RadComboBoxItem Text="朝阳区" Value="朝阳" />
                                            <telerik:RadComboBoxItem Text="丰台区" Value="丰台" />
                                            <telerik:RadComboBoxItem Text="石景山区" Value="石景山" />
                                            <telerik:RadComboBoxItem Text="海淀区" Value="海淀" />
                                            <telerik:RadComboBoxItem Text="门头沟区" Value="门头沟" />
                                            <telerik:RadComboBoxItem Text="房山区" Value="房山" />
                                            <telerik:RadComboBoxItem Text="通州区" Value="通州" />
                                            <telerik:RadComboBoxItem Text="顺义区" Value="顺义" />
                                            <telerik:RadComboBoxItem Text="昌平区" Value="昌平" />
                                            <telerik:RadComboBoxItem Text="大兴区" Value="大兴" />
                                            <telerik:RadComboBoxItem Text="怀柔区" Value="怀柔" />
                                            <telerik:RadComboBoxItem Text="平谷区" Value="平谷" />
                                            <telerik:RadComboBoxItem Text="亦庄" Value="亦庄" />
                                            <telerik:RadComboBoxItem Text="密云区" Value="密云" />
                                            <telerik:RadComboBoxItem Text="延庆区" Value="延庆" />
                                        </Items>
                                    </telerik:RadComboBox>
                                     其他：
                                    <telerik:RadComboBox ID="RadComboBoxQT_CFZC" runat="server" Width="120">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="请选择" Value="" Selected="true" />
                                            <telerik:RadComboBoxItem Text="监理企业" Value="监理企业" />
                                            <telerik:RadComboBoxItem Text="造价企业" Value="造价企业" />
                                            <telerik:RadComboBoxItem Text="其他（设计、勘察、招标代理等）" Value="其他" />                                            
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr id="trWorkerHide" runat="server" >
                                <td style="width: 60%; text-align: left">检索项目：
                                    <telerik:RadComboBox ID="RadComboBoxItem" runat="server" Width="120">
                                        <Items>
                                             <telerik:RadComboBoxItem Text="姓名" Value="UserName" />
                                            <telerik:RadComboBoxItem Text="证件号码" Value="IDCard" />                                           
                                            <telerik:RadComboBoxItem Text="注册证书编号" Value="CertCode" />
                                            <telerik:RadComboBoxItem Text="注册单位" Value="UnitName" />

                                        </Items>
                                    </telerik:RadComboBox>
                                    &nbsp;<telerik:RadTextBox ID="RadTextBoxZJHM" runat="server" Width="150px" Text=""></telerik:RadTextBox>
                                    <asp:Button ID="ButtonQuery" runat="server" Text="查 询" CssClass="button" OnClick="ButtonQuery_Click" />
                                </td>
                                <td align="right" style="width: 34%;">
                                    <asp:FileUpload ID="FileUpload1" runat="server" Visible="false" />
                                    <asp:Button ID="ButtonAdd" runat="server" Text="导 入" CssClass="button" OnClick="ButtonAdd_Click" Visible="false" />
                                </td>
                                <td style="width: 6%;">
                                    <a runat="server" id="linkTemplateCFZC" href="../Template/重复注册核查导入模板.xls" visible="false">
                                        <img src="../Images/xls.gif" title="下载模板" alt="下载模板" /></a>
                                </td>
                            </tr>
                        </table>
                        <telerik:RadGrid ID="RadGridCFZC" runat="server" AllowCustomPaging="True"
                            GridLines="None" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"  Skin="Blue" EnableAjaxSkinRendering="False" EnableEmbeddedSkins="False"
                            SortingSettings-SortToolTip="单击进行排序" Width="98%" PagerStyle-AlwaysVisible="true" OnPageIndexChanged="RadGridCFZC_PageIndexChanged">
                            <ClientSettings EnableRowHoverStyle="true">
                            </ClientSettings>
                            <HeaderContextMenu EnableEmbeddedSkins="False">
                            </HeaderContextMenu>
                            <MasterTableView NoMasterRecordsText=" 没有可显示的记录" CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Left">
                                <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                                <Columns>
                                    <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="个人基本信息">
                                        <ItemTemplate>
                                            <b>姓　　名：</b><%# Eval("UserName")%><br />
                                            <b>性　　别：</b><%# Eval("Sex")%><br />
                                            <b>出生年月：</b><%# Eval("Birthday")%><br />
                                            <b>证件号码：</b><%# Eval("IDCard")%><br />
                                            <b>注册单位：</b><%# Eval("UnitName")%>
                                        </ItemTemplate>
                                        <ItemStyle Width="35%" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="注册证书信息">
                                        <ItemTemplate>

                                            <b>所属省份：</b><%# Eval("Provice")%><br />
                                            <b>归属地：</b><%# Eval("Region")%><br />
                                            <b>注册类型：</b><%# Eval("RegType")%><br />
                                            <b>注册证书编号：</b><%# Eval("CertCode")%><br />                                        
                                            <b>注册有效期：</b><%# Eval("ValidEnd")%><br />
                                          
                                        </ItemTemplate>
                                        <ItemStyle Width="25%" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="问题描述">
                                        <ItemTemplate>
                                             <b>数据发布日期：</b><%#  Convert.ToDateTime(Eval("PublishDate")).ToString("yyyy-MM-dd")%><br />
                                            <%# Eval("Remark") ==DBNull.Value||Eval("Remark")==""?"":string.Format("<b>备注：</b>{0}<br />",Eval("Remark"))%>
                                            <%# Eval("Question") ==DBNull.Value||Eval("Question")==""?"":string.Format("<b>问题说明：</b>{0}<br />",Eval("Question"))%>
                                        </ItemTemplate>
                                        <ItemStyle Width="35%" />
                                    </telerik:GridTemplateColumn>

                                </Columns>

                                <PagerStyle AlwaysVisible="True"></PagerStyle>

                                <HeaderStyle Font-Bold="True" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                <AlternatingItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                <CommandItemStyle HorizontalAlign="Left"></CommandItemStyle>
                                <PagerTemplate>
                                    <uc1:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                                </PagerTemplate>
                            </MasterTableView>

                            <PagerStyle AlwaysVisible="True"></PagerStyle>

                            <FilterMenu EnableEmbeddedSkins="False">
                            </FilterMenu>
                            <SortingSettings SortToolTip="单击进行排序"></SortingSettings>
                        </telerik:RadGrid>
                        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="Model.Out_CheckCFZCMAL"
                            SelectMethod="GetList" TypeName="DataAccess.Out_CheckCFZCDAL"
                            SelectCountMethod="SelectCount" EnablePaging="true"
                            MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex" SortParameterName="orderBy">
                            <SelectParameters>
                                <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                                    DefaultValue="" ConvertEmptyStringToNull="false" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                        <div style="width: 100%; margin: 20px auto;">
                            <asp:Button ID="ButtonExport" runat="server" Text="导 出" CssClass="button" OnClick="ButtonExport_Click" /><span id="spanOutput" runat="server" class="excel" style="padding: 0 40px; font-weight: bold"></span>
                        </div>
                    </div>

                    <div id="div_CheckSheBao" runat="server" style="width: 98%; height: 100%; margin: 10px auto; text-align: center;" visible="true">
                        <table>
                            <tr id="trClass" runat="server" visible="false">
                                <td colspan="3" align="left">集团企业：
                                    <telerik:RadComboBox ID="RadComboBoxJT" runat="server" Width="120">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="请选择" Value="" Selected="true" />
                                            <telerik:RadComboBoxItem Text="城建集团" Value="北京城建集团有限责任公司" />
                                            <telerik:RadComboBoxItem Text="城乡集团" Value="北京城乡建设集团有限责任公司" />
                                            <telerik:RadComboBoxItem Text="建工集团" Value="北京建工集团有限责任公司" />
                                            <telerik:RadComboBoxItem Text="市政集团" Value="北京市政建设集团有限责任公司" />
                                            <telerik:RadComboBoxItem Text="市政路桥集团" Value="北京市政路桥管理养护集团有限公司" />
                                            <telerik:RadComboBoxItem Text="新兴建设集团" Value="北京中关村开发建设股份有限公司" />
                                            <telerik:RadComboBoxItem Text="中关村建设" Value="北京住总集团有限责任公司" />
                                            <telerik:RadComboBoxItem Text="中建二局" Value="中国建筑第二工程局有限公司" />
                                            <telerik:RadComboBoxItem Text="中建一局" Value="中国建筑一局（集团）有限公司" />
                                            <telerik:RadComboBoxItem Text="住总集团" Value="中国新兴建设开发总公司" />
                                        </Items>

                                    </telerik:RadComboBox>
                                    区县：
                                    <telerik:RadComboBox ID="RadComboBoxQX" runat="server" Width="120">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="请选择" Value="" Selected="true" />
                                            <telerik:RadComboBoxItem Text="东城区" Value="东城" />
                                            <telerik:RadComboBoxItem Text="西城区" Value="西城" />
                                            <telerik:RadComboBoxItem Text="崇文区" Value="崇文" />
                                            <telerik:RadComboBoxItem Text="宣武区" Value="宣武" />
                                            <telerik:RadComboBoxItem Text="朝阳区" Value="朝阳" />
                                            <telerik:RadComboBoxItem Text="丰台区" Value="丰台" />
                                            <telerik:RadComboBoxItem Text="石景山区" Value="石景山" />
                                            <telerik:RadComboBoxItem Text="海淀区" Value="海淀" />
                                            <telerik:RadComboBoxItem Text="门头沟区" Value="门头沟" />
                                            <telerik:RadComboBoxItem Text="房山区" Value="房山" />
                                            <telerik:RadComboBoxItem Text="通州区" Value="通州" />
                                            <telerik:RadComboBoxItem Text="顺义区" Value="顺义" />
                                            <telerik:RadComboBoxItem Text="昌平区" Value="昌平" />
                                            <telerik:RadComboBoxItem Text="大兴区" Value="大兴" />
                                            <telerik:RadComboBoxItem Text="怀柔区" Value="怀柔" />
                                            <telerik:RadComboBoxItem Text="平谷区" Value="平谷" />
                                            <telerik:RadComboBoxItem Text="亦庄" Value="亦庄" />
                                            <telerik:RadComboBoxItem Text="密云区" Value="密云" />
                                            <telerik:RadComboBoxItem Text="延庆区" Value="延庆" />
                                        </Items>
                                    </telerik:RadComboBox>
                                     其他：
                                    <telerik:RadComboBox ID="RadComboBoxQT" runat="server" Width="120">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="请选择" Value="" Selected="true" />
                                            <telerik:RadComboBoxItem Text="监理企业" Value="监理企业" />
                                            <telerik:RadComboBoxItem Text="造价企业" Value="造价企业" />
                                            <telerik:RadComboBoxItem Text="其他（设计、勘察、招标代理等）" Value="其他" />                                            
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr id="tr_WorkHide2" runat="server">
                                <td style="width: 60%; text-align: left">检索项目：
                                    <telerik:RadComboBox ID="RadComboBoxSheBao" runat="server" Width="120">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="证件号码" Value="IDCard18" /> 
                                            <telerik:RadComboBoxItem Text="姓名" Value="UserName" />                                                                                       
                                            <telerik:RadComboBoxItem Text="注册号" Value="RegCode" />
                                            <telerik:RadComboBoxItem Text="归属地" Value="Region" />
                                            <telerik:RadComboBoxItem Text="注册单位" Value="Unit" />
                                        </Items>
                                    </telerik:RadComboBox>
                                    &nbsp;<telerik:RadTextBox ID="RadTextBoxSheBao" runat="server" Width="150px" Text=""></telerik:RadTextBox>
                                    <asp:Button ID="ButtonQuerySheBao" runat="server" Text="查 询" CssClass="button" OnClick="ButtonQuerySheBao_Click" />
                                </td>
                                <td align="right" style="width: 34%;">
                                    <asp:FileUpload ID="FileUploadShebao" runat="server" Visible="false" />
                                    <asp:Button ID="ButtonImportShebao" runat="server" Text="导 入" CssClass="button" OnClick="ButtonImportShebao_Click" Visible="false" />
                                </td>
                                <td style="width: 6%;">
                                    <a runat="server" id="linkTemplateSheBao" href="../Template/社保数据核查导入模板.xls" visible="false">
                                        <img src="../Images/xls.gif" title="下载模板" alt="下载模板" /></a>
                                </td>
                            </tr>
                            
                        </table>
                        <telerik:RadGrid ID="RadGridSheBao" runat="server"
                            GridLines="None" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" AllowCustomPaging="true" OnPageIndexChanged="RadGridSheBao_PageIndexChanged"
                            Skin="Blue" EnableAjaxSkinRendering="False" EnableEmbeddedSkins="False"
                            SortingSettings-SortToolTip="单击进行排序" Width="98%" PagerStyle-AlwaysVisible="true">
                            <ClientSettings EnableRowHoverStyle="true">
                            </ClientSettings>
                            <HeaderContextMenu EnableEmbeddedSkins="False">
                            </HeaderContextMenu>
                            <MasterTableView NoMasterRecordsText=" 没有可显示的记录" CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Left">
                                <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                                <Columns>
                                    <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>

                                    <telerik:GridTemplateColumn HeaderText="个人基本信息">
                                        <ItemTemplate>
                                            <b>姓　　名：</b><%# Eval("UserName")%><br />
                                            <b>性　　别：</b><%# Eval("Sex")%><br />
                                            <b>出生年月：</b><%# Eval("Birthday")%><br />
                                            <b>证件号码：</b><%# Eval("IDCard18")%><br />
                                          
                                        </ItemTemplate>
                                        <ItemStyle Width="35%" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="注册证书信息">
                                        <ItemTemplate>

                                       
                                            <b>归属地：</b><%# Eval("Region")%><br />
                                            <b>注册类型：</b><%# Eval("RegType")%><br />
                                            <b>注册号：</b><%# Eval("RegCode")%><br />
                                    <b>注册单位：</b><%# Eval("Unit")%>
                                        </ItemTemplate>
                                        <ItemStyle Width="25%" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="问题描述">
                                        <ItemTemplate>
                                            <b>数据发布日期：</b><%#  Eval("PublishDateTime")%><br />
                                            <b>社保单位：</b><%# Eval("ShebaoUnit")==DBNull.Value||Eval("ShebaoUnit").ToString()==""?"":"<br />>"+ Eval("ShebaoUnit").ToString().Replace(",","<br />>")%><br />
                                            <%-- <%# Eval("Remark") ==DBNull.Value||Eval("Remark")==""?"":string.Format("<b>备注：</b>{0}<br />",Eval("Remark"))%>--%>
                                            <%# Eval("Question") ==DBNull.Value||Eval("Question")==""?"":string.Format("<b>问题说明：</b>{0}<br />",Eval("Question"))%>
                                            <%# Eval("Remark") ==DBNull.Value||Eval("Remark")==""?"":string.Format("<b>2019年11月在京缴纳社保情况：</b>{0}<br />",Eval("Remark"))%>
                                        </ItemTemplate>
                                        <ItemStyle Width="35%" />
                                    </telerik:GridTemplateColumn>
                                </Columns>

                                <PagerStyle AlwaysVisible="True"></PagerStyle>

                                <HeaderStyle Font-Bold="True" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                <AlternatingItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                <CommandItemStyle HorizontalAlign="Left"></CommandItemStyle>
                                <PagerTemplate>
                                    <uc1:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                                </PagerTemplate>
                            </MasterTableView>

                            <PagerStyle AlwaysVisible="True"></PagerStyle>

                            <FilterMenu EnableEmbeddedSkins="False">
                            </FilterMenu>
                            <SortingSettings SortToolTip="单击进行排序"></SortingSettings>
                        </telerik:RadGrid>
                        <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" DataObjectTypeName="Model.Out_CheckShebaoMAL"
                            SelectMethod="GetList" TypeName="DataAccess.Out_CheckShebaoDAL"
                            SelectCountMethod="SelectCount" EnablePaging="true"
                            MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex" SortParameterName="orderBy">
                            <SelectParameters>
                                <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                                    DefaultValue="" ConvertEmptyStringToNull="false" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                        <div style="width: 100%; margin: 20px auto;">
                            <asp:Button ID="ButtonButtonExportSheBao" runat="server" Text="导 出" CssClass="button" OnClick="ButtonButtonExportSheBao_Click" /><span id="spanOutputSheBao" runat="server" class="excel" style="padding: 0 40px; font-weight: bold"></span>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

