<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/Masters/Management.Master" AutoEventWireup="true" CodeBehind="BanReaderManagement.aspx.cs" Inherits="CDManager_Dev4.Management.Security.BanReaderManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    禁止读者登录
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h3>禁止读者登录</h3>
    <hr />
    <center>
        <table class="input">
            <tr>
                <th>读者条码:</th>
                <td>
                    <asp:TextBox ID="txtDZTM" runat="server"></asp:TextBox><br />
                    <asp:CustomValidator ID="valDZTM" runat="server" Font-Bold="True" ForeColor="Maroon"
                        Display="Dynamic"></asp:CustomValidator>
                </td>
                <th>禁止时间:
                </th>
                <td>
                    <asp:DropDownList ID="dropJSSJ" runat="server">
                        <asp:ListItem>=选择禁止时间=</asp:ListItem>
                        <asp:ListItem Value="1">一天</asp:ListItem>
                        <asp:ListItem Value="7">一周</asp:ListItem>
                        <asp:ListItem Value="30">一个月</asp:ListItem>
                        <asp:ListItem Value="150">一学期</asp:ListItem>
                        <asp:ListItem Value="99999">永久</asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    <asp:CustomValidator ID="valJSSJ" runat="server" Font-Bold="True" ForeColor="Maroon"
                        Display="Dynamic" ErrorMessage="请选择禁止时间"></asp:CustomValidator>
                </td>
                <td>
                    <asp:Button ID="btnBan" runat="server" Text="禁止读者登录" OnClick="btnBan_Click" />
                </td>
            </tr>
        </table>
    </center>
    <hr />
    <asp:EntityDataSource ID="edsReader" runat="server" ConnectionString="name=CDManagerDevEntities" DefaultContainerName="CDManagerDevEntities" EnableFlattening="False" EntitySetName="Reader"
        Where="it.YHZT = 0" OrderBy="it.DZTM" EnableUpdate="True"></asp:EntityDataSource>
    <h2>已禁止登录的读者</h2>
    <hr />
    <center>
        <asp:UpdatePanel ID="upReader" runat="server">
            <ContentTemplate>
                <asp:GridView ID="gvReader" runat="server" CellPadding="4" DataSourceID="edsReader"
                    ForeColor="#333333" GridLines="None" AllowPaging="True" AutoGenerateColumns="False"
                    BorderStyle="Solid" BorderWidth="1px" DataKeyNames="DZTM"
                    Font-Size="Small" AllowSorting="True"
                    PageSize="15" BorderColor="#999999" OnRowDataBound="gvReader_RowDataBound"
                    OnRowUpdating="gvReader_RowUpdating">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="取消禁止">
                            <ItemTemplate>
                                <asp:Button ID="btnDelete" runat="server" Text="取消禁止" CommandArgument='<%# Eval("DZTM") %>' OnCommand="btnDelete_Command"  />
                            </ItemTemplate>
                            <ItemStyle BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                        </asp:TemplateField>
                        <asp:CommandField ButtonType="Button" HeaderText="编辑" ShowEditButton="True">
                            <ItemStyle BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                        </asp:CommandField>
                        <asp:TemplateField HeaderText="禁止读者" SortExpression="DZTM">
                            <ItemTemplate>
                                <asp:HyperLink ID="linkDZTM" runat="server" Text='<%# Eval("DZTM") %>' NavigateUrl='<%# Eval("DZTM","/Management/User/ReaderManager/EditReader.aspx?DZTM={0}") %>'></asp:HyperLink>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:HiddenField ID="hideDZTM" runat="server" Value='<%# Eval("DZTM") %>' />
                                <asp:TextBox ID="txtEditDZTM" runat="server" Text='<%# Eval("DZTM") %>'></asp:TextBox><br />
                                <asp:CustomValidator ID="valEditDZTM" runat="server" Font-Bold="True" ForeColor="Maroon"
                                    Display="Dynamic"></asp:CustomValidator>
                            </EditItemTemplate>
                            <ItemStyle BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="KSSJ" HeaderText="开始时间" SortExpression="KSSJ" ReadOnly="true">
                            <ItemStyle BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="结束时间" SortExpression="JSSJ">
                            <ItemTemplate>
                                <asp:Label ID="lblJSSJ" runat="server" Text='<%# Eval("JSSJ") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:HiddenField ID="hideJSSJ" runat="server" Value='<%# Eval("JSSJ") %>' />
                                <asp:DropDownList ID="dropEditJSSJ" runat="server">
                                    <asp:ListItem>=选择禁止时间=</asp:ListItem>
                                    <asp:ListItem Value="1">一天</asp:ListItem>
                                    <asp:ListItem Value="7">一周</asp:ListItem>
                                    <asp:ListItem Value="30">一个月</asp:ListItem>
                                    <asp:ListItem Value="150">一学期</asp:ListItem>
                                    <asp:ListItem Value="99999">永久</asp:ListItem>
                                </asp:DropDownList>
                                <br />
                                <asp:CustomValidator ID="valEditJSSJ" runat="server" Font-Bold="True" ForeColor="Maroon"
                                    Display="Dynamic" ErrorMessage="请选择禁止时间"></asp:CustomValidator>
                            </EditItemTemplate>
                            <ItemStyle BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="JZCZY" HeaderText="操作人" SortExpression="JZCZY" ReadOnly="true">
                            <ItemStyle BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                        </asp:BoundField>
                    </Columns>
                    <EditRowStyle BackColor="#99CCFF" />
                    <EmptyDataTemplate>
                        没有读者被禁止登录
                    </EmptyDataTemplate>
                    <FooterStyle BackColor="#2461BF" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#1383DC" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#1383DC" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </center>
</asp:Content>
