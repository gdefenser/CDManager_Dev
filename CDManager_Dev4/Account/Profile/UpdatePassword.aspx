<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/Masters/Profile.master"
    AutoEventWireup="true" CodeBehind="UpdatePassword.aspx.cs" Inherits="CDManager_Dev4.Account.Profile.UpdatePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    修改密码
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h3>
        修改密码</h3>
    <hr />
    <table style="margin-left: 10px; margin-bottom: 10px;">
        <tr>
            <th>
                原密码:
            </th>
            <td>
                <asp:TextBox ID="txtOldPassword" runat="server" TextMode="Password"></asp:TextBox>
            </td>
            <td class="validation">
                <asp:CustomValidator ID="valOldPassword" runat="server"></asp:CustomValidator>
            </td>
        </tr>
        <tr>
            <th>
                新密码:
            </th>
            <td>
                <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password"></asp:TextBox>
            </td>
            <td class="validation">
                <asp:CustomValidator ID="valNewPassword" runat="server"></asp:CustomValidator>
            </td>
        </tr>
        <tr>
            <th>
                确认密码:
            </th>
            <td>
                <asp:TextBox ID="txtConfirmNewPassword" runat="server" TextMode="Password"></asp:TextBox>
            </td>
            <td class="validation">
                <asp:CustomValidator ID="valConfirmNewPassword" runat="server"></asp:CustomValidator>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="validation">
                <center>
                    <%--<%=Html.ValidationMessageFor(m=>m.Summary) %>--%>
                </center>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="btnClick" runat="server" Text="确  定" onclick="btnClick_Click" />
                <asp:Button ID="btnReset" runat="server" Text="重  写" onclick="btnReset_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
