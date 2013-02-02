<%@ Page Title="" Language="C#" MasterPageFile="../Shared/twoColumn.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ContentPlaceHolderID="body" runat="server">
    <style type="text/css">
		span {
			display: block;
			float: left;
			clear: both;
			font-family: Verdana, Geneva, sans-serif;
			color: #33578b;
			margin-top: 5px;
			width: 100%;
        }
							
		a { color: #33578b;}
	</style>
                
<div id="content_thin2">
	<img style="float: left; clear: both; margin-top: 20px;" src="<%= Url.Image("contact_us_top.jpg") %>" alt="contact AishAudio.com" />
                    
	<div style="float: left; margin-top: 10px; width: 549px;">
			<img style="float: left; clear: both;"  src="<%= Url.Image("contact_us_header.jpg") %>" />
			<div style="float: left; background-image:url(<%= Url.Image("contact_us_middle.jpg") %>); padding-left: 18px; width: 536px;">
				<span style="font-size: 15px; font-style: italic;" class="red_list">Voices from Jerusalem</span>
				<img style="float: left; clear: both; margin-left: -18px;" src="<%= Url.Image("contact_us_line.jpg") %>">
				<br /><br />
				<span><span class="red_list">USA:&nbsp;&nbsp;</span>28 Park Avenue, Airmont, NY 10952</span>
				<span><span class="red_list">Toll Free:&nbsp;&nbsp; </span>1-800-VOICES-3</span>
				<br /><br /><br /><br />
				<span><span class="red_list">Israel:&nbsp;&nbsp; </span>1 Western Wall Plaza, POB 14149, Old City, Jerusalem 97500</span>
				<span><span class="red_list">Tel:&nbsp;&nbsp; </span>(972-2) 628-5666 ext 230 or 232</span>
				<span><span class="red_list">Fax:&nbsp;&nbsp; </span>(972-2) 627-3172</span>
				<br /><br /><br /><br /><br /><br />
				<img src="<%= Url.Image("contact_us_line.jpg") %>" style="float: left; clear: both; margin-left: -18px;">
				<br />
<span><b><i>Having a Technical, Billing or Other Problem?</i></b>&nbsp;<a href="<%= Url.RouteUrl("HelpFAQPage") %>">Click Here.</a></span><br /><br />
				<span>For our <b>tape / cd catalog,</b> email <a href="mailto:Audio@aish.com">Audio@aish.com</a></span>
<br /><br />
				<span><a href="http://aish.com">aish.com</a></span>
				<br /><br /><br />
				<img src="<%= Url.Image("contact_us_line.jpg") %>" style="float: left; clear: both; margin-left: -18px;">
				<br /><br />
				<span style="font-size: 15px; font-style: italic; margin-left: 90px; *margin-left: 45px;" class="red_list">Support Israel. Buy Jerusalem Wisdom.</span><br /><br /><br />
            </div>                       
                           
            <img style="float: left; clear: both;" src="<%= Url.Image("contact_us_footer.jpg") %>">                 
						
</div>				
</div>			
</asp:Content>