<%@ Page Title="" Language="C#" MasterPageFile="../Shared/twoColumn.Master"
    Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <% if ((bool)(this.ViewData["NoSubscribe"] ?? false) || (bool)(this.TempData["NoSubscribe"] ?? false))
       { %>
       You are not currently subscribed to any paid plans.
    <% }
       else
       { %>
        <p>To cancel your monthly subscription, please click the button below.<br /><br />
        The classes you downloaded previously will no longer be available for download from the My Library section so be sure to download every class you’d like to keep before cancelling your account.<br /><br />
        After cancellation, your monthly subscription will remain in effect until your charge day.<br /><br /></p>
    <% using (Html.BeginForm("DoCancelSubscribe", "Shopping", FormMethod.Post))
       { %>
    <input type="submit" value="Yes, please cancel my subscription." />
    <% } %>
    <% } %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
</asp:Content>
