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
				</span>
                <span class="search_downloadunits">
                <% if (Model.FileAvailable)
                    { %><%= Model.FilePrice2.ToString("0")%> Cart Unit<% } %>&nbsp;
                </span>
                <div class="mp3_item">
                    <div class="music_icons">
                    <% if (Model.IsFree)
                        { %><a target="_blank" href="javascript:void(0)" onclick="return streamPopupflex('<%= Url.Action("GetFreeStream", "Audio") + "?id=" + Model.FileID %>',250,50)"><img height="25" border="0" width="26" title="FREE Listening" src="/Content/default/Images/listening_icon-over.jpg" /></a>
                        <% }
                        else
                        { %><img height="25" border="0" width="26" title="FREE Listening Not Available" src="/Content/default/Images/listening_icon.jpg" />
                        <% } %>
                            <a href="<%= Url.Action("GetAudioNer", "Audio") + "?id=" + Model.FileID %>"><img height="25" width="25" border="0" src='<%= Url.Image("arrow_icon-over.jpg") %>' title="Buy MP3 Download" alt="Buy MP3 Download" /></a>
                    </div>
                </div>                        
            </div>

            <div class="right_info" <%= Model.NewOrder!=null?"style=\"padding-top:27px\"":string.Empty %>>
                <div class="title_info">
                    <span class="search_title">
                        <%= Html.Encode(Model.Title)%>
                        </span><span class="search_author full_author">&nbsp;by
                        <%= Html.ActionLink(Model.SpeakerName,"resultsdetail", "search", new { speaker = Model.SpeakerName }, null) %></span>
                </div>
                <div class="search_id full_id">
                    <%= Html.Encode(Model.Code) %>
                </div>
                <div class="search_synopsis">
                    <%= Model.Description %>
                </div>
                <div>
                <div class="clear"></div>
            </div>
            </div>
        </div>
    </div>

