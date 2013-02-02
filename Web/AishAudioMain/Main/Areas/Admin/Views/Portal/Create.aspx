<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/twocolumns.Master"
    Inherits="System.Web.Mvc.ViewPage<Main.Areas.Admin.Models.Portal.EditModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<div class="title title-spacing">
	    <h2>Add Branch</h2>
        <%= Html.ValidationSummary(true, "Saving was unsuccessful. Please correct the errors and try again.", new { @class = "response-msg error ui-corner-all" })%>
	</div>
	<div class="portlet ui-widget ui-widget-content ui-helper-clearfix ui-corner-all form-container">
		<div class="portlet-header ui-widget-header">Add Branch</div>
		<div class="portlet-content">
        <% using (Html.BeginForm("Create", "Portal", FormMethod.Post, new { @class = "forms" }))
           { %>
        <% Html.RenderPartial("EditableListItemEdit", Model); %>

        <input type="submit" value=" Save " class="btn ui-state-default" />&nbsp;&nbsp;
        <a class="btn ui-state-default" href="<%= Url.Action("Index") %>"><span class="ui-icon ui-icon-circle-close"></span>Cancel</a>
        <%} %>
        </div>
    </div>
</asp:Content>
