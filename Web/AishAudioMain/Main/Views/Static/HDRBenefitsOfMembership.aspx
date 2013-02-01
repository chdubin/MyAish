<%@ Page Title="" Language="C#" MasterPageFile="../Shared/oneColumnEmpty.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">

   	<div id="content_wide" style="float: left;">
   	    <ul id="topMenu2">
			<li>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href="<%= Url.RouteUrl("HDRWhatIsAishAudioPage") %>">What is AishAudio</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</li>
			<li><a href="<%= Url.RouteUrl("HDRGettingStartedPage") %>">Getting Started</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;</li>
			<li id="topMenu2_active"><a href="<%= Url.RouteUrl("HDRBenefitsOfMembershipPage") %>">Benefits of Membership</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</li>
			<li><a href="<%= Url.RouteUrl("HDRAvailablePlansPage") %>">Available Plans</a></li>
		</ul>
   
		<div style="float: left; width: 320px;">
            <h1>Benefits of Membership</h1>
			<p class="red_list">1. Listen to the best.</p>
			<p> These days you have a lot of choices when it comes to Torah Audio on the web, but only AishAudio offers you the unique Wisdom for Living that has been Aish HaTorah's stock and trade for over 30 years. Free Listening Members have instant access to over 360 of our best classes and speakers.</p>
			<p class="red_list">2. The Wisdom you need, when you need it.</p>
			<p>At Aishaudio, we understand you're pressed for time and it's important for you to maximize your Torah learning. That's why we offer plans that let you download classes you can take with you anywhere for anytime listening. Left your mp3 player at home? Monthly members can access the classes they've purchased from any computer.</p>
			<p class="red_list">3. Save Big with Discount Memberships.</p>
			<p>We love to save you money, and with a Monthly Membership you'll pay just $1 for most classes. That's just a fraction of the price of tapes or CDs, and over 60% off the regular ala carte price of our mp3s. Even better, your first month is free -- (a $10 value ($35 at non-member prices) -- and you can quit any time.</p>
			<p><a href="<%= Url.RouteUrl("HDRAvailablePlansPage") %>">Available Plans</a> &gt;&gt;</p>
		</div>
		<img src="<%= Url.Image("steps.jpg") %>" style="float: right;">
	</div>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="title" runat="server">
</asp:Content>
