<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/twocolumns.Master"
    Inherits="System.Web.Mvc.ViewPage<IEnumerable<Main.Areas.Admin.Models.Catalog.CatalogItem>>" %>

    <%@ Import Namespace="MainEntity.Models.Catalog" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

<style type="text/css">
    div.dwnld
    {
        float:left;
        line-height:30px;
    }
</style>

    <div class="title title-spacing">
        <h2>
            &laquo;<%= ViewData["PortalName"] ?? "No portal name" %>&raquo; products
        </h2>
    </div>
    <div class="clearfix">
    </div>
    <div class="hastable">
        <% Html.RenderAction("ClassFilter", "Catalog"); %>
        <% var rv = new RouteValueDictionary { { "area", "Admin" } };
           foreach (string key in Request.QueryString.AllKeys)
               rv.Add(key, Request.QueryString[key]);
        %>
        <% using (Html.BeginForm("SavePortalClasses", "Portal", rv, FormMethod.Post))
           { %>
        <table>
            <thead>
                <tr>
                    <th>
                        In portal
                    </th>
                    <th class="header <%= ViewData["SortTitleHeaderClass"] %>">
                        Title <a href="<%= ViewData["SortTitleUrl"] %>"><span class="ui-icon ui-icon-carat-2-n-s">
                        </span></a>
                    </th>
                    <th class="header">
                        Code
                    </th>
                    <th class="header <%= ViewData["SortDateHeaderClass"] %>">
                        CreateDate <a href="<%= ViewData["SortDateUrl"] %>"><span class="ui-icon ui-icon-carat-2-n-s">
                        </span></a>
                    </th>
                    <th class="header <%= ViewData["SortActiveHeaderClass"] %>">
                        Active <a href="<%= ViewData["SortActiveUrl"] %>"><span class="ui-icon ui-icon-carat-2-n-s">
                        </span></a>
                    </th>
                    <th class="header <%= ViewData["SortVisibleHeaderClass"] %>">
                        Visible <a href="<%= ViewData["SortVisibleUrl"] %>"><span class="ui-icon ui-icon-carat-2-n-s">
                        </span></a>
                    </th>
                    <th class="header <%= ViewData["SortFreeHeaderClass"] %>">
                        Free <a href="<%= ViewData["SortFreeUrl"] %>"><span class="ui-icon ui-icon-carat-2-n-s">
                        </span></a>
                    </th>
                    <th class="header <%= ViewData["SortFreeOfferHeaderClass"] %>">
                        Free offer <a href="<%= ViewData["SortFreeOfferUrl"] %>"><span class="ui-icon ui-icon-carat-2-n-s">
                        </span></a>
                    </th>
                    <th>
                        notes
                    </th>
                    <th>
                    </th>
                </tr>
            </thead>
            <tbody>
                <% int i = 0; %>
                <% foreach (var item in Model)
                   { %>
                <tr>
                    <td>
                        <%= Html.CheckBox("CatalogItems[" + i + "].Selected", item.Selected) %>
                    </td>
                    <td>
                        <%= Html.Hidden("CatalogItems[" + i + "].CatalogItemID", item.CatalogItemID) %>
                        <%= Html.Encode(item.Title)%>
                    </td>
                    <td>
                        <%= Html.Encode(item.Code) %>
                    </td>
                    <td>
                        <%= Html.Encode(String.Format("{0:d}", item.CreateDate)) %>
                    </td>
                    <td>
                        <%= Html.CheckBox("CatalogItems[" + i + "].Active", item.Active) %>
                    </td>
                    <td>
                        <%= Html.CheckBox("CatalogItems[" + i + "].Visible", (item.Visible.HasValue ? item.Visible.Value : false)) %>
                    </td>
                    <td>
                        <% if (item.TypeID == MainCommon.EntityItemTypeEnum.ClassItem)
                           { %>
                        <%= Html.CheckBox("CatalogItems[" + i + "].IsFree", (item.IsFree.HasValue ? item.IsFree.Value : false)) %>
                        <% } %>
                    </td>
                    <td>
                        <% if (item.TypeID == MainCommon.EntityItemTypeEnum.ClassItem)
                           { %>
                        <%= Html.CheckBox("CatalogItems[" + i + "].IsFreeOffer", (item.IsFreeOffer.HasValue ? item.IsFreeOffer.Value : false)) %>
                        <% } %>
                    </td>
                    <td>
                        <%= Html.TextArea("CatalogItems[" + i + "].Notes",item.Notes, new { @class = "field textarea full" }) %>
                    </td>
                    <td>
                        <% if (item.TypeID == MainCommon.EntityItemTypeEnum.ClassItem)
                           { %>
                        <a href="<%=   
                            Url.Action("EditClass", "Catalog",  new { catalog_item_id = item.CatalogItemID, CancelUrl = HttpUtility.UrlEncode(Request.Url.ToString()) }) %>" class="btn_no_text btn ui-state-default ui-corner-all tooltip"
                            title="Edit this class"><span class="ui-icon ui-icon-wrench"></span></a><a href="<%= Url.Action("DeleteClass", "Catalog",  new { catalog_item_id = item.CatalogItemID }) %>"
                                class="btn_no_text btn ui-state-default ui-corner-all tooltip" title="Delete this class"
                                onclick="return confirm('Are you sure to delete this class?');"><span class="ui-icon ui-icon-trash">
                                </span></a>
                        <% }
                           else
                           { %>
                        <a href="<%=   
                            Url.Action("EditPackage","Catalog",  new { catalog_item_id = item.CatalogItemID, CancelUrl = HttpUtility.UrlEncode(Request.Url.ToString()) }) %>"
                            class="btn_no_text btn ui-state-default ui-corner-all tooltip" title="Edit this package">
                            <span class="ui-icon ui-icon-wrench"></span></a><a href="<%= Url.Action("DeletePackage", "Catalog",  new { catalog_item_id = item.CatalogItemID }) %>"
                                class="btn_no_text btn ui-state-default ui-corner-all tooltip" title="Delete this package"
                                onclick="return confirm('Are you sure to delete this package?');"><span class="ui-icon ui-icon-trash">
                                </span></a>
                        <% } %>




                        
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
                <% i++; %>
                <% } %>
            </tbody>
        </table>
        <div style="float: right; margin-top: -55px">
            <input type="submit" style="display: none" />
            <a class="btn ui-state-default" href="#" onclick="$(this).prev().click();return false;">
                <span class="ui-icon ui-icon-disk"></span>Update</a>
        </div>
        <% } %>
        <div style="float: left; margin-top: -70px">
            <% Html.RenderPartial("Pager", ViewData["Pager"]); %>
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

<asp:Content ID="Content1" ContentPlaceHolderID="rightTop" runat="server">
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