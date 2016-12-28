<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/Masters/Management.Master"
    AutoEventWireup="true" CodeBehind="EditAdmin.aspx.cs" Inherits="CDManager_Dev4.Management.User.AdminManager.EditAdmin" %>

<%@ Register Src="~/Resources/UserControl/CalendarExtender.ascx" TagPrefix="uc1" TagName="CalendarExtender" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    编辑管理员
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>编辑管理员</h2>
    <hr />
    <center>
        <table class="input">
            <tr>
                <th class="decol">登录账号:
                </th>
                <td>
                    <asp:Label ID="lblGLYTM" runat="server"></asp:Label>
                </td>
                <th class="decol">*姓名:
                </th>
                <td>
                    <asp:TextBox ID="txtXM" runat="server" MaxLength="20"></asp:TextBox><br />
                    <asp:CustomValidator ID="valXM" runat="server" Font-Bold="True" ForeColor="Maroon"
                        ErrorMessage="请输入姓名" Display="Dynamic"></asp:CustomValidator>
                </td>
                <th class="decol">密码:
                </th>
                <td>
                    <asp:HyperLink ID="linkMM" runat="server" NavigateUrl="~/Account/Profile/UpdatePassword.aspx">修改密码</asp:HyperLink>
                    <asp:TextBox ID="txtMM" runat="server" Visible="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th class="decol">性别:
                </th>
                <td align="left">
                    <asp:DropDownList ID="dropXB" runat="server">
                        <asp:ListItem>=选择性别=</asp:ListItem>
                        <asp:ListItem>男</asp:ListItem>
                        <asp:ListItem>女</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <th class="decol">*有效日期:
                </th>
                <td>
                    <uc1:CalendarExtender runat="server" ID="CalendarExtenderYXRQ" />
                    <br />
                    <asp:CustomValidator ID="valYXRQ" runat="server" Font-Bold="True" ForeColor="Maroon"
                        ErrorMessage="请选择有效日期" Display="Dynamic"></asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <th class="decol">一级单位:
                </th>
                <td>
                    <asp:TextBox ID="txtYJDW" runat="server"></asp:TextBox>
                </td>
                <th class="decol">二级单位:
                </th>
                <td>
                    <asp:TextBox ID="txtEJDW" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="6" align="center">
                    <asp:Button ID="btnStatistics" runat="server" Text="查看管理员管理统计" OnClick="btnStatistics_Click" /><br />
                    <asp:Button ID="btnEdit" runat="server" Text="  确  定  "
                        OnClick="btnEdit_Click" />
                    <asp:Button ID="btnReset" runat="server" Text="  重  置  "
                        OnClick="btnReset_Click" />
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
