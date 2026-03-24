<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BusinessQueryDetail.aspx.cs" Inherits="ZYRYJG.County.BusinessQueryDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../css/Style3.css" rel="stylesheet" type="text/css" />
    <link href="../css/Grid.Blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Public.js" type="text/javascript"></script>
    <%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc1" %>
    <%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc2" %>


</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server">
        </telerik:RadStyleSheetManager>
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Sunset">
        </telerik:RadWindowManager>
        <div class="div_out">
            <div class="table_border" style="width: 99%; margin: 5px auto; padding-bottom: 30px;">
                <div id="div_top" class="dqts">
                    <div style="float: left;">
                        <img alt="" src="../Images/ts.gif" width="17" height="11" />当前位置 &gt;&gt;业务管理 &gt;&gt;业务查询 &gt;&gt;<strong>业务详细</strong>
                    </div>
                </div>
                <div class="content">
                    <p class="jbxxbt">
                        业务详细
                    </p>
                    <div style="width: 95%; margin: 10px auto; text-align: center;">
                        <telerik:RadGrid ID="RadGridRYXX" runat="server"
                            GridLines="None" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                            SortingSettings-SortToolTip="单击进行排序" Width="98%" Skin="Default" EnableAjaxSkinRendering="true"
                            EnableEmbeddedSkins="true" PagerStyle-AlwaysVisible="true">
                            <ClientSettings EnableRowHoverStyle="true">
                            </ClientSettings>
                            <HeaderContextMenu EnableEmbeddedSkins="False">
                            </HeaderContextMenu>
                            <MasterTableView NoMasterRecordsText=" 没有可显示的记录" CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Left">
                                <Columns>
                                    <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="AA" DataField="AA" HeaderText="流程">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="BB" DataField="BB" HeaderText="办理人">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="CC" DataField="CC" HeaderText="办理时间">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="DD" DataField="DD" HeaderText="办理结果">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="EE" DataField="EE" HeaderText="办理意见">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                </Columns>
                                <HeaderStyle Font-Bold="True" />
                                <PagerTemplate>
                                    <uc1:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                                </PagerTemplate>
                            </MasterTableView>
                            <FilterMenu EnableEmbeddedSkins="False">
                            </FilterMenu>
                            <SortingSettings SortToolTip="单击进行排序"></SortingSettings>
                        </telerik:RadGrid>
                        <table runat="server" id="AddTable" width="98%" border="0" cellpadding="5" cellspacing="1" class="table" style="text-align: center; margin-top:10px">
                            <tr class="GridLightBK">
                                <td colspan="4" style="color: #434343; background-color: #E4E4E4; font-weight: bold;">基本信息</td>
                            </tr>
                            <tr class="GridLightBK">
                                <td width="15%" align="right">人员类型：</td>
                                <td width="85%" align="left" colspan="3">
                                    <telerik:RadComboBox ID="RadComboBox9" runat="server" Width="125px">
                                          <Items>                             
                                            <telerik:RadComboBoxItem Text="二级注册建造师" Value="" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                             <tr class="GridLightBK">
                                <td width="15%" align="right">姓名：</td>
                                <td width="35%" align="left">
                                    <telerik:RadTextBox ID="RadTextBoxPSN_Name" runat="server"></telerik:RadTextBox>
                                </td>
                                <td width="15%" align="right">性别：</td>
                                <td width="35%" align="left" colspan="3">
                                    <telerik:RadComboBox ID="RadComboBoxPSN_Sex" runat="server" Width="125px">
                                          <Items>                             
                                            <telerik:RadComboBoxItem Text="男" Value="男" />
                                            <telerik:RadComboBoxItem Text="女" Value="女" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                             <tr class="GridLightBK">
                                <td width="15%" align="right">名族：</td>
                                <td width="35%" align="left">
                                    <telerik:RadTextBox ID="RadTextBoxNation" runat="server"></telerik:RadTextBox>
                                </td>
                                <td width="15%" align="right">出身年月：</td>
                                <td width="35%" align="left">
                                    <telerik:RadDatePicker ID="RadDatePickerBirthday" runat="server"></telerik:RadDatePicker>
                                </td>
                            </tr>
                             <tr class="GridLightBK">
                                <td colspan="4" style="color: #434343; background-color: #E4E4E4; font-weight: bold;">证件信息</td>
                            </tr>
                            <tr class="GridLightBK">
                                <td width="15%" align="right">证件类型：</td>
                                <td width="35%" align="left">
                                    <telerik:RadComboBox ID="RadComboBoxPSN_CertificateType" runat="server" Width="125px">
                                          <Items>                             
                                            <telerik:RadComboBoxItem Text="身份证" Value="身份证" />
                                            <telerik:RadComboBoxItem Text="护照" Value="护照" />
                                            <telerik:RadComboBoxItem Text="军官站" Value="军官证" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td width="15%" align="right">证件号码：</td>
                                <td width="35%" align="left">
                                   <telerik:RadTextBox ID="RadTextBoxPSN_CertificateNO" runat="server"></telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td width="15%" align="right">证件扫描件：</td>
                                <td width="35%" align="left" colspan="3">
                                    <asp:Image ID="Image1" runat="server" Height="100px" Width="100px" />
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td colspan="4" style="color: #434343; background-color: #E4E4E4; font-weight: bold;">学历信息</td>
                            </tr>
                            <tr class="GridLightBK">
                                <td width="17%" align="right">毕业学校：</td>
                                <td width="35%" align="left">
                                    <telerik:RadTextBox ID="RadTextBoxSchool" runat="server"></telerik:RadTextBox>
                                </td>
                                <td width="15%" align="right">所学专业：</td>
                                <td width="35%" align="left">
                                    <telerik:RadTextBox ID="RadTextBoxMajor" runat="server"></telerik:RadTextBox>
                                </td>
                            </tr>
                              <tr class="GridLightBK">
                                <td width="17%" align="right">毕业（肄业）时间：</td>
                                <td width="35%" align="left">
                                   <telerik:RadDatePicker ID="RadDatePickerGraduationTime" runat="server"></telerik:RadDatePicker>
                                </td>
                                <td width="15%" align="right">最高学历：</td>
                                <td width="35%" align="left">
                                    <telerik:RadTextBox ID="RadTextBoxXueLi" runat="server"></telerik:RadTextBox>
                                </td>
                            </tr>
                             <tr class="GridLightBK">
                                <td width="17%" align="right">学位：</td>
                                <td width="35%" align="left">
                                   <telerik:RadTextBox ID="RadTextBoxXueWei" runat="server"></telerik:RadTextBox>
                                </td>
                                <td width="15%" align="right">学历证书附件：</td>
                                <td width="35%" align="left">
                                   <asp:Image ID="Image2" runat="server" Height="100px" Width="100px" />
                                </td>
                            </tr>
                              <tr class="GridLightBK">
                                <td colspan="4" style="color: #434343; background-color: #E4E4E4; font-weight: bold;">联系信息</td>
                            </tr>
                            <tr class="GridLightBK">
                                <td width="17%" align="right">手机：</td>
                                <td width="35%" align="left">
                                    <telerik:RadTextBox ID="RadTextBoxPSN_MobilePhone" runat="server"></telerik:RadTextBox>
                                </td>
                                <td width="15%" align="right">联系电话：</td>
                                <td width="35%" align="left">
                                    <telerik:RadTextBox ID="RadTextBoxPSN_Telephone" runat="server"></telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td width="17%" align="right">电子邮箱：</td>
                                <td width="35%" align="left" colspan="3">
                                    <telerik:RadTextBox ID="RadTextBoxPSN_Email" runat="server"></telerik:RadTextBox>
                                </td>
                            </tr>
                             <tr class="GridLightBK">
                                <td colspan="4" style="color: #434343; background-color: #E4E4E4; font-weight: bold;">执业资格证书信息</td>
                            </tr>
                            <tr class="GridLightBK">
                                <td width="15%" align="right">资格证书专业类别：</td>
                                <td width="35%" align="left">
                                    <telerik:RadComboBox ID="RadComboBoxZhuanYe" runat="server" Width="125px">
                                          <Items>                             
                                            <telerik:RadComboBoxItem Text="建筑工程" Value="建筑工程" />
                                            <telerik:RadComboBoxItem Text="公路工程" Value="公路工程" />
                                            <telerik:RadComboBoxItem Text="水利水电工程" Value="水利水电工程" />
                                            <telerik:RadComboBoxItem Text="市政公用工程" Value="市政公用工程" />
                                            <telerik:RadComboBoxItem Text="矿业工程" Value="矿业工程" />
                                            <telerik:RadComboBoxItem Text="机电工程" Value="机电工程" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td width="15%" align="right">取得方式：</td>
                                <td width="35%" align="left">
                                   <telerik:RadTextBox ID="RadTextBoxGetType" runat="server"></telerik:RadTextBox>
                                </td>
                            </tr>
                             <tr class="GridLightBK">
                                <td width="17%" align="right">证书编号：</td>
                                <td width="35%" align="left">
                                    <telerik:RadTextBox ID="RadTextBoxPSN_RegisterNo" runat="server"></telerik:RadTextBox>
                                </td>
                                <td width="15%" align="right">签发日期：</td>
                                <td width="35%" align="left">
                                  <telerik:RadDatePicker ID="RadDatePickerConferDate" runat="server"></telerik:RadDatePicker>
                                </td>
                            </tr>
                             <tr class="GridLightBK">
                                 <td width="15%" align="right">申请注册专业：</td>
                                <td width="35%" align="left">
                                    <telerik:RadTextBox ID="RadTextBoxApplyRegisteProfession" runat="server"></telerik:RadTextBox>
                               </td>
                                <td width="15%" align="right">执业资格证书附件：</td>
                                <td width="35%" align="left">
                                    <asp:Image ID="Image3" runat="server" Height="100px" Width="100px" />
                               </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td colspan="4" style="color: #434343; background-color: #E4E4E4; font-weight: bold;">劳动合同信息</td>
                            </tr>
                             <tr class="GridLightBK">
                                <td width="15%" align="right">劳动合同开始时间：</td>
                                <td width="35%" align="left">
                                    <telerik:RadDatePicker ID="RadDatePicker1" runat="server"></telerik:RadDatePicker>
                               </td>
                                 <td width="15%" align="right">劳动合同结束时间：</td>
                                <td width="35%" align="left">
                                    <telerik:RadDatePicker ID="RadDatePicker2" runat="server"></telerik:RadDatePicker>
                               </td>
                            </tr>
                              <tr class="GridLightBK">
                                <td width="15%" align="right">企业负责人：</td>
                                <td width="35%" align="left">
                                    <telerik:RadTextBox ID="RadTextBox1" runat="server"></telerik:RadTextBox>
                               </td>
                                 <td width="15%" align="right">劳动合同附件：</td>
                                <td width="35%" align="left">
                                    <asp:Image ID="Image4" runat="server" Height="100px" Width="100px" />
                               </td>
                            </tr>
                        </table>
                    </div>

                </div>
            </div>
        </div>
    </form>
</body>
</html>
