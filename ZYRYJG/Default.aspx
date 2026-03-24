<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ZYRYJG.Default" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="renderer" content="webkit|ie-comp|ie-stand" />
    <title>北京市住房和城乡建设领域人员资格管理信息系统 7.37</title>
    <script type="text/javascript" src="./Scripts/jquery-3.4.1.min.js"></script>    
    <script src="Scripts/Public.js?v=1.005"></script>
    <link href="./css/menuRed.css?v=1.03" rel="stylesheet" />
     <link href="./css/defaultRed.css?v=1.01" rel="stylesheet" />
    <link rel="shortcut icon" type="image/x-icon" href="./Network Neighbourhood.ico" media="screen" /> 
   
    <script type="text/javascript">
        $(document).ready(
           function () {
               //setURL('Main.aspx'); 

               if (typeof cur == "undefined") {
                   setURL('Main.aspx');
               }
               else
               {                   
                   setURL(cur);
               }
               
               setPageSize();
               $(window).resize(function () {
                   setPageSize();
               });
           }
       );

        function setPageSize() {
            var wh = $(window).height() - 70;
            //var wh = $(window).height() - 70 - 130;
            $(".middle").height(wh);
            $("#right_side").height(wh);
            $("#mainFrame").height(wh);
            $("#left_side").height(wh);
            $(".split").height(wh);

            //获取左边宽度
            var leftwidth = $("#left_side").width();
            //alert(leftwidth);
            //可见宽度
            var xy = $(window).width();
            //右边宽度
            var rightwidth = 0;
            if (leftwidth == 0) {
                rightwidth = xy - $("#left_side").outerWidth() - 6 - 3;
            }
            else {
                rightwidth = xy - $("#left_side").outerWidth() - 5 - 6 - 6;
            }
            $("#right_side").width(rightwidth + "px");
        }

        // 设置PanelBar的链接
        function setURL(url) {
            $('.cd-nav-container, .cd-overlay').toggleClass('is-visible', false);
            $("#divMenu").width(0);
            $("#divalpha").width(0);
            if (url.indexOf("?") != -1)
                $("#mainFrame").attr('src', url + "&" + Math.random());
            else
                $("#mainFrame").attr('src', url + "?invoke=" + Math.random());
        }

        function SetCwinHeight() {
            var ifrmView = document.getElementById("mainFrame"); //iframe id
            document.documentElement.scrollTop = 0;
            if (document.getElementById) {
                if (ifrmView && !window.opera) {
                    if (ifrmView.contentDocument && ifrmView.contentDocument.body.offsetHeight) {
                        ifrmView.height = ifrmView.contentDocument.body.offsetHeight;

                    } else if (ifrmView.Document && ifrmView.Document.body.scrollHeight) {
                        ifrmView.height = ifrmView.Document.body.scrollHeight;

                    }
                }
            }
        }
        function hideshow() {
            if ($(".left").width() == 0) {
                $(".left").outerWidth("210px");
                $(".split").css("left", "215px");
                $(".right").css("left", "221px");
                $(".split").addClass("split_l");
                setPageSize();
            }
            else {
                $(".left").width("0px");
                $(".split").css("left", "6px");
                $(".right").css("left", "12px");
                $(".split").addClass("split_r");
                setPageSize();
            }
        }
    </script>
    <style type="text/css">
        .jd100{
            background:url(./images/jiandang100-2.jpg) top center no-repeat;
            background-size:cover;
            height:200px;
            width:100%;
            margin:0 0;
        }
        .jd100z{
            background:url(./images/jiandang100.jpg) top center no-repeat #BE1B2E;
            height:120px;
            width:100%;
            margin:0 0;
        }
    </style>
</head>
<body scroll="no">
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <%--<div class="jd100"></div>--%>
        <div class="header">
            <div class="header-left">
                <div class="sysName"><span style="font-size: 16px; padding-right:4px">北京市住房和城乡建设领域</span>人员资格管理信息系统</div>
       
            </div>

            <%--<div style="color: #FF0000">通知：由于北京市社会保险网上服务平台升级，2017年10月1日至10月9日暂停服务，我委社保比对服务同步暂停服务 </div>--%>
            <div class="user">
                <div style="float: left; padding-right: 10px;">
                    <span id="divNow" runat="server" class="time"></span>
                    <span class="userImg">当前用户：</span><span id="spanLogined" runat="server"></span>
                </div>
                <div class="headbtn">
                    <asp:Button ID="ButtonLogout" runat="server" Text="退出登录" CssClass="logout close" OnClick="LinkButtonLogout_Click" ForeColor="#f9f9f9" />
                </div>
                <div class="headbtn">
                    <span onclick="setURL('Main.aspx');" class="home">信息通知</span>
                </div>
                 <div class="headbtn">
                    <span onclick="setURL('linkme.html');" class="linkme">操作说明</span>
                </div>
            </div>
            <div class="header-right">
            </div>
        </div>

        <div class="middle">
            <div id="left_side" class="left" runat="server">
            </div>
            <div class="split split_l" onclick="hideshow();">
            </div>
            <div id="right_side" class="right">
                <iframe id="mainFrame" onload="javascript:SetCwinHeight();"
        style="background-color: White;  text-align: center; width:100%; overflow:scroll"  scrolling="auto"  frameborder="0"  marginwidth="0" marginheight="0"></iframe>

            </div>
        </div>
 
        <script type="text/javascript" src="./Scripts/menu_min.js"></script>
        <script>

            $(".menu ul li").menu();

            ////100年
            //setTimeout(function () {              
            //    $(".jd100").addClass("jd100z");
            //    $(".jd100").removeClass("jd100");
            //    setPageSize();
            //}, 10000);
        </script>
       
    </form>
</body>
</html>
