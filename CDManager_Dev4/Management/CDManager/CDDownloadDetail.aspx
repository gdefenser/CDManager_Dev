<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/Masters/Management.Master" AutoEventWireup="true" CodeBehind="CDDownloadDetail.aspx.cs" Inherits="CDManager_Dev4.Management.CDManager.CDDownloadDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    光盘下载统计明细
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>光盘下载统计明细</h2>
    <input type="button" value="返回上一页" onclick="javascript: window.history.back(-1);" />
    <asp:EntityDataSource ID="edsDownloadLog" runat="server" ConnectionString="name=CDManagerDevEntities" DefaultContainerName="CDManagerDevEntities" EnableFlattening="False" EntitySetName="DownloadLog" Where="it.CD.CDXH=@CDXH">
        <WhereParameters>
            <asp:QueryStringParameter Name="CDXH" QueryStringField="CDXH" DbType="String" />
        </WhereParameters>
    </asp:EntityDataSource>
    <asp:EntityDataSource ID="edsCD" runat="server" ConnectionString="name=CDManagerDevEntities" DefaultContainerName="CDManagerDevEntities" EnableDelete="True" EnableFlattening="False" EnableInsert="True" EnableUpdate="True" EntitySetName="CD" EntityTypeFilter="" Select="" Where="it.CDXH=@CDXH">
        <WhereParameters>
            <asp:QueryStringParameter Name="CDXH" QueryStringField="CDXH" DbType="String" />
        </WhereParameters>
    </asp:EntityDataSource>
    <center>
        <hr />
        <asp:GridView ID="gvCD" runat="server" CellPadding="4" DataSourceID="edsCD"
            ForeColor="#333333" GridLines="None" AllowPaging="True" AutoGenerateColumns="False"
            BorderStyle="Solid" BorderWidth="1px" DataKeyNames="CDID"
            Font-Size="Small" AllowSorting="True"
            PageSize="15" BorderColor="#999999" OnRowDataBound="gvCD_RowDataBound">
            <AlternatingRowStyle BackColor="White" />
            <Columns>

                <asp:BoundField DataField="CDXH" HeaderText="光盘序号" ItemStyle-HorizontalAlign="Center" ReadOnly="true">
                    <ItemStyle HorizontalAlign="Center" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="CDMC" HeaderText="光盘名称" ItemStyle-HorizontalAlign="Center" ReadOnly="true">
                    <ItemStyle HorizontalAlign="Center" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="CDGS" HeaderText="光盘格式" ItemStyle-HorizontalAlign="Center" ReadOnly="true">
                    <ItemStyle HorizontalAlign="Center" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="ZXZT" HeaderText="在线状态" ItemStyle-HorizontalAlign="Center" ReadOnly="true">
                    <ItemStyle HorizontalAlign="Center" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"></ItemStyle>
                </asp:BoundField>
                <asp:TemplateField HeaderText="下载总数">
                    <ItemTemplate>
                        <asp:Label ID="lblCount" runat="server"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="本日下载数">
                    <ItemTemplate>
                        <asp:Label ID="lblDayCount" runat="server"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="平均月下载数">
                    <ItemTemplate>
                        <asp:Label ID="lblMouthCount" runat="server"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                </asp:TemplateField>
                <asp:BoundField DataField="CZSJ" HeaderText="最近操作时间" ItemStyle-HorizontalAlign="Center" ReadOnly="true">
                    <ItemStyle HorizontalAlign="Center" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"></ItemStyle>
                </asp:BoundField>
            </Columns>
            <EditRowStyle BackColor="#99CCFF" />
            <EmptyDataTemplate>
                没有光盘信息
            </EmptyDataTemplate>
            <FooterStyle BackColor="#1383DC" Font-Bold="True" ForeColor="White" />
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
        <asp:Chart ID="chartDownload" runat="server" Width="550px">
            <Series>
                <asp:Series Name="Series1" ChartType="Line" YValuesPerPoint="2"></asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1">
                    <AxisX>
                        <MajorGrid Enabled="False" />
                    </AxisX>
                </asp:ChartArea>
            </ChartAreas>
        </asp:Chart>
        <hr />
        <h3>下载用户</h3>
        <asp:EntityDataSource ID="edsDownload" runat="server" ConnectionString="name=CDManagerDevEntities"
            DefaultContainerName="CDManagerDevEntities" EnableFlattening="False" EntitySetName="DownloadLog"
            Where="it.CD.CDXH=@CDXH" Select="it.CD.CDXH,it.XZSJ,it.CZYTM,it.XZBH,it.IP"
            OrderBy="it.XZSJ">
            <WhereParameters>
                <asp:QueryStringParameter DbType="String" Name="CDXH" QueryStringField="CDXH" />
            </WhereParameters>
        </asp:EntityDataSource>
        <asp:UpdatePanel ID="UpdatePanel" runat="server">
            <ContentTemplate>
                <asp:GridView ID="gvDownload" runat="server" CellPadding="4" DataSourceID="edsDownload"
                    ForeColor="#333333" GridLines="None" AllowPaging="True" AutoGenerateColumns="False"
                    BorderStyle="Solid" BorderWidth="1px" DataKeyNames="XZBH"
                    Font-Size="Small" AllowSorting="True"
                    PageSize="15" BorderColor="#999999">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="CZYTM" HeaderText="下载人" ReadOnly="True" SortExpression="CZYTM">
                            <ItemStyle BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="XZSJ" HeaderText="下载时间" ReadOnly="True" SortExpression="XZSJ">
                            <ItemStyle BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="IP" HeaderText="下载IP" ReadOnly="True" SortExpression="IP">
                            <ItemStyle BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="禁止IP登录">
                            <ItemTemplate>
                                <asp:Button ID="btnBanIP" runat="server" Text="禁止IP" OnCommand="btnBanIP_Command" CommandArgument='<%# Eval("IP") %>' />
                            </ItemTemplate>
                            <ItemStyle BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
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
            </ContentTemplate>
        </asp:UpdatePanel>
    </center>
</asp:Content>
