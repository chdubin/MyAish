<%@ Page Title="" Language="C#" MasterPageFile="../Shared/twoColumn.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">

<br /><br />
<div>    
    Would you like to <a href="<%= Url.Action("FreeOffer", "Shopping") %>">choose your 2 free downloads now</a>, or <a href="<%= Url.Action("Index", "Home") %>">continue with your purchase/browsing</a>?
    <p>
        If you choose to continue, you can access the choice page later from your My Account section.	
        <br /><br />
	</p>
</div>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="title" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
</asp:Content>
