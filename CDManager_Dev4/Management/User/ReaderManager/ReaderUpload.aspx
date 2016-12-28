<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReaderUpload.aspx.cs" Inherits="CDManager_Dev4.Management.User.ReaderManager.ReaderUpload" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<script>
    window.top.callBack(fileName);
</script>

<body>
    <form id="form1" runat="server">
        <div>
            <asp:FileUpload ID="FileUpload" runat="server" /><asp:Button ID="btnUpload" runat="server" Text=" 开始导入 " OnClick="btnUpload_Click" />
        </div>
    </form>
</body>
</html>
