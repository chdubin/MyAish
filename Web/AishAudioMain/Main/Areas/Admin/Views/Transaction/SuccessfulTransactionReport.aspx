<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/twocolumns.Master" Inherits="System.Web.Mvc.ViewPage<MainEntity.Models.Shopping.ReportShoppingTransaction[]>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="title title-spacing">
        <h2>Transaction Report - <%=ViewData["Title"] %> - Successful Transactions Only</h2>
    </div>
    <% Html.RenderPartial("ShoppingTransactionsFilter", ViewData["Filter"]); %>
	<div class="hastable">
        <table>
            <thead>
                <tr>
                    <th class="header">
                        Name
                    </th>
                    <th class="header">
                        Username
                    </th>
                    <th class="header">
                        CURRENT Membership Type
                    </th>
                    <th class="header">
                        Credit Card #
                    </th>
                    <th class="header">
                        Transaction ID
                    </th>
                    <th class="header">
                        Amount
                    </th>
                    <th class="header">
                        Transaction Date
                    </th>
                    <th class="header">
                        Transaction Type
                    </th>
                    <th class="header">
                        Purchased Item(s)
                    </th>
                </tr>
            </thead>
            <tbody>
            <%foreach (var item in Model)
              { %>
            <tr>
                <td><%=item.MembershipFirstName +" "+ item.MembershipLastName %></td>
                <td><%=item.MembershipUserName%></td>
                <td><%if(item.CurrentSubscribeEndDate>DateTime.Now){ %> <%=item.CurrentSubscribeTitle%><%} %></td>
                <td><%="xxxx-xxxx-xxxx-"+item.CardID %></td>
                <td><%=item.TransactionID %></td>
                <td><%=item.Amount!=null?"$"+item.Amount.Value.ToString("F2"):string.Empty %></td>
                <td><%=item.TransactionDate.ToString("MM/dd/yyyy hh:mm:ss")%></td>
                <td><%=string.Join(", ", item.TransactionType.Split('_')) %></td>
                <td><%=string.Join(", ", item.ProductTypes.Distinct().Select(t=>t.ToString()).ToArray())%></td>
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
