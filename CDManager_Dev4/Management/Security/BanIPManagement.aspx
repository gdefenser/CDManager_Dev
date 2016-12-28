<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/Masters/Management.Master"
    AutoEventWireup="true" CodeBehind="BanIPManagement.aspx.cs" Inherits="CDManager_Dev4.Management.Security.BanIPManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    禁止IP
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        禁止IP</h2>
    <hr />
    <center>
        <table class="input">
            <tr>
                <th>
                    禁止IP:
                </th>
                <td>
                    <asp:TextBox ID="txtJZIP" runat="server"></asp:TextBox><br />
                    <asp:CustomValidator ID="valJZIP" runat="server" Font-Bold="True" ForeColor="Maroon"
                        Display="Dynamic"></asp:CustomValidator>
                </td>
                <th>
                    禁止时间:
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
                    <asp:Button ID="btnBan" runat="server" Text="  禁止IP  " OnClick="btnBan_Click" />
                </td>
            </tr>
        </table>
    </center>
    <hr />
    <asp:EntityDataSource ID="edsBanIP" runat="server" ConnectionString="name=CDManagerDevEntities"
        DefaultContainerName="CDManagerDevEntities" EnableFlattening="False" EnableUpdate="True"
        EntitySetName="BanIP">
    </asp:EntityDataSource>
    <h2>
        已禁止IP</h2>
    <hr />
    <center>
        <asp:UpdatePanel ID="upBanIP" runat="server">
            <ContentTemplate>
                <asp:GridView ID="gvBanIP" runat="server" CellPadding="4" DataSourceID="edsBanIP"
                    ForeColor="#333333" GridLines="None" AllowPaging="True" AutoGenerateColumns="False"
                    BorderStyle="Solid" BorderWidth="1px" DataKeyNames="IPTM" 
                    Font-Size="Small" AllowSorting="True"
                    PageSize="15" BorderColor="#999999" onrowdatabound="gvBanIP_RowDataBound" 
                    onrowupdating="gvBanIP_RowUpdating">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="删除">
                            <ItemTemplate>
                                <asp:Button ID="btnDelete" runat="server" Text="删除" CommandArgument='<%# Eval("IPTM") %>' OnCommand="btnDelete_Command" />
                            </ItemTemplate>
                            <ItemStyle BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                        </asp:TemplateField>
                        <asp:CommandField ButtonType="Button" HeaderText="编辑" ShowEditButton="True">
                            <ItemStyle BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                        </asp:CommandField>
                        <asp:TemplateField HeaderText="禁止IP" SortExpression="JZIP">
                            <ItemTemplate>
                                <asp:Label ID="lblJZIP" runat="server" Text='<%# Eval("JZIP") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                            <asp:HiddenField ID="hideIPTM" runat="server" Value='<%# Eval("IPTM") %>'/>
                                <asp:TextBox ID="txtEditJZIP" runat="server" Text='<%# Eval("JZIP") %>'></asp:TextBox><br />
                                <asp:CustomValidator ID="valEditJZIP" runat="server" Font-Bold="True" ForeColor="Maroon"
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
                                <asp:HiddenField ID="hideJSSJ" runat="server" Value='<%# Eval("JSSJ") %>'/>
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
                    <EmptyDataTemplate>
                        没有IP被禁止登录
                    </EmptyDataTemplate>
                    <EditRowStyle BackColor="#99CCFF" />
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
