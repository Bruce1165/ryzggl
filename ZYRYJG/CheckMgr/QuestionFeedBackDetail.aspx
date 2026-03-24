<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuestionFeedBackDetail.aspx.cs" Inherits="ZYRYJG.CheckMgr.QuestionFeedBackDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html>

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
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server">
        </telerik:RadStyleSheetManager>
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Windows7">
        </telerik:RadWindowManager>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1"
            EnableAJAX="true">
            <AjaxSettings>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true" Skin="Default" />
        <style>
            .pl64 {
                padding-left: 64px;
            }

            .b {
                font-weight: bold;
            }

            .black {
                color: #333;
            }
        </style>
        <div class="div_out">
            <div id="div_top" class="dqts">
                <div style="float: left;">
                    当前位置 &gt;&gt;综合监管 &gt;&gt;监管问题反馈 &gt;&gt;<strong>反馈详细信息</strong>
                </div>
            </div>
            <div class="content">
                <div class="step">
                    <div class="stepLabel">办理进度：</div>
                    <div id="step_待反馈" runat="server" class="stepItem lgray">个人填写反馈></div>
                    <div id="step_待审查" runat="server" class="stepItem lgray">个人上报区县></div>
                    <div id="step_待复审" runat="server" class="stepItem lgray">区县审查审核></div>
                    <div id="step_待决定" runat="server" class="stepItem lgray">市建委复审></div>
                    <div id="step_已决定" runat="server" class="stepItem lgray">市建委决定</div>
                    <div class="stepArrow">▶</div>
                </div>
                <div style="width: 100%; margin: 10px auto; text-align: center; overflow: hidden;">
                    <div>
                        <a href="../Template/反馈填报须知.doc" target="_blank"><font style="color: blue; font-size: 18px; font-weight: bold; text-decoration: none; margin-left: 10px;">【反馈填报须知】</font></a>
                        <a href="../Template/常见问题及统一解释.doc" target="_blank"><font style="color: blue; font-size: 18px; font-weight: bold; text-decoration: none; margin-left: 10px;">【常见问题及统一解释】</font></a>
                    </div>
                    <div style="width: 66%; float: left; clear: left">
                        <table runat="server" id="EditTable" class="detailTable" cellpadding="5" cellspacing="1">
                            <tr class="GridLightBK">
                                <td colspan="4" class="barTitle">监管问题信息</td>
                            </tr>
                            <tr>
                                <td class="infoHead">姓名：
                                </td>
                                <td class="formItem_1">
                                    <asp:Label ID="LabelWorkerName" runat="server" Text=""></asp:Label>
                                </td>
                                <td class="infoHead">证件号码：
                                </td>
                                <td class="formItem_1">
                                    <asp:Label ID="LabelWorkerCertificateCode" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr id="trPhone" runat="server">
                                <td class="infoHead">联系电话：
                                </td>
                                <td class="formItem_1">
                                    <asp:Label ID="LabelPhone" runat="server" Text=""></asp:Label>
                                </td>
                                <td class="infoHead">发布日期：
                                </td>
                                <td class="formItem_1">
                                    <asp:Label ID="LabelPublishiTime" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="infoHead">
                                    <asp:Label ID="LabelPostTypeName" runat="server" Text=""></asp:Label>：
                                </td>
                                <td class="formItem_1" colspan="3">
                                    <asp:Label ID="LabelCertificateCode" runat="server" Text=""></asp:Label>
                                </td>

                            </tr>
                            <tr>
                                <td class="infoHead" style="width: 20%">企业名称：
                                </td>
                                <td class="formItem_1">
                                    <asp:Label ID="LabelUnit" runat="server" Text=""></asp:Label>

                                </td>
                                <td class="infoHead">所属区：
                                </td>
                                <td class="formItem_1">
                                    <asp:Label ID="LabelCountry" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="infoHead">反馈截止时间：</td>
                                <td class="formItem_1">
                                    <asp:Label ID="LabelLastReportTime" runat="server" Text=""></asp:Label>
                                </td>
                                <td class="infoHead">个人反馈时间：
                                </td>
                                <td class="formItem_1">
                                    <asp:Label ID="LabelWorkerRerpotTime" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="infoHead">存在问题：
                                </td>
                                <td class="formItem_1" id="tdQuestion" runat="server" style="color: red" colspan="3"></td>
                            </tr>
                            <tr class="GridLightBK">
                                <td colspan="4" class="barTitle">在施锁定比对<span style="font-weight: normal">（数据来源：北京市建筑工程交易中心）</span></td>
                            </tr>
                            <tr>
                                <td class="formItem_1" id="tdZSSD" runat="server" style="color: red; padding-left: 20px" colspan="4"></td>
                            </tr>
                            <tr class="GridLightBK">
                                <td colspan="4" class="barTitle">社保比对<span style="font-weight: normal">（数据来源：北京市人力资源和社会保障局）</span></td>
                            </tr>
                            <tr>
                                <td class="formItem_1" colspan="4">
                                    <telerik:RadGrid ID="RadGridSheBao" AllowPaging="false" runat="server" AutoGenerateColumns="False" PagerStyle-AlwaysVisible="true"
                                        AllowSorting="false" GridLines="None" CellPadding="0" Width="100%" Skin="Blue" EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false">
                                        <ClientSettings EnableRowHoverStyle="false">
                                            <Selecting AllowRowSelect="false" />
                                        </ClientSettings>
                                        <MasterTableView NoMasterRecordsText="没有可显示的记录">
                                            <Columns>
                                                <telerik:GridBoundColumn UniqueName="WorkerName" DataField="WorkerName" HeaderText="姓名">
                                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="ENT_Name" DataField="ENT_Name" HeaderText="社保缴费单位">
                                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="JFYF" DataField="JFYF" HeaderText="缴费月份">
                                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn UniqueName="01" HeaderText="养老">
                                                    <ItemTemplate>
                                                        <%# Eval("01") != System.DBNull.Value ? "✔" : ""%>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                    <ItemStyle HorizontalAlign="Center" ForeColor="#009933" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn UniqueName="02" HeaderText="失业">
                                                    <ItemTemplate>
                                                        <%# Eval("02") != System.DBNull.Value ? "✔" : ""%>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                    <ItemStyle HorizontalAlign="Center" ForeColor="#009933" />
                                                </telerik:GridTemplateColumn>
                                                <%--  <telerik:GridTemplateColumn UniqueName="03" HeaderText="医疗">
                                                    <ItemTemplate>
                                                        <%# Eval("03") != System.DBNull.Value ? "✔" : ""%>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                    <ItemStyle HorizontalAlign="Center" ForeColor="#009933" />
                                                </telerik:GridTemplateColumn>--%>
                                                <telerik:GridTemplateColumn UniqueName="04" HeaderText="工伤">
                                                    <ItemTemplate>
                                                        <%# Eval("04") != System.DBNull.Value ? "✔" : ""%>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                    <ItemStyle HorizontalAlign="Center" ForeColor="#009933" />
                                                </telerik:GridTemplateColumn>
                                                <%-- <telerik:GridTemplateColumn UniqueName="05" HeaderText="生育">
                                                    <ItemTemplate>
                                                        <%# Eval("05") != System.DBNull.Value ? "✔" : ""%>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                    <ItemStyle HorizontalAlign="Center" ForeColor="#009933" />
                                                </telerik:GridTemplateColumn>--%>
                                                <telerik:GridBoundColumn UniqueName="CJSJ" DataField="CJSJ" HeaderText="比对日期">
                                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                            <HeaderStyle Font-Bold="True" />
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td colspan="4" class="barTitle">个人反馈信息</td>
                            </tr>
                            <tr class="GridLightBK" id="trCaseDesc" runat="server">
                                <td colspan="4">
                                    <div id="DivCaseDesc" runat="server" style="margin: 12px 12px; padding: 12px 20px; line-height: 160%; background-color: #EDF6FB"></div>
                                    <div id="DivCheckHelp" runat="server" style="display: none; margin: 12px 12px; padding: 12px 12px; background-color: #efefef">
                                        <p>审核上传材料要求标准：</p>
                                         <p>
                                            <b>1、本人已完成整改，完成证书注销，请上传以下相关材料。</b>
                                        </p>
                                        <div style="padding-left: 32px">
                                            （1）当前证书注册状态截图<br />
                                            （2）个人承诺<br />
                                        </div>
                                        <p>
                                            <b>2、本人已完成整改，社保正常缴纳且与注册单位一致，请上传以下相关材料。</b>
                                        </p>
                                        <div style="padding-left: 32px">
                                            （1）当前社会保险个人权益记录<br />
                                            （2）当前证书注册状态截图<br />
                                            （3）个人承诺<br />
                                        </div>
                                        <p>
                                            <b>3.本人属于以下六类特殊情形，不属于“挂证”行为，根据自身实际情况进行选择，提交对应的相关材料。</b>
                                        </p>
                                        <div style="padding-left: 32px">
                                            （1）注册单位与社保缴费单位为总分公司关系的：<br />
                                            应提供该单位分支机构的《营业执照》和《社会保险个人权益记录》<br />

                                            （2）达到法定退休年龄，正式退休和依法提前退休的：<br />
                                            应提供注册人员的《退休证》、本市或外省市社会保险机构出具的《城乡居民养老保险待遇核定表》<br />

                                            （3）因事业单位改制等原因保留事业单位身份，实际工作单位为所在事业单位下属企业，社会保险由该事业单位缴纳的<br />
                                            应提供事业单位改制有关计划、批复文件，提供该企业上级事业单位人事部门出具的注册人员在该企业工作的人事关系证明（应注明其社会保险由该企业上级事业单位统一缴纳）、企业上级事业单位产权登记部门出具的该企业国有产权登记表、注册人员社会保险个人权益记录<br />

                                            （4）属于大专院校所属勘察设计、工程监理、工程造价单位聘请的本校在职教师或科研人员，社会保险由所在院校缴纳的：<br />
                                            应提供该企业归属管理的大专院校人事部门出具的注册人员在该企业工作的人事关系证明（应注明其为本校在职教师或科研人员、其社会保险由该企业归属管理的大专院校统一缴纳）、该企业归属管理的大专院校产权登记部门出具的该企业国有产权登记表、注册人员社会保险人权益记录；<br />

                                            （5）因企业改制、征地拆迁等买断社会保险的：<br />
                                            应提供企业改制、征地拆迁等买断社会保险协议书、注册人员社会保险个人权益记录；<br />

                                            （6）有法律法规、国家政策依据的其他情形：<br />
                                            应提供相关证明材料。<br />
                                        </div>
                                    </div>
                                </td>
                            </tr>


                            <tr class="GridLightBK" id="trFuJan" runat="server" visible="false">
                                <td colspan="4" style="padding-left: 16px">
                                    <div>
                                        <b>请您从下列情形中，选择与自身实际情况一致的情形，并上传相对应的资料。</b><br />
                                        <span style="color: red">（所有上传材料扫描件要求与原件1:1比例正向扫描上传,信息清晰完整。附件格式要求：jpg格式图片,最大500K。）</span>
                                    </div>
                                    <div>
                                        <p>
                                            <b>
                                                <asp:RadioButton ID="RadioButtonCancel" runat="server" Text="1、本人已完成整改，完成证书注销，请上传以下相关材料。" GroupName="fback" /></b>
                                        </p>
                                        <div style="padding-left: 32px">                                           
                                            <p>
                                                <span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.当前证书注册状态截图%>','当前证书注册状态截图','','<%=DataID%>')">（1）当前证书注册状态截图</span><span style="color: Red">* </span><span style="margin-left: 200px">注销注册状态截图样例： </span>一建<img src="../Images/shot_yj.png" style="width: 32px; height: auto" class="img500" />， 二建<img src="../Images/shot_ej.png" style="width: 32px; height: auto" class="img500" />
                                                ， 监理<img src="../Images/shot_jl.png" style="width: 32px; height: auto" class="img500" />
                                            </p>
                                            <p><span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.个人承诺%>','个人承诺','','<%=DataID%>')">（2）个人承诺</span><span style="color: Red">* </span><span style="margin-left: 200px"><a href="../Template/监管反馈承诺书.doc">下载【监管反馈承诺书模板】</a> </span></p>
                                        </div>
                                    </div>
                                    <div>
                                        <p>
                                            <b>
                                                <asp:RadioButton ID="RadioButtonSheBao" runat="server" Text="2、本人已完成整改，社保正常缴纳且与注册单位一致，请上传以下相关材料。" GroupName="fback" /></b>
                                        </p>
                                        <div style="padding-left: 32px">
                                            <p>
                                                <span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.社保扫描件%>','社保扫描件','','<%=DataID%>')">（1）当前社会保险个人权益记录和公积金缴存记录</span> <span style="color: Red">* </span>
                                                <br />
                                                <span style="padding-left: 30px">注：当前注册单位社会保险个人权益记录和外埠社保暂停、销户等状态截图或证明；当前注册单位个人住房公积金缴存单位信息截图 、外埠公积金管理部门出具的个人公积金已封存或已转出销户的证明。</span>
                                            </p>                                           
                                            <p>
                                                <span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.当前证书注册状态截图%>','当前证书注册状态截图','','<%=DataID%>')">（2）当前证书注册状态截图</span><span style="color: Red">* </span>
                                            </p>
                                            <p><span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.个人承诺%>','个人承诺','','<%=DataID%>')">（3）个人承诺</span><span style="color: Red">* </span><span style="margin-left: 200px"><a href="../Template/监管反馈承诺书.doc">下载【监管反馈承诺书模板】</a> </span></p>
                                        </div>
                                    </div>

                                    <div>
                                        <p><b>3、本人属于以下六类特殊情形，不属于“挂证”行为，根据自身实际情况进行选择，提交对应的相关材料。</b> </p>
                                        <div style="padding-left: 12px">
                                            <p>
                                                <asp:RadioButton ID="RadioButton21" runat="server" Text="（1）注册单位与社保缴费单位为总分公司关系的：" GroupName="fback" Font-Bold="true" />
                                            </p>
                                            <p class="pl64">
                                                应提供该单位分支机构的<span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.符合规定情形的相关证明%>','符合规定情形的相关证明','','<%=DataID%>')">《营业执照》</span>
                                                和<span class="link" onclick="javascript:tips_pop ('<%=Model.EnumManager.FileDataType.符合规定情形的相关证明%>','符合规定情形的相关证明','','<%=DataID%>')">《社会保险个人权益记录》</span>；
                                            </p>
                                            <p>
                                                <asp:RadioButton ID="RadioButton22" runat="server" Text="（2）达到法定退休年龄，正式退休和依法提前退休的：" GroupName="fback" Font-Bold="true" />
                                            </p>
                                            <p class="pl64">应提供注册人员的<span class="link" onclick="javascript:tips_pop ('<%=Model.EnumManager.FileDataType.符合规定情形的相关证明%>','符合规定情形的相关证明','','<%=DataID%>')">《退休证》</span>、本市或外省市社会保险机构出具的<span class="link" onclick="javascript:tips_pop ('<%=Model.EnumManager.FileDataType.符合规定情形的相关证明%>','符合规定情形的相关证明','','<%=DataID%>')">《城乡居民养老保险待遇核定表》</span>；</p>
                                            <p>
                                                <asp:RadioButton ID="RadioButton23" runat="server" Text="（3）因事业单位改制等原因保留事业单位身份，实际工作单位为所在事业单位下属企业，社会保险由该事业单位缴纳的" GroupName="fback" Font-Bold="true" />
                                            </p>
                                            <p class="pl64">应提供事业单位改制有关计划、批复文件，提供该企业上级事业单位人事部门出具的注册人员在该企业工作的<span class="link" onclick="javascript:tips_pop ('<%=Model.EnumManager.FileDataType.符合规定情形的相关证明%>','符合规定情形的相关证明','','<%=DataID%>')">人事关系证明</span>（应注明其社会保险由该企业上级事业单位统一缴纳）、企业上级事业单位产权登记部门出具的该<span class="link" onclick="javascript:tips_pop ('<%=Model.EnumManager.FileDataType.符合规定情形的相关证明%>','符合规定情形的相关证明','','<%=DataID%>')">企业国有产权登记表</span>、注册人员<span class="link" onclick="javascript:tips_pop ('<%=Model.EnumManager.FileDataType.符合规定情形的相关证明%>','符合规定情形的相关证明','','<%=DataID%>')">社会保险个人权益记录</span>；</p>
                                            <p>
                                                <asp:RadioButton ID="RadioButton24" runat="server" Text="（4）属于大专院校所属勘察设计、工程监理、工程造价单位聘请的本校在职教师或科研人员，社会保险由所在院校缴纳的：" GroupName="fback" Font-Bold="true" />
                                            </p>
                                            <p class="pl64">应提供该企业归属管理的大专院校人事部门出具的注册人员在该企业工作的<span class="link" onclick="javascript:tips_pop ('<%=Model.EnumManager.FileDataType.符合规定情形的相关证明%>','符合规定情形的相关证明','','<%=DataID%>')">人事关系证明</span>（应注明其为本校在职教师或科研人员、其社会保险由该企业归属管理的大专院校统一缴纳）、该企业归属管理的大专院校产权登记部门出具的该<span class="link" onclick="javascript:tips_pop ('<%=Model.EnumManager.FileDataType.符合规定情形的相关证明%>','符合规定情形的相关证明','','<%=DataID%>')">企业国有产权登记表</span>、注册人员<span class="link" onclick="javascript:tips_pop ('<%=Model.EnumManager.FileDataType.符合规定情形的相关证明%>','符合规定情形的相关证明','','<%=DataID%>')">社会保险个人权益记录</span>；</p>
                                            <p>
                                                <asp:RadioButton ID="RadioButton25" runat="server" Text="（5）因企业改制、征地拆迁等买断社会保险的：" GroupName="fback" Font-Bold="true" />
                                            </p>
                                            <p class="pl64">应提供企业改制、征地拆迁等买断<span class="link" onclick="javascript:tips_pop ('<%=Model.EnumManager.FileDataType.符合规定情形的相关证明%>','符合规定情形的相关证明','','<%=DataID%>')">社会保险协议书</span>、注册人员<span class="link" onclick="javascript:tips_pop ('<%=Model.EnumManager.FileDataType.符合规定情形的相关证明%>','符合规定情形的相关证明','','<%=DataID%>')">社会保险个人权益记录</span>；</p>
                                            <p>
                                                <asp:RadioButton ID="RadioButton26" runat="server" Text="（6）有法律法规、国家政策依据的其他情形：" GroupName="fback" Font-Bold="true" />
                                            </p>
                                            <p class="pl64"><span class="link" onclick="javascript:tips_pop ('<%=Model.EnumManager.FileDataType.符合规定情形的相关证明%>','符合规定情形的相关证明','','<%=DataID%>')">应提供相关证明材料。</span></p>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <div id="divGR" visible="false" runat="server" style="width: 100%; padding-top: 20px; text-align: center; clear: both;">
                            <div id="divStepDesc" runat="server" style="line-height: 300%;">操作流程：个人保存反馈-->上传相关附件-->提交审核</div>
                            <asp:Button ID="ButtonSave" runat="server" Text="保 存" CssClass="bt_large" OnClick="ButtonSave_Click" UseSubmitBehavior="false" OnClientClick="try{var isValid = Page_ClientValidate();if(isValid==false) return false;this.value='正在提交';this.disabled=true;}catch{}" />

                            &nbsp;&nbsp;<asp:Button ID="ButtonSubmit" runat="server" Text="提交审核" CssClass="bt_large" OnClick="ButtonSubmit_Click" Enabled="false" />

                        </div>
                        <div id="divCheckHistory" visible="true" runat="server" style="width: 98%; padding-top: 20px; text-align: center; clear: both;">
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


                        <div id="divQX" runat="server" visible="false" style="width: 98%; padding-top: 20px; text-align: center; clear: both;">
                            <table id="TableEdit" runat="server" width="100%" border="0" cellpadding="5" cellspacing="1" class="table" style="text-align: center; margin: 10px auto">
                                <tr class="GridLightBK">
                                    <td colspan="2" class="barTitle">区县审查</td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td width="20%" align="right">审查结果：</td>
                                    <td width="80%" align="left">
                                        <asp:RadioButtonList ID="RadioButtonListApplyStatus" runat="server" RepeatDirection="Horizontal" TextAlign="right">
                                            <asp:ListItem Text="通过" Value="通过" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="不通过" Value="不通过"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td width="20%" align="right">驳回原因：</td>
                                    <td width="80%" align="left">

                                        <asp:TextBox ID="TextBoxApplyGetResult" runat="server" TextMode="MultiLine" Rows="3" Width="99%" Text="予以受理"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td colspan="2" align="center">
                                        <asp:Button ID="BtnCountryCheck" runat="server" CssClass="bt_large" Text="确认提交" OnClick="BtnCountryCheck_Click" />&nbsp;&nbsp;
                                            <input id="BtnReturn" type="button" class="bt_large" value="返 回" onclick='javascript: hideIfam()' />
                                    </td>
                                </tr>
                            </table>

                        </div>
                        <div id="divCheck" runat="server" visible="false" style="width: 98%; padding-top: 20px; text-align: center; clear: both;">
                            <table id="Table4" runat="server" width="100%" border="0" cellpadding="5" cellspacing="1" class="table" style="text-align: center; margin: 10px auto">
                                <tr class="GridLightBK">
                                    <td colspan="2" class="barTitle">市级复审</td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td style="width:20%; text-align:right" >复审结果：</td>
                                    <td style="width:80%; text-align:left" >
                                        <asp:RadioButtonList ID="RadioButtonListExamineResult" runat="server" RepeatDirection="Horizontal" TextAlign="right">
                                            <asp:ListItem Text="通过" Value="通过" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="不通过" Value="不通过"></asp:ListItem>
                                        </asp:RadioButtonList>

                                        
                                     
                                    </td>
                                </tr>
                                 <tr class="GridLightBK" id="trPassType" runat="server">
                                   <td style="text-align:right">合格类型：</td>
                                    <td style="text-align:left">
                                           <asp:RadioButtonList ID="RadioButtonListPassType" runat="server" RepeatDirection="Vertical" TextAlign="right">
                                            <asp:ListItem Text="已注销完成整改" Value="已注销完成整改" ></asp:ListItem>
                                            <asp:ListItem Text="社保一致完成整改" Value="社保一致完成整改"></asp:ListItem>
                                               <asp:ListItem Text="特殊六类总分公司" Value="特殊六类总分公司"></asp:ListItem>
                                               <asp:ListItem Text="特殊六类已退休" Value="特殊六类已退休"></asp:ListItem>
                                               <asp:ListItem Text="特殊六类事业单位改制" Value="特殊六类事业单位改制"></asp:ListItem>
                                               <asp:ListItem Text="特殊六类其他" Value="特殊六类其他"></asp:ListItem>
                                        </asp:RadioButtonList>
                                 </td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td style="text-align:right">驳回原因：</td>
                                     <td style="text-align:left">
                                        <asp:TextBox ID="TextBoxExamineRemark1" runat="server" TextMode="MultiLine" Rows="3" Width="99%" Text="允许通过"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td colspan="2"  style="text-align:center">
                                        <asp:Button ID="ButtonCheck" runat="server" CssClass="bt_large" Text="确认提交" OnClick="ButtonCheck_Click" />&nbsp;&nbsp;
                                            <input id="BtnReturnck" type="button" class="bt_large" value="返 回" onclick='javascript: hideIfam()' />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="divDecide" runat="server" visible="false" style="width: 98%; padding-top: 20px; text-align: center; clear: both;">
                            <table id="Table3" runat="server" width="100%" border="0" cellpadding="5" cellspacing="1" class="table" style="text-align: center; margin: 10px auto">
                                <tr class="GridLightBK">
                                    <td colspan="2" class="barTitle">市级决定</td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td width="20%" align="right">决定结果：</td>
                                    <td width="80%" align="left">
                                        <asp:RadioButtonList ID="RadioButtonListDecide" runat="server" RepeatDirection="Horizontal" TextAlign="right">
                                            <asp:ListItem Text="通过" Value="通过" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="不通过" Value="不通过"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td width="20%" align="right">驳回原因：</td>
                                    <td width="80%" align="left">
                                        <asp:TextBox ID="TextBoxConfirmDesc" runat="server" TextMode="MultiLine" Rows="3" Width="99%" Text="允许通过"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td colspan="2" align="center">
                                        <asp:Button ID="ButtonDecide" runat="server" CssClass="bt_large" Text="确认提交" OnClick="ButtonDecide_Click" />&nbsp;&nbsp;
                                            <input id="BtnReturn4" type="button" class="bt_large" value="返 回" onclick='javascript: hideIfam()' />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="divSendBack" runat="server" visible="false" style="width: 98%; padding-top: 20px; text-align: center; clear: both;">
                            <table width="100%" border="0" cellpadding="5" cellspacing="1" class="table" style="text-align: center; margin: 20px auto">
                                <tr class="GridLightBK">
                                    <td colspan="2" class="barTitle">审核流程后退操作</td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td width="20%" align="right">请选择要后退到的节点：</td>
                                    <td width="80%" align="left">
                                        <telerik:RadComboBox ID="RadComboBoxReturnApplyStatus" runat="server" Width="80">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="待审查" Value="3" />
                                                <telerik:RadComboBoxItem Text="待复审" Value="4" />
                                                 <telerik:RadComboBoxItem Text="待决定" Value="6" />
                                                 <telerik:RadComboBoxItem Text="已决定" Value="7" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td colspan="2" align="center">
                                        <asp:Button ID="ButtonSendBack" runat="server" CssClass="bt_large" Text="执行后退" OnClick="ButtonSendBack_Click" OnClientClick="javascript:if(!confirm('您确定要后退么?')) return false;" CausesValidation="false" />&nbsp;&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </div>
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
                                                                    <asp:ImageButton runat="server" ID="ImageButton1" ImageUrl="../images/Cancel.gif" CommandName="Delete" Visible='<%#(IfExistRoleID("0") == true && ((ViewState["CheckFeedBackMDL"] as Model.CheckFeedBackMDL).DataStatus == Model.EnumManager.CheckFeedStatus.待反馈 || (ViewState["CheckFeedBackMDL"] as Model.CheckFeedBackMDL).DataStatus == Model.EnumManager.CheckFeedStatus.已驳回))?true:false%>' OnClientClick="javascript:if(confirm('您确定要删除么?')==false) return false" />

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
        <div id="winpop">
        </div>
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

                $(".img500").hover(function () {

                    $(this).stop(true, true);
                    var imgWid2 = 0; var imgHei2 = 0;//局部变量
                    imgWid = $(this).width();
                    imgHei = $(this).height();
                    imgWid2 = imgWid * 20;
                    imgHei2 = imgHei * 20;

                    $("#divImg").css({ "float": "right", "overflow": "visible" });
                    $(this).animate({ "width": imgWid2, "height": imgHei2, "margin-left": -imgWid * (20 - 1), "position": "absolute", "z-index": 999 });
                }, function () {
                    $("#divImg").css({ "float": "right", "overflow": "auto" });
                    $(this).stop().animate({ "width": imgWid, "height": imgHei, "margin-left": 0, "position": "relative", "float": "none" });
                });

                $(".img500").click(function () {
                    var nw = window.open($(this)[0].src, "_blank", 'resizable=yes');
                });

                //变换受理结果
                $("#<%= RadioButtonListApplyStatus.ClientID%> input").each(function () {
                    $(this).click(function () {
                        var TextBoxApplyGetResult = $("#<%= TextBoxApplyGetResult.ClientID%>");

                        if ($(this).val() == "通过") {

                            TextBoxApplyGetResult.val("通过");
                            TextBoxApplyGetResult.attr("readonly", "true");
                        }
                        else {
                            TextBoxApplyGetResult.val("审查未通过");
                            TextBoxApplyGetResult.removeAttr("readonly");
                        }
                    });
                });
                //变换审核结果
                $("#<%= RadioButtonListExamineResult.ClientID%> input").each(function () {
                    $(this).click(function () {
                        var TextBoxApplyCheckRemark = $("#<%= TextBoxExamineRemark1.ClientID%>");
                        var trPassType = $("#<%= trPassType.ClientID%>");
                        if ($(this).val() == "通过") {

                            TextBoxApplyCheckRemark.val("通过");
                            TextBoxApplyCheckRemark.attr("readonly", "true");
                            trPassType.removeAttr("style");
                        }
                        else {
                            TextBoxApplyCheckRemark.val("审核未通过");
                            TextBoxApplyCheckRemark.removeAttr("readonly");
                            trPassType.attr("style", "display:none");
                        }
                    });
                });
                //变换决定结果
                $("#<%= RadioButtonListDecide.ClientID%> input").each(function () {
                    $(this).click(function () {
                        var TextBoxApplyCheckRemark = $("#<%= TextBoxConfirmDesc.ClientID%>");

                        if ($(this).val() == "通过") {

                            TextBoxApplyCheckRemark.val("通过");
                            TextBoxApplyCheckRemark.attr("readonly", "true");
                        }
                        else {
                            TextBoxApplyCheckRemark.val("决定未通过");
                            TextBoxApplyCheckRemark.removeAttr("readonly");
                        }
                    });
                });
            })
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
    </form>
</body>
</html>
