<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewsView.aspx.cs" Inherits="ZYRYJG.Register.NewsView" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" />

    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
    <style type="text/css">
        .main {
            margin-left: 100px;
            margin-right: 100px;
            margin-top: 20px;
        }

        .title {
            text-align: center;
            width: 100%;
            font: bold 24px Microsoft YaHei,sans-serif;
            color: darkblue;
        }

        .content1 {
            margin-top: 20px;
            text-align: left;
            width: 100%;
            font: normal 16px Microsoft YaHei,sans-serif;
            color: #333;
            line-height: 200%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="div_out">
            <div id="div_top" class="dqts">
                <div style="float: left;">
                    当前位置 &gt;&gt;<strong>信息通知查看</strong>
                </div>
            </div>
            <div  style="max-width:85%; margin:20px auto">
                <div id="title" runat="server" class="title">
                </div>
                <div id="content" runat="server" class="content1">
                </div>
            </div>
        </div>
    </form>
</body>
</html>
