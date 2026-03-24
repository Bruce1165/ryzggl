<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PublicityModify.aspx.cs" Inherits="ZYRYJG.County.PublicityModify" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
    <script src="../Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
    <link href="../layer/skin/layer.css" rel="stylesheet" />
    <script src="../layer/layer.js" type="text/javascript"></script>
    <%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc1" %>
    <%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc2" %>
    <%@ Register Src="../CheckAll.ascx" TagName="CheckAll" TagPrefix="uc3" %>

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
            <div class="table_border" style="width: 99%; margin: 5px auto; padding-bottom: 30px;">
                <div id="div_top" class="dqts">
                    <div style="float: left;">
                        当前位置 &gt;&gt;业务管理 &gt;&gt;<strong>公示修正（申述）</strong>
                    </div>
                </div>
                <div class="content">
                    <p class="jbxxbt">
                        公示修正（申述）
                    </p>
                    <div style="width: 95%; height: 100%; margin: 10px auto; text-align: center;">
                        <table id="tableSearch" runat="server">
                            <tr>
                                <td>
                                    <telerik:RadComboBox ID="RadComboBoxIfContinue1" runat="server">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="初始注册" Value="初始注册" />
                                            <telerik:RadComboBoxItem Text="重新注册" Value="重新注册" />
                                            <telerik:RadComboBoxItem Text="增项注册" Value="增项注册" />
                                            <telerik:RadComboBoxItem Text="延续注册" Value="延期注册" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                    <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
                                </td>
                            </tr>
                        </table>
                        <div style="width: 98%; text-align: left; margin: 12px 0 12px 0; color: blue;">步骤：1、为申述成功人员上传申述扫描件，被标记为√的记录表示已经上传了申述扫描件；2、点击保存按钮保存并修正公示结果。</div>
                        <div style="width: 100%; clear: both; position: relative;">
                            <telerik:RadGrid ID="RadGridADDRY" runat="server" Height="250"
                                GridLines="None" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" PageSize="500"
                                SortingSettings-SortToolTip="单击进行排序" Width="98%" Skin="Default" EnableAjaxSkinRendering="true"
                                EnableEmbeddedSkins="true" PagerStyle-AlwaysVisible="true" OnDataBound="RadGridADDRY_DataBound">
                                <ClientSettings EnableRowHoverStyle="true">
                                    <Scrolling AllowScroll="True" UseStaticHeaders="true"></Scrolling>
                                </ClientSettings>
                                <HeaderContextMenu EnableEmbeddedSkins="False">
                                </HeaderContextMenu>
                                <MasterTableView NoMasterRecordsText=" 没有可显示的记录" DataKeyNames="ApplyID,ShenShu" CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Left">
                                    <Columns>
                                         <telerik:GridTemplateColumn UniqueName="ShenShu" HeaderText=""   >
                                            <ItemTemplate>
                                             <%# Eval("ShenShu").ToString() == "1" ? "√" : ""%>
                                            </ItemTemplate>
                                              <HeaderStyle HorizontalAlign="Left" Wrap="false" Width="30" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                            <HeaderStyle HorizontalAlign="Left"  Wrap="false" Width="36"  />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="PSN_Name" DataField="PSN_Name" HeaderText="姓名">
                                            <HeaderStyle HorizontalAlign="Left" Width="70"/>
                                            <ItemStyle HorizontalAlign="Left"  Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="PSN_CertificateNO" DataField="PSN_CertificateNO" HeaderText="证件号码">
                                            <HeaderStyle HorizontalAlign="Left" Width="140"/>
                                            <ItemStyle HorizontalAlign="Left"  Wrap="false"  />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="ENT_Name" DataField="ENT_Name" HeaderText="企业名称">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left"  Wrap="false"  />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="PSN_RegisteProfession" DataField="PSN_RegisteProfession" HeaderText="注册专业">
                                            <HeaderStyle HorizontalAlign="Left" Width="80" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="PSN_RegisterNo" DataField="PSN_RegisterNo" HeaderText="注册编号">
                                            <HeaderStyle HorizontalAlign="Left" Width="120"/>
                                            <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="ApplyType" DataField="ApplyType" HeaderText="申报事项">
                                            <HeaderStyle HorizontalAlign="Left" Width="90" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="ConfirmDate" DataField="ConfirmDate" HeaderText="决定时间" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}">
                                            <HeaderStyle HorizontalAlign="Left" Width="90" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="ConfirmResult" DataField="ConfirmResult" HeaderText="决定结果">
                                            <HeaderStyle HorizontalAlign="Left" Width="70"/>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn UniqueName="Edit" HeaderText="申述附件操作">
                                            <ItemTemplate>                                             
                                                <%#Eval("ApplyStatus").ToString()=="已公示"?string.Format("<span class=\"link\" onclick=\"javascript:tips_pop('{0}/{2}','{1}','{2}')\">上传/删除</span>" ,Model.EnumManager.FileDataType.申述扫描件,Model.EnumManager.FileDataTypeName.申述扫描件,Eval("ApplyID")):""%>
                                                <a class="link_edit" style="color:blue" target="_blank"  href='<%# string.Format("./PublicityModifyImg.aspx?o={0}",Eval("ApplyID"))%>'>查看</a>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" Wrap="false" Width="130px" />
                                            <ItemStyle HorizontalAlign="Left" Wrap="false" ForeColor="Blue" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn UniqueName="t" HeaderText=""   >
                                            <ItemTemplate>
                                            </ItemTemplate>
                                              <HeaderStyle HorizontalAlign="Left" Wrap="false" Width="1" />
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                    <HeaderStyle Font-Bold="True" Wrap="false"/>
                                    <PagerTemplate>
                                        <uc1:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                                    </PagerTemplate>
                                </MasterTableView>
                                <FilterMenu EnableEmbeddedSkins="False">
                                </FilterMenu>
                                <SortingSettings SortToolTip="单击进行排序"></SortingSettings>
                            </telerik:RadGrid>
                            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="Model.ApplyMDL"
                                SelectMethod="GetListWithShenShu" TypeName="DataAccess.ApplyDAL"
                                SelectCountMethod="SelectCountWithShenShu" EnablePaging="true"
                                MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex" SortParameterName="orderBy">
                                <SelectParameters>
                                    <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                                        DefaultValue="" ConvertEmptyStringToNull="false" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </div>
                       
                    </div>
                    <div id="divMessage" runat="server" style="clear: both; position: relative; width: 100%; text-align: center; margin: 8px 0; line-height: 150%; color: blue;">
                    </div>
                    <div style="clear: both; position: relative; width: 100%; text-align: center; margin: 8px 0">
                        <asp:Button ID="BtnSave" runat="server" Text="保 存" CssClass="bt_large" OnClick="BtnSave_Click" />
                       &nbsp;&nbsp; 
                             <input id="Button2" type="button" class="bt_large" value="返 回" onclick='javascript: hideIfam();' />
                    </div>
                </div>
            </div>
        </div>
        <div id="winpop">
        </div>
          <uc2:IframeView ID="IframeView" runat="server" />
    </form>
    <script type="text/javascript">

        function tips_pop(code, ftype, pid) {

            layer.open({
                type: 2,
                title: ['资料上传', 'font-weight:bold;background: #5DA2EF;'],//标题
                maxmin: true, //开启最大化最小化按钮,
                area: ['800px', '500px'],
                shadeClose: false, //点击遮罩关闭
                //content: $('#winpop')
                content: '../uploader/Upload.aspx?v=1&o=' + code + '&t=' + ftype + '&a=' + pid,
                cancel: function (index, layero) {
                    refreshGrid();
                    return false;
                }

            });
            var MsgPop = document.getElementById("winpop");//获取窗口这个对象,即ID为winpop的对象
            MsgPop.style.display = "block";
            MsgPop.style.height = "400px";//高度增加4个象素

        }
        function changeH(str) {
            var MsgPop = document.getElementById("winpop");
            var popH = parseInt(MsgPop.style.height);
            if (str == "up") { //如果这个参数是UP
                if (popH <= 100) { //如果转化为数值的高度小于等于100
                    MsgPop.style.height = (popH + 4).toString() + "px";//高度增加4个象素
                }
                else {
                    clearInterval(show);//否则就取消这个函数调用,意思就是如果高度超过100象度了,就不再增长了
                }
            }
            if (str == "down") {
                if (popH >= 4) { //如果这个参数是down
                    MsgPop.style.height = (popH - 4).toString() + "px";//那么窗口的高度减少4个象素
                }
                else { //否则
                    clearInterval(hide); //否则就取消这个函数调用,意思就是如果高度小于4个象度的时候,就不再减了
                    MsgPop.style.display = "none"; //因为窗口有边框,所以还是可以看见1~2象素没缩进去,这时候就把DIV隐藏掉
                }
            }
        }

        function showImg(radUpload, eventArgs) {
            var input = eventArgs.get_fileInputField();
            PreviewImage(input, 'ImgReportList', 'divPreview');
        }
    </script>
</body>
</html>
