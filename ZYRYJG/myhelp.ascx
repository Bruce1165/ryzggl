<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="myhelp.ascx.cs" Inherits="ZYRYJG.myhelp" %>
    <script src="./Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
    <link href="./layer/skin/layer.css" rel="stylesheet" />
    <script src="./layer/layer.js" type="text/javascript"></script>
<div style="width:60px;text-align:left;top:0;float:right;padding-left:40px;cursor:pointer;z-index:9999999;background:url(../Images/help.png) no-repeat ; background-size:32px 32px ;background-position:left center;" onclick="javascript:help('<%=PageID %>')">帮助</div>
<asp:HiddenField ID="HiddenFieldHelpPageID" runat="server" />
<script type="text/javascript">
    function help(pid) {
      
        layer.open({
            type: 2,
            title: ['在线帮助' , 'font-weight:bold;background: #5DA2EF;color:#fefefe'],//标题
            maxmin: true, //开启最大化最小化按钮,
            //shade:0, 
            offset: [$(window).scrollTop() + 'px', $(window).width() - 600 + 'px'],// 'rt',
            area: ['600px', $(window).height() -12 +'px'],
            shadeClose: false, //点击遮罩关闭
            content: '../help/index.aspx?o=' + pid,
            resize: true,
            cancel: function (index, layero) {             
                layer.close(index);
                return false;
            }
        });
    }
</script>