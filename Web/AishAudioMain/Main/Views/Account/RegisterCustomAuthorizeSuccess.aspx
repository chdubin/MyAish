<%@ Page Title="" Language="C#" MasterPageFile="../Shared/twoColumn.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content2" ContentPlaceHolderID="title" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">

    <div>
        Would you like to choose your <a href="<%= Url.Action("FreeOffer", "Shopping") %>">2 free downloads now</a>, or <a href="<%= Url.Action("Index", "Home") %>">continue with your purchase/browsing</a>?
    </div>
    <div style="padding-top:5px;">
        If you choose to continue, you can access the choice page later from your <a href="<%= Url.Action("Index", "Account") %>">My Account section</a>. 
    </div>

</asp:Content>

