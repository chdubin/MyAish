<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/twocolumns.Master" 
Inherits="System.Web.Mvc.ViewPage<MainEntity.Models.Speaker.EntityItem[]>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<div class="title title-spacing">
        <h2>Speakers</h2>
        <% //if (TempData["success"] != null && Convert.ToBoolean(TempData["success"]) == true)
           //{ %>
           <!-- div class="response-msg success ui-corner-all" span Speaker has been successfully saved. /span /div -->
        <%//} %>
    </div>
    
    <% Html.RenderPartial("Pager", ViewData["Pager"]); %>                    
    
    <div class="hastable">
        <table>
            <thead>
                <tr>
                    <th class="header <%= ViewData["SortTitleHeaderClass"] %>">
                        Title <a href="<%= ViewData["SortTitleUrl"] %>"><span class="ui-icon ui-icon-carat-2-n-s"></span></a>
                    </th>
                    <th class="header <%= ViewData["SortDateHeaderClass"] %>">
                        CreateDate <a href="<%= ViewData["SortDateUrl"] %>"><span class="ui-icon ui-icon-carat-2-n-s"></span></a>
                    </th>
                    <th class="header <%= ViewData["SortActiveHeaderClass"] %>">
                        Active <a href="<%= ViewData["SortActiveUrl"] %>"><span class="ui-icon ui-icon-carat-2-n-s"></span></a>
                    </th>
                    <th>
        <div class="button float-right">
            <a class="btn ui-state-default" style="margin:0px" href="<%= Url.Action("Create") %>"><span class="ui-icon ui-icon-circle-plus">
            </span>Create New Speaker</a>
        </div>
                    </th>
                </tr>
            </thead>
            <tbody>
            <% foreach (var item in Model)
               { %>
            <% Html.RenderPartial("EditableListItem", new Main.Areas.Admin.Models.Speaker.SpeakerEditModel(item)); %>
            <%} %>
            </tbody>
        </table>
        <div style="margin-top: -70px">
            <% if (Model.Length > 0) { Html.RenderPartial("Pager", ViewData["Pager"]); }%>
        </div>

    </div>
</asp:Content>
