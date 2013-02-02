<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/twocolumns.Master"
    Inherits="System.Web.Mvc.ViewPage<Main.Areas.Admin.Models.ControllerView.User.ViewFiles>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        li
        {
            clear: none !important;
        }
    </style>
    <div class="title title-spacing">
    <h2>
        Class list</h2>
    </div>
    <div class="hastable">
        <% Html.RenderAction("ClassFilter", "Catalog"); %>
    </div>

    <% using (Html.BeginForm("UpdateViewFiles", "User", new { user_id = Model.UserID }))
       { %>
    <div id="other">
        <% Html.RenderAction("Classes", "User"); %>
    </div>

    <div class="title title-spacing">
        <h2>Classes in library</h2>
    </div><br />

    <div id="existing">
        <% Html.RenderAction("ClassesInLibrary", "User"); %>
    </div>
    <input type="submit" style="display: none;" />
    <a class="btn ui-state-default" href="#" onclick="$(this).prev().click();return false;">
        <span class="ui-icon ui-icon-disk"></span>Update</a>
    <% } %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainHeader" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="rightTop" runat="server">
</asp:Content>
