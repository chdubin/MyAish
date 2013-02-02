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
                    <a href="#">
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
                <% if (ViewData.IsSubscriber())
                   { %>

                        <div class="mp3_item">
                            <div class="music_icons">
                            <% if (Model.IsFree)
                               { %><a target="_blank" href="javascript:void(0)" onclick="return streamPopupflex('<%= Url.Action("GetFreeStream", "Audio") + "?id=" + Model.FileID %>',250,220)"><img height="25" border="0" width="26" title="FREE Listening" src="/Content/default/Images/listening_icon-over.jpg" /></a>
                               <% }
                               else
                               { %><img height="25" border="0" width="26" title="FREE Listening Not Available" src="/Content/default/Images/listening_icon.jpg" />
                               <% } %>
                                <% if (Model.FileAvailable)
                                   { %><a href="javascript:void(0)" onclick="return AddToCart(<%= Model.FileID %>, this)"><img height="25" width="25" border="0" src='<%= Url.Image("arrow_icon-over.jpg") %>' title="Buy MP3 Download" alt="Buy MP3 Download" /></a>
                                   <% }
                                   else
                                   { %><img height="25" width="25" border="0" src='<%= Url.Image("arrow_icon.jpg") %>' title="MP3 Download Not Available" />
                                   <% } %>
                                <% if (Model.TypeAvailable)
                                   { %><a href="javascript:void(0)" onclick="return AddMediaToCart(<%= Model.TypeID %>, this)"><img height="25" width="26" border="0" src='<%= Url.Image("tape_icon_over.jpg") %>' title="Order Tape" /></a>
                                   <% }
                                   else
                                   { %><img height="25" width="26" border="0" src='<%= Url.Image("tape_icon.jpg") %>' title="Tape Not Available" />
                                   <% } %>
                                <% if (Model.DiscAvailable)
                                   { %><a href="javascript:void(0)" onclick="return AddMediaToCart(<%= Model.DiscID %>, this)"><img height="25" width="26" border="0" src='<%= Url.Image("compactDisc_icon-over.jpg") %>' title="Order CD" /></a>
                                   <% }
                                   else
                                   { %><img height="25" width="26" border="0" src='<%= Url.Image("compactDisc_icon.jpg") %>' title="CD Not Available" />
                                   <% } %>
                            </div>
                        </div>                        
                    </div>

                    <div class="right_info" <%= Model.NewOrder!=null?"style=\"padding-top:27px\"":string.Empty %>>
                        <div class="title_info">
                            <span class="search_title">
                                <a href="#" onclick="return streamPopupflex('<%= Url.Action("Class", "Search") + "?id=" + Model.ClassID %>',670,230)" class="search_title">
                                    <%= Html.Encode(Model.Title)%>
                                </a>
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
                            
					        <%if (Model.FileAvailable)
					        { %>
                                <div class="left">
                                    <div class="price_prompt">
                                        Download Units:&nbsp;
                                    </div>
                                    <div class="price_value">
                                        <%= (int)Model.FilePrice2 %>
                                    </div>
                                </div>
                                <div class="left">
                                    <div class="price_prompt" style="margin-left: 8px;">
                                        Members:&nbsp;
                                    </div>
                                    <div class="price_value">
                                        <%= MyUtils.FormatPrice(Model.FilePrice2 * decimal.Parse(ViewData["UnitsRate"].ToString()), Model.FileAvailable, "$", "N/A")%>
                                    </div>
                                </div>
					        <%}
					        else
					        {%>
                                <div class="left">
                                    <div class="price_prompt">
                                        Download Units:&nbsp;
                                    </div>
                                    <div class="price_value">N/A</div>
                                </div>
                                <div class="left">
                                    <div class="price_prompt" style="margin-left: 8px;">
                                        Members:&nbsp;
                                    </div>
                                    <div class="price_value">N/A</div>
                                </div>
					        <%} %>
                            
                                <div class="left">
                                    <% if (Model.TypeAvailable)
                                        { %>
                                    <div class="price_prompt" style="margin-left: 8px;">
                                        <span onclick="return AddMediaToCart(<%= Model.TypeID %>, this)" style="cursor: pointer;">
                                            Tape:&nbsp;
                                        </span>
                                    </div>                                    
                                    <div class="price_value">
                                        <strike style="color:black;float:left;">$<%= Model.TypePrice.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)%></strike>
                                        <span onclick="return AddMediaToCart(<%= Model.TypeID %>, this)" style="cursor: pointer;margin-left:4px;">
                                            <%= MyUtils.FormatPrice(Model.TypePrice * (1 - decimal.Parse(ViewData["SubscriberDiscount"].ToString())), Model.TypeAvailable, "$", "N/A")%>
                                        </span>
                                    </div>
                                        <% }
                                           else
                                           { %>
                                    <div class="price_prompt" style="margin-left: 8px;">Tape:</div>
                                    <div class="price_value">N/A</div>
                                    <% } %>                                
                                </div>
                                <div class="left">
                                    <% if (Model.DiscAvailable)
                                    { %>
                                    <div class="price_prompt" style="margin-left: 8px;">                                
                                        <span onclick="return AddMediaToCart(<%= Model.DiscID %>, this)" style="cursor:pointer;">
                                            CD:&nbsp;
                                        </span>
                                    </div>                                    
                                    <div class="price_value">
                                        <strike style="color:black;float:left;">$<%= Model.DiscPrice.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)%></strike> 
                                        <span onclick="return AddMediaToCart(<%= Model.DiscID %>, this)" style="cursor:pointer;margin-left:4px;">
                                        <%= MyUtils.FormatPrice(Model.DiscPrice * (1 - decimal.Parse(ViewData["SubscriberDiscount"].ToString())), Model.DiscAvailable, "$", "N/A")%>
                                        </span>
                                    </div>
                                    <% }
                                        else
                                        { %>
                                    <div class="price_prompt" style="margin-left: 8px;">CD:&nbsp;</div>
                                    <div class="price_value">N/A</div>
							        <% } %>
                                </div>                            
                                <div class="clear"></div>
                        </div>
                        <span class="search_synopsis" style="margin-top: 3px; margin-bottom: 0px;">
				        <%= Model.ShippingLocation %> </span>
                    </div>
                <%}
                   else
                   { %>
                    
                        <div class="listen_button">

                <% if (Model.IsFree && Model.FileAvailable)
                   { %><a target="_blank" href="#" onclick="return streamPopupflex('<%= Url.Action("GetFreeStream", "Audio") + "?id=" + Model.FileID %>',250,220)"><img height="25" border="0" width="60" alt="FREE Listening" src="<%=Url.Image("listen_on.jpg") %>" /></a>
                   <% }
                   else
                   { %><img height="25" border="0" width="60" alt="Listening Not Available" src="<%=Url.Image("listen_off.jpg") %>" />
                   <% } %>
                </div>
                        <div class="listen_button">
                    <% if (Model.FileAvailable && !Model.FileInCart)
                       { %><a href="#" onclick="return AddToCart(<%= Model.FileID %>, this)"><img height="25" border="0" width="60" alt="Add MP3 Download to Cart" src='<%= Url.Image("add_to.jpg") %>' /></a>
                       <% }
                       else if (Model.FileAvailable && Model.FileInCart)
                       { %><img height="25" border="0" width="60" alt="MP3 Download Already In Cart" src='<%= Url.Image("in_cart.jpg") %>' />
                       <% }
                       else
                       { %><img height="25" border="0" width="60" alt="MP3 Download Not Available" src='<%= Url.Image("na.jpg") %>' />
                       <% }%>
                </div>
                    </div>
                    <div class="right_info">
                        <div class="title_info">
                            <span class="search_title">
                                <a href="#" onclick="return streamPopupflex('<%= Url.Action("Class", "Search") + "?id=" + Model.ClassID %>',670,230)" class="search_title">
                                    <%= Html.Encode(Model.Title)%>
                                </a>
                                </span><span class="search_author full_author">&nbsp;by
                                <%= Html.ActionLink(Model.SpeakerName,"resultsdetail", "search", new {speaker = Model.SpeakerName},null) %></span>
                        </div>
                        <div class="search_id full_id">
                            <%= Html.Encode(Model.Code) %>
                        </div>
                        <div class="search_synopsis">
                            <%= Model.Description %>
                        </div>
                        <div style="height: 47px; width: 413px; background-image: url(<%= Url.Image("new_background_free.jpg") %>);
                            background-repeat: no-repeat;">
                            <div style="height: 47px; width: 145px;">
					        <%if (Model.FileAvailable)
					        { %>
                                <div style="width: 117px;">
                                    <div class="price_prompt" style="margin-top: 12px; margin-left: 8px;position:absolute">
                                        MP3 Price:&nbsp;
                                    </div>
                                    <div class="price_value" style="margin-top: 12px;">
                                        <%= MyUtils.FormatPrice(Model.FilePrice1, Model.FileAvailable, "$", "N/A")%>
                                    </div>
                                </div>
                                <div style="width: 117px; margin-top: 3px;">
                                    <div class="price_prompt" style="margin-left: 8px; position:absolute">
                                        Member Price:
                                    </div>
                                    <div class="price_value">
                                        <%= MyUtils.FormatPrice(Model.FilePrice2 * decimal.Parse(ViewData["UnitsRate"].ToString()), Model.FileAvailable, "$", "N/A")%>
                                    </div>
                                </div>
					        <%}
					        else
					        {%>
                                <div style="width: 117px; *width: 125px">
                                    <div class="price_prompt" style="margin-top: 12px; margin-left: 8px;position:absolute">
                                        MP3 Price:&nbsp;
                                    </div>
                                    <div class="price_value" style="margin-top: 12px;">N/A</div>
                                </div>
                                <div style="width: 117px; margin-top: 3px;">
                                    <div class="price_prompt" style="margin-left: 8px; *margin-left: 4px;position:absolute">
                                        Member Price:
                                    </div>
                                    <div class="price_value">N/A</div>
                                </div>
					        <%} %>
                            </div>
                            <div style="height: 47px; width: 97px;">
                                <div style="width: 93px;">
                                    <% if (Model.TypeAvailable)
                                        { %>
                                    <div class="price_prompt" style="margin-top: 12px; margin-left: 8px;">
                                        <span onclick="return AddMediaToCart(<%= Model.TypeID %>, this)" style="cursor: pointer;position:absolute">
                                            Buy Tape:
                                        </span>
                                    </div>
                                    <div class="price_value" style="margin-top: 12px;">
                                        <span onclick="return AddMediaToCart(<%= Model.TypeID %>, this)" style="cursor: pointer">
                                            <%= MyUtils.FormatPrice(Model.TypePrice, Model.TypeAvailable, "$", "N/A")%>
                                        </span>
                                    </div>
                                        <% }
                                           else
                                           { %>
                                    <div class="price_prompt" style="margin-top: 12px; margin-left: 8px;">Buy Tape:</div>
                                    <div class="price_value" style="margin-top: 12px;">N/A</div>
                                    <% } %>                                
                                </div>
                                <div style="width: 93px; margin-top: 3px;">
                                    <% if (Model.DiscAvailable)
                                    { %>
                                    <div class="price_prompt" style="margin-left: 8px;">                                
                                        <span onclick="return AddMediaToCart(<%= Model.DiscID %>, this)" style="cursor:pointer;position:absolute">
                                            Buy CD:
                                        </span>
                                    </div>
                                    <div class="price_value">
                                        <span onclick="return AddMediaToCart(<%= Model.DiscID %>, this)" style="cursor:pointer">
                                        <%= MyUtils.FormatPrice(Model.DiscPrice, Model.DiscAvailable, "$", "N/A")%>
                                        </span>
                                    </div>
                                    <% }
                                        else
                                        { %>
                                    <div class="price_prompt" style="margin-left: 8px;">Buy CD:</div>
                                    <div class="price_value">N/A</div>
							        <% } %>
                                </div>
                            </div>                    

                                <% if ((bool)ViewData["ActiveSubscribe"])
                                   {%>
                                    <img title="SPECIAL OFFER Get this for FREE" src='<%= Url.Image("learn_more.jpg") %>' />
                                <% }
                                   else
                                   { %>
                                    <a href='<%= Url.RouteUrl("Offerings") %>'>
                                        <img title="SPETIAL OFFER Get this for FREE" src='<%= Url.Image("learn_more.jpg") %>' />
                                    </a>
                                <%} %>
                    
                        </div>
                        <span class="search_synopsis" style="margin-top: 3px; margin-bottom: 0px;">
				        <%= Model.ShippingLocation %> </span>
                    </div>
            <%} %>
        </div>
    </div>

