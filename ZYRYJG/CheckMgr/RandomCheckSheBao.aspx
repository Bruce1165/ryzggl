<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RandomCheckSheBao.aspx.cs" Inherits="ZYRYJG.CheckMgr.RandomCheckSheBao" %>

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
        .divLeft {
            width: 15%;
            float: left;
            margin-left: 1%;
            overflow: auto;
            border: 1px solid #e1e1e1;
            margin-bottom: 40px;
        }

            .divLeft option {
                padding: 4px 0;
            }

        .divRight {
            width: 80%;
            float: left;
            clear: right;
            margin-left: 1%;
            overflow: auto;
            border: none;
            margin-bottom: 40px;
        }

        .divTop {
            width: 97%;
            margin: 0 auto 16px auto;
            clear: both;
            line-height: 150%;
            padding:12px 16px;
            background-color:#efefef;
        }

        .grid {
            width: 99%;
            margin: 0 auto;
            clear: both;
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
                    当前位置 &gt;&gt;综合监管 &gt;&gt;双随机检查&gt;&gt;<strong>摇号结果社保比对</strong>
                </div>
            </div>
            <div class="content">
               
                <div class="divLeft">
                    <div style="color: #fefefe; background: #CD5C5C; text-align: center; line-height: 200%; border: 1px solid #CD5C5C;">选择任务编号</div>
                    <asp:ListBox ID="ListBoxYhrwbh" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ListBoxYhrwbh_SelectedIndexChanged" Width="100%" Height="600px" Style="border: none; text-align: center; font-size: 16px;"></asp:ListBox>
                </div>
                <div class="divRight">
                    <div class="divTop">
                        <div class="row">任务编号：<asp:Label ID="LabelYhrwbh" runat="server" Text=""></asp:Label></div>
                        <div class="row">任务名称：<asp:Label ID="LabelRwmc" runat="server" Text=""></asp:Label></div>
                        <div class="row">摇号时间：<asp:Label ID="LabelYhsj" runat="server" Text=""></asp:Label></div>
                        <div class="row">抽查对象：<asp:Label ID="LabelCount" runat="server" Text=""></asp:Label></div>
                    </div>
                    <div class="grid">
                        <telerik:RadGrid ID="RadGridTask" AutoGenerateColumns="False" runat="server" AllowPaging="false" AllowCustomPaging="false" AllowSorting="false"
                            Skin="Blue" EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false" Width="99%" GridLines="None" HeaderStyle-Font-Bold="true">
                            <ClientSettings EnableRowHoverStyle="false">
                                <Selecting AllowRowSelect="false" />
                            </ClientSettings>
                            <MasterTableView CommandItemDisplay="None" NoMasterRecordsText="　没有可显示的记录">
                                <Columns>
                                    <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                        <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Left"  Wrap="false"/>
                                    </telerik:GridBoundColumn>
                                    <%--<telerik:GridBoundColumn UniqueName="Yhrwbh" DataField="Yhrwbh" HeaderText="任务编号">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="Rwmc" DataField="Rwmc" HeaderText="任务名称">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="Yhsj" DataField="Yhsj" HeaderText="摇号时间" DataFormatString="{0:yy.MM.dd}">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                </telerik:GridBoundColumn>--%>
                                    <telerik:GridBoundColumn UniqueName="Ccdx" DataField="Ccdx" HeaderText="抽查对象">
                                        <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="Zjhm" DataField="Zjhm"
                                        HeaderText="证件号码">
                                        <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="Szdw" DataField="Szdw" HeaderText="所在单位">
                                        <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="Rylx" DataField="Rylx" HeaderText="人员类型">
                                        <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <%-- <telerik:GridBoundColumn UniqueName="Zfjcbh" DataField="Zfjcbh" HeaderText="检查单编号">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="Zfr" DataField="Zfr" HeaderText="执法人">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="Ccsx" DataField="Ccsx" HeaderText="抽查事项">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>--%>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </div>
                </div>

            </div>
        </div>
    </form>
</body>
</html>
