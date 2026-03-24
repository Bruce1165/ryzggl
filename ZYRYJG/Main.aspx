<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="ZYRYJG.Main" EnableViewStateMac="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc1" %>
<%@ Register Src="IframeView.ascx" TagName="IframeView" TagPrefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
    <script src="Scripts/Public.js?v=1.005" type="text/javascript"></script>
    <link href="layer/skin/layer.css" rel="stylesheet" />
    <script src="layer/layer.js" type="text/javascript"></script>
    <script src="Scripts/checkBrower.js" type="text/javascript"></script>
    <link href="css/main.css?v=1.012" rel="stylesheet" />
    <link href="Skins/Blue/Grid.Blue.css?v=1.007" rel="stylesheet" type="text/css" />
    <style type="text/css">
        @keyframes blink {         
          70% { padding-left:20px; } 
        } 
        .blinking-text {
          animation: blink 2s infinite;
        }

         @keyframes blink2 {         
          70% { color:#000; } 
        } 
        .blinking-text2 {
          animation: blink2 2s infinite;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Windows7">
        </telerik:RadWindowManager>
        <div class="div_out">
            <div class="dqts">
                <div style="float: left;">
                    当前位置 &gt;&gt; <strong>信息通知</strong>
                </div>

            </div>
            <div class="content">
                <div style="width: 99%; margin: 2px auto 10px auto; text-align: center; padding: 0 0">
                   <%-- <div id="divPeiXun" runat="server" visible="false">
                        <a target="_blank" href='http://localhost:1045/index.aspx?o=<%=getuInfo()%>'>在线公益教育培训入口</a>
                    </div>--%>
                    <div id="div_workerTask" runat="server" visible="false" class="bar" style="width: 98%; margin: 12px auto;">
                        <div class="bartitle">
                            业务办理快速通道
                        </div>
                        <div class="barmain">
                            <div class="barlable">
                                二级建造师注册业务：
                            </div>
                            <div class='round_div'>
                                <a href='Unit/ApplyFirst.aspx'>初始注册</a>
                            </div>
                            <div class='round_div'>
                                <a href='Unit/ApplyList.aspx?o=r'>重新注册</a>
                            </div>
                            <div class='round_div'>
                                <a href='Unit/ApplyList.aspx?o=a'>增项注册</a>
                            </div>
                            <div id="div2" runat="server" class='round_div'>
                                <a href='Unit/ApplyList.aspx?o=y'>延续注册</a>
                            </div>
                            <div id="div3" runat="server" class='round_div'>
                                <a href='Unit/ApplyList.aspx?o=z'>注销注册</a>
                            </div>
                            <div id="Div4" runat="server" class='round_div'>
                                <a href='Unit/ApplyList.aspx?o=c'>个人信息变更</a>
                            </div>
                            <div id="Div5" runat="server" class='round_div'>
                                <a href='Unit/ApplyChangePerson.aspx'>执业企业变更</a>
                            </div>

                            <div id="Div7" runat="server" class='round_div'>
                                <a href='http://zjw.beijing.gov.cn/bjjs/gcjs/kszczn/gs/index.shtml' target="_blank">公示查询</a>
                            </div>
                            <div id="Div8" runat="server" class='round_div'>
                                <a href='http://zjw.beijing.gov.cn/bjjs/gcjs/kszczn/tg/index.shtml' target="_blank">公告查询</a>
                            </div>
                            <div class='round_div'><a href='PersonnelFile/WorkerCertiInfoList.aspx'>电子证书下载</a></div>
                             <div class="barlable">
                                二级造价工程师注册业务：
                            </div>
                            <div class='round_div'>
                                <a href='zjs/zjsApplyFirst.aspx'>初始注册</a>
                            </div>
                            <div id="div9" runat="server" class='round_div'>
                                <a href='zjs/zjsApplyList.aspx?o=y'>延续注册</a>
                            </div>
                            <div id="div10" runat="server" class='round_div'>
                                <a href='zjs/zjsApplyList.aspx?o=z'>注销注册</a>
                            </div>
                            <div id="Div11" runat="server" class='round_div'>
                                <a href='zjs/zjsApplyList.aspx?o=c'>个人信息变更</a>
                            </div>
                            <div id="Div13" runat="server" class='round_div'>
                                <a href='zjs/zjsApplyList.aspx?o=u'>执业企业变更</a>
                            </div>
                            <div class='round_div'><a href='PersonnelFile/WorkerCertiInfoList.aspx'>电子证书下载</a></div>
                            <div class="barlable">
                                从业人员证书业务：
                            </div>
                            <div class='round_div'><a href='CertifManage/CertifChange.aspx?t=j'>证书变更</a></div>
                            <div class='round_div'><a href='CertifManage/CertifChange.aspx?t=z'>证书注销</a></div>
                            <div class='round_div'><a href='CertifManage/CertifChange.aspx?t=l'>证书离京</a></div>
                            <div class='round_div'><a href='RenewCertifates/CertifApply.aspx'>证书续期</a></div>
                            <div class='round_div'><a href='CertifManage/CertifMoreApplyList.aspx'>三类人A本增发</a></div>
                            <div class='round_div'><a href='CertifEnter/CertifEnterApplyList.aspx'>三类人证书进京</a></div>
                            <div class='round_div'><a href='CertifManage/CertificateMergeApply.aspx'>三类人C1C2证书合并</a></div>

                            <div class='round_div'><a href='PersonnelFile/WorkerCertiInfoList.aspx'>电子证书下载</a></div>
                            <div class="barlable">
                                从业人员考试业务：
                            </div>
                            <div id="Div6" runat="server" class='round_div'>
                                <a href='EXamManage/ExamSignList.aspx'>考试报名</a>
                            </div>
                            <div id="Div1" runat="server" class='round_div'>
                                <a href='EXamManage/ExamCardManage.aspx'>打印准考证</a>
                            </div>
                            <div id="Div12" runat="server" class='round_div'>
                                <a href='EXamManage/ScoreView.aspx'>查看考试结果</a>
                            </div>
                            <div class='round_div'>
                                <a href='EXamManage/ScoreTZZY.aspx'>特种作业理论考试成绩</a>
                            </div>
                            <div style="padding-left: 40px; float: left; line-height: 30px; margin: 5px 4px 10px 10px;">
                                我的信息：
                            </div>
                            <div class='round_div'>
                                <a href='PersonnelFile/WorkerInfoEdit.aspx'>本系统个人信息维护</a>
                            </div>
                            <div class='round_div'>
                                <a target="_blank" href="https://bjt.beijing.gov.cn/renzheng/p/basicInfo/myInfo.html?pubKey=MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCKIMhtJpI13/g/E/Z7pcQybEMXQPcI0XkydBtSquQeY195S4qw5V9lCLTxThgqlR/p6DMDsDo/3L3ZULRSXv4UplH4+8XTtHM/7SHOC5Yhp3OAtWOcOTmg/GNK6g75vPmW1u8eH2zwp3k8X9D92PyZYDLVx7OjFqGvyA5GqFnQvQIDAQAB">首都之窗个人中心</a>
                            </div>

                            <div style="clear: both"></div>
                        </div>
                    </div>

                    <div id="div_Task" runat="server" visible="false" class="bar" style="width: 98%; margin: 0 auto;">
                        <div class="bartitle">
                            待办业务
                        </div>
                        <div class="barmain">
                            <div class='round_div'>
                                <a href='./County/BusinessList.aspx?type=初始注册'>二建初始注册<sup><asp:Label ID="LabelFirst" runat="server" Text="0"></asp:Label></sup></a>
                            </div>
                            <div class='round_div'>
                                <a href='./County/BusinessList.aspx?type=重新注册'>二建重新注册<sup><asp:Label ID="LabelRenew" runat="server" Text="0"></asp:Label></sup></a>
                            </div>
                            <div class='round_div'>
                                <a href='./County/BusinessList.aspx?type=增项注册'>二建增项注册<sup><asp:Label ID="LabelAddItem" runat="server" Text="0"></asp:Label></sup></a>
                            </div>
                            <div id="divYQZC" runat="server" class='round_div'>
                                <a href='./County/BusinessList.aspx?type=延期注册'>二建延续注册<sup><asp:Label ID="LabelContinue" runat="server" Text="0"></asp:Label></sup></a>
                            </div>
                            <div id="divZXZC" runat="server" class='round_div'>
                                <a href='./County/BusinessList.aspx?type=注销'>二建注销注册<sup><asp:Label ID="LabelCancel" runat="server" Text="0"></asp:Label></sup></a>
                            </div>
                            <div id="GRXX" runat="server" class='round_div'>
                                <a href='./County/BusinessList.aspx?type=个人信息变更'>二建个人信息变更<sup><asp:Label ID="LabelChangeGR" runat="server" Text="0"></asp:Label></sup></a>
                            </div>
                            <div id="QYBG" runat="server" class='round_div'>
                                <a href='./County/BusinessList.aspx?type=执业企业变更'>二建执业企业变更<sup><asp:Label ID="LabelChangeZY" runat="server" Text="0"></asp:Label></sup></a>
                            </div>
                             <div style="clear: both"></div>

                            <div class='round_div'>
                                <a href='./zjs/zjsBusinessList.aspx?type=初始注册'>二级造价工程师初始注册<sup><asp:Label ID="Labelzjs_First" runat="server" Text="0"></asp:Label></sup></a>
                            </div>                            
                            <div id="div14" runat="server" class='round_div'>
                                <a href='./zjs/zjsBusinessList.aspx?type=延续注册'>二级造价工程延续注册<sup><asp:Label ID="Labelzjs_Continue" runat="server" Text="0"></asp:Label></sup></a>
                            </div>
                            <div id="div15" runat="server" class='round_div'>
                                <a href='./zjs/zjsBusinessList.aspx?type=注销'>二级造价工程注销注册<sup><asp:Label ID="Labelzjs_Cancel" runat="server" Text="0"></asp:Label></sup></a>
                            </div>
                            <div id="Div16" runat="server" class='round_div'>
                                <a href='./zjs/zjsBusinessList.aspx?type=个人信息变更'>二级造价工程个人信息变更<sup><asp:Label ID="Labelzjs_ChangeGR" runat="server" Text="0"></asp:Label></sup></a>
                            </div>
                            <div id="Div17" runat="server" class='round_div'>
                                <a href='./zjs/zjsBusinessList.aspx?type=执业企业变更'>二级造价工程执业企业变更<sup><asp:Label ID="Labelzjs_ChangeZY" runat="server" Text="0"></asp:Label></sup></a>
                            </div>
                             <div style="clear: both"></div>


                            <div id="KSBM" runat="server" class='round_div'>
                                <a href='EXamManage/CheckSignUnit.aspx'>从业人员考试报名<sup><asp:Label ID="LabelCYExamSignup" runat="server" Text="0"></asp:Label></sup></a>
                            </div>
                            <div id="ABZF" runat="server" class='round_div'>
                                <a href='./CertifManage/CertifMoreApplyList.aspx'>A本增发<sup><asp:Label ID="LabelCYAZF" runat="server" Text="0"></asp:Label></sup></a>
                            </div>
                            <div id="DivC1C2" runat="server" class='round_div'>
                                <a href='./CertifManage/CertificateMergeList.aspx'>C1、C2合并<sup><asp:Label ID="LabelC1C2" runat="server" Text="0"></asp:Label></sup></a>
                            </div>
                            <div id="ZSJJ" runat="server" class='round_div'>
                                <a href='./CertifEnter/CertifEnterApplyList.aspx'>三类人证书进京<sup><asp:Label ID="LabelCYEnter" runat="server" Text="0"></asp:Label></sup></a>
                            </div>
                            <div id="BGSQ" runat="server" class='round_div' style="width: 280px">
                                <a href='./CertifManage/CertifChangeCheckUnit.aspx'>从业人员证书变更、注销、离京<sup><asp:Label ID="LabelCYChage" runat="server" Text="0"></asp:Label></sup></a>
                            </div>
                            <div id="XXSQ" runat="server" class='round_div'>
                                <a href='./RenewCertifates/CertifCheckUnit.aspx'>从业人员证书续期<sup><asp:Label ID="LabelCYContinue" runat="server" Text="0"></asp:Label></sup></a>
                            </div>
                            <div style="clear: both"></div>
                        </div>
                        <br />
                    </div>
                   
                    
                     <div class="bar" style="width: 98%; margin: 0 auto;" id="div_YuJing" runat="server">
                        <div class="bartitle">
                            过期预警：<span id="p_endWarn" runat="server" style="color:red;font-weight:normal" class="blinking-text"></span>       
                        </div>
                        <div class="barmain">    
                                            
                            <div id="div_Warnej" runat="server" class='round_div news' onclick='javascript:SetIfrmSrc("./CertifManage/EndWarn.aspx?o=ej")' visible="false">
                                二级建造师<sup><asp:Label ID="LabelWarnEJend" runat="server" Text="0"></asp:Label></sup>
                            </div>
                            <div id="div_Warnez" runat="server" class='round_div news' onclick='javascript:SetIfrmSrc("./CertifManage/EndWarn.aspx?o=ez")' visible="false">
                                二级造价工程师<sup><asp:Label ID="LabelWarnEZend" runat="server" Text="0"></asp:Label></sup>
                            </div>
                            <div id="div_Warnslr" runat="server" class='round_div news' onclick='javascript:SetIfrmSrc("./CertifManage/EndWarn.aspx?o=slr")' visible="false">
                                安全生产三类人员<sup><asp:Label ID="LabelWarnSLRend" runat="server" Text="0"></asp:Label></sup>
                            </div>
                            <div id="div_Warntzzy" runat="server" class='round_div news' onclick='javascript:SetIfrmSrc("./CertifManage/EndWarn.aspx?o=tzzy")' visible="false">
                                特种作业<sup><asp:Label ID="LabelWarnTZZYend" runat="server" Text="0"></asp:Label></sup>
                            </div>
                            <div style="clear:both"></div>
                            <telerik:RadGrid ID="RadGridQY" runat="server" Visible="true"
                                GridLines="None" AllowPaging="false" AllowSorting="false" AutoGenerateColumns="False"
                                SortingSettings-SortToolTip="单击进行排序" Width="100%" EnableAjaxSkinRendering="false" Skin="Blue"
                                EnableEmbeddedSkins="false" PagerStyle-AlwaysVisible="true" PageSize="5">
                                <ClientSettings EnableRowHoverStyle="true">
                                </ClientSettings>
                                <HeaderContextMenu EnableEmbeddedSkins="False">
                                </HeaderContextMenu>
                                <MasterTableView NoMasterRecordsText=" 没有可显示的记录" CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Left">
                                    <Columns>                                   
                                        <telerik:GridBoundColumn UniqueName="PostTypeName" DataField="PostTypeName" HeaderText="证书类型">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="PSN_RegisterNO" DataField="PSN_RegisterNO" HeaderText="证书编号">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="PRO_Profession" DataField="PRO_Profession" HeaderText="专业">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="PRO_ValidityEnd" DataField="PRO_ValidityEnd" HeaderText="有效期截止日期" HtmlEncode="false" DataFormatString="{0:yyyy.MM.dd}">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                    <HeaderStyle Font-Bold="True" />
                                  <%--  <PagerTemplate>
                                        <uc1:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                                    </PagerTemplate>--%>
                                </MasterTableView>
                                <FilterMenu EnableEmbeddedSkins="False">
                                </FilterMenu>
                                <SortingSettings SortToolTip="单击进行排序"></SortingSettings>
                            </telerik:RadGrid>
                          <%--  <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="Model.ApplyMDL"
                                SelectMethod="GetListOverdueNotice" TypeName="DataAccess.COC_TOW_Person_BaseInfoDAL"
                                SelectCountMethod="SelectCounttOverdueNotice" EnablePaging="true"
                                MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex" SortParameterName="orderBy">
                                <SelectParameters>
                                    <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                                        DefaultValue="" ConvertEmptyStringToNull="false" />
                                </SelectParameters>
                            </asp:ObjectDataSource>--%>
                        </div>
                    </div>
                    <br />
                     <div id="div_help" style="width: 98%; vertical-align: top; margin: 0 auto 12px auto; padding: 0 0;">
                        <div id="div_exampass" runat="server" style="text-align: left;"  class="blinking-text2"></div>
                        <div style="width: 31%; float: left; height: 250px; border: 1px solid #f3f3f3; background-color: #f7faf7; border-radius: 12px 12px; margin: 0 auto 0 0px; line-height: 200%; padding: 0px 0px">
                            <p style="font-size: 18px; font-weight: bold;">浏览器建议使用:</p>
                            <div style="font-size: 16px; padding: 0 20px;">
                                <p align="left">
                                    <a href="https://browser.360.cn" target="_blank">>>360安全浏览器</a>&nbsp;&nbsp;&nbsp;&nbsp;
                                <a href="https://ie.sogou.com" target="_blank">>>搜狗浏览器</a>&nbsp;&nbsp;&nbsp;&nbsp;
                                <a href="https://browser.qq.com" target="_blank">>>QQ浏览器</a>
                                </p>
                                <p align="left"><a href="https://www.microsoft.com/zh-cn/download/internet-explorer.aspx" target="_blank">>>微软IE(IE10以上)浏览器</a></p>
                                提示：360、搜狗、QQ等都存在极速和兼容两种模式浏览，推荐使用极速模式。 
                                <a href="./Template/360浏览器设置极速模式.docx" target="_blank">>>浏览器极速模式设置教程</a>
                            </div>
                        </div>
                        <div style="width: 31%; height: 250px; font-size: 16px; float: left; border: 1px solid #f3f3f3; background-color: #f7faf7; border-radius: 12px 12px; margin: 0 1%; line-height: 180%; padding: 0px 0px">
                            <p style="font-size: 18px; font-weight: bold;">技术答疑时间:</p>
                            <div style="font-size: 16px; padding: 0 20px; margin: 0 0; line-height: 200%;">
                                <span>工作日:上午：09:00-12:00&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;下午：14:00-17:30</span>
                            </div>
                            <div style="font-size: 18px; font-weight: bold;">技术咨询电话:</div>
                            <div style="font-size: 16px; padding: 0 20px; margin: 0 0; line-height: 200%;">
                                010-55597717 、 13439012920
                            </div>
                            <br />
                            <div style="font-size: 18px; font-weight: bold;">审批咨询电话: <span style="color: red;"><a href="./Template/各区县对外办公电话.htm?v=1.0" target="_blank" style="text-decoration: underline;">&nbsp;&nbsp;市、区住建委对外办公电话&nbsp;&nbsp;</a></span></div>
                        </div>
                        <div style="width: 35%; font-size: 16px; min-height: 238px; float: left; border: 1px solid #f3f3f3; background-color: #f7faf7; border-radius: 12px 12px; margin: 0 0 0 auto; line-height: 180%; padding-top: 12px">
                            <p style="margin: 5px 0px; font-size: 18px; font-weight: bold;">操作须知:<span style="color: red; font-size: 14px">(申办业务前，请详细阅读)</span></p>
                            <p align="left" style="margin: 6px 25px;">
                                <a href="./Template/人员版使用手册.docx">>>个人使用手册</a>&nbsp;&nbsp;&nbsp;
                            <a href="./Template/企业版使用手册.docx">>>企业使用手册</a> &nbsp;&nbsp;&nbsp;
                            <a href="./Template/常见问题及处理方法最终版.docx">>>常见问题处理方法</a>
                            </p>
                            <p style="margin: 6px 0px; font-size: 18px; font-weight: bold;">关注微信公众号：<span style="color: #0000ff">安居北京</span></p>
                            <p style="margin: 0px; padding: 0px; text-align: center">
                                <img alt="二维码" src="../Images/1616028088.jpg" width="115px" height="115px" />
                            </p>
                            <div style="clear: both"></div>
                        </div>
                        <div style="clear: both"></div>
                    </div>
                    <br />
                    <div class="bar" style="width: 98%; margin: 0 auto">
                        <div class="bartitle">
                            信息通知
                        </div>
                        <div class="barmain">
                            <telerik:RadGrid ID="RadGridZCTZ" runat="server"
                                GridLines="None" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                                SortingSettings-SortToolTip="单击进行排序" Width="100%" EnableAjaxSkinRendering="false" Skin="Blue"
                                OnNeedDataSource="RadGridZCTZ_NeedDataSource"
                                EnableEmbeddedSkins="false" PagerStyle-AlwaysVisible="true" PageSize="20">
                                <ClientSettings EnableRowHoverStyle="true">
                                </ClientSettings>
                                <HeaderContextMenu EnableEmbeddedSkins="False">
                                </HeaderContextMenu>
                                <MasterTableView NoMasterRecordsText=" 没有可显示的记录" CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Left">
                                    <Columns>
                                        <telerik:GridTemplateColumn UniqueName="RowNum" HeaderText="序号">
                                            <ItemTemplate>
                                                <div class="num"><%#Eval("RowNum")%></div>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" Width="25" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Left" Width="25" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn UniqueName="Title" HeaderText="标 题">
                                            <ItemTemplate>
                                                <a target="_blank" href="./Register/NewsView.aspx?o=<%# Utility.Cryptography.Encrypt(Eval("ID").ToString())%>" class="news"><%#Eval("Title")%></a>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn UniqueName="GetDateTime" DataField="GetDateTime" HeaderText="发布日期" HtmlEncode="false" DataFormatString="{0:yyyy.MM.dd}">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn UniqueName="Fj" HeaderText="附件">
                                            <ItemTemplate>
                                                <a target="_blank" href='<%# Eval("FileUrl").ToString()==""?"#":ZYRYJG.UIHelp.ShowFile(Eval("FileUrl").ToString()) %>'><%# Eval("FileUrl").ToString()==""?"":"下载" %></a>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                    <HeaderStyle Font-Bold="True" />

                                    <PagerTemplate>
                                        <uc1:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                                    </PagerTemplate>
                                </MasterTableView>
                                <FilterMenu EnableEmbeddedSkins="False">
                                </FilterMenu>
                                <SortingSettings SortToolTip="单击进行排序"></SortingSettings>
                            </telerik:RadGrid>
                            <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" DataObjectTypeName="Model.PolicyNewMDL"
                                SelectMethod="GetList" TypeName="DataAccess.PolicyNewsDAL"
                                SelectCountMethod="SelectCount" EnablePaging="true"
                                MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex" SortParameterName="orderBy">
                                <SelectParameters>
                                    <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                                        DefaultValue="" ConvertEmptyStringToNull="false" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </div>
                    </div>
                    <br />
                    <div id="div_question" runat="server" visible="false" style="width: 98%; margin: 0 auto;">
                        <div class="bartitle">
                            常见问题
                        </div>
                        <div class="barmain RadGrid RadGrid_Blue">
                            <table id="guize" class="rgMasterTable" style="width: 100%; border-collapse: collapse;">
                                <tr class="rgHeader">
                                    <th>序号</th>
                                    <th>常见问题</th>
                                    <th>原因及解决方法</th>
                                </tr>
                                <tr class="rgRow">
                                    <td>1</td>
                                    <td>社保比对不成功</td>
                                    <td>(1)默认是按十八位的统一社会信用代码查询，如果您社保权益表上的企业代码是八位的组织机构代码，请联系公司到社保局修改企业关联代码。<br />
                                        &nbsp;&nbsp;(社保局指示的查询规则：<br />
                                        &nbsp;&nbsp;&nbsp;&nbsp;(a)本月交社保(并且已扣费)，下个月我们才能得到数据；<br />
                                        &nbsp;&nbsp;&nbsp;&nbsp;(b)补交了上一个月社保，但是在本月扣费，下个月我们才能得到数据；<br />
                                        &nbsp;&nbsp;&nbsp;&nbsp;(c)你在社保局能查到，但是在本系统查不到，是因为数据的推送需要特定的时间)<br />
                                        (2)社保比对，从提交业务到消旗需要一到两个工作日的时间</td>
                                </tr>
                                <tr class="rgRow">
                                    <td>2</td>
                                    <td>点击按钮(或链接)没有反应</td>
                                    <td>浏览器兼容问题 (360浏览器：选择"极速模式"(地址栏右侧有个图标,改成"闪电"样式)；IE浏览器最新版本 等)</td>
                                </tr>
                                <tr class="rgRow">
                                    <td>3</td>
                                    <td>显示有在办业务</td>
                                    <td>建造师有未办结的业务，处理完该业务即可重新发起申报(根据具体情况判断，是否需要删除该业务信息，而不是撤销)</td>
                                </tr>
                                <tr class="rgRow">
                                    <td>4</td>
                                    <td>重新(或注销)注册找不到建造师信息</td>
                                    <td>(1)有未办结的业务，根据具体情况判断是否需要删除<br />
                                        (2)个人信息中的企业组织机构代码错了</td>
                                </tr>
                                <tr class="rgRow">
                                    <td>5</td>
                                    <td>申报业务时，企业信息不正确</td>
                                    <td>这条申报记录保存的可能是企业变更信息之前的信息，须删除该业务信息，重新发起申报，以便获取最新数据</td>
                                </tr>
                            </table>
                        </div>
                        <br />
                    </div>
                   
                    <br />
                    <div class="bar" style="width: 98%; margin: 0 auto;" id="div_BanJie" runat="server">
                        <div class="bartitle">
                            业务办结通知
                        </div>
                        <div class="barmain">
                            <telerik:RadGrid ID="RadGridZSTZ" runat="server"
                                GridLines="None" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" Skin="Blue"
                                SortingSettings-SortToolTip="单击进行排序" Width="100%" EnableAjaxSkinRendering="false"
                                EnableEmbeddedSkins="false" PagerStyle-AlwaysVisible="true" BorderStyle="None" PageSize="5">
                                <ClientSettings EnableRowHoverStyle="true">
                                </ClientSettings>
                                <HeaderContextMenu EnableEmbeddedSkins="False">
                                </HeaderContextMenu>
                                <MasterTableView NoMasterRecordsText=" 没有可显示的记录" CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Left">
                                    <Columns>
                                        <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="PSN_Name" DataField="PSN_Name" HeaderText="姓名">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="PSN_RegisterNO" DataField="PSN_RegisterNO" HeaderText="注册号">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="ApplyType" DataField="ApplyType" HeaderText="业务类型">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="GetDateTime" DataField="GetDateTime" HeaderText="通知时间" HtmlEncode="false" DataFormatString="{0:yyyy.MM.dd}">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                    <HeaderStyle Font-Bold="True" />
                                    <PagerTemplate>
                                        <uc1:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                                    </PagerTemplate>
                                </MasterTableView>
                                <FilterMenu EnableEmbeddedSkins="False">
                                </FilterMenu>
                                <SortingSettings SortToolTip="单击进行排序"></SortingSettings>
                            </telerik:RadGrid>
                            <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" DataObjectTypeName="Model.ApplyNewsMDL"
                                SelectMethod="GetList" TypeName="DataAccess.ApplyNewsDAL"
                                SelectCountMethod="SelectCount" EnablePaging="true"
                                MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex" SortParameterName="orderBy">
                                <SelectParameters>
                                    <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                                        DefaultValue="" ConvertEmptyStringToNull="false" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <uc2:IframeView ID="IframeView" runat="server" />
        <div id="winpop">
            <div class="title">您有新的消息<span class="close" onclick="tips_pop()"><img src="images/close.gif" /></span></div>
            <div id="con">你有未读信息(1)</div>
        </div>
        <asp:HiddenField ID="HiddenField1" runat="server" />

        <%--考试确认--%>
        <%--<div id="DivExamConfirm" runat="server" style="line-height: 30px; width: 580px; display: none; margin-top: 30px; position: absolute; top: 100px; left: 200px; background-color: #dedede; padding: 20px 50px 50px 50px; border-left: 4px solid #eee; border-top: 4px solid #eee; border-right: 4px solid #999; border-bottom: 4px solid #999; color: #000">
            <p style="font-size: 30px; font-weight: bold; text-align: center;">考试确认</p>
            <p style="text-indent: 32px" id="p_ExamConvfirmDesc" runat="server">
            </p>
            <p style="text-align: center;">
                <asp:Button ID="ButtonExamYes" runat="server" Text="是" CssClass="bt_large btn_no" OnClick="ButtonExamYes_Click" CausesValidation="false" Enabled="false" />
                &nbsp;&nbsp; 
                <asp:Button ID="ButtonExamNo" runat="server" Text="否" CssClass="bt_large btn_no" OnClick="ButtonExamNo_Click" CausesValidation="false"  Enabled="false" />
                <span style="padding-left:20px;font-size:30px;color:red;font-weight:bold" id="spanCount">15</span>
            </p>
            
        </div>--%>

        <div id="DivExamConfirm" runat="server" style="line-height: 30px; width: 580px; display: none; margin-top: 30px; position: absolute; top: 100px; left: 200px; background-color: #dedede; padding: 20px 50px 50px 50px; border-left: 4px solid #eee; border-top: 4px solid #eee; border-right: 4px solid #999; border-bottom: 4px solid #999; color: #000">
            <p style="font-size: 30px; font-weight: bold; text-align: center;">系统提示</p>
            <p style="text-indent: 32px" id="p_ExamConvfirmDesc" runat="server">
            </p>
            <p style="text-align: center;">
                <asp:Button ID="ButtonExamYes" runat="server" Text="反馈" CssClass="bt_large btn_no" OnClick="ButtonExamYes_Click" CausesValidation="false" Enabled="false" />
                &nbsp;&nbsp; 
                <asp:Button ID="ButtonExamNo" runat="server" Text="关闭" CssClass="bt_large btn_no" OnClick="ButtonExamNo_Click" CausesValidation="false"  Enabled="false" />
                <span style="padding-left:20px;font-size:30px;color:red;font-weight:bold" id="spanCount">10</span>
            </p>
            
        </div>
     
       <%-- <div id="floadAD" class="floadAd">
            <a class="close" href="javascript:void();" style="color: #000000; text-align: right; float: right; clear: both; padding-bottom: 4px; font-size: 28px">×</a>
            <p class="item" style="text-align:center">
               <img src="Images/dc2023.jpg" alt="问卷调查" height="600" />
            </p>
        </div>--%>
         
        <%--临时通知飘窗--%>
       <%-- <div id="floadAD" class="floadAd">
            <a class="close" href="javascript:void();" style="color: #000000; text-align: right; float: right; clear: both; padding-bottom: 4px; font-size: 28px">×</a>
            <div class="item" style="line-height: 150%">
                <b>考试通知：</b>
                <p style="text-indent: 40px; line-height: 150%">
                    2023年10月份安全生产管理人员网络在线考核：<br />
	考生报名：2023年9月18日至9月20日<br />
	企业确认：2023年9月19日至9月22日<br />
	准考证下载：2023年10月18日至10月26日<br />
	模拟测试：2023年10月18日至10月25日（每日9:00至17:00，技术支持电话：4008703877）<br />
	正式考核：2023年10月27日<br />
	重要提示：逾期未按要求参加模拟测试或不满足网络在线考核相关要求的，视为考生自动放弃报考资格。
                </p>
            </div>
        </div>--%>
        <script src="./Scripts/FloatMessage.js" type="text/javascript"></script>
        <script src="Scripts/main.js?v=1.019" type="text/javascript"></script>
        <%--<script type="text/javascript" language="javascript"> FloatAd("#floadAD");</script>--%>

        <script type="text/javascript" language="javascript">
//            var tmpHtml = '<div style="width:640px;padding-left:5px;margin-right:5px;">\
//            <p  style="width:380px;margin-left:140px;font-weight:bold">关于暂停组织2022年第四季度北京市建筑施工企业<br />\
//<span style="margin-left:38px;">“安管人员”安全生产考核工作的通知</span></p>\
//            <p style="padding-left:15px;">\
//                各位考生：<br />\
//　　近期，本市疫情上升势头明显，叠加京外输入及冬季流感等其他呼吸道传染病传播风险，疫情防控压力进一步增加，为切实保障考生、考试工作人员的身体健康和生命安全，经研究，决定暂停组织2022年第四季度我市建筑施工企业“安管人员”安全生产考核工作。\
//　　<br />　　感谢您对我们工作的理解与支持，如需帮助请联系010-55598091（工作日9:00-12:00；14:00-17:00）。\
//            </p>\
//<div style="width:100%;height:20px;"><p style="float:right;">2022年11月24日</p></div>\
//<p style="text-align: center;">\
//<button ID="ButtonExamnan" style="display:none;width: 135px;height:30px;background-color:red;color:white;border: 0px;" onclick="closenan()">已知晓‥关闭页面</button>\
//            <span style="padding-left:20px;font-size:30px;color:red;font-weight:bold" id="spanCountnan">15</span></p>\
//        </div>';

//            function show15() {
//                var myVar = setInterval(function () {
//                    $("#ButtonExamnan").hide();
//                    var num = $("#spanCountnan").text();
//                    num--;
//                    $("#spanCountnan").text(num);
//                    if (num == 0) {
//                        $("#spanCountnan").text("");
//                        clearInterval(myVar);
//                        $("#ButtonExamnan").show();                        
//                    }
//                }, 1000);
//            }
    
            function show15() {
                var myVar = setInterval(function(){ 
                    var num = $("#spanCount").text();
                    num--;
                    $("#spanCount").text(num);
                    if(num==0){
                        $("#spanCount").text("");
                        clearInterval(myVar);    
                        $("#<%=ButtonExamYes.ClientID%>").removeClass("btn_no");
                        $("#<%=ButtonExamNo.ClientID%>").removeClass("btn_no");
                        $("#<%=ButtonExamYes.ClientID%>").removeAttr("disabled");
                        $("#<%=ButtonExamNo.ClientID%>").removeAttr("disabled");                        
                    }
                }, 1000);
            }

        </script>
    </form>
</body>
</html>
