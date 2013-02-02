<%@ Page Title="" Language="C#" MasterPageFile="../Shared/twoColumn.Master"
    Inherits="System.Web.Mvc.ViewPage<Main.Models.ControllerView.Search.ClassListItem[]>" %>

<%@ Import Namespace="MainCommon" %>
<%@ Import Namespace="Main.Areas.Admin.Models.Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <% PagingData pager = (PagingData)ViewData["Pager"]; %>
    <% if (ViewData["Speaker"] != null)
       {
           var speaker = (Main.Areas.Admin.Models.Speaker.SpeakerEditModel)ViewData["Speaker"];
    %>
    <div class="title_match">
        Title Match: <span class="search_rabbi">
            <%= speaker.Name%></span></div>
    <div class="search_big_blue">
        <table cellspacing="0" cellpadding="0" border="0" width="100%" style="margin-right: 30px;">
            <tbody>
                <tr>
                    <td nowrap="" width="10%" style="text-indent: 13px;" class="search_big_blue_rabbitext">
                        <%= speaker.Name%>
                    </td>
                    <td nowrap="" width="112" style="text-indent: 0px; padding-top: 2px" class="search_big_blue_rabbitext">
                        </td>
                    <td width="90%" class="search_big_blue_rabbitext">
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <% if (!string.IsNullOrEmpty(speaker.Description))
       { %>
    <table cellspacing="0" cellpadding="0" width="600">
        <tbody>
            <tr>
                <td height="5" width="600" valign="middle">
                    <img height="1" width="600" src="/ssi/aish/graphics/cleardot.gif">
                </td>
            </tr>
            <tr>
                <td>
                    <table cellspacing="0" cellpadding="0" style="background-color: #3366CC; text-align: center;
                        width: 470px; margin: 0 auto;">
                        <tbody>
                            <tr>
                                <td height="2" width="470">
                                    <img height="2" width="470" src="/ssi/aish/graphics/cleardot.gif">
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellspacing="0" cellpadding="10" style="background-color: #E8EFF9; text-align: center;
                                        width: 466px; margin: 0 auto; padding: 10px;">
                                        <tbody>
                                            <tr>
                                                <td valign="top" style="padding: 10px">
                                                    <% if (!string.IsNullOrEmpty(speaker.PhotoPath))
                                                       { %>
                                                    <img height="100" width="100" alt="<%= speaker.Name %>" src="<%= ResolveClientUrl(ConfigurationManager.AppSettings["UploadSpeakerImgFolderName"] + "/" + MainCommon.MyUtils.GetFileName(speaker.PhotoPath)) + "?_=" + (new Random().Next()) %>" />
                                                    <% } %>
                                                </td>
                                                <td valign="middle" style="font: 12px Verdana; padding: 10px;">
                                                    <%= speaker.Description%>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td height="2" width="470">
                                    <img height="2" width="470" src="/ssi/aish/graphics/cleardot.gif">
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
            <tr>
                <td height="5" width="600" valign="middle">
                    <img height="1" width="600" src="/ssi/aish/graphics/cleardot.gif">
                </td>
            </tr>
        </tbody>
    </table>
    <% }
       }
       else if (!string.IsNullOrEmpty((string)ViewData["SearchTitleWords"]))
       {
    %><div class="title_match">
        Title Match: <span class="search_rabbi">
            <%= ViewData["SearchTitleWords"]%></span></div>
    <%}
       else 
       {%><div class="search_indent your_search">
           Your search of:
           <%= this.ViewData["SearchWord"]%>
       </div>
    <%} %>
    <div class="search_big_blue">
        <span class="search_has_found">has found
            <%= pager.TotalRowCnt%>
            results</span>
    </div>
    <div class="search_indent">
        <div class="your_search">
            This page displays results
            <%= pager.TotalRowCnt > 0 ? pager.StartRowIndex + 1 : 0 %>
            to
            <%= pager.EndPageRowIndex %>
            of
            <%= pager.TotalRowCnt %>.
        </div>
    </div>
    <div class="search_indent" style="margin-top: 10px;">
        <div class="results_per_page">
            Results per page:
        </div>
        <select id="resultsperpagedropdown" style="font-size: 10px">
            <option value="5" <%= pager.PageSize == 10 ? "selected=\"selected\"" : string.Empty %>>
                5</option>
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
        <img style="margin-left: 10px; cursor: pointer" src='<%= Url.Image("submit.jpg") %>'
            onclick="setPageSize('<%= pager.ClearPagerPath %>ps=')">
        <div id="full_description">
            <table style="width: 100%">
                <tr>
                    <td style="width: 100%; vertical-align: middle">
                        <% if ((int)ViewData["RecordsCountWithoutFile"] > 0)
                           { %>
                        An additional
                        <%=ViewData["RecordsCountWithoutFile"]%>
                        classes are available only on tape or CD. <a href="<%=MyUtils.GetPathAndQueryParams(this.Request.Path, this.Request.QueryString, "additional", ViewData["RecordsCountWithoutFile"]) %>">
                            View tape/CD.</a>
                        <%}
                           else if ((int)ViewData["RecordsCountWithoutFile"] < 0)
                           { %>
                        <%=-(int)ViewData["RecordsCountWithoutFile"]%>
                        classes are available only on tape or CD. <a href="<%=MyUtils.GetPathAndQueryParams(this.Request.Path, this.Request.QueryString, "additional", ViewData["RecordsCountWithoutFile"]) %>">
                            Hide tape/CD.</a>
                        <%} %>
                    </td>
                    <td style="white-space: nowrap; vertical-align: middle">
                        <a href="<%= (Url.Action("Results") + Request.Url.AbsolutePath.ToLowerInvariant().Replace(Url.Action("resultsdetail").ToLowerInvariant(),"") + "?" + Request.QueryString).TrimEnd(new char[]{'?'}) %>">
                            <img alt="Change to List View" src='<%= Url.Image("view_as_list.jpg") %>'>
                        </a>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <img vspace="5" src='<%= Url.Image("linea_az.jpg") %>' alt="">
    <div class="search_indent">
        <% Html.RenderPartial("PagerCtrl", pager); %>
    </div>
    <% foreach (var item in Model)
       {
          Html.RenderPartial("ClassDetailAuthorized", item);
       }%>
    <div class="search_indent">
        <% Html.RenderPartial("PagerCtrl", pager); %>
    </div>
    <script type="text/javascript">
        function setPageSize(pageUrl) {
            var ddl = document.getElementById('resultsperpagedropdown');
            document.location.href = pageUrl + ddl.options[ddl.selectedIndex].value;
        }

        function AddToCart(id, elem) {
            $.ajax({
                url: '/shopping/AddItemToShoppingCart?item_type_id=1&item_id=' + id,
                success: function (data) {
                    if (data != "") {
                        $(elem).html("<img width='60' height='25' border='0' src='/Content/Default/Images/in_cart.jpg' alt='MP3 Download Already In Cart' />");
                        $("#HeaderCartSectionContent").html(data);
                        alert("Product was successfully added to your shopping cart");
                    } else {
                        alert("This lecture already exists in your library.");
                    }
                }
            });




            return false;
        }

        function AddMediaToCart(id, elem) {
            $.ajax({
                url: '/shopping/AddItemToShoppingCart?item_type_id=1&item_id=' + id,
                success: function (data) {
                    //$(elem).children().eq(0).unwrap();
                    //$(elem).after($(elem).text()).remove();                    

                    // Добавление обновленной инфы в шапку
                    $("#HeaderCartSectionContent").html(data);
                    alert("Product was successfully added to your shopping cart");
                }
            });





            return false;
        }

        function streamPopupflex(page, width, height) {
            self.name = "TMmain";
            options = "location=no,toolbar=0,status=0,menubar=0,scrollbars=0,resizable=0,width=" + width + ",height=" + height;
            window.open(page, "TMPlayer", options);

            return false;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="title" runat="server">
</asp:Content>
