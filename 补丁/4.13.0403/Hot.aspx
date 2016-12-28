<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/Masters/NoticeShower.Master"
    AutoEventWireup="true" CodeBehind="Hot.aspx.cs" Inherits="CDManager_Dev4.Hot" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    热门下载
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:SqlDataSource ID="sdsDownloadLog" runat="server" ConnectionString="<%$ ConnectionStrings:CDManagerDevConnectionString %>" 
        SelectCommand="SELECT b.ZTM,b.ISBN,b.CBS,b.ZRZ,b.FLTM,count(d.CDID) as mycount from DownloadLog as d 
join CD as cd on d.CDID=cd.CDID 
join Book as b on 
b.BookID=cd.BookID 
group by b.ZTM,b.ISBN,b.CBS,b.ZRZ,b.FLTM order by mycount desc"></asp:SqlDataSource>
    <center style="height: 700px; margin-top: 50px;">
        <br />
        <asp:UpdatePanel ID="upBook" runat="server">
            <ContentTemplate>
                <asp:ListView ID="lvBook" runat="server"  DataSourceID="sdsDownloadLog" OnItemDataBound="lvBook_ItemDataBound">
                    <AlternatingItemTemplate>
                        <tr class="alternating">
                            <td>
                                <asp:Label ID="ISBNLabel" runat="server" Text='<%# Eval("ISBN") %>' />
                            </td>
                            <td>
                                <asp:HyperLink ID="linkZTM" runat="server" NavigateUrl='<%# Eval("ISBN","~/BookDetail.aspx?ISBN={0}") %>' Text='<%# Eval("ZTM") %>'></asp:HyperLink>
                            </td>
                            <td>
                                <asp:Label ID="CBSLabel" runat="server" Text='<%# Eval("CBS") %>' />
                            </td>
                            <td>
                                <asp:Label ID="ZRZLabel" runat="server" Text='<%# Eval("ZRZ") %>' />
                            </td>
                            <td>
                                <asp:Label ID="FLTMLabel" runat="server" Text='<%# Eval("FLTM") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblDownloadApply" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblIsOnline" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="FFSJLabel" runat="server" />
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                    <EmptyDataTemplate>
                        <table id="Table1" runat="server" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #5381AC; border-style: solid; border-width: 1px;">
                            <tr>
                                <td>没有热门下载列表</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        
                        <tr class="item">
                            <td>
                                <asp:Label ID="ISBNLabel" runat="server" Text='<%# Eval("ISBN") %>' />
                            </td>
                            <td>
                                <asp:HyperLink ID="linkZTM" runat="server" NavigateUrl='<%# Eval("ISBN","~/BookDetail.aspx?ISBN={0}") %>' Text='<%# Eval("ZTM") %>'></asp:HyperLink>
                            </td>
                            <td>
                                <asp:Label ID="CBSLabel" runat="server" Text='<%# Eval("CBS") %>' />
                            </td>
                            <td>
                                <asp:Label ID="ZRZLabel" runat="server" Text='<%# Eval("ZRZ") %>' />
                            </td>
                            <td>
                                <asp:Label ID="FLTMLabel" runat="server" Text='<%# Eval("FLTM") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblDownloadApply" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblIsOnline" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="FFSJLabel" runat="server" />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table id="Table2" runat="server" style="border: 1px solid #5381AC; border-collapse: collapse" cellpadding="0">
                            <tr id="Tr1" runat="server">
                                <td id="Td1" runat="server">
                                    <table id="itemPlaceholderContainer" runat="server" border="1" style="border-collapse: collapse; border-color: #999999; border-style: none;">
                                        <tr id="Tr2" runat="server" class="tableheader" >
                                            <th id="Th1" runat="server">
                                                <asp:LinkButton ID="headerISBN" CommandArgument="ISBN" CommandName="Sort" runat="server" ForeColor="White">ISBN</asp:LinkButton></th>
                                            <th id="Th2" runat="server">
                                                <asp:LinkButton ID="headerZTM" CommandArgument="ZTM" CommandName="Sort" runat="server" ForeColor="White">图书名称</asp:LinkButton></th>
                                            <th id="Th3" runat="server">出版社</th>
                                            <th id="Th4" runat="server">作者</th>
                                            <th id="Th5" runat="server">分类</th>
                                            <th id="Th6" runat="server"><asp:LinkButton ID="headerDownload" runat="server" CommandArgument="mycount" CommandName="Sort" ForeColor="White">下载数</asp:LinkButton>/申请数</th>
                                            <th id="Th7" runat="server">光盘资源状态</th>
                                            <th id="Th8" runat="server">
                                                <asp:LinkButton ID="headerFFSJ" CommandArgument="ZTM" CommandName="Sort" runat="server" ForeColor="White">光盘操作时间</asp:LinkButton></th>
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
                                                    /<asp:Label ID="lblPageCount" runat="server" Text="<%# Container.TotalRowCount<Container.PageSize?1:Container.TotalRowCount/Container.PageSize%>"></asp:Label>
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
