<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/Masters/Management.Master"
    AutoEventWireup="true" CodeBehind="NewBook.aspx.cs" Inherits="CDManager_Dev4.Management.BookManager.NewBook" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    新建图书
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        新建图书</h2>
    <hr />
    <center>
        <table class="input">
            <tr>
                <th class="decol">
                    *ISBN:
                </th>
                <td>
                    <asp:TextBox ID="txtISBN" runat="server"></asp:TextBox><br />
                    <asp:CustomValidator ID="valISBN" runat="server" Font-Bold="True" ForeColor="Maroon"
                        Display="Dynamic"></asp:CustomValidator>
                </td>
                <th class="decol">
                    *书目名称:
                </th>
                <td>
                    <asp:TextBox ID="txtZTM" runat="server"></asp:TextBox><br />
                    <asp:CustomValidator ID="valZTM" runat="server" Font-Bold="True" ForeColor="Maroon"
                        Display="Dynamic" ErrorMessage="请输入书目名称"></asp:CustomValidator>
                </td>
                <th class="decol">
                    作者:
                </th>
                <td>
                    <asp:TextBox ID="txtZRZ" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th class="decol">
                    出版社:
                </th>
                <td>
                    <asp:TextBox ID="txtCBS" runat="server"></asp:TextBox>
                </td>
                <th class="decol">
                    定价:
                </th>
                <td>
                    <asp:TextBox ID="txtDJ" runat="server"></asp:TextBox>
                </td>
                <th class="decol">
                    页码:
                </th>
                <td>
                    <asp:TextBox ID="txtYEMA" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th class="decol">
                    版次:
                </th>
                <td>
                    <asp:TextBox ID="txtYSBMY" runat="server"></asp:TextBox>
                </td>
                <th class="decol">
                    开本:
                </th>
                <td>
                    <asp:TextBox ID="txtKB" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="6" align="center">
                    <asp:Button ID="btnNew" runat="server" Text="  确  定  " OnClick="btnNew_Click" />
                    <asp:Button ID="btnReset" runat="server" Text="  重  置  " OnClick="btnReset_Click" />
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
