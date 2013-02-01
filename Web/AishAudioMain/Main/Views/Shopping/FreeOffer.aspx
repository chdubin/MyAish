<%@ Page Title="" Language="C#" MasterPageFile="../Shared/twoColumn.Master"
    Inherits="System.Web.Mvc.ViewPage<Main.Models.ControllerView.Search.ClassListItem[]>" %>

<asp:Content ContentPlaceHolderID="body" runat="server">
    <% int freeOfferCnt = (int)(ViewData["FreeOfferCnt"] ?? 0); %>
    <img alt="" src='<%= Url.Image("2_free_downloads_banner.jpg") %>' />
    <br />
    <div style="font-size: 2px; height: 25px;">
        &nbsp;</div>
    <img alt="" src='<%= Url.Image("choose_your_2.jpg") %>'><br>
    <div style="font-size: 2px; height: 25px;">
        &nbsp;</div>
		<%=Html.ValidationSummary(false, null, new { style = "font-size:12pt;font-weight:bold;font-color:red;padding-bottom:1em" }) %>
    <%--<img alt="" src='<%= Url.Image("search_grey_line.jpg") %>'>--%>
    <table cellspacing="5">
        <% if (freeOfferCnt > 0)
           { %>
        <tr>
            <td>
                <table width="100%" border="0">
                    <tbody>
                        <tr>
                            <td class="bluebolditalictxt">
                                <% if ((bool)(this.TempData["TooMuchOffersSelected"] ?? false))
                                   { %>
                                You are only allowed to download
                                <%= freeOfferCnt%>
                                free files.
                                <% }
                                   else
                                   { %>
                                You have
                                <%= freeOfferCnt%>
                                item remaining.
                                <% } %>
                                <br />
                                <br />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
        <% } %>
        <tr>
            <td>
                <% if (freeOfferCnt == 0 || Model.Length == 0)
                   { %>
                You already downloaded all free offer files.
                <% }
                   else
                   { %>
                <% using (Html.BeginForm())
                   { %>
                <input type="image" src='<%= Url.Image("submit.jpg") %>' />
                <% foreach (var item in Model)
                   { %>
                <table cellspacing="0" cellpadding="0" border="0" width="580" valign="top">
                    <tbody>
                        <tr>
                            <td height="20" align="center" colspan="5">
                                <img src='<%= Url.Image("search_grey_line.jpg") %>' alt="">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span>
                                    <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                        <tbody>
                                            <tr>
                                                <td class="search_introduction">
                                                    Intro
                                                </td>
                                                <td align="right" class="search_downloadunits">
                                                    <%= item.FilePrice2.ToString("#")%>&nbsp;Cart&nbsp;Units
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <a name="14091"></a></span>
                            </td>
                            <td width="90%" colspan="4">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td width="120" valign="top">
                                <table cellspacing="0" cellpadding="0" border="0">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <a href="#">
                                                    <% if (!string.IsNullOrEmpty(item.SmallPosterUrl))
                                                       { %>
                                                    <img border="0" width="120" alt="" height="90" src="<%= ResolveClientUrl(item.SmallPosterUrl) %>">
                                                    <% }
                                                       else
                                                       { %>
                                                    <img border="0" width="120" src='<%= Url.Image("BY_807_S_jewish_philosophy_.gif") %>'>
                                                    <% } %></a>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" valign="top">
                                                <table cellspacing="0" cellpadding="0" width="100%">
                                                    <tbody>
                                                        <tr>
                                                            <td width="5">
                                                                <input type="checkbox" value="<%= item.FileID %>"<%= ViewData["free_file_ids"]!=null&&((long[])ViewData["free_file_ids"]).Contains(item.FileID)?" checked=\"checked\"":string.Empty %> name="free_file_ids" />
                                                            </td>
                                                            <td class="tinyblue">
                                                                &nbsp;click to choose
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                            <td width="5">
                                <%--<img height="1" width="5" src='<%= Url.Image("cleardot.gif") %>'>--%>
                            </td>
                            <td width="80%" valign="top" colspan="3">
                                <table cellspacing="0" cellpadding="0" border="0" width="100%" valign="top">
                                    <tbody>
                                        <tr valign="top">
                                            <td valign="top">
                                                <span class="search_title"><a class="search_title" href="#">
                                                    <%= item.Title%>
                                                </a></span>
                                            </td>
                                            <td align="right" valign="top">
                                                <span class="search_id">
                                                <%= item.Code %>
                                                </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <span class="search_author full_author">by
                                                    <%= item.SpeakerName%></span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <%--<img height="5" width="1" src='<%= Url.Image("cleardot.gif") %>'>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="search_synopsis" colspan="3">
                                                <%= item.Description%>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <br>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <% } %>
	<% if (!(bool)ViewData["IsAuthorized"])
	{ %>
	<br />
	<% Html.RenderAction("FreeOfferUnauthorize");%>
	<%} %>

                <% } %>
                <% } %>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <link type="text/css" href="/Content/main_styles2.css" rel="Stylesheet" />
</asp:Content>
