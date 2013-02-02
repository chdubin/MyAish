<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Main.Areas.Admin.Models.ControllerView.Catalog.AddProductsToPackage>" %>

<label for="title">Title:</label>
<%= Html.TextBoxFor(model => model.TitleFilter, new { @id = "title" })%>

<label for="speaker">Speaker:</label>
<%= Html.TextBoxFor(model => model.SpeakerFilter, new { @id = "speaker" })%>

<%= Ajax.ActionLink2("<span class='ui-icon ui-icon-search'></span>Filter", "AddProductsToPackage", new { random = DateTime.Now.Ticks },
    new AjaxOptions()
    {
        HttpMethod = "GET",
        InsertionMode = InsertionMode.Replace,
        UpdateTargetId = "addProductsToPackage",
        OnBegin = "onBegin"
    },
    new { @class = "btn ui-state-default" })%>

<br /><br />

<div class="other hastable">
    <table id="classesListTable" style="margin-bottom:0px">
        <thead>
            <tr>
                <th></th>
                <th>Title</th>
                <th>Speaker</th>
                <th>Date</th>
            </tr>
        </thead>
        <tbody>
        <% foreach (var c in Model.Classes)
           {%>
                <tr>
                    <td>
                        <% if (Model.SelectedProductsIds.Contains(c.Product.productID))
                           { %>
                            <input id="selected_<%= c.Product.productID %>" checked="checked" type="checkbox" onchange="onChange(this)" />  
                        <% }
                           else
                           { %>
                            <input id="selected_<%= c.Product.productID %>" type="checkbox" onchange="onChange(this)" />  
                        <% } %>
                    </td>
                    <td>
                        <%= c.EntityItem.title%>
                    </td>
                    <td>
                        <%= c.SpeakerEntityItem.title %>
                    </td>
                    <td>
                        <%= c.EntityItem.createDate.ToString("MM/dd/yyyy") %>
                    </td>
                </tr>
         <%} %>
         </tbody>
    </table>
</div>

<% Html.RenderPartial("DefaultPager", ViewData["Pager"]); %>
<div style="float:right;padding-top:12px">
    <%= Ajax.ActionLink2("<span class='ui-icon ui-icon-circle-check'></span>OK", "ProductsInPackage", null,
        new AjaxOptions()
        {
            HttpMethod = "POST",
            InsertionMode = InsertionMode.Replace,
            UpdateTargetId = "classesInPackage",
            OnBegin = "onBeginClassesInPackage",
            OnSuccess = "function() { $('#addProductsToPackage').dialog('close'); }"
        },
        new { @class = "btn ui-state-default" })%>
    
    <a href="javascript:void(0);" class="btn ui-state-default" onclick="$('#addProductsToPackage').dialog('close');return false;"><span class="ui-icon ui-icon-circle-close"></span>Cancel</a>
</div>

<script type="text/javascript">
    function onBegin(ajaxContext) {
        var url = ajaxContext.get_request().get_url();
        var title = $("#title").attr("value");
        var speaker = $("#speaker").attr("value");
        var toAddIds = $("#ProductsToAdd").attr("value");
        var toDeleteIds = $("#ProductsToDelete").attr("value");
        ajaxContext.get_request().set_url(url + "&title=" + title + "&speaker=" + speaker + "&products_to_add=" + toAddIds + "&products_to_delete=" + toDeleteIds);
    }

    function onBeginClassesInPackage(ajaxContext) {
        var url = ajaxContext.get_request().get_url();
        var idsToAdd = $("#ProductsToAdd").attr("value") + $("#toAdd").attr("value");
        var idsToDelete = $("#ProductsToDelete").attr("value") + $("#toDelete").attr("value");
        $("#ProductsToAdd").attr("value", idsToAdd);
        $("#ProductsToDelete").attr("value", idsToDelete);
        ajaxContext.get_request().set_body("&products_to_add=" + idsToAdd + "&products_to_delete=" + idsToDelete);
    }

    function onChange(cb) {
        var id = cb.id.substring(9);
        if (cb.checked) {
            var idsToAdd = $("#toAdd").attr("value");
            idsToAdd = idsToAdd + id + ",";
            $("#toAdd").attr("value", idsToAdd)
        }
        else {
            var idsToDelete = $("#toDelete").attr("value");
            idsToDelete = idsToDelete + id + ",";
            idsToDelete = $("#toDelete").attr("value", idsToDelete);
        }
    }

    function cancel() {
        $("#toAdd").attr("value", "");
        $("#toDelete").attr("value", "");
    }
</script>



