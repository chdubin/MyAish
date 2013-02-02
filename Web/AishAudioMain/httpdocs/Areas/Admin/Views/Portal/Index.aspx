<%@ Page Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/twocolumns.Master"
    Inherits="System.Web.Mvc.ViewPage<MainEntity.Models.Portal.PortalEntity[]>" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
	<div class="title title-spacing">
        <h2>Branches</h2>
        <% if (TempData["success"] != null && Convert.ToBoolean(TempData["success"]) == true)
           { %>
           <div class="response-msg success ui-corner-all"><span>Branch has been successfully saved.</span></div>
        <%} %>
    </div>
    <div class="hastable">
        <table>
            <thead>
                <tr>
                    <!-- th>id</th -->
                    <th>Name</th>
                    <th>Application Name</th>
                    <th>Selected Design</th>
                    <th>Create date</th>
                    <th>Domain Names</th>
                    <th>Active</th>
                    <th>        <div class="button float-right">
            <a class="btn ui-state-default" style="margin:0px" href="<%= Url.Action("Create") %>"><span class="ui-icon ui-icon-circle-plus">
            </span>Create New Branch</a>
        </div>
</th>
                </tr>
            </thead>
            <tbody>
            <% foreach (var item in Model)
               { %>
            <% Html.RenderPartial("EditableListItem", new Main.Areas.Admin.Models.Portal.EditModel(item)); %>
            <%} %>
            </tbody>
        </table>
    </div>
</asp:Content>
