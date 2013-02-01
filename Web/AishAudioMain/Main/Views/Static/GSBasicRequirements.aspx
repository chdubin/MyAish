<%@ Page Title="" Language="C#" MasterPageFile="../Shared/twoColumn.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">

	<div id="content_thin2">
		<img style="float: left; clear: both; margin-top: 20px;" height="36" width="549" src="<%= Url.Image("banner.jpg") %>" alt="AishAudio.com" />

		<div style="float: left; margin-top: 15px; width: 549px;">
			<ul id="menu_get_start">
				<li style="background: none;"><a href="<%= Url.RouteUrl("GSHowAishAudioWorksPage") %>" style=" padding: 0px;"><img src="<%= Url.Image("how_works_title.jpg") %>" alt="AishAudio.com - Who does it work?" /></a></li>
				<li><a href="<%= Url.RouteUrl("GSWhatIsAishAudioPage") %>" >What is Aishaudio</a></li>
				<li><a href="<%= Url.RouteUrl("GSBasicRequirementsPage") %>">Basic&nbsp;Requirements</a></li>
				<li><a href="<%= Url.RouteUrl("GSFindingClassesPage") %>">Finding Classes</a></li>
				<li><a href="<%= Url.RouteUrl("GSDownloadAndStreamPage") %>">Downloading/Listening</a></li>
				<li><a href="<%= Url.RouteUrl("GSBurningExportingPage") %>">Burning/Exporting</a></li>
				<li><a href="<%= Url.RouteUrl("GSAccountAndPaymentsPage") %>">Your Account/Payment</a></li>
				<li><a href="<%= Url.RouteUrl("GSCommonQuestionsPage") %>">Common Questions</a></li>
				<li id="last_list"><a href="<%= Url.RouteUrl("GSCustomerSupportPage") %>">Customer Support</a></li>
			</ul>

    		<div id="Get_start_right1">
			    <img src="<%= Url.Image("basic_requirement_top.jpg") %>" alt="">
			    <div id="gs_right_middle">
				    <p><b>What browsers are supported?</b></p>
				    <p>For PC users, we support the following browsers: For PC: The latest Internet Explorer and Firefox browsers. For Mac users, Safari works well. We have had issues with Internet Explorer for the Mac and do not recommend it. Free Listening on the Mac requires Quicktime.</p>
				    <p><b>What Players are supported?</b></p>
				    <p>This website supports the streaming and downloading of music in more than one format (WMA and MP3). To play Free Listening files on you PC, Windows Media Player must be installed. To play Free Listening files on your Mac, Quicktime must be installed. Once classes are downloaded to your computer you can use any software that plays MP3 or WMA files such as Windows Media Player or Real Player. Apple's iTunes will not play our low bandwidth WMA files, (it will try to import them as MP3 or another format), but will have no problem with our MP3s.</p>
				    <p>In order to preview or download music in any format from this website you must first install Windows Media Player, RealOne, iTunes, or (for MP3 downloads and streaming only).</p>
				    <p><b>What formats are supported?</b></p>
				    <p>We support the following download formats: MP3 and WMA. Click on the icon for low bandwidth download (WMA format) or high bandwidth (MP3 format). Free listening* is in MP3 format only.</p>
				    <p>*Free listening is not available for all titles.</p>
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
