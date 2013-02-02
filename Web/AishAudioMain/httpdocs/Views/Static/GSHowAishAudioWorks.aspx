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
				<img src="<%= Url.Image("steps.jpg") %>" />
				<br /><br /><br />
				<p>Getting started with <b>Aishaudio</b> is quick and easy. After you create your <b>Aishaudio</b> account, download and install software to manage your audio on your PC, or use iTunes 3.0 and above for the PC and Mac. Now you're ready to shop!</p>
				<p>Pick out and purchase the audio programs you want, buy them, and download them to your computer. Then just load up your mobile audio player (like the Apple iPod) and listen anytime, anywhere. You can also listen to Aishaudio right on your computer, or burn it to CDs that will work in any standard CD player. Our system requirements are:</p>
				<p><b>PC</b></p>
				<p>Operating System: Windows 98 SE, Windows ME, Windows 2000, Windows XP or Windows Vista Browsers: Internet Explorer, Firefox, AOL, or SBC Yahoo</p>
				<p><b>Mac</b></p>
				<p>Operating System: Mac OS X 10.1.5 or higher with iTunes 3 or higher Browsers: Safari, Firefox, or Camino</p>
			</div>
		</div>
	</div>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="title" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
</asp:Content>
