<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<string[]>" %>
<%= Html.TextBox("categories_" + ViewData["CatalogItemID"], string.Join("|",(string[])Model), new { @class = "field text medium", id = "edit-categories-" + ViewData["CatalogItemID"] })%>
<%=Ajax.ActionLink2(@"<span class=""ui-icon ui-icon-circle-check""></span>Update Categories", "UpdateCategories",
    new { catalog_item_id = ViewData["CatalogItemID"] },
        new AjaxOptions() { UpdateTargetId = "categories-" + ViewData["CatalogItemID"], OnBegin = "function(ajaxContext){OnBeginUpdateCategories(ajaxContext, 'edit-categories-" + ViewData["CatalogItemID"] + "');}" },
    new { @class = "btn ui-state-default" })%>
<%=Ajax.ActionLink2(@"<span class=""ui-icon ui-icon-circle-close""></span>Cancel", "ShowCategories",
    new { catalog_item_id = ViewData["CatalogItemID"] },
        new AjaxOptions() { UpdateTargetId = "categories-" + ViewData["CatalogItemID"] },
    new { @class = "btn ui-state-default" })%>
