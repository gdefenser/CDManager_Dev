<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/Masters/Management.Master" AutoEventWireup="true" CodeBehind="ApplyLogList.aspx.cs" Inherits="CDManager_Dev4.Management.CDManager.ApplyLogList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    光盘资源申请处理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:EntityDataSource ID="edsApplyLogList" runat="server" ConnectionString="name=CDManagerDevEntities" DefaultContainerName="CDManagerDevEntities" EnableFlattening="False"
        CommandText="select a.Book.ISBN,a.Book.ZTM,a.SQSJ,a.SQZT,a.IP from ApplyLog as a where a.SQSJ in (select value MAX(a2.SQSJ) from ApplyLog as a2 group by a2.Book.ISBN) and a.SQZT = 0" OrderBy="it.SQSJ">
    </asp:EntityDataSource>
    <asp:EntityDataSource ID="edsApplyLogDetail" runat="server" ConnectionString="name=CDManagerDevEntities" DefaultContainerName="CDManagerDevEntities" EnableFlattening="False" EntitySetName="ApplyLog" Where="it.Book.ISBN=@ISBN" Select="it.DZTM,it.Reader.XM,it.SQSJ,it.IP" OrderBy="it.SQSJ">
        <WhereParameters>
            <asp:QueryStringParameter Name="ISBN" QueryStringField="ISBN" DbType="String" />
        </WhereParameters>
    </asp:EntityDataSource>
    <center>
        <asp:UpdatePanel ID="upApplyLog" runat="server">
            <ContentTemplate>
                <h3>光盘资源申请处理</h3>
                <hr />
                <asp:ListView ID="lvApplyLogList" runat="server" DataSourceID="edsApplyLogList" OnItemDataBound="lvApplyLogList_ItemDataBound" Visible="false">
                    <AlternatingItemTemplate>
                        <tr class="alternating">
                            <td>
                                <asp:Button ID="btnBan" runat="server" Text="忽略申请" CommandArgument='<%# Eval("ISBN") %>' OnCommand="btnBan_Command" />
                                <asp:Button ID="btnUpload" runat="server" Text="上传光盘" CommandArgument='<%# Eval("ISBN") %>' OnCommand="btnUpload_Command" />
                            </td>
                            <td>
                                <asp:Label ID="lblISBN" runat="server" Text='<%# Eval("ISBN") %>' />
                            </td>
                            <td>
                                <asp:HyperLink ID="linkZTM" runat="server" NavigateUrl='<%# Eval("ISBN","~/Management/BookManager/EditBook.aspx?ISBN={0}") %>' Text='<%# Eval("ZTM") %>'></asp:HyperLink>
                            </td>
                            <td>
                                <asp:HyperLink ID="linkCountApply" runat="server" NavigateUrl='<%# Eval("ISBN","~/Management/CDManager/ApplyLogList.aspx?ISBN={0}") %>'></asp:HyperLink>
                            </td>
                            <td>
                                <asp:Label ID="SQSJLabel" runat="server" Text='<%# Eval("SQSJ") %>' />
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                    <EmptyDataTemplate>
                        <table id="Table1" runat="server" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #5381AC; border-style: solid; border-width: 1px;">
                            <tr>
                                <td>没有光盘申请</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <tr class="item">
                            <td>
                                <asp:Button ID="btnBan" runat="server" Text="忽略申请" CommandArgument='<%# Eval("ISBN") %>' OnCommand="btnBan_Command" />
                                <asp:Button ID="btnUpload" runat="server" Text="上传光盘" CommandArgument='<%# Eval("ISBN") %>' OnCommand="btnUpload_Command" />
                            </td>
                            <td>
                                <asp:Label ID="lblISBN" runat="server" Text='<%# Eval("ISBN") %>' />
                            </td>
                            <td>
                                <asp:HyperLink ID="linkZTM" runat="server" NavigateUrl='<%# Eval("ISBN","~/Management/BookManager/EditBook.aspx?ISBN={0}") %>' Text='<%# Eval("ZTM") %>'></asp:HyperLink>
                            </td>
                            <td>
                                <asp:HyperLink ID="linkCountApply" runat="server" NavigateUrl='<%# Eval("ISBN","~/Management/CDManager/ApplyLogList.aspx?ISBN={0}") %>'></asp:HyperLink>
                            </td>
                            <td>
                                <asp:Label ID="SQSJLabel" runat="server" Text='<%# Eval("SQSJ") %>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table id="Table2" runat="server" style="border: 1px solid #5381AC; border-collapse: collapse" cellpadding="0">
                            <tr id="Tr1" runat="server">
                                <td id="Td1" runat="server">
                                    <table id="itemPlaceholderContainer" runat="server" border="1" style="border-collapse: collapse; border-color: #999999; border-style: none;">
                                        <tr id="Tr2" runat="server" class="tableheader">
                                            <th id="Th1" runat="server">申请操作</th>
                                            <th id="Th2" runat="server">
                                                <asp:LinkButton ID="headerISBN" CommandArgument="ISBN" CommandName="Sort" runat="server" ForeColor="White">ISBN</asp:LinkButton></th>
                                            <th id="Th3" runat="server">
                                                <asp:LinkButton ID="headerZTM" CommandArgument="ZTM" CommandName="Sort" runat="server" ForeColor="White">图书名称</asp:LinkButton></th>
                                            <th id="Th4" runat="server">申请数</th>
                                            <th id="Th5" runat="server">
                                                <asp:LinkButton ID="headerFFSJ" CommandArgument="ZTM" CommandName="Sort" runat="server" ForeColor="White">光盘操作时间</asp:LinkButton></th>
                                        </tr>
                                        <tr id="itemPlaceholder" runat="server">
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="Tr3" runat="server">
                                <td id="Td2" runat="server" style="text-align: center; padding-top: 5px; padding-bottom: 5px">
                                    <asp:DataPager ID="dpApply" runat="server" PageSize="25">
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
                                                    跳转到第<asp:TextBox ID="txtPage" runat="server" Width="20px" Text="<%# (Container.StartRowIndex/Container.PageSize)+1%>" onkeypress="if (event.keyCode < 48 || event.keyCode >57) event.returnValue = false;" />页<asp:Button ID="btnListGo" runat="server" Text="GO" OnCommand="btnListGo_Command" />
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
        <asp:Panel ID="panelLogDetail" runat="server" Visible="false">
            <h4>
                <asp:Label ID="lblCount" runat="server"></asp:Label>
            </h4>
            <hr />
            <asp:Button ID="btnBack" runat="server" Text="查看其他申请记录" OnClick="btnBack_Click" /><br />
            <asp:Button ID="btnBanDetail" runat="server" Text="忽略申请" OnClick="btnBanDetail_Click" />
            <asp:Button ID="btnUploadDetail" runat="server" Text="上传光盘" OnClick="btnUploadDetail_Click" />

            <asp:ListView ID="lvApplyLogDetail" runat="server" DataSourceID="edsApplyLogDetail">
                <AlternatingItemTemplate>
                    <tr class="alternating">
                        <td>
                            <asp:HyperLink ID="linkDZTM" runat="server" NavigateUrl='<%# Eval("DZTM","~/Management/User/ReaderManager/EditReader.aspx?DZTM={0}") %>' Text='<%# Eval("DZTM") %>'></asp:HyperLink>
                        </td>
                        <td>
                            <asp:Label ID="lblXM" runat="server" Text='<%# Eval("XM") %>' />
                        </td>
                        <td><asp:Label ID="lblSQSJ" runat="server" Text='<%# Eval("SQSJ") %>' /></td>
                        <td>
                            <asp:Label ID="lblIP" runat="server" Text='<%# Eval("IP") %>' />
                        </td>
                        <td>
                            <asp:Button ID="btnBanIP" runat="server" CommandArgument='<%# Eval("IP") %>' OnCommand="btnBanIP_Command" Text="禁止IP" />
                        </td>
                    </tr>
                </AlternatingItemTemplate>
                <EmptyDataTemplate>
                    <table id="Table1" runat="server" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #5381AC; border-style: solid; border-width: 1px;">
                        <tr>
                            <td>没有申请</td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <ItemTemplate>
                    <tr class="item">
                        <td>
                            <asp:HyperLink ID="linkDZTM" runat="server" NavigateUrl='<%# Eval("DZTM","~/Management/User/ReaderManager/EditReader.aspx?DZTM={0}") %>' Text='<%# Eval("DZTM") %>'></asp:HyperLink>
                        </td>
                        <td>
                            <asp:Label ID="lblXM" runat="server" Text='<%# Eval("XM") %>' />
                        </td>
                        <td><asp:Label ID="lblSQSJ" runat="server" Text='<%# Eval("SQSJ") %>' /></td>
                        <td>
                            <asp:Label ID="lblIP" runat="server" Text='<%# Eval("IP") %>' />
                        </td>
                        <td>
                            <asp:Button ID="btnBanIP" runat="server" CommandArgument='<%# Eval("IP") %>' OnCommand="btnBanIP_Command" Text="禁止IP" />
                        </td>
                    </tr>
                </ItemTemplate>
                <LayoutTemplate>
                    <table id="Table2" runat="server" style="border: 1px solid #5381AC; border-collapse: collapse" cellpadding="0">
                        <tr id="Tr1" runat="server">
                            <td id="Td1" runat="server">
                                <table id="itemPlaceholderContainer" runat="server" border="1" style="border-collapse: collapse; border-color: #999999; border-style: none;">
                                    <tr id="Tr2" runat="server" class="tableheader">
                                        <th id="Th1" runat="server">
                                            <asp:LinkButton ID="headerDZTM" CommandArgument="DZTM" CommandName="Sort" runat="server" ForeColor="White">读者条码</asp:LinkButton></th>
                                        <th id="Th2" runat="server">
                                            <asp:LinkButton ID="headerXM" CommandArgument="XM" CommandName="Sort" runat="server" ForeColor="White">姓名</asp:LinkButton></th>
                                        <th id="Th3" runat="server">
                                            <asp:LinkButton ID="headerSQSJ" CommandArgument="SQSJ" CommandName="Sort" runat="server" ForeColor="White">申请时间</asp:LinkButton></th>
                                        <th id="Th4" runat="server">申请IP</th>
                                        <th id="Th5" runat="server">禁止登录</th>
                                    </tr>
                                    <tr id="itemPlaceholder" runat="server">
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="Tr3" runat="server">
                            <td id="Td2" runat="server" style="text-align: center; padding-top: 5px; padding-bottom: 5px">
                                <asp:DataPager ID="dpApply" runat="server" PageSize="25">
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
                                                跳转到第<asp:TextBox ID="txtPage" runat="server" Width="20px" Text="<%# (Container.StartRowIndex/Container.PageSize)+1%>" onkeypress="if (event.keyCode < 48 || event.keyCode >57) event.returnValue = false;" />页<asp:Button ID="btnDetailGo" runat="server" Text="GO" OnCommand="btnDetailGo_Command" />
                                            </PagerTemplate>
                                        </asp:TemplatePagerField>
                                    </Fields>
                                </asp:DataPager>
                            </td>
                        </tr>
                    </table>
                </LayoutTemplate>
            </asp:ListView>

        </asp:Panel>

    </center>
</asp:Content>
