<%@ Page Title="" Language="C#" MasterPageFile="../Shared/twoColumn.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">

				<div id="content_thin2">
					<img style="float: left; clear: both; margin-top: 20px;" height="36" width="549" src="<%= Url.Image("top_banner.jpg") %>" alt="AishAudio.com" />					
					<div style="float: left; clear: both; margin-top: 10px;">
						<p style="text-align: center; margin-top: 20px; margin-bottom: 40px; font-size: 14px;"><strong>Aishaudio.com User Testimonials</strong></p>                        
						<p><b>September 2003</b></p>  
						<p>“Aishaudio.com is very comprehensive and well organized, not to mention there is a wealth of information available for a fast and easy downloading experience.”</p>
						<div align="right"><i><b>- Melanie Goldfader</b><br />
						Public Relations Account Representative<br />
						St. Louis MO</i></div><br />
  
						<p>“This is great material &mdash; so convenient and so easy to access. All the classes are on this little tiny thing in my pocket (the mp3 player) … it is just great.”
						</p><div align="right"><i><b>- Alan Cohen</b><br />
						Chairman, Actuarial Systems Corp<br />
						Los Angeles</i></div><br />
  
						<p>“I’m impressed that Aish is on the cutting edge of technology. I can download Aish’s tape library and go anywhere I want with the classes.”</p>
						<div align="right"><i><b>- Michael William</b><br />
						Hockey Coach<br />
						New York</i></div><br />
  
						<p>“What an amazing invention! I have strived my whole life to make Jewish wisdom available to as many people as possible. Aishaudio.com is the new way to make it happen. Anyone, anywhere in the world can now access great   Jewish wisdom from some of the best teachers I know.”
						</p><div align="right"><i><b>- Rabbi Noah Weinberg</b><br />
						Dean and Founder of Aish HaTorah<br />
						Jerusalem</i></div>
					</div>
				</div>				

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="title" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
</asp:Content>
