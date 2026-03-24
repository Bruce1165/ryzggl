<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="ZYRYJG.test" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="./GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
        <script type="text/javascript" src="Scripts/jquery-3.4.1.min.js"></script>
          <link href="layer/skin/layer.css" rel="stylesheet" />
    <script src="layer/layer.js" type="text/javascript"></script>
     <link href="Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" />
    <link href="Skins/Blue/Grid.Blue.css?v=1.008" rel="stylesheet" type="text/css" />
    <script src="Scripts/Public.js?v=1.011" type="text/javascript"></script>
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
               
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true" Skin="Default" />
        <div style="width: 100%; text-align: left; padding: 100px 100px; line-height: 300%; ">
            <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Text="初始注册" Value="初始注册"></asp:ListItem>
                <asp:ListItem Text="延续注册" Value="延续注册"></asp:ListItem>
                <asp:ListItem Text="注销注册" Value="注销注册"></asp:ListItem>
                <asp:ListItem Text="变更注册" Value="变更注册"></asp:ListItem>
                <asp:ListItem Text="从业人员" Value="从业人员"></asp:ListItem>

            </asp:RadioButtonList>
            <asp:Button ID="ButtonWorker" runat="server" Text="个人登陆" OnClick="ButtonWorker_Click" Style="cursor: pointer;" class="button" onfocus="this.blur()" />
            &nbsp;&nbsp
     <asp:Button ID="ButtonQY" runat="server" Text="企业登陆" OnClick="ButtonQY_Click" class="button" onfocus="this.blur()" />
            &nbsp;&nbsp
                <asp:Button ID="ButtonAdmin" runat="server" Text="管理登录" OnClick="ButtonAdmin_Click" class="button" onfocus="this.blur()" Visible="false" />
            &nbsp;&nbsp
                <asp:Button ID="ButtonFW" runat="server" Text="市级受理人员登录" OnClick="ButtonFW_Click" class="button" onfocus="this.blur()" />
            &nbsp;&nbsp
                <asp:Button ID="ButtonFWLD" runat="server" Text="市级审核人员登录" OnClick="ButtonFWLD_Click" class="button" onfocus="this.blur()" />
            &nbsp;&nbsp
                <asp:Button ID="ButtonZCLD" runat="server" Text="市级决定人员登录" OnClick="ButtonZCLD_Click" class="button" onfocus="this.blur()" />
            &nbsp;&nbsp
                <asp:Button ID="ButtonZCYW" runat="server" Text="审批流程管理人员（可后退）登录" OnClick="ButtonZCYW_Click" class="button" onfocus="this.blur()" />
            &nbsp;&nbsp
                <asp:Button ID="ButtonaAdmin" runat="server" Text="管理员登录" OnClick="ButtonaAdmin_Click" class="button" onfocus="this.blur()" />
            &nbsp;&nbsp
                <asp:Button ID="ButtonTrainUnit" runat="server" Text="培训点登录" OnClick="ButtonTrainUnit_Click" class="button" onfocus="this.blur()" />
            &nbsp;&nbsp
                <asp:Button ID="ButtonTrainUnit2" runat="server" Text="培训点登录2" OnClick="ButtonTrainUnit2_Click" class="button" onfocus="this.blur()" />
            <br />

            <asp:Button ID="ButtonUpJZGCStoJSB" runat="server" Text="上传建造工程师到建设部" OnClick="ButtonUpJZGCStoJSB_Click" Visible="false" />
        </div>
    <div id="div_view" runat="server">
        <%--<code class="hljs language-cobol"><ol class="hljs-ln" style="width:1111px"><li><div class="hljs-ln-numbers"><div class="hljs-ln-line hljs-ln-n" data-line-number="1"></div></div><div class="hljs-ln-code"><div class="hljs-ln-line"><span class="hljs-comment">public</span>&nbsp;<span class="hljs-keyword">class</span>&nbsp;PdfToImage</div></div></li><li><div class="hljs-ln-numbers"><div class="hljs-ln-line hljs-ln-n" data-line-number="2"></div></div><div class="hljs-ln-code"><div class="hljs-ln-line">{</div></div></li><li><div class="hljs-ln-numbers"><div class="hljs-ln-line hljs-ln-n" data-line-number="3"></div></div><div class="hljs-ln-code"><div class="hljs-ln-line">&nbsp;&nbsp;&nbsp;&nbsp;<span class="hljs-operator">/</span><span class="hljs-operator">/</span><span class="hljs-operator">/</span>&nbsp;<span class="hljs-operator">&lt;</span>summary<span class="hljs-operator">&gt;</span></div></div></li><li><div class="hljs-ln-numbers"><div class="hljs-ln-line hljs-ln-n" data-line-number="4"></div></div><div class="hljs-ln-code"><div class="hljs-ln-line">&nbsp;&nbsp;&nbsp;&nbsp;<span class="hljs-operator">/</span><span class="hljs-operator">/</span><span class="hljs-operator">/</span>&nbsp;</div></div></li><li><div class="hljs-ln-numbers"><div class="hljs-ln-line hljs-ln-n" data-line-number="5"></div></div><div class="hljs-ln-code"><div class="hljs-ln-line">&nbsp;&nbsp;&nbsp;&nbsp;<span class="hljs-operator">/</span><span class="hljs-operator">/</span><span class="hljs-operator">/</span>&nbsp;<span class="hljs-operator">&lt;</span><span class="hljs-operator">/</span>summary<span class="hljs-operator">&gt;</span></div></div></li><li><div class="hljs-ln-numbers"><div class="hljs-ln-line hljs-ln-n" data-line-number="6"></div></div><div class="hljs-ln-code"><div class="hljs-ln-line">&nbsp;&nbsp;&nbsp;&nbsp;<span class="hljs-operator">/</span><span class="hljs-operator">/</span><span class="hljs-operator">/</span>&nbsp;<span class="hljs-operator">&lt;</span>param&nbsp;name<span class="hljs-operator">=</span><span class="hljs-string">"filePath"</span><span class="hljs-operator">&gt;</span>pdf文件路径<span class="hljs-operator">&lt;</span><span class="hljs-operator">/</span>param<span class="hljs-operator">&gt;</span></div></div></li><li><div class="hljs-ln-numbers"><div class="hljs-ln-line hljs-ln-n" data-line-number="7"></div></div><div class="hljs-ln-code"><div class="hljs-ln-line">&nbsp;&nbsp;&nbsp;&nbsp;<span class="hljs-operator">/</span><span class="hljs-operator">/</span><span class="hljs-operator">/</span>&nbsp;<span class="hljs-operator">&lt;</span>param&nbsp;name<span class="hljs-operator">=</span><span class="hljs-string">"picPath"</span><span class="hljs-operator">&gt;</span><span class="hljs-keyword">picture</span>文件路径<span class="hljs-operator">&lt;</span><span class="hljs-operator">/</span>param<span class="hljs-operator">&gt;</span></div></div></li><li><div class="hljs-ln-numbers"><div class="hljs-ln-line hljs-ln-n" data-line-number="8"></div></div><div class="hljs-ln-code"><div class="hljs-ln-line">&nbsp;&nbsp;&nbsp;&nbsp;public&nbsp;&nbsp;void&nbsp;PdfToPic(<span class="hljs-keyword">string</span>&nbsp;filePath,&nbsp;<span class="hljs-keyword">string</span>&nbsp;picPath)</div></div></li><li><div class="hljs-ln-numbers"><div class="hljs-ln-line hljs-ln-n" data-line-number="9"></div></div><div class="hljs-ln-code"><div class="hljs-ln-line">&nbsp;&nbsp;&nbsp;&nbsp;{</div></div></li><li><div class="hljs-ln-numbers"><div class="hljs-ln-line hljs-ln-n" data-line-number="10"></div></div><div class="hljs-ln-code"><div class="hljs-ln-line"> </div></div></li><li><div class="hljs-ln-numbers"><div class="hljs-ln-line hljs-ln-n" data-line-number="11"></div></div><div class="hljs-ln-code"><div class="hljs-ln-line">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;pdf&nbsp;<span class="hljs-operator">=</span>&nbsp;PdfiumViewer.PdfDocument.Load(filePath);</div></div></li><li><div class="hljs-ln-numbers"><div class="hljs-ln-line hljs-ln-n" data-line-number="12"></div></div><div class="hljs-ln-code"><div class="hljs-ln-line">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;pdfpage&nbsp;<span class="hljs-operator">=</span>&nbsp;pdf.PageCount;</div></div></li><li><div class="hljs-ln-numbers"><div class="hljs-ln-line hljs-ln-n" data-line-number="13"></div></div><div class="hljs-ln-code"><div class="hljs-ln-line">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;pagesizes&nbsp;<span class="hljs-operator">=</span>&nbsp;pdf.PageSizes;</div></div></li><li><div class="hljs-ln-numbers"><div class="hljs-ln-line hljs-ln-n" data-line-number="14"></div></div><div class="hljs-ln-code"><div class="hljs-ln-line"> </div></div></li><li><div class="hljs-ln-numbers"><div class="hljs-ln-line hljs-ln-n" data-line-number="15"></div></div><div class="hljs-ln-code"><div class="hljs-ln-line">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="hljs-keyword">for</span>&nbsp;(int&nbsp;i&nbsp;<span class="hljs-operator">=</span>&nbsp;<span class="hljs-number">1</span>;&nbsp;i&nbsp;<span class="hljs-operator">&lt;=</span>&nbsp;pdfpage;&nbsp;i<span class="hljs-operator">+</span><span class="hljs-operator">+</span>)</div></div></li><li><div class="hljs-ln-numbers"><div class="hljs-ln-line hljs-ln-n" data-line-number="16"></div></div><div class="hljs-ln-code"><div class="hljs-ln-line">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{</div></div></li><li><div class="hljs-ln-numbers"><div class="hljs-ln-line hljs-ln-n" data-line-number="17"></div></div><div class="hljs-ln-code"><div class="hljs-ln-line">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="hljs-keyword">Size</span>&nbsp;<span class="hljs-keyword">size</span>&nbsp;<span class="hljs-operator">=</span>&nbsp;new&nbsp;<span class="hljs-keyword">Size</span>();</div></div></li><li><div class="hljs-ln-numbers"><div class="hljs-ln-line hljs-ln-n" data-line-number="18"></div></div><div class="hljs-ln-code"><div class="hljs-ln-line">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="hljs-keyword">size</span>.Height&nbsp;<span class="hljs-operator">=</span>&nbsp;(int)pagesizes[(i&nbsp;-&nbsp;<span class="hljs-number">1</span>)].Height;</div></div></li><li><div class="hljs-ln-numbers"><div class="hljs-ln-line hljs-ln-n" data-line-number="19"></div></div><div class="hljs-ln-code"><div class="hljs-ln-line">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="hljs-keyword">size</span>.Width&nbsp;<span class="hljs-operator">=</span>&nbsp;(int)pagesizes[(i&nbsp;-&nbsp;<span class="hljs-number">1</span>)].Width;</div></div></li><li><div class="hljs-ln-numbers"><div class="hljs-ln-line hljs-ln-n" data-line-number="20"></div></div><div class="hljs-ln-code"><div class="hljs-ln-line">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="hljs-operator">/</span><span class="hljs-operator">/</span>可以把<span class="hljs-string">".jpg"</span>写成其他形式</div></div></li><li><div class="hljs-ln-numbers"><div class="hljs-ln-line hljs-ln-n" data-line-number="21"></div></div><div class="hljs-ln-code"><div class="hljs-ln-line">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;RenderPage(filePath,&nbsp;i,&nbsp;<span class="hljs-keyword">size</span>,&nbsp;picPath);</div></div></li><li><div class="hljs-ln-numbers"><div class="hljs-ln-line hljs-ln-n" data-line-number="22"></div></div><div class="hljs-ln-code"><div class="hljs-ln-line">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}</div></div></li><li><div class="hljs-ln-numbers"><div class="hljs-ln-line hljs-ln-n" data-line-number="23"></div></div><div class="hljs-ln-code"><div class="hljs-ln-line"> </div></div></li><li><div class="hljs-ln-numbers"><div class="hljs-ln-line hljs-ln-n" data-line-number="24"></div></div><div class="hljs-ln-code"><div class="hljs-ln-line">&nbsp;&nbsp;&nbsp;&nbsp;}</div></div></li><li><div class="hljs-ln-numbers"><div class="hljs-ln-line hljs-ln-n" data-line-number="25"></div></div><div class="hljs-ln-code"><div class="hljs-ln-line"> </div></div></li><li><div class="hljs-ln-numbers"><div class="hljs-ln-line hljs-ln-n" data-line-number="26"></div></div><div class="hljs-ln-code"><div class="hljs-ln-line">&nbsp;&nbsp;&nbsp;&nbsp;private&nbsp;void&nbsp;RenderPage(<span class="hljs-keyword">string</span>&nbsp;pdfPath,&nbsp;int&nbsp;pageNumber,&nbsp;System.Drawing.<span class="hljs-keyword">Size</span>&nbsp;<span class="hljs-keyword">size</span>,&nbsp;<span class="hljs-keyword">string</span>&nbsp;outputPath,&nbsp;int&nbsp;dpi&nbsp;<span class="hljs-operator">=</span>&nbsp;<span class="hljs-number">300</span>)</div></div></li><li><div class="hljs-ln-numbers"><div class="hljs-ln-line hljs-ln-n" data-line-number="27"></div></div><div class="hljs-ln-code"><div class="hljs-ln-line">&nbsp;&nbsp;&nbsp;&nbsp;{</div></div></li><li><div class="hljs-ln-numbers"><div class="hljs-ln-line hljs-ln-n" data-line-number="28"></div></div><div class="hljs-ln-code"><div class="hljs-ln-line">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="hljs-keyword">using</span>&nbsp;(var&nbsp;document&nbsp;<span class="hljs-operator">=</span>&nbsp;PdfiumViewer.PdfDocument.Load(pdfPath))</div></div></li><li><div class="hljs-ln-numbers"><div class="hljs-ln-line hljs-ln-n" data-line-number="29"></div></div><div class="hljs-ln-code"><div class="hljs-ln-line">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="hljs-keyword">using</span>&nbsp;(var&nbsp;stream&nbsp;<span class="hljs-operator">=</span>&nbsp;new&nbsp;FileStream(outputPath,&nbsp;FileMode.Create))</div></div></li><li><div class="hljs-ln-numbers"><div class="hljs-ln-line hljs-ln-n" data-line-number="30"></div></div><div class="hljs-ln-code"><div class="hljs-ln-line">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="hljs-keyword">using</span>&nbsp;(var&nbsp;image&nbsp;<span class="hljs-operator">=</span>&nbsp;GetPageImage(pageNumber,&nbsp;<span class="hljs-keyword">size</span>,&nbsp;document,&nbsp;dpi))</div></div></li><li><div class="hljs-ln-numbers"><div class="hljs-ln-line hljs-ln-n" data-line-number="31"></div></div><div class="hljs-ln-code"><div class="hljs-ln-line">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{</div></div></li><li><div class="hljs-ln-numbers"><div class="hljs-ln-line hljs-ln-n" data-line-number="32"></div></div><div class="hljs-ln-code"><div class="hljs-ln-line">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;image.Save(stream,&nbsp;ImageFormat.Jpeg);</div></div></li><li><div class="hljs-ln-numbers"><div class="hljs-ln-line hljs-ln-n" data-line-number="33"></div></div><div class="hljs-ln-code"><div class="hljs-ln-line">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}</div></div></li><li><div class="hljs-ln-numbers"><div class="hljs-ln-line hljs-ln-n" data-line-number="34"></div></div><div class="hljs-ln-code"><div class="hljs-ln-line">&nbsp;&nbsp;&nbsp;&nbsp;}</div></div></li><li><div class="hljs-ln-numbers"><div class="hljs-ln-line hljs-ln-n" data-line-number="35"></div></div><div class="hljs-ln-code"><div class="hljs-ln-line">&nbsp;&nbsp;&nbsp;&nbsp;private&nbsp;static&nbsp;System.Drawing.Image&nbsp;GetPageImage(int&nbsp;pageNumber,&nbsp;<span class="hljs-keyword">Size</span>&nbsp;<span class="hljs-keyword">size</span>,&nbsp;PdfiumViewer.PdfDocument&nbsp;document,&nbsp;int&nbsp;dpi)</div></div></li><li><div class="hljs-ln-numbers"><div class="hljs-ln-line hljs-ln-n" data-line-number="36"></div></div><div class="hljs-ln-code"><div class="hljs-ln-line">&nbsp;&nbsp;&nbsp;&nbsp;{</div></div></li><li><div class="hljs-ln-numbers"><div class="hljs-ln-line hljs-ln-n" data-line-number="37"></div></div><div class="hljs-ln-code"><div class="hljs-ln-line">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="hljs-keyword">return</span>&nbsp;document.Render(pageNumber&nbsp;-&nbsp;<span class="hljs-number">1</span>,&nbsp;<span class="hljs-keyword">size</span>.Width,&nbsp;<span class="hljs-keyword">size</span>.Height,&nbsp;dpi,&nbsp;dpi,&nbsp;PdfRenderFlags.Annotations);</div></div></li><li><div class="hljs-ln-numbers"><div class="hljs-ln-line hljs-ln-n" data-line-number="38"></div></div><div class="hljs-ln-code"><div class="hljs-ln-line"> </div></div></li><li><div class="hljs-ln-numbers"><div class="hljs-ln-line hljs-ln-n" data-line-number="39"></div></div><div class="hljs-ln-code"><div class="hljs-ln-line">&nbsp;&nbsp;&nbsp;&nbsp;}</div></div></li><li><div class="hljs-ln-numbers"><div class="hljs-ln-line hljs-ln-n" data-line-number="40"></div></div><div class="hljs-ln-code"><div class="hljs-ln-line">}</div></div></li></ol></code>--%>
    </div>
         <div id="winpop">
        </div>
      <%--<div>
           <a href="http://120.52.185.14/default.aspx?action=slrzx">施工单位主要负责人、项目负责人、专职安全生产管理人员安全生产考核证书考核</a>
          <a href="http://120.52.185.14/rygate.aspx?action=slrkh">施工单位主要负责人、项目负责人、专职安全生产管理人员安全生产考核证书考核</a>

             <a href="http://localhost:7191/default.aspx?action=slrzx">施工单位主要负责人、项目负责人、专职安全生产管理人员安全生产考核证书考核</a>
          <a href="http://120.52.185.14/rygate.aspx?action=slrkh">施工单位主要负责人、项目负责人、专职安全生产管理人员安全生产考核证书考核</a>
      </div>--%>

        <%-- <div style="width: 99%; margin: 8px auto;">
                    <telerik:RadGrid ID="RadGrid1" runat="server"
                        GridLines="None" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" PageSize="15"
                        SortingSettings-SortToolTip="单击进行排序" Width="100%" Skin="Blue" EnableAjaxSkinRendering="False" EnableEmbeddedSkins="False" PagerStyle-AlwaysVisible="true">
                        <ClientSettings EnableRowHoverStyle="true">
                        </ClientSettings>
                        <HeaderContextMenu EnableEmbeddedSkins="False">
                        </HeaderContextMenu>
                        <MasterTableView NoMasterRecordsText=" 没有可显示的记录" CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Left"
                            DataKeyNames="">
                            <Columns>
                                <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="DataType" DataField="DataType" HeaderText="DataType">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="DataID" DataField="DataID" HeaderText="DataID">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                               
                                <telerik:GridBoundColumn UniqueName="DoTime" DataField="DoTime" HeaderText="申报日期" HtmlEncode="false" DataFormatString="{0:yyyy.MM.dd HH:mm:ss}">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn UniqueName="progress" HeaderText="评价">
                                    <ItemTemplate>
                                          <%# string.Format("<span onclick=\"window.open('./Appraisetemp.aspx?t={0}&o={1}'); \" style=\"cursor:pointer;color:blue\">评价</span>", Page.Server.UrlEncode(Utility.Cryptography.Encrypt(Eval("DataType").ToString())), Page.Server.UrlEncode(Utility.Cryptography.Encrypt(Eval("DataID").ToString())))%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
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
                    SelectMethod="GetList_temp_ping" TypeName="DataAccess.ApplyDAL"
                    SelectCountMethod="SelectCount_temp_ping" EnablePaging="true"
                    MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex" SortParameterName="orderBy">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                            DefaultValue="" ConvertEmptyStringToNull="false" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                </div>--%>
              
    </form>
</body>
</html>
