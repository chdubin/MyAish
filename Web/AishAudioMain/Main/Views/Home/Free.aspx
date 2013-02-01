<%@ Page Title="" Language="C#" MasterPageFile="../Shared/twoColumn.Master" Inherits="System.Web.Mvc.ViewPage<Main.Models.ControllerView.Search.ClassListItem[]>" %>
<%@ Import Namespace="MainCommon" %>
<%@ Import Namespace="Main.Areas.Admin.Models.Common" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">

    <% PagingData pager = (PagingData)ViewData["Pager"]; %>


    <div class="search_indent your_search">
        <img src="<%= Url.Image("free_online_topics.jpg") %>" alt="Free online audio on this topic:" />
    </div>
    <div class="search_big_blue">
        <span class="search_has_found">Listen Free</span>
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
        <img style="margin-left: 10px; cursor: pointer" src='<%= Url.Image("submit.jpg") %>' onclick="setPageSize('<%= pager.ClearPagerPath %>ps=')" alt="Submit" />
    </div>
    <img vspace="5" src='<%= Url.Image("linea_az.jpg") %>' alt="" />
	<div class="search_indent">
    <% Html.RenderPartial("PagerCtrl", pager); %>
	</div>
    <% foreach (var item in Model)
       {
			   Html.RenderPartial("ClassDetailFree", item);
	   }%>
    <div class="search_indent">
    <% Html.RenderPartial("PagerCtrl", pager); %>
	</div>
    <script type="text/javascript">
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

    </script>


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="title" runat="server">
Free streaming
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
</asp:Content>
