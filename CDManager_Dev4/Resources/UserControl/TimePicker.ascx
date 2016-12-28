<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TimePicker.ascx.cs"
    Inherits="CDManager_Dev4.Resources.UserControl.TimePicker" %>
<%@ Register src="CalendarExtender.ascx" tagname="CalendarExtender" tagprefix="uc1" %>
<table cellspacing="0" style="font-size: small">
    <tr>
        <th>
            日期:
        </th>
        <td>
           
            <uc1:CalendarExtender ID="CalendarExtender1" runat="server" />
           
        </td>
        <th>
            时间:
        </th>
        <td>
            <asp:DropDownList ID="dropHour" runat="server">
            </asp:DropDownList>
            点<asp:DropDownList ID="dropMinute" runat="server">
            </asp:DropDownList>
            分
        </td>
    </tr>
</table>
