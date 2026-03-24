<%@ Page Title="" Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="MainExamOutlineNew.aspx.cs" Inherits="ZYRYJG.QuestionMgr.MainExamOutlineNew" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../PostSelect.ascx" TagName="PostSelect" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadWindowManager ID="RadWindowManager1" ShowContentDuringLoad="false" VisibleStatusbar="false"
        ReloadOnShow="true" runat="server" Skin="Windows7" EnableShadow="true">
    </telerik:RadWindowManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1"
        EnableAJAX="true">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="DivMain">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="DivMain" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true"
        Skin="Windows7" />
    <div class="div_out">
        <div class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt; 考务管理 &gt;&gt;
                题库管理 &gt;&gt; <strong>新建考试大纲</strong>
            </div>
        </div>
        <div id="DivMain" style="width: 100%; padding: 0; display: block;" runat="server">
            <div class="jbxxbt">
                新建考试大纲
            </div>
            <div class="table_border" id="div_grid" style="display: block; width: 95%; margin: 30px 20px;">
                <div class="DivContent" id="div_tip" runat="server" style="margin: 20px 0px; padding: 20px 20px 20px 20px;
                    line-height: 18px; width: 90%">
                    <b>使用说明：</b>
                    <br />
                    1、每年从知识大纲中确定考试范围、出题权重，发布后系统按考试大纲出题。
                    <br />
                    2、修改的知识大纲不影响已发布的考试大纲，如果希望将修改应用到当年考试大纲中，需要重新生成考试大纲。
                    <br />
                    3、删除考试大纲条目不影响知识大纲条目的存在。
                    <br />
                </div>
                <div style="width: 90%; margin: 0 auto; font-size: 12px; padding-left: 20px; font-weight: bold;
                    text-align: left; line-height: 30px;">
                    <div class="RadPicker">考试年度：</div>
                     <div class="RadPicker">
                    <telerik:RadNumericTextBox ID="RadNumericTextBoxYear" runat="server" MaxLength="4"
                        Type="Number" NumberFormat-DecimalDigits="0" ShowSpinButtons="true" Width="70px">
                        <NumberFormat DecimalDigits="0"></NumberFormat>
                    </telerik:RadNumericTextBox></div>
                </div>
                <div style="width: 90%; margin: 0 auto; font-size: 12px; padding-left: 20px; font-weight: bold;
                    text-align: left; line-height: 30px;">
                    <div style="width: 70px; text-align: left; float: left;">
                        岗位工种：
                    </div>
                    <div style="width: 360px; text-align: left; float: left;">
                        <uc1:PostSelect ID="PostSelect" runat="server" />
                    </div>
                    <div style="text-align: left; float: left; color: Red;">
                        （不选岗位工种表示：创建全部岗位考试大纲）
                    </div>
                </div>
                <div style="width: 90%; margin: 0 auto; font-size: 12px; padding-left: 20px; font-weight: bold;
                    text-align: left; line-height: 30px;">
                    
                    <asp:CheckBox ID="CheckBoxClearFirst" runat="server" Text="清除已存在大纲" Checked="false" />
                    <span style="color:#969696">（用于批量重新创建）</span> 
                </div>
                <div style="width: 90%; margin: 0 auto; font-size: 12px; padding-left: 20px; color: #DC2804;
                    font-weight: bold; text-align: center; line-height: 30px; margin: 20px 0px;">
                    <asp:Button ID="ButtonNew" runat="server" Text="创 建" CssClass="bt_large" OnClick="ButtonNew_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="ButtonCancel" runat="server" Text="返 回" CssClass="bt_large" OnClick="ButtonCancel_Click" /></div>
            </div>
        </div>
    </div>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    </telerik:RadCodeBlock>
</asp:Content>
