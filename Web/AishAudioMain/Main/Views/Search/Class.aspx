<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Main.Models.ControllerView.Search.ClassListItem>" %>

<%@ Import Namespace="MainCommon" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AishAudio Player</title>
    <link href="<%= Url.Css("aishstyles_yisrael.css") %>" rel="stylesheet" type="text/css" />
   <%
        //Model.IsFree &&         
        System.Web.HttpBrowserCapabilities br = Request.Browser;
        if ((bool)ViewData["showPlayer"] && !br.Type.Contains("IE")) { 
   %>
        <link href="<%= Url.Css("jplayer.blue.monday.css") %>" rel="stylesheet" type="text/css" />
        <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8/jquery.min.js"></script>
        <script type="text/javascript" src="<%= Url.Content("~/Scripts/jquery/jPlayer/jquery.jplayer.min.js") %>"></script>
        <script type="text/javascript" src="<%= Url.Content("~/Scripts/jquery/jPlayer/jquery.jplayer.inspector.js") %>"></script>
        <script type="text/javascript">
        //<![CDATA[
                $(document).ready(function () {

                    $("#jquery_jplayer_1").jPlayer({
                        ready: function (event) {
                            $(this).jPlayer("setMedia", {
                                mp3: "<%= Main.MvcApplication.S3Amazon.GetPreSignedUrlRequest(Model.FilePath) %>"
                                //oga: "http://www.jplayer.org/audio/ogg/TSP-01-Cro_magnon_man.ogg"
                            }).jPlayer("play"); //attempts to start the player automatically
                        },
                        //cssSelector: { currentTime: ".jp-current-time" },
                        swfPath: "<%= Url.Content("~/Scripts/jquery/jPlayer") %>",
                        supplied: "mp3",
                        solution: "html, flash",
                        wmode: "window"
                    });

                    $("#jplayer_inspector").jPlayerInspector({ jPlayer: $("#jquery_jplayer_1") });
                });
        //]]>
        </script>
    <% } %>
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

<%
    
    //Model.IsFree && 
    if ((bool)ViewData["showPlayer"]) 
   
   { %>
                                <tr>
                                    <td><img src='<%= Url.Image("spacer.gif") %>' width="15" height="1" alt="" /></td>
                                    <td><img src='<%= Url.Image("spacer.gif") %>' width="15" height="1" alt="" /></td>
                                    <td colspan="3" align="left">

    <table cellpadding="0" cellspacing="0" border="0" width="250">
        <tr>
            <td align="center" height="45">

               <% System.Web.HttpBrowserCapabilities br = Request.Browser; 
                  if (Request.UserAgent.Contains("Windows") && br.Type.Contains("IE"))
                  { %>
            <OBJECT ID="MediaPlayer1" width=225 height=69 classid="CLSID:22D6F312-B0F6-11D0-94AB-0080C74C7E95"
                codebase="http://activex.microsoft.com/activex/controls/mplayer/en/nsmp2inf.cab#Version=6,0,02,902"
                standby="Loading Microsoft Windows Media Player components..."
                type="application/x-oleobject" VIEWASTEXT>
                <PARAM NAME="FileName" VALUE="<%= Main.MvcApplication.S3Amazon.GetPreSignedUrlRequest(Model.FilePath) %>">
                <PARAM NAME="AutoStart" VALUE="true">
                <PARAM NAME="ShowStatusBar" VALUE="true">
                <EMBED type="application/x-mplayer2"
                    pluginspage = "http://www.microsoft.com/Windows/MediaPlayer/"
                    SRC="<%= Main.MvcApplication.S3Amazon.GetPreSignedUrlRequest(Model.FilePath) %>"
                    name="MediaPlayer1" width=225 height=69 ShowStatusBar=-1 AutoStart=true>
                </EMBED>
            </OBJECT>
            <%}
                  else
                  { %>

<!-- <JPLAYER> -->
                                                <div id="jquery_jplayer_1" class="jp-jplayer"></div>

                                                <div id="jp_container_1" class="jp-audio">
                                                    <div class="jp-type-single">
                                                        <div class="jp-gui jp-interface">
                                                            <ul class="jp-controls">
                                                                <li><a href="javascript:;" class="jp-play" tabindex="1">play</a></li>
                                                                <li><a href="javascript:;" class="jp-pause" tabindex="1">pause</a></li>
                                                                <li><a href="javascript:;" class="jp-stop" tabindex="1">stop</a></li>
                                                                <li><a href="javascript:;" class="jp-mute" tabindex="1" title="mute">mute</a></li>
                                                                <li><a href="javascript:;" class="jp-unmute" tabindex="1" title="unmute">unmute</a></li>
                                                                <li><a href="javascript:;" class="jp-volume-max" tabindex="1" title="max volume">max volume</a></li>
                                                            </ul>
                                                            <div class="jp-progress">
                                                                <div class="jp-seek-bar">
                                                                    <div class="jp-play-bar"></div>

                                                                </div>
                                                            </div>
                                                            <div class="jp-volume-bar">
                                                                <div class="jp-volume-bar-value"></div>
                                                            </div>
                                                            <div class="jp-current-time"></div>
                                                            <div class="jp-duration"></div>
                                                            <ul class="jp-toggles">
                                                                <li><a href="javascript:;" class="jp-repeat" tabindex="1" title="repeat">repeat</a></li>
                                                                <li><a href="javascript:;" class="jp-repeat-off" tabindex="1" title="repeat off">repeat off</a></li>
                                                            </ul>
                                                        </div>
                                                        <div class="jp-title">
                                                            <ul>
                                                                <li><%= Model.Title%></li>
                                                            </ul>
                                                        </div>
                                                        <div class="jp-no-solution">
                                                            <span>Update Required</span>
                                                            To play the media you will need to either update your browser to a recent version or update your <a href="http://get.adobe.com/flashplayer/" target="_blank">Flash plugin</a>.
                                                        </div>
                                                    </div>
                                                </div>
                                        <!-- </JPLAYER> -->
        <!--<div id="jplayer_inspector"></div>-->
               <% } %>
            </td>
        </tr>
    </table>

                                        <img src='<%= Url.Image("spacer.gif") %>' width="15" height="5" alt="" />

<% } %>


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
