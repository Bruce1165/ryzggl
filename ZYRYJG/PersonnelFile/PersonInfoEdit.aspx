<%@ Page Title="" Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="PersonInfoEdit.aspx.cs" Inherits="ZYRYJG.PersonnelFile.PersonInfoEdit" %>


<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadWindowManager ID="RadWindowManager1" ShowContentDuringLoad="false" VisibleStatusbar="false"
        ReloadOnShow="true" runat="server" Skin="Windows7" EnableShadow="true">
    </telerik:RadWindowManager>
    <div class="div_out">
        <div id="div_top" class="dqts">
            <div id="divRoad" runat="server" style="float: left;">
                当前位置 &gt;&gt; 综合查询  &gt;&gt; 人员基本信息查询&gt;&gt; <strong>人员基本信息详细</strong>
               
            </div>
        </div>
        <div class="table_border" style="width: 98%; margin: 5px auto;">
            <div class="content">
                <div class="jbxxbt">
                    人员基本信息
                </div>
                <div style="width: 66%; float: left; clear: left">
                    <div style="width: 98%; margin: 0 auto; padding: 5px;" runat="server" id="divExamSignUp">

                        <table width="95%" border="0" cellpadding="5" cellspacing="1" class="table2" align="center">
                            <tr class="GridLightBK">
                                <td width="15%" nowrap="nowrap" align="right">
                                    <span style="color: Red">*</span>姓名：
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="RadTextBoxWorkerName" runat="server" Width="95%" Skin="Default">
                                    </telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="必填"
                                        ControlToValidate="RadTextBoxWorkerName" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                                <td width="15%" nowrap="nowrap" align="right">
                                    <span style="color: Red">*</span>性别：
                                </td>
                                <td width="35%">
                                    <asp:RadioButton ID="RadioButtonMan" runat="server" Text="男" GroupName="1" Checked="true" />
                                    &nbsp;<asp:RadioButton ID="RadioButtonWoman" Text="女" GroupName="1" runat="server" />
                                </td>

                            </tr>
                            <tr class="GridLightBK">
                                <td nowrap="nowrap" align="right">
                                    <span style="color: Red">*</span>证件类别：
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="RadComboBoxCertificateType" runat="server" Width="95%" NoWrap="true">
                                    </telerik:RadComboBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="必填"
                                        ControlToValidate="RadComboBoxCertificateType" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                                <td nowrap="nowrap" align="right">
                                    <span style="color: Red">*</span>证件号码：
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="RadTextCertificateCode" runat="server" Width="95%" Skin="Default"
                                        AutoPostBack="True" MaxLength="50" OnTextChanged="RadTextCertificateCode_TextChanged">
                                    </telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="必填"
                                        ControlToValidate="RadTextCertificateCode" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td align="right">
                                    <span style="color: Red">*</span>出生日期：
                                </td>
                                <td>
                                    <telerik:RadDatePicker ID="RadDatePickerBirthday" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                        Width="98%" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="必填"
                                        ControlToValidate="RadDatePickerBirthday" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                                <td width="10%" nowrap="nowrap" align="right">民族：
                                </td>
                                <td>

                                    <telerik:RadComboBox ID="RadComboBoxNation" runat="server" Width="98%" NoWrap="true" DropDownCssClass="multipleRowsColumns" DropDownWidth="620px" Height="300px">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td nowrap="nowrap" align="right">文化程度：
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="RadComboBoxCulturalLevel" runat="server" Width="98%" NoWrap="true">
                                    </telerik:RadComboBox>
                                </td>
                                <td align="right" width="10%" nowrap="nowrap">政治面貌：
                                </td>
                                <td colspan="2">

                                    <telerik:RadComboBox ID="RadComboBoxPoliticalBackground" runat="server" Width="95%" NoWrap="true">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td align="right">电子邮箱：
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="RadTextBoxEmail" runat="server" Width="95%" Skin="Default">
                                    </telerik:RadTextBox>
                                </td>
                                <td align="right" width="10%" nowrap="nowrap">联系电话：
                                </td>
                                <td colspan="2">
                                    <telerik:RadTextBox ID="RadTextBoxPhone" runat="server" Width="95%" Skin="Default">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td align="right">移动电话：
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="RadTextMobile" runat="server" Width="95%" Skin="Default">
                                    </telerik:RadTextBox>
                                </td>
                                <td align="right" width="10%" nowrap="nowrap">邮政编码：
                                </td>
                                <td colspan="2">
                                    <telerik:RadTextBox ID="RadTextZipCode" runat="server" Width="95%" Skin="Default">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                        </table>
                        <div id="DivDetail" runat="server" visible="false" style="background-position: top left; background: url(../Images/lock1.png) no-repeat; width: 250px; height: 250px; position: fixed; top: 50px; left: 150px;">
                        </div>
                    </div>
                    <br />
                    <div style="width: 95%; padding: 5px; margin: 0 auto; text-align: center;">
                        <asp:Button ID="btnSave" runat="server" Text="保 存" CssClass="button" OnClick="btnSave_Click" />
                        &nbsp;&nbsp;<asp:Button ID="ButtonLock" runat="server" Text="加 锁" Visible="false"
                            OnClientClick="if(this.value=='解 锁') return confirm('确定要解锁么？');" CssClass="button"
                            OnClick="ButtonLock_Click" />&nbsp;&nbsp;
                    <input id="ButtonReturn" type="button" value="返 回" class="button" onclick="javascript: hideIfam();" />
                    </div>
                    <br />
                    <telerik:RadGrid ID="RadGridLock" runat="server" GridLines="None" AllowPaging="True"
                        PageSize="10" AllowSorting="True" AutoGenerateColumns="False" SortingSettings-SortToolTip="单击进行排序"
                        Width="100%" Skin="Blue" EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false">
                        <MasterTableView CommandItemDisplay="Top" DataKeyNames="WorkerID" NoMasterRecordsText="　没有可显示的记录">
                            <CommandItemTemplate>
                                <div class="grid_CommandBar">
                                    &nbsp;人员加锁、解锁记录（锁定后无法报名考试）
                                </div>
                            </CommandItemTemplate>
                            <CommandItemStyle Height="30" HorizontalAlign="Left" />
                            <Columns>
                                <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="行号" AllowSorting="false">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" Font-Bold="true" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="LockPerson" DataField="LockPerson" HeaderText="加锁人">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="LockTime" DataField="LockTime" HeaderText="加锁日期"
                                    HtmlEncode="false" DataFormatString="{0:yyyy.MM.dd}">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="LockEndTime" DataField="LockEndTime" HeaderText="锁定截止时间"
                                    HtmlEncode="false" DataFormatString="{0:yyyy.MM.dd}">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="Remark" DataField="Remark" HeaderText="锁定原因说明">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="UnlockPerson" DataField="UnlockPerson" HeaderText="解锁人">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="UnlockTime" DataField="UnlockTime" HeaderText="解锁日期"
                                    HtmlEncode="false" DataFormatString="{0:yyyy.MM.dd}">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>
                            </Columns>
                            <PagerTemplate>
                                <uc2:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                            </PagerTemplate>
                            <EditFormSettings>
                                <EditColumn UniqueName="EditCommandColumn1">
                                </EditColumn>
                            </EditFormSettings>
                        </MasterTableView>
                    </telerik:RadGrid>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="Model.WorkerLockOB"
                        SelectMethod="GetList" TypeName="DataAccess.WorkerLockDAL" SelectCountMethod="SelectCount"
                        EnablePaging="true" MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex"
                        SortParameterName="orderBy">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                                DefaultValue="" ConvertEmptyStringToNull="false" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </div>
                <div id="divImg" style="width: 32%; float: left; clear: right; margin-left: 1%; overflow: visible;  margin-bottom: 200px; margin:20px 0">
                    <div >
                        <div style="width: 49%; float: left; line-height:300%;">一寸照片</div>
                        <div style="width: 49%; float: right; clear:right;line-height:300%;"><img id="img_lock" runat="server" src="~/Images/s_lock.png" visible="false" style="padding-right:8px" title="锁定中，个人无法修改" />手写签名照</div>
                        <div style="width: 49%; float: left;">
                            <img id="ImgCode" runat="server" height="140" width="110" alt="一寸照片" style="border: 1px solid #dddddd;" />
                        </div>
                        <div style="width: 49%; float: right;clear:right;">
                            <img id="ImgSign" runat="server" height="43" width="99" alt="手写签名照" style="border: 1px solid #dddddd;" />
                            <asp:Button ID="ButtonOpenUpdateSignPhoto" runat="server" Text="授权个人修改签名" CssClass="bt_maxlarge" OnClick="ButtonOpenUpdateSignPhoto_Click" Visible="false" style="margin:20px 20px" />
                        </div>
                        <div style="clear:both; line-height:300%;">手持身份证半身照</div>
                        <div>
                            <img id="ImgIDCard" runat="server" height="300" width="400" alt="手持身份证半身照" style="border: 1px solid #dddddd;"  />
                        </div>

                    </div>
                </div>
                <div style="clear: both"></div>
            </div>
        </div>
    </div>
</asp:Content>
