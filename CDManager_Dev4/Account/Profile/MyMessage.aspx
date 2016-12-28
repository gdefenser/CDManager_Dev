<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/Masters/Profile.Master" AutoEventWireup="true" CodeBehind="MyMessage.aspx.cs" Inherits="CDManager_Dev4.Account.Profile.MyMessage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    我的消息箱
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:EntityDataSource ID="edsMessage" runat="server" ConnectionString="name=CDManagerDevEntities" DefaultContainerName="CDManagerDevEntities" EnableFlattening="False" EntitySetName="Message" EntityTypeFilter="" Select="" Where="it.SXRTM=@SXRTM" OrderBy="it.FSSJ desc">
        <WhereParameters>
            <asp:SessionParameter DbType="String" Name="SXRTM" SessionField="TM" />
        </WhereParameters>
    </asp:EntityDataSource>
    <h3>我的消息箱</h3>
    <hr />
    <div style="margin-top: 5px; margin-bottom: 100px">
        <asp:Panel ID="panelAdmin" runat="server" Visible="false">
            <center>
                管理员<asp:Label ID="lblAdmin" runat="server"></asp:Label>，请在后台查看消息。【<asp:HyperLink ID="linkAdmin" runat="server" NavigateUrl="~/Management/Message/AdminMessage.aspx">点击查看</asp:HyperLink>】
            </center>
        </asp:Panel>
        <asp:ListView ID="lvMessage" runat="server" DataKeyNames="XXTM" DataSourceID="edsMessage" Visible="false">
            <EmptyDataTemplate>
                <table runat="server" style="">
                    <tr>
                        <td>你没有收到消息</td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <ItemTemplate>
                <tr style="">
                    <th>发送人:</th>
                    <td>
                        <asp:Label ID="FSRTMLabel" runat="server" Text='<%# Eval("FSRTM") %>' />

                    </td>
                    <th>发送时间:</th>
                    <td>
                        <asp:Label ID="FSSJLabel" runat="server" Text='<%# Eval("FSSJ") %>' /></td>
                </tr>
                <tr>
                    <td colspan="5">
                        <asp:Label ID="XXNRLabel" runat="server" Text='<%# Eval("XXNR") %>' />
                        <hr />
                    </td>
                </tr>
            </ItemTemplate>
            <LayoutTemplate>
                <table runat="server">
                    <tr runat="server">
                        <td runat="server">
                            <table id="itemPlaceholderContainer" runat="server" border="0" style="">
                                <tr runat="server" style="">
                                </tr>
                                <tr id="itemPlaceholder" runat="server">
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr runat="server">
                        <td runat="server" style="">
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
    </div>
</asp:Content>
