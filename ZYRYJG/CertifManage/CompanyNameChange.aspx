<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompanyNameChange.aspx.cs" Inherits="ZYRYJG.CertifManage.CompanyNameChange" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc1" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" />
    <link href="../Skins/Blue/Grid.Blue.css?v=1.008" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
    <script src="../Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
    <link href="../layer/skin/layer.css" rel="stylesheet" />
    <script src="../layer/layer.js" type="text/javascript"></script>
    <style type="text/css">
        .barTitle {
            color: #434343;
            background-color: #E4E4E4;
            font-weight: bold;
            border-left: 5px solid #ff6a00;
            text-align: left;
        }

        .img {
            border: none;
            width: 0px;
        }

        .img200 {
            border: none;
            width: 200px;
        }

        td {
            line-height: 200%;
        }

        .subtable td {
            border: 1px solid #cccccc;
            border-collapse: collapse;
        }

        .addrow {
            float: right;
            background: url(../images/jiah.gif) no-repeat center center;
            width: 15px;
            height: 15px;
            padding-right: 20px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server">
        </telerik:RadStyleSheetManager>
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Windows7">
        </telerik:RadWindowManager>
        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
            <script type="text/javascript">
                $(function () {
                    var imgWid = 0;
                    var imgHei = 0; //变量初始化
                    var big = 2.5;//放大倍数
                    $(".img200").hover(function () {

                        $(this).stop(true, true);
                        var imgWid2 = 0; var imgHei2 = 0;//局部变量
                        imgWid = $(this).width();
                        imgHei = $(this).height();
                        imgWid2 = imgWid * big;
                        imgHei2 = imgHei * big;

                        $("#divImg").css({ "float": "right", "overflow": "visible" });
                        $(this).animate({ "width": imgWid2, "height": imgHei2, "margin-left": -imgWid * (big - 1), "position": "absolute", "z-index": 999 });
                    }, function () {
                        $("#divImg").css({ "float": "right", "overflow": "auto" });
                        $(this).stop().animate({ "width": imgWid, "height": imgHei, "margin-left": 0, "position": "relative", "float": "none" });
                    });

                    $(".img200").click(function () {
                        var nw = window.open($(this)[0].src, "_blank", 'resizable=yes');
                    });

                    //变换审核结果
                    $("#<%= RadioButtonListApplyStatus.ClientID%> input").each(function () {
                $(this).click(function () {
                    var TextBoxCheckResult = $("#<%= TextBoxCheckResult.ClientID%>");

                    if ($(this).val() == "通过") {

                        TextBoxCheckResult.val("通过");
                    }
                    else {

                        TextBoxCheckResult.val("退回个人");

                    }
                });

            });

             })


        //审核确认
        function JWSubmitTip() {
            var CheckSelect = $("input[name='RadioButtonListApplyStatus']:checked").val();
            var CheckResult = $("#<%= TextBoxCheckResult.ClientID%>").val();
            if (CheckSelect == "通过" && CheckResult != "通过") {
                return confirm('您选择了审核通过，却修改了审核意见，您确定要提交审核意见么?');
            }
            if (CheckSelect == "不通过" && CheckResult == "退回个人") {
                return confirm('您选择了审核不通过，却没有修改详细的审核意见，您确定要提交审核意见么?');
            }
            return true；
        }
            </script>
        </telerik:RadCodeBlock>
        <div class="div_out">
            <div style="width: 99%; margin: 5px auto; padding-bottom: 30px;">
                <div id="div_top" class="dqts">
                    <div style="float: left;">
                        当前位置&gt;&gt;从业人员证书管理&gt;&gt;<strong>企业信息变更（从业人员）</strong>
                    </div>
                </div>
                <div class="content">
                    <div class="step">
                        <div class="stepLabel">办理进度：</div>
                        <div id="step_未申报" runat="server" class="stepItem lgray step_w80">单位填写</div>
                        <div id="step_已申报" runat="server" class="stepItem lgray step_w80">单位申报</div>
                        <div id="step_已告知" runat="server" class="stepItem lgray">建委审核（办结）</div>
                        <div class="stepArrow">▶</div>
                    </div>
                    <div class="DivTitleOn" onclick="DivOnOff(this,'Td3',event);" title="折叠" id="Td2" runat="server" visible="false">
                        填报说明
                    </div>
                    <div class="DivContent" id="Td3" runat="server" visible="false">
                        1、保存后打印申请表，盖章签字并扫描上传才能申报。
                         <br />
                        2、变更后的企业信息来至企业资质信息，请确保首先变更资质后再发起变更。没有资质的企业请在建委官网更新企业信息后再发起变更。
                         <br />
                        <span style="color:red">
                        3、企业名称变更，请分别在“二建建造师注册”和“从业人员证书管理”菜单中分别发起企业信息变更（审批机构不同、审批流程不同），并尽可能同日申报，以免因为一种证书企业名称变更后另一种证书无法申请企业名称变更 。
                        </span>
                    </div>
                    <div style="width: 100%; margin: 10px auto; text-align: center; overflow: hidden;">
                        <div style="width: 66%; float: left; clear: left">
                            <table id="TableEdit" class="detailTable" cellpadding="5" runat="server" style="width: 100%!important">
                                <tr class="GridLightBK">
                                    <td class="barTitle" colspan="3">变更信息</td>
                                </tr>
                                <tr>
                                    <td class="infoHead" style="width: 20%"></td>
                                    <td class="formItem_1 lightgray" style="text-align: center; width: 40%"><strong>变更前</strong></td>
                                    <td class="formItem_2" style="text-align: center; width: 40%"><strong>变更后</strong></td>
                                </tr>
                                <tr>
                                    <td class="infoHead">企业名称：</td>
                                    <td class="formItem_1 lightgray">
                                        <asp:Label ID="LabelENT_NameFrom" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td class="formItem_2">
                                        <telerik:RadTextBox ID="RadTextBoxENT_NameTo" runat="server" Width="98%"></telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="ValidatorENT_NameTo" runat="Server" ControlToValidate="RadTextBoxENT_NameTo"
                                            ErrorMessage="请填写企业名称" Display="Dynamic">*请填写企业名称</asp:RequiredFieldValidator>
                                    </td>
                                </tr>

                                <tr class="GridLightBK">
                                    <td class="barTitle" colspan="3">可变更人员列表<asp:Label ID="LabelPersonCount" runat="server" Text=""></asp:Label></td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <div style="width: 100%; max-height: 450px; overflow-y: auto;">
                                            <telerik:RadGrid ID="RadGridPerson" runat="server"
                                                GridLines="None" AllowPaging="false" AllowSorting="false" AutoGenerateColumns="False"
                                                Width="97%" Skin="Blue" EnableAjaxSkinRendering="False" EnableEmbeddedSkins="False" PagerStyle-AlwaysVisible="true">
                                                <ClientSettings EnableRowHoverStyle="false">
                                                </ClientSettings>
                                                <HeaderContextMenu EnableEmbeddedSkins="False">
                                                </HeaderContextMenu>
                                                <MasterTableView NoMasterRecordsText=" 没有可显示的记录" CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Left">
                                                    <Columns>

                                                        <telerik:GridBoundColumn UniqueName="PostName" DataField="PostName" HeaderText="岗位工种">
                                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="CertificateCode" DataField="certificateCode"
                                                            HeaderText="证书编号">
                                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridTemplateColumn UniqueName="ValidEndDate" HeaderText="有效期至">
                                                            <ItemTemplate>
                                                                <%# Convert.ToDateTime(Eval("ValidEndDate")).ToString("yyyy.MM.dd")=="2050.01.01"?"当前有效证书":Convert.ToDateTime(Eval("ValidEndDate")).ToString("yyyy.MM.dd")%>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn UniqueName="WorkerName" DataField="WorkerName" HeaderText="姓名">
                                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn UniqueName="WorkerCertificateCode" DataField="WorkerCertificateCode"
                                                            HeaderText="证件号码">
                                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </telerik:GridBoundColumn>
                                                    </Columns>
                                                    <HeaderStyle Font-Bold="True" />
                                                </MasterTableView>
                                            </telerik:RadGrid>
                                        </div>
                                    </td>
                                </tr>

                                <tr class="GridLightBK" id="trFuJanTitel" runat="server" visible="false">
                                    <td colspan="4" class="barTitle">附件上传                                   
                                    </td>
                                </tr>
                                <tr class="GridLightBK" id="trFuJan" runat="server" visible="false">
                                    <td colspan="4">
                                        <div class="fujian">
                                            企业信息变更证明：<span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.企业信息变更证明%>','企业信息变更证明','','<%=ApplyCode%>')">选择文件</span><span class="tishi">（要求：上传企业信息变更证明扫描件原件、jpg格式图片,最大500K）</span>
                                        </div>
                                        <div class="fujian">
                                            申请表扫描件：<span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.申请表扫描件%>','申请表扫描件','','<%=ApplyCode%>')">选择文件</span><span class="tishi">（要求：请在本页面导出打印申请表，单位盖章签字后扫描上传）</span>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                            <div id="divQY" visible="false" runat="server" style="width: 100%; padding-top: 20px; text-align: center; clear: both;">
                                <asp:Button ID="ButtonSave" runat="server" Text="保 存" CssClass="bt_large" UseSubmitBehavior="false" OnClientClick="this.value='正在提交';this.disabled=true;" OnClick="ButtonSave_Click" Enabled="false" />
                                <%--<asp:Button ID="ButtonSave" runat="server" Text="保 存" CssClass="button" OnClick="ButtonSave_Click" Enabled="false" />--%>
                                &nbsp;&nbsp;<asp:Button ID="ButtonOutput" runat="server" Text="导出打印" CssClass="bt_large" OnClick="ButtonOutput_Click" Enabled="false" />
                                &nbsp;&nbsp;<asp:Button ID="ButtonApply" runat="server" Text="申 报" CssClass="bt_large" OnClick="ButtonApply_Click" Enabled="false" />
                                &nbsp;&nbsp;<asp:Button ID="ButtonDelete" runat="server" Text="删 除" CssClass="bt_large" OnClick="ButtonDelete_Click" Enabled="false" OnClientClick="javascript:if(!confirm('您确定要删除么?'))return false;" />
                            </div>
                            <div id="divCheckHistory" visible="true" runat="server" style="width: 100%; padding-top: 20px; text-align: center; clear: both;">
                                <table id="Table2" runat="server" width="100%" border="0" cellpadding="5" cellspacing="1" class="table" style="text-align: center; margin: 10px auto">
                                    <tr class="GridLightBK">
                                        <td class="barTitle">审办记录</td>
                                    </tr>
                                    <tr class="GridLightBK">
                                        <td align="left" style="border-collapse: collapse;">
                                            <telerik:RadGrid ID="RadGridCheckHistory" runat="server" ShowHeader="true" CellPadding="0" CellSpacing="0"
                                                GridLines="None" AllowPaging="False" AllowSorting="False" AutoGenerateColumns="False"
                                                Width="100%" EnableEmbeddedSkins="false" PagerStyle-AlwaysVisible="False">
                                                <ClientSettings EnableRowHoverStyle="False">
                                                </ClientSettings>
                                                <MasterTableView NoMasterRecordsText="" CommandItemDisplay="None">
                                                    <Columns>
                                                        <telerik:GridBoundColumn HeaderText="序号" UniqueName="RowNo" DataField="RowNo">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn HeaderText="流程" UniqueName="Action" DataField="Action">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn HeaderText="办理人" UniqueName="ActionMan" DataField="ActionMan" Display="false">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn HeaderText="办理时间" UniqueName="ActionData" DataField="ActionData">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn HeaderText="办理结果" UniqueName="ActionResult" DataField="ActionResult">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn HeaderText="办理意见" UniqueName="ActionRemark" DataField="ActionRemark">
                                                        </telerik:GridBoundColumn>
                                                    </Columns>
                                                    <HeaderStyle Font-Bold="True" BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1" />
                                                    <ItemStyle CssClass="subtable" />
                                                    <AlternatingItemStyle CssClass="subtable" />
                                                </MasterTableView>
                                            </telerik:RadGrid>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <table id="divQX" visible="false" runat="server" width="100%" border="0" cellpadding="5" cellspacing="1" class="table" style="text-align: center; margin: 10px auto">
                                <tr class="GridLightBK">
                                    <td colspan="2" class="barTitle">建委审核</td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td width="20%" align="right">审核结果：</td>
                                    <td width="80%" align="left">
                                        <asp:RadioButtonList ID="RadioButtonListApplyStatus" runat="server" RepeatDirection="Horizontal" TextAlign="right">
                                            <asp:ListItem Text="通过" Value="通过" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="不通过" Value="不通过"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td width="20%" align="right">审核意见：</td>
                                    <td width="80%" align="left">

                                        <asp:TextBox ID="TextBoxCheckResult" runat="server" TextMode="MultiLine" Rows="3" Width="99%" Text="通过" MaxLength="200"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td colspan="2" align="center">
                                        <asp:Button ID="BtnCheck"  runat="server" CssClass="bt_large" Text="确 定" OnClick="BtnCheck_Click" OnClientClick="if(JWSubmitTip()==false) return false;" />
                                    </td>
                                </tr>
                            </table>
                            
                        </div>
                        <div id="divImg" style="width: 32%; float: left; clear: right; margin-left: 1%; overflow: auto; border: 1px solid #cccccc; margin-bottom: 200px">
                            <telerik:RadGrid ID="RadGridFile" runat="server"
                                GridLines="None" AllowPaging="false" AllowSorting="false" AutoGenerateColumns="False"
                                Width="100%" Skin="Default" EnableAjaxSkinRendering="false"
                                EnableEmbeddedSkins="false" OnItemDataBound="RadGridFile_ItemDataBound">
                                <ClientSettings EnableRowHoverStyle="false">
                                </ClientSettings>
                                <MasterTableView NoMasterRecordsText=" 没有相关附件" GridLines="None" CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Left"
                                    DataKeyNames="ApplyID,FileName,FileUrl">
                                    <Columns>
                                        <telerik:GridTemplateColumn UniqueName="ApplyFile" HeaderText="附件">
                                            <ItemTemplate>
                                                <div class="DivTitleOn" onclick="DivOnOff(this,'Div<%# Eval("DataType") %>',event);" title="折叠">
                                                    <%# Eval("DataType") %>
                                                </div>
                                                <div class="DivContent" id="Div<%# Eval("DataType") %>" style="position: relative;">
                                                    <telerik:RadGrid ID="RadGrid1" runat="server" ShowHeader="false"
                                                        GridLines="None" AllowPaging="false" AllowSorting="false" AutoGenerateColumns="False"
                                                        Width="100%" Skin="Default" EnableAjaxSkinRendering="false" OnDeleteCommand="RadGridFile_DeleteCommand"
                                                        EnableEmbeddedSkins="false">
                                                        <ClientSettings EnableRowHoverStyle="false">
                                                        </ClientSettings>
                                                        <MasterTableView NoMasterRecordsText="" GridLines="None" CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Left"
                                                            DataKeyNames="ApplyID,FileID">
                                                            <Columns>
                                                                <telerik:GridTemplateColumn UniqueName="ApplyFile" HeaderText="附件">
                                                                    <ItemTemplate>
                                                                        <img class="img200" alt="图片" src='<%# ZYRYJG.UIHelp.ShowFile(Eval("FileUrl").ToString())%>' />
                                                                        <asp:ImageButton runat="server" ID="ImageButton1" ImageUrl="../images/Cancel.gif" CommandName="Delete" Visible='<%#(IfExistRoleID("2") == true && ((ViewState["CertificateChangeOB"] as Model.CertificateChangeOB).Status == Model.EnumManager.CertificateChangeStatus.NewSave || (ViewState["CertificateChangeOB"] as Model.CertificateChangeOB).Status == Model.EnumManager.CertificateChangeStatus.SendBack))?true:false%>' OnClientClick="javascript:if(confirm('您确定要删除么?')==false) return false" />

                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" Height="30px" />
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </telerik:GridTemplateColumn>

                                                            </Columns>
                                                            <HeaderStyle BackColor="#E4E4E4" Height="22px" Font-Bold="true" />
                                                        </MasterTableView>
                                                    </telerik:RadGrid>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" Height="30px" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                    <HeaderStyle BackColor="#E4E4E4" Height="22px" Font-Bold="true" />
                                </MasterTableView>
                            </telerik:RadGrid>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="winpop">
        </div>
    </form>
    <script type="text/javascript">

        //
        function tips_pop(code, ftype, fsname, pid) {

            layer.open({
                type: 2,
                title: ['资料上传 - ' + ftype, 'font-weight:bold;background: #5DA2EF;'],//标题
                maxmin: true, //开启最大化最小化按钮,
                offset: $(parent.document).scrollTop() + 20 + 'px',
                area: ['800px', '500px'],
                shadeClose: false, //点击遮罩关闭
                content: '../uploader/Upload.aspx?o=' + code + '&t=' + ftype + '&s=' + fsname + '&a=' + pid,
                cancel: function (index, layero) {
                    __doPostBack('refreshFile', '');
                    layer.close(index);
                    return false;
                }
            });
            var MsgPop = document.getElementById("winpop");//获取窗口这个对象,即ID为winpop的对象
            MsgPop.style.display = "block";
            MsgPop.style.height = "400px";//高度增加4个象素
        }
    </script>

</body>
</html>
