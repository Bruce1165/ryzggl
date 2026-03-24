<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ZYRYJG.Login" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>北京市住房和城乡建设领域人员资格管理信息系统</title>
    <style type="text/css">
        body {
            margin: 0 0;
            font: normal Microsoft YaHei,sans-serif;
            text-align: center;
                        background: url(./Images/bg.jpg) no-repeat right top;
        }
        .main {
            text-align: center;
            width: 100%;

        }
        .header {
            width: 100%;
            height: 300px;
            line-height: 30px;
            color: white;
            font: 14px 黑体;
            vertical-align: top;
            background:#b22222;
            filter: alpha(opacity=80);
            background: rgba(178,34,34,0.9);
            box-shadow: 0px 0px 5px rgba(178,34,34,0.1);
        }
        .sysName {
            font: bold 28px Microsoft YaHei,sans-serif;
            color: #db1a2d;
            z-index: 6;            
            padding-top: 10px;
            line-height: 45px;
            text-align: center;

        }
          .unitName {
            font: bold 22px Microsoft YaHei,sans-serif;
            color: #f7f7f7;
            background: url(./Images/logow.png) no-repeat 20px center;
            z-index: 6;
            padding-top:15px;
            line-height: 50px;
            text-align: left;
            padding-left:80px;

            /*filter: alpha(opacity=80);
            background: rgba(61,123,232,0.8);
            box-shadow: 0px 0px 5px rgba(61,123,232,0.3);*/
        }
        .m-loginboxbg {
            position: absolute;
            left: 25%;
            top: 150px;
            width: 560px;
            height: 390px;
            filter: alpha(opacity=90);
            background: #F5F5F5;
            background: rgba(245,245,245,0.9);
            box-shadow: 0px 0px 5px rgba(0,0,0,0.3);
            border: 1px solid rgba(255, 255, 255, 0.3);
            border-radius:12px;
            padding: 20px 20px;
            font-size: 12px;
            text-align:center;        
        }
        .inputdiv {
            /*margin: 20px 0px 20px 120px;*/
            margin:20px auto;
            padding-left:120px;
            line-height:36px;
            position: relative;
            width: 380px;
            text-align:left;
            
        }
        .login_textbox {
            width: 220px;
            height: 18px;
            font-size: 16px;
            color: #bbbbbb;
            border: 1px solid #ccc;
            padding: 8px;
            vertical-align:top;
        }
        .l_button {
            width: 120px;
            height: 36px;
            font-size: 16px;
            background-color: #bd1a2d;
            border: 1px solid #bd1a2d;
            border-radius:5px;
            color: white;
            cursor: pointer;
            margin-right:20px;
        }

    </style>
    <script type="text/javascript" src="Scripts/jquery-3.4.1.min.js"></script>
    <script type="text/javascript" language="javascript" src="Scripts/BigInt.js"></script>
    <script type="text/javascript" language="javascript" src="Scripts/Barrett.js"></script>
    <script type="text/javascript" language="javascript" src="Scripts/RSA.js"></script>
    <script type="text/javascript" language="javascript" src="Scripts/CodeManage.js"></script>
        <link href="layer/skin/layer.css" rel="stylesheet" />
    <script src="layer/layer.js" type="text/javascript"></script>
     
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
             <script type="text/javascript">

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
               if (document.getElementById("<%=TextBoxPassWord.ClientID %>") != null && document.getElementById("<%=TextBoxPassWord.ClientID %>").value == "") {
                     layer.alert('请输入密码！', { offset: '100px', icon: 5, time: 0, area: ['500px', 'auto'] });
                     document.getElementById("<%=TextBoxPassWord.ClientID %>").focus();
                   return (false);
               }
               if (document.getElementById("<%=txtValidator.ClientID %>") != null && document.getElementById("<%=txtValidator.ClientID %>").value == "") {
                     layer.alert('请输入验证码！', { offset: '100px', icon: 5, time: 0, area: ['500px', 'auto'] });
                     document.getElementById("<%=txtValidator.ClientID %>").focus();
                   return (false);
               }

                 //
               var encodedString = Base64.encode(document.getElementById("<%=TextBoxUserName.ClientID %>").value + "\\" + document.getElementById("<%=TextBoxPassWord.ClientID %>").value);
                 document.getElementById("<%=HiddenFieldLogin.ClientID %>").value = encodedString;
                 document.getElementById("<%=TextBoxUserName.ClientID %>").value = "";
                 document.getElementById("<%=TextBoxPassWord.ClientID %>").value = "";
                 return (true);
             }
          </script>
        </telerik:RadCodeBlock>
        <telerik:RadWindowManager ID="RadWindowManager1" ShowContentDuringLoad="false" VisibleStatusbar="false"
            ReloadOnShow="false" runat="server" Skin="Windows7" EnableShadow="true">
        </telerik:RadWindowManager>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" EnableAJAX="true">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="TextBoxUserName">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="TextBoxPassWord" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelRenderMode="Inline" />
                        <telerik:AjaxUpdatedControl ControlID="remUsername" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelRenderMode="Inline" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true" Skin="Default" />
        <div class="header">
            <div class="unitName">北京市住房和城乡建设委员会</div>
        </div>
        <div class="main" style="width: 100%;  margin: 0 auto; padding-top: 8px;">

            <div id="divLogin" runat="server" style="padding-top: 15px;" class="m-loginboxbg">
                 <div class="sysName">北京市住房和城乡建设领域 <br /> 人员资格管理信息系统</div>
                <div class="inputdiv">
                     <div style="background: url(../Images/manw.png) no-repeat center center #db1a2d; width:36px; height:36px; float:left; vertical-align:middle;"></div>
                    <asp:TextBox ID="TextBoxUserName" runat="server" CssClass="login_textbox"
                        MaxLength="50"  placeholder="输入登录名"></asp:TextBox>

                </div>
                 <div class="inputdiv" >
                          <div style="background: url(../Images/lockw.png) no-repeat center center #db1a2d; width:36px; height:36px; float:left;"></div>
                     <asp:TextBox ID="TextBoxPassWord" runat="server" CssClass="login_textbox" TYPE="password" AUTOCOMPLETE="off"
                        TextMode="Password" MaxLength="50" placeholder="输入密码"></asp:TextBox>              
                                 
                </div>
                 <div class="inputdiv">
                    <input name="txtValidator" id="txtValidator" runat="server" class="login_textbox"
                        style="width: 100px; margin-right: 4px; vertical-align: middle;" maxlength="4" placeholder="验证码" />
                    <img id="ImgCode"  src='' alt="验证码" style="vertical-align: middle;" />

                    <span style="padding-left: 8px; line-height: 30px; cursor: pointer; color: #666; font-size: 14px; font-weight: bold; text-decoration: none;" onclick="document.getElementById('ImgCode').src='ValidateCode.aspx?o=' + new Date().getTime();">换一换
                    </span>
                </div>
                <div  class="inputdiv">
                    <asp:HiddenField ID="HiddenFieldLogin" runat="server" />
                    <asp:Button ID="ButtonLogin" runat="server" CssClass="l_button" Text="登 录" OnClick="ButtonLogin_Click" onfocus="this.blur()"
                       OnClientClick="if(cmdEncrypt()==false) return false;" /><asp:Button ID="Button1" runat="server" Text="重 置" CssClass="l_button" OnClick="Button1_Click" />
                </div>

            </div>
        </div>
          <div id="winpop">
        </div>
    </form>
</body>
</html>
