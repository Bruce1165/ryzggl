<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AgencyFinish.aspx.cs" Inherits="ZYRYJG.County.AgencyFinish" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" />

    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
    <%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc1" %>
    <%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc2" %>

    <style type="text/css">
        .round_div {
            border-radius: 8px;
            box-shadow: 0 1px 1px rgba(0, 0, 0, 0.05);
            background-color: #EFEFFE;
            width: 202px;
            float: left;
            margin-right: 10px;
            margin-top: 5px;
            margin-bottom: 10px;
            font: normal normal 600 16px '微软雅黑';
            vertical-align: middle;
            line-height: 50px;
            padding: 10px 0px 30px 70px;
            color: black;
        }

        .clearR {
            clear: right;
        }


        a {
            font-size: 24px;
            color: #696969;
        }

        .zc_cs {
            background: url(../images/zc_cs.png) no-repeat #EFEFFE;
            background-position-x:10%;
            background-position-y:50%;
        }

        .zc_cx {
            background: url(../images/zc_cx.png) no-repeat center center #EFEFFE;
            background-position-x:10%;
            background-position-y:50%;
        }

        .zc_zx {
            background: url(../images/zc_zx.png) no-repeat center center #EFEFFE;
            background-position-x:10%;
            background-position-y:50%;
        }

        .zc_yq {
            background: url(../images/zc_yq.png) no-repeat center center #EFEFFE;
            background-position-x:10%;
            background-position-y:50%;
        }

        .zc_bb {
            background: url(../images/zc_bb.png) no-repeat center center #EFEFFE;
            background-position-x:10%;
            background-position-y:50%;
        }

        .zc_cancel {
            background: url(../images/zc_cancel.png) no-repeat center center #EFEFFE;
            background-position-x:10%;
            background-position-y:50%;
        }
        .zc_grbg {
            background: url(../images/zc_grbg.png) no-repeat center center #EFEFFE;
            background-position-x:10%;
            background-position-y:50%;
        }
        .zc_zybg {
            background: url(../images/zc_zybg.png) no-repeat center center #EFEFFE;
            background-position-x:10%;
            background-position-y:50%;
        }
        .zc_qybg {
            background: url(../images/zc_qybg.png) no-repeat center center #EFEFFE;

            background-position-x:10%;
            background-position-y:50%;
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
        <div class="div_out">
            <div class="dqts">
                <div style="float: left;">
                    当前位置 &gt;&gt;建造师注册管理 &gt;&gt;<strong>已办业务</strong>
                </div>
            </div>
           
            <div class="content">
                <div style="width: 95%; margin: 10px auto; text-align: center; height: 400px;">
                    <div style="text-align: left; color: darkorange; font-size: 16px; font-weight: bold; width: 100%">这是您本人办理完成的任务。</div>


                    <div class='round_div zc_cs'>

                        <div>初始注册</div>
                        <a href='BusinessFinishList.aspx?type=初始注册'>
                            <asp:Label ID="LabelFirst" runat="server" Text="0"></asp:Label>条</a>
                    </div>
                    <div class='round_div zc_cx'>
                        <div>重新注册</div>
                        <a href='BusinessFinishList.aspx?type=重新注册'>
                            <asp:Label ID="LabelRenew" runat="server" Text="0"></asp:Label>条</a>
                    </div>

                    <div class='round_div zc_zx'>
                        <div>增项注册</div>
                        <a href='BusinessFinishList.aspx?type=增项注册'>
                            <asp:Label ID="LabelAddItem" runat="server" Text="0"></asp:Label>
                            条</a>
                    </div>
                  
                    <div id="divYQZC" runat="server" class='round_div zc_yq'>
                        <div>延续注册</div>
                        <a href='BusinessFinishList.aspx?type=延期注册'>
                            <asp:Label ID="LabelContinue" runat="server" Text="0"></asp:Label>条</a>
                    </div>
                    <div id="divYSWSBB" runat="server" class='round_div zc_bb' visible="false">
                        <div>遗失（污损）补办</div>
                        <a href='BusinessFinishList.aspx?type=遗失补办'>
                            <asp:Label ID="LabelReplace" runat="server" Text="0"></asp:Label>条</a>
                    </div>
                    <div id="divZXZC" runat="server" class='round_div zc_cancel'>
                        <div>注销注册</div>
                        <a href='BusinessFinishList.aspx?type=注销'>
                            <asp:Label ID="LabelCancel" runat="server" Text="0"></asp:Label>条</a>
                    </div>
                    
                    <div id="GRXX" runat="server" class='round_div zc_grbg'>
                        <div>个人信息变更</div>
                        <a href='BusinessFinishList.aspx?type=个人信息变更'>
                            <asp:Label ID="LabelChangeGR" runat="server" Text="0"></asp:Label>条</a>
                    </div>

                    <div id="QYBG" runat="server" class='round_div zc_zybg'>
                        <div>执业企业变更</div>
                        <a href='BusinessFinishList.aspx?type=执业企业变更'>
                            <asp:Label ID="LabelChangeZY" runat="server" Text="0"></asp:Label>条</a>
                    </div>

                    <div id="XXBG" runat="server" class='round_div zc_qybg'>
                        <div>企业信息变更</div>
                        <a href='Examine.aspx?type=企业信息变更'>
                            <asp:Label ID="LabelChangeQY" runat="server" Text="0"></asp:Label>条</a>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

