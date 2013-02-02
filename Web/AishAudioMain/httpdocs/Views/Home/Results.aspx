<%@ Page Title="" Language="C#" MasterPageFile="../Shared/twoColumn.Master"
    Inherits="System.Web.Mvc.ViewPage<Main.Models.ControllerView.Search.ClassListItem[]>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <% foreach (var item in Model)
       { %>
    <div>
        <%= item.Title %>
    </div>
    <% } %>
</asp:Content>
<asp:Content ContentPlaceHolderID="title" runat="server">
    Results
</asp:Content>
