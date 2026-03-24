<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CertLockView.aspx.cs" Inherits="ZYRYJG.County.CertLockView" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" /> 
    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
    <style type="text/css">
        #main th,tr {
            line-height:22px;

        }
         #main th {
            line-height:22px;
            background-color:#efefef;
        }

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="div_out">
            <div class="dqts">
                <div style="float: left;">
                    当前位置 &gt;&gt;<strong>证书异常锁定信息查看</strong>
                </div>
            </div>

            <div class="jbxxbt">
                证书异常锁定信息查看
            </div>           
            <div id="main" runat="server" class="content" style="margin: 8px 20px; background:url(../Images/lock1.png) no-repeat 40px center">
            </div>
            <div style="text-align: center; line-height: 40px;width:90%;"> <input id="ButtonReturn" type="button" value="返 回" class="button" onclick="javascript: hideIfam();" /></div>
        </div>
    </form>
</body>
</html>
