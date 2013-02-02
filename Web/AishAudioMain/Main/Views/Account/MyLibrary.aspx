<%@ Page Title="" Language="C#" MasterPageFile="../Shared/twoColumn.Master" Inherits="System.Web.Mvc.ViewPage<MainEntity.Models.File.FileEntity[]>" %>

<%@ Import Namespace="Main.Areas.Admin.Models.Common" %>
<%@ Import Namespace="MainCommon" %>
<asp:Content ContentPlaceHolderID="body" runat="server">
    <% PagingData pager = (PagingData)ViewData["Pager"]; %>
    <table border="0" width="580">
        <tbody>
            <tr valign="top">
                <td>
                    <div id="welcome2Div">
                        Welcome,
                        <%= this.ViewData["UserName"] %></div>
                    <!-- Account Page content below (table closed in account_bottom.php) -->
                </td>
                <td rowspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td width="95%" valign="top">
                    <p>
                        <br />
                        <img height="91" border="0" width="548" usemap="#Map" src='<%= Url.Image("suggested_upgratde.jpg") %>' />
                    </p>
                    <p>
                        <map name="Map">
                            <area href="<%= Url.RouteUrl("FreeMP3") %>" coords="195,2,318,29" shape="rect" />
                            <area href="<%= Url.RouteUrl("IPodOffer") %>" coords="192,35,413,59" shape="rect" />
                            <area href="<%= Url.RouteUrl("Offerings") %>" coords="193,63,542,88" shape="rect" />
                        </map>
                    </p>
                    <table cellspacing="0" cellpadding="0" border="0" width="555">
                        <tbody>
                            <tr>
                                <td height="5" colspan="4">
                                    <img height="1" width="1" src='<%= Url.Image("spacer.gif") %>' />
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" colspan="4">
                                    <img height="33" width="555" alt="My Library Archives" src='<%= Url.Image("my_library_555X33.jpg") %>' />
                                </td>
                            </tr>
                            <tr>
                                <td width="10" rowspan="2">
                                    <img height="1" width="10" src='<%= Url.Image("spacer.gif") %>' />
                                </td>
                                <td width="80%">
                                    <table cellspacing="0" cellpadding="0" border="0">
                                        <tbody>
                                            <tr height="10">
                                                <td colspan="3">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <img height="16" width="81" alt="Units Remaining" src='<%= Url.Image("units_remaining_2.jpg") %>' />
                                                </td>
                                                <td>
                                                    <div style="width: 40px; font-family: verdana; font-size: 10px;" class="box">
                                                        <%= ((decimal)ViewData["balance"]).ToString("0") %></div>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr height="10">
                                                <td colspan="3">
                                                </td>
                                            </tr>
                                            <!-- <input type="hidden" name="start" value="1">
               <input type="hidden" name="stop" value="1"> -->
                                            <input type="hidden" value="1" name="sortbysubmit">
                                            <tr>
                                                <td>
                                                    <img height="17" width="80" alt="Results per page" src='<%= Url.Image("results_per_page.jpg") %>' />
                                                </td>
                                                <td>
                                                    <select id="resultsperpagedropdown" style="font-size: 9px;" name="numdisplayed">
                                                        <!--  id="resultsperpagedropdown" -->
                                                        <%--<option value="5" <%= pager.PageSize == 5 ? "selected=\"selected\"" : string.Empty %>>5</option>--%>
                                                        <option value="10" <%= pager.PageSize == 10 ? "selected=\"selected\"" : string.Empty %>>
                                                            10</option>
                                                        <option value="20" <%= pager.PageSize == 20 ? "selected=\"selected\"" : string.Empty %>>
                                                            20</option>
                                                        <option value="50" <%= pager.PageSize == 50 ? "selected=\"selected\"" : string.Empty %>>
                                                            50</option>
                                                        <option value="100" <%= pager.PageSize == 100 ? "selected=\"selected\"" : string.Empty %>>
                                                            100</option>
                                                        <option value="150" <%= pager.PageSize == 150 ? "selected=\"selected\"" : string.Empty %>>
                                                            150</option>
                                                        <option value="200" <%= pager.PageSize == 200 ? "selected=\"selected\"" : string.Empty %>>
                                                            200</option>
                                                    </select>
                                                </td>
                                                <td align="left">
                                                    <input height="22" type="image" width="52" alt="Submit" src='<%= Url.Image("submitbutton.gif") %>'
                                                        onclick="setPageSize('<%= pager.ClearPagerPath %>ps=')" />
                                                </td>
                                            </tr>
                                            <tr height="10">
                                                <td colspan="3">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td nowrap="" valign="bottom" class="libraryblueboldsmall" colspan="3">
                                                    <%= pager.TotalRowCnt%>
                                                    files in Library. Displaying
                                                    <%= pager.StartRowIndex + 1 %>
                                                    to
                                                    <%= pager.EndPageRowIndex %>.
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                                <td>
                                    <img height="1" width="20" src="<%= Url.Image("spacer.gif") %>" />
                                </td>
                                <td align="right" valign="bottom">
                                    <a href="mylibrarylist" target="_blank">
                                        <img height="51" width="51" src='<%= Url.Image("print_my_list.jpg") %>' />
                                    </a>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top" colspan="3">
                                    <br>
                                    <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                        <tbody>
                                            <tr>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <% Html.RenderPartial("PagerCtrl", pager); %>
                    <%
                        var queryForSort = MyUtils.ExcludeQueryParam(Request.QueryString, new string[] { "sort" })
                            .GroupBy(g => g.Key, e => e.Value)
                            .ToDictionary(k => k.Key, v => v.Count() > 1 ? (object)v.ToArray() : v.Single());
                        queryForSort.Add("sort", null);
                        var sort = (int)ViewData["SortOrder"];
                        queryForSort["sort"] = (sort / 2) == 0 && (sort % 2) == 0 ? MyLibraryClassSortEnum.Date_desc : MyLibraryClassSortEnum.Date_asc;
                        string dateAction = Url.Action(null, new RouteValueDictionary(queryForSort));
                        queryForSort["sort"] = (sort / 2) == 1 && (sort % 2) == 0 ? MyLibraryClassSortEnum.Code_desc : MyLibraryClassSortEnum.Code_asc;
                        string codeAction = Url.Action(null, new RouteValueDictionary(queryForSort));
                        queryForSort["sort"] = (sort / 2) == 2 && (sort % 2) == 0 ? MyLibraryClassSortEnum.Title_desc : MyLibraryClassSortEnum.Title_asc;
                        string titleAction = Url.Action(null, new RouteValueDictionary(queryForSort));
                        queryForSort["sort"] = (sort / 2) == 3 && (sort % 2) == 0 ? MyLibraryClassSortEnum.Speaker_desc : MyLibraryClassSortEnum.Speaker_asc;
                        string speakerAction = Url.Action(null, new RouteValueDictionary(queryForSort));
                    %>
                    <table cellspacing="0" cellpadding="0" border="0" width="550">
                        <tbody>
                            <tr>
                                <td>
                                    <br />
                                    <table cellspacing="2" cellpadding="0" border="0" width="100%" id="myLibraryArchives">
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <div class="myLibraryArchivesTableTH sort" onclick="document.location.href='<%= dateAction %>'">
                                                        Date</div>
                                                </td>
                                                <td>
                                                    <div class="myLibraryArchivesTableTH sort" onclick="document.location.href='<%= codeAction %>'">
                                                        Subject</div>
                                                </td>
                                                <td>
                                                    <div class="myLibraryArchivesTableTH sort" onclick="document.location.href='<%= titleAction %>'">
                                                        Title</div>
                                                </td>
                                                <td>
                                                    <div class="myLibraryArchivesTableTH sort" onclick="document.location.href='<%= speakerAction %>'">
                                                        Speaker</div>
                                                </td>
                                                <td>
                                                    <div class="myLibraryArchivesTableTH">
                                                        Download</div>
                                                </td>
                                                <td>
                                                    <div class="myLibraryArchivesTableTH">
                                                        Stream</div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="8" colspan="6">
                                                </td>
                                            </tr>
                                            <% foreach (var item in Model)
                                               { %>
                                            <tr class="myLibraryArchivesRegular">
                                                <td valign="top" class="td1">
                                                    <%= item.EntityItem.ShoppingDate.ToString("MM/dd/yy", System.Globalization.CultureInfo.InvariantCulture) %><br />
                                                </td>
                                                <td nowrap="" valign="top" class="td2">
                                                    <%= item.EntityItem.FileClassEntity.CatalogItemExtend.code %>
                                                </td>
                                                <td valign="top" class="td3">
                                                    <%= item.EntityItem.FileClassEntity.ClassEntityItem.title %>
                                                </td>
                                                <td valign="top" class="td4">
                                                    <%= item.EntityItem.FileClassEntity.SpeakerEntityItem.title %>
                                                </td>
                                                <% if (!item.EntityItem.FileClassEntity.ClassEntityItem.active || item.EntityItem.FileClassEntity.ClassEntityItem.deleted)
                                                   {%>
                                                <td valign="top" class="td5" colspan="2">
                                                    Class is no longer offered.
                                                </td>
                                                <% }
                                                   else
                                                   {%>
                                                <td valign="top" class="nopadding" style="white-space: nowrap">
                                                    <% if (!string.IsNullOrEmpty(item.filePath))
                                                       { %>
                                                    <a href="<%= Url.Action("GetAudio", "Audio") + "?id=" + item.fileID %>">
                                                        <img src='<%= Url.Image("high_icon-over.jpg") %>' /></a>
                                                    <% } %>
                                                    <% if (!string.IsNullOrEmpty(item.alternateFilePath))
                                                       { %>
                                                    <a href="<%= Url.Action("GetAudio", "Audio") + "?low=true&id=" + item.fileID %>">
                                                        <img src='<%= Url.Image("low_icon-over.jpg") %>' /></a>
                                                    <% } %>
                                                </td>
                                                <td align="center" valign="top" class="nopadding" style="white-space: nowrap">
                                                    <% if (!string.IsNullOrEmpty(item.filePath))
                                                       { %>
                                                       <a href="#" onclick="return streamPopupflex('<%= Url.Action("Class", "Search") + "?id=" + item.EntityItem.FileClassEntity.ClassEntityItem.entityID + "&sp=true" %>',670,400)"> 
                                                        <!--<a href="#" onclick="return streamPopupflex('<%= Url.Action("GetStream", "Audio") + "?id=" + item.fileID %>',250,220)">-->
                                                        <img border="0" src='<%= Url.Image("volume_icon.jpg") %>' /></a>
                                                    <% } %>
                                                </td>
                                                <%}%>
                                            </tr>
                                            <% } %>
                                            <!-- DOUBLE LINE -->
                                            <tr>
                                                <td colspan="6">
                                                    <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                                        <tbody>
                                                            <tr bgcolor="#ebe9ea">
                                                                <td>
                                                                    <div style="height: 1px;">
                                                                        <img height="1" width="1" src='<%= Url.Image("spacer.gif") %>' /></div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <div style="height: 3px;">
                                                                        <img height="3" width="1" src='<%= Url.Image("spacer.gif") %>' /></div>
                                                                </td>
                                                            </tr>
                                                            <tr bgcolor="#ebe9ea">
                                                                <td>
                                                                    <div style="height: 1px;">
                                                                        <img height="1" width="1" src='<%= Url.Image("spacer.gif") %>' /></div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <div style="height: 3px;">
                                                                        <img height="3" width="1" src='<%= Url.Image("spacer.gif") %>' /></div>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                        <tbody>
                                            <tr>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <!-- Bottom of account page (and mylibrary too) -->
                    <!-- Account Page content above (table opened in account_top.php) -->
                    <% Html.RenderPartial("PagerCtrl", pager); %>
                </td>
            </tr>
        </tbody>
    </table>
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

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <link type="text/css" href="/Content/main_styles2.css" rel="Stylesheet" />
</asp:Content>
