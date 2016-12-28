<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/Masters/Front.Master"
    AutoEventWireup="true" CodeBehind="SpecialLogin.aspx.cs" Inherits="CDManager_Dev4.Account.SpecialLogin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    前台已关闭
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="upLogin" runat="server">
        <ContentTemplate>
            <center style="padding-top: 200px; padding-bottom: 200px">
                <h1>系统已经关闭</h1>
                非管理员用户将暂时不能使用该系统,管理员用户请登录继续操作
        <div id="logon">
            <asp:Panel ID="panelNotice" runat="server" Visible="False">
                <hr style="width: 95%" />
                <div style="width: 700px; text-align: left;">
                    <center>
                        <h3>
                            <asp:Label ID="lblTitle" runat="server" Text="Label"></asp:Label></h3>
                    </center>
                    <div style="margin-right: 20%; margin-left: 20%">
                        <asp:Label ID="lblGGDX" runat="server" Text="Label"></asp:Label>:<br />
                        <p style="text-indent: 2em;">
                            <asp:Label ID="lblGGNR" runat="server" Text="Label"></asp:Label>
                        </p>
                        <asp:Panel ID="panelDownload" runat="server" Visible="false">
                            附件:<asp:HyperLink ID="linkDownload" runat="server"></asp:HyperLink>
                        </asp:Panel>
                        <div style="text-align: right">
                            <asp:Label ID="lblLKR" runat="server" Text="Label"></asp:Label><br />
                            <asp:Label ID="lblLKSJ" runat="server" Text="Label"></asp:Label>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <hr style="width: 95%" />
            <asp:CustomValidator ID="loginValidator" runat="server" Font-Bold="True" Font-Size="Small"
                ForeColor="Maroon" />
            <table id="logoninput">
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
                    <td colspan="2" align="center">
                        <asp:Button ID="btnLogin" runat="server" Text="登  录" OnClick="btnLogin_Click" />&nbsp;
                        <asp:Button ID="btnReset" runat="server" Text="重  置" OnClick="btnReset_Click" />
                        <br />
                        <asp:Label ID="lblURL" runat="server" Visible="false"></asp:Label>
                    </td>
                </tr>
            </table>
            <br />
        </div>
            </center>
            </center>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
