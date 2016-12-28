<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/Masters/Management.Master"
    AutoEventWireup="true" CodeBehind="NewAdmin.aspx.cs" Inherits="CDManager_Dev4.Management.User.AdminManager.NewAdmin" %>

<%@ Register Src="../../../Resources/UserControl/CalendarExtender.ascx" TagName="CalendarExtender"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    新建图书管理员用户
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        新建图书管理员用户</h2>
    <hr />
    <center>
        <table class="input">
            <tr>
                <th class="decol">
                    *登录账号:
                </th>
                <td>
                    <asp:TextBox ID="txtGLYTM" runat="server" MaxLength="15"></asp:TextBox><br />
                    <asp:CustomValidator ID="valGLYTM" runat="server" Font-Bold="True" 
                        ForeColor="Maroon" Display="Dynamic"></asp:CustomValidator>
                </td>
                <th class="decol">
                    *姓名:
                </th>
                <td>
                    <asp:TextBox ID="txtXM" runat="server" MaxLength="20"></asp:TextBox><br />
                    <asp:CustomValidator ID="valXM" runat="server" Font-Bold="True" ForeColor="Maroon" ErrorMessage="请输入姓名" Display="Dynamic"></asp:CustomValidator>
                </td>
                <th class="decol">
                    密码:
                </th>
                <td>
                    <asp:TextBox ID="txtMM" runat="server" MaxLength="20" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th class="decol">
                    性别:
                </th>
                <td align="left">
                    <asp:DropDownList ID="dropXB" runat="server">
                        <asp:ListItem>=选择性别=</asp:ListItem>
                        <asp:ListItem>男</asp:ListItem>
                        <asp:ListItem>女</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <th class="decol">
                    *有效日期:
                </th>
                <td>
                    <asp:DropDownList ID="dropYXRQ" runat="server">
                        <asp:ListItem>=选择有效日期=</asp:ListItem>
                        <asp:ListItem Value="3">3年</asp:ListItem>
                        <asp:ListItem Value="5">5年</asp:ListItem>
                        <asp:ListItem Value="10">10年</asp:ListItem>
                        <asp:ListItem Value="20">20年</asp:ListItem>
                        <asp:ListItem Value="30">30年</asp:ListItem>
                    </asp:DropDownList> <br />
                    <asp:CustomValidator ID="valYXRQ" runat="server" Font-Bold="True" ForeColor="Maroon" ErrorMessage="请选择有效日期" Display="Dynamic"></asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <th class="decol">
                    一级单位:
                </th>
                <td>
                    <asp:TextBox ID="txtYJDW" runat="server"></asp:TextBox>
                </td>
                <th class="decol">
                    二级单位:
                </th>
                <td>
                    <asp:TextBox ID="txtEJDW" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="6" align="center">
                    <asp:Button ID="btnNew" runat="server" Text="  确  定  " onclick="btnNew_Click" />
                    <asp:Button ID="btnReset" runat="server" Text="  重  置  " 
                        onclick="btnReset_Click" />
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
