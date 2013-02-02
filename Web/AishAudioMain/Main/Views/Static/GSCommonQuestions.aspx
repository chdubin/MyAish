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
				<img src="<%= Url.Image("common_questions_top.jpg") %>" alt="">
				<div id="gs_right_middle">
					<p><b>1. When I click the download button nothing happens on my PC?</b><br />If the download doesn't start right away, click on the link to start download.</p>
					<p><b>2. How do I download aishaudio programs using a Mac?</b><br />In your Personal Library, click on the "Low" link or the "High" link download and double click. For the "Low" format you will need to install a player that can handle Windows Media Player files. Quicktime does not support them, and iTunes will not play them, but will import them as mp3s if you have that option set in iTunes.</p>
					<p><b>3. I have a monthly listener plan. Do my credits carry over to the next month?</b><br />Yes, maximum of 28 credits will carry over to subsequent months.</p>
					<p><b>4. My Personal Library listings have disappeared. Why?</b><br />The Personal Library feature is only available to monthly members in good standing. If you cancel your monthly membership, your Library listings will no longer be available. Any classes that you purchase at the ala carte rates will be available in your Library for two months after the date of purchase.</p>
					<p><b>5. How do I use my Mac to listen to classes and burn them to CDs?</b><br />Simply download the "High" version, drag and drop the file onto "Toast" or other CD burning software and burn. The "Low" version is not compatible for CD burning on the Mac but can be listened to using "Microsoft Windows Media Player", or imported into iTunes..</p>
					<p><b>6. Can I use my Mac to transfer to a portable device?</b><br />Yes, as long as the device is Mac compatible (our "giveaway" device is Mac compatible).</p>
					<p><b>7. How can I burn a CD that is longer than 80 minutes on a PC?</b><br />Apple iTunes is a free download available at www.itunes.com. It has a CD burning feature that let's you to burn long files onto multiple audio CDs.</p>
					<p>Just follow these easy steps:</p>
					<p>Using iTunes:</p>
					<ol style="list-style-type: lower-alpha;">
						<li>Download a class from Aishaudio.com</li>
						<li>Open iTunes and add the class you want to burn to CDs into your iTunes Library</li>
						<li>Create a playlist and drag the class you want to burn from your iTunes Library to the playlist.</li>
						<li>Select the playlist with your cursor and select Burn to CD in the File menu.</li>
						<li>Add blank CDs to your CD burner when prompted.</li>
					</ol>
					<br /><br />
					<p>From within Nero Express (CD burning software that usually comes with a CD burner):</p>

					<ol style="list-style-type: lower-alpha;">
						<li>Launch Nero Express</li>
						<li>Select Music-&gt;Audio CD</li>
						<li>Click on the 'Add' button and add your audio file, then select 'Finished'</li>
						<li>Highlight your audio file and right click on the 'Properties' button</li>
						<li>Select the 'Index, Limits, Splits' tab</li>
						<li>Now click anywhere in the WAV form with your mouse and then click on the 'Split' button to split the audio at this point. This does not modify the actual file.</li>
						<li>Click OK or Apply to have your changes take affect</li>
						<li>On the second file in your list, right-click with your mouse and and "Cut"</li>
						<li>Burn the first CD and eject</li>
						<li>Delete the first file and "Paste" the second file into the Audio CD window and burn the second disc</li>
					</ol>
					<br /><br />
					<p><b>9. Does aishaudio.com sell CDs or cassette tapes?</b><br />Yes! In addition to digital downloads, we've recently incorporated our Tape and CD selections into Aishaudio.com. Monthly members receive a 30% discount on all tape and CD purchases. If a tape or CD is not available for a particular class, the Purchase Tape or CD icons will appear in light grey.</p>
					<p><b>10. How do I cancel my aishaudio.com subscription?</b><br />You may cancel your subscription to particular Aish Content ("Subscription") by clicking on the appropriate link in the My Account section or by contacting our Customer Service Department via telephone at 1-800 VOICES3 subject to the terms and conditions set forth in our Policy on Aish Content Plans.</p>
					<p><b>11. Will I lose my previous downloads if I cancel my monthly subscription?</b><br />No, once you have downloaded classes they're yours to keep, and you can listen to them whenever you want, even if you subsequently cancel your subscription. Your Personal Library, however, will not be available.</p>
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
