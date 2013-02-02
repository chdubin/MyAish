<%@ Page Title="" Language="C#" MasterPageFile="../Shared/twoColumn.Master"
    Inherits="System.Web.Mvc.ViewPage<Main.Models.ControllerView.Search.ClassListItem[]>" %>
<%@ Import Namespace="MainCommon" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <% string sort = (string)(this.ViewData["sort"] ?? string.Empty); %>
    <script type="text/javascript">
        function chooseSort(sel) {
            var sortKey = sel.options[sel.selectedIndex].value;
            if (sortKey != 'speaker')
                sortKey = '?sort=' + sortKey;
            else
                sortKey = '';
            window.location = '<%=Url.Action("fulllist","search") %>' + sortKey;
        }
    </script>
    <div class="blue_banner_title">
        Full Listing</div>
    <br />
    <div class="blueboldtextsmall">
        Sort by:
        <select class="inputselect" onchange="chooseSort(this)">
            <option value="title" <%= sort == "title" ? "selected=\"selected\"" : "" %>>Title </option>
            <option value="speaker" <%= string.IsNullOrEmpty(sort) ? "selected=\"selected\"" : "" %>>Speaker </option>
            <option value="code" <%= sort == "code" ? "selected=\"selected\"" : "" %>>Catalog number</option>
            <%--<option value="classtype">Subject Code </option>--%>
        </select>
    </div>
    <div id="full_description">
        <% if ((int)ViewData["RecordsCountWithoutFile"] > 0)
            { %>
            An additional <%=ViewData["RecordsCountWithoutFile"]%> classes are available only on tape or CD. 
                <a href="<%=MyUtils.GetPathAndQueryParams(this.Request.Path, this.Request.QueryString, "additional", ViewData["RecordsCountWithoutFile"]) %>">View tape/CD.</a>
            <%}
            else if ((int)ViewData["RecordsCountWithoutFile"] < 0){ %>
            <%=-(int)ViewData["RecordsCountWithoutFile"]%> classes are available only on tape or CD. 
                <a href="<%=MyUtils.GetPathAndQueryParams(this.Request.Path, this.Request.QueryString, "additional", ViewData["RecordsCountWithoutFile"]) %>">Hide tape/CD.</a>
            <%} %>
    </div><br />

    <table border="0" cellpadding="0" cellspacing="0" width="600" style="line-height: 1.3">
        <tbody>
            <tr class="redbold">
                <td>
                    <b>Title</b>
                </td>
                <td>
                    &nbsp;&nbsp;
                </td>
                <td width="30%">
                    <b>Speaker</b>
                </td>
                <td>
                    &nbsp;&nbsp;
                </td>
                <td>
                    <b>Cat #.</b>
                </td>
                <td>
                    &nbsp;&nbsp;
                </td>
                <td width="30">
                    <b>Length</b>
                </td>
                <td>
                    &nbsp;&nbsp;
                </td>
                <td>
                    <b>Units</b>
                </td>
                <td>
                    &nbsp;&nbsp;
                </td>
                <td align="center">
                    <b>Level</b>
                </td>
                <td>
                    &nbsp;&nbsp;
                </td>
                <td>
                    <b>
                        <!-- Tape/CD Only -->
                    </b>
                </td>
            </tr>
            <tr>
                <td colspan="13" class="redline" style="height: 2px; line-height: 2px; font-size: 1px;">
                </td>
            </tr>
            <% foreach (var item in Model)
               { %>
            <tr class="graytxt">
                <td valign="top" width="25%">
                    <%= Html.ActionLink(item.Title, "ResultsDetail", "Search", new { @class = item.ClassID }, new { @class = "bluetext" }) %>
                </td>
                <td>
                    &nbsp;
                </td>
                <td valign="top" width="20%">
                    <a class="bluetext" href="/search/resultsdetail/speaker/<%= ((string)item.SpeakerName).Replace(" ","-") %>">
                        <%= item.SpeakerName %>
                    </a>
                </td>
                <td>
                    &nbsp;
                </td>
                <td valign="top" nowrap="">
                    <%= item.Code %>
                </td>
                <td>
                    &nbsp;
                </td>
                <td valign="top" nowrap="">
                    <%= item.Length %>
                </td>
                <td>
                    &nbsp;
                </td>
                <td valign="top">
                    <%= item.FilePrice2.ToString("0") %>
                </td>
                <td>
                    &nbsp;
                </td>
                <td valign="top">
                    <div class="tinyfontwithoffset">
                        <i>
                            <%= item.Level %></i></div>
                </td>
                <td>
                    &nbsp;
                </td>
                <td valign="top" nowrap="">
                    <div class="tinyfontwithoffset">
                    </div>
                </td>
            </tr>
            <% } %>
        </tbody>
    </table>


    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
</asp:Content>
