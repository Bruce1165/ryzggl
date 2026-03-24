<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Head.aspx.cs" Inherits="Head" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style>
        body {
            margin: 0 0;
        }

        .top {
            background-color: #FFF4F4;
            margin: 0 0;
            width: auto;
            height: 130px;
            background-repeat: no-repeat;
            border-bottom: 20px solid #FFF4F4;
            background: url(./img/top.jpg) left center no-repeat;
            background-size: cover;
        }

        .title {
            font-size: 36px;
            font-weight: bolder;
            font-family: 'Microsoft YaHei UI', 'Microsoft YaHei',SimHei,simsun, Arial, Helvetica, sans-serif, Lucida Grande;
            letter-spacing: 4px;
            color: #f3df90;
            line-height: 36px;
            text-align: center;
            padding: 30px 0 14px 0;
            text-shadow: 1px 1px 1px #666;
        }

        .fun {
            text-align: center;
            line-height: 49px;
            font-size: 18px;
            margin: 0px auto;
            border-bottom: 1px solid #ccc;
            vertical-align: middle;
            font-family: Microsoft YaHei, Arial;
            font-size: 18px;
            color: #fefefc;
            text-shadow: 1px 1px 1px #666;
        }

            .fun a {
                margin-left: 30px;
                font-family: Microsoft YaHei, Arial;
                font-size: 20px;
                color: #fcfcfc;
                text-decoration: none;
                padding: 4px 20px;
            }

        .loginer {
            vertical-align: middle;
            padding-right: 6px;
            height: 46px;
            color: #eee;
        }

        .loginer span {
            color: #eee;
            font-weight: bold;
            font-size: 20px;
        }

        .cur {
            font-size: 18px;
            background: rgba(200, 200, 200,0.4);
            border: 0px solid #eee;
            border-radius: 12px 12px;
            color: #fff;
            font-weight: bold;
        }

        .not {
            font-size: 18px;
            color: #fefefc;
        }
    </style>
    <script type="text/javascript" src="./Scripts/jquery-3.4.1.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="top">
            <div class="title">北京市建设行业从业人员公益培训平台</div>
            <div class="fun">
                <a class="cur" target="sFram" href="./Student/TopClass.aspx">首 页</a>
                <a class="not" target="sFram" href="./Student/WebClass.aspx?t=工匠讲堂">工匠讲堂</a>
                <a class="not" target="sFram" href="./Student/WebClass.aspx?t=首都建设云课堂">首都建设云课堂</a>
                <a class="not" target="sFram" href="./Student/WebClass.aspx?t=行业推荐精品课程">行业推荐精品课程</a>
                <a class="not" target="sFram" href="./sFram.aspx">个人空间</a>
                <div id="loginer" runat="server" class="loginer" style="float: right; padding-right: 20px"></div>
            </div>
        </div>
    </form>
    <script type="text/javascript">
        $(function () {
            $("a").bind("mousedown", function () {
                $("a.cur").addClass("not");
                $("a.cur").removeClass("cur");
                $(this).addClass("cur");
                $(this).removeClass("not");
            });
        })
    </script>
</body>
</html>
