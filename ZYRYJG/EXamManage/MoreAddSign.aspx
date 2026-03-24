<%@ Page Title="" Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="MoreAddSign.aspx.cs" Inherits="ZYRYJG.EXamManage.MoreAddSign" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../ExamNotice.ascx" TagName="ExamNotice" TagPrefix="uc1" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<%@ Register Src="../CheckAll.ascx" TagName="CheckAll" TagPrefix="uc3" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function onRequestStart(sender, args) {
                if (args.get_eventTarget().indexOf("ButtonPrint") >= 0
                    || args.get_eventTarget().indexOf("ButtonOutputWord") >= 0
                || args.get_eventTarget().indexOf("ButtonUploadSignUpTable") >= 0
                || args.get_eventTarget().indexOf("RadAsyncUploadFacePhoto") >= 0                    
                || args.get_eventTarget().indexOf("ButtonUploadImg") >= 0) {
                    args.set_enableAjax(false);
                }
            }
            function validateRadUploadTaboe(source, arguments) {
                arguments.IsValid = getRadUpload('<%= RadUploadSignUpTable.ClientID %>').validateExtensions();
            }

            function getEventObject(W3CEvent) {   //事件标准化函数
                return W3CEvent || window.event;
            }
            function getPointerPosition(e) {   //兼容浏览器的鼠标x,y获得函数
                e = e || getEventObject(e);
                var x = e.pageX || (e.clientX + (document.documentElement.scrollLeft || document.body.scrollLeft));
                var y = e.pageY || (e.clientY + (document.documentElement.scrollTop || document.body.scrollTop));

                return { 'x': x, 'y': y };
            }

            function setImgSize(img, imgWidth, timgHeight, position, e) {
                img.style.width = imgWidth + "px";
                img.style.height = timgHeight + "px";

                var pos = getPointerPosition(e);

                img.style.position = position;
                if (position == "absolute") {
                    img.style.top = -timgHeight + 20 + "px";
                    img.style.left = -imgWidth + 40 + "px";
                }
                else {
                    img.style.top = 0;
                    img.style.left = 0;
                }
            }
            function SelectMeOnly(objRadioButton, grdName) {

                var i, obj;
                for (i = 0; i < document.all.length; i++) {
                    obj = document.all(i);

                    if (obj.type == "radio") {
                        if (obj.id.indexOf(grdName) > 0) {
                            if (objRadioButton.id == obj.id)
                                obj.checked = true;
                            else
                                obj.checked = false;
                        }
                    }
                }
            }

            function RowSelecting(sender, args) {
                var id = args.get_id();
                var inputCheckBox = $get(id).getElementsByTagName("input")[0];
                SelectMeOnly(inputCheckBox, 'CheckBoxSIGNUPPLACEID');
            }
            var i = 0;
            function waitUpload() {
                var span = document.getElementById("spanwait");
                if ((i % 6) == 0) {
                    span.innerText = '';
                }
                span.innerText = span.innerText + '.';
                i++;
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1"
        EnableAJAX="true">
        <ClientEvents OnRequestStart="onRequestStart" />
        <AjaxSettings>
             <telerik:AjaxSetting AjaxControlID="divMain">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divMain"  LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>        
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true"
        Skin="Windows7" />
    <telerik:RadWindowManager runat="server" RestrictionZoneID="offsetElement" ID="RadWindowManager1"
        EnableShadow="true" EnableEmbeddedScripts="true" Skin="Windows7">
        <Windows>
            <telerik:RadWindow ID="RadWindow1" runat="server" AutoSize="true" VisibleStatusbar="false">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <style type="text/css">
        #ctl00_ContentPlaceHolder1_RadAsyncUploadFacePhoto ul li div {
            width: 215px !important;
        }
    </style>
    <div class="div_out">
        <div class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt; 考务管理 &gt;&gt;
                考试报名 &gt;&gt; <strong>考试报名--批量报名</strong>
            </div>
        </div>
        <div class="table_border" style="width: 98%; margin: 5px auto;">
            <div style="float: right; padding: 10px 30px 0px 0px;">
                <uc1:ExamNotice ID="ExamNotice1" runat="server" />
            </div>
            <div class="DivTitleOn" onclick="DivOnOff(this,'Td3',event);" title="折叠">
                填报格式说明
            </div>
            <div class="DivContent" id="Td3">
                1、办事流程:个人网上申请并上传扫描件-企业网上审核确认-住建委网上审批(咨询电话:010-55598091)。<br />
                    2、报名时填写的个人和单位信息必须真实有效，否则对后期系统使用和证书制作带来的后果自负。<br />
                    3、个人网报企业确认之后，在住建委审批前个人可以进入详细页面进行修改和取消申请操作。及时登录系统查看审核结果。(当日报名,次日社保显示结果)<br />
                    4、审核合格者按全年考试计划的规定日期打印准考证，考生持准考证和身份证按准考证上的要求参加考试。<br />
                    5、审核未合格者若在报名截止期限内，个人可重新网上申请报名或取消报名。<br />
                    6、身份证样式：必须为18位新版身份证（带X字母的必须使用英文大写）；验证身份证号请登录 <a title="身份证查询" href="http://www.nciic.com.cn"
                        target="_blank" style="color: Blue; text-decoration: underline;">http://www.nciic.com.cn</a>
                    网站。<br />
                   7、日期格式：2010-01-01或2010-1-1，其中分隔符为英文减号“-”。<br />
                   8、组织机构代码：9位数字或大写字母组合,带“-”横杠的去掉横杠，社会统一信用代码中的第9位至第17位就是企业的组织机构代码；不知道组织机构代码的请登录<a title="组织机构代码查询" href="https://www.cods.org.cn"
                        target="_blank" style="color: Blue; text-decoration: underline;">https://www.cods.org.cn</a>
                    网站，在“信息核查”栏目中查询。<br />
                    9、电子照片格式：50k以内，宽高110 x 140像素且必须为jpg格式图片（推荐使用“<a href="../Images/1寸照片生成器.exe" style="text-decoration: underline; color: Blue;"><img alt="" src="../Images/Soft_common.gif" style="border-width: 0;" />
                        1寸照片生成器.exe</a>”工具调整大小，确保图片可用）。电子照片需用本人身份证号命名。<br />
                   10、一年内累积三次考试缺考，一年内不能再次报名考试。
            </div>
            <div class="content" id="divMain" runat="server">
                <p class="jbxxbt">
                    考试批量报名
                </p>
                <div style="width: 95%; margin: 0 auto; padding: 5px;">
                    <div class="table_cx" style="clear: left; width: 100%; color: #444; line-height: 30px; font-weight: bold">
                        考试计划：<asp:Label ID="Label_ExamPlanName" runat="server" Text=""></asp:Label><br />
                        岗位类别：<asp:Label ID="Label_PostTypeName" runat="server" Text=""></asp:Label>，
                         岗位工种：<asp:Label ID="Label_PostName" runat="server" Text=""></asp:Label>，
                         考试时间：<asp:Label ID="Label_ExamDate" runat="server" Text=""></asp:Label>
                    </div>
                    <div style="font-size: 12px; padding-left: 5px;">
                        <div id="divSignupPlace" runat="server" style="width: 100%; margin: 12px auto; ">
                            <div style="color: blue; line-height: 150%; text-align: left"><span style="color: Red">* </span>请选择报名审核点（您将去此地进行初审和领取证书。）</div>
                            <telerik:RadGrid ID="RadGridSignupPlace" runat="server" AutoGenerateColumns="false"
                                Width="98%">
                                <ClientSettings EnableRowHoverStyle="true" ClientEvents-OnRowSelected="RowSelecting">
                                    <Selecting AllowRowSelect="True" />
                                </ClientSettings>
                                <MasterTableView NoMasterRecordsText="　没有可显示的记录" DataKeyNames="SIGNUPPLACEID,PLACENAME,ManLimit,SignupManCount,CHECKPERSONLIMIT,ADDRESS,PHONE">
                                    <Columns>
                                        <telerik:GridTemplateColumn UniqueName="TemplateColumn">
                                            <ItemTemplate>
                                                <asp:RadioButton ID="CheckBoxSIGNUPPLACEID" runat="server" GroupName="SIGNUPPLACEID" onclick="SelectMeOnly(this,'CheckBoxSIGNUPPLACEID')" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" Width="50px" />
                                        </telerik:GridTemplateColumn>

                                        <telerik:GridBoundColumn UniqueName="PLACENAME" DataField="PLACENAME" HeaderText="报名审核点">
                                            <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="ManLimit" DataField="ManLimit" HeaderText="报名人数上限">
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="SignupManCount" DataField="SignupManCount" HeaderText="已报名人数">
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="ADDRESS" DataField="ADDRESS" HeaderText="地址">
                                            <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>

                                    </Columns>

                                </MasterTableView>
                            </telerik:RadGrid>        
                            <div style="line-height:200%; text-align:left; padding-left:20px">当日报名最早次日社保比对才出结果,社保符合人员系统自动初审通过，不用去现场审核（请报名后次日打印报名表）。</div>                   
                        </div>
                        <table width="100%">
                            <tr>
                                <td align="left">
                                    <div style="float: left; line-height: 28px; width: 130px; text-align: left;">
                                        1、报名表模版下载：
                                    </div>
                                    <div style="float: left; line-height: 28px; clear: right;">
                                        <a id="A1" runat="server" href="~/Template/报名表导入模版.xls"><font style="color: blue; text-decoration: underline;">报名表导入模版.xls</font></a> &nbsp; &nbsp;（如果不能下载，请检查是否安装了其他下载软件的影响）
                                    </div>
                                </td>
                            </tr>
                           
                            <tr>
                                <td align="left">
                                    <span id="SpanTip" runat="server" style="font-size: 12px;">2、批量上传照片：（格式要求：一寸jpg格式图片，最大为50K，宽高102
                                        X 140像素）</span> &nbsp;&nbsp;&nbsp;&nbsp;辅助工具下载：<a href="../Images/1寸照片生成器.exe" style="text-decoration: underline; color: Blue;"><img alt="" src="../Images/Soft_common.gif" style="border-width: 0;" />
                                            1寸照片生成器.exe</a>
                                    <div style="color: Red; padding-left: 100px">
                                        （注意：图片名称必须使用考生证件号码，如“210504198805200015.jpg”）
                                    </div>
                                    <p onclick="javascript:layer.alert('<p style=\'font-size:15.0pt;font-family:仿宋;color:#333333\'><p style=\'font-size:18.0pt; text-align:center\'>考试报名上传电子照片要求</p><p>1.电子照片规格</p><p style=\'text-indent:30.0pt;\'>考生须上传近期彩色一寸白底标准正面免冠证件照。上传前，必须使用网上报名流程中提供的“一寸照片生成器”将照片处理成报考文件中要求的像素，以保证格式的正确。（本人近期彩色一寸白底标准正面免冠证件照，照片必须清晰，亮度足够，一寸jpg格式图片，最大为50K，宽高110 X 140像素）。</p><p>2.电子照片用途</p><p style=\'text-indent:30.0pt;\'>电子照片供考生参加考试和电子证书使用，请考生务必按要求上传照片。(避免因照片原因影响审核、考试及电子证书。)</p><p>3.上传照片注意</p><p style=\'text-indent:30.0pt;\'>（1）严禁上传风景照或生活照或艺术照，头像后不能出现杂物；</p><p style=\'text-indent:30.0pt;\'>（2）严禁上传使用摄像头、手机等非专业摄像装置拍摄的电子照片；</p><p style=\'text-indent:30.0pt;\'>（3）确保编辑好的电子照片头像轮廓清晰，不能模糊，照片上严禁出现姓名、号码和印章痕迹。</p></p>',{offset:'30px',icon:1,time:0,area: ['1000px', 'auto']});" style="color:blue;cursor:pointer;">【考试报名上传电子照片要求说明】</p>
                        
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <div style="float: left; clear: left; line-height: 28px; width: 130px; text-align: left;">
                                    </div>
                                    <div style="float: left; width: 215px;">
                                        <telerik:RadAsyncUpload runat="server" ID="RadAsyncUploadFacePhoto" MultipleFileSelection="Automatic" Width="215px"
                                            AutoAddFileInputs="true" OverwriteExistingFiles="true" AllowedFileExtensions="jpg"
                                            MaxFileInputsCount="1" MaxFileSize="51200" Culture="(Default)"
                                            Skin="Hot" EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false" ControlObjectsVisibility="None"
                                            InitialFileInputsCount="1" TemporaryFileExpiration="04:00:00" OnFileUploaded="RadAsyncUploadFacePhoto_FileUploaded"
                                            OnValidatingFile="RadAsyncUploadFacePhoto_ValidatingFile" HttpHandlerUrl="~/EXamManage/CustomHandler.ashx"
                                            Enabled="true" EnableFileInputSkinning="false">
                                            <Localization Delete="" Remove="" Select="选择文件" />
                                        </telerik:RadAsyncUpload>
                                    </div>
                                    <div style="float: left; padding-left: 13px;">
                                        <asp:Button ID="ButtonUploadImg" runat="server" Text="上 传" CssClass="button" OnClick="ButtonUploadImg_Click" />
                                    </div>
                                </td>
                            </tr>
                             <tr>
                                <td align="left">
                                    <div style="float: left; clear: left; line-height: 28px; width: 130px; text-align: left;">
                                        3、报名表上传：
                                    </div>
                                    <div style="float: left; text-align: left;">
                                        <telerik:RadUpload ID="RadUploadSignUpTable" runat="server" InitialFileInputsCount="1"
                                            AllowedFileExtensions="xls" ControlObjectsVisibility="None" EnableEmbeddedScripts="False"
                                            MaxFileInputsCount="1" MaxFileSize="1073741824" ReadOnlyFileInputs="true" Width="215px"
                                            Height="28px" InputSize="23" Skin="Hot" EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false">
                                            <Localization Select="选择文件" />
                                        </telerik:RadUpload>
                                    </div>
                                    <div style="float: left; padding-left: 13px;">
                                        <asp:Button ID="ButtonUploadSignUpTable" runat="server" Text="上 传" CssClass="button" OnClientClick="javascript:setInterval('waitUpload()',500)"
                                            OnClick="ButtonUploadSignUpTable_Click" />&nbsp;<asp:CustomValidator ID="Customvalidator1"
                                                runat="server" Display="Dynamic" ClientValidationFunction="validateRadUploadTaboe">
                    <span style="FONT-SIZE: 11px;">只能上传扩展名为xls的Excel文件！</span>
                                            </asp:CustomValidator>
                                        <span id="spanwait" style="font-weight:bold; font-size:20px;"></span>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="table_cx" style="clear: left; width: 100%;">
                        <span>
                            <img src="../Images/Soft_common.gif" />&nbsp;批量报名列表<asp:Label ID="LabelBatCode" runat="server"
                                Text="" ForeColor="Blue"></asp:Label></span> <span id="DivUnitName" runat="server"
                                    style="padding-left: 80px;" visible="false">按单位名称筛选：
                                    <telerik:RadComboBox ID="RadComboBoxUnitName" runat="server" Skin="Office2007" CausesValidation="False"
                                        ExpandAnimation-Duration="0" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="RadComboBoxUnitName_SelectedIndexChanged">
                                    </telerik:RadComboBox>
                                </span>
                        </div>
                        <telerik:RadGrid ID="RadGridExamSignUp" AutoGenerateColumns="False" runat="server"
                            AllowPaging="True" PageSize="10" AllowSorting="False" SortingSettings-SortToolTip="单击进行排序"
                            Skin="Blue" EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false" Width="100%" HeaderStyle-Font-Bold="true"
                            GridLines="None" OnPageIndexChanged="RadGridExamSignUp_PageIndexChanged" OnDataBound="RadGridExamSignUp_DataBound"
                            OnDeleteCommand="RadGridExamSignUp_DeleteCommand">
                            <ClientSettings Selecting-AllowRowSelect="true" EnableRowHoverStyle="false">
                                <%--    <Selecting AllowRowSelect="True" />--%>
                            </ClientSettings>
                            <MasterTableView EditMode="PopUp" CommandItemDisplay="None" DataKeyNames="ExamSignUpID,CertificateCode,SignUpCode"
                                NoMasterRecordsText="　没有可显示的记录">
                                <Columns>
                                    <telerik:GridTemplateColumn UniqueName="TemplateColumn" HeaderText="Highlight <br/> ship name">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBox1" runat="server" onclick='checkBoxClick(this.checked);' />
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            <uc3:CheckAll ID="CheckAll1" runat="server" />
                                        </HeaderTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn UniqueName="sb" HeaderText="社保">
                                        <ItemTemplate>
                                            <%# (((Eval("PostTypeID").ToString() == "1") || (Eval("PostTypeID").ToString() == "3") || (Eval("PostID").ToString() == "159") || (Eval("PostID").ToString() == "1021") || (Eval("PostID").ToString() == "1024")) 
                                             && (Convert.ToDateTime(Eval("SignUpDate")).CompareTo(DateTime.Parse("2014-07-01")) >= 0))?
                                            string.Format("<span class=\"link_edit\" onclick='javascript:SetIfrmSrc(\"../PersonnelFile/RYSBView.aspx?o1={0}&o2={1}&o3={2}\");'><nobr>社保</nobr></span>", Eval("CertificateCode").ToString() , Eval("UnitCode").ToString(), Convert.ToDateTime(Eval("SignUpDate")).ToString()):""%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn UniqueName="PostTypeName" DataField="PostTypeName" HeaderText="岗位类别"
                                        Visible="false">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="PostName" DataField="PostName" HeaderText="岗位工种"
                                        Visible="false">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="WorkerName" DataField="WorkerName" HeaderText="姓名">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="CertificateType" DataField="CertificateType"
                                        HeaderText="证件类型">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="CertificateCode" DataField="CertificateCode"
                                        HeaderText="证件号码">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="UnitName" DataField="UnitName" HeaderText="单位全称">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="UnitCode" DataField="UnitCode" HeaderText="组织机构代码">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="Status" DataField="Status" HeaderText="状态">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    </telerik:GridBoundColumn>

                                    <telerik:GridTemplateColumn UniqueName="Faceimage" HeaderText="照片">
                                        <ItemTemplate>
                                            <div style="position: relative;">
                                                <asp:Image ID="Image1" runat="server" Width="14" Height="18" ImageUrl='<%#  ShowFaceimage(Eval("CertificateCode").ToString())  %>'
                                                    onmouseover="setImgSize(this,100,140,'absolute',event);" onmouseout="setImgSize(this,14,18,'relative',event);"></asp:Image>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn UniqueName="SingUpBat" HeaderText="">
                                        <ItemTemplate>
                                            <asp:Button ID="Button2" runat="server" CssClass="button" Text="删除" CommandName="Delete"
                                                OnClientClick="if (confirm('你确定要取消报名吗?')) { } else return false;" Enabled='<%# Eval("Status").ToString() !="待初审"?false:true %>' />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                </Columns>
                                <PagerTemplate>
                                    <uc2:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                                </PagerTemplate>
                            </MasterTableView>
                        </telerik:RadGrid>
                        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="DataAccess.ExamSignUpDAL"
                            DataObjectTypeName="Model.ExamSignUpOB" SelectMethod="GetList_New" InsertMethod="Insert"
                            EnablePaging="true" UpdateMethod="Update" DeleteMethod="Delete" SelectCountMethod="SelectCount_New"
                            MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex"
                            SortParameterName="orderBy">
                            <SelectParameters>
                                <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                                    DefaultValue="" ConvertEmptyStringToNull="false" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    
                </div>
                <div style="width: 95%; padding: 5px; margin: 0 auto; text-align: center;">
                    &nbsp;<asp:Button ID="ButtonOutputWord" runat="server" Text="批量导出报名表" CssClass="bt_maxlarge"
                        OnClick="ButtonOutputWord_Click" ToolTip="大量数据导出可能会超时，推荐使用批量打印！" />
                    &nbsp;<asp:Button ID="ButtonPrint" runat="server" Text="批量打印报名表" CssClass="bt_maxlarge"
                        OnClick="ButtonPrint_Click" />
                    &nbsp;<asp:Button ID="ButtonOutputExcel" runat="server" Text="导出报名名单" CssClass="bt_maxlarge"
                        OnClick="ButtonOutputExcel_Click" />&nbsp;
                    <asp:Button runat="server" CssClass="bt_maxlarge" ID="btnDelete" Text="取消所有报名" OnClick="btnDelete_Click"
                        ToolTip="取消经我的所有报名" OnClientClick="javascript:if(confirm('确认要取消所有报名信息吗？')==false) return false;" />
                    &nbsp;<input id="Button1" type="button" value="返 回" class="button" onclick="javascript: location.href = 'ExamSignList.aspx';" />
                </div>
              
                <br />
            </div>
        </div>
    </div>
    <uc4:IframeView ID="IframeView" runat="server" />
</asp:Content>
