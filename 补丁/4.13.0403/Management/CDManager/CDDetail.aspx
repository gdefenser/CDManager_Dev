<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/Masters/Management.Master" AutoEventWireup="true" CodeBehind="CDDetail.aspx.cs" Inherits="CDManager_Dev4.Management.CDManager.CDDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    光盘管理明细
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>光盘管理明细</h2>
    <input type="button" value="返回上一页" onclick="javascript: window.history.back(-1);" />
    <hr />
    <center>
        <h3>图书信息</h3>
        <asp:UpdatePanel ID="upCD" runat="server">
            <ContentTemplate>
                <hr />
                <asp:EntityDataSource ID="edsBook" runat="server" ConnectionString="name=CDManagerDevEntities" DefaultContainerName="CDManagerDevEntities" EnableFlattening="False" EntitySetName="Book" Where="it.ISBN=@ISBN">
                    <WhereParameters>
                        <asp:QueryStringParameter Name="ISBN" QueryStringField="ISBN" DbType="String" />
                    </WhereParameters>
                </asp:EntityDataSource>
                <asp:GridView ID="gvBook" runat="server" CellPadding="4" DataSourceID="edsBook"
                    ForeColor="#333333" GridLines="None" AllowPaging="True" AutoGenerateColumns="False"
                    BorderStyle="Solid" BorderWidth="1px" DataKeyNames="BookID"
                    Font-Size="Small" AllowSorting="True"
                    PageSize="15" BorderColor="#999999" OnRowDataBound="gvBook_RowDataBound">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="ISBN" HeaderText="ISBN" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle HorizontalAlign="Center" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"></ItemStyle>
                        </asp:BoundField>
                        <asp:HyperLinkField DataNavigateUrlFields="ISBN" DataNavigateUrlFormatString="~/Management/BookManager/EditBook.aspx?ISBN={0}"
                            DataTextField="ZTM" HeaderText="图书名称" ItemStyle-HorizontalAlign="Center">
                            <HeaderStyle Width="35%" />

                            <ItemStyle HorizontalAlign="Center" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"></ItemStyle>
                        </asp:HyperLinkField>
                        <asp:BoundField DataField="CBS" HeaderText="出版社" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle HorizontalAlign="Center" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="ZRZ" HeaderText="作者" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle HorizontalAlign="Center" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="申请数" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblApply" runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="下载总数" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblDownload" runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="光盘数量" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblCDCount" runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="在线光盘数量" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblOnlineCount" runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"></ItemStyle>
                        </asp:TemplateField>
                    </Columns>
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
                <hr />
                <asp:EntityDataSource ID="edsCD" runat="server" ConnectionString="name=CDManagerDevEntities" DefaultContainerName="CDManagerDevEntities" EnableDelete="True" EnableFlattening="False" EnableInsert="True" EnableUpdate="True" EntitySetName="CD" EntityTypeFilter="" Select="" Where="it.Book.ISBN=@ISBN">
                    <WhereParameters>
                        <asp:QueryStringParameter Name="ISBN" QueryStringField="ISBN" DbType="String" />
                    </WhereParameters>
                </asp:EntityDataSource>
                <h3>光盘信息</h3>
                <hr />
                <asp:Button ID="btnNew" runat="server" Text="添加光盘信息" OnClick="btnNew_Click" />
                <asp:Panel ID="panelCD" runat="server" Visible="false">
                    <table>
                        <tr>
                            <th align="right">光盘名称:</th>
                            <td>
                                <asp:TextBox ID="txtCDMC" runat="server" Width="300px"></asp:TextBox>
                                <br />
                                <asp:CustomValidator ID="valCDMC" runat="server" ErrorMessage="请输入光盘名称" Font-Bold="True" ForeColor="Maroon"
                                    Display="Dynamic"></asp:CustomValidator>
                            </td>
                            <td>
                                <asp:Button ID="btnNewCD" runat="server" Text="确认添加" OnClick="btnNewCD_Click" /></td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:GridView ID="gvCD" runat="server" CellPadding="4" DataSourceID="edsCD"
                    ForeColor="#333333" GridLines="None" AllowPaging="True" AutoGenerateColumns="False"
                    BorderStyle="Solid" BorderWidth="1px" DataKeyNames="CDID"
                    Font-Size="Small" AllowSorting="True"
                    PageSize="15" BorderColor="#999999" OnRowDataBound="gvCD_RowDataBound" OnRowUpdating="gvCD_RowUpdating">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="光盘记录">
                            <ItemTemplate>
                                <asp:Button ID="btnDelete" runat="server" Text="删除" CommandArgument='<%# Eval("CDXH") %>' OnCommand="btnDelete_Command" />
                            </ItemTemplate>
                            <ItemStyle BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="FTP操作">
                            <ItemTemplate>
                                <asp:Button ID="btnFTPUpload" runat="server" Text="确认上传" CommandArgument='<%# Eval("CDXH") %>' OnCommand="btnFTPUpload_Command" Visible="false" />
                                <asp:Button ID="btnFTPDelete" runat="server" Text="从目录删除" CommandArgument='<%# Eval("CDXH") %>' OnCommand="btnFTPDelete_Command" Visible="false" />
                            </ItemTemplate>
                            <ItemStyle BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                        </asp:TemplateField>
                        <asp:CommandField ShowEditButton="True" HeaderText="编辑">
                            <ItemStyle BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                        </asp:CommandField>
                        <asp:TemplateField HeaderText="光盘序号">
                            <ItemTemplate>
                                <asp:Label ID="lblCDXH" runat="server" Text='<%# Eval("CDXH") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:HiddenField ID="hideCDID" runat="server" Value='<%# Eval("CDID") %>' />

                                <asp:TextBox ID="txtEditCDXH" runat="server" Text='<%# Eval("CDXH") %>' Width="80px"></asp:TextBox><br />
                                <asp:CustomValidator ID="valEditCDXH" runat="server" ErrorMessage="请输入光盘序号" Font-Bold="True" ForeColor="Maroon"
                                    Display="Dynamic"></asp:CustomValidator>
                            </EditItemTemplate>
                            <ItemStyle BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="光盘名称">
                            <ItemTemplate>
                                <asp:Label ID="lblCDMC" runat="server" Text='<%# Eval("CDMC") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEditCDMC" runat="server" Text='<%# Eval("CDMC") %>' Width="300px"></asp:TextBox><br />
                                <asp:CustomValidator ID="valEditCDMC" runat="server" ErrorMessage="请输入光盘名称" Font-Bold="True" ForeColor="Maroon"
                                    Display="Dynamic"></asp:CustomValidator>
                            </EditItemTemplate>
                            <ItemStyle BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="CDGS" HeaderText="光盘格式" ItemStyle-HorizontalAlign="Center" ReadOnly="true">
                            <ItemStyle HorizontalAlign="Center" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="CDDX" HeaderText="光盘大小" ItemStyle-HorizontalAlign="Center" SortExpression="CDDX" ReadOnly="true">
                            <ItemStyle HorizontalAlign="Center" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="ZXZT" HeaderText="在线状态" ItemStyle-HorizontalAlign="Center" SortExpression="ZXZT" ReadOnly="true">
                            <ItemStyle HorizontalAlign="Center" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="下载数">
                            <ItemTemplate>
                                <asp:HyperLink ID="linkDownloadCount" runat="server" NavigateUrl='<%# Eval("CDXH","~/Management/CDManager/CDDownloadDetail.aspx?CDXH={0}") %>'></asp:HyperLink>
                            </ItemTemplate>
                            <ItemStyle BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="CZSJ" HeaderText="操作时间" ItemStyle-HorizontalAlign="Center" SortExpression="CZSJ" ReadOnly="true">
                            <ItemStyle HorizontalAlign="Center" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"></ItemStyle>
                        </asp:BoundField>
                        <asp:HyperLinkField DataNavigateUrlFields="CZCZY" DataNavigateUrlFormatString="~/Management/User/AdminManager/AdminStatistics.aspx?GLYTM={0}"
                            DataTextField="CZCZY" HeaderText="操作管理员">
                            <ItemStyle BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                        </asp:HyperLinkField>
                    </Columns>
                    <EditRowStyle BackColor="#99CCFF" />
                    <EmptyDataTemplate>
                        没有光盘信息
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
