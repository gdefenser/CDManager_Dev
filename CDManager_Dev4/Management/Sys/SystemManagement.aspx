<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/Masters/Management.Master"
    AutoEventWireup="true" CodeBehind="SystemManagement.aspx.cs" Inherits="CDManager_Dev4.Management.Sys.SystemManagement" %>

<%@ Register Src="../../Resources/UserControl/TimePicker.ascx" TagName="TimePicker" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    系统信息与维护
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h3>系统信息与维护</h3>
    <hr />
    <div>
        <table id="backtable">
            <tr>
                <td class="backitemtitle">前台状态:<asp:Label ID="lblStatus" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <b>当需要对系统或界面作出重大更改时,可与后台快捷关闭系统,届时匿名用户与普通用户访问系统时,将提示系统维护信息,但此时不限制图书管理员和系统管理员登录</b>
                    <br />

                    状态操作:
                    <br />
                    <asp:Label ID="lblSetTime" runat="server" Font-Bold="True" ForeColor="#990000"></asp:Label>
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="btnStatus" runat="server" OnClick="btnStatus_Click" Visible="false" /></td>
                            <td>
                                <asp:Button ID="btnResetTime" runat="server" Visible="false" Text="取消定时关闭" OnClick="btnResetTime_Click" />
                            </td>
                            <td>
                                <asp:Button ID="btnSetTime" runat="server" Text="定时关闭" OnClick="btnSetTime_Click" Visible="false" /></td>
                            <td>
                                <uc1:TimePicker ID="timePicker" runat="server" Visible="false" />
                            </td>
                            <td>
                                <asp:Button ID="btnClick" runat="server" Text="设定关闭时间" Visible="false" OnClick="btnClick_Click" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="backitemtitle">最大下载读者数
                </td>
            </tr>
            <tr>
                <td>
                    <b>FTP读者用户的并发下载数</b><br />
                    <asp:UpdatePanel ID="UpdatePanel" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txtMaxUser" runat="server" Columns="3"></asp:TextBox><asp:Button ID="btnSetUser" runat="server" Text="确定" OnClick="btnSetUser_Click" /><asp:Button ID="btnReSetUser" runat="server" Text="重置" Height="21px" OnClick="btnReSetUser_Click" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="backitemtitle">系统信息
                </td>
            </tr>
            <tr>
                <td class="backitem">
                    <b>系统名称 : </b>广州大学华软软件学院图书馆光盘查询系统
                    <hr />
                    <b>建项日期 : </b>2012/3/9
                    <hr />
                    <b>系统版本 : </b>4.0                    
                    <hr />
                    <b>版本日期 : </b>2013/1/21
                    <hr />
                    <b>开发语言 : </b>ASP.NET(C#)4.0
                    <hr />
                    <b>数据库 : </b>Microsoft SQL Sever 2008
                </td>
            </tr>
            <tr>
                <td class="backitemtitle">开发团队
                </td>
            </tr>
            <tr>
                <td class="backitem">
                    <b>指导老师 : </b>软件工程系 林平荣 老师
                    <hr />
                    <b>实现人员 : </b>软件工程系 黄煜祺 赵政 连伟江
                </td>
            </tr>
            <tr>
                <td class="backitemtitle">维护团队
                </td>
            </tr>
            <tr>
                <td class="backitem">
                    <b>总负责人 : </b>林平荣 老师<hr />
                    <b>维护人 : </b>黄煜祺 赵政 连伟江<br />
                </td>
            </tr>
        </table>
    </div>

</asp:Content>
