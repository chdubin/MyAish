<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Main.Models.ControllerView.Account.DetailShoppingTransactions>" %>
<div class="orders">
    <table style="width: 100%;" border="0">
        <tr>
            <td style="width: 34%;">
                <%if (Model.SelectedTransactionID > 0)
                  { %><p>
                      <b>Order#:</b><br />
                      <%= Model.SelectedTransactionID%></p>
                <%} %>
                <p>
                    <b>Date purchased:</b><br />
                    <%= Model.CreatedDate.ToString("MM/dd/yyy mm:hh tt") %></p>
            </td>
            <td style="width: 33%;">
                <div>
                    <b>Shipping Address:</b></div>
                <%if (Model.ShippingAddress != null)
                  { %><%=Html.Partial("ShowAddress", Model.ShippingAddress)%><%}
                  else
                  { %>
                  Same as Billing Address
                  <%} %>
            </td>
            <td style="width: 33%;">
                <div>
                    <b>Billing Address:</b></div>
                <%if (Model.BillingAddress != null)
                  { %><%=Html.Partial("ShowAddress", Model.BillingAddress)%><%} %>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <table style="width: 100%" cellspacing="0" cellpadding="5">
                    <thead>
                        <tr>
                            <th valign="bottom" align="left" style="width: 60px;">
                                Product ID
                            </th>
                            <th valign="bottom" align="left">
                                Title
                            </th>
                            <th valign="bottom" align="left">
                                Speaker
                            </th>
                            <th valign="bottom" align="left">
                                Type
                            </th>
                            <th valign="bottom" align="right">
                                Qty
                            </th>
                            <th valign="bottom" align="right">
                                Unit Price
                            </th>
                            <th valign="bottom" align="right">
                                Total
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <% foreach (var order in Model.ShoppingInSelectedTransaction)
                           {%>
                        <tr>
                            <td valign="top">
                                <%= order.ItemNumber%>
                            </td>
                            <td valign="top">
                                <%= order.Title.Replace("[Tape] ", "").Replace("[Disk] ", "").Replace("[File/Small Poster] ", "").Replace("[File] ", "")%>
                            </td>
                            <td valign="top">
                                <%= order.Speaker %>
                            </td>
                            <td valign="top">
                                <%= order.TypeName.Replace("Disk", "CD")%>
                            </td>
                            <td valign="top" align="right">
                                <%= order.cnt%>
                            </td>
                            <td valign="top" align="right">
                                <%if (order.price1 > 0)
                                  {%>
                                $<%= (order.price1 / order.cnt).ToString("N2")%>
                                <%}
                                  else if (order.price2 > 0)
                                  {%>
                                <%= order.price2.ToString("N2")%>
                                unit(s)
                                <%}
                                  else
                                  {%>
                                $0
                                <%} %>
                            </td>
                            <td valign="top" align="right">
                                <%if (order.price1 > 0)
                                  {%>$<%= order.price1.ToString("N2")%><%}%>
                            </td>
                        </tr>
                        <%} %>
                    </tbody>
                    <tfoot>
                        <tr>
                            <td colspan="4">
                            </td>
                            <td valign="top" align="right" colspan="2">
                                Subtotal:<br />
                                Shipping:<br />
                                Tax:<br />
                                Grand Total:
                            </td>
                            <td valign="top" align="right">
                                $<%= Model.SubTotal.ToString("N2")%><br />
                                $<%= Model.Shipping.ToString("N2")%><br />
                                $<%= Model.Taxes.ToString("N2")%><br />
                                $<%= Model.FinalTotal.ToString("N2")%>
                            </td>
                        </tr>
                    </tfoot>
                </table>
            </td>
        </tr>
    </table>
</div>
