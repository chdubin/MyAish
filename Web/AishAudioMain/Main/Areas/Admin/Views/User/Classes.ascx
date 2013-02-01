<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Main.Areas.Admin.Models.ControllerView.User.ViewFiles>" %>
<div class="other hastable">
    <table style="margin: 0px">
        <thead>
            <tr>
                <th>
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
            <% foreach (var item in Model.OtherClasses)
               { %>
            <tr>
                <td>
                    <%= Html.CheckBox("class_items_to_add[" + i + "].value", false) %>
                    <%= Html.Hidden("class_items_to_add[" + i + "].key", item.entityID)%>
                </td>
                <td>
                    <%= Html.Encode(item.title) %>
                </td>
                <td>
                    <%= Html.Encode(item.ClassSpeakerName) %>
                </td>
                <td>
                    <%= Html.Encode(String.Format("{0:d}", item.createDate))%>
                </td>
            </tr>
            <% i++; %>
            <% } %>
        </tbody>
    </table>
    <% Html.RenderPartial("AjaxPager", ViewData["Pager2"]); %>
</div>
