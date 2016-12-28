<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoginFront.ascx.cs"
    Inherits="CDManager_Dev4.Resources.UserControl.LoginFront" %>
<asp:Panel ID="panelLogin" runat="server" Visible="false">
    <asp:Label ID="lblStatus" runat="server" ForeColor="Maroon" Text="[前台已关闭]"
        Visible="False" Font-Bold="True"></asp:Label><br />欢迎回来!<asp:Label ID="lblLoginTitle"
            runat="server"></asp:Label>(<asp:Label ID="lblLoginAccount" runat="server"></asp:Label>)&nbsp;|&nbsp;<asp:HyperLink ID="linkLoginMessage" runat="server" NavigateUrl="~/Account/Profile/MyMessage.aspx">消息箱</asp:HyperLink>&nbsp;|&nbsp;<asp:HyperLink ID="linkLoginProfile" runat="server" NavigateUrl="~/Account/Profile/MyApply.aspx" Visible="false">[个人信息管理]</asp:HyperLink>
    <asp:HyperLink ID="linkApplyLog" runat="server" NavigateUrl="~/Management/CDManager/ApplyLogList.aspx" Visible="false">[申请列表]</asp:HyperLink>
    <asp:HyperLink ID="linkLoginManager" runat="server" NavigateUrl="~/Management/Main.aspx"
        Visible="false">[系统管理后台]</asp:HyperLink>&nbsp;|&nbsp;[<asp:LinkButton ID="linkLogout" runat="server" OnClick="linkLogout_Click">退出登录</asp:LinkButton>]
</asp:Panel>
<asp:Panel ID="panelNoLogin" runat="server" Visible="false">
    [<asp:HyperLink ID="linkLogin" runat="server" NavigateUrl="~/Account/Login.aspx">登录</asp:HyperLink>]
</asp:Panel>
