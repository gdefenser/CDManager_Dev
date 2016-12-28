<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/Masters/Management.Master" AutoEventWireup="true" CodeBehind="FeedBackcheck.aspx.cs" Inherits="CDManager_Dev4.Management.Sys.FeedBackcheck" %>

<%@ Register Src="~/Resources/UserControl/CalendarExtender.ascx" TagPrefix="uc1" TagName="CalendarExtender" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    用户体验调查与意见反馈查看
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h3>用户体验调查与意见反馈查看</h3>
    <hr />
    <asp:EntityDataSource ID="edsMessage" runat="server" ConnectionString="name=CDManagerDevEntities"
        DefaultContainerName="CDManagerDevEntities" EnableFlattening="False" EntitySetName="Message" Where="it.YHLX='feedback'">
    </asp:EntityDataSource>
    <center>
        <asp:Panel ID="panelCalendar" runat="server" Visible="false">
            按时间过滤:从<uc1:CalendarExtender runat="server" ID="CalendarExtenderFrom" />
            到<uc1:CalendarExtender runat="server" ID="CalendarExtenderTo" />
            <asp:Button ID="btnSelect" runat="server" Text=" 查 询 " OnClick="btnSelect_Click" />
            <asp:HiddenField ID="hideWhere" runat="server" />
        </asp:Panel>
        <hr />
        <asp:UpdatePanel ID="upMessage" runat="server">
            <ContentTemplate>
                <asp:ListView ID="lvMessage" runat="server" DataKeyNames="XXTM" DataSourceID="edsMessage" OnItemDataBound="lvMessage_ItemDataBound" Visible="false">
                    <AlternatingItemTemplate>
                        <tr class="alternating">
                            <td>
                                <asp:Button ID="btnDetail" runat="server" Text="详细内容" CommandArgument='<%# Eval("XXTM") %>' OnCommand="btnDetail_Command" />
                            </td>
                            <td>
                                <asp:Label ID="lblXXNR" runat="server" Text='<%# Eval("XXNR") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblFSRTM" runat="server" Text='<%# Eval("FSRTM") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblFSSJ" runat="server" Text='<%# Eval("FSSJ") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblYD" runat="server" Text='<%# Eval("YD") %>' />
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                    <EmptyDataTemplate>
                        <table id="Table1" runat="server" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #5381AC; border-style: solid; border-width: 1px;">
                            <tr>
                                <td>没有用户体验调查与意见反馈</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <tr class="item">
                            <td>
                                <asp:Button ID="btnDetail" runat="server" Text="详细内容" CommandArgument='<%# Eval("XXTM") %>' OnCommand="btnDetail_Command" />
                            </td>
                            <td>
                                <asp:Label ID="lblXXNR" runat="server" Text='<%# Eval("XXNR") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblFSRTM" runat="server" Text='<%# Eval("FSRTM") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblFSSJ" runat="server" Text='<%# Eval("FSSJ") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblYD" runat="server" Text='<%# Eval("YD") %>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table id="Table2" runat="server" style="border: 1px solid #5381AC; border-collapse: collapse" cellpadding="0">
                            <tr id="Tr1" runat="server">
                                <td id="Td1" runat="server">
                                    <table id="itemPlaceholderContainer" runat="server" border="1" style="border-collapse: collapse; border-color: #999999; border-style: none;">
                                        <tr id="Tr2" runat="server" class="tableheader">
                                            <th id="Th0" runat="server"></th>
                                            <th id="Th1" runat="server">建议预览</th>
                                            <th id="Th2" runat="server">
                                                <asp:LinkButton ID="headerFSRTM" CommandArgument="FSRTM" CommandName="Sort" runat="server" ForeColor="White">发送人</asp:LinkButton></th>
                                            <th id="Th3" runat="server">
                                                <asp:LinkButton ID="headerFSSJ" CommandArgument="FSSJ" CommandName="Sort" runat="server" ForeColor="White">发送时间</asp:LinkButton></th>
                                            <th id="Th4" runat="server">
                                                <asp:LinkButton ID="linkYD" CommandArgument="YD" CommandName="Sort" runat="server" ForeColor="White">是否已读</asp:LinkButton></th>
                                        </tr>
                                        <tr id="itemPlaceholder" runat="server">
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="Tr3" runat="server">
                                <td id="Td2" runat="server" style="text-align: center; padding-top: 5px; padding-bottom: 5px">
                                    <asp:DataPager ID="dpMessage" runat="server" PageSize="25">
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
                <asp:Panel ID="panelDetail" runat="server" Visible="false">
                    <table border="1" cellspacing="0">
                        <tr>
                            <th>建议者:</th>
                            <td>
                                <asp:HyperLink ID="linkYH" runat="server"></asp:HyperLink></td>
                        </tr>
                        <tr>
                            <th colspan="2">建议内容</th>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblNR" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <th>时间:</th>
                            <td>
                                <asp:Label ID="lblFSSJ" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="panelMessage" runat="server" Visible="false">
                                    <asp:Button ID="btnMessage" runat="server" Text="发送消息" OnClick="btnMessage_Click" />
                                </asp:Panel>

                                <asp:HyperLink ID="linkBack" runat="server" NavigateUrl="~/Management/Sys/FeedBackcheck.aspx">返回</asp:HyperLink>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </center>
</asp:Content>
