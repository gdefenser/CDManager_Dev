<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/Masters/Management.Master" AutoEventWireup="true" CodeBehind="CDOnline.aspx.cs" Inherits="CDManager_Dev4.Management.CDManager.CDOnline" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    在线光盘资源管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:EntityDataSource ID="edsBook" runat="server" ConnectionString="name=CDManagerDevEntities"
        DefaultContainerName="CDManagerDevEntities" EnableFlattening="False" CommandText="select b.ZTM,b.ISBN from Book as b where b.ISBN in (select value distinct cd.Book.ISBN from CD as cd where cd.ZXZT=1)" OrderBy="it.ISBN">
    </asp:EntityDataSource>
    <h3>在线光盘资源管理</h3>
    <hr />
    <center>
        <asp:UpdatePanel ID="upBook" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <th>上传时间大于:</th>
                        <td>
                            <asp:DropDownList ID="dropUploaded" runat="server">
                                <asp:ListItem>全部</asp:ListItem>
                                <asp:ListItem Value="7">一周</asp:ListItem>
                                <asp:ListItem Value="14">二周</asp:ListItem>
                                <asp:ListItem Value="30">一个月</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>

                        <th>ISBN:</th>
                        <td>
                            <asp:TextBox ID="txtISBN" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <th>光盘序号:</th>
                        <td>
                            <asp:TextBox ID="txtCDXH" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Button ID="btnSelect" runat="server" Text=" 确 定 " OnClick="btnSelect_Click" /></td>
                    </tr>
                </table>
                <hr />
                <asp:ListView ID="lvBook" runat="server" DataKeyNames="ISBN" DataSourceID="edsBook" OnItemDataBound="lvBook_ItemDataBound">
                    <AlternatingItemTemplate>
                        <tr class="alternating">
                            <td>
                                <asp:Button ID="btnCDDetail" runat="server" Text="查看光盘明细" CommandArgument='<%# Eval("ISBN") %>' OnCommand="btnCDDetail_Command" />
                            </td>
                            <td>
                                <asp:Label ID="lblISBN" runat="server" Text='<%# Eval("ISBN") %>' />
                            </td>
                            <td>
                                <asp:HyperLink ID="linkZTM" runat="server" NavigateUrl='<%# Eval("ISBN","~/Management/BookManager/EditBook.aspx?ISBN={0}") %>' Text='<%# Eval("ZTM") %>'></asp:HyperLink>
                            </td>
                            <td>
                                <asp:Label ID="lblCount" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblOnlineCount" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblTime" runat="server" />
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                    <EmptyDataTemplate>
                        <table id="Table1" runat="server" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #5381AC; border-style: solid; border-width: 1px;">
                            <tr>
                                <td>没有在线光盘资源</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <tr class="item">
                            <td>
                                <asp:Button ID="btnCDDetail" runat="server" Text="查看光盘明细" CommandArgument='<%# Eval("ISBN") %>' OnCommand="btnCDDetail_Command" />
                            </td>
                            <td>
                                <asp:Label ID="lblISBN" runat="server" Text='<%# Eval("ISBN") %>' />
                            </td>
                            <td>
                                <asp:HyperLink ID="linkZTM" runat="server" NavigateUrl='<%# Eval("ISBN","~/Management/BookManager/EditBook.aspx?ISBN={0}") %>' Text='<%# Eval("ZTM") %>'></asp:HyperLink>
                            </td>
                            <td>
                                <asp:Label ID="lblCount" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblOnlineCount" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblTime" runat="server" />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table id="Table2" runat="server" style="border: 1px solid #5381AC; border-collapse: collapse" cellpadding="0">
                            <tr id="Tr1" runat="server">
                                <td id="Td1" runat="server">
                                    <table id="itemPlaceholderContainer" runat="server" border="1" style="border-collapse: collapse; border-color: #999999; border-style: none;">
                                        <tr id="Tr2" runat="server" class="tableheader">
                                            <th id="Th1" runat="server"></th>
                                            <th id="Th2" runat="server">
                                                <asp:LinkButton ID="headerISBN" CommandArgument="ISBN" CommandName="Sort" runat="server" ForeColor="White">ISBN</asp:LinkButton></th>
                                            <th id="Th3" runat="server">
                                                <asp:LinkButton ID="headerZTM" CommandArgument="ZTM" CommandName="Sort" runat="server" ForeColor="White">图书名称</asp:LinkButton></th>
                                            <th id="Th4" runat="server">光盘数量</th>
                                            <th id="Th5" runat="server">在线光盘数量</th>
                                            <th id="Th6" runat="server">最近操作时间</th>
                                        </tr>
                                        <tr id="itemPlaceholder" runat="server">
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="Tr3" runat="server">
                                <td id="Td2" runat="server" style="text-align: center; padding-top: 5px; padding-bottom: 5px">
                                    <asp:DataPager ID="dpBook" runat="server" PageSize="25">
                                        <Fields>
                                            <asp:TemplatePagerField>
                                                <PagerTemplate>
                                                    当前
                                            <asp:Label ID="lblPage" runat="server" ForeColor="Red" Text="<%# (Container.StartRowIndex/Container.PageSize)+1%>"></asp:Label>
                                                    /<asp:Label ID="lblPageCount" runat="server" Text="<%# Container.TotalRowCount<Container.PageSize?1:Container.TotalRowCount/Container.PageSize+1%>"></asp:Label>
                                                    页
                                                </PagerTemplate>
                                            </asp:TemplatePagerField>
                                            <asp:NextPreviousPagerField ButtonType="Link" ShowFirstPageButton="True" ShowNextPageButton="False"
                                                ShowPreviousPageButton="False" />
                                            <asp:NumericPagerField PreviousPageText="上十页" ButtonCount="10" NextPageText="下十页" />
                                            <asp:NextPreviousPagerField ButtonType="Link" ShowLastPageButton="True" ShowNextPageButton="False"
                                                ShowPreviousPageButton="False" />
                                            <asp:TemplatePagerField>
                                                <PagerTemplate>
                                                    跳转到第<asp:TextBox ID="txtPage" runat="server" Width="20px" Text="<%# (Container.StartRowIndex/Container.PageSize)+1%>" onkeypress="if (event.keyCode < 48 || event.keyCode >57) event.returnValue = false;" />页<asp:Button ID="btnGo" runat="server" Text="GO" OnCommand="btnGo_Command" />
                                                </PagerTemplate>
                                            </asp:TemplatePagerField>
                                        </Fields>
                                    </asp:DataPager>
                                </td>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </center>
</asp:Content>
