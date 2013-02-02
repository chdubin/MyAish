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


<asp:Content ID="additionalNav" ContentPlaceHolderID="rightAdditionalNav" runat="server">
    <div>
        <h3><a href="#">User Options</a></h3>
            <div>
                <ul class="side-menu">
                    <li><a href="<%= Url.Action("EditUserInfo","User",  new { user_id = Model.UserID }) %>" title="Edit User">Edit User Info</a></li>
                    <li><%=Html.ActionLink("View Files", "ClassActivityLog", new { user_id = Model.UserID })%></li>
                    <li><a href="<%= Url.Action("ViewFiles","User",  new { user_id = Model.UserID })%>" title="">Add Downloads</a></li>
                    <li><a href="<%= Url.Action("ViewShoppingTransactions", new { user_id = Model.UserID }) %>" title="">View Transactions</a></li>
                    <li style="border-bottom: none;">&nbsp;</li>
                    <li><%=Html.ActionLink("Change CC", "ChangeCreditCard", new { user_id = Model.UserID })%></li>
                    <li style="border-bottom: none;">&nbsp;</li>
                    <li>
                        <%--<a href="<= Url.Action("PlaceOrder","User",  new { user_id = Model.UserID }) >">--%>
                            Place Order
                        <%--</a> --%>
                    </li>
                    <li>
                        <%--<a href="<= Url.Action("EnterReturn","User",  new { user_id = Model.UserID }) >">--%>
                            Enter Return
                        <%--</a> --%>
                    </li>
                </ul>
            </div>
    </div>
</asp:Content>
