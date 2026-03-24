<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SelectImg.ascx.cs" Inherits="ZYRYJG.SelectImg" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<telerik:RadCodeBlock ID="cb1" runat="server">

    <script type="text/javascript">
        //<![CDATA[
        function openWin() {
            var oWnd = radopen("<%=RootUrl %>/jxjy/SelectSourceImg.aspx", "RadWindow1");
            oWnd.maximize();
        }

        function OnClientClose(oWnd, args) {
            var arg = args.get_argument();
            if (arg) {
                var SourceImg = arg.SourceImg;
                document.getElementById("<%=HiddenFieldSourceImg.ClientID %>").value = SourceImg;
                var divSourceImg = document.getElementById("<%=divSourceImg.ClientID %>");
                if (SourceImg == "") {
                    divSourceImg.style.background = "none";
                }
                else {
                    divSourceImg.style.background = "url(../Images/jz/" + SourceImg + ")";
                    divSourceImg.style.backgroundSize = "cover";
                }
            }
        }   
        //]]>
    </script>

</telerik:RadCodeBlock>
 <style type="text/css">
          .simg {
              width: 400px;
              height: 150px;
              background-size: cover;
              background-position: left top;
              background-repeat: no-repeat;
              margin: 20px 20px;
              float: left;
              cursor: pointer;
          }
      </style>
<div onclick="openWin();return false;" style="cursor: pointer;">默认随机显示图片（点击选择特定图片）</div>
<div id="divSourceImg" runat="server" class="simg"  onclick="openWin();return false;" title="点击选择图片">
    
</div>

<asp:HiddenField ID="HiddenFieldSourceImg" runat="server" />
