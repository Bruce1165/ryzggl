<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QuestionGet.aspx.cs" Inherits="Student_QuestionGet" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Content/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" />
    <link href="../Skins/Blue/Grid.Blue.css?v=1.008" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
    <script src="../Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
    <link href="../layer/skin/layer.css" rel="stylesheet" />
    <script src="../layer/layer.js" type="text/javascript"></script>
    <style>
        body{
            background-color:#efefef;
        }
        .div {
            border: 1px solid #fcfcfc;
            border-radius: 10px 10px;
            padding: 20px 20px;
            margin: 0 5px;
            color: #333;
            background: #fefefe;
            line-height: 200%;
            text-align:justify;
        }

            .div .row {
            }

            .div .rb {
                padding-left:42px;
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
                margin-left:20px;                
            }

            .div .type {
                text-align:right;
                 color: #1558FF;
            }
            .headTitle{
                line-height:300%;
                font-size:24px;
                color:#1558FF;
                font-weight:bold;
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
        <div style="width: 100%; text-align: center">
            <div class="headTitle">
                公益培训年度问卷调查
            </div>
            <telerik:RadGrid ID="RadGridQuestion" AutoGenerateColumns="False"
                runat="server"
                AllowPaging="false" AllowSorting="false"
                 EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false" Width="98%" ShowHeader="false"
                GridLines="None">
                <ClientSettings EnableRowHoverStyle="true">
                </ClientSettings>
                <MasterTableView DataKeyNames="QuestionID,WorkerCertificateCode,SaveYear"
                    NoMasterRecordsText="　没有可显示的记录">
                    <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="QuestionID">
                            <ItemTemplate>
                                <div class="div">
                                    <div class="row">
                                        <span class="num"><%# string.Format("{0:00}", Eval("QuestionNo"))%></span><span class="title"><%# Eval("Title")%></span><span class="type">单选</span>
                                    </div>
                                    <div class="row rb">
                                        <asp:RadioButton ID="RadioButtonA" runat="server" Text='<%# Eval("A")%>' Visible='<%# Eval("A") != DBNull.Value%>' GroupName='<%# Eval("QuestionID")%>' Checked='<%# Eval("CheckItem") != DBNull.Value && Eval("CheckItem").ToString()=="A" %>' />
                                        <asp:RadioButton ID="RadioButtonB" runat="server" Text='<%# Eval("B")%>' Visible='<%# Eval("B") != DBNull.Value%>' GroupName='<%# Eval("QuestionID")%>' Checked='<%# Eval("CheckItem") != DBNull.Value && Eval("CheckItem").ToString()=="B" %>' />
                                        <asp:RadioButton ID="RadioButtonC" runat="server" Text='<%# Eval("C")%>' Visible='<%# Eval("C") != DBNull.Value%>' GroupName='<%# Eval("QuestionID")%>' Checked='<%# Eval("CheckItem") != DBNull.Value && Eval("CheckItem").ToString()=="C" %>' />
                                        <asp:RadioButton ID="RadioButtonD" runat="server" Text='<%# Eval("D")%>' Visible='<%# Eval("D") != DBNull.Value%>' GroupName='<%# Eval("QuestionID")%>' Checked='<%# Eval("CheckItem") != DBNull.Value && Eval("CheckItem").ToString()=="D" %>' />
                                        <asp:RadioButton ID="RadioButtonE" runat="server" Text='<%# Eval("E")%>' Visible='<%# Eval("E") != DBNull.Value%>' GroupName='<%# Eval("QuestionID")%>' Checked='<%# Eval("CheckItem") != DBNull.Value && Eval("CheckItem").ToString()=="E" %>' />
                                        <asp:RadioButton ID="RadioButtonF" runat="server" Text='<%# Eval("F")%>' Visible='<%# Eval("F") != DBNull.Value%>' GroupName='<%# Eval("QuestionID")%>' Checked='<%# Eval("CheckItem") != DBNull.Value && Eval("CheckItem").ToString()=="F" %>' />
                                    </div>
                                </div>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
            <div style="text-align: center;margin:40px 0">
                <asp:Button ID="ButtonSave" runat="server" CssClass="bt_large" Text="提交问卷" OnClick="ButtonSave_Click" />
            </div>
          
        </div>
    </form>
</body>
</html>
