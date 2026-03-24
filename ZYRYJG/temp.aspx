<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="temp.aspx.cs" Inherits="ZYRYJG.temp" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="./Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="Scripts/BigInt.js"></script>
    <script type="text/javascript" src="Scripts/Barrett.js"></script>
    <script type="text/javascript" src="Scripts/RSA.js"></script>
    <script type="text/javascript" src="Scripts/CodeManage.js"></script>
    <link href="layer/skin/layer.css" rel="stylesheet" />
    <script src="layer/layer.js" type="text/javascript"></script>

    <style type="text/css">
        .body {
            font-size: 16px;
            background-color: red;
        }

        .main div {
            width: 100%;
            margin: auto auto;
            /*border:1px solid #ccc;
            border-radius:5px;*/
            line-height: 120%;
            /*background:url(./images/man.png) no-repeat 20px center #cccccc;*/
            text-align: center;
        }

        .main input {
            width: 700px;
            margin: 10px auto;
            border: 1px solid #F5F5FF;
            border-radius: 5px;
            line-height: 120%;
            background: url(./images/man.png) no-repeat 20px center #F5F5FF;
            text-align: left;
            padding: 20px 0 20px 40px;
            cursor: pointer;
            font-size: 16px;
        }

        .mainqy {
            width: 560px;
            margin: 10px auto;
            border: 1px solid #F5F5FF;
            border-radius: 5px;
            line-height: 100%;
            text-align: left;
            padding: 40px 40px;
            font-size: 16px;
            background: url(./images/bg.jpg) no-repeat 20px center #F5F5FF;
            background-size: 320px 320px;
            padding-left: 400px;
        }

        .mainqy2 {
            width: 560px;
            margin: 10px auto;
            border: 1px solid #F5F5FF;
            border-radius: 5px;
            line-height: 100%;
            text-align: left;
            padding: 40px 40px;
            font-size: 16px;
            padding-left: 400px;
        }
          .mainqy3{
            width: 920px;
            margin: 10px auto;
            border: 1px solid #F5F5FF;
            border-radius: 5px;
            line-height: 100%;
            text-align: left;
            padding: 40px 40px;
            font-size: 16px;
        }

        .hide {
            display: block;
        }

        .button {
            width: 160px;
            height: 34px;
            font-size: 16px;
            background-color: #5C9DD3;
            border: 1px solid #2F8399;
            border-radius: 3px;
            color: white;
            cursor: pointer;
        }
    </style>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <%--<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">--%>
        <script type="text/javascript">

            //var browser = navigator.appName;
            //var b_version = navigator.appVersion;
            //var version = parseFloat(b_version);

            //alert(browser + '：' + version)


            //var uA = navigator.userAgent;
            //var browserType = "unknown";
            //if (uA.indexOf("Opera") > -1) {
            //    browserType = "Opera";
            //} else if (uA.indexOf("Safari") > -1) {
            //    browserType = "Safari";
            //} else if (uA.indexOf("Konqueror") > -1) {
            //    browserType = "Konqueror";
            //} else if (uA.indexOf("Gecko") > -1) {
            //    browserType = "Mozilla";
            //} else if (uA.indexOf("MSIE") > -1) {
            //    browserType = "Internet Explorer";
            //}
            ////window.alert(browserType);
            //document.write(browserType);
            //document.write("<br/>");
            //document.write(document.lastModified); //显示页面的修改日期
            //document.write("<br/>");
            //document.write(navigator.userAgent); //显示客户端环境参数
            //document.write("<br/>");
            //document.write("h,w:" + screen.availHeight + "x" + screen.availWidth + "pixels");//测定显示器大小

            $(document).ready(

            function () {

                document.getElementById('ImgCode').src = 'ValidateCode.aspx?o=' + Math.random();
            });

            function cmdEncrypt() {
                if (document.getElementById("<%=TextBoxUserName.ClientID %>") != null && document.getElementById("<%=TextBoxUserName.ClientID %>").value == "") {
                        layer.alert('请输入用户名！', { offset: '100px', icon: 5, time: 0, area: ['500px', 'auto'] });
                        document.getElementById("<%=TextBoxUserName.ClientID %>").focus();
                         return (false);
                     }
                     if (document.getElementById("<%=TextPW.ClientID %>") != null && document.getElementById("<%=TextPW.ClientID %>").value == "") {
                        layer.alert('请输入密码！', { offset: '100px', icon: 5, time: 0, area: ['500px', 'auto'] });
                        document.getElementById("<%=TextPW.ClientID %>").focus();
                         return (false);
                     }
                     if (document.getElementById("<%=txtValidator.ClientID %>") != null && document.getElementById("<%=txtValidator.ClientID %>").value == "") {
                        layer.alert('请输入验证码！', { offset: '100px', icon: 5, time: 0, area: ['500px', 'auto'] });
                        document.getElementById("<%=txtValidator.ClientID %>").focus();
                         return (false);
                     }

                    //
                     var encodedString = Base64.encode(document.getElementById("<%=TextBoxUserName.ClientID %>").value + "\\" + document.getElementById("<%=TextPW.ClientID %>").value);
                    document.getElementById("<%=HiddenFieldLogin.ClientID %>").value = encodedString;
                    document.getElementById("<%=TextBoxUserName.ClientID %>").value = "";
                    document.getElementById("<%=TextPW.ClientID %>").value = "";
                    return (true);
                }

                function cmdEncrypt_w() {
                    if (document.getElementById("<%=RadTextBoxCID.ClientID %>") != null && document.getElementById("<%=RadTextBoxCID.ClientID %>").value == "") {
                         layer.alert('请输入用户名！', { offset: '100px', icon: 5, time: 0, area: ['500px', 'auto'] });
                         document.getElementById("<%=RadTextBoxCID.ClientID %>").focus();
                         return (false);
                     }
                     if (document.getElementById("<%=TextPW.ClientID %>") != null && document.getElementById("<%=TextPW.ClientID %>").value == "") {
                         layer.alert('请输入密码！', { offset: '100px', icon: 5, time: 0, area: ['500px', 'auto'] });
                         document.getElementById("<%=TextPW.ClientID %>").focus();
                         return (false);
                     }
                     if (document.getElementById("<%=txtValidator.ClientID %>") != null && document.getElementById("<%=txtValidator.ClientID %>").value == "") {
                         layer.alert('请输入验证码！', { offset: '100px', icon: 5, time: 0, area: ['500px', 'auto'] });
                         document.getElementById("<%=txtValidator.ClientID %>").focus();
                         return (false);
                     }

                     //
                     var encodedString = Base64.encode(document.getElementById("<%=RadTextBoxCID.ClientID %>").value + "\\" + document.getElementById("<%=TextPW.ClientID %>").value);
                     document.getElementById("<%=HiddenFieldLogin.ClientID %>").value = encodedString;
                     document.getElementById("<%=RadTextBoxCID.ClientID %>").value = "";
                     document.getElementById("<%=TextPW.ClientID %>").value = "";
                     return (true);
                 }

                 function cmdEncrypt_u() {
                     if (document.getElementById("<%=RadTextBoxZZJGDM.ClientID %>") != null && document.getElementById("<%=RadTextBoxZZJGDM.ClientID %>").value == "") {
                         layer.alert('请输入用户名！', { offset: '100px', icon: 5, time: 0, area: ['500px', 'auto'] });
                         document.getElementById("<%=RadTextBoxZZJGDM.ClientID %>").focus();
                         return (false);
                     }
                     if (document.getElementById("<%=TextPW.ClientID %>") != null && document.getElementById("<%=TextPW.ClientID %>").value == "") {
                         layer.alert('请输入密码！', { offset: '100px', icon: 5, time: 0, area: ['500px', 'auto'] });
                         document.getElementById("<%=TextPW.ClientID %>").focus();
                         return (false);
                     }
                     if (document.getElementById("<%=txtValidator.ClientID %>") != null && document.getElementById("<%=txtValidator.ClientID %>").value == "") {
                         layer.alert('请输入验证码！', { offset: '100px', icon: 5, time: 0, area: ['500px', 'auto'] });
                         document.getElementById("<%=txtValidator.ClientID %>").focus();
                         return (false);
                     }

                     //
                     var encodedString = Base64.encode(document.getElementById("<%=RadTextBoxZZJGDM.ClientID %>").value + "\\" + document.getElementById("<%=TextPW.ClientID %>").value);
                     document.getElementById("<%=HiddenFieldLogin.ClientID %>").value = encodedString;
                     document.getElementById("<%=RadTextBoxZZJGDM.ClientID %>").value = "";
                     document.getElementById("<%=TextPW.ClientID %>").value = "";
                     return (true);
                 }



        </script>
        <%--</telerik:RadCodeBlock>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Windows7">
        </telerik:RadWindowManager>--%>
        <div style="font: bold 30px 宋体; text-align: center; line-height: 300%;">
            北京市住房和城乡建设领域人员资格管理信息系统 - 测试专用模拟登陆
        </div>
        <div class="mainqy">
            <p>
                请选择登陆方式：<asp:RadioButtonList ID="RadioButtonListuserType" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="RadioButtonListuserType_SelectedIndexChanged">
                    <asp:ListItem Text="个人登录" Value="个人登录" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="企业登录" Value="企业登录"></asp:ListItem>
                    <asp:ListItem Text="管理登录" Value="管理登录"></asp:ListItem>
                </asp:RadioButtonList>
            </p>
            <p>
                <asp:Label ID="Labelzzjgdm" runat="server" Text="用户名：" Visible="false"></asp:Label><asp:TextBox ID="RadTextBoxZZJGDM" runat="server" Text="" Width="160px" Height="28px" Visible="false"></asp:TextBox>
                <asp:Label ID="Labelsfzhm" runat="server" Text="用户名："></asp:Label><asp:TextBox ID="RadTextBoxCID" runat="server" Text="" Width="160px" Height="28px"></asp:TextBox>
                <asp:Label ID="LabelUserName" runat="server" Text="用户名：" Visible="false"></asp:Label><asp:TextBox ID="TextBoxUserName" runat="server" Text="" Width="160px" Height="28px" Visible="false"></asp:TextBox>
            </p>
            <p>
                密　码：<input name="TextPW" id="TextPW" runat="server" class="login_textbox" type="password" autocomplete="off"
                    style="width: 160px; height: 28px; line-height: 28px; vertical-align: middle;" />
            </p>
            <p>
                验证码&nbsp;&nbsp;：<input name="txtValidator" id="txtValidator" runat="server" class="login_textbox"
                    style="width: 60px; height: 28px; line-height: 28px; vertical-align: middle;" maxlength="4" placeholder="验证码" />
                <img id="ImgCode" src="./ValidateCode.aspx" alt="验证码" style="vertical-align: middle;" />
                <span style="padding-left: 8px; line-height: 30px; cursor: pointer; color: blue; font-size: 14px; font-weight: bold; text-decoration: none;" onclick="document.getElementById('ImgCode').src='ValidateCode.aspx?o=' + new Date().getTime();">换一换
                </span>
            </p>
            <p>
                <asp:HiddenField ID="HiddenFieldLogin" runat="server" />
                <asp:Button ID="ButtonQY" runat="server" Text="企业登陆" OnClick="ButtonQY_Click" class="button" Visible="false" onfocus="this.blur()"
                    OnClientClick="if(cmdEncrypt_u()==false) return false;" />
                <asp:Button ID="ButtonWorker" runat="server" Text="个人登陆" OnClick="ButtonWorker_Click" Style="cursor: pointer;" class="button" onfocus="this.blur()"
                    OnClientClick="if(cmdEncrypt_w()==false) return false;" />

                &nbsp;&nbsp
                <asp:Button ID="ButtonaAdmin" runat="server" Text="管理端登录" OnClick="ButtonaAdmin_Click" class="button" Visible="false" onfocus="this.blur()"
                    OnClientClick="if(cmdEncrypt()==false) return false;" />
            </p>
        </div>
        <div class="mainqy2">
             <p>
                <a target="_blank" href="https://bjt.beijing.gov.cn/renzheng/open/login/goUserLogin?client_id=100100000502&redirect_uri=http://120.52.185.14/loginYZT.aspx&response_type=code&scope=user_info&state=">首都之窗集成认证登录 - 个人</a>
            </p>
             <p>
                <a target="_blank" href="http://110.43.71.80/qykj">企业空间集成登录 - 企业</a>
            </p>
            </div>
       <%-- <div class="mainqy2">
           <a class="gr gr_dl" id="qydl_qr" linktype="own" href="https://yzt.beijing.gov.cn/am/oauth2/authorize?module=BjzwLDAP&amp;service=bjzwService&amp;response_type=code&amp;client_id=400711506_01&amp;scope=cn+uid+idCardNumber+reserve3+extProperties&amp;redirect_uri=http%3A%2F%2Fbjjs.zjw.beijing.gov.cn%2FEGovHall%2Fbjca%2FYZTAuth.aspx" target="_blank" title="确认" style="
    width: 120px;
">确认</a>
            <p>
                <a target="_blank" href="https://bjt.beijing.gov.cn/renzheng/open/login/goUserLogin?client_id=100100000502&redirect_uri=http://120.52.185.14/loginYZT.aspx&response_type=code&scope=user_info&state=">首都之窗集成认证登录</a>

            </p>
            <p>
                <asp:TextBox ID="TextBoxUrl" runat="server"></asp:TextBox>
            </p>
            <asp:Button ID="ButtonTest" runat="server" Text="好差评推送" OnClick="ButtonTest_Click" class="button" />
            &nbsp;<asp:Button ID="ButtonInputHaoCha" runat="server" Text="好差评填报" OnClick="ButtonInputHaoCha_Click" class="button" />
            <p runat="server" id="p_haoCaPing">
            </p>
            <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" NavigateUrl="#">评价</asp:HyperLink>
        </div>
        <div id="winpop">
        </div>
        <div class="mainqy3" style="text-align:center;display:none">
            
            输入照片相对地址：<asp:TextBox ID="TextBoxCheckImg" runat="server" Width="400px"></asp:TextBox>             <asp:Button ID="ButtonCheckImg" runat="server" Text="一寸照识别" CssClass="button" OnClick="ButtonCheckImg_Click" />
           <br /><br /> <img id="Img1" runat="server" height="140" width="110" alt="一寸照片" src="" />
        </div>--%>
    </form>
</body>
</html>
