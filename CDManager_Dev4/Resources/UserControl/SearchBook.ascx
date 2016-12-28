<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchBook.ascx.cs" Inherits="CDManager_Dev4.Resources.UserControl.SearchBook" %>
<asp:TextBox ID="txtKeyword" runat="server" Style="height: 28px; width: 366px" Font-Size="X-Large"></asp:TextBox>
<asp:Button ID="btnSearch" runat="server" Style="height: 28px" Text="开始检索" Font-Size="Large" OnClick="btnSearch_Click" />
<br />
<br />
<asp:Table ID="tableSearch" runat="server">
    <asp:TableRow>
        <asp:TableCell>
            <asp:RadioButton ID="rdoBook" runat="server" Text="查询图书" Checked="True" GroupName="Search" AutoPostBack="true" OnCheckedChanged="rdoBook_CheckedChanged" />
            <asp:RadioButton ID="rdoCD" runat="server" Text="查询光盘" GroupName="Search" AutoPostBack="true" OnCheckedChanged="rdoCD_CheckedChanged" />
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow ID="trBook">
        <asp:TableCell BorderWidth="1" BorderColor="#5381AC">
            <table>
                <tr>
                    <th>检索词:</th>
                    <td>
                        <asp:CheckBox ID="chkISBN" runat="server" Text="ISBN" /></td>
                    <td>
                        <asp:CheckBox ID="chkZTM" runat="server" Text="图书名称" />
                    </td>
                    <td>
                        <asp:CheckBox ID="chkCBS" runat="server" Text="出版社" />
                    </td>
                    <td>
                        <asp:CheckBox ID="chkZRZ" runat="server" Text="作者" />
                    </td>
                </tr>
                <tr>
                    <th>排 序:</th>
                    <td colspan="2">
                        <asp:DropDownList ID="dropSelect" runat="server">
                            <asp:ListItem>=选择排序方式=</asp:ListItem>
                            <asp:ListItem>ISBN</asp:ListItem>
                            <asp:ListItem>书名</asp:ListItem>
                            <asp:ListItem>出版社</asp:ListItem>
                            <asp:ListItem>作者</asp:ListItem>
                        </asp:DropDownList></td>
                    <td>
                        <asp:RadioButton ID="rdoUp" runat="server" Text="升序" GroupName="Select" /></td>
                    <td>
                        <asp:RadioButton ID="rdoDown" runat="server" Text="降序" GroupName="Select" /></td>
                </tr>
            </table>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow ID="trCD" Visible="false" BorderWidth="1">
        <asp:TableCell BorderWidth="1" BorderColor="#5381AC">
            <table>
                <tr>
                    <th>检索词:</th>
                    <td>
                        <asp:CheckBox ID="chkCDXH" runat="server" Text="光盘序号" /></td>
                    <td>
                        <asp:CheckBox ID="chkCDMC" runat="server" Text="光盘名称" /></td>
                    
                </tr>
                <tr>
                    <th>在线状态:</th>
                    <td><asp:DropDownList ID="dropZXZT" runat="server">
                        <asp:ListItem>全部</asp:ListItem>
                        <asp:ListItem>在线</asp:ListItem>
                        <asp:ListItem>不在线</asp:ListItem>
                    </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th>排 序:</th>
                    <td colspan="2">
                        <asp:DropDownList ID="dropSelectCD" runat="server">
                            <asp:ListItem>=选择排序方式=</asp:ListItem>
                            <asp:ListItem>光盘序号</asp:ListItem>
                            <asp:ListItem>光盘名称</asp:ListItem>
                            <asp:ListItem>在线状态</asp:ListItem>
                        </asp:DropDownList></td>
                    <td>
                        <asp:RadioButton ID="rdoCDUp" runat="server" Text="升序" GroupName="Select" /></td>
                    <td>
                        <asp:RadioButton ID="rdoCDDown" runat="server" Text="降序" GroupName="Select" /></td>
                </tr>
            </table>
        </asp:TableCell>
    </asp:TableRow>
</asp:Table>

