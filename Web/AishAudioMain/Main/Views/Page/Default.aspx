<%@ Page Title="" Language="C#" MasterPageFile="../Shared/twoColumn.Master"
    Inherits="System.Web.Mvc.ViewPage<string>" %>

<asp:Content ContentPlaceHolderID="title" runat="server">
    Default
</asp:Content>
<asp:Content ContentPlaceHolderID="body" runat="server">
    <%= Model %>
</asp:Content>
