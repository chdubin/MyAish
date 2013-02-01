<%@ Page Title="" Language="C#" MasterPageFile="../Shared/twoColumn.Master" Inherits="System.Web.Mvc.ViewPage<Main.Models.ControllerView.Search.ClassListItem[]>" %>

<%@ Import Namespace="MainCommon" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
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
    <% string sort = (string)(this.ViewData["sort"] ?? string.Empty); %>
    <style type="text/css">
        .redline
        {
            height: 2px;
        }
    </style>
    <div class="blue_banner_title">
        Full Listing</div>
    <br />
    <div class="blueboldtextsmall">
        Sort by:
        <select class="inputselect" onchange="chooseSort(this)">
            <option value="title" <%= sort == "title" ? "selected=\"selected\"" : "" %>>Title
            </option>
            <option value="speaker" <%= string.IsNullOrEmpty(sort) ? "selected=\"selected\"" : "" %>>
                Speaker </option>
            <option value="code" <%= sort == "code" ? "selected=\"selected\"" : "" %>>Catalog number</option>
            <%--<option value="classtype">Subject Code </option>--%>
        </select>
    </div>
    <div id="full_description">
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
    </div>
    <br />
    <table border="0" cellpadding="0" cellspacing="0" width="600" style="line-height: 1.2">
        <tr>
            <td colspan="12">
                <img src="<%=Url.Image("spacer.gif") %>" width="1" height="10" border="0" />
            </td>
        </tr>
        <% bool isFirstColumn = true; %>
        <% foreach (var group in Model.GroupBy(m => m.SpeakerName))
           { %>
        <tr class="redbold">
            <td colspan="2">
                <b><a class="redbold" href="/search/resultsdetail/speaker/<%= ((string)group.Key).Replace(" ","-") %>">
                    <%= group.Key %></a></b>
            </td>
            <td>
                &nbsp;&nbsp;
            </td>
            <td>
                <% if (isFirstColumn)
                   { %>Cat #.<% } %>
            </td>
            <td>
                &nbsp;&nbsp;
            </td>
            <td width='30'>
                <% if (isFirstColumn)
                   { %><b>Units</b><% } %>
            </td>
            <td>
                &nbsp;&nbsp;
            </td>
            <td>
                <% if (isFirstColumn)
                   { %><b>Length</b><% } %>
            </td>
            <td>
                &nbsp;&nbsp;
            </td>
            <td align="center">
                <% if (isFirstColumn)
                   { %><b>Level</b><% } %>
            </td>
            <td>
                &nbsp;&nbsp;
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td colspan="12" class="redline">
            </td>
        </tr>
        <% foreach (var item in group)
           { %>
        <tr class="graybold">
            <td width="20">
                <img src="<%=Url.Image("clear.gif") %>" width="20" height="1" border="0" />
            </td>
            <td valign="top" width='95%'>
                <%= Html.ActionLink(item.Title, "ResultsDetail", "Search", new { @class = item.ClassID }, new { @class = "bluetext" })%>
            </td>
            <td>
                &nbsp;
            </td>
            <td valign="top" style="white-space: nowrap">
                <%= item.Code %>
            </td>
            <td>
                &nbsp;
            </td>
            <td valign="top" style="white-space: nowrap">
                <% if (item.FileAvailable)
                   { %>
                <%= ((decimal)item.FilePrice2).ToString("0") %>
                <%= item.FilePrice2 > 1 ? "units" : "unit" %>
                <% }
                   else if (item.DiscAvailable && !item.TypeAvailable)
                   {%>
                CD only
                <%}
                   else if (!item.DiscAvailable && item.TypeAvailable)
                   {%>
                Type only
                <%}
                   else if (item.DiscAvailable && item.TypeAvailable)
                   {%>
                CD & Type only
                <%} %>
            </td>
            <td>
                &nbsp;
            </td>
            <td valign="top" style="white-space: nowrap">
                <%= item.Length %>
            </td>
            <td>
                &nbsp;
            </td>
            <td valign="top">
            </td>
            <td>
                <%=item.Level %>
            </td>
            <td valign="top" style="white-space: nowrap">
                <div class="tinyfontwithoffset">
                </div>
            </td>
        </tr>
        <% } %>
        <tr>
            <td colspan="12">
                <img src="<%=Url.Image("spacer.gif") %>" width="1" height="10" border="0" />
            </td>
        </tr>
        <% isFirstColumn = false; %>
        <% } %>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <link href="<%= Url.Css("main_styles2.css") %>" rel="stylesheet" type="text/css" />
</asp:Content>
