<%@ Page Title="" Language="C#" ValidateRequest="false" MasterPageFile="~/Areas/Admin/Views/Shared/twocolumns.Master"
    Inherits="System.Web.Mvc.ViewPage<Main.Areas.Admin.Models.ControllerView.Catalog.CreateClass>" %>

<%@ Import Namespace="MainCommon" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainHeader" runat="server">
    <link href="<%= Url.Css("css/jquery/plugins/tokeninput/1_1/token-input.css") %>" rel="stylesheet" media="all" />
    <link href="<%= Url.Css("css/jquery/plugins/tokeninput/1_1/token-input-facebook.css") %>" rel="stylesheet" media="all" />
    <script type="text/javascript" src="<%= Url.JavaScript("jquery/plugins/tokeninput/1_1/jquery.tokeninput.js") %>"></script>
    <script src="<%= Url.JavaScript("fckeditor/2_6_6/fckeditor.js") %>" type="text/javascript"></script>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <% bool isSuperUser = (bool)(ViewData["isSuperUser"] ?? false); %>
    <div class="title title-spacing">
        <h2>
            Create Class
        </h2>
    </div>
    <% if (ViewMessageEnum.CreateSuccess == (ViewMessageEnum)(TempData["ViewMessage"] ?? ViewMessageEnum.None))
       { %>
    <div class="response-msg success ui-corner-all">
        <span>Create class success</span> You can create another class. Go to <a href="<%= Url.Action("Index") %>">catalog</a>.
    </div>
    <% } %>
    <%= Html.ValidationSummary(true, "Creating Class was unsuccessful. Please correct the errors and try again.", new { @class = "response-msg error ui-corner-all" })%>
    <div class="portlet ui-widget ui-widget-content ui-helper-clearfix ui-corner-all form-container">
        <div class="portlet-header ui-widget-header">
            Add Class</div>
        <div class="portlet-content">
            <% using (Html.BeginForm("CreateClass", "Catalog", new { area = "Admin" }, FormMethod.Post, new { enctype = "multipart/form-data" }))
               {%>
            <table>
                <tr>
                    <td style="width: 75%">
                        <% Html.RenderPartial("EditClassForm", Model); %>
                    </td>
                    <% if (isSuperUser)
                       { %>
                    <td style="width: 25%; padding-left: 2em">
                        <div class="page-title ui-widget-content ui-corner-all">
                            <h1>
                                Select portals</h1>
                            <div class="other hastable">
                                <% Html.RenderPartial("PortalList", Model.InPortals); %>
                            </div>
                        </div>
                    </td>
                    <% } %>
                </tr>
            </table>
            <input type="submit" style="display:none;" />
            <a class="btn ui-state-default" href="javascript:void(0);" onclick="$(this).prev().click();"><span class="ui-icon ui-icon-circle-plus"></span>Create Class</a>&nbsp;&nbsp;

            <a class="btn ui-state-default" href="<%= Url.Action("Index") %>"><span class="ui-icon ui-icon-circle-close">
            </span>Cancel</a>
            <% } %>
        </div>
    </div>
    
</asp:Content>
