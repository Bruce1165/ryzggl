<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Body.aspx.cs" Inherits="Body" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="Content/styleRed.css?v=1.0.5" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .yuan {
            float: left;
            width: 200px;
            height: 200px;
            background: #eee;
            border: 0px solid #eee;
            border-bottom: 3px solid #aaa;
            border-right: 4px solid #aaa;
            border-radius: 100px;
            text-align: center;
            vertical-align: middle;
            line-height: 200px;
            font-weight: bold;
            margin: 0px 50px;
        }

        .jt {
            float: left;
            /*width: 200px;*/
            height: 200px;
            font-weight: bolder;
            font-size: 72px;
            font-family: SimHei;
            line-height: 200px;
            vertical-align: middle;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="background: #fff; border-width: 1px; border-style: solid; border-color: #fff; border-radius: 10px 10px; padding: 20px 20px; min-height: 600px; margin-right: 35px">
            <div class="div_fun">
                学习流程
            </div>

            <div id="divMain" class="content" style="text-align: center; margin: 40px 40px;padding:40px 40px; border:1px dashed #ddd;">
                <div style="height:500px;background:url(./Img/step.png) no-repeat center center; background-size:contain;">
                  
                </div>
            </div>
        </div>
    </form>
</body>
</html>
