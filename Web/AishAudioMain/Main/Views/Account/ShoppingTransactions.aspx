<%@ Page Title="" Language="C#" MasterPageFile="../Shared/twoColumn.Master" 
Inherits="System.Web.Mvc.ViewPage<MainEntity.Models.Shopping.UserShoppingTransaction[]>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="title" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">    

    <div class="maintitle" style="padding-bottom:15px;">
        View Order Status
    </div>

    <% if (Model != null && Model.Length > 0)
       {%>

        <div>
            Select an Order:
        </div>
        <div>
            <table class="shopping-transactions" cellspacing="5" cellpadding="0" border="0">
                <thead>
                    <td>
                        Order #
                    </td>                    
                    <td>
                        Date Order Placed
                    </td>
                </thead>

            <% foreach (var item in Model)
               { %>
                    <tr>
                        <td>
                            <b>                                    
                                <a href="<%= Url.Action("DetailShoppingTransaction", "Account", new { transaction_id = item.shoppingTransactionID }) %>">
                                    <%= item.shoppingTransactionID %>
                                </a>
                            </b>
                        </td>                        
                        <td>
                            <%= item.createDate.ToString("MM/dd/yy") %>
                        </td>
                    </tr>
            <%} %>
            </table>
        </div>	
    <%}else{ %>
        <div>
            No orders were found in our database under your username.
        </div>	
    <%} %>

</asp:Content>

