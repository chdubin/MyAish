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

   <%-- <div style="margin-bottom:10px;background-color:#D0E9ED;">
        <% Html.RenderPartial("SpeakerFilter", ViewData["Filter"]); %>
    </div>--%>
        
    <table cellpadding="2" cellspacing="0" style="width:100%;">
    <tr>
    <td style="vertical-align:baseline;">
        Total Records Found: <%= ViewData["TotalCount"] %>
    </td>
    <td style="vertical-align:baseline;">
        <% if (Model.Length > 0) { Html.RenderPartial("Pager", ViewData["Pager"]); }%>
    </td>
    <td style="text-align:right;" align="right">
        <a class="btn ui-state-default" href="<%= Url.Action("Index", "Speaker", new RouteValueDictionary { { "downloadtype","text" }, { "ps", ((Main.Areas.Admin.Models.Common.PagingData)ViewData["Pager"]).PageSize } } ) %>"><span class="ui-icon ui-icon-circle-plus"></span>Download as Text</a>
        <a class="btn ui-state-default" href="<%= Url.Action("Index", "Speaker", new RouteValueDictionary { { "downloadtype","excel" }, { "ps", ((Main.Areas.Admin.Models.Common.PagingData)ViewData["Pager"]).PageSize } } ) %>"><span class="ui-icon ui-icon-circle-plus"></span>Download All as Excel 2007</a>
    </td>
    </tr>
    </table>
                
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
                    <th class="header <%= ViewData["SortActiveHeaderClass"] %>">Active <a href="<%= ViewData["SortActiveUrl"] %>"><span class="ui-icon ui-icon-carat-2-n-s"></span></a>
                    </th>
                    <th>
                        <div class="button float-right">
                            <a class="btn ui-state-default" style="margin:0px" href="<%= Url.Action("Create") %>"><span class="ui-icon ui-icon-circle-plus"></span>Create New Speaker</a>
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
<%--        <div style="margin-top: -70px">
            <% if (Model.Length > 0) { Html.RenderPartial("Pager", ViewData["Pager"]); }%>
        </div>--%>

    </div>
</asp:Content>
