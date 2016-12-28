<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CalendarExtender.ascx.cs"
    Inherits="CDManager_Dev4.Resources.UserControl.CalendarExtender" %>
<script src="<%=ResolveClientUrl("~/Resources/Scripts/jquery-1.8.0.min.js")%>"
    type="text/javascript"></script>
<script src="<%=ResolveClientUrl("~/Resources/Scripts/cal.js")%>" type="text/javascript"></script>
<link href="<%=ResolveClientUrl("~/Resources/Styles/calendar.css")%>" rel="stylesheet"
    type="text/css" />
<script>
    jQuery(document).ready(function () {
        $('input.date').simpleDatepicker({ startdate: 1900, enddate: 2050 });
        $("TextBox").readOnly = true;
    });

    function pageLoad(sender, args) {
        if (args.get_isPartialLoad()) {
            $('input.date').simpleDatepicker({ startdate: 1900, enddate: 2050 });
            $("TextBox").readOnly = true;
        }
    }   
</script>
<asp:TextBox ID="TextBox" runat="server" CssClass="date" onkeypress="if (event.keyCode >0) event.returnValue = false;"
    Columns="10"></asp:TextBox>
