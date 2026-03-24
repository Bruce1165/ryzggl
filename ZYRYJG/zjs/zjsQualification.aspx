<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="zjsQualification.aspx.cs" Inherits="ZYRYJG.zjs.zjsQualification" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" />
    <link href="../Skins/Blue/Grid.Blue.css?v=1.008" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
    <script src="../My97DatePicker/WdatePicker.js"  type="text/javascript"></script>
    <link href="../layer/skin/layer.css" rel="stylesheet" />
    <script src="../layer/layer.js" type="text/javascript"></script>
    <%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc1" %>
    <%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc2" %>

    <style type="text/css">
        .link {
            border: none;
            color: blue;
            background-color: transparent;
            cursor: pointer;
            font-size: 12px;
        }
    </style>
    <script type="text/javascript">

        //格式化时间
        function getDateTime(date) {
            var year = date.getFullYear();
            var month = date.getMonth() + 1;
            var day = date.getDate();
            return year + "-" + month + "-" + day; 
        }
        //调用的是这个方法
        function ConvertJSONDateToJSDate(jsondate) {
            var date = new Date(parseInt(jsondate.replace("/Date(", "").replace(")/", ""), 10));
            return date;
        }
        //修改资格库
        function Update(zgzsbh)
        {
            layer.open({
                type: 1,
                //title :'资格库修改',
                title: ['资格库修改', 'font-weight:bold;background: #5DA2EF;'],//标题
                maxmin: true, //开启最大化最小化按钮,
                area: ['400px', '430px'],
                shadeClose: false, //点击遮罩关闭
                content: '\<\div style="padding:20px;">'+
                           '<table id="zgk" style="width:98%" class="table">'+
                            '<tr><td style="text-align:right">姓名：</td> <td><input id="TxtXm" type="text" style="width:98%" /></td></tr>'+
                            '<tr><td style="text-align:right">证件号码：</td> <td><input id="TxtZjhm" type="text"style="width:98%" /></td></tr>'+
                            '<tr><td style="text-align:right">工作单位：</td> <td><input id="TxtGzdw" type="text"style="width:98%" /></td></tr>'+
                            '<tr><td style="text-align:right">资格证书编号：</td> <td><input id="TxtZgzsbh" type="text"style="width:98%" /></td></tr>'+
                            '<tr><td style="text-align:right">取得年份：</td> <td><input id="TxtQdnf" type="text"  style="width:98%" /></td></tr>'+
                            '<tr><td style="text-align:right">专业类别：</td> <td><input id="TxtZylb" type="text"style="width:98%" /></td></tr>'+
                            '<tr><td style="text-align:right">签发日期：</td> <td><input id="TxtQfsj" class="Wdate" type="text"  onClick="WdatePicker()" style="width:98%" /></td></tr>'+
                            '<tr><td style="text-align:right">毕业学校：</td> <td><input id="TxtByxx" type="text"style="width:98%" /></td></tr>'+
                            '<tr><td style="text-align:right">毕业时间：</td> <td><input id="TxtBysj" class="Wdate" type="text"  onClick="WdatePicker()" style="width:98%" /></td></tr>'+
                            '<tr><td style="text-align:right">所学专业：</td> <td><input id="TxtSxzy" type="text"style="width:98%" /></td></tr>'+
                            '<tr><td style="text-align:right">最高学历：</td> <td><input id="TxtZgxl" type="text"style="width:98%" /></td></tr>'+
                           '<tr><td colspan="2" style="text-align:center"><input id="BtnSave" class="button" type="button" value="确 定"  /></td></tr>' +
                           ' </table>'+
                         '\<\/div>'
            });//
            $.post("../Ajax/zjsQualification.ashx",{action:'select',dzgzsbh:zgzsbh},function(result){
                if(result!="False")
                {
                    //拿到一个资格库对象，给编辑先附上他原来的值
                    var obj=$.parseJSON(result);
                    $("#TxtXm").val(obj.XM);
                    $("#TxtZjhm").val(obj.ZJHM);
                    $("#TxtGzdw").val(obj.GZDW);
                    $("#TxtZgzsbh").val(obj.ZGZSBH);
                    $("#TxtQdnf").val(obj.QDNF);
                    $("#TxtZylb").val(obj.ZYLB);
                    //var date = "/Date(1379944571737)/";
                    $("#TxtQfsj").val(getDateTime(ConvertJSONDateToJSDate(obj.QFSJ)));
                    $("#TxtByxx").val(obj.BYXX);
                    $("#TxtBysj").val(getDateTime(ConvertJSONDateToJSDate(obj.BYSJ)));
                    $("#TxtSxzy").val(obj.SXZY);
                    $("#TxtZgxl").val(obj.ZGXL);
                }

            })
           
            //确定按钮，如果result返回True先关闭DIV层，弹出成功信息，刷新界面
            $("#BtnSave").click(function(){
                layer.confirm('是否修改?', {icon: 3, title:'提示'}, function(index){
                    $.post("../Ajax/zjsQualification.ashx",{action:'update',dzgzsbh:zgzsbh,XM:$("#TxtXm").val(),ZJHM:$("#TxtZjhm").val(),GZDW:$("#TxtGzdw").val(),ZGZSBH:$("#TxtZgzsbh").val(),QDNF:$("#TxtQdnf").val(),ZYLB: $("#TxtZylb").val(),QFSJ: $("#TxtQfsj").val(),ZGZH: $("#TxtZgzh").val(),BYXX: $("#TxtByxx").val(),BYSJ: $("#TxtBysj").val(),SXZY: $("#TxtSxzy").val(),ZGXL: $("#TxtZgxl").val()},function(result){
                        //$.post("../Ajax/Qualification.ashx",{action:'update',XM:$("#TxtXm").val(),ZJHM:$("#TxtZjhm").val(),GZDW: $("#TxtGzdw").val(),ZYLB: $("#TxtZylb").val(),ZGZSBH: $("#TxtZgzh").val()},function(result){
                        if(result=="True")
                        {
                            layer.closeAll(); 
                            layer.alert('修改成功', {icon: 1});
                            window.location.href = window.location.href;
                        }
                        else
                        {
                            layer.closeAll(); 
                            layer.alert('修改失败'+ result, {icon: 2});
                        }
                    })
                });
            })
        }
        //删除方法
        function Delete(zgzsbh) {
            layer.confirm('是否删除?', {icon: 3, title:'提示'}, function(index){
                $.post("../Ajax/zjsQualification.ashx",{action:'delete',dzgzsbh:zgzsbh},function(result){
                    if(result=="True")
                    {
                        layer.alert('删除成功', {icon: 1});
                        window.location.href = window.location.href;
                    }
                    else
                    {
                        layer.alert('删除失败', {icon: 2});
                    }
                })
            });
        }

        //更新证书资格证编号
        function UpdateZgNo(zgzsbh,zjhm)
        {
            layer.open({
                type: 1,
                //title :'资格库修改',
                title: ['证书修改', 'font-weight:bold;background: #5DA2EF;'],//标题
                maxmin: true, //开启最大化最小化按钮,
                area: ['400px', '250px'],
                shadeClose: false, //点击遮罩关闭
                content: '\<\div style="padding:20px;">'+
                                  '<table id="zgk" style="width:98%" class="table">'+
                                   '<tr><td style="text-align:right">姓名：</td> <td><input id="TxtXm" type="text" style="width:98%" /></td></tr>'+
                                   '<tr><td style="text-align:right">证件号码：</td> <td><input id="TxtZjhm" type="text" disabled="disabled" style="width:98%" /></td></tr>'+
                                   '<tr><td style="text-align:right">性别：</td> <td><input id="TxtSex" type="text"style="width:98%" /></td></tr>'+
                                   '<tr><td style="text-align:right">当前资格证号：</td> <td><input id="TxtZgzh" type="text" /></td></tr>'+
                                   '<tr><td style="text-align:right">更改后资格证：</td> <td><input id="TxtZgzh2" type="text" style="color:#FF0000" /></td></tr>'+
                                   '<tr><td colspan="2" style="text-align:center"><input id="BtnSave" class="button" type="button" value="确 定"/></td></tr>' +
                                  ' </table>'+
                                '\<\/div>'
            });//
            $.post("../Ajax/zjsQualification.ashx",{action:'selectCert',dzgzsbh:zgzsbh},function(result){
                if(result!="False")
                {
                    //拿到一个证书表对象，给编辑先附上他原来的值
                    var obj=$.parseJSON(result);
                    $("#TxtXm").val(obj.PSN_Name);
                    $("#TxtZjhm").val(obj.PSN_CertificateNO);
                    $("#TxtSex").val(obj.PSN_Sex);
                    $("#TxtZgzh").val(obj.ZGZSBH);
                    $("#TxtZgzh2").val(zgzsbh);
                }
                else {
                    $("#BtnSave").hide();
                }
            })

            //确定按钮，如果result返回True先关闭DIV层，弹出成功信息，刷新界面
            $("#BtnSave").click(function(){
                layer.confirm('是否修改证书?', {icon: 3, title:'提示'}, function(index){
                    $.post("../Ajax/zjsQualification.ashx",{action:'updateCert',dzgzsbh:zgzsbh,PSN_Name:$("#TxtXm").val(),PSN_CertificateNO:$("#TxtZjhm").val(),PSN_Sex: $("#TxtSex").val(),ZGZSBH: $("#TxtZgzh2").val()},function(result){
                        //$.post("../Ajax/Qualification.ashx",{action:'update',XM:$("#TxtXm").val(),ZJHM:$("#TxtZjhm").val(),GZDW: $("#TxtGzdw").val(),ZYLB: $("#TxtZylb").val(),ZGZSBH: $("#TxtZgzh").val()},function(result){
                        if(result=="True")
                        {
                            layer.closeAll(); 
                            layer.alert('修改成功', {icon: 1});
                            //window.location.href = window.location.href;
                        }
                        else
                        {
                            layer.closeAll(); 
                            layer.alert('修改失败', {icon: 2});
                        }
                    })
                });
            })
        }


    </script>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server">
        </telerik:RadStyleSheetManager>
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Windows7">
        </telerik:RadWindowManager>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1"
            EnableAJAX="true">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="ButtonSearch">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGridZGGL" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="RadGridZGGL">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGridZGGL" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true" Skin="Default" />
        <div class="div_out">
            <div  style="width: 99%; margin: 5px auto; padding-bottom: 30px;">
                <div id="div_top" class="dqts">
                    <div style="float: left;">
                        <img alt="" src="../Images/ts.gif" width="17" height="11" />当前位置 &gt;&gt;造价工程师注册管理 &gt;&gt;<strong>资格库管理</strong>
                    </div>
                </div>
                <div class="content">
                    <div style="width: 95%; height: 100%; margin: 10px auto; text-align: center;">
                        <table>
                            <tr>
                                <td style="text-align: left">过滤条件：
                                    <telerik:RadComboBox ID="RadComboBoxItem" runat="server" Width="120">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="姓名" Value="XM" />
                                            <telerik:RadComboBoxItem Text="身份证号" Value="ZJHM" />
                                            <telerik:RadComboBoxItem Text="企业名称" Value="GZDW" />
                                            <telerik:RadComboBoxItem Text="资格证书编号" Value="ZGZSBH" />
                                            <telerik:RadComboBoxItem Text="专业" Value="ZYLB" />
                                        </Items>
                                    </telerik:RadComboBox>
                                    &nbsp;<telerik:RadTextBox ID="RadTextBoxZJHM" runat="server" Width="150px" Text=""></telerik:RadTextBox>
                                    <asp:Button ID="ButtonQuery" runat="server" Text="查 询" CssClass="button" OnClick="ButtonQuery_Click" />
                                </td>
                                <td align="right" style="padding-left:100px">
                                    造价工程师资格数据导入：
                                    <asp:FileUpload ID="FileUpload1" runat="server" />
                                    <asp:Button ID="ButtonAdd" runat="server" Text="导 入" CssClass="button" OnClick="ButtonAdd_Click" />
                                </td>
                                <td style="padding-left:40px">
                                    <a href="../Template/二级造价工程师资格.xlsx">
                                        <img src="../Images/xls.gif" title="下载模板" alt="下载模板" style="padding-right:4px" />下载导入模板</a>
                                </td>
                            </tr>
                        </table>
                        <div style="text-align: right" id="spanOutput" runat="server"></div>
                        <telerik:RadGrid ID="RadGridZGGL" runat="server" PageSize="5"
                            GridLines="None" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"  Skin="Blue" EnableAjaxSkinRendering="False" EnableEmbeddedSkins="False"
                            SortingSettings-SortToolTip="单击进行排序" Width="98%" PagerStyle-AlwaysVisible="true">
                            <ClientSettings EnableRowHoverStyle="true">
                            </ClientSettings>
                            <HeaderContextMenu EnableEmbeddedSkins="False">
                            </HeaderContextMenu>
                            <MasterTableView NoMasterRecordsText=" 没有可显示的记录" CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Left">
                                <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                                <Columns>
                                    <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="个人基本信息">
                                        <ItemTemplate>
                                            <b>姓　　名：</b><%# Eval("XM")%><br />
                                            <b>证件号码：</b><%# Eval("ZJHM")%><br />
                                            <b>工作单位：</b><%# Eval("GZDW")%>
                                        </ItemTemplate>
                                        <ItemStyle Width="35%" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="资格证书信息">
                                        <ItemTemplate>
                                            <b>资格证书编号：</b><%# Eval("ZGZSBH")%><br />
                                            <b>取得年份：</b><%# Eval("QDNF")%><br />
                                            <b>专业类别：</b><%# Eval("ZYLB")%><br />
                                            <b>签发时间：</b><%# Eval("QFSJ")==DBNull.Value?"":Convert.ToDateTime(Eval("QFSJ")).ToString("yyyy-MM-dd")%>
                                        </ItemTemplate>
                                        <ItemStyle Width="25%" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="个人学历信息">
                                        <ItemTemplate>
                                            <b>毕业学校：</b><%# Eval("BYXX")%><br />
                                            <b>毕业时间：</b><%# Eval("BYSJ")==DBNull.Value?"":Convert.ToDateTime(Eval("BYSJ")).ToString("yyyy-MM-dd")%><br />
                                            <b>所学专业：</b><%# Eval("SXZY")%><br />
                                            <b>最高学历：</b><%# Eval("ZGXL")%>
                                        </ItemTemplate>
                                        <ItemStyle Width="30%" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn UniqueName="Edit" HeaderText="操作">
                                        <ItemTemplate>
                                            <input id="Button1" type="button" value="修改" class="link" onclick="return Update('<%# Eval("ZGZSBH")%>')" /><br />
                                            <input id="Button2" type="button" value="删除" class="link" onclick="return Delete('<%# Eval("ZGZSBH")%>')" /><br />
                                            <input id="Button3" type="button" value="更新证书" class="link" onclick="return UpdateZgNo('<%# Eval("ZGZSBH")%>','<%#Eval("ZYLB") %>')" />
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                                    </telerik:GridTemplateColumn>
                                </Columns>
                                <PagerStyle AlwaysVisible="True"></PagerStyle>
                                <HeaderStyle Font-Bold="True" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                <AlternatingItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
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
                        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="Model.zjs_QualificationDAL"
                            SelectMethod="GetList" TypeName="DataAccess.zjs_QualificationDAL"
                            SelectCountMethod="SelectCount" EnablePaging="true"
                            MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex" SortParameterName="orderBy">
                            <SelectParameters>
                                <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                                    DefaultValue="" ConvertEmptyStringToNull="false" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

