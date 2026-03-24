<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/RadControls.Master"
    CodeBehind="CertificateInfo.aspx.cs" Inherits="ZYRYJG.PersonnelFile.CertificateInfo" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .table td {
                line-height: 150%;
                padding:4px 4px;
            }
    </style>
    <div class="div_out">
        <div id="div_top" class="dqts">
            <div id="divRoad" runat="server" style="float: left;">
                当前位置 &gt;&gt; 业务办理 &gt;&gt;从业人员
                &gt;&gt; 从业人员证书详细信息
            </div>
        </div>

        <div class="content">
            <table cellpadding="5" cellspacing="5" border="0" width="98%" class="table" align="center">
                <tr class="GridLightBK">
                    <td width="9%" nowrap="nowrap" align="right">
                        <strong>姓 名</strong>
                    </td>
                    <td width="38%">
                        <asp:Label ID="LabelWorkerName" runat="server" Text=""></asp:Label>
                    </td>
                    <td align="right" width="9%">
                        <strong>性 别</strong>
                    </td>
                    <td width="30%">
                        <asp:Label ID="LabelSex" runat="server" Text=""></asp:Label>
                    </td>
                    <td width="110px" rowspan="5" align="center" nowrap="nowrap">
                        <img id="ImgCode" runat="server" height="140" width="110" src="~/Images/photo_ry.jpg"
                            alt="一寸照片" />
                    </td>
                </tr>
                <tr class="GridLightBK">
                    <td align="right" nowrap="nowrap">
                        <strong>证件号码</strong>
                    </td>
                    <td>
                        <asp:Label ID="LabelWorkerCertificateCode" runat="server" Text=""></asp:Label>
                    </td>
                    <td align="right" nowrap="nowrap">
                        <strong>出生日期</strong>
                    </td>
                    <td>
                        <asp:Label ID="LabelBirthday" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr class="GridLightBK">
                    <td align="right" nowrap="nowrap">
                        <strong>企业名称</strong>
                    </td>
                    <td colspan="3">
                        <asp:Label ID="LabelUnitName" runat="server" Text=""></asp:Label>
                    </td>

                </tr>
                <tr class="GridLightBK">
                    <td align="right" nowrap="nowrap">
                        <strong>组织机构代码</strong>
                    </td>
                    <td>
                        <asp:Label ID="LabelUnitCode" runat="server" Text=""></asp:Label>
                    </td>
                    <td align="right" nowrap="nowrap">
                        <strong>隶属</strong>
                    </td>
                    <td>
                        <asp:Label ID="LabelLSGX" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr class="GridLightBK">
                    <td align="right" nowrap="nowrap">                      
                        <strong>职务</strong>
                    </td>
                    <td nowrap="nowrap">
                        <asp:Label ID="LabelJob" runat="server" Text=""></asp:Label>
                    </td>
                    <td nowrap="nowrap" align="right">
                        <strong>
                            <nobr>技术职称或等级</nobr>
                        </strong>
                    </td>
                    <td nowrap="nowrap">
                        <asp:Label ID="LabelSKILLLEVEL" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr class="GridLightBK">
                    <td align="right" nowrap="nowrap">
                        <strong>岗位类别</strong>
                    </td>
                    <td>
                        <asp:Label ID="LabelPostTypeID" runat="server" Text=""></asp:Label>
                    </td>
                    <td align="right" nowrap="nowrap">
                        <strong>岗位工种</strong>
                    </td>
                    <td colspan="2">
                        <asp:Label ID="LabelPostID" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr class="GridLightBK">
                    <td align="right" nowrap="nowrap">
                        <strong>证书编号</strong>
                    </td>
                    <td width="30%">
                        <asp:Label ID="LabelCertificateCode" runat="server" Text=""></asp:Label><asp:Label ID="LabelPrintCount" runat="server" Text="" Visible="false" Font-Bold="true" ForeColor="Red"></asp:Label>
                    </td>
                    <td align="right" nowrap="nowrap">
                        <strong>有效期至</strong>
                    </td>
                    <td colspan="2">
                        <asp:Label ID="LabelValidate" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr class="GridLightBK">
                    <td nowrap="nowrap" align="right">
                        <strong><asp:Label ID="LabelFZJG" runat="server" Text="发证机关"></asp:Label></strong>
                    </td>
                    <td>
                        <asp:Label ID="LabelConferUnit" runat="server" Text=""></asp:Label>
                    </td>
                    <td align="right" nowrap="nowrap">
                        <strong>发证时间</strong>
                    </td>
                    <td colspan="2">
                        <asp:Label ID="LabelConferDate" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr class="GridLightBK">
                    <td nowrap="nowrap" align="right">
                        <strong>联系电话</strong>
                    </td>
                    <td>
                        <asp:Label ID="LabelPhone" runat="server" Text=""></asp:Label>
                    </td>
                    <td align="right" nowrap="nowrap">
                        <strong>最新业务状态</strong>
                    </td>
                    <td colspan="2">
                        <asp:Label ID="LabelStatus" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr class="GridLightBK" id="TR_Remark" runat="server" visible="false">
                    <td align="left" colspan="5">
                        <strong>备注（历史数据）</strong> <span id="P_Remark" runat="server" style="line-height: 20px;"></span>
                    </td>
                </tr>
                <tr class="GridLightBK" id="TR_Disabled" runat="server" visible="false">
                    <td align="left" colspan="5" style="color: red; font-weight: bold; text-align: center">此证书为无效证书（过期、离京、注销），不能办理任何业务。
                    </td>
                </tr>
            </table>
            <div id="DivDetail" runat="server" visible="false" style="background-position: top left; background: url(../Images/lock1.png) no-repeat; width: 250px; height: 250px; position: fixed; top: 50px; left: 100px;">
            </div>

            <br />
            <div id="divCertInfo" runat="server" visible="true">
                <div style="width: 95%; margin: 10px auto; text-align: center;">
                    <input id="ButtonReturn" type="button" value="返 回" class="button" onclick="javascript: hideIfam();" />
                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="ButtonModify" runat="server" Text="修 正" Visible="false"
                        CssClass="button" OnClick="ButtonModify_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="ButtonApplyCancel" runat="server" Text="注 销" Visible="false"
                        CssClass="button" OnClick="ButtonApplyCancel_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="ButtonLock" runat="server" Text="加 锁" Visible="false"
                        OnClientClick="if(this.value=='解 锁') return confirm('确定要解锁么？');" CssClass="button"
                        OnClick="ButtonLock_Click" />
                </div>
                <telerik:RadGrid ID="RadGridLock" runat="server" GridLines="None" AllowPaging="True"
                    PageSize="10" AllowSorting="True" AutoGenerateColumns="False" SortingSettings-SortToolTip="单击进行排序"
                    Width="98%" Skin="Blue" EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false">
                    <MasterTableView CommandItemDisplay="Top" DataKeyNames="CertificateID" NoMasterRecordsText="　没有可显示的记录">
                        <CommandItemTemplate>
                            <div class="grid_CommandBar">
                                &nbsp; 证书加锁、解锁记录
                            </div>
                        </CommandItemTemplate>
                        <CommandItemStyle Height="30" HorizontalAlign="Left" />
                        <Columns>
                            <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="行号" AllowSorting="false">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" Font-Bold="true" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="LockPerson" DataField="LockPerson" HeaderText="加锁人" Visible="false">
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
                            <telerik:GridBoundColumn UniqueName="UnlockPerson" DataField="UnlockPerson" HeaderText="解锁人" Visible="false">
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
            </div>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="Model.CertificateLockOB"
                SelectMethod="GetList" TypeName="DataAccess.CertificateLockDAL" SelectCountMethod="SelectCount"
                EnablePaging="true" MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex"
                SortParameterName="orderBy">
                <SelectParameters>
                    <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                        DefaultValue="" ConvertEmptyStringToNull="false" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </div>
        <div id="divCancelReison" runat="server" style="width: 98%; margin: 1% auto; font-size: 16px; line-height: 250%" visible="false">
            <p style="font-size: 24px; font-weight: bold;">证书强制注销</p>
            <p style="color: red">注意：注销后，证书状态将同步到建设部，无法恢复，请小心操作。</p>
            <div style="width: 720px; margin: 12px auto;">
                <p style="text-align: left; font-weight: bold;">请填写注销原因（最大200字）：</p>
                <p>
                    <telerik:RadTextBox ID="RadTextBoxCertRemark" runat="server" Width="700px"
                        Skin="Default" MaxLength="400" TextMode="MultiLine" Rows="5">
                    </telerik:RadTextBox>
                </p>
            </div>
            <p>
                <asp:Button ID="ButtonCancel" runat="server" Text="确 定"
                    CssClass="button" OnClick="ButtonCancel_Click" OnClientClick="javascript:if(confirm('确定要注销证书么？')==false) return false;" />
                &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="ButtonBak" runat="server" Text="取 消"
                    CssClass="button" OnClick="ButtonBak_Click" />
            </p>
        </div>
    </div>
</asp:Content>
