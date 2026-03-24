<%@ Page Title="" Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="WorkerImgEdit.aspx.cs" Inherits="ZYRYJG.PersonnelFile.WorkerImgEdit" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript">
            var wideheight = 1;
            function up() {
                var ctrl = $("#<%=ImgCode.Visible==true?ImgCode.ClientID:ImgSign.Visible==true?ImgSign.ClientID:ImgIDCard.Visible==true?ImgIDCard.ClientID:""%>");
                var top = parseInt(ctrl.css("top").replace("px", ""));
                ctrl.css("top", (top - 3) + 'px');
            }
            function down() {
                var ctrl = $("#<%=ImgCode.Visible==true?ImgCode.ClientID:ImgSign.Visible==true?ImgSign.ClientID:ImgIDCard.Visible==true?ImgIDCard.ClientID:""%>");
                var top = parseInt(ctrl.css("top").replace("px", ""));
                ctrl.css("top", (top + 3) + 'px');
            }
            function left() {
                var ctrl = $("#<%=ImgCode.Visible==true?ImgCode.ClientID:ImgSign.Visible==true?ImgSign.ClientID:ImgIDCard.Visible==true?ImgIDCard.ClientID:""%>");
                var left = parseInt(ctrl.css("left").replace("px", ""));
                ctrl.css("left", (left - 3) + 'px');
            }
            function right() {
                var ctrl = $("#<%=ImgCode.Visible==true?ImgCode.ClientID:ImgSign.Visible==true?ImgSign.ClientID:ImgIDCard.Visible==true?ImgIDCard.ClientID:""%>");
                var left = parseInt(ctrl.css("left").replace("px", ""));
                ctrl.css("left", (left + 3) + 'px');
            }
            function big() {
                var ctrl = $("#<%=ImgCode.Visible==true?ImgCode.ClientID:ImgSign.Visible==true?ImgSign.ClientID:ImgIDCard.Visible==true?ImgIDCard.ClientID:""%>");
                var width = parseInt(ctrl.css("width").replace("px", ""));
                var height = parseInt(ctrl.css("height").replace("px", ""));
                var widthheight = $("#<%=Hiddenwidthheight.ClientID%>").val().split(';');
                //ctrl.css("width", (width + Number(widthheight[0])) + 'px');
                //ctrl.css("height", (height + Number(widthheight[1])) + 'px');
                ctrl.css("width", (width + 3) + 'px');
                ctrl.css("height", (height + (Number(widthheight[1]) * 5 / Number(widthheight[0]))) + 'px');
            }
            function small() {
                var ctrl = $("#<%=ImgCode.Visible==true?ImgCode.ClientID:ImgSign.Visible==true?ImgSign.ClientID:ImgIDCard.Visible==true?ImgIDCard.ClientID:""%>");
                var width = parseInt(ctrl.css("width").replace("px", ""));
                var height = parseInt(ctrl.css("height").replace("px", ""));
                var widthheight = $("#<%=Hiddenwidthheight.ClientID%>").val().split(';');
                //ctrl.css("width", (width - Number(widthheight[0])) + 'px');
                //ctrl.css("height", (height - Number(widthheight[1])) + 'px');
                ctrl.css("width", (width - 3) + 'px');
                ctrl.css("height", (height - (Number(widthheight[1]) * 5 / Number(widthheight[0]))) + 'px');
            }
            function save() {
                var ctrl = $("#<%=ImgCode.Visible==true?ImgCode.ClientID:ImgSign.Visible==true?ImgSign.ClientID:ImgIDCard.Visible==true?ImgIDCard.ClientID:""%>");
                var width = parseInt(ctrl.css("width").replace("px", ""));
                var height = parseInt(ctrl.css("height").replace("px", ""));
                var top = parseInt(ctrl.css("top").replace("px", ""));
                var left = parseInt(ctrl.css("left").replace("px", ""));
                var ctrl = $("#<%=HiddenField1.ClientID%>");
                ctrl.val("width:" + width + ";height:" + height + ";top:" + top + ";left:" + left);
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindowManager ID="RadWindowManager1" ShowContentDuringLoad="false" VisibleStatusbar="false"
        ReloadOnShow="true" runat="server" Skin="Windows7" EnableShadow="true">
    </telerik:RadWindowManager>
    <div class="div_out">
        <div class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt; 我的信息 
                &gt;&gt; 个人信息维护 &gt;&gt; <strong>图片微调</strong>
            </div>
        </div>
        <div class="content">
            <div style=" padding-right: 100px; margin: 20px 20px; text-align: right;">
                <asp:Button ID="btnSave" runat="server" Text="保 存" CssClass="button" OnClick="btnSave_Click" OnClientClick="save()" />
                 <asp:Button ID="ButtonCancel" runat="server" Text="取 消" CssClass="button" OnClick="ButtonCancel_Click"  />
                <asp:HiddenField ID="HiddenField1" runat="server" />
                <asp:HiddenField ID="Hiddenwidthheight" runat="server" Value="1" />
            </div>
            <div style="width: 800px; height: 600px; margin: 20px auto;">
                <div style="line-height:300%;">通过调整按钮，使得图片在红框内显示比例最佳，点击保存只保存红框内图片内容。</div>
                <div id="myImg" runat="server" style="height: 140px; width: 110px; border: 1px solid red; position: relative; overflow: hidden">
                    <img id="ImgCode" runat="server" height="140" width="110" alt="图片" style="top: 0; left: 0; position: absolute" />
                </div>
                <div id="DivImgSign" runat="server" style="height: 42px; width: 99px; border: 1px solid red; position: relative; overflow: hidden">
                    <img id="ImgSign" runat="server" height="42" width="99" alt="图片" style="top: 0; left: 0; position: absolute" />
                </div>
                <div id="DivImgIDCard" runat="server" style="height: 300px; width: 400px; border: 1px solid red; position: relative; overflow: hidden">
                    <img id="ImgIDCard" runat="server" height="300" width="400" alt="图片" style="top: 0; left: 0; position: absolute" />
                </div>
                <div style="margin: 20px 20px; ">
                    <div>
                        <input id="ButtonUp" type="button" value="↑" onclick="up()" style="margin-bottom: 10px;font-size:24px;font-weight:bold" title="上移" />
                    </div>
                    <div>
                        <div style="width: 45%; float: left; margin-right: 20px; text-align: right;">
                            <input id="ButtonLeft" type="button" value="←" onclick="left()" title="左移"  style="font-size:24px;font-weight:bold" />
                        </div>

                        <div style="width: 45%; float: right; margin-left: 20px;text-align: left;">
                            <input id="ButtonRight" type="button" value="→" onclick="right()" title="右移" style="font-size:24px;font-weight:bold"  />
                        </div>
                        <div style="clear: both;"></div>
                    </div>
                    <div>
                        <input id="ButtonDown" type="button" value="↓" onclick="down()" style="margin-top: 10px;font-size:24px;font-weight:bold" title="下移"  />
                    </div>
                    <div style="margin-top:20px">
                        <div style="width: 45%; float: left; margin-right: 20px; text-align: right;">
                            <input id="ButtonBig" type="button" value="＋" onclick="big()" title="放大" style="font-size:24px;font-weight:bold" />
                        </div>

                        <div style="width: 45%; float: right; margin-left: 20px; text-align: left">
                            <input id="ButtonSmall" type="button" value="－" onclick="small()" title="缩小" style="font-size:24px;font-weight:bold"  />
                        </div>
                        <div style="clear: both;"></div>
                    </div>
                </div>
            </div>
        </div>

    </div>
</asp:Content>
