<%@ Page Title="" Language="C#" MasterPageFile="../Shared/twoColumn.Master"
    Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <script type="text/javascript">
        function FA(form_id) {
            var form = document.getElementById(form_id);
            var str = '';
            for (var i = 0; i < form.elements.length; i++) {
                var elem = form.elements[i];
                if (elem.value != 0 && elem.value != undefined && elem.value != null) {
                    if (elem.type == 'checkbox') {
                        if (elem.checked) str = str + '&' + elem.name + '=' + elem.checked;
                    }
                    else if (elem.type != 'submit') {
                        str = str + '&' + elem.name + '=' + encodeURIComponent(elem.value);
                    }
                }
            }

            window.location = form.action + '?filter=true' + str;

            return false;
        }
    </script>




    <% using (Html.BeginForm("results", "search", FormMethod.Post, new { enctype = "multipart/form-data", id = "filter_form" }))
       { %>

    <table cellspacing="0" cellpadding="0" border="0">
        <tbody>
            <tr>
                <td>
                    <img height="35" width="548" src="<%= Url.Image("detailed_search_548X35.jpg") %>">
                </td>
            </tr>
            <tr>
                <td height="20">
                </td>
            </tr>
            <tr>
                <td class="left_indent">
                    <img height="37" width="538" src="<%= Url.Image("Enter_info_538X37.jpg") %>">
                </td>
            </tr>
            <tr>
                <td height="15">
                </td>
            </tr>
            <tr>
                <td class="left_indent">
                    <table cellspacing="0" cellpadding="0" border="0" class="detailedsearch_text">
                        <tbody>
                            <tr>
                                <td>
                                    Title:
                                </td>
                                <td width="30">
                                </td>
                                <td>
                                    Category:
                                </td>
                                <td width="30">
                                </td>
                                <td>
                                    Code:
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <input type="text" value="" name="title">
                                </td>
                                <td width="30">
                                </td>
                                <td>
                                    <input type="text" value="" name="cat">
                                </td>
                                <td width="30">
                                </td>
                                <td>
                                    <input type="text" value="" name="code" style="width:100px">
                                </td>
                            </tr>
                            <tr>
                                <td height="10" colspan="5">
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Speaker:
                                </td>
                                <td width="30">
                                </td>
                                <td>
                                    Keyword:
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <input type="text" name="speaker" />
                                </td>
                                <td width="30">
                                </td>
                                <td>
                                    <select class="inputselect" name="cat_equals">
                                        <option value="0"></option>
                                        <% foreach (var cat in (MainEntity.Models.Tag.Tag[])this.ViewData["categories"])
                                           {  %>
                                           <option value="<%=  HttpUtility.UrlEncode(cat.name.Replace(" ", "-").Replace("/", "slash")) %>"><%= cat.name %></option>
                                        <% } %>
                                    </select>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td height="10" colspan="4">
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Level:
                                </td>
                                <td width="30">
                                </td>
                                <td>
                                    Purchase Option:
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <select class="inputselect" name="level">
                                        <option value="0" selected="selected">All Levels</option>
                                         <% foreach (var level in (MainEntity.Models.Tag.Tag[])this.ViewData["levels"])
                                           { %>
                                           <option value="<%= level.tagID %>"><%= level.name%></option>
                                        <% } %>
                                    </select>
                                </td>
                                <td width="30">
                                </td>
                                <td>
                                    <table cellspacing="0" cellpadding="0" border="0">
                                        <tbody>
                                            <tr>
                                                <td align="center">
                                                    <input type="checkbox" name="cd">
                                                </td>
                                                <td width="20">
                                                </td>
                                                <td align="center">
                                                    <input type="checkbox" name="tape">
                                                </td>
                                                <td width="20">
                                                </td>
                                                <td align="center">
                                                    <input type="checkbox" name="mp3">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    CD
                                                </td>
                                                <td>
                                                </td>
                                                <td align="center">
                                                    Tape
                                                </td>
                                                <td>
                                                </td>
                                                <td align="center">
                                                    MP3
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                                <td>
                                </td>
                                <td valign="top">
                                    <input height="19" width="79" type="image" src="<%= Url.Image("submit_search_button_79X19.jpg") %>" onclick="return FA('filter_form');return false" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
        </tbody>
    </table>

    <% } %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
</asp:Content>
