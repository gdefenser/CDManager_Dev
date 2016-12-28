<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/Masters/NoticeShower.Master"
    AutoEventWireup="true" CodeBehind="BookDetail.aspx.cs" Inherits="CDManager_Dev4.BookDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    图书与光盘信息明细
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="Resources/Scripts/jquery-1.8.0.js">
    </script>
    <div id="detail">
        <h3>图书与光盘信息明细 -
            <asp:Label ID="lblTitle" runat="server"></asp:Label></h3>
        <hr />
        <input type="button" value="返回上一页" onclick="javascript: window.history.back(-1);" />
        <center>
            <div id="deContent">
                <table>
                    <tr>
                        <td class="decol">ISBN:
                        </td>
                        <td>
                            <asp:Label ID="lblISBN" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="decol">书名:
                        </td>
                        <td>
                            <asp:Label ID="lblZTM" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="decol">出版社:
                        </td>
                        <td>
                            <asp:HyperLink ID="linkCBS" runat="server">HyperLink</asp:HyperLink>

                        </td>
                    </tr>
                    <tr>
                        <td class="decol">作者:
                        </td>
                        <td>
                            <asp:HyperLink ID="linkZRZ" runat="server">HyperLink</asp:HyperLink>

                        </td>
                    </tr>
                    <tr>
                        <td class="decol">页码:
                        </td>
                        <td>
                            <asp:Label ID="lblYEMA" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="decol">下载数/申请数:
                        </td>
                        <td>
                            <asp:Label ID="lblDownload" runat="server"></asp:Label>/<asp:Label ID="lblApply" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="decol">光盘是否在线:
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblIsOnline" runat="server"></asp:Label>
                            <asp:Label ID="lblOnlineCount" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="decol">操作日期:
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblTime" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="decol" colspan="4" align="center">光盘下载
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="padding-top: 5px; width: 400px; height: 50px" align="center">
                            <asp:Panel ID="panelApply" runat="server" Visible="False" Font-Bold="True" BorderColor="#990000"
                                BorderStyle="Dotted" BorderWidth="1px">
                                此图书并没有上传光盘文件,若需要下载资源请先向管理员发出申请!<br />
                                <asp:Button ID="btnApplySubmit" runat="server" Text="提交申请" Visible="false" OnClick="btnApplySubmit_Click" />
                                <br />
                                <asp:Label ID="lblApplyed" runat="server" Text="你已经提交申请,请耐心等待管理员上传资源" Visible="False" Font-Size="Medium" ForeColor="Maroon"></asp:Label><br />
                                <asp:CustomValidator ID="valApplyed" runat="server" Display="Dynamic" ForeColor="Maroon" Font-Size="Small"></asp:CustomValidator>
                            </asp:Panel>
                            <asp:Panel ID="panelDownload" runat="server" Visible="False">
                            </asp:Panel>
                            <asp:Panel ID="panelUpload" runat="server" Visible="False" Font-Bold="True">
                                还没有上传该资源,请在后台中作上传操作!<asp:HyperLink ID="linkUpload" runat="server">点击上传</asp:HyperLink>
                            </asp:Panel>
                            <asp:Panel ID="panelNoLogin" runat="server" Visible="False" Font-Bold="True">
                                想下载该资源?赶快登录吧!<asp:HyperLink ID="linkLogin" runat="server" NavigateUrl="~/Account/Login.aspx">登录</asp:HyperLink>
                            </asp:Panel>
                            <asp:Panel ID="panelFTPShutDown" runat="server" Visible="False" Font-Bold="True">
                                很抱歉,FTP服务器已关闭并暂停下载
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </div>
        </center>
    </div>
</asp:Content>
