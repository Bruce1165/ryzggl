<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="zjsBusinessList.aspx.cs" Inherits="ZYRYJG.zjs.zjsBusinessList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc1" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc2" %>
<%@ Register Src="../CheckAll.ascx" TagName="CheckAll" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" />
    <link href="../Skins/Blue/Grid.Blue.css?v=1.008" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
    <link href="../layer/skin/layer.css" rel="stylesheet" />
    <script src="../layer/layer.js" type="text/javascript"></script>
     <link href="../css/timelimit.css?v=1.001" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Scripts/timelimit.js?v=1.003"></script>
    <style type="text/css">
        .tl, .flag_red, .flag_orange, .flag_blue, .flag_green, .flag_black,.lookout,.watch {
            color: #444;
            float: left;
            margin: 0 2px;
        }

        .pl20 {
            padding-left: 20px;
        }

        .flag_red {
            background: url(../Images/flag_red.png) no-repeat left center transparent;
        }

        .flag_orange {
            background: url(../Images/flag_purple.png) no-repeat left center transparent;
        }

        .flag_blue {
            background: url(../Images/flag_blue.png) no-repeat left center transparent;
        }

        .flag_green {
            background: url(../Images/flag_green.png) no-repeat left center transparent;
        }

        .flag_black {
            background: url(../Images/flag_black.png) no-repeat left center transparent;
        }

        .lookout {
            background: url(../Images/lookout.png) no-repeat left center transparent;
            background-size:16px 16px;
        }  
        
         .watch {
            background: url(../Images/watch.png) no-repeat left center transparent;
            background-size:16px 16px;
        }          

        .pointer {
            cursor: pointer;
            width: 16px;
            height: 16px;
        }

        .RadPicker2 {
            float: left;
            line-height: 27px;
            padding: 0px 1px;
            background-repeat: no-repeat;
        }

        .float {
            float: left; margin:4px 12px;
            position:relative;
        }
        .absolute
        {
            position:relative;
            clear:both;
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
        <div class="div_out">
            <div class="dqts">
                <div style="float: left;">
                    当前位置 &gt;&gt;造价工程师注册管理 &gt;&gt;<strong>待办业务</strong>
                </div>
            </div>
            <div class="content">
                <div class="cx absolute">
                    <div class="float">
                        <telerik:RadComboBox ID="RadComboBoxIten" runat="server" Width="100px">
                            <Items>
                                <telerik:RadComboBoxItem Text="姓名" Value="PSN_Name" />
                                <telerik:RadComboBoxItem Text="证件号码" Value="PSN_CertificateNO" />
                                <telerik:RadComboBoxItem Text="单位名称" Value="ENT_Name" />
                                <telerik:RadComboBoxItem Text="注册号" Value="PSN_RegisterNO" />
                            </Items>
                        </telerik:RadComboBox>
                        <telerik:RadTextBox ID="RadTextBoxValue" runat="server" Skin="Default" Width="150px">
                        </telerik:RadTextBox>
                    </div>

                    <%--  市级受理查询--%>
                    <div class="float" id="qxywy" runat="server"  visible="false">
                  
                        <div class="RadPicker md">申报日期：</div>
                        <telerik:RadDatePicker ID="RadDatePickerApplyTimeStart" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                            Width="100px" />
                        <div class="RadPicker md">至</div>
                        <telerik:RadDatePicker ID="RadDatePickerApplyTimeEnd" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                            Width="100px" />
                    </div>
                    <%--  市级审核查询--%>
                    <div id="qxkz" class="float" runat="server" visible="false">
                        <div class="RadPicker md">受理日期：</div>
                        <telerik:RadDatePicker ID="RadDatePickerGetDateTimeStart" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                            Width="100px" />
                        <div class="RadPicker md">至</div>
                        <telerik:RadDatePicker ID="RadDatePickerGetDateTimeEnd" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                            Width="100px" />
                    </div>                  
                    <%--  市级决定查询--%>
                    <div id="zczxld" class="float" runat="server" visible="false">
                        <div class="RadPicker md">审核日期：</div>

                        <telerik:RadDatePicker ID="RadDatePickerCheckDateStart" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                            Width="100px" />
                        <div class="RadPicker md">至</div>
                        <telerik:RadDatePicker ID="RadDatePickerCheckDateEnd" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                            Width="100px" />
                    </div>
                    <div id="Td_Status" class="float" runat="server" visible="false">
                        <telerik:RadComboBox ID="RadComboBoxApplyStatus_City" runat="server" Width="80">
                            <Items>
                                <telerik:RadComboBoxItem Text="全部" Value="全部" />
                                <telerik:RadComboBoxItem Text="已申报" Value="已申报" />
                                <telerik:RadComboBoxItem Text="已受理" Value="已受理" />
                                <telerik:RadComboBoxItem Text="已审核" Value="已审核" />                        
                                <telerik:RadComboBoxItem Text="已决定" Value="已决定" />            
                                <telerik:RadComboBoxItem Text="已公告" Value="已公告" />
                                <telerik:RadComboBoxItem Text="已驳回" Value="已驳回" />
                            </Items>
                        </telerik:RadComboBox>
                    </div>
                    <div class="float">
                        <asp:RadioButtonList ID="RadioButtonListSearchExamineResult" runat="server" RepeatDirection="Horizontal" TextAlign="right" Visible="false">
                            <asp:ListItem Text="全部" Value="" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="通过" Value="通过"></asp:ListItem>
                            <asp:ListItem Text="不通过" Value="不通过"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="float">
                        <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
                    </div>
                     <div style="clear:both"></div>
                </div>
                <div style="clear:both"></div>
                <div style="width: 90%; line-height: 20px; height: 20px; vertical-align: middle; padding: 0; margin:4px 0;">
                    <div class="tl pl20">图例说明：</div>
                   
                    <div class="flag_orange pl20">重复注册，</div>
                    <div class="flag_blue pl20">社保缺失，</div>
                    <div class="flag_black pl20">异常注册，</div>
                     <div class="lookout pl20">重点监管人员，</div>
                     <div class="watch pl20">重点核查企业</div>
                    <div id="divCheckLimit" runat="server" style="text-align:right; right:20px;cursor:pointer;color:blue;" onclick="showCheckListHelp()">《审核时限说明》</div>
                </div>                
                <telerik:RadGrid ID="RadGridQY" runat="server"
                    GridLines="None" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" Skin="Blue" EnableAjaxSkinRendering="False" EnableEmbeddedSkins="False"
                    SortingSettings-SortToolTip="单击进行排序" Width="99%" PagerStyle-AlwaysVisible="true">
                    <ClientSettings EnableRowHoverStyle="true">
                    </ClientSettings>
                    <HeaderContextMenu EnableEmbeddedSkins="False">
                    </HeaderContextMenu>
                    <MasterTableView NoMasterRecordsText=" 没有可显示的记录" CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Left"
                        DataKeyNames="ApplyID,CheckXSL,PSN_CertificateNO">
                        <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                        <Columns>
                            <telerik:GridTemplateColumn UniqueName="SelectAllColumn">
                                <HeaderTemplate>
                                    <uc3:CheckAll ID="CheckAll1" runat="server" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" CssClass="ck" runat="server" onclick='checkBoxClick(this.checked);' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="36" />
                            </telerik:GridTemplateColumn>

                            <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="PSN_Name" DataField="PSN_Name" HeaderText="姓名">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="PSN_CertificateNO" HeaderText="证件号码">
                                <ItemTemplate>
                                    <%# Eval("PSN_CertificateNO") %>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn UniqueName="ENT_Name" DataField="ENT_Name" HeaderText="单位名称">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="PSN_RegisteProfession" DataField="PSN_RegisteProfession" HeaderText="注册专业">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="PSN_RegisterNo" DataField="PSN_RegisterNo" HeaderText="注册编号">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>                            
                            <telerik:GridTemplateColumn UniqueName="ApplyType" HeaderText="申报事项">
                                        <ItemTemplate>                                   
                                            <%# Eval("ApplyType").ToString() == "变更注册" ? Eval("ApplyTypeSub") : (Eval("ApplyType").ToString()=="注销"&&Eval("Memo")!=DBNull.Value  && Eval("Memo").ToString() == "申请强制注销") ?"强制注销": Eval("ApplyType")%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridTemplateColumn>                       
                        
                            <telerik:GridTemplateColumn UniqueName="OldUnitCheckTime" HeaderText="申报日期" SortExpression="OldUnitCheckTime">
                                <ItemTemplate>
                                    <%# (Eval("newUnitCheckTime")!=DBNull.Value &&  Eval("OldUnitCheckTime")!=DBNull.Value && Convert.ToDateTime(Eval("newUnitCheckTime")) >Convert.ToDateTime(Eval("OldUnitCheckTime")) ) 
                                             ?Convert.ToDateTime(Eval("newUnitCheckTime")).ToString("yyyy.MM.dd")
                                             :(Eval("OldUnitCheckTime")!=DBNull.Value
                                                ?Convert.ToDateTime(Eval("OldUnitCheckTime")).ToString("yyyy.MM.dd")
                                                :(Eval("Memo")!=DBNull.Value && Eval("Memo").ToString()=="申请强制注销" && Eval("ApplyTime")!=DBNull.Value)
                                                    ?Convert.ToDateTime(Eval("ApplyTime")).ToString("yyyy.MM.dd")
                                                    :"")%>                               
                                </ItemTemplate> 
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridTemplateColumn>                     
                            <telerik:GridTemplateColumn UniqueName="CheckLimit" HeaderText="审核时限(工作日)" >
                                <ItemTemplate>
                                     <%# formatCheckList(Eval("ApplyType").ToString()=="变更注册"?Eval("ApplyTypeSub"):Eval("ApplyType")
                                     ,(Eval("Memo")!=DBNull.Value && Eval("Memo").ToString()=="申请强制注销" )?Eval("ApplyTime"):Eval("OldUnitCheckTime")
                                     ,Eval("newUnitCheckTime")
                                     ,Eval("ExamineDatetime"))%>                                
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridTemplateColumn>                         
                            <telerik:GridTemplateColumn UniqueName="progress" HeaderText="进度" Visible="false">
                                <ItemTemplate>
                                    <%# Eval("ApplyStatus")==DBNull.Value?"未填写": Eval("ApplyStatus")%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridTemplateColumn>
                             <telerik:GridTemplateColumn UniqueName="ExamineResult" HeaderText="审批结果"  Visible="false">
                                        <ItemTemplate>
                                            <%# Eval("ApplyStatus").ToString() =="已驳回"?(Eval("GetDateTime")==DBNull.Value?"":Eval("GetResult")):
                                                Eval("ApplyStatus").ToString() =="待确认"?(Eval("ApplyTime")==DBNull.Value?"":Convert.ToDateTime(Eval("ApplyTime")).ToString("yyyy-MM-dd")):
                                                Eval("ApplyStatus").ToString() =="已申报"?(Eval("OldUnitCheckTime")==DBNull.Value?"":Convert.ToDateTime(Eval("OldUnitCheckTime")).ToString("yyyy-MM-dd")):
                                                Eval("ApplyStatus").ToString() =="已受理"?Eval("GetResult"):
                                                Eval("ApplyStatus").ToString() =="已审核"?Eval("ExamineResult"):                                              
                                                Eval("ApplyStatus").ToString() =="已决定"?Eval("ConfirmResult"):
                                                Eval("ApplyStatus").ToString() =="已公告"?Eval("ConfirmResult"):""%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="CheckImg" HeaderText="校验图例">
                                <ItemTemplate>
                                    <%# (Eval("ApplyType").ToString() =="注销"||(Eval("ApplyType").ToString() =="变更注册" && Eval("ApplyTypeSub").ToString() != "执业企业变更"))?"":Eval("SheBaoCheck") ==DBNull.Value?"<div class=\"flag_blue pointer\" onclick='javascript:alert(\"尚未比对!（当日提交数据后,次日进行比对）\")'>&nbsp;</div>": Eval("SheBaoCheck").ToString()=="1"?"":string.Format("<div class=\"flag_blue pointer\" onclick='javascript:SetIfrmSrc(\"../County/RYSBView.aspx?o1={0}&o2={1}&o3={2}\");'>&nbsp;</div>", Eval("PSN_CertificateNO"), Eval("ENT_OrganizationsCode").ToString().Substring(8,9), Eval("ApplyTime"))%>
                                     <%# (CheckYCZC_SFZ(Eval("PSN_CertificateNO").ToString()) == true ? "<div class=\"lookout pointer\" onclick='javascript:alert(\"重点监管人员!\")'> &nbsp;</div>" : "")%>
                                    <%# (CheckUnitWatch(Eval("ENT_OrganizationsCode").ToString()) == true ? "<div class=\"watch pointer\" onclick='javascript:alert(\"重点核查企业!\")'> &nbsp;</div>" : "")%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" Wrap="false" />
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn UniqueName="Edit" HeaderText="详细">
                                <ItemTemplate>            
                                    <span class="link_edit" onclick='javascript:SetIfrmSrc("<%# GetApplyTypeUrl(Eval("ApplyType").ToString(),Eval("ApplyTypeSub").ToString()) %>?a=<%# Utility.Cryptography.Encrypt(Eval("ApplyID").ToString()) %>")'>详细</span>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn UniqueName="CheckXSL" DataField="CheckXSL" HeaderText="新设立" Visible="false">
                            </telerik:GridBoundColumn>
                        </Columns>
                        <PagerStyle AlwaysVisible="True"></PagerStyle>
                        <HeaderStyle Font-Bold="True" Wrap="false" />
                        <CommandItemStyle HorizontalAlign="Left"></CommandItemStyle>
                        <PagerTemplate>
                            <uc1:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                        </PagerTemplate>
                    </MasterTableView>
                    <PagerStyle AlwaysVisible="True"></PagerStyle>
                    <FilterMenu EnableEmbeddedSkins="False">
                    </FilterMenu>
                    <SortingSettings SortToolTip="单击进行排序"></SortingSettings>
                </telerik:RadGrid>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="Model.zjs_ApplyMDL"
                    SelectMethod="GetList" TypeName="DataAccess.zjs_ApplyDAL"
                    SelectCountMethod="SelectCount" EnablePaging="true"
                    MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex" SortParameterName="orderBy">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                            DefaultValue="" ConvertEmptyStringToNull="false" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </div>
            <div id="divQX" runat="server" visible="false" style="width: 99%; padding-top: 20px; text-align: center; clear: both; margin: auto">
                <table id="TableEdit" runat="server" border="0" cellpadding="5" cellspacing="1" class="table" style="margin: 10px auto; width: 99%">
                    <tr class="GridLightBK">
                        <td colspan="2" class="barTitle">批量受理操作（请先勾选要受理的记录）</td>
                    </tr>
                    <tr class="GridLightBK">
                        <td width="20%" align="right">受理结果：</td>
                        <td width="80%" align="left">
                            <asp:RadioButtonList ID="RadioButtonListApplyStatus" runat="server" RepeatDirection="Horizontal" TextAlign="right">
                                <asp:ListItem Text="通过" Value="通过" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="不通过" Value="不通过"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td width="20%" align="right">受理意见：</td>
                        <td width="80%" align="left">

                            <asp:TextBox ID="TextBoxApplyGetResult" runat="server" TextMode="MultiLine" Rows="3" Width="99%" Text="予以受理"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td colspan="2" align="center">
                            <asp:Button ID="BtnSave" runat="server" CssClass="bt_large" Text="确认提交" OnClick="BtnSave_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divQXCK" runat="server" visible="false" class="content" style="width: 99%; padding-top: 20px; text-align: center; clear: both; margin: auto">
                <table id="Table4" runat="server" cellspacing="5" cellpadding="5" class="table" style="text-align: center; margin: 10px auto; width: 99%!important">
                    <tr class="GridLightBK">
                        <td colspan="2" class="barTitle">批量审核操作（请先勾选要审核的记录）</td>
                    </tr>
                    <tr class="GridLightBK">
                        <td width="20%" align="right">审核结果：</td>
                        <td width="80%" align="left">
                            <asp:RadioButtonList ID="RadioButtonListExamineResult" runat="server" RepeatDirection="Horizontal" TextAlign="right">
                                <asp:ListItem Text="通过" Value="通过" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="不通过" Value="不通过"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td width="20%" align="right">审核意见：</td>
                        <td width="80%" align="left">
                            <asp:TextBox ID="TextBoxExamineRemark1" runat="server" TextMode="MultiLine" Rows="3" Width="99%" Text="允许通过"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td colspan="2" align="center">
                            <asp:Button ID="BttSave" runat="server" CssClass="bt_large" Text="确认提交" OnClick="BttSave_Click" />&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </div>          
            <div id="divDecide" runat="server" visible="false" class="content" style="width: 99%; padding-top: 20px; text-align: center; clear: both; margin: auto">
                <table id="Table3" runat="server" width="99%" border="0" cellpadding="5" cellspacing="1" class="table" style="text-align: center; margin: 10px auto">
                    <tr class="GridLightBK">
                        <td colspan="2" class="barTitle">批量决定操作（请先勾选要批准的记录）</td>
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
                        <td colspan="2" align="center">
                            <asp:Button ID="ButtonDecide" runat="server" CssClass="bt_large" Text="确认提交" OnClick="ButtonDecide_Click" />&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <uc2:IframeView ID="IframeView" runat="server" />
    </form>
    <script type="text/javascript">
        function checkBoxAllClick(checkBoxAllClientID) {
            if (checkBoxAllClientID == undefined) return;
            var ckall = document.getElementById(checkBoxAllClientID);
            if (ckall == null) return;
            var grid = ckall.parentNode;
            while (grid != null && grid != undefined && grid.nodeName != "div") {
                grid = grid.parentNode;
            }
            var ifSelect = ckall.checked;
            var Chks;
            if (grid == undefined)
                Chks = document.getElementsByTagName("input");
            else
                Chks = grid.getElementsByTagName("input");

            if (Chks.length) {
                for (i = 0; i < Chks.length; i++) {
                    if (Chks[i].type == "checkbox") {
                        Chks[i].checked = ifSelect;
                    }
                }
            }
            else if (Chks) {
                if (Chks.type == "checkbox") {
                    Chks.checked = ifSelect;
                }
            }
        }
        //变换受理结果
        $("#<%= RadioButtonListApplyStatus.ClientID%> input").each(function () {
            $(this).click(function () {
                var TextBoxApplyGetResult = $("#<%= TextBoxApplyGetResult.ClientID%>");

                if ($(this).val() == "通过") {
                    TextBoxApplyGetResult.val("予以受理");
                }
                else {
                    TextBoxApplyGetResult.val("不予受理");
                }
            });
        });
        //变换审核结果
        $("#<%= RadioButtonListExamineResult.ClientID%> input").each(function () {
            $(this).click(function () {
                var TextBoxApplyCheckRemark = $("#<%= TextBoxExamineRemark1.ClientID%>");

                if ($(this).val() == "通过") {
                    TextBoxApplyCheckRemark.val("允许通过");
                }
                else {
                    TextBoxApplyCheckRemark.val("审核未通过");
                }
            });
        });       
    </script>
</body>
</html>
