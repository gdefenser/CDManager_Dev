<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/Masters/Front.Master"
    AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CDManager_Dev4.Account.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    系统登录
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="upLogin" runat="server">
        <ContentTemplate>
            <center style="padding-top: 250px; padding-bottom: 250px">
                <asp:CustomValidator ID="loginValidator" runat="server" Font-Bold="True" Font-Size="Small"
                    ForeColor="Maroon" />
                <table id="logoninput" style="">
                    <tr>
                        <td colspan="2" align="left">
                            <ul>
                                <li>登录账号为10位学生学号或4位职工工号</li>
                                <li>初始密码为4个0</li>
                                <li>用户首次登录后请尽快修改密码</li>
                            </ul>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">用户名:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtUsername" runat="server" Columns="19"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="usernameValidator" runat="server" ControlToValidate="txtUsername"
                                ErrorMessage="请输入用户名" Font-Bold="True" Font-Size="Small" ForeColor="Maroon" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">密 码:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                        </td>
                        <td colspan="2">
                            <asp:RequiredFieldValidator ID="passwordValidator" runat="server" ControlToValidate="txtUsername"
                                ErrorMessage="请输入密码" Font-Bold="True" Font-Size="Small" ForeColor="Maroon" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <center>
                                <asp:Button ID="btnLogin" runat="server" Text="登录" OnClick="btnLogin_Click" />&nbsp;
                        <input id="Reset" type="reset" value="重置" />
                                <br />
                            </center>
                        </td>
                    </tr>
                </table>
            </center>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
