<%@ Page Title="" Language="C#" MasterPageFile="../Shared/twoColumn.Master" Inherits="System.Web.Mvc.ViewPage<Main.Models.ControllerView.Search.ClassListItem[]>" %>

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
                        <%= speaker.Name%>&nbsp;on&nbsp;
                    </td>
                    <td nowrap="" width="112" style="text-indent: 0px; padding-top: 2px">
                        <img height="24" border="0" width="112" alt="aishAudio" src="<%= Url.Image("aishaudio_rabbis.jpg") %>">
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
       { %>
    <div class="title_match">
        Title Match: <span class="search_rabbi">
            <%= ViewData["SearchTitleWords"]%></span></div>
    <% }
       else
       { %>
    <div class="search_indent your_search">
        Your search of:
        <%= this.ViewData["SearchWord"] %>
    </div>
    <% } %>
    <div class="search_big_blue">
        <span class="search_has_found">has found
            <%= pager.TotalRowCnt %>
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
            <%--<option value="5" <%= pager.PageSize == 10 ? "selected=\"selected\"" : string.Empty %>>
                5</option>--%>
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
                        <a href="<%= (Url.Action("ResultsDetail") + Request.Url.AbsolutePath.ToLowerInvariant().Replace(Url.Action("results").ToLowerInvariant(),"")  + "?" + Request.QueryString).TrimEnd(new char[]{'?'}) %>">
                            <img alt="Change to Catalog View" src='<%= Url.Image("View_ful.jpg") %>'>
                        </a>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <img vspace="5" src='<%= Url.Image("linea_az.jpg") %>' alt="">
    <% Html.RenderPartial("PagerCtrl", pager); %>
    <div>
        <% foreach (var item in Model)
           { %>
        <div class="mp3_item">
            <div class="music_icons">
                <% if (item.IsFree)
                   {
                       if ((bool)ViewData["IsAuthorized"])
                       {
                            %>
                            <a href="#" onclick="return streamPopupflex('<%= Url.Action("Class", "Search") + "?id=" + item.ClassID + "&sp=true" %>',670,400)"> 
                            <!--<a target="_blank" href="javascript:void(0)" onclick="return streamPopupflex('<%= Url.Action("GetFreeStream", "Audio") + "?id=" + item.FileID %>',250,220)">-->
                            <%}
                       else
                       {
                            %>
                            <a class="search_title" href="<%= Url.Action("RegisterCustom", "Account", new RouteValueDictionary { { "returnUrl", Request.Url.AbsoluteUri + "?id=" + item.FileID } }) %>">
                            <%
                       } %>
                        <img height="25" border="0" width="26" title="FREE Listening" src="<%= Url.Image("listening_icon-over.jpg") %>" alt="FREE Listening" /></a>
                        <!--/Content/default/Images/listening_icon-over.jpg-->
                    <% }
                   else
                   { %>
                    <img height="25" border="0" width="26" title="FREE Listening Not Available" src="<%= Url.Image("listening_icon.jpg") %>" />
                    <!--/Content/default/Images/listening_icon.jpg-->
                    <% } %>
                    <% if (item.FileAvailable)
                       { %>
                    <a href="javascript:void(0)" onclick="return AddToCart(<%= item.FileID %>, this)">
                        <img height="25" width="25" border="0" src='<%= Url.Image("arrow_icon-over.jpg") %>'
                            title="Buy MP3 Download" alt="Buy MP3 Download" /></a>
                    <% }
                       else
                       { %>
                    <img height="25" width="25" border="0" src='<%= Url.Image("arrow_icon.jpg") %>' title="MP3 Download Not Available" />
                    <% } %>
                    <% if (item.TypeAvailable)
                       { %>
                    <a href="javascript:void(0)" onclick="return AddMediaToCart(<%= item.TypeID %>, this)">
                        <img height="25" width="26" border="0" src='<%= Url.Image("tape_icon_over.jpg") %>'
                            title="Order Tape" /></a>
                    <% }
                       else
                       { %>
                    <img height="25" width="26" border="0" src='<%= Url.Image("tape_icon.jpg") %>' title="Tape Not Available" />
                    <% } %>
                    <% if (item.DiscAvailable)
                       { %>
                    <a href="javascript:void(0)" onclick="return AddMediaToCart(<%= item.DiscID %>, this)">
                        <img height="25" width="26" border="0" src='<%= Url.Image("compactDisc_icon-over.jpg") %>'
                            title="Order CD" /></a>
                    <% }
                       else
                       { %>
                    <img height="25" width="26" border="0" src='<%= Url.Image("compactDisc_icon.jpg") %>'
                        title="CD Not Available" />
                    <% } %>
            </div>
            <div class="rightpart_mp3">
                <div class="rightpart_mp3">
                    <div class="title">
                        <%if (item.NewOrder != null)
                          { %>
                        <span style="color: Red"><b>NEW&nbsp;&nbsp;</b></span>
                        <%} %>
                        <% if ((bool)ViewData["IsAuthorized"])
                           { %>
                        <a href="javascript:void(0)" onclick="return streamPopupflex('<%= Url.Action("Class", "Search") + "?id=" + item.ClassID %>',670,230)"
                            class="search_title">
                            <%}
                           else
                           { %>
                            <a class="search_title" href="<%= Url.Action((item.IsFree?"RegisterCustom":"Register"),"Account",null) %>">
                                <%} %>
                                <%= Html.Encode(item.Title) %></a><span class="search_author">&nbsp;&nbsp;
                                    <%= Html.ActionLink(item.SpeakerName,"resultsdetail", "search", new {speaker = item.SpeakerName},null) %></span>
                    </div>
                    <div class="search_id">
                        <%= item.Code %>
                    </div>
                </div>
                <div class="rightpart_mp3">
                    <div class="price_and_details">
                        <% if (ViewData.IsSubscriber())
                           { %>
                        <div>
                            <div class="price_prompt left">
                                Download Units:&nbsp;
                            </div>
                            <div class="price_value left">
                                <% if (item.FileAvailable)
                                   { %>
                                <%= (int)item.FilePrice2%>
                                <%}
                                   else
                                   { %>
                                N/A
                                <%} %>
                            </div>
                            <div class="price_prompt left" style="margin-left: 8px;">
                                Members:&nbsp;
                            </div>
                            <div class="price_value left">
                                <%= MyUtils.FormatPrice(item.FilePrice2 * decimal.Parse(ViewData["UnitsRate"].ToString()), item.FileAvailable, "$", "N/A")%>
                            </div>
                            <% if (item.TypeAvailable)
                               { %>
                            <div class="price_prompt left" style="margin-left: 8px;">
                                <span onclick="return AddMediaToCart(<%= item.TypeID %>, this)" style="cursor: pointer;">
                                    Tape:&nbsp; </span>
                            </div>
                            <div class="price_value left">
                                <strike style="color: black; float: left;">$<%= item.TypePrice.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)%></strike>
                                <span onclick="return AddMediaToCart(<%= item.TypeID %>, this)" style="cursor: pointer;
                                    margin-left: 4px;">
                                    <%= MyUtils.FormatPrice(item.TypePrice * (1 - decimal.Parse(ViewData["SubscriberDiscount"].ToString())), item.TypeAvailable, "$", "N/A")%>
                                </span>
                            </div>
                            <% }
                               else
                               { %>
                            <div class="price_prompt left" style="margin-left: 8px;">
                                Tape:&nbsp;</div>
                            <div class="price_value left">
                                N/A</div>
                            <% } %>
                            <% if (item.DiscAvailable)
                               { %>
                            <div class="price_prompt left" style="margin-left: 8px;">
                                <span onclick="return AddMediaToCart(<%= item.DiscID %>, this)" style="cursor: pointer;">
                                    CD:&nbsp; </span>
                            </div>
                            <div class="price_value left">
                                <strike style="color: black; float: left;">$<%= item.DiscPrice.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)%></strike>
                                <span onclick="return AddMediaToCart(<%= item.DiscID %>, this)" style="cursor: pointer;
                                    margin-left: 4px;">
                                    <%= MyUtils.FormatPrice(item.DiscPrice * (1 - decimal.Parse(ViewData["SubscriberDiscount"].ToString())), item.DiscAvailable, "$", "N/A")%>
                                </span>
                            </div>
                            <% }
                               else
                               { %>
                            <div class="price_prompt left" style="margin-left: 8px;">
                                CD:&nbsp;</div>
                            <div class="price_value left">
                                N/A</div>
                            <% } %>
                            <div class="clear">
                            </div>
                        </div>
                        <%}
                           else
                           { %>
                        <% if (item.FileAvailable)
                           { %>
                        <a href="javascript:void(0)" onclick="return AddToCart(<%= item.FileID %>, this)"
                            class="nolinkformat">
                            <%} %>
                            <span class="price_prompt">mp3: </span><span class="price_value">
                                <%= MyUtils.FormatPrice(item.FilePrice1, item.FileAvailable, "$", "N/A")%>
                            </span>
                            <% if (item.FileAvailable)
                               { %>
                        </a>
                        <%} %>
                        <% if (item.FileAvailable && (bool)ViewData["IsAuthorized"])
                           { %>
                        <a href="javascript:void(0)" onclick="return AddToCart(<%= item.FileID %>, this)"
                            class="nolinkformat">
                            <%}
                           else if (item.FileAvailable)
                           { %>
                            <a href='<%= Url.Action("Register", "Account") %>' class="nolinkformat">
                                <%} %>
                                &nbsp; <span class="price_prompt">Members </span><span class="price_value">
                                    <%= MyUtils.FormatPrice(item.FilePrice2 * decimal.Parse(ViewData["UnitsRate"].ToString()), item.FileAvailable, "$", "N/A")%>
                                </span>
                                <% if (item.FileAvailable)
                                   {%>
                            </a>
                            <%} %>
                            &nbsp;&nbsp;
                            <% if (item.TypeAvailable)
                               { %>
                            <a href="javascript:void(0)" onclick="return AddMediaToCart(<%= item.TypeID %>, this)"
                                class="nolinkformat">
                                <%} %>
                                <span class="price_prompt">Tape:&nbsp; </span><span class="price_value">
                                    <%= MyUtils.FormatPrice(item.TypePrice, item.TypeAvailable, "$", "N/A")%>
                                </span>
                                <% if (item.TypeAvailable)
                                   { %>
                            </a>
                            <%} %>
                            &nbsp;&nbsp;
                            <% if (item.DiscAvailable)
                               { %>
                            <a href="javascript:void(0)" onclick="return AddMediaToCart(<%= item.DiscID %>, this)"
                                class="nolinkformat">
                                <%} %>
                                <span class="price_prompt">CD:&nbsp; </span><span class="price_value">
                                    <%= MyUtils.FormatPrice(item.DiscPrice, item.DiscAvailable, "$", "N/A")%>
                                </span>
                                <% if (item.TypeAvailable)
                                   { %>
                            </a>
                            <%} %>
                            <% } %>
                    </div>
                    <div class="search_details">
                        <a href="#" onclick="return streamPopupflex('<%= Url.Action("Class", "Search") + "?id=" + item.ClassID + "&sp=false" %>',670,230)"
                            class="search_details">DETAILS </a>
                    </div>
                </div>
            </div>
        </div>
        <% } %>
    </div>
    <% Html.RenderPartial("PagerCtrl", pager); %>
    <script type="text/javascript">

        function AddToCart(id, elem) {
            $.ajax({
                url: '/shopping/AddItemToShoppingCart?item_type_id=1&item_id=' + id,
                success: function (data) {
                    if (data != "") {
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
                    $("#HeaderCartSectionContent").html(data);
                    alert("Product was successfully added to your shopping cart");
                }
            });

            return false;
        }

        function setPageSize(pageUrl) {
            var ddl = document.getElementById('resultsperpagedropdown');
            document.location.href = pageUrl + ddl.options[ddl.selectedIndex].value;
        }

        function streamPopupflex(page, width, height) {
            self.name = "TMmain";
            options = "toolbar=0,status=0,menubar=0,scrollbars=0,resizable=0,width=" + width + ",height=" + height;
            window.open(page, "TMPlayer", options);

            return false;
        }

        function getQuerystring(key, default_) {
            if (default_ == null) default_ = "";
            key = key.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
            var regex = new RegExp("[\\?&]" + key + "=([^&#]*)");
            var qs = regex.exec(window.location.href);
            if (qs == null)
                return default_;
            else
                return qs[1];
        }

        $(document).ready(function () {
            var id = getQuerystring("id", "");
            if (id != "") {
                streamPopupflex('<%= Url.Action("GetFreeStream", "Audio") + "?id=" %>' + id, 250, 220);
            }
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="title" runat="server">
</asp:Content>
