<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/twocolumns.Master"
    Inherits="System.Web.Mvc.ViewPage<MainEntity.Models.Activity.ActivityLog[]>" %>
<%@ Import Namespace="Main.Areas.Admin.Models.ControllerView.User" %>
<%@ Import Namespace="MainCommon" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="title title-spacing">
        <h2>
            All Files accessed by <%=ViewData["UserName"] %> (Full)</h2>
    </div>
    <%
        var queryForSort = MyUtils.ExcludeQueryParam(Request.QueryString, new string[] { "sort" })
            .GroupBy(g => g.Key, e => e.Value)
            .ToDictionary(k => k.Key, v => v.Count() > 1 ? (object)v.ToArray() : v.Single());
        queryForSort.Add("sort", null);
        var sort = (ClassActivityLogSortEnum)Enum.Parse(typeof(ClassActivityLogSortEnum), (string)ViewData["SortOrder"], true);
        string classIDStyle=string.Empty, dateStyle = string.Empty, speakerStyle = string.Empty, titleStyle = string.Empty, typeStyle = string.Empty, unitsStyle = string.Empty;
        queryForSort["sort"] = ClassActivityLogSortEnum.ClassID_desc;
        string classAction = Url.Action(null, new RouteValueDictionary(queryForSort));
        queryForSort["sort"] = ClassActivityLogSortEnum.Date_desc;
        string dateAction = Url.Action(null, new RouteValueDictionary(queryForSort));
        queryForSort["sort"] = ClassActivityLogSortEnum.Speaker_desc;
        string speakerAction = Url.Action(null, new RouteValueDictionary(queryForSort));
        queryForSort["sort"] = ClassActivityLogSortEnum.Title_desc;
        string titleAction = Url.Action(null, new RouteValueDictionary(queryForSort));
        queryForSort["sort"] = ClassActivityLogSortEnum.Type_desc;
        string typeAction = Url.Action(null, new RouteValueDictionary(queryForSort));
        queryForSort["sort"] = ClassActivityLogSortEnum.Units_desc;
        string unitsAction = Url.Action(null, new RouteValueDictionary(queryForSort));
        switch (sort)
        {
            case ClassActivityLogSortEnum.ClassID_asc:
                classIDStyle = " headerSortUp";
                break;
            case ClassActivityLogSortEnum.ClassID_desc:
                classIDStyle = " headerSortDown";
                queryForSort["sort"] = ClassActivityLogSortEnum.ClassID_asc;
                classAction = Url.Action(null, new RouteValueDictionary(queryForSort));
                break;
            case ClassActivityLogSortEnum.Date_asc:
                dateStyle = " headerSortUp";
                break;
            case ClassActivityLogSortEnum.Date_desc:
                dateStyle = " headerSortDown";
                queryForSort["sort"] = ClassActivityLogSortEnum.Date_asc;
                dateAction = Url.Action(null, new RouteValueDictionary(queryForSort));
                break;
            case ClassActivityLogSortEnum.Speaker_asc:
                speakerStyle = " headerSortUp";
                break;
            case ClassActivityLogSortEnum.Speaker_desc:
                speakerStyle = " headerSortDown";
                queryForSort["sort"] = ClassActivityLogSortEnum.Speaker_asc;
                speakerAction = Url.Action(null, new RouteValueDictionary(queryForSort));
                break;
            case ClassActivityLogSortEnum.Title_asc:
                titleStyle = " headerSortUp";
                break;
            case ClassActivityLogSortEnum.Title_desc:
                titleStyle = " headerSortDown";
                queryForSort["sort"] = ClassActivityLogSortEnum.Title_asc;
                titleAction = Url.Action(null, new RouteValueDictionary(queryForSort));
                break;
            case ClassActivityLogSortEnum.Type_asc:
                typeStyle = " headerSortUp";
                break;
            case ClassActivityLogSortEnum.Type_desc:
                typeStyle = " headerSortDown";
                queryForSort["sort"] = ClassActivityLogSortEnum.Type_asc;
                typeAction = Url.Action(null, new RouteValueDictionary(queryForSort));
                break;
            case ClassActivityLogSortEnum.Units_asc:
                unitsStyle = " headerSortUp";
                break;
            case ClassActivityLogSortEnum.Units_desc:
                unitsStyle = " headerSortDown";
                queryForSort["sort"] = ClassActivityLogSortEnum.Units_asc;
                unitsAction = Url.Action(null, new RouteValueDictionary(queryForSort));
                break;
            default:
                dateStyle = " headerSortDown";
                break;
        }
    %>
	<% Html.RenderPartial("ClassActivityLogFilter", ViewData["Filter"]); %>
    <div class="hastable">
        <table>
            <thead>
                <tr>
                    <th onclick="document.location.href='<%= classAction %>'" class="header<%=classIDStyle  %>">
                        Class ID<span class="ui-icon ui-icon-carat-2-n-s"></span>
                    </th>
                    <th onclick="document.location.href='<%= titleAction %>'" class="header<%=titleStyle  %>">
                        Title<span class="ui-icon ui-icon-carat-2-n-s"></span>
                    </th>
                    <th onclick="document.location.href='<%= speakerAction %>'" class="header<%=speakerStyle  %>">
                        Speaker<span class="ui-icon ui-icon-carat-2-n-s"></span>
                    </th>
                    <th onclick="document.location.href='<%= dateAction %>'" class="header<%=dateStyle  %>">
                        Date<span class="ui-icon ui-icon-carat-2-n-s"></span>
                    </th>
                    <th onclick="document.location.href='<%= typeAction %>'" class="header<%=typeStyle  %>">
                        Type<span class="ui-icon ui-icon-carat-2-n-s"></span>
                    </th>
                    <th onclick="document.location.href='<%= unitsAction %>'" class="header<%=unitsStyle  %>">
                        Unit<span class="ui-icon ui-icon-carat-2-n-s"></span>
                    </th>
                </tr>
            </thead>
            <tbody>
            <%foreach (var item in Model)
              { %>
            <tr>
                <td><%=item.ClassID %></td>
                <td><%=item.Title %></td>
                <td><%=item.Speaker %></td>
                <td><%=item.createDate.Value.ToString("MMM dd, yyyy hh:mm:ss") %></td>
                <td><%=(ActivityLogTypeEnum)item.activityLogTypeID == ActivityLogTypeEnum.DownloadClass ?
					"download" : (ActivityLogTypeEnum)item.activityLogTypeID== ActivityLogTypeEnum.StreamingClass?
					"stream":"free stream"%></td>
                <td><%=item.Units!=null?item.Units.Value.ToString("0"):string.Empty %></td>
            </tr>
            <%} %>
            </tbody>
        </table>
        <% Html.RenderPartial("Pager", ViewData["Pager"]); %>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainHeader" runat="server">
        <link href="<%= Url.Css("css/jquery/plugins/datepicker/3_7_5/smoothness.datepick.css") %>" rel="stylesheet" media="all" />
		<script type="text/javascript" src="<%= Url.JavaScript("jquery/plugins/datepicker/3_7_5/jquery.datepick.js") %>"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="rightTop" runat="server">
</asp:Content>
