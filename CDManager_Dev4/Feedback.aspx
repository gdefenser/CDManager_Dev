<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/Masters/Front.Master" AutoEventWireup="true" CodeBehind="Feedback.aspx.cs" Inherits="CDManager_Dev4.Feedback" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    用户体验调查与意见反馈
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <%--  <script language="javascript" type="text/javascript">
        function countNum() {
            var count = document.getElementById('<%=txtXX.ClientID %>').value.length;
            document.getElementById('<%=lblTextCount.ClientID %>').value = count;
        }
    </script>--%>
    <div style="margin-top: 30px; margin-right: 20px; margin-left: 20px; margin-bottom: 50px; width: 100%;">
        <h2 style="text-align: center">用户体验调查与意见反馈</h2>
        
        <div style="margin-right: 20%; margin-left: 20%; margin-top: 50px">
            <hr />
            <p>
                各位光盘系统使用者，您好：
            </p>
            <p style="text-indent: 2em;">
                即日起，广州大学华软软件学院图书馆光盘系统正式上线使用，由于本系统目前为止仍处于测试阶段，还有许多更大的提升空间。而本系统设计的最大初衷和受众是广大师生读者，因此我们十分期待读者给予我们开发组关于用户体验、使用反馈以及不足之处的建议，有了你们对本系统的建议，才能使我们更有针对性的对本系统进行完善工作。感谢各位读者对图书馆和本系统开发小组的支持！
            </p>
            <hr />
            <center id="feedback">
                <table>
                    <tr>
                        <th colspan="2">建议内容(300字以内)</th>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:TextBox ID="txtXX" runat="server" MaxLength="300" Columns="60" TextMode="MultiLine" Rows="10"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btnClick" runat="server" Text=" 确 定 " OnClick="btnClick_Click" /><asp:Button ID="btnReset" runat="server" Text=" 重 置 " OnClick="btnReset_Click" style="height: 21px" />
                        </td>
                    </tr>
                </table>
            </center>
        </div>
    </div>
</asp:Content>
