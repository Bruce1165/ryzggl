<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="zjsReportSend.aspx.cs" Inherits="ZYRYJG.zjs.zjsReportSend" %>

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
            <div  style="width: 99%; margin: 5px auto; padding-bottom: 30px;">
                <div class="dqts">
                    <div style="float: left;">
                        当前位置 &gt;&gt;造价工程师注册管理 &gt;&gt;<strong>上报</strong>
                    </div>
                </div>
               <%-- <div class="content">
                    <div class="step">
                        <div class="stepLabel">审批流程：</div>
                        <div id="step_未申报" runat="server" class="stepItem lgray step_w80" style="width: 100px">业务员受理</div>
                        <div id="Div3" runat="server" class="stepItem lgray step_w80" style="width: 100px">领导审查</div>
                        <div id="Div2" runat="server" class="stepItem lgray step_w80" style="width: 100px">业务员汇总</div>
                        <div id="step_已申报" runat="server" class="stepItem lgray green" style="max-width: 400px!important">业务员上报（先导出、领导签字、扫描、上报扫描件）</div>
                        <div class="stepArrow">▶</div>
                    </div>
                    <div class="DivTitleOn" onclick="DivOnOff(this,'Td3',event);" title="折叠">
                        说明
                    </div>
                    <div class="DivContent" id="Td3">
                        1、上报前可以修改上报数据或删除重新汇总再上报；<br />
                        2、必须导出汇总列表，经区县领导签字后扫描上传后，系统才允许上报；<br />
                        3、市建委未审查前，可以取消上报，进行修改再上报。
                        <br />
                    </div>
                    <div style="text-align: left; width: 98%; clear: both; float: left; clear: both; line-height: 26px;">
                        <div style="text-align: right; width: 15%; float: left;">
                            当前显示上报状态：
                        </div>
                        <div style="text-align: left; width: 45%; float: left;">
                            <asp:RadioButtonList ID="RadioButtonListReportStatus" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="RadioButtonListReportStatus_SelectedIndexChanged">
                                <asp:ListItem Text="全部" Value=""></asp:ListItem>
                                <asp:ListItem Text="未上报" Value="未上报" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="已上报" Value="已上报"></asp:ListItem>
                            </asp:RadioButtonList>

                        </div>
                        <div style="text-align: right; width: 39%; float: left; clear: right">
                            <asp:ImageButton ID="ImageButtonOutput" runat="server" ImageUrl="~/Images/excel.gif" OnClick="ImageButton1_Click1" Visible="false" />
                            <div id="spanOutput" runat="server"></div>
                        </div>
                    </div>
                    <div style="width: 98%; text-align: center; clear: both;">

                        <telerik:RadGrid ID="RadGridHZSB" runat="server"
                            GridLines="None" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" PageSize="15" Skin="Blue" EnableAjaxSkinRendering="False"
                            EnableEmbeddedSkins="False"
                            SortingSettings-SortToolTip="单击进行排序" Width="100%" OnItemCommand="RadGridHZSB_ItemCommand" PagerStyle-AlwaysVisible="true">
                            <ClientSettings EnableRowHoverStyle="true">
                            </ClientSettings>
                            <MasterTableView NoMasterRecordsText=" 没有可显示的记录" DataKeyNames="ReportCode,ENT_City,ApplyType,ApplyTypeSub" CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Left">
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
                                    <telerik:GridBoundColumn UniqueName="ReportCode" DataField="ReportCode" HeaderText="汇总批次号">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    
                                    <telerik:GridTemplateColumn UniqueName="ApplyType" HeaderText="事项名称">
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
                                    <telerik:GridBoundColumn UniqueName="ReportStatus" DataField="ReportStatus" HeaderText="上报状态">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn UniqueName="Edit" HeaderText="操作">
                                        <ItemTemplate>
                                            <a class="link" style="color: #0000ff" target="_blank" href="../Upload/ReportXls/Excel<%#Eval("ReportCode")%>.xls">导出</a>
                                            <%#Eval("ReportStatus").ToString()=="未上报"?"<span class=\"link\" onclick=\"javascript:tips_pop('" +Eval("ReportCode").ToString() +"')\">上传汇总扫描件</span>":""%>
                                            <input id="Button1" runat="server" type="button" value="修改" class="link" onclick='<%# string.Format("javascript:SetIfrmSrc(\"zjsReportNew.aspx?o1={0}&o2={1}&o3={2}\");return false;",Eval("ENT_City"),Eval("ReportCode"),Eval("ApplyType"))%>' visible='<%#Eval("ReportStatus").ToString()=="未上报" %>' />
                                            <asp:Button ID="Button3" runat="server" Text="上报" CssClass="link" CommandName="report" OnClientClick="javascript:if(confirm('您确认要上报吗？')==false) return false;" Visible='<%#Eval("ReportStatus").ToString()=="未上报" && IfUploadReportImg(Eval("ReportCode").ToString())==true %>' />
                                            <asp:Button ID="Button2" runat="server" Text="删除" CssClass="link" CommandName="delete" OnClientClick="javascript:if(confirm('您确认要删除吗？')==false) return false;" Visible='<%#Eval("ReportStatus").ToString()=="未上报"%>' />
                                            <%#IfUploadReportImg(Eval("ReportCode").ToString())==true?string.Format("<a class=\"link\" target=\"_blank\" style=\"color: #0000ff; margin-left:20px\"   href=\"../County/ReportImgView.aspx?o={0}\">预览</a>",Eval("ReportCode")):"" %>
                                            <asp:Button ID="Button5" runat="server" Text="取消上报" CssClass="link" CommandName="Cancelreport" OnClientClick="javascript:if(confirm('您确认要取消上报吗？')==false) return false;" Visible='<%#Eval("ReportStatus").ToString()=="已上报" && Eval("CheckStatus").ToString()=="未审查" && Eval("ApplyTypeSub").ToString()!="执业企业变更"&& Eval("ApplyTypeSub").ToString()!="企业信息变更" %>' />
                                            <input id="Button6" runat="server" type="button" value="详细" class="link" onclick='<%# string.Format("javascript:SetIfrmSrc(\"zjsReportNew.aspx?o1={0}&o2={1}&o3={2}&o4={3}\");return false;",Eval("ENT_City"),Eval("ReportCode"),Eval("ApplyType"),"详细")%>' visible='<%#Eval("ReportStatus").ToString()=="已上报" %>' />
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
                        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="Model.zjs_ApplyMDL"
                            SelectMethod="GetReportList" TypeName="DataAccess.zjs_ApplyDAL"
                            SelectCountMethod="SelectReportCount" EnablePaging="true"
                            MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex" SortParameterName="orderBy">
                            <SelectParameters>
                                <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                                    DefaultValue="" ConvertEmptyStringToNull="false" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </div>

                </div>--%>
            </div>
        </div>

        <uc2:IframeView ID="IframeView" runat="server" />

        <div id="winpop">
        </div>
    </form>
   <%-- <script type="text/javascript">

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


    </script>--%>
</body>
</html>
