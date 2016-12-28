<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/Masters/Management.Master"
    AutoEventWireup="true" CodeBehind="NoticeManagement.aspx.cs" Inherits="CDManager_Dev4.Management.Message.NoticeManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    公告管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:EntityDataSource ID="edsNotice" runat="server" ConnectionString="name=CDManagerDevEntities"
        DefaultContainerName="CDManagerDevEntities" EnableFlattening="False" EntitySetName="Notice">
    </asp:EntityDataSource>
    <center>
        <h3>公告管理</h3>
        <hr />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:ListView ID="lvNotice" runat="server" DataKeyNames="GGTM" DataSourceID="edsNotice" OnItemDataBound="lvNotice_ItemDataBound">
                    <AlternatingItemTemplate>
                        <tr class="alternating">
                            <td>
                                <asp:Button ID="btnDelete" runat="server" Text="删除" CommandArgument='<%# Eval("GGTM") %>' OnCommand="btnDelete_Command" />
                            </td>
                            <td>
                                <asp:HyperLink ID="linkGGBT" runat="server" NavigateUrl='<%# Eval("GGTM","~/Management/Message/EditNotice.aspx?GGTM={0}") %>' Text='<%# Eval("GGBT") %>'></asp:HyperLink>
                            </td>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("GGDX") %>' />
                            </td>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("DXLX") %>' />
                            </td>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("LKSJ") %>' />
                            </td>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("GLYTM") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblFJ" runat="server" Text='<%# Eval("FJ") %>'/>
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                    <EmptyDataTemplate>
                        <table id="Table1" runat="server" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #5381AC; border-style: solid; border-width: 1px;">
                            <tr>
                                <td>暂无公告或教程</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <tr class="item">
                            <td>
                                <asp:Button ID="btnDelete" runat="server" Text="删除" CommandArgument='<%# Eval("GGTM") %>' OnCommand="btnDelete_Command" />
                            </td>
                            <td>
                                <asp:HyperLink ID="linkGGBT" runat="server" NavigateUrl='<%# Eval("GGTM","~/Management/Message/EditNotice.aspx?GGTM={0}") %>' Text='<%# Eval("GGBT") %>'></asp:HyperLink>
                            </td>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("GGDX") %>' />
                            </td>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("DXLX") %>' />
                            </td>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("LKSJ") %>' />
                            </td>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("GLYTM") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblFJ" runat="server" Text='<%# Eval("FJ") %>'/>
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
                                                <asp:LinkButton ID="headerGGBT" CommandArgument="GGBT" CommandName="Sort" runat="server" ForeColor="White">公告名称</asp:LinkButton></th>
                                            <th id="Th3" runat="server">
                                                <asp:LinkButton ID="headerGGDX" CommandArgument="GGDX" CommandName="Sort" runat="server" ForeColor="White">公告对象</asp:LinkButton></th>
                                            <th id="Th4" runat="server">
                                                <asp:LinkButton ID="headerDXLX" CommandArgument="DXLX" CommandName="Sort" runat="server" ForeColor="White">对象类型</asp:LinkButton></th>
                                            <th id="Th5" runat="server"><asp:LinkButton ID="headerLKSJ" CommandArgument="LKSJ" CommandName="Sort" runat="server" ForeColor="White">落款时间</asp:LinkButton></th>
                                            <th id="Th6" runat="server"><asp:LinkButton ID="headerGLYTM" CommandArgument="GLYTM" CommandName="Sort" runat="server" ForeColor="White">发布人</asp:LinkButton></th>                                         
                                            <th id="Th7" runat="server">
                                                <asp:LinkButton ID="headerFJ" CommandArgument="FJ" CommandName="Sort" runat="server" ForeColor="White">是否含附件</asp:LinkButton></th>
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
