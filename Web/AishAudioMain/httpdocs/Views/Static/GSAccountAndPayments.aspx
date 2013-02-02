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
				<img src="<%= Url.Image("your_account_top.jpg") %>" alt="AishAudio.com Getting Started" />
				<div id="gs_right_middle">
					<p><b>How do I purchase classes?</b></p>
					<p>In order to purchase classes from this web site you need to first register and select your account type, Free Listening, Monthly, or a pre-paid 6 or 12 month membership. As a monthly member you must store your credit card information in your account so that you do not have to enter payment information every time you want to make a purchase.</p>
					<p><b>How do I register?</b></p>
					<p>The first time you use the service choose the plan your interested in on the home page. During registration you'll be asked to provide your name, email address and a username and password of your choice. The Aishaudio.com auto-responder will immediately email you a link to continue your registration. Click on the link to complete the registration process. To use the service from then on, please click the "Log in" link at the top of every page. You can start accessing free-listening or purchase downloads, tapes or CDs immediately. If you wish to change your password you can do so at any time in the MY ACCOUNT section.</p>
					<p><b>What payment methods are supported?</b></p>
					<p>We support the following credit cards: Visa, MasterCard, and American Express.</p>
					<p><b>What happens if the file I downloaded gets lost or damaged?</b></p>
					<p>As long as you remain a monthly member in good standing, all of the files that you've previously downloaded can be re-downloaded for free from the MY ACCOUNT or Personal Library section.</p>
					<p>Monthly Members: If you request that your account be cancelled, once cancellation goes into effect we no longer keep a record of your downloaded files, and you will no longer be able to download files you've downloaded previously.</p>
					<p>Standard Members: A class will be available in the Personal Library section for two-full month after the date of purchase.</p>
					<p>Pre-paid Members: At the end of your pre-paid period, your account will automatically be converted to a regular 10 units for $10 per month downloading account. Your Personal Library will remain available to you as long as you remain a monthly member in good standing. One month before your pre-paid membership is due to expire you will receive an email reminding you to cancel if you do not wish to continue as a monthly member.</p>
					<p><b>Is my credit card information safe?</b></p>
					<p>Yes. We use industry-standard Internet security procedures for transmitting all credit card information. To meet this standard, all credit card information is encrypted and securely transmitted using SSL technology. This information will not be disclosed under any circumstances.</p>
					<p><b>If I don't use all ten of my monthly units during one month, can I use them in the future?</b></p>
					<p>You will be able to carry over up to 28 units in your account that were not accessed during the months they were accrued.</p>
					<p><b>How can I get a refund?</b></p>
					<p>To obtain a refund, please submit a request through customer support. In the details of your request, provide details explaining why you are requesting a refund.</p>
					<p>We're sorry, we cannot provide refunds for anything other than technical difficulties on our end. If you downloaded the wrong class or decided you didn't really want the class you downloaded, we cannot take responsibility, as there is no way for you to "return" the product.</p>
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
