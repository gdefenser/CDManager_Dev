<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/Masters/Management.Master"
    AutoEventWireup="true" CodeBehind="EditBook.aspx.cs" Inherits="CDManager_Dev4.Management.BookManager.EditBook" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    编辑图书
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        编辑图书</h2>
    <hr />
    <center>
        <table class="input">
            <tr>
                <th class="decol">
                    ISBN:
                </th>
                <td>
                    <asp:Label ID="lblISBN" runat="server"></asp:Label>
                    <asp:TextBox ID="txtISBN" runat="server"></asp:TextBox>
                    <asp:CustomValidator ID="valISBN" runat="server" Font-Bold="True" ForeColor="Maroon"
                        Display="Dynamic" ErrorMessage="请输入ISBN"></asp:CustomValidator>
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
                    <asp:Button ID="btnEdit" runat="server" Text="  确  定  " 
                        onclick="btnEdit_Click" />
                    <asp:Button ID="btnReset" runat="server" Text="  重  置  " 
                        onclick="btnReset_Click"/>
                    <input type="button" value="返回上一页" onclick="javascript: window.history.back(-1);" />
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
