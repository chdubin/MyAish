<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/twocolumns.Master" 
Inherits="System.Web.Mvc.ViewPage<IEnumerable<Main.Areas.Admin.Models.Catalog.CatalogItem>>" %>

<%@ Import Namespace="MainEntity.Models.Catalog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<style type="text/css">
    div.dwnld
    {
        float:left;
        line-height:30px;
    }
</style>

	<div class="title title-spacing">
        <h2>Classes</h2>
        <% //if (TempData["success"] != null && Convert.ToBoolean(TempData["success"]) == true)
           //{ %>
           <!-- div class="response-msg success ui-corner-all" span Speaker has been successfully saved. /span /div -->
        <%//} %>
    </div>
    <div class="hastable">
        <table>
            <thead>
                <tr>
                    <th class="header">
                        Active
                    </th>
                    <th class="header">
                        Title
                    </th>
                    <th class="header">
                        Code
                    </th>
                    <th class="header">
                        CreateDate
                    </th>
                    <th>
                        Options
                    </th>
                </tr>
            </thead>
            <tbody>
            <% foreach (var item in Model)
               { %>
                <tr id="<%= item.CatalogItemID + "_tr" %>">
                    <td>
                        <%=Html.CheckBoxFor(a => item.Active, new { disabled = "disabled" })%>
                    </td>
                    <td>
                        <%=Html.Encode(item.Title) %>
                    </td>
                    <td>
                        <%=Html.Encode(item.Code) %>
                    </td>
                    <td>
                        <%=Html.Encode(item.CreateDate.ToString("d"))%>
                    </td>
                    <td>
                        <a href="<%= Url.Action("EditClass", "Catalog",  new { catalog_item_id = item.CatalogItemID,
                            CancelUrl = HttpUtility.UrlEncode(Request.Url.ToString()) }) %>" class = "btn_no_text btn ui-state-default ui-corner-all tooltip" 
                        title="Edit this class">
                            <span class="ui-icon ui-icon-wrench"></span>
                        </a>


                          <% if (item.DownloadFileItem != null)
                           { %>
                           <div class="dwnld">(
                           <% if(!string.IsNullOrEmpty(item.DownloadFileItem.filePath)) { %>
                            <a title="Listen in High Quality" href="#" onclick="return streamPopupflex('<%= Url.Action("GetStream", "Audio") + "?id=" + item.DownloadFileItem.fileID %>',300,50)">HI</a>
                           <% } %>

                           <% if(!string.IsNullOrEmpty(item.DownloadFileItem.alternateFilePath)) { %>
                            <a title="Listen in Low Quality" href="#" onclick="return streamPopupflex('<%= Url.Action("GetStream", "Audio") + "?low=true&id=" + item.DownloadFileItem.fileID %>',300,50)">Low</a>
                           <% } %>
                           |
                           <% if(!string.IsNullOrEmpty(item.DownloadFileItem.filePath)) { %>
                            <a  title="Download in High Quality" target="_blank" href="<%= Url.Action("GetAudio", "Audio") + "?id=" + item.DownloadFileItem.fileID %>">HI</a>
                           <% } %>

                           <% if(!string.IsNullOrEmpty(item.DownloadFileItem.alternateFilePath)) { %>
                            <a  title="Download in Low Quality" target="_blank" href="<%= Url.Action("GetAudio", "Audio") + "?low=true&id=" + item.DownloadFileItem.fileID %>">Low</a>
                           <% } %>)
                           </div>
                        <% } %>


                    </td>
                </tr>                        

            <%} %>
            </tbody>
        </table>
        <div style="margin-top: -70px">
            <% if (Model.Count() > 0) { Html.RenderPartial("Pager", ViewData["Pager"]); } %>
        </div>

    </div>

     <script type="text/javascript">

         function streamPopupflex(page, width, height) {
             self.name = "TMmain";
             options = "toolbar=0,status=0,menubar=0,scrollbars=0,resizable=0,width=" + width + ",height=" + height;
             window.open(page, "TMPlayer", options);

             return false;
         }

    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="rightTop" runat="server">
    <div class="portlet ui-widget ui-widget-content ui-helper-clearfix ui-corner-all">
        <div class="portlet-header ui-widget-header">
            Category filter</div>
        <div class="portlet-content">
            <ul class="side-menu">
                <% foreach (vw_Category c in (vw_Category[])ViewData["Categories"])
                   { %>
                <li>
                    <a href="<%= (string)ViewData["ClearFilterPath"] + "category_id=" + c.tagID %>" <%= (int)ViewData["CurrentCategoryID"] == c.tagID ? "style='font-weight:bold'" : string.Empty %>><%= Html.Encode(c.name) %></a>
                </li>
                <% } %>
            </ul>
        </div>
    </div>
</asp:Content>