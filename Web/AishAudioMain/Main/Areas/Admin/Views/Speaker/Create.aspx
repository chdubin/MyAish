<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/twocolumns.Master" 
Inherits="System.Web.Mvc.ViewPage<Main.Areas.Admin.Models.Speaker.SpeakerEditModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<div class="title title-spacing">
	    <h2>Add Speaker</h2>
	</div>
        <%= Html.ValidationSummary(true, "Create was unsuccessful. Please correct the errors and try again.", new { @class = "response-msg error ui-corner-all" })%>
        <% if (TempData["success"] != null && Convert.ToBoolean(TempData["success"]) == true)
           { %>
           <div class="response-msg success ui-corner-all"><span>Speaker has been successfully created.</span></div>
        <%} %>
	<div class="portlet ui-widget ui-widget-content ui-helper-clearfix ui-corner-all form-container">
		<div class="portlet-header ui-widget-header">Add Speaker</div>
		<div class="portlet-content">
        <% using (Html.BeginForm("Create", "Speaker", FormMethod.Post, new { @class = "forms", enctype = "multipart/form-data" }))
           { %>
        <% Html.RenderPartial("EditableListItemEdit", Model); %>

        <input type="submit" style="display:none;" />
        <a class="btn ui-state-default" href="javascript:void(0);" onclick="$(this).prev().click();"><span class="ui-icon ui-icon-circle-plus"></span>Create Speaker</a>&nbsp;&nbsp;
        <a class="btn ui-state-default" href="<%= Url.Action("Index") %>"><span class="ui-icon ui-icon-circle-close"></span>Cancel</a>
        <%} %>
        </div>
    </div>
</asp:Content>

