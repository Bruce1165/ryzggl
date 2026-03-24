<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/RadControls.Master"
    CodeBehind="EJuse.aspx.cs" Inherits="ZYRYJG.PersonnelFile.EJuse" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc4" %>
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
        .rgCommandRow a{color:blue!important;
                        background:url(../images/new.gif) no-repeat right center;
                        padding-right:30px;
        }
    </style>
    <div class="div_out">

        <div id="div_top" class="dqts">
            <div id="divRoad" runat="server" style="float: left;">
                当前位置 &gt;&gt; 二级建造师电子证书使用件下载
            </div>
        </div>
        <div class="content" style="min-height: 400px">
            <div id="DivContent">
                <div class="DivContent" style="text-align: left">
                    1、持证人需在个人信息维护栏目按要求上传本人手写签字图像后方可获取新版电子证照。<br />
			2、持证人获取新版电子证照时，应确认电子证照的使用有效期，使用有效期应在注册专业有效期范围内。超出使用有效期的新版电子证照无效，需重新下载新版电子证照，并再次确认使用有效期。<br />
            3、申报事项公告通过后，申请人可在 电子证书 下载栏目中生成该电子证书使用有效期范围，<span style="color:red">公告后24小时内未自行填写，系统将会自动生成最大时间范围的使用有效期。</span><br />
               &nbsp;&nbsp; &nbsp; &nbsp;新电子证书生成后，申请人也可以根据自身需求再次调整证书使用有效期时间范围。<br />
			4、持证人打印新版电子证照后，应在<span style="color:red">个人签名处手写本人签名并签署日期</span>，未手写签名或与签名图像笔迹不一致的，该电子证照无效。<br />
			5、本人手写签名图像模糊不清或发生变更的，可登录北京市住房和城乡建设领域人员资格管理信息系统，通过申请二级建造师个人信息变更重新提交。<br />
			6、持证人应妥善保管本人的北京市统一身份认证平台账号及密码信息，因本人保管不善造成账号及密码信息泄露所产生的一切后果由本人承担。<br />
			7、电子证照申请流程：完善个人签名→点击”添加使用有效期”→“保存”→“12小时后下载

                </div>

                <div class="DivContent">
                    <div id="DivDetail" runat="server" style="padding: 12px; line-height: 200%; font-size: 16px;">
                    </div>
                    <div style="text-align: left; padding: 0 20px; margin: 12px;">
                        <asp:Button ID="ButtonDownload" runat="server" Text="下 载" CssClass="pdfbtn" OnClick="ButtonDownload_Click" Visible="false" />
                    
                        <asp:Button ID="ButtonDownload_OFD" runat="server" Text="下 载" CssClass="ofdbtn" OnClick="ButtonDownload_OFD_Click" Visible="false" style="margin-left:200px" />
                    </div>
                </div>
                <div style="width: 98%; margin: 0 auto;">
                <telerik:RadGrid ID="RadGridUse" runat="server" GridLines="None" 
                    AllowPaging="True" PageSize="5" AllowSorting="True" AutoGenerateColumns="False"
                    SortingSettings-SortToolTip="单击进行排序" Width="100%" Skin="Blue" EnableAjaxSkinRendering="false" OnInsertCommand="RadGridUse_InsertCommand"
                    EnableEmbeddedSkins="false">
                    <MasterTableView  NoMasterRecordsText="　没有可显示的记录" EditMode="PopUp" CommandItemDisplay="Top" DataKeyNames="CertificateCAID">
                        <Columns>
                            <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" Font-Bold="true" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="PSN_RegisterNO" DataField="PSN_RegisterNO" HeaderText="证书注册号">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn UniqueName="cjsj" DataField="cjsj" HeaderText="填写使用范围日期"
                                HtmlEncode="false" DataFormatString="{0:yyyy.MM.dd}">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                              <telerik:GridTemplateColumn UniqueName="Valid" HeaderText="状态">
                                <ItemTemplate>
                                   <%# Eval("Valid").ToString()=="1"&& Convert.ToDateTime(Eval("EndTime")).AddDays(1) >=DateTime.Now?"<span style='color:#1F7246'>有效</span>":"<span style='color:#aaa'>失效</span>"%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" Font-Bold="true" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" Font-Bold="true" />
                            </telerik:GridTemplateColumn>
                        
                            <telerik:GridBoundColumn UniqueName="BeginTime" DataField="BeginTime" HeaderText="使用有效期起始"
                                HtmlEncode="false" DataFormatString="{0:yyyy.MM.dd}">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="EndTime" DataField="EndTime" HeaderText="使用有效期截止"
                                HtmlEncode="false" DataFormatString="{0:yyyy.MM.dd}">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                           
                        </Columns>
                          <CommandItemSettings AddNewRecordText="点我添加使用范围" ShowRefreshButton="false" />
                             
                        <EditFormSettings InsertCaption="添加使用范围" CaptionFormatString="添加使用范围" 
                            EditFormType="Template" PopUpSettings-Width="500px" PopUpSettings-Modal="true">
                            <EditColumn UniqueName="EditCommandColumn1">
                            </EditColumn>
                            <FormTemplate>
                                <table class="bar_cx" style="margin-top: 20px">
                                    <tr>
                                        <td align="right" width="11%" nowrap="nowrap">
                                            <span style="color: Red">*</span>使用有效期日期范围：
                                        </td>
                                        <td align="left">
                                            <telerik:RadDatePicker ID="RadDatePickerBeginTime" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                            Width="46%" />
                        <div class="RadPicker">至</div>
                        <telerik:RadDatePicker ID="RadDatePickerEndTime" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                            Width="46%" />
                                        </td>
                                    </tr>
                                </table>
                                <table style="width: 100%; padding-bottom: 20px;">
                                    <tr>
                                        <td align="center" colspan="2">
                                            <asp:Button ID="Button1" CssClass="button" Text='<%# (Container is GridEditFormInsertItem) ? "保存" : "更新" %>'
                                                runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'></asp:Button>&nbsp;
                                                    <asp:Button ID="Button2" CssClass="button" Text="取消" runat="server" CausesValidation="False"
                                                        CommandName="Cancel"></asp:Button>
                                        </td>
                                    </tr>
                                </table>
                            </FormTemplate>
                         
                        </EditFormSettings>
                        <PagerTemplate>
                            
                            <uc2:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                        </PagerTemplate>
                        <EditFormSettings>
                            <EditColumn UniqueName="EditCommandColumn1">
                            </EditColumn>
                        </EditFormSettings>
                        <PagerStyle AlwaysVisible="true" />
                    </MasterTableView>
                </telerik:RadGrid>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="Model.EJCertUseMDL"
                    DeleteMethod="Delete" InsertMethod="Insert" SelectMethod="GetList" TypeName="DataAccess.EJCertUseDAL"
                    UpdateMethod="Update" SelectCountMethod="SelectCount" EnablePaging="true" MaximumRowsParameterName="maximumRows"
                    StartRowIndexParameterName="startRowIndex" SortParameterName="orderBy">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                            DefaultValue="" ConvertEmptyStringToNull="false" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </div>
                <div id="div_downlog" runat="server" style="text-align: left; padding: 20px; margin: 12px; line-height: 150%; max-height: 100px; overflow-y: auto;"></div>
            </div>
            <div style="width: 95%; margin: 10px auto; text-align: center;">             
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
