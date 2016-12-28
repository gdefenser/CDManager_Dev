<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/Masters/Front.Master" AutoEventWireup="true" CodeBehind="Page404.aspx.cs" Inherits="CDManager_Dev4.Page404" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        window.setTimeout("window.history.back(-1)", 3000);
        function back() {
            var num = document.getElementById("hid").value;
            document.getElementById("hid").value = --num;
            var value = document.getElementById("hid").value;
            document.getElementById("num").innerHTML = value;
        }
        window.setInterval('back()', 1000)
    </script>
    <center style="padding-top: 250px; padding-bottom: 250px">
        <h2 style="color: #990000">很抱歉,我们找不到你请求的页面<br />
            <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
        </h2>

        <hr style="width: 90%" />
        <h4><span id="num" style="color: #990000">3</span>秒后自动返回原页面</h4>
        <input type="hidden" value="3" id="hid">
    </center>
</asp:Content>
