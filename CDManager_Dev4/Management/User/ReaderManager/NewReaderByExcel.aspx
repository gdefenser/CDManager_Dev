<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/Masters/Management.Master" AutoEventWireup="true" CodeBehind="NewReaderByExcel.aspx.cs" Inherits="CDManager_Dev4.Management.User.ReaderManager.NewReaderByExcel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    读者信息批量导入
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="<%=ResolveClientUrl("~/Resources/Scripts/ReaderUpload.js")%>"></script>
    <h2>读者信息批量导入</h2>
    <hr />
    <div style="margin-left: 100px">
        <h3>Excel文件导入</h3>
        <ul>
            <li>系统只能识别Excel2000-2003(.xls)或者Excel2007以上(.xlsx)的数据文件 </li>
            <li>数据文件请确保严格按照以下格式排版及编辑字段名,否则数据导入将会出错
            <ul>
                <li>
                    <table id="excelexample">
                        <tr>
                            <th>DZTM
                            </th>
                            <th>XM
                            </th>
                            <th>BZRQ
                            </th>
                            <th>YXRQ
                            </th>
                            <th>XB
                            </th>
                            <th>DZLX
                            </th>
                            <th>YJDW
                            </th>
                            <th>EJDW
                            </th>
                        </tr>
                        <tr>
                            <th>读者条码(学号/工号)</th>
                            <th>姓名</th>
                            <th>办证日期</th>
                            <th>有效日期</th>
                            <th>性别</th>
                            <th>读者类型</th>
                            <th>一级单位</th>
                            <th>二级单位</th>
                        </tr>
                    </table>
                </li>
            </ul>
            </li>
        </ul>

        <asp:Panel ID="panelUpload" runat="server" Visible="false">
            <asp:FileUpload ID="FileUpload" runat="server" /><asp:Button ID="btnUpload" runat="server" Text="上传待导入读者数据" OnClick="btnUpload_Click" />
        </asp:Panel>
        <br />
        <asp:UpdatePanel ID="upMessage" runat="server">
            <ContentTemplate>
                <asp:Label ID="lblMsg" runat="server"></asp:Label><br />
                <asp:HyperLink ID="linkCheck" runat="server">查看待导入数据</asp:HyperLink>
                <asp:Button ID="btnReUplod" runat="server" Text="重新上传" OnClick="btnReUplod_Click" /><asp:Button ID="btnInsert" runat="server" Text="开始导入数据到数据库" OnClick="btnInsert_Click" /><br />
                <asp:Label ID="lblUploading" runat="server"></asp:Label>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upMessage" DisplayAfter="0" DynamicLayout="false">
                    <ProgressTemplate>
                        <img src="../../../Resources/Images/5-120604091310-50.gif" /><br />
                        读者数据处理中...
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
