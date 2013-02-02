<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/twocolumns.Master" Inherits="System.Web.Mvc.ViewPage<Main.Areas.Admin.Models.ControllerView.Transaction.TransactionDetail>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
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
 

</style>
  <div class="title title-spacing">
          <div class="button float-right">
            <a class="btn ui-state-default" style="margin: 0" href="javascript:void(0)" onclick="history.go(-1)">
                <span class="ui-icon ui-icon-circle-close"></span>Back</a>
        </div>
    <h2>Transaction details</h2></div>
    <div class="clearfix"></div>

<%if (Model.SelectedTransactionID != 0)
  { %>
<div class="orders">
    <table style="width:100%;" border="0">
    <tr>
        <td style="width:34%;">
            <p><b>Order#:</b> <%= Model.SelectedTransactionID%></p>
            <p><b>Date purchased:</b> <%= Model.CreatedDate.ToString("f")%></p>
        </td>
        <td style="width:33%;">
            <div><b>Shipping Address:</b></div>
            <%if (Model.ShippingAddress != null)
              { %><%=Html.Partial("ShowAddress", Model.ShippingAddress)%><%} %>
        </td>
        <td style="width:33%;">
            <div><b>Billing Address:</b></div>
            <%if (Model.BillingAddress != null)
              { %><%=Html.Partial("ShowAddress", Model.BillingAddress)%><%} %>
        </td>
    </tr>
    <tr>
        <td colspan="3">
        <table style="width:100%" cellspacing="0" cellpadding="5">
        <thead>
        <tr>
            <th  valign="bottom" align="left" style="width:60px;">
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
            <th valign="bottom" align="left">
                Qty
            </th>
            <th valign="bottom" align="left">
                Unit Price
            </th>
            <th valign="bottom" align="left">
                Total Status
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
                   <%= order.Title%>
                </td>
                <td valign="top">
                    <%= order.Speaker%>
                </td>
                <td valign="top">
                    <%= order.TypeName%>
                </td>
                <td valign="top" align="right">
                    <%= order.cnt%>
                </td>
                <td valign="top" align="right">
                <%if (order.price1 > 0)
                  {%>
                    $<%= (order.price1 / order.cnt).ToString("0.##")%>
                    <%}
                  else
                  {%>
                    <%= order.price2.ToString("0.##")%> unit(s)
                    <%} %>
                </td>
                <td valign="top"><%if (order.price1 > 0)
                                   {%>$<%= order.price1.ToString("0.##")%><%}%></td>
            </tr>
        <%} %>
        </tbody>
        <tfoot>
        <tr>
            <td colspan="4">
            </td>
            <td valign="top" colspan="2">
                <div style="text-align:right">Subtotal:<br />
                Shipping:<br />
                Tax:<br />
                Grand Total:</div>
            </td>
            <td valign="top">
                $<%= Model.SubTotal.ToString("0.##")%><br/>
                $<%= Model.Shipping.ToString("0.##")%><br/>
                $<%= Model.Taxes.ToString("0.##")%><br/>
                $<%= Model.FinalTotal.ToString("0.##")%>
            </td>
        </tr>
        </tfoot>
        </table>
        </td>
    </tr>
    </table>
    <%}
  else
  { %>
      <div class="response-msg error ui-corner-all"><span>Order #<%=ViewData["TransactioID"] %> not found</span></div>
    <%} %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainHeader" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="rightTop" runat="server">
</asp:Content>
