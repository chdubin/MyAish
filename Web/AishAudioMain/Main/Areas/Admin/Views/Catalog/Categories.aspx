<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/twocolumns.Master"
    Inherits="System.Web.Mvc.ViewPage<MainEntity.Models.Catalog.vw_Category[]>" %>

<%@ Import Namespace="MainCommon" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div class="title title-spacing">
        <div class="button float-right">
            <a class="btn ui-state-default" style="margin: 0" href="#" onclick="return openCreateCategoryDialog()">
                <span class="ui-icon ui-icon-document"></span>Create New Category </a>
        </div>
        <h2>
            Categories
        </h2>
    </div>
    <% if (!string.IsNullOrEmpty((string)(TempData["ViewErrorType"] ?? string.Empty)))
       { %>
    <div class="response-msg error ui-corner-all">
        <span>
            <%= TempData["ViewErrorType"] %></span><%= TempData["ViewErrorMessage"] %>
    </div>
    <% }
       else if (ViewMessageEnum.CreateSuccess == (ViewMessageEnum)(TempData["ViewMessage"] ?? ViewMessageEnum.None))
       { %>
    <div class="response-msg success ui-corner-all">
        <span>Create category success</span> You category created successfully.
    </div>
    <% }
       else if (ViewMessageEnum.DeleteSuccess == (ViewMessageEnum)(TempData["ViewMessage"] ?? ViewMessageEnum.None))
       { %>
    <div class="response-msg success ui-corner-all">
        <span>Delete category success</span> You category deleted successfully.
    </div>
    <% }
       else if (ViewMessageEnum.UpdateSuccess == (ViewMessageEnum)(TempData["ViewMessage"] ?? ViewMessageEnum.None))
       { %>
    <div class="response-msg success ui-corner-all">
        <span>Update category success</span> You category updated successfully.
    </div>
    <% }
       else if (ViewMessageEnum.DeleteError == (ViewMessageEnum)(TempData["ViewMessage"] ?? ViewMessageEnum.None))
       { %>
    <div class="response-msg error ui-corner-all">
        <span>Delete category error</span> You can't delete this category.
    </div>
    <% } %>
    <div class="hastable">
        <table>
            <thead>
                <tr>
                    <th class="header">
                        ID
                    </th>
                    <th class="header">
                        Name
                    </th>
                    <th>
                        Products cnt
                    </th>
                    <th>
                    </th>
                </tr>
            </thead>
            <tbody>
                <% foreach (MainEntity.Models.Catalog.vw_Category category in Model)
                   { %>
                <tr>
                    <td>
                        <%= category.tagID%>
                    </td>
                    <td>
                        <%= Html.Encode(category.name)%>
                    </td>
                    <td>
                        <%= category.count %>
                    </td>
                    <td>
                        <a href="#" onclick="return openUpdateCategoryDialog(<%= category.tagID %>,'<%= category.name %>')"
                            class="btn_no_text btn ui-state-default ui-corner-all tooltip" title="Edit category">
                            <span class="ui-icon ui-icon-wrench"></span></a><a href="<%= Url.Action("DeleteCategory", new { category_id = category.tagID }) %>"
                                class="btn_no_text btn ui-state-default ui-corner-all tooltip" title="Delete this category"
                                onclick="return confirm('Are you sure to delete this category?');"><span class="ui-icon ui-icon-trash">
                                </span></a>

                                <a href="<%= Url.Action("Index", new { category_id = category.tagID }) %>"
                                class="btn_no_text btn ui-state-default ui-corner-all tooltip" title="Filter products"
                                ><span class="ui-icon ui-icon-folder-open">
                                </span></a>
                    </td>
                </tr>
                <% } %>
            </tbody>
        </table>
    </div>
    <div style="display: none">
        <% using (Html.BeginForm("CreateCategory", "Catalog", FormMethod.Post, new { id = "createCategory", title = "Create new category" }))
           { %>
        <label class="desc" for="category_name">
            Category name
        </label>
        <div>
            <input type="text" id="category_name" name="category_name" class="field text full" />
        </div>
        <input style="display: none" type="submit" />
        <% } %>
        <% using (Html.BeginForm("UpdateCategory", "Catalog", FormMethod.Post, new { id = "updateCategory", title = "Edit category" }))
           { %>
        <input type="hidden" id="category_id" name="category_id" />
        <label class="desc" for="category_name_update">
            Category name
        </label>
        <div>
            <input type="text" id="category_name_update" name="category_name" class="field text full" />
        </div>
        <input style="display: none" type="submit" />
        <% } %>
    </div>
    <script type="text/javascript">
        $(function () {
            $('#createCategory').dialog({
                width: 400,
                autoOpen: false,
                modal: true,
                buttons: {
                    'Cancel': function () {
                        $(this).dialog('close');
                    },
                    'Create': function () {
                        $('#createCategory :submit').click();
                    }
                }
            });

            $('#updateCategory').dialog({
                width: 400,
                autoOpen: false,
                modal: true,
                buttons: {
                    'Cancel': function () {
                        $(this).dialog('close');
                    },
                    'Save': function () {
                        $('#updateCategory :submit').click();
                    }
                }
            });
        });

        function openCreateCategoryDialog() {
            $('#createCategory').dialog('open');
            return false;
        }

        function openUpdateCategoryDialog(c_id, c_name) {
            $('#updateCategory #category_id').val(c_id);
            $('#updateCategory #category_name_update').val(c_name);
            $('#updateCategory').dialog('open');
            return false;
        }
    </script>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainHeader" runat="server">
</asp:Content>
