<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Main.Areas.Admin.Models.ControllerView.Catalog.EditPackage>" %>

    <script type="text/javascript">

        $(document).ready(function () {

            var autocompleeteUrl = '<%= Url.Action("GetCategories") %>';
            var data = $("#edit-class-form-categories").val().split('|');
            var prePopulateData = [];
            for (var i in data) {
                if (data[i] != "")
                    prePopulateData[i] = { 'name': data[i] };
            }

            $("#edit-class-form-categories").tokenInput(autocompleeteUrl, {

                noResultsText: "No results",
                searchingText: "Searching...",

                classes: {
                    tokenList: "token-input-list-facebook",
                    token: "token-input-token-facebook",
                    tokenDelete: "token-input-delete-token-facebook",
                    selectedToken: "token-input-selected-token-facebook",
                    highlightedToken: "token-input-highlighted-token-facebook",
                    dropdown: "token-input-dropdown-facebook",
                    dropdownItem: "token-input-dropdown-item-facebook",
                    dropdownItem2: "token-input-dropdown-item2-facebook",
                    selectedDropdownItem: "token-input-selected-dropdown-item-facebook",
                    inputToken: "token-input-input-token-facebook"
                },

                //prePopulate: [{ 'name': 'House' }, { 'name': 'Desperate Housewives2' }, { 'name': 'Dollhouse' }, { 'name': 'Full House'}]
                prePopulate: prePopulateData

            });

        });

    </script>

<% bool isSuperUser = (bool)(ViewData["isSuperUser"] ?? false); 
   if (Model.PackageID != 0)
   { %>
<%= Html.HiddenFor(m => m.PackageID)%>
<% } %>
<ul>
    <%--<li>
        <div style="float: left; margin-right: 1em">
            <label class="desc" for="Active">Active</label>
            <div class="editor-field">
                <%= Html.CheckBoxFor(model => model.Active, new { @class = "field checkbox" })%>
            </div>
        </div>
    </li>--%>
     <% if (isSuperUser)
        { %>
    <li><span>
        <label class="desc">
            Active
        </label>
        <%= Html.CheckBoxFor(model => model.Active) %>
        </span>
    </li>
    <li>
        <span>
        <label class="desc">
            unlimitedAccessInLibrary
        </label>
        <%= Html.CheckBoxFor(model => model.unlimitedAccessInLibrary) %>
        </span>
    </li>
    <% }
        else
        { %>
    <li><span>
        <label class="desc">
            Visible
        </label>
        <%= Html.CheckBoxFor(m => m.IsVisible) %>
    </span></li>
    <% } %>
    <li>
        <label class="desc" for="Title">Title</label>
        <div>
            <%= Html.TextBoxFor(model => model.Title, new { @class = "field text large" })%>
        </div>
    </li>
    <li>
        <label class="desc" for="Description">Description</label>
        <div>
            <%= Html.TextAreaFor(model => model.Description, new { @id = "description", @class = "field textarea  small" })%>
        </div>
    </li>
    <% if (Model.PackageID == 0)
       { %>
    <li>
        <label class="desc" for="Image">
            Upload image</label>
        <div>
            <input type="file" name="Image" id="Image" />
        </div>
    </li>
    <% }
       else
       { %>
    <li>
        <% if (!string.IsNullOrEmpty(Model.ClassImagePath))
           { %>
        <img src="<%= ResolveClientUrl(Model.ClassImagePath) + "?_=" + (new Random().Next()) %>" />
        <% } %>
    </li>
    <li>
        <label class="desc" for="Image">
            Upload new image</label>
        <div>
            <input type="file" name="Image" id="Image" />
        </div>
    </li>
    <% } %>
    <li>
        <label class="desc" for="PriceUSD">Price in USD</label>
        <div>
            <%= Html.TextBoxFor(model => model.PriceUSD, new { @class = "field text large" })%>
        </div>
    </li>
        <li>
        <label class="desc" for="Code">
            Code</label>
        <div>
            <%= Html.TextBoxFor(model => model.Code, new { @class = "field text large"} )%>
        </div>
    </li>
    <li>
        <label class="desc" for="Categories">
            Categories</label>
        <div>
            <%= Html.TextBoxFor(model => model.Categories, new { @class = "field text small", id = "edit-class-form-categories" })%>
        </div>
    </li>
    <li>
        <span style="padding-right:2em">
            <%=Html.LabelFor(m=>m.SubscribePlanMonths) %>
            <%= Html.TextBoxFor(model => model.SubscribePlanMonths, new { @class = "field text small", id = "edit-class-form-categories" })%>
        </span>
        <span style="padding-right:2em">
            <%=Html.LabelFor(m=>m.FreeUnitsOnSubscribe) %>
            <%= Html.TextBoxFor(model => model.FreeUnitsOnSubscribe, new { @class = "field text small", id = "edit-class-form-categories" })%>
        </span>
        <span style="padding-right:2em">
            <%=Html.LabelFor(m=>m.FreeUnitsOnNextSubscribe) %>
            <%= Html.TextBoxFor(model => model.FreeUnitsOnNextSubscribe, new { @class = "field text small", id = "edit-class-form-categories" })%>
        </span>
    </li>
	<li>
		<label class="desc" for="SpeakerID">
			Shipping Location</label>
		<div>
			<%=Html.DropDownListFor(m => m.ShippingLocationID, Model.ShippingLocation, new { @class = "field select large" })%>
		</div>
	</li>
</ul>
<ul>
    <li>
        <%= Ajax.ActionLink2("<span class='ui-icon ui-icon-circle-plus'></span>Attach files from classes", "AddProductsToPackage", new { random = DateTime.Now.Ticks },
            new AjaxOptions() 
            { 
                HttpMethod="GET", 
                UpdateTargetId = "addProductsToPackage",
                OnBegin = "onBeginAddProductsToPackage",
                OnSuccess = "function() { $('#addProductsToPackage').dialog('open'); }"
            },
            new { @class = "btn ui-state-default" })%>
        <div id="classesInPackage">
            <% Html.RenderPartial("ProductsInPackage", new Main.Areas.Admin.Models.ControllerView.Catalog.ProductsInPackage(Model.Products)); %>
        </div>
    </li>
</ul>
<%--<%= Html.HiddenFor(model => model.EditMode, new { @id = "EditMode" }) %>--%>
<%=Html.HiddenFor(m=>m.ProductsToAdd,new{id="ProductsToAdd"}) %>
<%=Html.HiddenFor(m=>m.ProductsToDelete,new{id="ProductsToDelete"}) %>

<input id="toAdd" type="hidden" />
<input id="toDelete" type="hidden" />

<div style="display:none">
    <div id="addProductsToPackage" title="Add classes to package">
    </div>
</div>

<script type="text/javascript">
    $(function () {
        $('#addProductsToPackage').dialog({
            width: 600,
            autoOpen: false,
            modal: true,
            close: function (event, ui) { cancel(); }
        });
    });

    function onBeginAddProductsToPackage(ajaxContext) {
        var url = ajaxContext.get_request().get_url();
        var toAddIds = $("#ProductsToAdd").attr("value");
        var toDeleteIds = $("#ProductsToDelete").attr("value");
        ajaxContext.get_request().set_url(url + "&products_to_add=" + toAddIds + "&products_to_delete=" + toDeleteIds);
    }
</script>
<script type="text/javascript">
    var oFCKeditor = new FCKeditor("description");
    oFCKeditor.BasePath = "<%= Url.Content("~/Areas/Admin/Content/fckeditor/2_6_6/") %>";
    oFCKeditor.Width = 507;
    oFCKeditor.ReplaceTextarea();
</script>
