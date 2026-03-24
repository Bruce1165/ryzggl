<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShebaoReSend.aspx.cs" Inherits="ZYRYJG.SystemManage.ShebaoReSend" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../CheckAll.ascx" TagName="CheckAll" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head id="Head1" runat="server">
    <title></title>
    <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" /> 
    <script src="../Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
        <link href="../layer/skin/layer.css" rel="stylesheet" />
    <script src="../layer/layer.js" type="text/javascript"></script>

    <style type="text/css">
        .div_out div{
             margin:5px;
        }
    </style>
</head>
<body>  
    <form ID="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />   

            <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
            </telerik:RadCodeBlock>
            <telerik:RadWindowManager ID="RadWindowManager1" ShowContentDuringLoad="false" VisibleStatusbar="false"
                ReloadOnShow="true" runat="server" Skin="Sunset" EnableShadow="true">
            </telerik:RadWindowManager>
            <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1"
                EnableAJAX="true">
            </telerik:RadAjaxManager>
            <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true" />
            <div class="div_out">
                <div class="dqts">
                    <div style="float: left;">
                        <img alt="" src="../Images/ts.gif" width="17" height="11" />当前位置 
                        信息查看 &gt;&gt; <strong>重发社保</strong>
                    </div>
                </div>
                <div class="table_border" style="width: 99%; margin: 15px auto;">
                    <div class="content">
                        <div style="text-align:center;font-size:24px;font-weight:bold">
                            社保重新发送
                        </div>
                        <div style="width: 100%; margin: 15px auto; text-align: left">
                            <div class="table_cx">
                                <img alt="" src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                                查询条件：
                            </div>
                            <div style="width: 95%; background-color: #EFEFFE; padding: 12px 20px">
                                <div class="RadPicker">
                                    单位名称：<telerik:RadTextBox ID="RadTextBoxUnitName" runat="server" Width="180px" Skin="Default">
                                    </telerik:RadTextBox>
                                    &nbsp;&nbsp;组织机构代码：
                                   <telerik:RadTextBox ID="RadTextBoxUnitCode" runat="server" Width="80px" Skin="Default"
                                       MaxLength="9">
                                   </telerik:RadTextBox>
                                </div>

                                <div class="RadPicker">&nbsp;&nbsp;&nbsp;&nbsp;业务类型：</div>
                                <div class="RadPicker">
                                    <asp:RadioButtonList ID="RadioButtonListType" runat="server" RepeatDirection="Horizontal"
                                        Style="float: left;">
                                        <asp:ListItem Value="" Selected="True">全部</asp:ListItem>
                                        <asp:ListItem Value="初始">初始</asp:ListItem>
                                        <asp:ListItem Value="重新">重新</asp:ListItem>
                                        <asp:ListItem Value="变更">变更</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                                <div class="RadPicker" style="padding-left: 50px">
                                    <asp:Button ID="btnSearch" runat="server" Text="查 询" CssClass="button" OnClick="btnSearch_Click" />
                                </div>
                                <div style="clear: both;"></div>
                            </div>
                            <div style="clear: both;"></div>
                             <div class="table_cx">
                                <img alt="" src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                                社保法人库：
                            </div>
                            <div id="divFR" runat="server" style="width: 95%; padding: 20px 20px; font-weight: bolder; background-color: #EFEFFE;">
                                组织机构代码：<telerik:RadTextBox ID="RadTextBoxYZZJGDM" runat="server" Width="80px" Skin="Default"
                                    MaxLength="8">
                                </telerik:RadTextBox>（8位）
                                &nbsp;&nbsp;统一社会信用代码：<telerik:RadTextBox ID="RadTextBoxTYSHXYDM" runat="server" Width="150px" Skin="Default"
                                    MaxLength="18">
                                </telerik:RadTextBox>
                                &nbsp;&nbsp;数据来源：<telerik:RadTextBox ID="RadTextBoxSJLY" runat="server" Width="80px" Skin="Default"
                                    MaxLength="50">
                                </telerik:RadTextBox>
                                &nbsp;&nbsp;是否有效：<asp:CheckBox ID="CheckBoxValid" runat="server" />（勾选表示有效）
                                &nbsp;&nbsp;<asp:Button ID="ButtonResetUnitCode" runat="server" Text="更 新" CssClass="button" OnClick="ButtonResetUnitCode_Click" />
                            </div>

                             <div class="table_cx">
                                <img alt="" src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                                业务列表：
                            </div>

                            <telerik:RadGrid ID="RadGrid1" runat="server" AutoGenerateColumns="False" AllowSorting="false" AllowPaging="false"
                                GridLines="None" CellPadding="0" Width="98%" Skin="Blue" EnableAjaxSkinRendering="false"
                                EnableEmbeddedSkins="false">
                                <ClientSettings EnableRowHoverStyle="true">
                                    <Selecting AllowRowSelect="True" />
                                </ClientSettings>
                                <MasterTableView DataKeyNames="CHECKID" NoMasterRecordsText="没有可显示的记录">
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
                                        <telerik:GridBoundColumn UniqueName="cjsj" DataField="cjsj" HeaderText="申请日期"
                                            HtmlEncode="false" DataFormatString="{0:yyyy.MM.dd}" AllowSorting="false">
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="applytype" DataField="applytype" HeaderText="业务类型">
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>

                                        <telerik:GridBoundColumn UniqueName="psn_name" DataField="psn_name" HeaderText="姓名">
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="PSN_CertificateNO" DataField="PSN_CertificateNO"
                                            HeaderText="证件号码">
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="ent_name" DataField="ent_name" HeaderText="单位名称">
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="ENT_OrganizationsCode" DataField="ENT_OrganizationsCode" HeaderText="组织机构代码">
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="checkresult" DataField="checkresult" HeaderText="社保对比结果">
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>

                                    </Columns>

                                    <HeaderStyle Font-Bold="True" />
                                </MasterTableView>
                            </telerik:RadGrid>
                        </div>
                        <asp:Button ID="ButtonReSend" runat="server" Text="重发社保" CssClass="bt_large" OnClick="ButtonReSend_Click" />
                    </div>
                </div>
            </div>
          <div id="winpop">
        </div>
    </form>
</body>
</html>