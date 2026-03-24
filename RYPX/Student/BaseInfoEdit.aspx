<%@ Page Title="" Language="C#" MasterPageFile="~/RadControls.master" AutoEventWireup="true" CodeFile="BaseInfoEdit.aspx.cs" Inherits="Student_BaseInfoEdit" %>

<%@ Register Src="~/IframeView.ascx" TagPrefix="uc1" TagName="IframeView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadWindowManager ID="RadWindowManager1" ShowContentDuringLoad="false" VisibleStatusbar="false"
        ReloadOnShow="true" runat="server" Skin="Windows7" EnableShadow="true">
        <AlertTemplate>
            <div class="alertText">
                {1}
            </div>
            <div class="confrimButton">
                <input onclick="$find('{0}').close();" class="button" id="ButtonOK" type="button"
                    value="确 定" />
            </div>
        </AlertTemplate>
        <ConfirmTemplate>
            <div class="confrimText">
                {1}
            </div>
            <div class="confrimButton">
                <input onclick="$find('{0}').close(true);" class="button" id="ButtonOK" type="button"
                    value="确 定" />&nbsp;&nbsp;
                <input onclick="$find('{0}').close(false);" class="button" id="ButtonCancel" type="button"
                    value="取 消" />
            </div>
        </ConfirmTemplate>
    </telerik:RadWindowManager>
    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
    </telerik:RadScriptBlock>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true"
        Skin="Windows7" />
    <div style="padding: 0 8px;">
        <div class="div_fun">
            持证情况
        </div>
        <div class="content">
           <%-- <div class="grid_label" style="text-align: left;">
                个人基本信息
            </div>
            <table id="TableEdit" runat="server" width="95%" border="0" cellpadding="6" cellspacing="1" visible="false"
                class="table" align="center">
                <tr class="GridLightBK">
                    <td width="20%" nowrap="nowrap" align="right">姓 名：</td>
                    <td>
                        <asp:Label ID="LabelWorkerName" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr class="GridLightBK">
                    <td align="right">证件号码：</td>
                    <td>
                        <asp:Label ID="LabelWorkerCertificateCode" runat="server" Text=""></asp:Label>
                    </td>
                </tr>              
            </table>--%>
            <div class="grid_label" style="text-align: left;">
                证书列表
            </div>
            <div style="width: 95%; margin: 5px auto;">
                <telerik:RadGrid ID="RadGridCertificate" runat="server" AutoGenerateColumns="False" GridLines="None" AllowPaging="false"
                    Skin="Blue" EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false" Width="100%">
                    <ClientSettings EnableRowHoverStyle="true">
                    </ClientSettings>
                    <MasterTableView CommandItemDisplay="None" DataKeyNames="CertificateID" NoMasterRecordsText="　没有可显示的记录">
                        <Columns>
                            <telerik:GridBoundColumn UniqueName="PostTypeName" DataField="PostTypeName" HeaderText="证书类别">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="PostName" DataField="PostName" HeaderText="证书专业">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="CertificateCode" DataField="CertificateCode" HeaderText="证书编号">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="ValidEndDate" HeaderText="有效期至">
                                <ItemTemplate>
                                    <%# Convert.ToDateTime(Eval("ValidEndDate")).ToString("yyyy.MM.dd")=="2050.01.01"?"当前有效证书":Convert.ToDateTime(Eval("ValidEndDate")).ToString("yyyy.MM.dd")%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="Status" HeaderText="证书状态">
                                <ItemTemplate>
                                    <img alt="" class="img16" src='<%# (Eval("POSTTYPENAME").ToString() != "一级建造师" && Eval("POSTTYPENAME").ToString() != "一级临时建造师"&&(Convert.ToDateTime(Eval("ValidEndDate")).AddDays(1) < DateTime.Now || Eval("Status").ToString()=="注销" || Eval("Status").ToString()=="离京变更" ))?"../img/wx.png":"../img/yx.png"%>' />
                                    <%# (Eval("POSTTYPENAME").ToString() != "一级建造师" && Eval("POSTTYPENAME").ToString() != "一级临时建造师"&&(Convert.ToDateTime(Eval("ValidEndDate")).AddDays(1) < DateTime.Now || Eval("Status").ToString()=="注销" || Eval("Status").ToString()=="离京变更" ))?"无效":"有效"%>
                                    <%# (Eval("POSTTYPENAME").ToString() != "一级建造师" && Eval("POSTTYPENAME").ToString() != "一级临时建造师"&&Convert.ToDateTime(Eval("ValidEndDate")).AddDays(1) < DateTime.Now)?"(过期)": ""%>
                                    <%# Eval("Status").ToString()=="注销" ?"(注销)":""%>
                                    <%# Eval("Status").ToString()=="离京变更"?"(离京)":""%>
                                    <%# Eval("Remark") != null && Eval("Remark").ToString().Contains("超龄")==true?"(超龄)":""%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridTemplateColumn>
                             <telerik:GridTemplateColumn UniqueName="TrainUrl" HeaderText="相关公益培训课程">
                                <ItemTemplate>
                                    <%# Eval("PackageID") == DBNull.Value?"暂无本专业课程":string.Format("<a href=\"WebClass.aspx?o={0}\">进入课程</a>",Utility.Cryptography.Encrypt(string.Format("{0}|{1}|{2}",Eval("POSTTYPENAME"), Eval("PostName"),DateTime.Now)))%>
                                    
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridTemplateColumn>
                        </Columns>
                        <HeaderStyle Font-Bold="true" />
                    </MasterTableView>
                </telerik:RadGrid>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="DataAccess.CertificateDAL"
                    DataObjectTypeName="Model.CertificateOB" SelectMethod="GetList" EnablePaging="true"
                    SelectCountMethod="SelectCount" MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex"
                    SortParameterName="orderBy">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                            DefaultValue="" ConvertEmptyStringToNull="false" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </div>
            <div style="line-height:300%;color:red;font-weight:bold;text-align:center" id="divTip" runat="server" visible="false">目前处于人员续期业务办理高峰期，如遇无法进入课程情况，系当前在线人数较多，为保证学习质量,请您错峰登录,给您带来的不便,敬请谅解。</div>
             <div class="grid_label" style="text-align: left;">
                继续教育学习要求
            </div>
            <div  style="width: 99%; margin: 5px auto;">
                <div class="xqyq">
                    <div class="hd" style="background-color:#F54B5D">二级造价工程师</div>
                    <div class="nr">
                        <p>一、必修课继续教育</p>
                        <p class="sp">
1、在注册有效期内，应完成60学时的必修课继续教育。<b class="red">注册两个专业的继续教育学时可重复计算。</b> <br />
2、重新申请初始注册的，应完成60学时的必修课继续教育；自职业资格证书批准之日起至重新申请初始注册之日止不足4年的，应完成每满1个年度不少于15学时的必修课继续教育。<br />
3、自职业资格证书批准之日起18个月后，首次申请初始注册的，应完成自申请注册之日起算，近1年不少于15学时的必修课继续教育。<br />
4、<b class="red">超出规定要求的学习时长，不计入有效学时</b>。<br />
5、申请人应在“北京市建设行业从业人员公益培训平台”<b class="red">自行完成</b>上述必修课继续教育。
                            </p>
  <p>二、选修课继续教育  </p>
                        <p class="sp">
1、选修课继续教育包括：申请人参加行业主管部门、协会等单位组织的面授或网络培训、学术会议、学术报告、专业论坛等活动；参加企业自行组织的造价工程师继续教育培训。<br />
2、<b class="red">选修课继续教育学时要求与必修课一致</b>。</p>

                    </div>
                </div> 
                <div class="xqyq">
                     <div class="hd" style="background-color:#3DB87C">安管人员</div>
                    <div class="nr">
                        <p>一、企业年度安全生产教育和培训（企业自行组织完成）</p>
<p class="sp">各建筑施工企业应对本企业安管人员开展<b class="red">每年不少于20学时</b>的企业年度安全生产教育和培训。</p>
<p>二、证书延续继续教育（各区住房城乡（市）建设委等初审单位组织完成）</p>
<p class="sp">在证书有效期内，申请人应完成各初审单位组织的<b class="red">不少于16学时</b>的证书延续继续教育学习。</p>
<p>三、证书延续专项继续教育（申请人在“北京市建设行业从业人员公益培训平台”自行完成）</p>
<p class="sp">1.在证书有效期内，申请人应完成<b class="red">不少于8学时</b>的证书延续专项继续教育学习，<b class="red">超出规定要求的学习时长，不计入有效学时</b>。<br />
2.在<b class="red">同一年度申请多本证书延续</b>的，证书延续专项继续教育<b class="red">学时可重复计算</b>。
</p>
                    </div>
                </div>
                 <div class="xqyq">
                      <div class="hd"  style="background-color:#67A6FF">特种作业人员</div>
                    <div class="nr"> <p>一、年度安全生产教育培训（企业自行组织完成）</p>
<p class="sp">各建筑施工企业应组织或者委托具备安全技术培训条件的机构对本企业建筑施工特种作业人员开展每<b class="red">年不少于24学时</b>的年度安全生产教育培训。</p>
 <p>二、延期复核继续教育（申请人在“北京市建设行业从业人员公益培训平台”自行完成）</p>
 <p class="sp">1.在证书有效期内，申请人应完成<b class="red">每年不少于8学时</b>的延期复核继续教育学习，<b class="red">超出规定要求的学习时长，不计入有效学时</b>。<br />
2.在<b class="red">同一年度申请多本证书延期</b>的，延期复核继续教育<b class="red">学时可重复计算</b>。
     </p>
                    </div>
                 </div>
                <div style="clear:both"></div>
            </div>
        </div>
    </div>
    <style type="text/css">
        .xqyq{
            float:left;
            max-width:31%;
            margin:8px 12px;
            box-shadow: 0 0 10px 2px rgb(0 0 0 / 8%);
            border:1px solid #f0f0f0;
            border-radius: 12px;
            overflow: hidden;
            background: #fff;
            padding:0;
            height:520px;
            font-size:14px;
            background-color: #f2f2f2;
        }
        .xqyq .hd{
            line-height:280%;
            font-size:16px;
            font-weight:bolder;
             text-align:center;
             background-color:#8D0706;
             color:#f0f0f0;
              border-radius: 12px 12px 0 0;
            overflow: hidden;
        }
        .xqyq .nr{
            line-height:160%;
            padding:8px 24px;
        }
        .red{color:red;}
        .sp{
            margin-left:16px;
         }
    </style>
    <script src="../layer/layer.js" type="text/javascript"></script>
    <uc1:IframeView ID="IframeView" runat="server" />
</asp:Content>

