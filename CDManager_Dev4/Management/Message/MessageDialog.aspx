<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/Masters/Management.Master" AutoEventWireup="true" CodeBehind="MessageDialog.aspx.cs" Inherits="CDManager_Dev4.Management.Message.MessageDialog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    消息对话
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>与<asp:Label ID="lblTitle" runat="server"></asp:Label>的对话</h2><input type="button" value="返回消息箱列表" onclick="javascript: window.history.back(-1);" style="float: right" />
    <hr />
    
    <asp:EntityDataSource ID="edsMessage" runat="server" ConnectionString="name=CDManagerDevEntities" DefaultContainerName="CDManagerDevEntities" EnableFlattening="False" EntitySetName="Message" EntityTypeFilter="Message" Where="it.SXRTM=@TM and it.FSRTM=@MTM or it.FSRTM=@TM and it.SXRTM=@MTM">
        <WhereParameters>
            <asp:QueryStringParameter Name="TM" DbType="String" QueryStringField="TM" />
            <asp:SessionParameter Name="MTM" DbType="String" SessionField="MTM" />
        </WhereParameters>
    </asp:EntityDataSource>
    <asp:ListView ID="lvMessage" runat="server" DataSourceID="edsMessage">
        <EmptyDataTemplate>
            <table id="Table1" runat="server" style="">
                <tr>
                    <td>你没有收到消息。</td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <ItemTemplate>
            <tr style="">
                <th>发送人:</th>
                <td>
                    <asp:Label ID="FSRTMLabel" runat="server" Text='<%# Eval("FSRTM") %>' />
                </td>
                <th>接收人:</th>
                <td>
                    <asp:Label ID="SXRTMLabel" runat="server" Text='<%# Eval("SXRTM") %>' /></td>
            </tr>
            <tr>
                <th>发送时间:</th>
                <td colspan="3">
                    <asp:Label ID="FSSJLabel" runat="server" Text='<%# Eval("FSSJ") %>' />
                </td>
            </tr>
            <tr>
                <th colspan="6">消息内容</th>
            </tr>
            <tr>
                <td colspan="6" style="border-bottom-style: dotted; border-width: 1px">
                    <asp:Label ID="XXNRLabel" runat="server" Text='<%# Eval("XXNR") %>' /><hr />
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
    <hr />
    <table>
        <tr>
            <th bgcolor="#5381AC" style="color: #FFFFFF">发送新消息</th>
        </tr>
        <tr>
            <td align="center">
                <asp:TextBox ID="txtXXNR" runat="server" Rows="8" Columns="40" TextMode="MultiLine"></asp:TextBox>
                <br />
                <asp:CustomValidator ID="valXXNR" runat="server" Font-Bold="True" ForeColor="Maroon"
                    Display="Dynamic"></asp:CustomValidator>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Button ID="btnNew" runat="server" Text=" 发  送 " OnClick="btnNew_Click" />
                <asp:Button ID="btnReset" runat="server" Text=" 重  置 " OnClick="btnReset_Click" /></td>
        </tr>
    </table>
</asp:Content>
