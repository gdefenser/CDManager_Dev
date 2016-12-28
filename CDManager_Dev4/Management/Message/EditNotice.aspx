<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/Masters/Management.Master" AutoEventWireup="true" CodeBehind="EditNotice.aspx.cs" Inherits="CDManager_Dev4.Management.Message.EditNotice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    公告明细与修改
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>明细与修改-
        <asp:Label ID="lblTitle" runat="server"></asp:Label></h2>
    <hr />
    <center>
        <table class="input">
            <tr>
                <th class="decol">公告标题:
                </th>
                <td align="left">
                    <asp:TextBox ID="txtGGBT" runat="server" Columns="40"></asp:TextBox><br />
                    <asp:CustomValidator ID="valGGBT" runat="server" Display="Dynamic" Font-Bold="True"
                        Font-Size="Small" ForeColor="Maroon"></asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <th class="decol">公告对象类型:
                </th>
                <td align="left">
                    <asp:DropDownList ID="dropDXLX" runat="server">
                        <asp:ListItem>=选择对象类型=</asp:ListItem>
                        <asp:ListItem>读者</asp:ListItem>
                        <asp:ListItem>管理员</asp:ListItem>
                        <asp:ListItem>使用教程</asp:ListItem>
                        <asp:ListItem>前台关闭公告</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th class="decol">公告对象:
                </th>
                <td align="left">
                    <asp:TextBox ID="txtGGDX" runat="server" Columns="30"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th class="decol" colspan="2">公告内容
                </th>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:CustomValidator ID="valGGNR" runat="server" Display="Dynamic" Font-Bold="True"
                        Font-Size="Small" ForeColor="Maroon"></asp:CustomValidator><br />
                    <asp:TextBox ID="txtGGNR" runat="server" Columns="60" MaxLength="3000" Rows="10"
                        TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th class="decol">附件:
                </th>
                <td align="left">
                    <asp:FileUpload ID="FileUpload" runat="server" Visible="false" />
                    <asp:HyperLink ID="linkDownload" runat="server" Visible="false" ></asp:HyperLink>
                    <asp:Button ID="btnReUpload" runat="server" Text="重新上传" Visible="false" OnClick="btnReUpload_Click" />
                </td>
            </tr>
            <tr>
                <th class="decol">落款人:
                </th>
                <td align="left">
                    <asp:TextBox ID="txtLKR" runat="server" Columns="30"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th class="decol">建立时间:
                </th>
                <td align="left">
                    <asp:Label ID="lblLKSJ" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="btnEdit" runat="server" Text="修  改" OnClick="btnEdit_Click" />
                    <asp:Button ID="btnReset" runat="server" Text="重  置"
                        OnClick="btnReset_Click" /><asp:Button ID="btnBack" runat="server" Text="返  回" OnClick="btnBack_Click" />
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
