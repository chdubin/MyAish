<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/twocolumns.Master" Inherits="System.Web.Mvc.ViewPage<MainEntity.Models.Shopping.UserShoppingTransaction[]>" %>
<%@ Import Namespace="Main.Areas.Admin.Models.ControllerView.User" %>
<%@ Import Namespace="MainCommon" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="title title-spacing">
        <h2>
            <%=ViewData["UserName"] %> - Transactions</h2>
    </div>
	<% Html.RenderPartial("ShoppingTransactionsFilter", ViewData["Filter"]); %>
	<div class="hastable">
        <table>
            <thead>
                <tr>
                    <th class="header">
                        TransID
                    </th>
                    <th class="header">
                        TransDate
                    </th>
                    <th class="header">
                        Amount
                    </th>
                    <th class="header">
                        ccLast4
                    </th>
                    <th class="header">
                        ccExp
                    </th>
                    <th class="header">
                        TransType
                    </th>
                    <th class="header">
                        Response
                    </th>
                    <th class="header">
                        Transaction state
                    </th>
                </tr>
            </thead>
            <tbody>
            <%foreach (var item in Model)
              { %>
            <tr>
                <td><%=Html.ActionLink(item.shoppingTransactionID.ToString(),"TransactionDetail","Transaction",new{transaction_id=item.shoppingTransactionID},null) %></td>
                <td><%=item.createDate.ToString("MM/dd/yyyy hh:mm:ss")%></td>
                <td><%=item.amount!=null?item.amount.Value.ToString("F2"):string.Empty %></td>
                <td><%=item.membershipCartID %></td>
                <td><%=item.expirationDate != null ? item.expirationDate.Value.ToString("MM/dd/yyyy") : string.Empty%></td>
                <td><%=item.chargetype %></td>
                <td><%=item.response %></td>
                <td><%= (ShoppingTransactionStateEnum)item.transactionState %></td>
            </tr>
            <%} %>
            </tbody>
        </table>
        <% Html.RenderPartial("Pager", ViewData["Pager"]); %>
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainHeader" runat="server">
        <link href="<%= Url.Css("css/jquery/plugins/datepicker/3_7_5/smoothness.datepick.css") %>" rel="stylesheet" media="all" />
		<script type="text/javascript" src="<%= Url.JavaScript("jquery/plugins/datepicker/3_7_5/jquery.datepick.js") %>"></script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="rightTop" runat="server">
</asp:Content>


<asp:Content ID="additionalNav" ContentPlaceHolderID="rightAdditionalNav" runat="server">
    <div>
        <h3><a href="#">User Options</a></h3>
            <div>
                <ul class="side-menu">
                    <li><a href="<%= Url.Action("EditUserInfo","User",  new { user_id = Model.First().UserId }) %>" title="Edit User">Edit User Info</a></li>
                    <li><%=Html.ActionLink("View Files", "ClassActivityLog", new { user_id = Model.First().UserId })%></li>
                    <li><a href="<%= Url.Action("ViewFiles","User",  new { user_id = Model.First().UserId })%>" title="">Add Downloads</a></li>
                    <li><a href="<%= Url.Action("ViewShoppingTransactions", new { user_id = Model.First().UserId }) %>" title="">View Transactions</a></li>
                    <li style="border-bottom: none;">&nbsp;</li>
                    <li><%=Html.ActionLink("Change CC", "ChangeCreditCard", new { user_id = Model.First().UserId })%></li>
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
