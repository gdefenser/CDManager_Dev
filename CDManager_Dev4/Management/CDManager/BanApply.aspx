<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/Masters/Management.Master" AutoEventWireup="true" CodeBehind="BanApply.aspx.cs" Inherits="CDManager_Dev4.Management.CDManager.BanApply" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    光盘资源申请申请拒绝
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>光盘资源申请申请拒绝</h2>
    <input type="button" value="返回上一页" onclick="javascript: window.history.back(-1);" />
    <hr />
    <asp:EntityDataSource ID="edsApplyLogList" runat="server" ConnectionString="name=CDManagerDevEntities" DefaultContainerName="CDManagerDevEntities" EnableFlattening="False"
        CommandText="select a.ISBN,a.Book.ZTM,a.SQSJ,a.SQZT from ApplyLog as a where a.SQSJ in (select value MAX(a2.SQSJ) from ApplyLog as a2 group by a2.ISBN) and a.SQZT = 0" OrderBy="it.ISBN" Where="it.ISBN=@ISBN">
        <WhereParameters>
            <asp:QueryStringParameter Name="ISBN" QueryStringField="ISBN" DbType="String" />
        </WhereParameters>
    </asp:EntityDataSource>
    <center>
    <asp:UpdatePanel ID="upBanApply" runat="server">
        <ContentTemplate>
            <asp:GridView ID="gvApplyLog" runat="server" CellPadding="4" DataSourceID="edsApplyLogList"
                    ForeColor="#333333" GridLines="None" AutoGenerateColumns="False"
                    BorderStyle="Solid" BorderWidth="1px"
                    Font-Size="Small"
                    PageSize="15" BorderColor="#999999" OnRowDataBound="gvApplyLog_RowDataBound">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>                      
                        <asp:BoundField DataField="ISBN" HeaderText="ISBN" SortExpression="ISBN" ReadOnly="True">
                            <ItemStyle BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                        </asp:BoundField>
                        <asp:HyperLinkField DataNavigateUrlFields="ISBN" DataNavigateUrlFormatString="~//Management/BookManager/EditBook.aspx?ISBN={0}" SortExpression="ZTM" DataTextField="ZTM" HeaderText="图书名称">
                            <ItemStyle BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                        </asp:HyperLinkField>
                        <asp:TemplateField HeaderText="申请数">
                            <ItemTemplate>
                                <asp:HyperLink ID="linkCountApply" runat="server" NavigateUrl='<%# Eval("ISBN","~/Management/CDManager/ApplyLogList.aspx?ISBN={0}") %>'></asp:HyperLink>
                            </ItemTemplate>
                            <ItemStyle BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="SQSJ" HeaderText="最近申请时间" ReadOnly="True" SortExpression="SQSJ">
                            <ItemStyle BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                        </asp:BoundField>
                    </Columns>
                    <EditRowStyle BackColor="#99CCFF" />
                    <FooterStyle BackColor="#2461BF" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#1383DC" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#5381AC" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>
            <hr />
            <table class="input">
                <tr>
                    <th>拒绝理由</th>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList ID="dropMessage" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dropMessage_SelectedIndexChanged">
                            <asp:ListItem>==选择拒绝理由==</asp:ListItem>
                            <asp:ListItem>资源已经上传</asp:ListItem>
                            <asp:ListItem>服务器磁盘空间已满</asp:ListItem>
                            <asp:ListItem>系统需要维护</asp:ListItem>
                            <asp:ListItem>其他</asp:ListItem>
                        </asp:DropDownList><br />
                        <asp:TextBox ID="txtMessage" runat="server" Columns="35" Visible="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnClick" runat="server" Text=" 确 定 " OnClick="btnClick_Click" />
                    </td>
                </tr>                              
            </table>
        </ContentTemplate>
    </asp:UpdatePanel></center>
</asp:Content>
