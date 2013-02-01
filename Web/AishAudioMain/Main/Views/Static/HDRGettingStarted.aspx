<%@ Page Title="" Language="C#" MasterPageFile="../Shared/oneColumnEmpty.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
   	<div id="content_wide" style="float: left;">
   	    <ul id="topMenu2">
			<li>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href="<%= Url.RouteUrl("HDRWhatIsAishAudioPage") %>">What is AishAudio</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</li>
			<li id="topMenu2_active"><a href="<%= Url.RouteUrl("HDRGettingStartedPage") %>">Getting Started</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;</li>
			<li><a href="<%= Url.RouteUrl("HDRBenefitsOfMembershipPage") %>">Benefits of Membership</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</li>
			<li><a href="<%= Url.RouteUrl("HDRAvailablePlansPage") %>">Available Plans</a></li>
		</ul>
   
		<div style="float: left; width: 320px;">
            <h1>Getting Started </h1>
			<p class="red_list">1. Create an Account:</p>
			<p> The first step in setting up an account is choosing a plan that suits your listening habits and offers other great benefits. 360 of our very best selections are available for on-line listening when you sign up for a Free Listening account. If you want to take the wisdom with you, purchase your classes at the ala carte prices, or sign up for one of our Monthly  or pre-paid memberships for BIG savings.</p>
			<p class="red_list">2. Browse our Catalog</p>
			<p>Search over 2,200 titles. You can sample any program that's available for Free Listening by clicking on the Listening button, and the items you select for purchase will appear in My Library, your online archive after checkout.</p>
			<p class="red_list">3. Listen on your computer, download and transfer to your iPod or mp3 player, or burn to an audio CD.</p>
			<p>Your My Library archive will be available as long as you remain a monthly member in good standing. Download your classes, and whether you're driving to work, exercising on the treadmill, or relaxing at home, your personal audio Torah library is available any time.</p>
			<p><a href="<%= Url.RouteUrl("HDRBenefitsOfMembershipPage") %>">Benefits of Membership</a> &gt;&gt;</p>
		</div>
		<img src="<%= Url.Image("steps.jpg") %>" style="float: right;">
	</div>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="title" runat="server">
</asp:Content>
