<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CertCodeBarPrint.aspx.cs" Inherits="ZYRYJG.County.CertCodeBarPrint" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" /> 
    
    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
    <script type="text/javascript">




        //检验浏览器ActiveX权限
        function checkActiveXEnable() {
            var canActiveX = false;
            try {
                var wshShell = new ActiveXObject("WScript.Shell");
                canActiveX = true;
            } catch (e) { }
            if (canActiveX == false) {
                alert("您目前的IE设置不能打印，请将本网站加入可信任站点，并修改可信任站点的自定义级别，勾选“对没有标记为安全的ActiveX控件进行初始化和运行”,然后重新启动浏览器。（详细设置方法请阅读系统帮助）");
                location.href = location.href;
            }
            return canActiveX;
        }

        //打印word
        var word = null;
        function Print(url) {
            if (checkActiveXEnable() == false) {
                return;
            }
            try {
                word = new ActiveXObject("Word.Application");
                word.visible = false;

                var oWB = word.Documents.Open(url)
                //                 word.Application.Activate();

                oWB.Application.DisplayAlerts = false;
                oWB.PrintOut();
                oWB.Saved = true
                //                 word.Application.Quit();
                //                 word.Application.DisplayAlerts = false;
                //                 word = null;
                idTmr = window.setInterval("Cleanup();", 1500); //1秒后关闭word

            }
            catch (e) {
                try {
                    word.Application.Quit();
                    word = null;
                }
                catch (ex)
                { }
                idTmr = window.setTimeout("Cleanup();", 1000); //1秒后关闭word
                location.href = location.href;
                alert("您目前的IE设置不能打印，请将本网站加入可信任站点，并修改可信任站点的自定义级别，勾选“对没有标记为安全的ActiveX控件进行初始化和运行”！");

            }
        }
        var idTmr = "";

        function Cleanup() {
            word.Application.Quit();
            word = null;
            window.clearTimeout(idTmr);
            CollectGarbage();
        }


    </script>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server">
        </telerik:RadStyleSheetManager>
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Windows7">
        </telerik:RadWindowManager>
        <div class="div_out">
            <div class="table_border" style="width: 99%; margin: 5px auto; padding-bottom: 30px;">
                <div class="dqts">
                    <div style="float: left;">
                        当前位置 &gt;&gt;业务管理 &gt;&gt;证书打印&gt;&gt;<strong>打印防伪条</strong>
                    </div>
                </div>
                <div class="content">
                    <div style="width: 95%; margin: 10px auto; text-align: center;">
                        
                        <div runat="server" id="P_PrintTimeSpan" style="width: 18.2cm; padding-bottom: 8px; text-align: center;">
                            <div style="float: left;">
                                <asp:CheckBox ID="CheckBoxAutoPrint" runat="server" Text="自动完成批量打印" TextAlign="Left" />&nbsp;&nbsp;&nbsp;时间间隔：
                            </div>
                            <div style="float: left;">
                                <asp:RadioButtonList ID="RadioButtonListPrintTimeSpan" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="5秒（默认）" Value="5" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="10秒" Value="10秒"></asp:ListItem>
                                    <asp:ListItem Text="20秒" Value="20"></asp:ListItem>
                                    <asp:ListItem Text="30秒" Value="30"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                        <asp:Button ID="ButtonPrev" runat="server" Text="上一条" CssClass="button" Visible="False"
                            OnClick="ButtonPrev_Click" />
                        &nbsp;&nbsp;

                          &nbsp;&nbsp;
                       
                    <asp:Button ID="ButtonPrint" runat="server" Text="打 印" CssClass="button" OnClick='ButtonPrint_Click'
                        ToolTip="提示：直接按回车键（Enter）也可打印" />
                        &nbsp;&nbsp;
                    <asp:Button ID="ButtonNext" runat="server" Text="下一条" CssClass="button" Visible="False"
                        OnClick="ButtonNext_Click" />
                        &nbsp;&nbsp;
                   
                          <input id="BtnReturn2" type="button" class="button" value="返 回" onclick='javascript: hideIfam()' />
                    </div>
                     <div style="text-align:center;width: 800px;margin-left: auto; margin-right: auto; ">
                            <telerik:RadComboBox ID="RadComboBoxBeginNo" runat="server" Width="50"   Label="选择打印起始位置：" OnSelectedIndexChanged="RadComboBoxBeginNo_SelectedIndexChanged" AutoPostBack="true">
                                <Items>
                                    <telerik:RadComboBoxItem Text="1" Value="1" Selected="true" />
                                    <telerik:RadComboBoxItem Text="2" Value="2" />
                                    <telerik:RadComboBoxItem Text="3" Value="3" />
                                    <telerik:RadComboBoxItem Text="4" Value="4" />
                                    <telerik:RadComboBoxItem Text="5" Value="5" />
                                    <telerik:RadComboBoxItem Text="6" Value="6" />
                                    <telerik:RadComboBoxItem Text="7" Value="7" />
                                    <telerik:RadComboBoxItem Text="8" Value="8" />
                                    <telerik:RadComboBoxItem Text="9" Value="9" />
                                    <telerik:RadComboBoxItem Text="10" Value="10" />
                                    <telerik:RadComboBoxItem Text="11" Value="11" />
                                    <telerik:RadComboBoxItem Text="12" Value="12" />
                                </Items>
                            </telerik:RadComboBox>
                        </div>
                    <div style="width: 95%; margin: 10px auto; text-align: center;">
                        <asp:Label ID="LabelPage" runat="server" Text="" Font-Bold="true" Visible="false"></asp:Label>
                    </div>
                    <div id="divPhoto" runat="server" style="width: 700px; height: 989px; margin-left: auto; margin-right: auto; text-align: left; vertical-align: top; vertical-align: middle; background: url(../images/CodeBarBG.jpg); background-repeat: no-repeat; background-position-x: left; background-position-y: top; background-size: cover; position:relative;">
                    </div>
                </div>
            </div>
        </div>
        <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Enabled="false" Interval="10000"
            EnableViewState="true">
        </asp:Timer>
    </form>
</body>
</html>
