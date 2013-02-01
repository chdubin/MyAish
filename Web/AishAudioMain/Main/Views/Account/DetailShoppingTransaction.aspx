<%@ Page Title="" Language="C#" MasterPageFile="../Shared/oneColumnEmpty.Master" 
Inherits="System.Web.Mvc.ViewPage<Main.Models.ControllerView.Account.DetailShoppingTransactions>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">

<style type="text/css">
 .orders table td
 {
    background-color:#99ccff;
    padding:5px;
    border: 2px solid #ffffff;
 }
 .orders table table td
 {
    border: 0px;
 }
 .orders table th
 {
    padding:5px;
    font-weight:bold;
 }
.orders table td b
{
    font-size:12px;
}
.orders table td
{
    font-size:11px;
}

</style>

<div class="maintitle" style="padding-bottom:15px;">
    Order Details:
</div>

<%=Html.Partial("DetailShopping", Model)%>

<%if(Model.Transactions.Length>1){ %>
<div class="transactions" style="margin:20px 0;">
    <div class="maintitle" style="margin:10px 0;">
        View Other Orders:
    </div>
    <div>
        <table class="shopping-transactions">
            <thead>
                <th style="padding:5px">
                    Order #
                </th>                    
                <th style="padding:5px">
                    Date Order Placed
                </th>
            </thead>

        <% foreach (var item in Model.Transactions)
            { %>
                <tr>
                    <td style="padding:5px">
                        <b>
                            <% if (item.shoppingTransactionID != Model.SelectedTransactionID)
                               { %>
                                <a href="<%= Url.Action("DetailShoppingTransaction", "Account", new { transaction_id = item.shoppingTransactionID }) %>">
                                    <%= item.shoppingTransactionID%>
                                </a>
                            <% }
                               else
                               { %>
                               <%= item.shoppingTransactionID%>
                            <%} %>
                        </b>
                    </td>                        
                    <td style="padding:5px">
                        <%= item.createDate.ToString("MM/dd/yy mm:hh tt") %>
                    </td>
                </tr>
        <%} %>
        </table>
    </div>	
</div>
<%} %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="title" runat="server">Order Details</asp:Content>