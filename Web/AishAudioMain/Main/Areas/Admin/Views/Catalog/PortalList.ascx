<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Main.Areas.Admin.Models.Catalog.CatalogItemXPortal[]>" %>
<%@ Import Namespace="Main.Common" %>
<% bool isPackage = ViewData["isPackage"] != null && (bool)ViewData["isPackage"]; %>
<table>
    <thead>
        <tr>
            <th>
                <input type="checkbox" value="" class="checkbox" onchange="$('#ClassXPortal_Selected').val($(this).val());" />
            </th>
            <th>
                Visible
            </th>
            <% if (!isPackage)
               { %>
            <th>
                Free
            </th>
            <th>
                Is Free Offer
            </th>
            <th>
                Free Listen Page
            </th>
            <% } %>
            <th>
                ID
            </th>
            <th>
                Title
            </th>
        </tr>
    </thead>
    <tbody>
        <% for (int i = 0; i < Model.Length; i++)
           {
               var item = Model[i]; %>
        <tr class="<%=  (long)ViewData["currentPortalID"] == item.PortalID?"odd":"even" %>">
            <td>
                <%= Html.CheckBox("InPortals[" + i + "].Selected", item.Selected, new { @class = "checkbox" }) %>
            </td>
            <td>
                <%= Html.CheckBox("InPortals[" + i + "].IsVisible", item.IsVisible, new { @class = "checkbox" }) %>
            </td>
            <% if (!isPackage)
               { %>
            <td>
                <%= Html.CheckBox("InPortals[" + i + "].IsFree", item.IsFree, new { @class = "checkbox" })%>
            </td>
            <td>
                <%= Html.CheckBox("InPortals[" + i + "].IsFreeOffer", item.IsFreeOffer, new { @class = "checkbox" })%>
            </td>
            <td>
                <%= Html.CheckBox("InPortals[" + i + "].IsFullFree", item.IsFullFree, new { @class = "checkbox" })%>
            </td>
            <% } %>
            <td>
                <%= Html.Encode(item.PortalID) %>
                <%= Html.Hidden("InPortals[" + i + "].PortalID", item.PortalID) %>
            </td>
            <td>
                <%= Html.Encode(item.Title) %>
                <%= Html.Hidden("InPortals[" + i + "].Title", item.Title) %>
            </td>
        </tr>
        <% } %>
    </tbody>
</table>
