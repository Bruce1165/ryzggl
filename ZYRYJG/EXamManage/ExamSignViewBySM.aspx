<%@ Page Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="ExamSignViewBySM.aspx.cs" Inherits="ZYRYJG.EXamManage.ExamSignViewBySM" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc1" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <%--  <telerik:AjaxSetting AjaxControlID="div_content">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="div_content" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>--%>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true"
        Skin="Windows7" />
    <telerik:RadWindowManager ID="RadWindowManager1" ShowContentDuringLoad="false" VisibleStatusbar="false"
        ReloadOnShow="true" runat="server" Skin="Windows7" EnableShadow="true">
    </telerik:RadWindowManager>
    <style>
        .GridLightBK {
            background-color: #FFFFFF;
            font-family: 'Microsoft YaHei',STSong,Arial,SimHei;
            font-size: 14px !important;
            line-height: 150%;
            color: #333;
            text-align: left;
        }
    </style>
    <div class="div_out">
        <div class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt;考务管理 &gt;&gt;报名管理 &gt;&gt; <strong>报名初审</strong>
            </div>
        </div>
        <div class="table_border" style="margin: 12px 12px;">

<%--            <div class="content" id="div_content" runat="server" style="width: 100%;">
                <div class="jbxxbt">
                    报名初审
                </div>
                <div id="divShaoMa" runat="server" style="width: 100%; height: 350px; padding-top: 20px; text-align: left; font-family: 'Microsoft YaHei',STSong,Arial,SimHei;">
                    <div style="font-size: 20px; font-weight: bold; color: #444; float: left; line-height: 32px; width: 200px; text-align: right; vertical-align: middle">请扫码：</div>
                    <input id="RadTextBoxExamSignupID" runat="server" type="text" onkeyup="ShaoQiangSearch(event)" style="width: 400px; height: 30px; line-height: 30px; font-size: 30px;" />
                    <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" Style="display: none" />
                    <div style="width: 95%; margin: 20px auto;">
                        <div style="line-height:200%; text-align:left;">最近审核记录</div>
                        <telerik:RadGrid ID="RadGridChecked" runat="server" Width="100%" AllowSorting="false" AllowPaging="false" PageSize="5"
                            GridLines="None" AutoGenerateColumns="False" Skin="Blue" EnableAjaxSkinRendering="False"  
                            EnableEmbeddedSkins="False">
                            <ClientSettings EnableRowHoverStyle="false">
                                <Selecting AllowRowSelect="false" />
                            </ClientSettings>
                            <HeaderContextMenu EnableEmbeddedSkins="False">
                            </HeaderContextMenu>
                            <MasterTableView 
                                NoMasterRecordsText="　没有可显示的记录">
                                <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                                
                                <Columns>
                                    <telerik:GridBoundColumn UniqueName="TRAINUNITNAME" DataField="TRAINUNITNAME" HeaderText="审核人">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="FIRSTTRIALTIME" DataField="FIRSTTRIALTIME" HeaderText="审核时间"
                                        HtmlEncode="false" DataFormatString="{0:yyyy.MM.dd HH:mm}">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn UniqueName="PostName" HeaderText="申报岗位工种">
                                        <ItemTemplate>
                                            <nobr><%# Eval("PostName")%><%# Eval("PostID").ToString() == "12" && Eval("EXAMPLANNAME").ToString().Contains("暖通") ? "（暖通）":
                                                   Eval("PostID").ToString() == "12" && Eval("EXAMPLANNAME").ToString().Contains("电气") ? "（电气）" : ""%></nobr>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn UniqueName="SignUpCode" DataField="SignUpCode" HeaderText="报名批号">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="WorkerName" DataField="WorkerName" HeaderText="姓名">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="CertificateCode" DataField="CertificateCode"
                                        HeaderText="证件号码">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="UnitName" DataField="UnitName" HeaderText="单位名称">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    
                                </Columns>
                                <HeaderStyle Font-Bold="True" />
                               
                            </MasterTableView>
                            <StatusBarSettings LoadingText="正在读取数据" ReadyText="完成" />
                           
                        </telerik:RadGrid>
                    </div>
                </div>
                <div style="width: 95%; margin: 20px auto; padding: 5px; font-size: 14px; font-family: 'Microsoft YaHei',STSong,Arial,SimHei;" runat="server" id="divExamSignUp" visible="false">
                    <div style="float: right; padding-right: 30px;">
                        报名批号:<asp:Label ID="lblSignUpCode" runat="server" Text=""></asp:Label>
                    </div>
                    <div style="margin-bottom: 5px; text-align: left; padding-left: 40px">
                        报名时间
                        <asp:Label ID="LabelExamSignupDate" runat="server">
                        </asp:Label>
                    </div>
                    <table width="95%" border="0" cellpadding="5" cellspacing="1" class="table2" align="center">

                        <tr class="GridLightBK">
                            <td width="15%" nowrap="nowrap" align="center">姓名
                            </td>
                            <td width="20%">
                                <asp:Label ID="RadTextBoxWorkerName" runat="server">
                                </asp:Label>
                                <asp:HiddenField ID="HiddenFieldPhone" runat="server" />
                                <asp:HiddenField ID="HiddenFieldBirthday" runat="server" />
                            </td>
                            <td align="center" width="8%">性别
                            </td>
                            <td width="7%">
                                <asp:Label ID="RadioButtonMan" runat="server" />
                            </td>
                            <td align="center" width="17%">年龄
                            </td>
                            <td width="18%">
                                <asp:Label ID="RadDatePickerBirthday" runat="server" />
                            </td>
                            <td width="110" rowspan="4" align="center">
                                <img id="ImgCode" runat="server" height="140" width="110" alt="一寸照片" />
                            </td>
                        </tr>
                        <tr class="GridLightBK">

                            <td align="center">身份证号
                            </td>
                            <td colspan="3">
                                <asp:Label ID="RadTextCertificateCode" runat="server">
                                </asp:Label>
                            </td>
                            <td align="center">联系电话
                            </td>
                            <td>
                                <asp:Label ID="RadTextBoxPhone" runat="server">
                                </asp:Label>
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td width="10%" nowrap="nowrap" align="center">申报岗位工种
                            </td>
                            <td colspan="3">
                                <asp:Label ID="RadTextPostID" runat="server" Width="95%">
                                </asp:Label>
                            </td>
                            <td nowrap="nowrap" align="center">文化程度
                            </td>
                            <td>
                                <asp:Label ID="RadTextBoxCulturalLevel" runat="server">
                                </asp:Label>
                            </td>

                        </tr>

                        <tr class="GridLightBK">
                            <td align="center">技术职称或技术等级
                            </td>
                            <td colspan="3">
                                <asp:Label ID="RadTextBoxSKILLLEVEL" runat="server">
                                </asp:Label>
                            </td>
                            <td align="center">从事本岗位工作的时间
                            </td>
                            <td>
                                <asp:Label ID="RadDatePickerWorkStartDate" runat="server">                                  
                                </asp:Label>
                            </td>
                        </tr>

                        <tr class="GridLightBK">

                            <td align="center">聘用单位名称
                            </td>
                            <td colspan="3">
                                <asp:Label ID="RadTextBoxUnitName" runat="server">
                                </asp:Label>
                            </td>
                            <td align="center">组织机构代码
                            </td>
                            <td colspan="2">
                                <asp:Label ID="RadTextBoxUnitCode" runat="server">
                                </asp:Label>
                            </td>
                        </tr>
                        <tr class="GridLightBK">

                            <td align="center">社保比对
                            </td>
                            <td colspan="3">
                                <div runat="server" id="divSheBao" style="width: 95%; padding-left: 20px; text-align: left; line-height: 40px;"></div>
                            </td>
                            <td align="center">申请人
                            </td>
                            <td colspan="2">
                                <asp:Label ID="LabelSignupMan" runat="server">
                                </asp:Label>
                            </td>
                        </tr>
                        <tr class="GridLightBK">

                            <td align="center">要求初审地点
                            </td>
                            <td colspan="3">
                                <asp:Label ID="LabelPlace" runat="server">
                                </asp:Label>
                            </td>
                            <td align="center">计划初审日期
                            </td>
                            <td colspan="2">
                                <asp:Label ID="LabelCheckDatePlan" runat="server">
                                </asp:Label>
                            </td>
                        </tr>
                        <tr class="GridLightBK">

                            <td align="center">审核状态
                            </td>
                            <td colspan="3">
                                <asp:Label ID="LabelStatus" runat="server">
                                </asp:Label>
                            </td>
                            <td align="center"></td>
                            <td colspan="2"></td>
                        </tr>
                        <tr class="GridLightBK">
                            <td align="center">备注
                            </td>
                            <td colspan="6" id="divCheckPlan" runat="server" style="color:red">
                            
                            </td>                           
                        </tr>
                    </table>
                    <div style="margin-top: 20px">
                        <asp:Button ID="ButtonCheck" runat="server" Text="初审通过" CssClass="bt_large" OnClick="ButtonCheck_Click" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="ButtonClose" runat="server" Text="关 闭" CssClass="bt_large" OnClick="ButtonClose_Click" />
                    </div>
                </div>
            </div>--%>
        </div>
    </div>
    <uc1:IframeView ID="IframeView" runat="server" />
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function getEventObject(W3CEvent) {   //事件标准化函数
                return W3CEvent || window.event;
            }

            //回车执行查询
            function ShaoQiangSearch(e) {

                e = e || getEventObject(e);
                if (e.keyCode == 13) {
                    window.setTimeout(function () {
                        __doPostBack('<%=ButtonSearch.UniqueID %>', '');
                    }, 300);
                }
            }

        </script>
    </telerik:RadCodeBlock>
</asp:Content>
