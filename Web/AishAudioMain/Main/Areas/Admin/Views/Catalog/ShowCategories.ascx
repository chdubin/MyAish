<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<string[]>" %>
<div id="categories-<%=ViewData["CatalogItemID"] %>">
    <%=string.Join(", ", Model) %>
    <%=Ajax.ActionLink("edit", "ChangeCategories", new { catalog_item_id = ViewData["CatalogItemID"] }, new AjaxOptions() { UpdateTargetId = "categories-" + ViewData["CatalogItemID"], HttpMethod = "POST", OnSuccess = "function(){InitializeEditCategories('edit-categories-" + ViewData["CatalogItemID"] + "');}" })%>
</div>
