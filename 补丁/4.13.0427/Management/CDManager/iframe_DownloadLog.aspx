<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="iframe_DownloadLog.aspx.cs" Inherits="CDManager_Dev4.Management.CDManager.iframe_DownloadLog" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body style="margin: 0px; width:180px;background-color: #EFF3FB">
    <form id="formDownload" runat="server" style="margin: 0px; width:180px">
    <asp:Button ID="btnExcel" runat="server" Text="导出并下载当前列表" OnClick="btnExcel_Click"/>
    </form>
</body>
</html>
