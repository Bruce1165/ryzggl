<%@ Page Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true" CodeBehind="ExamPlanDetail.aspx.cs" Inherits="ZYRYJG.EXamManage.ExamPlanDetail" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../PostSelect.ascx" TagName="PostSelect" TagPrefix="uc1" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
        body{
            background-color:#ccc;
        }
    </style>
    <div class="div_out">
        <div class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt; 考务管理 &gt;&gt;
                考试报名 &gt;&gt; <strong>考试报名</strong>
            </div>
        </div>
        <div class="table_border" style="margin: 5px 5%; padding:20px 20px">
            <div class="content">
                <div class="jbxxbt">
                    考试计划详细信息
                </div>
                <div style="width: 95%; margin: 0 auto; padding: 5px;" runat="server" id="divExamSignUp">

                    <table width="95%" border="0" cellpadding="5" cellspacing="1" class="table2" align="center">
                        <tr class="GridLightBK">
                            <td align="right" style="width: 19%">考试计划名称</td>
                            <td style="width: 33%">
                                <asp:Label ID="ExamPlanName" runat="server" Text=""></asp:Label>
                            </td>
                            <td align="right" style="width: 19%">岗位工种
                            </td>
                            <td>
                                <asp:Label ID="PostTypeID" runat="server" Text=""></asp:Label>，<asp:Label ID="PostID" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td nowrap="nowrap" align="right" style="width: 19%">报名时间
                            </td>
                            <td style="width: 33%">
                                <asp:Label ID="SignUpStartDate" runat="server" Text=""></asp:Label>~
                                <asp:Label ID="SignUpEndDate" runat="server" Text=""></asp:Label>
                            </td>

                            <td align="right" nowrap="nowrap" style="width: 19%">&nbsp;&nbsp;&nbsp;&nbsp;报名地点</td>
                            <td>
                                <asp:Label ID="SignUpPlace" runat="server"></asp:Label>
                            </td>
                        </tr>

                        <tr class="GridLightBK">
                            <td align="right" style="width: 19%">准考证发放开时间</td>
                            <td style="width: 33%">
                                <asp:Label ID="ExamCardSendStartDate" runat="server"></asp:Label>~<asp:Label ID="ExamCardSendEndDate" runat="server"></asp:Label>
                            </td>
                            <td align="right" style="width: 19%">考试日期
                            </td>
                            <td>
                                <asp:Label ID="ExamStartDate" runat="server" Text=""></asp:Label>~ 
                                <asp:Label ID="ExamEndDate" runat="server"></asp:Label>
                            </td>
                        </tr>

                        <tr class="GridLightBK">
                            <td width="19%" nowrap="nowrap" align="right">审核时间</td>
                            <td width="40%">
                                <asp:Label ID="LatestCheckDate" runat="server" Text=""></asp:Label>
                            </td>
                            <td width="19%" nowrap="nowrap" align="right">
                               报名上限

                            </td>
                            <td width="40%">
                                <asp:Label ID="LatestPersonLimit" runat="server" Text="" Visible="false"></asp:Label>
                            </td>
                        </tr>

                        <tr class="GridLightBK">
                            <td width="10%" nowrap="nowrap" align="right">备注 
                            </td>
                            <td colspan="3">
                                <asp:Label ID="Remark" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>



                    <br />
                </div>

                <br />
                <div style="width: 50%; padding: 5px; margin: 0 auto; text-align: center;">
                   <%-- <asp:Button ID="btnSignUp" runat="server" Text="我要报名" CssClass="button"
                        OnClick="btnSignUp_Click" />&nbsp;&nbsp;
                    <asp:Button ID="btnMoreSignUp" runat="server" Text="批量报名" CssClass="button"
                        OnClick="btnMoreSignUp_Click" />--%>
                    &nbsp;&nbsp;<input id="Button1" type="button" value="返 回" class="button" onclick="javascript: location.href = 'ExamSignList.aspx';" />
                </div>
                <br />
            </div>
        </div>
    </div>
</asp:Content>
