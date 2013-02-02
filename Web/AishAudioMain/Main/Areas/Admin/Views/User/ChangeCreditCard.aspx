<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/twocolumns.Master"
    Inherits="System.Web.Mvc.ViewPage<Main.Models.Account.ChangeCreditCard>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

	<div class="title title-spacing">
	    <h2>Change credit card for <%=this.Model.FirstName %> <%=this.Model.LastName %></h2>
        <%=this.Model.Email %>
	</div>
    
    <%= Html.Partial("ChangeCreditCardForm", this.Model) %>

</asp:Content>
<asp:Content ContentPlaceHolderID="MainHeader" runat="server">
<style type="text/css">
    .editform-twocol-leftcol label
    {
   	    font-size:95%;
	    font-weight:bold;
	    color:#222;
	    line-height:150%;
	    margin:0;
	    padding:0 0 3px 0;
	    border:none;
	    display:block;
    }
    div.editform-twocol-leftcol
    {
        float:left;width:140px;padding-right:4em;
    }
</style>
</asp:Content>

