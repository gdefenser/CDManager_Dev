<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/Masters/Management.Master"
    AutoEventWireup="true" CodeBehind="AdminManagement.aspx.cs" Inherits="CDManager_Dev4.Management.User.AdminManager.AdminManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    图书管理员管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:EntityDataSource ID="edsAdmin" runat="server" ConnectionString="name=CDManagerDevEntities"
        DefaultContainerName="CDManagerDevEntities" EnableFlattening="False" EntitySetName="Admin"
        OrderBy="it.GLYTM" Select="it.[GLYTM], it.[XM], it.[XB], it.[YXRQ], it.[FFSJ], it.[FFCZY], it.[GLYLX]">
    </asp:EntityDataSource>
    <h3>图书管理员管理</h3>
    <hr />
    <asp:UpdatePanel ID="upAdmin" runat="server">
        <ContentTemplate>
            <center style="font-size: small">
                管理员条码:<asp:TextBox ID="txtGLYTM" runat="server"></asp:TextBox>
                <asp:Button ID="btnSelect" runat="server" Text="查找管理员" OnClick="btnSelect_Click" />
                <hr />
                <asp:ListView ID="lvAdmin" runat="server" DataKeyNames="GLYTM" DataSourceID="edsAdmin" OnItemDataBound="lvAdmin_ItemDataBound">
                    <AlternatingItemTemplate>
                        <tr class="alternating">
                            <td>
                                <asp:Button ID="btnDelete" runat="server" Text="注销" CommandArgument='<%# Eval("GLYTM") %>' OnCommand="btnDelete_Command" />
                            </td>
                            <td>
                                <asp:HyperLink ID="linkGLYTM" runat="server" NavigateUrl='<%# Eval("GLYTM","~/Management/User/AdminManager/EditAdmin.aspx?GLYTM={0}") %>' Text='<%# Eval("GLYTM") %>'></asp:HyperLink>
                            </td>
                            <td>
                                <asp:Label ID="XMLabel" runat="server" Text='<%# Eval("XM") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblGLYLX" runat="server" Text='<%# Eval("GLYLX") %>' />
                            </td>
                            <td>
                                <asp:Label ID="FFSJLabel" runat="server" Text='<%# Eval("FFSJ") %>' />
                            </td>
                            <td>
                                <asp:Label ID="YXRQLabel" runat="server" Text='<%# Eval("YXRQ") %>' />
                            </td>
                            <td>
                                <asp:Label ID="FFCZYLabel" runat="server" Text='<%# Eval("FFCZY") %>' />
                            </td>  
                        </tr>
                    </AlternatingItemTemplate>
                    <EmptyDataTemplate>
                        <table id="Table1" runat="server" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #5381AC; border-style: solid; border-width: 1px;">
                            <tr>
                                <td>没有管理员</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <ItemTemplate>
                        <tr class="item">
                            <td>
                                <asp:Button ID="btnDelete" runat="server" Text="注销" CommandArgument='<%# Eval("GLYTM") %>' OnCommand="btnDelete_Command" />
                            </td>
                            <td>
                                <asp:HyperLink ID="linkGLYTM" runat="server" NavigateUrl='<%# Eval("GLYTM","~/Management/User/AdminManager/EditAdmin.aspx?GLYTM={0}") %>' Text='<%# Eval("GLYTM") %>'></asp:HyperLink>
                            </td>
                            <td>
                                <asp:Label ID="XMLabel" runat="server" Text='<%# Eval("XM") %>' />
                            </td>
                            <td>
                                <asp:Label ID="lblGLYLX" runat="server" Text='<%# Eval("GLYLX") %>' />
                            </td>
                            <td>
                                <asp:Label ID="FFSJLabel" runat="server" Text='<%# Eval("FFSJ") %>' />
                            </td>
                            <td>
                                <asp:Label ID="YXRQLabel" runat="server" Text='<%# Eval("YXRQ") %>' />
                            </td>
                            <td>
                                <asp:Label ID="FFCZYLabel" runat="server" Text='<%# Eval("FFCZY") %>' />
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
                                                <asp:LinkButton ID="headerGLYTM" CommandArgument="GLYTM" CommandName="Sort" runat="server" ForeColor="White">管理员条码</asp:LinkButton></th>
                                            <th id="Th3" runat="server">
                                                <asp:LinkButton ID="headerXM" CommandArgument="XM" CommandName="Sort" runat="server" ForeColor="White">姓名</asp:LinkButton></th>
                                            <th id="Th4" runat="server">
                                                <asp:LinkButton ID="headerGLYLX" CommandArgument="GLYLX" CommandName="Sort" runat="server" ForeColor="White">管理员类型</asp:LinkButton>
                                            </th>
                                            <th id="Th5" runat="server">
                                                <asp:LinkButton ID="headerFFSJ" CommandArgument="FFSJ" CommandName="Sort" runat="server" ForeColor="White">创建时间</asp:LinkButton>
                                            </th>
                                            <th id="Th6" runat="server">
                                                <asp:LinkButton ID="headerYXRQ" CommandArgument="YXRQ" CommandName="Sort" runat="server" ForeColor="White">有效日期</asp:LinkButton></th>
                                            <th id="Th7" runat="server">
                                                <asp:LinkButton ID="headerFFCZY" CommandArgument="FFCZY" CommandName="Sort" runat="server" ForeColor="White">创建人</asp:LinkButton></th>
                                        </tr>
                                        <tr id="itemPlaceholder" runat="server">
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="Tr3" runat="server">
                                <td id="Td2" runat="server" style="text-align: center; padding-top: 5px; padding-bottom: 5px">
                                    <asp:DataPager ID="dpAdmin" runat="server" PageSize="15">
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
            </center>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
