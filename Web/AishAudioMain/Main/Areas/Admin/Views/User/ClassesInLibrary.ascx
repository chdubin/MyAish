<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Main.Areas.Admin.Models.ControllerView.User.ViewFiles>" %>
<style type="text/css">
    .title
    {
        margin-bottom: 0px !important;
    }
    #classesListTable
    {
        margin: 0px;
    }
    .deleted
    {
        text-decoration: line-through;
    }
</style>
<div class="other hastable">
    <table id="classesListTable">
        <thead>
            <tr>
                <th>
                </th>
                <th>
                    Available in Library
                </th>
                <th>
                    Title
                </th>
                <th>
                    Speaker
                </th>
                <th>
                    Date
                </th>
            </tr>
        </thead>
        <tbody>
            <% int i = 0; %>
            <% foreach (var item in Model.Classes)
               { %>
            <tr>
                <td>
                    <%= Html.CheckBox("shopping_class_items[" + i + "].value", !item.deleted) %>
                    <%= Html.Hidden("shopping_class_items[" + i + "].key", item.classID)%>
                    <%= Html.Hidden("shopping_class_items_state[" + i + "].key", item.classID)%>
                    <%= Html.Hidden("shopping_class_items_state[" + i + "].value", !item.deleted)%>
                </td>
                <td>
                    <%= Html.CheckBox("shopping_class_available[" + i + "].value", item.unlimitedAccessInLibrary)%>
                    <%= Html.Hidden("shopping_class_available[" + i + "].key", item.classID) %>
                </td>
                <td class="<%= item.deleted ? "deleted" : "" %>">
                    <%= Html.Encode(item.title) %>
                </td>
                <td>
                    <%= Html.Encode(item.speakerName) %>
                </td>
                <td>
                    <%= Html.Encode(String.Format("{0:d}", item.createDate))%>
                </td>
            </tr>
            <% i++; %>
            <% } %>
        </tbody>
    </table>
</div>
<% Html.RenderPartial("AjaxPager", ViewData["Pager1"]); %>