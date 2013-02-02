<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/twocolumns.Master" 
Inherits="System.Web.Mvc.ViewPage<Main.Areas.Admin.Models.Speaker.SpeakerEditModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<div class="title title-spacing">
	    <h2>Edit Speaker</h2>
	</div>
        <%= Html.ValidationSummary(true, "Update was unsuccessful. Please correct the errors and try again.", new { @class = "response-msg error ui-corner-all" })%>
        <% if (TempData["success"] != null && Convert.ToBoolean(TempData["success"]) == true)
           { %>
           <div class="response-msg success ui-corner-all"><span>Speaker has been successfully updated.</span></div>
        <%} %>
	<div class="portlet ui-widget ui-widget-content ui-helper-clearfix ui-corner-all form-container">
		<div class="portlet-header ui-widget-header">Edit Speaker</div>
		<div class="portlet-content">
        <% using (Html.BeginForm("Edit", "Speaker", new { speaker_id = Model.SpeakerID }, FormMethod.Post, new { @class = "forms", enctype = "multipart/form-data" }))
           { 
                Html.RenderPartial("EditableListItemEdit", Model); 
        %>
        
        <input type="submit" value="Save" style="display:none;" />
        <a class="btn ui-state-default" href="javascript:void(0);" onclick="$(this).prev().click();"><span class="ui-icon ui-icon-circle-check"></span>Update</a>&nbsp;&nbsp;
        <a class="btn ui-state-default" href="<%= Url.Action("Index") %>"><span class="ui-icon ui-icon-circle-close"></span>Cancel</a>
        <%} %>
        </div>
    </div>
</asp:Content>

