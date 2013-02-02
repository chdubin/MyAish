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
				<img src="<%= Url.Image("download_listening_top.jpg") %>" alt="">
				<div id="gs_right_middle">
					<p><b>How do I buy a class?</b></p>
					<p><b>Monthly Plan</b></p>
					<p>Members of our $10/month Monthly Plan are given 10 units to use during a monthly period.* When you select a class to be downloaded, your account is automatically debited the amount of units listed for the class. Once your monthly supply of units has been exhausted, you can purchase additional units for the price of $1.00 each. If your unit supply is at zero and you attempt to download additional classes, a minimum charge of $5.00 for five units will appear in your shopping cart.</p>
					<p>*Pre-paid 6 or 12 month plans work the same way -- 10 units accrue each month of the pre-paid period..</p>
					<p><b>Standard Plan (Free Listening Members)</b></p>
					<p>Non-monthly users can purchase 1 unit classes for $3.50 or 2 unit classes for $5.00. Certain classes may cost more. See listed prices. Any Free Listening member who makes a pay as you go purchase automatically becomes a Standard Member. Also, any user who cancels his or her Monthly Membership immediately reverts to Standard.</p>
					<p><b>Can I listen to all of the files on AishAudio for free?</b></p>
					<p>Approximately three hundred and fifty of our classes are available for free listening. If a class is not available the icon for free listening will appear in light grey. Any file that is purchased on a Monthly Plan or pre-paid plan will be available for re-downloading in your Personal Library as long as your membership remains in good standing. Free listening in the Personal Library will work even if a file is not available for free listening in the main listings.</p>
					<p><b>How long does it take to download a class?</b></p>
					<p>This depends on your connection speed and the song's file size. To estimate how long a download may take, look below and find the speed closest to your actual connection speed and multiply the estimated time by the # of minutes for your download.</p>
					<p><b>56K takes about 2.5 minutes per minute of class</p>
					<p>128K takes about 1 minute per minute of class</p>
					<p>384K (DSL or Cable Modem) takes about 25 seconds per minute of a class</p>
					<p>1M (LAN) takes about 8 seconds per minute of a class.</b></p>
					<p>For example, an MP3 file containing a 60-minute class will take approximately 2 and 1/2 hours to download via a 56K modem. If you are using a 28.8K modem, it will take twice as long.</p>
					<p>*Please note that Internet congestion might add time to the download process.</p>
					<p><b>How do I listen to a class?</b></p>
					<p>To listen to classes you have already downloaded, simply launch your, Windows Media Player or RealOne Player and select the class you wish to listen to.</p>
					<p><b>Where are my files stored after they are successfully downloaded?</b></p>
					<p>By default, files are typically stored in the C drive on your hard disk. However, most applications will allow you to change the default location where you want to store your files.</p>
					<p>If you do not recall where you specified the default storage location, you may use the "Find File" utility to search your hard disk. The utility is located on your Windows menu bar under the Start button. Select "Find" and then "Files or Folders". Specify the file extension (e.g., *.mp3 or *.wma) in the named dialog box and click "Find Now".</p>
					<p><b>When I listen to a class in streaming audio, I notice that my computer has a full copy of the mp3 file in cache. Am I allowed to save the cached file?</b></p>
					<p>No, keeping the cached file without paying is prohibited according to Jewish law. Similarly, recording an audio stream of one of our classes to a digital file or with any type of recording device is prohibited.</p>
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
