<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WenjuanTj.aspx.cs" Inherits="ZYRYJG.jxjy.WenjuanTj" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" />
    <link href="../Skins/Blue/Grid.Blue.css?v=1.008" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
    <script src="../Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
    <link href="../layer/skin/layer.css" rel="stylesheet" />
    <script src="../layer/layer.js" type="text/javascript"></script>
    <style>
        body {
            background-color: #fff;
        }

        .div {
            border: 1px solid #ececec;
            border-radius: 10px 10px;
            padding: 20px 20px;
            margin: 0 5px;
            color: #333;
            background: #fafefa;
            line-height: 200%;
            text-align: justify;
        }

            .div .row {
            }
            option
            {
                padding:4px 0!important;
            }

            .div .rb {
                padding-left: 42px;
            }

            .div .num {
                border: 1px solid #0e72bd;
                border-radius: 12px 12px;
                padding: 4px 5px;
                margin: 0;
                color: #fefefe;
                background: #1558FF;
                text-align: center;
            }

            .div .title {
                margin-left: 20px;
            }

            .div .type {
                text-align: right;
                color: #1558FF;
            }

        .headTitle {
            line-height: 300%;
            font-size: 24px;
            color: #1558FF;
            font-weight: bold;
            text-align:center;
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
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true" Skin="Default" />
        <div class="div_out">
            <div id="div_top" class="dqts">
                <div style="float: left;">
                    当前位置 &gt;&gt;在线公益教育培训 &gt;&gt;<strong>年度调查统计</strong>
                </div>
            </div>
            <div class="content">
                <div class="headTitle">
                    公益培训年度问卷调查
                </div>
                <div id="divLeft" style="width: 150px; float: left; margin-left: 1%; overflow: auto; border: 1px solid #e1e1e1; margin-bottom: 40px">
                    <div style=" color:#fefefe;background: #1558FF;text-align:center;line-height:200%;border: 1px solid #1558FF; ">选择统计年度</div>
                    <asp:ListBox ID="ListBoxYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ListBoxYear_SelectedIndexChanged" width="100%" height="500px" style="border:none;text-align:center;font-size:16px;"></asp:ListBox>
                 </div>
                 <div id="divRight" style="min-width: 80%; float: left; clear: right; margin-left: 1%; overflow: auto; border: none; margin-bottom: 40px">
                       <telerik:RadGrid ID="RadGridQuestion" AutoGenerateColumns="False"
                    runat="server"
                    AllowPaging="false" AllowSorting="false"
                    EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false" Width="98%" ShowHeader="false"
                    GridLines="None">
                    <ClientSettings EnableRowHoverStyle="true">
                    </ClientSettings>
                    <MasterTableView NoMasterRecordsText="　没有可显示的记录">
                        <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                        <Columns>
                            <telerik:GridTemplateColumn UniqueName="QuestionID">
                                <ItemTemplate>
                                    <div class="div">
                                        <div class="row">
                                            <span class="num"><%# string.Format("{0:00}", Eval("QuestionNo"))%></span><span class="title"><%# Eval("Title")%></span><span class="type">单选</span>
                                        </div>
                                        <div class="row rb">
                                            <asp:RadioButton ID="RadioButtonA" Enabled="false" runat="server" Text='<%# Eval("A")%>' Visible='<%# Eval("A") != DBNull.Value%>' GroupName='<%# Eval("QuestionID")%>'  /><%# Eval("A")== DBNull.Value?"":string.Format("（{0}%）<br />",Eval("Apre"))%>
                                            <asp:RadioButton ID="RadioButtonB" Enabled="false" runat="server" Text='<%# Eval("B")%>' Visible='<%# Eval("B") != DBNull.Value%>' GroupName='<%# Eval("QuestionID")%>'  /><%# Eval("B")== DBNull.Value?"":string.Format("（{0}%）<br />",Eval("Bpre"))%>
                                            <asp:RadioButton ID="RadioButtonC" Enabled="false" runat="server" Text='<%# Eval("C")%>' Visible='<%# Eval("C") != DBNull.Value%>' GroupName='<%# Eval("QuestionID")%>'  /><%# Eval("C")== DBNull.Value?"":string.Format("（{0}%）<br />",Eval("Cpre"))%>
                                            <asp:RadioButton ID="RadioButtonD" Enabled="false" runat="server" Text='<%# Eval("D")%>' Visible='<%# Eval("D") != DBNull.Value%>' GroupName='<%# Eval("QuestionID")%>'  /><%# Eval("D")== DBNull.Value?"":string.Format("（{0}%）<br />",Eval("Dpre"))%>
                                            <asp:RadioButton ID="RadioButtonE" Enabled="false" runat="server" Text='<%# Eval("E")%>' Visible='<%# Eval("E") != DBNull.Value%>' GroupName='<%# Eval("QuestionID")%>'  /><%# Eval("E")== DBNull.Value?"":string.Format("（{0}%）<br />",Eval("Epre"))%>
                                            <asp:RadioButton ID="RadioButtonF" Enabled="false" runat="server" Text='<%# Eval("F")%>' Visible='<%# Eval("F") != DBNull.Value%>' GroupName='<%# Eval("QuestionID")%>'  /><%# Eval("F")== DBNull.Value?"":string.Format("（{0}%）<br />",Eval("Fpre"))%>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
                 </div>
            </div>
        </div>
    </form>
</body>
</html>
