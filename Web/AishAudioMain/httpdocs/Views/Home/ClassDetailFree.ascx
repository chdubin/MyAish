<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Main.Models.ControllerView.Search.ClassListItem>" %>
<%@ Import Namespace="MainCommon" %>
<%@ Import Namespace="Main.Areas.Admin.Models.Common" %>

    <div class="big_item">
        <img alt="" src='<%= Url.Image("search_grey_line.jpg") %>' style="display: block; float: left;
            margin-bottom: 18px;" />
        <div>
            <div class="left_img">
                <% if (Model.NewOrder != null)
                   { %>
                <img alt="New!" width="67" height="27" src="<%= Url.Image("new_67x27.gif") %>" /><br />
                <%} %>
                <span class="search_introduction">
				<%= Model.Level %>
				</span><span class="search_downloadunits"><% if (Model.FileAvailable)
                    { %><%= Model.FilePrice2.ToString("0")%> Cart Unit<% } %>&nbsp;</span>
				<div class="search_big_img">
                    <a class="search_title" href="#" onclick="return streamPopupflex('<%= Url.Action("Class", "Search") + "?id=" + Model.ClassID %>',670,230)">
                        <% if (!string.IsNullOrEmpty(Model.SmallPosterUrl))
                           { %>
                        <img border="0" width="120" alt="<%= Html.AttributeEncode(Model.Title)  %>" height="90"
							src="<%= ResolveClientUrl(Model.SmallPosterUrl) %>" />
                        <% }
                           else
                           { %>
                        <img border="0" width="120" alt="<%= Html.AttributeEncode(Model.Title)  %>"
                            src='<%= Url.Image("BY_807_S_jewish_philosophy_.gif") %>' />
                        <% } %>
                    </a>
                </div>
                <div class="music_icons">
                    <a href="#" onclick="return streamPopupflex('<%= Url.Action("GetFullFreeStream", "Audio") + "?id=" + Model.FileID %>',250,220)" title="FREE Listening">
                        <img border="0" alt="FREE Listening" src="<%=Url.Image("listening_icon-over.jpg") %>" width="29" height="29" />
                    </a>
                </div>
            </div>
            <div class="right_info" <%= Model.NewOrder!=null?"style=\"padding-top:27px\"":string.Empty %>>
                <div class="title_info">
                    <span class="search_title">
					<a class="search_title" href="#" onclick="return streamPopupflex('<%= Url.Action("Class", "Search") + "?id=" + Model.ClassID %>',670,230)">
                        <%= Html.Encode(Model.Title)%>
                    </a></span><span class="search_author full_author">&nbsp;by
                        <%= Html.ActionLink(Model.SpeakerName,"resultsdetail", "search", new {speaker = Model.SpeakerName},null) %></span>
                </div>
                <div class="search_id full_id">
                    <%= Html.Encode(Model.Code) %>
                </div>
                <div class="search_synopsis">
                    <%= Model.Description %>
                </div>
            </div>
        </div>
    </div>

