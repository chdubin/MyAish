<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/twocolumns.Master" Inherits="System.Web.Mvc.ViewPage<MainEntity.Models.Shopping.ReportShoppingTransaction[]>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="title title-spacing">
        <h2>Transaction Report - <%=ViewData["Title"] %> - Successful Transactions Only</h2>
    </div>

    <div style="margin-bottom:10px;background-color:#D0E9ED;">
        <% Html.RenderPartial("ShoppingTransactionsFilter", ViewData["Filter"]); %>
    </div>

    <table cellpadding="2" cellspacing="0" style="width:100%;">
    <tr>
    <td style="vertical-align:baseline;">
        Total Records Found: <%= ViewData["TotalCount"] %><br />
    </td>
    <td style="vertical-align:baseline;">
        <% Html.RenderPartial("Pager", ViewData["Pager"]); %>
    </td>
    <td style="text-align:right;" align="right">
        <a class="btn ui-state-default" href="<%= Url.Action("ReportPurchases", "Transaction", new RouteValueDictionary { { "downloadtype", "text" }, { "ps", ((Main.Areas.Admin.Models.Common.PagingData)ViewData["Pager"]).PageSize } } ) %>"><span class="ui-icon ui-icon-circle-plus"></span>Download as Text</a>
        <a class="btn ui-state-default"     href="<%= Url.Action("ReportPurchases", "Transaction", new RouteValueDictionary { { "downloadtype", "excel" }, { "ps", ((Main.Areas.Admin.Models.Common.PagingData)ViewData["Pager"]).PageSize } } ) %>"><span class="ui-icon ui-icon-circle-plus"></span>Download All as Excel 2007</a>
    </td>
    </tr>
    </table>

<%--    <% using (Html.BeginForm("DownloadAsASCII", "Transaction", new { area = "Admin" }, FormMethod.Post, new { enctype = "multipart/form-data" }))
       {%>
       <table>
            <tr>
                <td style="width:75%">
                    <!-- Html.RenderPartial("EditPackageForm", Model); -->
                </td>
            </tr>
       </table>
       <input type="submit" style="display:none;" />
       <a class="btn ui-state-default" href="<%= Url.Action("ReportPurchasesDownload") %>"><span class="ui-icon ui-icon-circle-plus"></span>Download</a>
    <%} %>--%>
    <br />

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
                        Credit Card #<br />(last 4 digits)
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
                <td style="width:150px;" >
                    <a href="http://<%=
                    ((Url.RequestContext.HttpContext).Request).Url.Authority
                    +((((Url.RequestContext.HttpContext).Request).ApplicationPath == "/") ? "" : ((Url.RequestContext.HttpContext).Request).ApplicationPath) %>/Admin/User?ssubscribe=None&sfirstname=<%= item.MembershipFirstName %>&slastname=<%= item.MembershipLastName %>"><%=item.MembershipLastName + ", " + item.MembershipFirstName%></a>
                </td>
                <td style="width:150px;"><%=item.MembershipUserName%></td>
                <td><%if(item.CurrentSubscribeEndDate>DateTime.Now){ %> <%=item.CurrentSubscribeTitle%><%} %></td>
                <td><%=item.CardID %></td>
                <td><%=item.TransactionID %></td>
                <td style="width:80px;"><%=item.Amount!=null?"$"+item.Amount.Value.ToString("F2"):string.Empty %></td>
                <td style="width:150px;"><%=item.TransactionDate.ToString("MM/dd/yyyy hh:mm:ss")%></td>
                <td><%=string.Join(", ", item.TransactionType.Split('_')) %></td>
                <td style="width:100px;"><%=string.Join(", ", item.ProductTypes.Distinct().Select(t=>t.ToString()).ToArray())%></td>
            </tr>
            <%} %>
            </tbody>
        </table>
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainHeader" runat="server">
        <link href="<%= Url.Css("css/jquery/plugins/datepicker/3_7_5/smoothness.datepick.css") %>" rel="stylesheet" media="all" />
		<script type="text/javascript" src="<%= Url.JavaScript("jquery/plugins/datepicker/3_7_5/jquery.datepick.js") %>"></script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="rightTop" runat="server">
</asp:Content>
