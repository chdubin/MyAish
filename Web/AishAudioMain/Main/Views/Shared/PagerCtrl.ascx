<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Main.Areas.Admin.Models.Common.PagingData>" %>
<% if (Model.TotalPageCnt > 1)
   { %>
<div style="clear: both">
</div>
<div style="padding: 10px 0; line-height: 1.5">
    <% if (Model.StartRowIndex > 0)
       { %>
    <div style="float: left">
        <a href="<%= Model.FirsPageUrl %>" class="prev_next">First
            <%= Model.PageSize%></a><br />
        <a href="<%= Model.PrevPageUrl %>" class="prev_next"><img height="12" border="0" align="absmiddle" width="6" src="<%= Url.Image("arrow_previous.jpg") %>" /> Prev
            <%= Model.PageSize%>
            Results</a>
    </div>
    <% } %>
    <% if (Model.StartRowIndex < Model.StartRowForLastFullPage)
       { %>
    <div style="float: right; text-align: right">
        <a href="<%= Model.LastFullPageUrl %>" class="prev_next">Last
            <%= Model.PageSize%></a><br />
        <a href="<%= Model.NextPageUrl %>" class="prev_next">Next
            <%= Model.PageSize%>
            Results <img height="12" border="0" align="absmiddle" width="6" src="<%= Url.Image("arrow_next.jpg") %>" /></a>
    </div>
    <% } %>

    <% if (Model.StartRowIndex > Model.StartRowForLastFullPage && Model.TotalRowCnt > Model.PageSize)
       { %>
       <div style="float: right; text-align: right">
        <a href="<%= Model.LastFullPageUrl %>" class="prev_next">Last
            <%= Model.PageSize%></a><br />
            </div>
       <% } %>
    <div style="clear: both">
    </div>
</div>
<% } %>
