<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/twocolumns.Master" Inherits="System.Web.Mvc.ViewPage<Main.Areas.Admin.Models.User.UserEditModel>" %>

<asp:Content ID="Header1" ContentPlaceHolderID="MainHeader" runat="server">
    <link href="<%= Url.Css("css/jquery/plugins/datepicker/3_7_5/smoothness.datepick.css") %>" rel="stylesheet" media="all" />
    <script type="text/javascript" src="<%= Url.JavaScript("jquery/plugins/datepicker/3_7_5/jquery.datepick.js") %>"></script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<div class="title title-spacing">
	    <h2>Edit user info</h2>
	</div>
        <%= Html.ValidationSummary(true, "Update was unsuccessful. Please correct the errors and try again.", new { @class = "response-msg error ui-corner-all" })%>
        <% if (TempData["success"] != null && Convert.ToBoolean(TempData["success"]) == true)
           { %>
           <div class="response-msg success ui-corner-all"><span>User info has been successfully updated.</span></div>
        <%} %>

        <% if (TempData["created"] != null && Convert.ToBoolean(TempData["created"]) == true)
           { %>
           <div class="response-msg success ui-corner-all"><span>User has been successfully created.</span></div>
        <%} %>
              
<%--    <div class="portlet ui-widget ui-widget-content ui-helper-clearfix ui-corner-all form-container">
    	<div class="portlet-header ui-widget-header">Edit user info</div>
--%>		<div class="portlet-content">
            <div id="tabs2">
	            <ul>
		            <li><a href="#tabs-1">Personal Information</a></li>
		            <%--<li><a href="#tabs-2">Paid-Ahead Information</a></li>--%>
                    <li><a href="#tabs-2">Select roles</a></li>
	            </ul>

                <% using (Html.BeginForm("EditUserInfo", "User", FormMethod.Post,
                    new { @class = "forms", enctype = "multipart/form-data" }))
                    { 
                %>            
                    <% Html.RenderPartial("EditUserInfoForm", Model); %>
                    <input type="submit" value="Update" style="display:none;" />
                <% } %>
            </div>


            <a class="btn ui-state-default" href="javascript:void(0);" onclick="$('#tabs2').find('form').submit();"><span class="ui-icon ui-icon-circle-check"></span>Update</a>&nbsp;&nbsp;
            <a class="btn ui-state-default" href="<%= Url.Action("Index") %>"><span class="ui-icon ui-icon-circle-close"></span>Cancel</a>

        </div>
<%--    </div>--%>
</asp:Content>

