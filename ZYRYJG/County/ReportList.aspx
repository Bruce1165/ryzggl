<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportList.aspx.cs" Inherits="ZYRYJG.County.ReportList" %>

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
    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
    <script src="../Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
    <link href="../layer/skin/layer.css" rel="stylesheet" />
    <script src="../layer/layer.js" type="text/javascript"></script>
    <style type="text/css">
        .link {
            border: none;
            color: #0000ff;
            background-color: transparent;
            cursor: pointer;
            font-size: 12px;
            margin-right: 12px;
        }

        #winpop {
            margin: 0;
            padding: 20px;
            overflow: hidden;
            display: none;
        }

            #winpop .title {
                width: 100%;
                height: 22px;
                line-height: 20px;
                background: #5DA2EF;
                font-weight: bold;
                text-align: center;
                font-size: 14px;
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
                    当前位置 &gt;&gt;建造师注册管理 &gt;&gt;<strong>区县汇总上报查询</strong>
                </div>
            </div>
            <div class="content">
                <table class="cx" border="0" align="center" cellspacing="1" style="width: 98%;">
                    <tr>
                        <td align="right" width="10%">区县：</td>
                        <td width="20%" align="left" nowrap="nowrap">
                            <telerik:RadComboBox ID="RadComboBoxENT_City" runat="server" Width="90%">
                                <Items>
                                    <telerik:RadComboBoxItem Text="全部" Value="" />
                                    <telerik:RadComboBoxItem Text="东城区" Value="东城区" />
                                    <telerik:RadComboBoxItem Text="西城区" Value="西城区" />
                                    <telerik:RadComboBoxItem Text="朝阳区" Value="朝阳区" />
                                    <telerik:RadComboBoxItem Text="海淀区" Value="海淀区" />
                                    <telerik:RadComboBoxItem Text="丰台区" Value="丰台区" />
                                    <telerik:RadComboBoxItem Text="石景山区" Value="石景山区" />
                                    <telerik:RadComboBoxItem Text="昌平区" Value="昌平区" />
                                    <telerik:RadComboBoxItem Text="通州区" Value="通州区" />
                                    <telerik:RadComboBoxItem Text="顺义区" Value="顺义区" />
                                    <telerik:RadComboBoxItem Text="门头沟区" Value="门头沟区" />
                                    <telerik:RadComboBoxItem Text="房山区" Value="房山区" />
                                    <telerik:RadComboBoxItem Text="大兴区" Value="大兴区" />
                                    <telerik:RadComboBoxItem Text="怀柔区" Value="怀柔区" />
                                    <telerik:RadComboBoxItem Text="平谷区" Value="平谷区" />
                                    <telerik:RadComboBoxItem Text="密云区" Value="密云" />
                                    <telerik:RadComboBoxItem Text="延庆区" Value="延庆" />
                                    <telerik:RadComboBoxItem Text="亦庄" Value="亦庄" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <td align="right" width="10%">申报事项：                                    
                        </td>
                        <td width="25%" align="left" nowrap="nowrap">

                            <telerik:RadComboBox ID="RadComboBoxApplyType" runat="server" Width="90%">
                                <Items>
                                    <telerik:RadComboBoxItem Text="全部" Value="" />
                                    <telerik:RadComboBoxItem Text="初始注册" Value="初始注册" />
                                    <telerik:RadComboBoxItem Text="变更注册" Value="变更注册" />
                                    <telerik:RadComboBoxItem Text="延续注册" Value="延期注册" />
                                    <telerik:RadComboBoxItem Text="增项注册" Value="增项注册" />
                                    <telerik:RadComboBoxItem Text="重新注册" Value="重新注册" />
                                    <telerik:RadComboBoxItem Text="遗失补办" Value="遗失补办" />
                                    <telerik:RadComboBoxItem Text="注销" Value="注销" />
                                </Items>
                            </telerik:RadComboBox>

                        </td>
                        <td align="right" width="10%">审批状态：                                    
                        </td>
                        <td width="20%" align="left" nowrap="nowrap">
                            <telerik:RadComboBox ID="RadComboBoxCheckStatus" runat="server" Width="90%">
                                <Items>
                                    <telerik:RadComboBoxItem Text="全部" Value="" />
                                    <telerik:RadComboBoxItem Text="未审查" Value="未审查" Selected="true" />
                                    <telerik:RadComboBoxItem Text="已审查" Value="已审查" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <td width="10%"></td>
                    </tr>
                    <tr>
                        <td align="right" width="90px">上报批次号：                                    
                        </td>
                        <td align="left" width="130px">
                            <telerik:RadTextBox ID="RadTextBoxReportCode" runat="server" Skin="Default" Width="90%">
                            </telerik:RadTextBox>
                        </td>
                        <td align="right" width="80px">上报日期：                                    
                        </td>
                        <td align="left">

                            <telerik:RadDatePicker ID="RadDatePickerApplyTimeStart" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                Width="100px" />
                            <div class="RadPicker md">至</div>
                            <telerik:RadDatePicker ID="RadDatePickerApplyTimeEnd" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                Width="100px" />
                        </td>

                        <td align="right">
                            <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
                        </td>
                        <td colspan="2"></td>
                    </tr>
                </table>
                <div style="width: 98%; text-align: center; clear: both; padding:20px 0">

                    <telerik:RadGrid ID="RadGridHZSB" runat="server"
                        GridLines="None" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" PageSize="15" Skin="Blue" EnableAjaxSkinRendering="False" EnableEmbeddedSkins="False"
                        SortingSettings-SortToolTip="单击进行排序" Width="100%" PagerStyle-AlwaysVisible="true">
                        <ClientSettings EnableRowHoverStyle="true">
                        </ClientSettings>
                        <MasterTableView NoMasterRecordsText=" 没有可显示的记录" DataKeyNames="ReportCode,ENT_City,ApplyType,ApplyTypeSub" CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Left">
                            <Columns>

                                <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="ENT_City" DataField="ENT_City" HeaderText="区县">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="ReportDate" DataField="ReportDate" HeaderText="上报日期" HtmlEncode="false" DataFormatString="{0:yyyy.MM.dd}">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn UniqueName="ReportCode" HeaderText="上报批次号">
                                    <ItemTemplate>
                                        <a style="color: blue" href="BusinessList.aspx?type=<%#(Eval("ApplyType").ToString()=="变更注册"?Eval("ApplyTypeSub").ToString():Eval("ApplyType").ToString()) %>&r=<%#Eval("ReportCode")%>"><%#Eval("ReportCode")%></a>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn UniqueName="ApplyType" HeaderText="申报事项">
                                    <ItemTemplate>
                                        <%#Eval("ApplyType")%><%#Eval("ApplyType").ToString()=="变更注册"?string.Format("({0})",Eval("ApplyTypeSub")):""%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn UniqueName="ENT_Name" DataField="ENT_Name" HeaderText="企业名称(只显示1个)">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="ManCount" DataField="ManCount" HeaderText="人员数量">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>

                                <telerik:GridTemplateColumn UniqueName="Edit" HeaderText="附件">
                                    <ItemTemplate>
                                        <a class="link" style="color: #0000ff" target="_blank" href="<%# ZYRYJG.UIHelp.ShowFile(string.Format("~/Upload/ReportXls/Excel{0}.xls",Eval("ReportCode")))%>" title="点击下载或鼠标右键另存">导出Execl</a>
                                        <a class="link" target="_blank" style="color: #0000ff; margin-left: 20px" href="./ReportImgView.aspx?o=<%#Eval("ReportCode")%>">扫描件</a>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="false" ForeColor="Blue" />
                                </telerik:GridTemplateColumn>
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
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="Model.ApplyMDL"
                        SelectMethod="GetReportList" TypeName="DataAccess.ApplyDAL"
                        SelectCountMethod="SelectReportCount" EnablePaging="true"
                        MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex" SortParameterName="orderBy">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                                DefaultValue="" ConvertEmptyStringToNull="false" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </div>

            </div>
        </div>

        <uc2:IframeView ID="IframeView" runat="server" />

        <div id="winpop">
        </div>
    </form>
    <script type="text/javascript">

        function tips_pop(code) {

            layer.open({
                type: 2,
                title: ['资料上传', 'font-weight:bold;background: #5DA2EF;'],//标题
                maxmin: true, //开启最大化最小化按钮,
                area: ['800px', '500px'],
                shadeClose: false, //点击遮罩关闭
                //content: $('#winpop')
                content: '../uploader/Upload.aspx?v=1&o=ReportImg/' + code,
                cancel: function (index, layero) {
                    refreshGrid();
                    return false;
                }

            });
            var MsgPop = document.getElementById("winpop");//获取窗口这个对象,即ID为winpop的对象
            MsgPop.style.display = "block";
            MsgPop.style.height = "400px";//高度增加4个象素
            //show = setInterval("changeH('up')", 2);
            //var MsgPop = document.getElementById("winpop");//获取窗口这个对象,即ID为winpop的对象
            //var popH = parseInt(MsgPop.style.height);//用parseInt将对象的高度转化为数字,以方便下面比较
            //if (popH == 0) { //如果窗口的高度是0
            //    MsgPop.style.display = "block";//那么将隐藏的窗口显示出来
            //    show = setInterval("changeH('up')", 2);//开始以每0.002秒调用函数changeH("up"),即每0.002秒向上移动一次
            //}
            //else { //否则
            //    hide = setInterval("changeH('down')", 2);//开始以每0.002秒调用函数changeH("down"),即每0.002秒向下移动一次
            //}

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


    </script>
</body>
</html>
