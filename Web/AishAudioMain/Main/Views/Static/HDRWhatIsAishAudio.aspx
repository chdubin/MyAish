<%@ Page Title="" Language="C#" MasterPageFile="../Shared/oneColumnEmpty.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">

   	<div id="content_wide" style="float: left;">
   	    <ul id="topMenu2">
			<li id="topMenu2_active">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href="<%= Url.RouteUrl("HDRWhatIsAishAudioPage") %>">What is AishAudio</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</li>
			<li><a href="<%= Url.RouteUrl("HDRGettingStartedPage") %>">Getting Started</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;</li>
			<li><a href="<%= Url.RouteUrl("HDRBenefitsOfMembershipPage") %>">Benefits of Membership</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</li>
			<li><a href="<%= Url.RouteUrl("HDRAvailablePlansPage") %>">Available Plans</a></li>
		</ul>
   
		<div style="float: left; width: 320px;">
            <h1>What is AishAudio? </h1>
			<p>It's the place you go for instant access to over 2,200 (and growing) digital audio classes by more than one hundred of the great Jewish minds from around the world. </p>
			<p>Come join the tens of thousands of people who have enriched their lives through our audio Torah classes. Our dynamic speakers are available to you 24/6, and digital downloads make it easy to take our wisdom for living with you on the the road.  Whatever interests you most -- Jewish History, law, the Torah, raising teenagers, prayer, Israel, and many more-- you'll find it  just a few clicks away.</p>
			<p> <a href="<%= Url.RouteUrl("HDRGettingStartedPage") %>">Getting Started </a>&gt;&gt;</p>
		</div>
		<img src="<%= Url.Image("steps.jpg") %>" style="float: right;">
	</div>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="title" runat="server">
</asp:Content>
