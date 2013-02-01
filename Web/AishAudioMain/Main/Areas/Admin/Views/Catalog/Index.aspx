<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/twocolumns.Master"
    Inherits="System.Web.Mvc.ViewPage<IEnumerable<Main.Areas.Admin.Models.Catalog.CatalogItem>>" %>

<%@ Import Namespace="MainCommon" %>
<%@ Import Namespace="MainEntity.Models.Catalog" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainHeader" runat="server">
    <link href="<%= Url.Css("css/jquery/plugins/tokeninput/1_1/token-input.css") %>" rel="stylesheet" media="all" />
    <link href="<%= Url.Css("css/jquery/plugins/tokeninput/1_1/token-input-facebook.css") %>" rel="stylesheet" media="all" />
    <script type="text/javascript" src="<%= Url.JavaScript("jquery/plugins/tokeninput/1_1/jquery.tokeninput.js") %>"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<style type="text/css">
    div.dwnld
    {
        float:left;
        line-height:30px;
    }
    .token-input-dropdown-facebook
    {
        width:200px !important;
    }
    .token-input-list-facebook
    {
        width:100% !important;
    }
</style>


    <div class="title title-spacing">
        <div class="button float-right">
            <a class="btn ui-state-default" style="margin: 0" href="<%= Url.Action("CreatePackage") %>">
                <span class="ui-icon ui-icon-suitcase"></span>Create New Package </a>
        </div>
        <div class="button float-right">
            <a class="btn ui-state-default" style="margin: 0 10px 0 0" href="<%= Url.Action("CreateClass") %>">
                <span class="ui-icon ui-icon-document"></span>Create New Class</a>
        </div>
        <h2>
            Class Catalog
        </h2>
    </div>
    <div class="clearfix">
    </div>
    <% if (ViewMessageEnum.UpdateSuccess == (ViewMessageEnum)(TempData["ViewMessage"] ?? ViewMessageEnum.None))
       { %>
    <div class="response-msg success ui-corner-all">
        <span>Update catalog success</span>
    </div>
    <% } %>
    <%= Html.ValidationSummary(true, "Creating Class was unsuccessful. Please correct the errors and try again.", new { @class = "response-msg error ui-corner-all" })%>
    <% bool isSuperUser = (bool)(ViewData["isSuperUser"] ?? false); %>
    <div class="hastable">
        <% var rv = new RouteValueDictionary { { "area", "Admin" } };
           foreach (string key in Request.QueryString.AllKeys)
               rv.Add(key, Request.QueryString[key]);
        %>
        <% Html.RenderAction("ClassFilter"); %>
        <% using (Html.BeginForm("SaveCatalogItems", "Catalog", rv, FormMethod.Post))
           { %>
            <% Html.RenderPartial("Pager", ViewData["Pager"]); %>                    

        <table>
            <thead>
                <tr>
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
                    <% if (isSuperUser)
                       { %>
                    <th class="header <%= ViewData["SortActiveHeaderClass"] %>">
                        Active <a href="<%= ViewData["SortActiveUrl"] %>"><span class="ui-icon ui-icon-carat-2-n-s">
                        </span></a>
                    </th>
                    <% }
                       //else
                       //{ 
                     %>
                    <th class="header <%= ViewData["SortVisibleHeaderClass"] %>">
                        Visible to Users <a href="<%= ViewData["SortVisibleUrl"] %>"><span class="ui-icon ui-icon-carat-2-n-s">
                        </span></a>
                    </th>
                    <th class="header <%= ViewData["SortFreeHeaderClass"] %>">
                        Free Streaming<a href="<%= ViewData["SortFreeUrl"] %>"><span class="ui-icon ui-icon-carat-2-n-s">
                        </span></a>
                    </th>
                    <th class="header <%= ViewData["SortFreeOfferHeaderClass"] %>">
                        Free Download Offer <a href="<%= ViewData["SortFreeOfferUrl"] %>"><span class="ui-icon ui-icon-carat-2-n-s">
                        </span></a>
                    </th>
                    <% //} %>
                    <th class="header">
                        New
                    </th>
                    <th class="header" style="width:20%">
                        Category
                    </th>
                    <th>
                        action (stream | file)
                    </th>
                </tr>
            </thead>
            <tbody>
                <% int i = 0; %>
                <% foreach (var item in Model)
                   { %>
                <tr>
                    <td>
                        <%= Html.Hidden("CatalogItems[" + i + "].CatalogItemID", item.CatalogItemID) %>
                        <%= Html.Encode(item.Title) %>
                    </td>
                    <td>
                        <%= Html.Encode(item.Code) %>
                    </td>
                    <td>
                        <%= Html.Encode(String.Format("{0:d}", item.CreateDate)) %>
                    </td>
                    <% if (isSuperUser)
                       { %>
                    <td>
                        <%= Html.CheckBox("CatalogItems[" + i + "].Active", item.Active) %>
                    </td>
                    <% }
                       //else
                       //{ 
                    %>
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
                    <% //} %>
                    <td>
                    <%if(item.IsNew.HasValue){ %>
                        <%= Html.CheckBox("CatalogItems[" + i + "].IsNew", item.IsNew.Value, new { disabled = "disabled" })%>
                        <%} %>
                    </td>
                    <td>
                        <% var viewData = new ViewDataDictionary();viewData.Add(new KeyValuePair<string,object>("CatalogItemID",item.CatalogItemID));
                            Html.RenderPartial("ShowCategories", item.Categories.Select(c => c.Value).ToArray(), viewData); %>
                    <td>
                        <% if (item.TypeID == MainCommon.EntityItemTypeEnum.ClassItem)
                           { %>
                        <a href="<%=   
                            Url.Action("EditClass",  new { catalog_item_id = item.CatalogItemID }) %>" class="btn_no_text btn ui-state-default ui-corner-all tooltip"
                            title="Edit this class"><span class="ui-icon ui-icon-wrench"></span></a>
                        <% if (isSuperUser)
                           { %>
                        <a href="<%= Url.Action("DeleteClass",  new { catalog_item_id = item.CatalogItemID }) %>"
                            class="btn_no_text btn ui-state-default ui-corner-all tooltip" title="Delete this class"
                            onclick="return confirm('Are you sure to delete this class?');"><span class="ui-icon ui-icon-trash">
                            </span></a>
                        <% } %>
                        <% }
                           else
                           { %>
                        <a href="<%=   
                            Url.Action("EditPackage",  new { catalog_item_id = item.CatalogItemID }) %>"
                            class="btn_no_text btn ui-state-default ui-corner-all tooltip" title="Edit this package">
                            <span class="ui-icon ui-icon-wrench"></span></a>
                        <% if (isSuperUser)
                           { %>
                        <a href="<%= Url.Action("DeletePackage",  new { catalog_item_id = item.CatalogItemID }) %>"
                            class="btn_no_text btn ui-state-default ui-corner-all tooltip" title="Delete this package"
                            onclick="return confirm('Are you sure to delete this package?');"><span class="ui-icon ui-icon-trash">
                            </span></a>
                        <% } %>
                        <%} %>

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
            <input type="submit" style="display: none;" />
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

        function setPageSize(pageUrl) {
            var ddl = document.getElementById('resultsperpagedropdown');
            document.location.href = pageUrl + ddl.options[ddl.selectedIndex].value;
        }

        function InitializeEditCategories(id) {
            var autocompleeteUrl = '<%= Url.Action("GetCategories") %>';
            var data = $("#" + id).val().split('|');
            var prePopulateData = [];
            for (var i in data) {
                if (data[i] != "")
                    prePopulateData[i] = { 'name': data[i] };
            }

            $("#" + id).tokenInput(autocompleeteUrl, {

                noResultsText: "No results",
                searchingText: "Searching...",

                classes: {
                    tokenList: "token-input-list-facebook",
                    token: "token-input-token-facebook",
                    tokenDelete: "token-input-delete-token-facebook",
                    selectedToken: "token-input-selected-token-facebook",
                    highlightedToken: "token-input-highlighted-token-facebook",
                    dropdown: "token-input-dropdown-facebook",
                    dropdownItem: "token-input-dropdown-item-facebook",
                    dropdownItem2: "token-input-dropdown-item2-facebook",
                    selectedDropdownItem: "token-input-selected-dropdown-item-facebook",
                    inputToken: "token-input-input-token-facebook"
                },

                prePopulate: prePopulateData

            });

        }

        function OnBeginUpdateCategories(ajaxContext, id) {
            var data = $("#" + id).val()
            var url = ajaxContext.get_request().get_url();
            ajaxContext.get_request().set_url(url + "&categories=" + encodeURIComponent(data));
        }


    </script>
    

</asp:Content>
<asp:Content ContentPlaceHolderID="rightTop" runat="server">
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
