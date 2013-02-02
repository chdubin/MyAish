<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/twocolumns.Master"
    Inherits="System.Web.Mvc.ViewPage<Main.Areas.Admin.Models.Portal.EditModel>" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
	<div class="title title-spacing">
	    <h2>Edit Branch</h2>
        <%= Html.ValidationSummary(true, "Update was unsuccessful. Please correct the errors and try again.", new { @class = "response-msg error ui-corner-all" })%>
        <% if (TempData["success"] != null && Convert.ToBoolean(TempData["success"]) == true)
           { %>
           <div class="response-msg success ui-corner-all"><span>Branch has been successfully updated.</span></div>
        <%} %>
	</div>
	<div class="portlet ui-widget ui-widget-content ui-helper-clearfix ui-corner-all form-container">
		<div class="portlet-header ui-widget-header">Edit Branch</div>
		<div class="portlet-content">
        <% using (Html.BeginForm("Edit", "Portal", new { portal_id = Model.PortalID }, FormMethod.Post, new { @class = "forms" }))
           { 
                Html.RenderPartial("EditableListItemEdit", Model); 
        %>
        
        <input type="submit" value=" Update " class="btn ui-state-default" />&nbsp;&nbsp;
        <a class="btn ui-state-default" href="<%= Url.Action("Index") %>"><span class="ui-icon ui-icon-circle-close"></span>Cancel</a>
        <%} %>
        </div>
    </div>
    <div class="title title-spacing">
        <div class="button float-right"><%=Html.ActionLink2(@"<span class=""ui-icon ui-icon-suitcase""></span><span style=""white-space: nowrap;"">Create New Administrator</span>", "CreateUser", "User", new { portal_id = Model.PortalID }, new { @class = "btn ui-state-default" })%></div>
	    <h2>Administrators</h2>
	</div>
    <%=Html.Action("Administrators", new { portal_id = Model.PortalID }) %>
</asp:Content>
