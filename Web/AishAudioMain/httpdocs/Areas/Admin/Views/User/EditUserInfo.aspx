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

<asp:Content ID="additionalNav" ContentPlaceHolderID="rightAdditionalNav" runat="server">
    <div>
        <h3><a href="#">User Options</a></h3>
            <div>
                <ul class="side-menu">
                    <li><a href="<%= Url.Action("EditUserInfo","User",  new { user_id = Model.UserID }) %>" title="Edit User">Edit User Info</a></li>
                    <li><%=Html.ActionLink("View Files", "ClassActivityLog", new { user_id = Model.UserID })%></li>
                    <li><a href="<%= Url.Action("ViewFiles","User",  new { user_id = Model.UserID })%>" title="">Add Downloads</a></li>
                    <li><a href="<%= Url.Action("ViewShoppingTransactions", new { user_id = Model.UserID }) %>" title="">View Transactions</a></li>
                    <li style="border-bottom: none;">&nbsp;</li>
                    <li><%=Html.ActionLink("Change CC", "ChangeCreditCard", new { user_id = Model.UserID })%></li>
                    <li style="border-bottom: none;">&nbsp;</li>
                    <li>
                        <%--<a href="<= Url.Action("PlaceOrder","User",  new { user_id = Model.UserID }) >">--%>
                            Place Order
                        <%--</a> --%>
                    </li>
                    <li>
                        <%--<a href="<= Url.Action("EnterReturn","User",  new { user_id = Model.UserID }) >">--%>
                            Enter Return
                        <%--</a> --%>
                    </li>
                    <li style="border-bottom: none;">&nbsp;</li>
                    <li>
                            <% if (Model.PlanID != 0)
                               { %>

                            <a href="<%= Url.Action("CancelMembership","User",  new { user_id = Model.UserID }) %>" onclick="return confirm('Are you sure to cancel membership for this user?');">
                                Cancel Membership
                            </a> 
                            <% }
                               else
                               { %>
                               Cancel Membership
                            <%} %>
                    </li>
                    <li><a href="<%= Url.Action("Delete","User", new { user_id = Model.UserID }) %>" onclick="return confirm('Are you sure to delete this user?');">Delete User</a></li>
                </ul>
            </div>
    </div>
</asp:Content>

