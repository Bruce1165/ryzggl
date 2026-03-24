<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IframeView.ascx.cs" Inherits="ZYRYJG.IframeView" %>

<style>
    body{background-color:#ffffff;
    }
    .if_m {
       position: absolute;
       left:19px;
       right:19px;
        height: 30px;
        background: url(../images/red/if_m.png) repeat-x center center transparent;
       
        z-index: 0;
    }

    .if_l {
        width: 19px;
        height: 30px;
        left:0px;
        background: url(../images/red/if_l.png) no-repeat left center transparent;
        position: absolute;
        z-index: 99999999;
    }

    .if_r {
        width: 19px;
        height: 30px;
        right:0px;
        background: url(../images/red/if_r.png) no-repeat right center transparent;
        position: absolute;
        z-index: 99999999;
    }
    .ifbord{
          border:4px solid #ccc;
          border-top:16px solid #ccc;
    border-radius:2px 2px;
    }
</style>
<div id="divFram" style="position: absolute; top: -99999px; left: 8px; right:5px; margin: 1px 0px;  text-align: left; padding:0 ; background-color:#fff;" >
    
   <%-- <div class="if_m" >
       
    </div>
     <div class="if_l"></div>
        <div class="if_r"></div>--%>
     <div style="position: absolute;left:0px;right:0px;margin:0 0;background-color:#fff;" class="ifbord">
         <iframe id="IfrmView"
        onload="javascript:SetCwinHeight();" height="1" frameborder="0" src="about:blank" scrolling="no"
        style="overflow: visible; text-align: center; width:100%;  background-color:transparent;"></iframe>

   </div>
</div>
  


<iframe id="iframeGray" style="display: none; width: 1px; height: 1px; position: absolute; background-color: #fff; filter: alpha(opacity=80); opacity: 0.8;"
    scrolling="no"
    frameborder="0" src="about:blank"></iframe>
<input id="HiddenOpenMode" type="hidden" value="true" />


<%--<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IframeView.ascx.cs" Inherits="ZYRYJG.IframeView" %>

<div id="divFram" style="position: absolute; width: 98%; top: -99999px; left: 0px; margin: 5px 0px; overflow: visible; text-align: left; padding-right: 1%">
    <iframe width="100%" id="IfrmView" 
        onload="javascript:SetCwinHeight();" height="1" frameborder="0" src="about:blank" scrolling="auto" 
        style="background-color: White; overflow: auto; text-align: center;"></iframe>
</div>
<iframe id="iframeGray" style="display: none; width: 1px; height: 1px; position: absolute; background-color: #FFFFFF; filter: alpha(opacity=80); opacity: 0.8;"
    scrolling="no"
    frameborder="0" src="about:blank"></iframe>
<input id="HiddenOpenMode" type="hidden" value="true" />--%>