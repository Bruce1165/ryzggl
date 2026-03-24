<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/RadControls.Master"
    CodeBehind="CertificatePdf.aspx.cs" Inherits="ZYRYJG.PersonnelFile.CertificatePdf" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
        .pdfbtn {
            background: url(../images/red/pdf.png) no-repeat left center;
            background-size:60px 72px;
            padding-left: 64px;
            border: none;
            color: blue;
            cursor: pointer;
            height:72px;
            font-size:16px!important;
        }
        .ofdbtn {
            background: url(../images/red/ofd.png) no-repeat left center;
            background-size:60px 72px;
            padding-left: 64px;
            border: none;
            color: blue;
            cursor: pointer;
            height:72px;
            font-size:16px!important;
        }

        .div_jdk {
            position: static;
            right: 10px;
            top: 200px;
            width: 600px;
            text-align: left;
            background-color: #EDF6FB;
            border: 2px solid #5A97DD;
          
            margin: 20px 20px;
            line-height: 160%;
            text-indent:32px;
        }

        .jdk_head {
            background-color: #5A97DD;
            color: white;
            text-align: center;
             line-height: 300%;
             font-weight:bold;
        }
        .div_body{
              padding: 20px 40px;
        }
    </style>
    <div class="div_out">

        <div id="div_top" class="dqts">
            <div id="divRoad" runat="server" style="float: left;">
                当前位置 &gt;&gt; 电子证书下载
            </div>
        </div>
        <div class="content" style="min-height: 400px">
            <div id="DivContent">
                <p class="jbxxbt" style="text-align: center">电子证书下载</p>

                <div class="DivContent">
                    <div id="DivDetail" runat="server" style="padding: 12px; line-height: 200%; font-size: 16px;">
                    </div>
                    <div style="text-align: left; padding: 0 20px; margin: 12px;">
                        <asp:Button ID="ButtonDownload" runat="server" Text="下 载" CssClass="pdfbtn" OnClick="ButtonDownload_Click" Visible="false" />
                    
                        <asp:Button ID="ButtonDownload_OFD" runat="server" Text="下 载" CssClass="ofdbtn" OnClick="ButtonDownload_OFD_Click" Visible="false" style="margin-left:200px" />
                    </div>
                </div>
                <div id="div_downlog" runat="server" style="text-align: left; padding: 20px; margin: 12px; line-height: 150%; max-height: 100px; overflow-y: auto;"></div>
            </div>
            <div style="width: 95%; margin: 10px auto; text-align: center;">
                 <asp:Button ID="ButtonReCheck" runat="server" Text="申请重新校验" Visible="false"
                         CssClass="bt_large" OnClick="ButtonReCheck_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
              <asp:Button ID="ButtonPause" runat="server" Text="申请暂扣" Visible="false"
                        OnClientClick="if(this.value=='申请暂扣') return confirm('确定要申请暂扣么？'); else  return confirm('确定要申请解除暂扣么？');" CssClass="button"
                        OnClick="ButtonPause_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                  <input id="ButtonReturn" type="button" value="返 回" class="button" onclick="javascript: hideIfam();" />

            </div>
            <div id="div_jdk" runat="server" class="div_jdk" visible="false">
                <div class="jdk_head">北京市住房和城乡建设委员会监督卡</div>
                <div class="div_body">
                    <p>您好，为了加强党风廉政建设和反腐败斗争，我们制作了监督卡，欢迎您对我们的工作进行监督并提供举报线索。谢谢您的支持！</p>

                    <p style="font-weight:bold">举报受理范围：</p>

                    <p>受理对北京市住房和城乡建设委员会所属党组织、党员违反政治纪律、组织纪律、廉洁纪律、群众纪律、工作纪律、生活纪律等党的纪律行为的检举控告。</p>

                    <p>比如：工作人员在工作中有吃、拿、卡、要行为，利用职权或职务上的影响谋取利益等。</p>

                    <p style="font-weight:bold">举报受理方式：</p>

                    <p>1.举报邮箱：szjwjgjw＠126.com</p>

                    <p>2.邮寄地址：北京市通州区达济街9号院北京市住房和城乡建设委员会机关纪委（邮编：101160）</p>

                    <p>3.举报线索小程序扫描码</p>
                    <p>
                        <img alt="监督卡" src="../Images/jdk.jpg" style="width: 200px; height: 200px; " /></p>
                </div>
            </div>
        </div>
    </div>
    <div id="winpop">
        </div>
</asp:Content>
