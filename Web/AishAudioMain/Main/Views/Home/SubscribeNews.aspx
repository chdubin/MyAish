<%@ Page Title="" Language="C#" MasterPageFile="../Shared/twoColumn.Master" Inherits="System.Web.Mvc.ViewPage<System.Exception>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Body" runat="server">

<%if (this.Model == null)
  { %>
<div class="search_big_blue">
    Subscribe News <span class="search_has_found">Completed</span></div>
    <%}
  else
  { %>
<div class="search_big_blue">
    Subscribe News <span class="search_has_found">Not Completed</span></div>
    <p>Please try later</p>
    <p style="color:Red"><%=this.Model.Message %></p>
    <%} %>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="title" runat="server">Subscribe News</asp:Content>
