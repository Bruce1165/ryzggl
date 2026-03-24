<%@ Page Title="" Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="CertifMoreApply.aspx.cs" Inherits="ZYRYJG.CertifManage.CertifMoreApply" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadWindowManager ID="RadWindowManager1" ShowContentDuringLoad="false" VisibleStatusbar="false"
        ReloadOnShow="true" runat="server" Skin="Windows7" EnableShadow="true">
    </telerik:RadWindowManager>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

            function onRequestStart(sender, args) {
                if (args.get_eventTarget().indexOf('<%=ButtonSave.UniqueID %>') >= 0
                || args.get_eventTarget().indexOf('<%=ButtonExport.UniqueID %>') >= 0) {
                    args.set_enableAjax(false);
                }
            }

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

                //单位审核结果
                $("#<%= RadioButtonListOldUnitCheckResult.ClientID%> input").each(function () {
                    $(this).click(function () {
                        var TextBoxOldUnitCheckRemark = $("#<%= TextBoxOldUnitCheckRemark.ClientID%>");

                        if ($(this).val() == "同意") {

                            TextBoxOldUnitCheckRemark.val("提交建委审核");
                        }
                        else {

                            TextBoxOldUnitCheckRemark.val("退回个人");

                        }
                    });

                });


                //建委审核结果
                $("#<%= RadioButtonListJWCheck.ClientID%> input").each(function () {
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
            });


            //选择文件
            function tips_pop(code, ftype, fsname, pid) {

                layer.open({
                    type: 2,
                    title: ['资料上传 - ' + ftype, 'font-weight:bold;'],//标题
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

            function CheckClientValidate() {
                Page_ClientValidate();
                if (Page_IsValid) {
                    return true;
                } else {
                    return false;
                }
            }

            //选择企业
            function selectQY() {

                layer.open({
                    type: 2,
                    title: ['选择一个企业 ', 'font-weight:bold;'],//标题
                    maxmin: true, //开启最大化最小化按钮,
                    offset: $(parent.document).scrollTop() + 20 + 'px',
                    area: ['800px', '500px'],
                    shadeClose: false, //点击遮罩关闭
                    content: '../SelectQY.aspx',
                    btn: ['确定', '关闭'],
                    yes: function (index, oArg) {
                        //获取选择的row,并加载到页面
                        var data = window["layui-layer-iframe" + index].callbackdata();
                        if (data) {
                            var RadTextBoxUnitCodeMore = $find("<%= RadTextBoxUnitCodeMore.ClientID%>");
                            var RadTextBoxUnitNameMore = $find("<%= RadTextBoxUnitNameMore.ClientID%>");
                            RadTextBoxUnitCodeMore.set_value(data.ENT_OrganizationsCode);
                            RadTextBoxUnitNameMore.set_value(data.ENT_Name)
                            layer.close(index);
                            return false;
                        } else {
                            layer.msg('请选择一个企业', { icon: 0 });
                        }
                    },
                    cancel: function (index, layero) {
                        layer.close(index);
                        return false;
                    }
                });
                var MsgPop = document.getElementById("winpop");//获取窗口这个对象,即ID为winpop的对象
                MsgPop.style.display = "block";
                MsgPop.style.height = "400px";//高度增加4个象素
            }

        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="TableEdit">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="TableEdit" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>

        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true"
        Skin="Windows7" />
    <div class="div_out">

        <div id="div_top" class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt; 证书管理 &gt;&gt;三类人员 &gt;&gt; <strong>三类人A本增发</strong>
            </div>
        </div>
        <div class="step">
            <div class="stepLabel">办理进度：</div>
            <div id="step_填报中" runat="server" class="stepItem lgray">个人填写></div>
            <div id="step_待单位确认" runat="server" class="stepItem lgray">待单位确认></div>
            <div id="step_已申请" runat="server" class="stepItem lgray">待市建委审核></div>
            <div id="step_已审核" runat="server" class="stepItem lgray">市建委审核></div>               
            <div id="step_已决定" runat="server" class="stepItem lgray">市建委决定></div>
            <div id="step_已办结" runat="server" class="stepItem lgray">住建部生成电子证书（办结，下载电子证书）</div>
            <div class="stepArrow">▶</div>
        </div>
        <div class="DivTitleOn" onclick="DivOnOff(this,'Td3',event);" title="折叠">
            填报格式说明
        </div>
        <div class="DivContent" id="Td3">
            1、办事流程:个人网上申请并上传扫描件-企业网上审核确认-住建委网上审批（咨询电话:010-89150138）。<br />
            2、身份证样式：须为18位二代身份证（带X字母的必须使用英文大写）。<br />
            3、组织机构代码：9位数字或大写字母组合,带“-”的去掉“-”，社会统一信用代码中的第9位至第17位为企业的组织机构代码。<br />
            4、不再提供纸质证书补办业务，请企业或个人自行下载电子证书。<br />
            5、A本增发需要增发企业网上审核确认。<br />
            6、同时担任本市两家（或两家以上）建筑施工企业法人，已取得其中一家建筑施工企业A本的,可申请A本增发。<br />
            7、增发A本的发证日期与有效期同原A本保持一致。
        </div>
        <div class="content">
            <div id="DivEdit" runat="server" style="width: 100%; margin: 10px auto; text-align: center; overflow: hidden;">
                <div style="width: 66%; float: left; clear: left">
                    <table id="TableEdit" runat="server" width="99%" border="0" cellpadding="5" cellspacing="1"
                        class="table2" align="center">
                        <tr class="GridLightBK">
                            <td colspan="6" class="barTitle">申请表</td>
                        </tr>
                        <tr class="GridLightBK">
                            <td width="16%" nowrap="nowrap" align="right">申请日期：
                            </td>
                            <td colspan="4">
                                <asp:Label ID="LabelApplyDate" runat="server" Text=""></asp:Label>
                            </td>
                            <td rowspan="5" align="center" style="width: 110px;">
                                <img id="ImgCode" runat="server" height="140" width="110" alt="照片" src="~/Images/photo_ry.jpg" />
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td align="right">身份证号：
                            </td>
                            <td colspan="4">
                                <asp:Label ID="LabelWorkerCertificateCode" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td nowrap="nowrap" align="right">姓 名：
                            </td>
                            <td colspan="4">
                                <asp:Label ID="LabelWorkerName" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr class="GridLightBK">

                            <td align="right">性别：
                            </td>
                            <td width="16%">
                                <asp:Label ID="LabelSex" runat="server" Text=""></asp:Label>

                            </td>
                            <td align="right" width="16%">出生日期：
                            </td>
                            <td colspan="2" width="32%">
                                <asp:Label ID="LabelBirthday" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td nowrap="nowrap" align="right">
                                <span style="color: Red">* </span>联系电话：
                            </td>
                            <td colspan="4">
                                <telerik:RadTextBox ID="RadTextBoxPeoplePhone" runat="server" Width="80%" Skin="Default" MaxLength="25">
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorPhone" runat="server" ErrorMessage="必填"
                                    ControlToValidate="RadTextBoxPeoplePhone" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr class="GridLightBK" id="tr_upPhoto" runat="server" visible="false">
                            <td colspan="6" align="left">

                                <span id="SpanTip" runat="server" style="font-size: 12px;">附件-照片：（格式要求：近期彩色一寸白底标准正面免冠证件照，jpg格式图片，最大为50K，宽高110 X 140像素）</span>
                                &nbsp;&nbsp;&nbsp;&nbsp;辅助工具下载：<a href="../Images/1寸照片生成器.exe" style="text-decoration: underline; color: Blue;">
                                    <img alt="" src="../Images/Soft_common.gif" style="border-width: 0;" />1寸照片生成器.exe</a>
                            </td>
                        </tr>
                        <tr class="GridLightBK" id="tr_upPhotoCtl" runat="server" visible="false">
                            <td colspan="3" align="right">上传照片： </td>
                            <td colspan="3" align="left">
                                <telerik:RadUpload ID="RadUploadFacePhoto" runat="server" ControlObjectsVisibility="None"
                                    Height="23px" MaxFileInputsCount="1" OverwriteExistingFiles="True" Skin="Hot"
                                    EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false" ReadOnlyFileInputs="False"
                                    Width="300px">
                                    <Localization Select="选 择" />
                                </telerik:RadUpload>
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td colspan="3" align="center">现有证书信息
                            </td>
                            <td colspan="3" align="center">增发证书信息
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td colspan="3" align="center"></td>
                            <td colspan="3" align="right">
                                <div id="divSelectUnit" runat="server" style="display: none;">
                                    <input id="ButtonSelectQY" type="button" value="请选择一个单位" class="bt_maxlarge" onclick="javascript: selectQY();" />
                                </div>
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td nowrap="nowrap" align="right" width="16%">单位全称：
                            </td>
                            <td colspan="2">
                                <asp:Label ID="LabelUnitName" runat="server" Text=""></asp:Label>
                            </td>
                            <td nowrap="nowrap" align="right" width="16%">&nbsp;<span style="color: Red">* </span>单位全称：
                            </td>
                            <td colspan="2">
                                <telerik:RadTextBox ID="RadTextBoxUnitNameMore" runat="server" Width="90%" Skin="Default" MaxLength="100" OnTextChanged="RadTextBoxUnitNameMore_TextChanged" AutoPostBack="true">
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="必填"
                                    ControlToValidate="RadTextBoxUnitNameMore" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td nowrap="nowrap" align="right">组织机构代码（9位）：
                            </td>
                            <td colspan="2">
                                <asp:Label ID="LabelUnitCode" runat="server" Text=""></asp:Label>

                            </td>
                            <td nowrap="nowrap" align="right">&nbsp;<span style="color: Red">* </span>组织机构代码（9位）：
                            </td>
                            <td colspan="2">
                                <telerik:RadTextBox ID="RadTextBoxUnitCodeMore" runat="server" Width="90%" Skin="Default"
                                    MaxLength="9">
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="必填"
                                    ControlToValidate="RadTextBoxUnitCodeMore" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td width="10%" align="right">证书编号：
                            </td>
                            <td colspan="2">
                                <asp:Label ID="LabelCertificateCode" runat="server" Text=""></asp:Label>

                            </td>
                            <td align="right">证书编号：
                            </td>
                            <td colspan="2">系统自动核发
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td align="right" nowrap="nowrap">发证日期：
                            </td>
                            <td colspan="2">
                                <asp:Label ID="LabelValidStartDate" runat="server" Text=""></asp:Label>

                            </td>
                            <td align="right" nowrap="nowrap">发证日期：
                            </td>
                            <td align="left" colspan="2">系统自动核发
                            </td>
                        </tr>
                        <tr class="GridLightBK">

                            <td align="right" nowrap="nowrap">有效期至：
                            </td>
                            <td colspan="2" align="left">
                                <asp:Label ID="LabelValidEndDate" runat="server" Text=""></asp:Label>

                            </td>
                            <td align="right" nowrap="nowrap">有效期至：
                            </td>
                            <td align="left" colspan="2">系统自动核发
                                
                            </td>
                        </tr>
                        <tr class="GridLightBK" id="trFuJanTitel" runat="server" visible="false">
                            <td colspan="6" class="barTitle">附件上传</td>
                        </tr>
                        <tr class="GridLightBK" id="trFuJan" runat="server" visible="false">
                            <td align="left" style="line-height: 20px;" colspan="6">
                                <div class="DivContent">
                                    <p>附件要求说明：</p>
                                    <p>
                                        1、增发申请表1份（须加盖聘用单位公章）；<br />
                                        2、身份证正反面1份；<br />
                                        3、增发企业营业执照扫描件1份。
                                    </p>
                                </div>
                                <div class="fujian">
                                    申请表扫描件：<span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.变更申请表扫描件%>','变更申请表扫描件','','<%=ApplyID%>')">选择文件</span><span class="tishi">（请打印申请表，聘用单位盖章签字后扫描上传，jpg格式图片最大500K）</span>
                                </div>
                                <div class="fujian">
                                    身份证扫描件：<span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.证件扫描件%>','证件扫描件','','<%=ApplyID%>')">选择文件</span><span class="tishi">（上传身份证正反面扫描件原件、jpg格式图片，jpg格式图片最大500K）</span>
                                </div>
                                <div class="fujian">
                                    增发企业营业执照扫描件：<span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.企业营业执照扫描件%>','企业营业执照扫描件','','<%=ApplyID%>')">选择文件</span><span class="tishi">（要求：上传企业营业执照扫描件原件、jpg格式图片,最大500K）</span>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <div id="divCheckHistory" runat="server" style="width: 100%; padding-top: 20px; text-align: center; clear: both;">
                        <table id="Table1" runat="server" width="100%" border="0" cellpadding="5" cellspacing="1" class="table" style="text-align: center; margin: 10px auto">
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
                                                        <ItemStyle Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="流程" UniqueName="Action" DataField="Action">
                                                        <ItemStyle Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="办理人" UniqueName="ActionMan" DataField="ActionMan" Display="false">
                                                        <ItemStyle Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="办理时间" UniqueName="ActionData" DataField="ActionData" HtmlEncode="false" DataFormatString="{0:yyyy.MM.dd}">
                                                        <ItemStyle Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="办理结果" UniqueName="ActionResult" DataField="ActionResult">
                                                        <ItemStyle Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="办理意见" UniqueName="ActionRemark" DataField="ActionRemark">
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                            <HeaderStyle Font-Bold="True" BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1" Wrap="false" />
                                            <ItemStyle CssClass="subtable" />
                                            <AlternatingItemStyle CssClass="subtable" />
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <table id="tableWorker" runat="server" visible="false" style="width: 100%; padding-bottom: 20px;">
                        <tr>
                            <td align="center" colspan="2">
                                 <div id="divStepDesc" runat="server" style="line-height: 300%;">操作流程：个人申请保存-->点击导出打印(导出申请表)-->上传相关附件-->提交单位审核</div>
                                <asp:Button ID="ButtonSave" Text='保 存' runat="server" CssClass="bt_large" OnClick="ButtonSave_Click"></asp:Button>&nbsp;
                                <asp:Button ID="ButtonExport" runat="server" Text="导出打印" CssClass="bt_large" OnClick="ButtonExport_Click" />&nbsp;
                                <asp:Button ID="ButtonExit" Text="取消申报" runat="server" CssClass="bt_large" Enabled="false"
                                    OnClick="ButtonExit_Click"></asp:Button>&nbsp;
                                <asp:Button ID="ButtonDelete" Text='删 除' runat="server" CssClass="bt_large" OnClick="ButtonDelete_Click"
                                    OnClientClick="return confirm('你确定要删除吗?');" Enabled="false"></asp:Button>&nbsp;
                                <br />
                            </td>
                        </tr>
                    </table>
                    <table id="TableUnitCheck" runat="server" visible="false" width="100%" border="0" cellpadding="5" cellspacing="1" class="table" style="text-align: center; margin: 10px auto">
                        <tr class="GridLightBK">
                            <td colspan="2" class="barTitle">单位审核</td>
                        </tr>
                        <tr class="GridLightBK">
                            <td width="20%" align="right">处理结果：</td>
                            <td width="80%" align="left">
                                <asp:RadioButtonList ID="RadioButtonListOldUnitCheckResult" runat="server" RepeatDirection="Vertical" TextAlign="right">
                                    <asp:ListItem Text="同意" Value="同意" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="不同意" Value="不同意"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td width="20%" align="right">处理意见：</td>
                            <td width="80%" align="left">

                                <asp:TextBox ID="TextBoxOldUnitCheckRemark" runat="server" TextMode="MultiLine" Rows="3" Width="99%" MaxLength="100" Text="提交建委审核"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td colspan="2" align="center">
                                <asp:Button ID="ButtonUnitCheck" runat="server" CssClass="bt_large" Text="确 定" OnClick="ButtonUnitCheck_Click" />&nbsp;&nbsp;
                                         
                            </td>
                        </tr>
                    </table>
                    <table id="TableJWCheck" runat="server" visible="false" border="0" cellpadding="5" cellspacing="1" width="100%" class="table" style="text-align: center; margin: 10px auto">
                        <tr class="GridLightBK">
                            <td colspan="2" class="barTitle">增发审核</td>
                        </tr>
                        <tr class="GridLightBK">
                            <td width="20%" align="right">处理结果：</td>
                            <td width="80%" align="left">
                                <asp:RadioButtonList ID="RadioButtonListJWCheck" runat="server" RepeatDirection="Vertical" TextAlign="right">
                                    <asp:ListItem Text="通过" Value="通过" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="不通过" Value="不通过"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td width="20%" align="right">处理意见：</td>
                            <td width="80%" align="left">

                                <asp:TextBox ID="TextBoxCheckResult" runat="server" TextMode="MultiLine" Rows="3" Width="99%" MaxLength="100" Text="通过"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td colspan="2" align="center">
                                <asp:Button ID="ButtonCheck" runat="server" CssClass="bt_large" Text="确 定" OnClick="ButtonCheck_Click" />&nbsp;&nbsp;
                                         
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
                                                                <asp:ImageButton runat="server" ID="ImageButton1" ImageUrl="../images/Cancel.gif" CommandName="Delete" Visible='<%#(IfExistRoleID("0") == true && ((ViewState["CertificateMoreMDL"] as Model.CertificateMoreMDL).ApplyStatus == Model.EnumManager.CertificateMore.NewSave||(ViewState["CertificateMoreMDL"] as Model.CertificateMoreMDL).ApplyStatus == Model.EnumManager.CertificateMore.SendBack))?true:false%>' OnClientClick="javascript:if(confirm('您确定要删除么?')==false) return false" />

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
    <div id="winpop"></div>
</asp:Content>
