<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="QuestionEdit.aspx.cs" Inherits="ZYRYJG.jxjy.QuestionEdit" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<%@ Register Src="../CheckAll.ascx" TagName="CheckAll" TagPrefix="uc3" %>
<%@ Register Src="~/SelectSource.ascx" TagPrefix="uc1" TagName="SelectSource" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" />
    <link href="../Skins/Hot/Upload.hot.css?v=1.001" rel="stylesheet" type="text/css" />
    <link href="../Skins/Blue/Grid.Blue.css?v=1.008" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
    <script src="../Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
    <link href="../layer/skin/layer.css" rel="stylesheet" />
    <script src="../layer/layer.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:radscriptmanager id="RadScriptManager1" runat="server">
        </telerik:radscriptmanager>
        <telerik:radwindowmanager id="RadWindowManager1" showcontentduringload="false" visiblestatusbar="false" EnableEmbeddedScripts="true"  OnClientClose="OnClientClose"
            reloadonshow="true" runat="server" skin="Sunset" enableshadow="true">
        <AlertTemplate> 
            <div class="alertText">
                {1}</div>
            <div class="confrimButton">
                <input onclick="$find('{0}').close();" class="button" id="ButtonOK" type="button"
                    value="确 定" />
            </div>
        </AlertTemplate>
        <ConfirmTemplate>
            <div class="confrimText">
                {1}</div>
            <div class="confrimButton">
                <input onclick="$find('{0}').close(true);" class="button" id="ButtonOK" type="button"
                    value="确 定" />&nbsp;&nbsp;
                <input onclick="$find('{0}').close(false);" class="button" id="ButtonCancel" type="button"
                    value="取 消" />
            </div>
        </ConfirmTemplate>
    </telerik:radwindowmanager>
        <telerik:radcodeblock id="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function checkExtension(radUpload, eventArgs) {
                var input = eventArgs.get_fileInputField();
                if (!radUpload.isExtensionValid(input.value)) {
                    var inputs = radUpload.getFileInputs();
                    for (i = 0; i < inputs.length; i++) {
                        if (inputs[i] == input) {
                            alert(input.value + " 类型文件不允许上传！");
                            radUpload.clearFileInputAt(i);
                            break;
                        }
                    }
                }
            }
        </script>
    </telerik:radcodeblock>
        <telerik:radajaxmanager id="RadAjaxManager1" runat="server">
        <AjaxSettings>
        </AjaxSettings>
    </telerik:radajaxmanager>
        <telerik:radajaxloadingpanel id="RadAjaxLoadingPanel1" runat="server" visible="true"
            skin="Sunset" />
        <div class="div_out">
            <div id="div_top" class="dqts">
                <div style="float: left;">
                    当前位置 &gt;&gt;在线公益教育培训 &gt;&gt;培训试题管理&gt;&gt;<strong>试题编辑</strong>
                </div>
            </div>
            <div class="content" style="min-height:570px">
                <table id="TableEdit" runat="server" width="95%" border="0" cellpadding="5" cellspacing="1" 
                    class="tableEdit" align="center">                    
                     <tr class="GridLightBK">
                        <td width="10%" align="right">
                            <span style="color: Red">* </span>隶属课程：
                        </td>
                        <td width="48%" colspan="3">
                            <uc1:SelectSource runat="server" id="SelectSource" />
                        </td>                        
                    </tr>
                    <tr class="GridLightBK">
                        <td width="10%" align="right">
                            <span style="color: Red">* </span>试题类型：
                        </td>
                        <td width="48%">
                              <telerik:RadComboBox ID="RadComboBoxQuestionType" runat="server">
                                <Items>
                                    <telerik:RadComboBoxItem Text="请选择" Value="" />
                                     <telerik:RadComboBoxItem Text="判断题" Value="判断题" />
                                      <telerik:RadComboBoxItem Text="单选题" Value="单选题" />
                                    <telerik:RadComboBoxItem Text="多选题" Value="多选题" />
                                </Items>
                            </telerik:RadComboBox>
                            &nbsp;<asp:CompareValidator ID="CompareValidator1" runat="server" Display="Dynamic"
                                ControlToValidate="RadComboBoxQuestionType" ValueToCompare="" Type="String"
                                Operator="NotEqual" ErrorMessage="必填"></asp:CompareValidator>
                        </td>
                        <td align="right" width="10%" nowrap="nowrap">
                            <span style="color: Red">* </span>状态：
                        </td>
                        <td width="28%">
                            <asp:RadioButtonList ID="RadioButtonListStatus" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="启用" Value="启用" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="停用" Value="停用"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>                   
                    <tr class="GridLightBK" id="Tr1" runat="server">
                        <td width="12%" align="right" nowrap="nowrap">
                            <span style="color: Red">* </span>试题编号：
                        </td>
                        <td width="38%">
                            <telerik:radtextbox id="RadTextBoxQuestionNo" runat="server" width="200" skin="Default" maxlength="16">
                            </telerik:radtextbox><span style="color:#ccc">（唯一，不可重复）</span>
                            &nbsp;
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic"
                                ErrorMessage="必填" ControlToValidate="RadTextBoxQuestionNo"></asp:RequiredFieldValidator>
                        </td>
                        <td width="12%" align="right">
                            <span style="color: Red">* </span>分数：
                        </td>
                        <td width="38%">
                            <telerik:radnumerictextbox id="RadNumericTextBoxScore" runat="server" maxlength="2"
                                type="Number" numberformat-decimaldigits="0" showspinbuttons="true" width="80px"
                                minvalue="1" maxvalue="100" value="1" enabled="false">
                                <NumberFormat DecimalDigits="0"></NumberFormat>
                            </telerik:radnumerictextbox>
                            &nbsp;
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorSourceName" runat="server"
                                Display="Dynamic" ErrorMessage="必填" ControlToValidate="RadNumericTextBoxScore"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="GridLightBK" id="Tr4" runat="server">
                        <td width="12%" align="right" nowrap="nowrap">
                            <span style="color: Red">* </span>试题题目：
                        </td>
                        <td colspan="3">
                            <telerik:radtextbox id="RadTextBoxTitle" runat="server" width="95%" skin="Default"
                                textmode="MultiLine" rows="3" maxlength="4000">
                            </telerik:radtextbox>
                            &nbsp;
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorTitle" runat="server" Display="Dynamic"
                                ErrorMessage="必填" ControlToValidate="RadTextBoxTitle"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                         <td >                            
                        </td>
                        <td  align="left" colspan="3">
                            <span style="color: orangered;">添加答案选项后，勾选行头前复选框表示该选项为正确答案（可多选）。
                            </span>
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td width="10%" nowrap="nowrap" align="right">
                            <span style="color: Red">* </span>答案：
                        </td>
                        <td colspan="3">
                            <telerik:radgrid id="RadGridQuestOption" runat="server" autogeneratecolumns="False"
                                 skin="Blue" gridlines="None" allowpaging="true" EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false"
                                pagesize="10" allowsorting="false" sortingsettings-sorttooltip="单击进行排序" ondeletecommand="RadGridQuestOption_DeleteCommand"
                                oninsertcommand="RadGridQuestOption_InsertCommand" onupdatecommand="RadGridQuestOption_UpdateCommand" OnDataBound="RadGridQuestOption_DataBound"
                                cellspacing="0">
                                <ClientSettings>
                                    <Selecting AllowRowSelect="True"></Selecting>
                                </ClientSettings>
                                <MasterTableView CommandItemDisplay="Top" DataKeyNames="QuestOptionID,OptionNo" ClientDataKeyNames="QuestOptionID"
                                    EditMode="EditForms" NoMasterRecordsText="　没有可显示的记录">
                                    <CommandItemSettings AddNewRecordText="添加选项" ShowAddNewRecordButton="true" ShowRefreshButton="false">
                                    </CommandItemSettings>
                                    <CommandItemStyle Height="28px" />
                                    <Columns>
                                        <telerik:GridTemplateColumn UniqueName="SelectAllColumn">
                                            <HeaderTemplate>
                                            <uc3:CheckAll ID="CheckAll1" runat="server" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBox1" runat="server" onclick='checkBoxClick(this.checked);' />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" Width="30px" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="OptionNo" HeaderText="编号" UniqueName="OptionNo"
                                            AllowSorting="false">
                                            <HeaderStyle HorizontalAlign="Left" Width="30px" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="OptionContent" HeaderText="选项" UniqueName="OptionContent">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridEditCommandColumn ButtonType="LinkButton" EditText="修改" HeaderText="修改">
                                            <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridEditCommandColumn>
                                        <telerik:GridButtonColumn UniqueName="column2" ButtonType="LinkButton" CommandName="Delete"
                                            ConfirmText="确认删除？" ConfirmDialogType="RadWindow" Text="删除" HeaderText="删除">
                                            <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridButtonColumn>
                                    </Columns>
                                    <PagerTemplate>
                                        <uc2:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                                    </PagerTemplate>
                                    <PagerStyle AlwaysVisible="false" />
                                    <EditFormSettings EditFormType="Template" PopUpSettings-Modal="true">
                                        <FormTemplate>
                                            <br />
                                            <table class="cxtj" width="95%" cellpadding="5" cellspacing="0" border="0" align="center">
                                                <tr style="visibility: hidden">
                                                    <td width="20%" class="tousu_content" nowrap="nowrap">
                                                        <font color="red">*</font>QuestOptionID：
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextBoxQuestOptionID" runat="server" Text='<%# Eval("QuestOptionID") %>'></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="20%" class="tousu_content" nowrap="nowrap">
                                                        <font color="red">*</font> 编号：
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextBoxOptionNo" runat="server" Text='<%# Eval("OptionNo") %>' CssClass="texbox" MaxLength="1"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="必填"
                                                            ControlToValidate="TextBoxOptionNo" Display="Dynamic"></asp:RequiredFieldValidator>
                                                        <asp:RangeValidator ID="RangeValidator1" runat="server"  Type="String"  MaximumValue="H" MinimumValue="A" ControlToValidate="TextBoxOptionNo"
                                                            Display="Dynamic" ErrorMessage="范围大写字符A - H"></asp:RangeValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="20%" class="tousu_content" nowrap="nowrap">
                                                        <font color="red">*</font> 选项：
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextBoxOptionContent" runat="server" Text='<%# Eval("OptionContent") %>'
                                                            CssClass="texbox" Width="95%"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="必填"
                                                            ControlToValidate="TextBoxOptionContent" Display="Dynamic"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" align="center">
                                                        <asp:Button ID="Button2" runat="server" CssClass="button" Text='<%# (Container is GridEditFormInsertItem) ? "添 加" : "修 改" %>'
                                                            CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>' />
                                                        <asp:Button ID="Button1" runat="server" CssClass="button" Text="取 消" CausesValidation="False"
                                                            CommandName="Cancel" />
                                                    </td>
                                                </tr>
                                            </table>
                                            <br />
                                        </FormTemplate>
                                        <PopUpSettings Modal="True" Width="500px"></PopUpSettings>
                                    </EditFormSettings>
                                </MasterTableView>
                            </telerik:radgrid>
                            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="DataAccess.TrainQuestOptionDAL"
                                DataObjectTypeName="Model.TrainQuestOptionMDL" SelectMethod="GetList" InsertMethod="Insert"
                                EnablePaging="true" UpdateMethod="Update" DeleteMethod="Delete" SelectCountMethod="SelectCount"
                                MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex"
                                SortParameterName="orderBy">
                                <SelectParameters>
                                    <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                                        DefaultValue=" and 1=2" ConvertEmptyStringToNull="false" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                </table>
                <div style="padding-top: 10px; padding-bottom: 200px; vertical-align: middle; width:95%; margin:10px 0; text-align:center">
                <asp:Button ID="ButtonSave" runat="server" Text="保 存" CssClass="button" OnClick="ButtonSave_Click" />&nbsp;&nbsp;
                <input id="ButtonReturn" type="button" value="返 回" class="button" onclick="javascript: parent.refreshGrid(); hideIfam();" />
            </div>
            </div>
            
        </div>
        <div id="winpop">
        </div>
    </form>
</body>
</html>

