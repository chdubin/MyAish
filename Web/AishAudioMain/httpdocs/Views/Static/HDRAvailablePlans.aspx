<%@ Page Title="" Language="C#" MasterPageFile="../Shared/oneColumnEmpty.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">

   	<div id="content_wide" style="float: left;">
   	    <ul id="topMenu2">
			<li>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href="<%= Url.RouteUrl("HDRWhatIsAishAudioPage") %>">What is AishAudio</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</li>
			<li><a href="<%= Url.RouteUrl("HDRGettingStartedPage") %>">Getting Started</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;</li>
			<li><a href="<%= Url.RouteUrl("HDRBenefitsOfMembershipPage") %>">Benefits of Membership</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</li>
			<li id="topMenu2_active"><a href="<%= Url.RouteUrl("HDRAvailablePlansPage") %>">Available Plans</a></li>
		</ul>
   
		<div style="float: left; width: 500px;">
            <h1>Available Plans</h1>
			<p>Listen free, or SAVE BIG with one of our Discount Membership Plans. </p>
			<p align="left" class="style18 style20"><img height="361" border="0" width="489" usemap="#Map3" alt="All 3" src="<%= Url.Image("all_3.jpg") %>" /></p>
			<p><a href="http://aishaudio.com">Home</a> &gt;&gt;</p>
		</div>
        <div style="float: right; width:220px; text-align: center; margin-top: 20px;">
            <p>Paid members enjoy<br />additional benefits:&nbsp;&nbsp;&nbsp;&nbsp;</p>
			<img height="304" width="138" alt="Additional Benefits" src="<%= Url.Image("column_2.jpg") %>" />
        </div>	
</div>

    <map name="Map3" id="Map3"><area shape="rect" coords="232,271,354,293" href="#" alt="Free MP3 Deal" />
    <area shape="rect" coords="371,269,451,298" href="#" alt="" />
    <area shape="rect" coords="382,165,453,201" href="#" alt="Monthly" />
    <area shape="rect" coords="378,73,461,98" href="#" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="title" runat="server">
</asp:Content>
