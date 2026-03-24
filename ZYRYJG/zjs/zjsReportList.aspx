<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="zjsReportList.aspx.cs" Inherits="ZYRYJG.zjs.zjsReportList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc1" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc2" %>
<%@ Register Src="../CheckAll.ascx" TagName="CheckAll" TagPrefix="uc3" %>
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
        .link {
            border: none;
            color: #0000ff;
            background-color: transparent;
            cursor: pointer;
            font-size: 12px;
            margin-right: 12px;
        }

        #winpop {
            margin: 0;
            padding: 20px;
            overflow: hidden;
            display: none;
        }

            #winpop .title {
                width: 100%;
                height: 22px;
                line-height: 20px;
                background: #5DA2EF;
                font-weight: bold;
                text-align: center;
                font-size: 14px;
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
                    当前位置 &gt;&gt;造价工程师注册管理 &gt;&gt;<strong>区县汇总上报查询</strong>
                </div>
            </div>
            <div class="content">
              
            </div>
        </div>

        <uc2:IframeView ID="IframeView" runat="server" />

        <div id="winpop">
        </div>
    </form>
</body>
</html>
