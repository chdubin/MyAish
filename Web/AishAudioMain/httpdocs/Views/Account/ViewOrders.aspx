<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/twoColumn.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content2" ContentPlaceHolderID="title" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">    

    <div class="maintitle" style="padding-bottom:15px;">
        View Order Status
    </div>

    <% if (true)
       { // если нет Orders %>
        <div>
            No orders were found in our database under your username.
        </div>	
    <%}else{ %>
        <div>
            Список заказов. Откуда брать????
        </div>	
    <%} %>

</asp:Content>

