<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoginManager.ascx.cs"
    Inherits="CDManager_Dev4.Resources.UserControl.LoginManager" %>
<asp:Label ID="lblSetTime" runat="server" ForeColor="Maroon" Visible="False" Font-Bold="True" Font-Size="16px"></asp:Label><asp:Label ID="lblStatus" runat="server" ForeColor="Maroon" Text="[前台已关闭]"
    Visible="False" Font-Bold="True"></asp:Label>
<br />
<b>你的账号是:</b>
<asp:Label ID="lblLoginAccount" runat="server"></asp:Label>|
    <asp:HyperLink ID="linkEditAdmin" runat="server">编辑管理员信息</asp:HyperLink>|
    <asp:HyperLink ID="linkLoginMessage" runat="server" NavigateUrl="~/Management/Message/ReceivedMessage.aspx">消息箱</asp:HyperLink>|
    <asp:HyperLink ID="linkLoginFront" runat="server" NavigateUrl="~/Index.aspx">系统前台</asp:HyperLink>|
[<asp:LinkButton ID="linkLogout" runat="server" OnClick="linkLogout_Click">退出登录</asp:LinkButton>]

