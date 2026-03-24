<%@ Page Title="" Language="C#" MasterPageFile="~/RadControls.master" AutoEventWireup="true" CodeFile="WebClass.aspx.cs" Inherits="Student_WebClass" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc1" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="divMain">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divMain" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default" />
     <link href="../Content/WebClass.css?v=1.001" rel="stylesheet" type="text/css" />
    <div style="padding: 8px 8px;">
        <div class="div_fun">
            <asp:Label ID="LabelBarType" runat="server" Text="首都建设云课堂"></asp:Label>
        </div>
        <div id="divMain" class="content" runat="server">
            <div id="div_list" runat="server" style="display: block;">
                <div class="cx">
                    <span><b>学习进度：</b>&nbsp;</span>
                    <asp:Button ID="ButtonAll" runat="server" Text="全 部" CssClass="btnCur" OnClick="ButtonAll_Click" />
                    <asp:Button ID="ButtonNoFisnish" runat="server" Text="未完成" CssClass="btnNo" OnClick="ButtonNoFisnish_Click" />
                    <asp:Button ID="ButtonFinish" runat="server" Text="已完成" CssClass="btnNo" OnClick="ButtonFinish_Click" />
                 <%--   &nbsp;&nbsp;&nbsp;&nbsp;<b>上架年份：</b>&nbsp;               
                <telerik:RadComboBox ID="RadComboBoxSourceYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RadComboBoxSourceYear_SelectedIndexChanged">
                    <Items></Items>
                </telerik:RadComboBox>--%>
                    &nbsp;&nbsp;&nbsp;&nbsp;<b>专业类型：</b>&nbsp;<telerik:RadComboBox ID="RadComboBoxPostType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RadComboBoxPostType_SelectedIndexChanged">
                        <Items></Items>
                    </telerik:RadComboBox>
                    <span style="color:#777;white-space: nowrap;padding-left:20px">说明：带有随堂测试题的课程，只有完成了听课并通过测试才算完成</span>
                </div>
               
                <div id="divClass" runat="server" class="main">
                </div>
            </div>
            <div id="div_class" runat="server" style="display: none;">
            </div>
            <div id="div_test" runat="server" style="display: none;">
                <div id="showData" style="width: 100%; line-height: 20px; text-align: left; padding-left: 20px; font-size: 16px;">
                    &nbsp;
                </div>
                <div id="DivTip" class="DivContent" style="text-align: left; font-size: 16px;" runat="server">
                </div>
                <div runat="server" style="margin: 5px 10px; padding: 5px 20px;">
                    <div id="DivQuestion" runat="server" style="text-align: left; padding-left: 10px; font-size: 18px; line-height: 150%;">
                    </div>
                    <telerik:RadGrid ID="RadGridQuestOption" runat="server" AutoGenerateColumns="False"
                        GridLines="None" AllowPaging="false" PageSize="10" EnableAjaxSkinRendering="true"
                        EnableEmbeddedSkins="false" Width="100%" ShowHeader="false">
                        <ClientSettings EnableRowHoverStyle="false">
                        </ClientSettings>
                        <MasterTableView CommandItemDisplay="None" DataKeyNames="QuestOptionID,OptionNo" NoMasterRecordsText="　没有可显示的记录">
                            <Columns>
                                <telerik:GridTemplateColumn UniqueName="SelectAllColumn" HeaderText="Highlight <br/> ship name">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" Width="20" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn UniqueName="SelectAllColumn" HeaderText="Highlight <br/> ship name">
                                    <ItemTemplate>
                                        <%# Eval("OptionNo")%>、<%# Eval("OptionContent")%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"  Font-Size="18px" />
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
                <br />
                  <div  style="margin: 5px 20px; padding: 5px 20px;">
                <asp:Button ID="ButtonSave" runat="server" Text="开始考试" CssClass="bt_large" OnClick="ButtonSave_Click" />&nbsp;&nbsp;
                <div id="DivQuestionAnswerTip" runat="server" style="margin: 30px 30px; text-align: center; line-height: 200%; color: red; font-size: 16px; font-weight: bold;">
                </div>
                      </div>
            </div>
            <div style="text-align:center; width:90%;margin:0 auto;"><asp:Button ID="ButtonReturn" runat="server" Text="返 回" CssClass="bt_large" OnClick="ButtonReturn_Click" Visible="false" />
                <span id="span_save" runat="server" >
                   
                </span>
            </div>
               <asp:HiddenField ID="HiddenFieldSaveSource" runat="server" />
        </div>
    </div>
 
      <script type="text/javascript">

          var interval = 1000;
          function ShowCountDown(year, month, day, _hour, _minute, _second, divname) {
              var now = new Date();
              var endDate = new Date(year, month, day, _hour, _minute, _second);
              var leftTime = endDate.getTime() - now.getTime();
              if (endDate < now) leftTime = 0;

              var leftsecond = parseInt(leftTime / 1000);
              var day1 = Math.floor(leftsecond / (60 * 60 * 24));
              var hour1 = Math.floor((leftsecond - day1 * 24 * 60 * 60) / 3600);
              var hour = Math.floor((leftsecond - 60 * 60) / 3600);
              var cc = document.getElementById(divname);

              //如果小时为负数 显示0 
              if (hour > 0) {
              }
              else {
                  hour = 0;
              }
              if (day1 < 0) {
                  hour = hour1
              }
              var minute = Math.floor((leftsecond - day1 * 24 * 60 * 60 - hour1 * 3600) / 60);
              var second = Math.floor(leftsecond - day1 * 24 * 60 * 60 - hour1 * 3600 - minute * 60);

              //如果结束时间为负数 就显示0 
              if (endDate >= now) {
                  cc.innerHTML = "剩余时间：<span style=\"color:red; font-size:18px;\">" + hour1 + "</span> 时<span style=\"color:red; font-size:18px;\">" + minute + "</span> 分<span style=\"color:red; font-size:18px;\">" + second + "</span> 秒";
              }
              else {
                  cc.innerHTML = 0 + "小时" + 0 + "分" + 0 + "秒";
              }
          }


            function setcss(listSave) {
                var s = document.getElementById(listSave).value.split(",");
                var d;
                for (var i = 0; i < s.length; i++) {
                    if (s[i] == "") continue;

                    d = document.getElementById(s[i]);
                    if (d) {
                        if (d.className == "save") {
                            d.className = "delsave";
                            d.innerText = "取消收藏";
                        }
                        else {
                            d.className = "save";
                            d.innerText = "添加收藏";
                        }
                    }
                }               
            }
            function setButtonSave(bname)
            {
                var btn = document.getElementById('ButtonSave');
                btn.value = bname;
            }
    </script>
      
</asp:Content>

