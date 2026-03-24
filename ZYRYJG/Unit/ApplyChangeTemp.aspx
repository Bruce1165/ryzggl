<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplyChangeTemp.aspx.cs" Inherits="ZYRYJG.Unit.ApplyChangeTemp" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
    <script src="../Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
    <link href="../layer/skin/layer.css" rel="stylesheet" />
    <script src="../layer/layer.js" type="text/javascript"></script>
    <style type="text/css">
        .barTitle {
            color: #434343;
            background-color: #E4E4E4;
            font-weight: bold;
            border-left: 5px solid #ff6a00;
            text-align: left;
        }

        .img {
            border: none;
            width: 0px;
        }

        .img200 {
            border: none;
            width: 200px;
        }

        td {
            line-height: 200%;
        }

        .subtable td {
            border: 1px solid #cccccc;
            border-collapse: collapse;
        }

        .addrow {
            float: right;
            background: url(../images/jiah.gif) no-repeat center center;
            width: 15px;
            height: 15px;
            padding-right: 20px;
        }
        .ts{
             line-height: 200%;
             color:#999;
             text-align:center;
        }     

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Windows7">
        </telerik:RadWindowManager>
        <div class="div_out">
            <div class="table_border" style="width: 99%; margin: 5px auto; padding-bottom: 30px;">
                <div id="div_top" class="dqts">
                    <div style="float: left;">
                        当前位置 &gt;&gt;事项申报 &gt;&gt;变更注册 &gt;&gt;<strong>个人信息变更（测试）</strong>
                    </div>
                </div>
                <div class="content">
                    <p class="jbxxbt">
                        个人信息变更
                    </p>
                    <div class="step">
                        <div class="stepLabel">办理进度：</div>                     
                        <div id="step_未申报" runat="server" class="stepItem lgray" >个人填写></div>
                         <div id="step_待确认" runat="server" class="stepItem lgray">待单位审核></div>
                        <div id="step_已申报" runat="server" class="stepItem lgray">已申报></div>
                        <div id="step_已受理" runat="server" class="stepItem lgray">区级受理></div>
                        <div id="step_区县审查" runat="server" class="stepItem lgray">区级审核></div>
                        <div id="step_已上报" runat="server" class="stepItem lgray">汇总上报></div>
                        <div id="step_已审查" runat="server" class="stepItem lgray">市级审核></div>
                        <div id="step_已决定" runat="server" class="stepItem lgray">市级决定></div>
                        <div id="step_已公示" runat="server" class="stepItem lgray">公示></div>
                        <div id="step_已公告" runat="server" class="stepItem lgray">公告(办结)</div>
                        <div class="stepArrow">▶</div>
                    </div>
                    
                </div>
            </div>
        </div>
        <div id="winpop">
        </div>
  
    </form>
</body>
</html>
