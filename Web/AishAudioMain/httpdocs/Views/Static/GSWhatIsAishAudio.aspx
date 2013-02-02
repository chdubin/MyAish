<%@ Page Title="" Language="C#" MasterPageFile="../Shared/twoColumn.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">

	<div id="content_thin2">
		<img style="float: left; clear: both; margin-top: 20px;" height="36" width="549" src="<%= Url.Image("banner.jpg") %>" alt="AishAudio.com" />

		<div style="float: left; margin-top: 15px; width: 549px;">
			<ul id="menu_get_start">
				<li style="background: none;"><a href="<%= Url.RouteUrl("GSHowAishAudioWorksPage") %>" style=" padding: 0px;"><img src="<%= Url.Image("how_works_title.jpg") %>" alt="AishAudio.com - Who does it work?" /></a></li>
				<li><a href="<%= Url.RouteUrl("GSWhatIsAishAudioPage") %>">What is Aishaudio</a></li>
				<li><a href="<%= Url.RouteUrl("GSBasicRequirementsPage") %>">Basic&nbsp;Requirements</a></li>
				<li><a href="<%= Url.RouteUrl("GSFindingClassesPage") %>">Finding Classes</a></li>
				<li><a href="<%= Url.RouteUrl("GSDownloadAndStreamPage") %>">Downloading/Listening</a></li>
				<li><a href="<%= Url.RouteUrl("GSBurningExportingPage") %>">Burning/Exporting</a></li>
				<li><a href="<%= Url.RouteUrl("GSAccountAndPaymentsPage") %>">Your Account/Payment</a></li>
				<li><a href="<%= Url.RouteUrl("GSCommonQuestionsPage") %>">Common Questions</a></li>
				<li id="last_list"><a href="<%= Url.RouteUrl("GSCustomerSupportPage") %>">Customer Support</a></li>
			</ul>

			<div id="Get_start_right1">
				<img src="<%= Url.Image("what_is_aishaudio_top.jpg") %>" alt="" />
				<div id="gs_right_middle">
					<p>	This web site offers a searchable database of over 1400 (and growing) classes, lectures and seminars available for download from the <b>Aish HaTorah &ndash; Voices From Jerusalem</b> catalog. It is the largest, most comprehensive catalog of secure downloadable Torah content for sale on the Internet.</p>
					<p>Here is how this site works:</p>
					<p><b>Free Listening:</b></p>
					<ol>
						<li>Download a player (Windows Media Player is recommended).</li>
						<li>Find the class you want.</li>
						<li>Click on the link for Free Listening (if available).</li>
						<li>Login, or if you have not yet signed up for Free Listening, do so at this time.</li>
						<li>Listen to the class on your computer.</li>
					</ol>
					<br />
					<p><b>MP3 or WMA Download:</b></p>

					<ol>
						<li>Find the class you want.</li>
						<li>Login, or if you have not yet signed up for a Free Listening or monthly plan, do so at this time.</li>
						<li>Add a class you want to purchase to the shopping cart by clicking on the Download icon.</li>
						<li>Finish the checkout process and click on the link to bring you to your Personal Library.</li>
						<li>Click on the icon for low bandwidth download (WMA format) or high bandwidth (MP3 format).</li>
						<li>Save the file in the directory of your choice.</li>
					</ol><br />
				</div>
				<img src="<%= Url.Image("bottom.jpg") %>" alt="AishAudio.com Getting Started" /><br />
			</div>
		</div>
	</div>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="title" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
</asp:Content>
