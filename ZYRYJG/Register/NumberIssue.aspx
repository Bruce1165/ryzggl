<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NumberIssue.aspx.cs" Inherits="ZYRYJG.Register.NumberIssue" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc1" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" />
     <link href="../Skins/Blue/Grid.Blue.css?v=1.008" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
    <script src="../Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
        <link href="../layer/skin/layer.css" rel="stylesheet" />
    <script src="../layer/layer.js" type="text/javascript"></script>
    <style type="text/css">
        .floatL {
            float: left;
            padding: 4px 0 4px 4px;
            line-height: 26px;
        }
        .clearL {
            clear: left;
            padding-left: 12px;
        }
        .clearR {
            clear: right;
        }
        .hide {
            display: none;
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
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1"
            EnableAJAX="true">
            <AjaxSettings>
                <%--<telerik:AjaxSetting AjaxControlID="divMain">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="divMain" LoadingPanelID="RadAjaxLoadingPanel1" />

                    </UpdatedControls>
                </telerik:AjaxSetting>--%>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true" Skin="Default" />
        <div class="div_out">
            <div id="div_top" class="dqts">
                <div style="float: left;">
                    当前位置 &gt;&gt;建造师注册管理 &gt;&gt;<strong>编号发放</strong>
                </div>
            </div>
            <div class="content" id="divMain" runat="server">
                <div class="floatL" style="width: 98%; text-align: left; padding-left: 8px">
                    <div class="floatL">
                        <input id="Button1" type="button" class="bt_large" value="选择公告" onclick='javascript: location.href = "NumberIssueChoice.aspx"' />
                    </div>
                    <div class="floatL">
                        <input runat="server" id="ButtonSelectRePlaceApply" type="button" class="bt_large" value="选择遗失补办" onserverclick="ButtonSelectRePlaceApply_ServerClick" Visible="false" />
                    </div>
                     <div class="floatL">
                       <telerik:RadComboBox ID="RadComboBoxApplyType" runat="server" Width="150px" OnSelectedIndexChanged="RadComboBoxApplyType_SelectedIndexChanged" AutoPostBack="true" Visible="false">
                            <Items>
                              
                            </Items>
                        </telerik:RadComboBox>
                    </div>
                    <div class="floatL">
                       <telerik:RadComboBox ID="RadComboBoxApplyLevel" runat="server" Width="100px"  AutoPostBack="true" Visible="false" OnSelectedIndexChanged="RadComboBoxApplyLevel_SelectedIndexChanged" >
                            <Items>
                              <telerik:RadComboBoxItem Text="二级" Value="二级" Selected="true" />
                                <telerik:RadComboBoxItem Text="二级临时" Value="二级临时" />
                            </Items>
                        </telerik:RadComboBox>
                    </div>

                    <div class="floatL">
                        <asp:Label ID="LabelPersonCount" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="floatL hide" id="divCertTypeLable" runat="server">发放内容：</div>
                    <div class="floatL hide" id="divCertType" runat="server">
                        <telerik:RadComboBox ID="RadComboBoxType" runat="server" Width="150px">
                            <Items>
                                <telerik:RadComboBoxItem Text="请选择" Value="" />
                                <telerik:RadComboBoxItem Text="注册编号和证书编号" Value="注册编号和证书编号" />
                                <telerik:RadComboBoxItem Text="证书编号" Value="证书编号" />
                            </Items>
                        </telerik:RadComboBox>
                    </div>
                    <div class="floatL hide" id="divStartRegionNoLable" runat="server">起始注册编号：</div>
                    <div class="floatL hide" id="divStartRegionNo" runat="server">
                        <telerik:RadNumericTextBox ID="RadNumericTextBoxStartRegionNo" runat="server" MaxLength="5"
                            Type="Number"  NumberFormat-DecimalDigits="0" ShowSpinButtons="true" NumberFormat-GroupSeparator="" Width="100px" 
                            MinValue="0" MaxValue="99999">
                                             
                        </telerik:RadNumericTextBox>
                    </div>
                    <div class="floatL hide" id="divStartCertNoLable" runat="server">起始证书编号：</div>
                    <div class="floatL hide" id="divStartCertNo" runat="server">
                        <telerik:RadNumericTextBox ID="RadNumericTextBoxStartCertNo" runat="server" MaxLength="8"
                            Type="Number" NumberFormat-DecimalDigits="0" ShowSpinButtons="true" NumberFormat-GroupSeparator="" Width="100px"
                            MinValue="0" MaxValue="99999999">
                            <NumberFormat DecimalDigits="0"></NumberFormat>
                        </telerik:RadNumericTextBox>
                    </div>
                    <div class="floatL hide" id="divBtn" runat="server">
                        <asp:Button ID="ButtonCreateCode" runat="server" Text="发放编号" CssClass="bt_large" OnClick="ButtonCreateCode_Click" />
                    </div>
                </div>
                <div class="floatL clearL" style="width: 98%; margin: 4px 0; text-align: left;">
                    <telerik:RadGrid ID="RadGridRYXX" runat="server"
                        GridLines="None" AllowSorting="True" AutoGenerateColumns="False" Skin="Blue" EnableAjaxSkinRendering="False"
                            EnableEmbeddedSkins="False"
                        SortingSettings-SortToolTip="单击进行排序" Width="100%" PagerStyle-AlwaysVisible="true">
<SortingSettings SortToolTip="单击进行排序"></SortingSettings>

                        <ClientSettings EnableRowHoverStyle="false" Scrolling-ScrollHeight="420px" Scrolling-AllowScroll="true">
<Scrolling AllowScroll="True" ScrollHeight="420px"></Scrolling>
                        </ClientSettings>
                        <HeaderContextMenu EnableEmbeddedSkins="False">
                        </HeaderContextMenu>
                        <MasterTableView NoMasterRecordsText=" 没有可显示的记录" CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Left" DataKeyNames="ApplyID">
<CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                            <Columns>
                                <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="PSN_Name" DataField="PSN_Name" HeaderText="姓名">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="PSN_Name" DataField="PSN_CertificateNO" HeaderText="身份证号">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="ENT_Name" DataField="ENT_Name" HeaderText="企业名称">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="ApplyType" DataField="ApplyType" HeaderText="注册类型">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="PSN_RegisteProfession" DataField="PSN_RegisteProfession" HeaderText="注册专业">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                               
                                <telerik:GridTemplateColumn UniqueName="RegisterCertificateNo" HeaderText="旧证书编号" Visible="False" >
                                    <ItemTemplate>
                                         <%#Eval("ApplyType").ToString() == "遗失补办"? Eval("RegisterCertificateNo"):"" %>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridTemplateColumn>
                               
                                <telerik:GridTemplateColumn UniqueName="PSN_RegisterCertificateNo" HeaderText="证书编号">
                                    <ItemTemplate>
                                     <telerik:RadTextBox ID="RadTextBoxPSN_RegisterCertificateNo" runat="server" Skin="Telerik" Text='<%# Eval("CodeDate") == DBNull.Value?"":Eval("PSN_RegisterCertificateNo") %>' Enabled='<%# Eval("CodeDate") == DBNull.Value?true:false %>'></telerik:RadTextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridTemplateColumn>



                                <telerik:GridTemplateColumn UniqueName="PSN_RegisterNo" HeaderText="注册编号">
                                    <ItemTemplate>
                                        <telerik:RadTextBox ID="RadTextBoxPSN_RegisterNo" runat="server" Skin="Telerik" Text='<%# Eval("CodeDate") == DBNull.Value?"": Eval("PSN_RegisterNo") %>' Enabled='<%# Eval("CodeDate") == DBNull.Value?true:false %>'></telerik:RadTextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridTemplateColumn>
                            </Columns>

<PagerStyle AlwaysVisible="True"></PagerStyle>

                            <HeaderStyle Font-Bold="True" />

<CommandItemStyle HorizontalAlign="Left"></CommandItemStyle>
                            <PagerTemplate>
                                <uc1:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                            </PagerTemplate>
                        </MasterTableView>

<PagerStyle AlwaysVisible="True"></PagerStyle>

                        <FilterMenu EnableEmbeddedSkins="False">
                        </FilterMenu>
                    </telerik:RadGrid>

                </div>
                <div class="floatL clearL" style="width: 98%; margin: 4px 0; text-align: left;">
                    <div class="floatL">选择发证日期：</div>
                    <div class="floatL">
                        <telerik:RadDatePicker ID="RadDatePickerFZRQ" runat="server" MinDate="2017-1-1" MaxDate="2050-1-1" Width="150px" Calendar-DayCellToolTipFormat="yyyy年MM月dd日">
                        </telerik:RadDatePicker>
                    </div>
                    <div class="floatL">
                     <asp:Button ID="ButtonSave" runat="server" Text="保存放号结果" CssClass="bt_large" UseSubmitBehavior="false" OnClientClick="this.value='正在提交';this.disabled=true;" onclick="ButtonSave_Click"/> 
                      <%--  <asp:Button ID="ButtonSave" runat="server" Text="保存放号结果" CssClass="bt_large" OnClick="ButtonSave_Click" />--%>
                    </div>
                     <div class="floatL">
                         
                        <asp:Button ID="ButtonOut" runat="server" Text="导出放号结果" CssClass="bt_large" OnClick="ButtonOut_Click"  />
                         <span id="spanOutput" runat="server" class="excel" style="padding-right:20px; font-weight:bold"></span>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
