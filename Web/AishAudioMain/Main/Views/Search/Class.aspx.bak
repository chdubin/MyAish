<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Main.Models.ControllerView.Search.ClassListItem>" %>

<%@ Import Namespace="MainCommon" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Class</title>
    <link href="<%= Url.Css("aishstyles_yisrael.css") %>" rel="stylesheet" type="text/css" />
</head>
<body>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <table border='0' cellpadding='10' cellspacing='0' style='background-color: white;'>
                    <tr>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="0" width="580" valign="top">
                                <tr>
                                    <td colspan="5" height="20" align="center">
                                        <img alt="" src='<%= Url.Image("search_grey_line.jpg") %>'>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span>
                                            <table border='0' cellpadding='0' cellspacing='0' width='100%'>
                                                <tr>
                                                    <td class='search_introduction'>
                                                        <%= Model.Level %>
                                                    </td>
                                                    <td align="right" class='search_downloadunits'>
                                                        <%= Model.FilePrice2.ToString("0") %>&nbsp;Cart&nbsp;<%= Model.FilePrice2 > 1 ? "Units" : "Unit" %>
                                                    </td>
                                                </tr>
                                            </table>
                                        </span>
                                    </td>
                                    <td colspan="4" width="90%">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td width="120" valign="top">
                                        <table border='0' cellpadding='0' cellspacing='0'>
                                            <tr>
                                                <td>
                                                    <a href="#">
                                                        <% if (!string.IsNullOrEmpty(Model.SmallPosterUrl))
                                                           { %>
                                                        <img border="0" width="120" alt="" height="90" src="<%= ResolveClientUrl(Model.SmallPosterUrl) %>" />
                                                        <% }
                                                           else
                                                           { %>
                                                        <img border="0" width="120" alt="Jewish Philosophy 101: #19 Develop a Bias for Truth"
                                                            src='<%= Url.Image("BY_807_S_jewish_philosophy_.gif") %>' />
                                                        <% } %></a>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="108" valign='top' align='left'>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="15">
                                        <img src='<%= Url.Image("spacer.gif") %>' width="15" height="1">
                                    </td>
                                    <td colspan="3" valign="top" width="80%">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" valign="top">
                                            <tr valign="top">
                                                <td valign="top">
                                                    <span class="search_title"><a href='#' class="search_title">
                                                        <%= Model.Title %></a></span>
                                                </td>
                                                <td valign="top" align="right">
                                                    <span class="search_id">
                                                        <%= Model.Code %></span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                    <span class="search_author full_author">by
                                                        <%= Model.SpeakerName %></a></span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <img src='<%= Url.Image("spacer.gif") %>' height="5" width="1">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" class="search_synopsis">
                                                    <%= Model.Description %>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                        <span class="price_prompt">mp3: </span><span class="price_value">
                                            <%= MyUtils.FormatPrice(Model.FilePrice1, Model.FileAvailable, "$", "N/A")%></span>
                                        <span class="price_prompt">Members</span> <span class="price_value">
                                            <%= MyUtils.FormatPrice(Model.FilePrice2, Model.FileAvailable, "$", "N/A") %></span>
                                        <span class="price_prompt">Tape:&nbsp;</span> <span class="price_value">
                                            <%= MyUtils.FormatPrice(Model.TypePrice, Model.TypeAvailable, "$", "N/A") %></span>
                                        <span class="price_prompt">CD:&nbsp;</span> <span class="price_value">
                                            <%= MyUtils.FormatPrice(Model.DiscPrice, Model.DiscAvailable, "$", "N/A") %></span>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</body>
</html>
