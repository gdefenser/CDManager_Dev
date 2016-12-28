<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/Masters/Management.Master"
    AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="CDManager_Dev4.Management.Main" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    广州大学华软软件学院图书馆光盘查询系统管理后台
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h3>
        <asp:Label ID="lblAdmin" runat="server"></asp:Label>你好,欢迎使用广州大学华软软件学院图书馆光盘管理系统管理后台</h3>
    <hr />
    <div id="backmain">
        <div id="apply">
            <table width="100%">
                <tr>
                    <td>
                        <table id="backtable" style="width: 100%">
                            <tr>
                                <td class="backitemtitle">
                                    未处理申请
                                </td>
                            </tr>
                            <tr>
                                <td class="backitem">
                                    目前有 <asp:HyperLink ID="linkApply" runat="server"></asp:HyperLink>&nbsp;条未处理申请,请及时处理
                                </td>
                            </tr>                           
                            <tr>
                                <td class="backitemtitle">
                                    在线光盘资源
                                </td>
                            </tr>
                            <tr>
                                <td class="backitem">
                                    目前有 <asp:HyperLink ID="linkOnline" runat="server" NavigateUrl="~/Management/CDManager/CDOnline.aspx"></asp:HyperLink> 个已经上传的光盘资源,请注意检查申请数和下载数,视情况处理在线光盘资源
                                </td>
                            </tr>
                            <tr>
                                <td class="backitemtitle">
                                    未确认光盘资源
                                </td>
                            </tr>
                            <tr>
                                <td class="backitem">
                                    目前有 <asp:HyperLink ID="linkUploaded" runat="server"></asp:HyperLink> 个待上传确认光盘资源,请注意及时处理
                                </td>
                            </tr>

                            <tr>
                                <td class="backitemtitle">
                                    管理员使用说明书
                                </td>
                            </tr>
                            <tr>
                                <td class="backitem">
                                    为了您能更好地使用本系统，请在初次使用时查看使用说明书，
                                    <a href="光盘系统管理员使用说明书.docx">点击下载</a>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="border: 1px solid #1074C9; width: 300px; vertical-align: top">
                        <table style="width: 100%">
                            <tr>
                                <td style="background-color: #1380D8; color: #FFFFFF; font-size: small;" align="center">
                                    管理员公告
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 200px;">
                                    <marquee direction="up" scrollamount="2" onmouseover="this.stop()" onmouseout="this.start()"
                                        style="height: 100%; width: 100%;">
                            <ul id="notice_link">
                            <asp:Panel ID="panelNotice" runat="server"></asp:Panel>                        
                            </ul></marquee>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
