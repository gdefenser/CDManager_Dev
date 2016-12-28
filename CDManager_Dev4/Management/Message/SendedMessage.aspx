<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/Masters/Management.Master" AutoEventWireup="true" CodeBehind="SendedMessage.aspx.cs" Inherits="CDManager_Dev4.Management.Message.SendedMessage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    管理员发信箱
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:EntityDataSource ID="edsAdmin" runat="server" ConnectionString="name=CDManagerDevEntities" DefaultContainerName="CDManagerDevEntities"
        CommandText="select m1.YHLX,m1.SXRTM,m1.FSRTM,m1.FSSJ,m1.XXNR from Message as m1 where m1.XXTM in (select value MAX(m2.XXTM) from Message as m2 group by m2.FSRTM,m2.SXRTM)" AutoGenerateOrderByClause="True"
        Where="it.FSRTM=@FSRTM">
        <WhereParameters>
            <asp:SessionParameter DbType="String" Name="FSRTM" SessionField="TM" />
        </WhereParameters>
        <OrderByParameters>
            <asp:Parameter DefaultValue="FSSJ" />
        </OrderByParameters>
    </asp:EntityDataSource>
    <h2>管理员发信箱</h2>
    <hr />
    <asp:ListView ID="lvAdmin" runat="server" DataSourceID="edsAdmin">
        <EmptyDataTemplate>
            <table id="Table1" runat="server" style="">
                <tr>
                    <td>你没有发送消息。</td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <ItemTemplate>
            <tr style="">
                <th>收信人:</th>
                <td>
                    <asp:HyperLink ID="linkSXRTM" runat="server" Text='<%# Eval("SXRTM") %>' NavigateUrl='<%# Eval("SXRTM","~/Management/Message/MessageDialog.aspx?TM={0}") %>'></asp:HyperLink>
                </td>
                <th>发送时间:</th>
                <td>
                    <asp:Label ID="FSSJLabel" runat="server" Text='<%# Eval("FSSJ") %>' />
                </td>
            </tr>
            <tr>
                <th colspan="5" bgcolor="#5381AC" style="color: #FFFFFF">最新消息</th>
            </tr>
            <tr>
                <td colspan="5" style="border-bottom-style: dotted; border-width: 1px">
                    <asp:Label ID="XXNRLabel" runat="server" Text='<%# Eval("XXNR") %>' />
                </td>
            </tr>
        </ItemTemplate>
        <LayoutTemplate>
            <table id="Table2" runat="server">
                <tr id="Tr1" runat="server">
                    <td id="Td1" runat="server">
                        <table id="itemPlaceholderContainer" runat="server" border="0" style="">
                            <tr id="Tr2" runat="server" style="">
                            </tr>
                            <tr id="itemPlaceholder" runat="server">
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr id="Tr3" runat="server">
                    <td id="Td2" runat="server" style="">
                        <asp:DataPager ID="DataPager1" runat="server">
                            <Fields>
                                <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False" />
                                <asp:NumericPagerField />
                                <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False" />
                            </Fields>
                        </asp:DataPager>
                    </td>
                </tr>
            </table>
        </LayoutTemplate>
    </asp:ListView>
</asp:Content>
