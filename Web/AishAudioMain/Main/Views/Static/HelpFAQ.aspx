<%@ Page Title="" Language="C#" MasterPageFile="../Shared/twoColumn.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ContentPlaceHolderID="body" runat="server">

				<div id="content_thin">
					<img style="margin-top: 20px;" height="36" width="549" src="<%= Url.Image("help_banner.jpg") %>" />
					<p style=" margin-left: 20px; margin-top: 40px; font-style: italic; color: #2a60b9; width: 520px; line-height: 14px; font-family: Arial, Helvetica, sans-serif;">If you are experlencing any technical difficulties, please check through the following list of Frequently Asked Tecnical Questions, to see if one of the answers applies to your problem. If not, please use the Customer Service Email form that follows to send us a description of your problem. One of our representatives will respond to your query within 24 hours.</p>
                    <br />
					<p style="color: #000;"><strong>9/12/2010 - Our credit card server is currently not communicating with our main program. If you are trying to purchase a class ala carte, please try back tomorrow. If you would like to sign up for a monthly subscription, please call 973-767-3700 for assistance. Monthly members and members with units in their account will be able to purchase classes with their units as usual.</strong></p>
					<p><strong>4/21/2009</strong> - <strong>The problem with the database server has been resolved as of 12:35 EDT.</strong></p>
					<p><strong>4/21/2009</strong> - <strong>The database server where the My Library records are kept is being upgraded. Due to a technical glitch, the past few months of My Library records are temporarily unavailable. Our tech people predict that everything should be working again sometime Wednesday morning, so if you are trying to download a class that was in your library, please check back then.</strong></p>
					<p><strong>4/1/2009 - Please Note: We've been undergoing technical difficulties over the past couple of days. The only outstanding issue is our connection with our credit card server. Our tech people are working frantically to fix the problem. Members with units can use them to purchase classes, but unfortunately, cash transactions, and monthly membership authorizations are not going through. Please check back later to see if the problem has been resolved.</strong></p><br />

					<span class="red_list">FREQUENTLY ASKED TECHNICAL QUESTIONS</span>
					<img height="6" width="549" style="margin: 5px 0pt 20px 0px;" src="<%= Url.Image("blue_line.jpg") %>">
					<br />
					<ol id="top_questions">
						<li><a href="#1">I signed up for a yearly subscription, but in the My Account section it lists me as Monthly. Is that a mistake?</a></li>
						<li><a href="#2">The file I attempted to download was very short, or was a different class than the one listed?</a></li>
						<li><a href="#3">My username and password are not working? Why not?</a></li>
						<li><a href="#4">I get an error message when I try to sign up for a Monthly or Yearly subscription using Internet Explorer on my Mac. Why?</a></li>
						<li><a href="#5">I signed up for a yearly subscription, but the program is charging me $1 or $2 per unit. Why?</a></li>
						<li><a href="#6">I recently cancelled my Monthly Membership and the classes I purchased previously are no longer showing up in my Personal Library. What happened to them?</a></li>
						<li><a href="#7">I'm a Standard User and all the classes I purchased earlier than 2 months ago are no longer showing up in my Personal Library. What happened to them?</a></li>
						<li><a href="#8">I'm a Monthly User and although I've been a member for several months, I have only 28 units in my account?</a></li>
						<li><a href="#9">My credit card was declined, but I still have units left in my account. I don't wish to continue as monthly, but I'd like to be able to access my units. Your website tells me that my account is invalid. How do I get it to work without having to pay for a new membership?</a></li>
					</ol>
                    
                    <br /><br /><br />

					<span class="red_list">ANSWERS</span>
                    <img height="6" width="549" style="margin: 5px 0pt 20px 0px;" src="<%= Url.Image("blue_line.jpg") %>" alt="" />
                    
					<div id="answers">

					<a name="1"><p><span class="red_list">Q:</span><b> I signed up for a pre-paid subscription (MP3 player deal), but in the My Account section it lists me as Monthly. Is that a mistake?</b></a></p>
					<p><span class="red_list">A:</span> The Aishaudio system only recognizes two types of memberships: Free Listening and Monthly. Although you pre-paid for 6 months or a year, the system will list you as a Monthly Member throughout the duration of your pre-paid membership.</p>

					<a name="2"><p><span class="red_list">Q:</span><b> The file I attempted to download was very short, or was a different class than the one listed.</b></a></p>
					<p><span class="red_list">A:</span> In very, very rare occasions you may encounter this type of problem with one of the files. If you do encounter this problem, please contact customer service with the catalog number of the file with as well as a brief description of the problem occurring.</p>

					<a name="3"><p><span class="red_list">Q:</span><b> My username and password are not working? Why not?</b></a></p>
					<p><span class="red_list">A:</span> If you have previously set up a username and password as part of your Aish.com membership, that information will not work on the Aishaudio.com site and you will need to register for an Aishaudio.com account separately. On the homepage   follow the links for a Download Plan, or Free Listening.  If you're sure you've signed up for an Aishaudio.com account, please remember that usernames and passwords are case sensitive. If you've used a lower case letter in place of an uppercase, or an uppercase in place of a lowercase, your login will fail. If you're not sure of your password, please click on the appropriate link at Aishaudio.com to have your username and password emailed to you.</p>

					<a name="4"><p><span class="red_list">Q:</span><b> I get an error message when I try to sign up for a Monthly or Yearly subscription using Internet Explorer on my Mac. Why?</b></a></p>
					<p><span class="red_list">A:</span> We are having problems getting registration to work on IE on the Mac. Please use Safari to sign up for one of our paid subscriptions.</p>

					<a name="5"><p><span class="red_list">Q:</span><b> I signed up for a yearly subscription, but the program is charging me $1 or $2 per unit.&nbsp; Why?</b></a></p>
					<p><span class="red_list">A:</span> When you first sign up for a yearly subscription, and each month on the day of the month your registered,  you will be receiving an allocation of 10 units. If during a particular month you use up your 10 units, you will be charged $1 per unit for any additional classes that you download.</p>

					<a name="6"><p><span class="red_list">Q:</span><b> I recently cancelled my Monthly Membership and the classes I purchased previously are no longer showing up in my Personal Library. What happened to them?</b></a></p>
					<p><span class="red_list">A:</span> The Personal Library archive of previously purchased classes is only available to Monthly (or Pre-paid) Members. Once you cancelled your account, it was no longer available. Please make sure to download all of the classes you purchased before cancelling your Monthly Membership.</p>

					<a name="7"><p><span class="red_list">Q:</span><b> I'm a Free Listening member who purchased classes at ala carte prices, and all the classes I purchased earlier than 2 months ago are no longer showing up in my Personal Library. What happened to them?</b></a></p>
					<p><span class="red_list">A:</span> Listings of your previously purchased classes appear in your Personal Library for up to 2 months in order to give you ample time to complete a download. After 2 months, those classes are removed from your Personal Library.</p>

					<a name="8"><p><span class="red_list">Q:</span><b> I'm a Monthly User, and although I've been a member for several months, I only have 28 units in my account. What happened to the rest of my units?</b></a></p>
					<p><span class="red_list">A:</span> Units accrue in your account up to a maximum of 28, so in order that you don't miss out on any units you should download enough classes so that your unit total stands at 18 or fewer.</p>

					<a name="9"><p><span class="red_list">Q:</span><b> My credit card was declined, but I still have units left in my account. I don't wish to continue as monthly, but I'd like to be able to access my units. Your website tells me that my account is invalid. How do I get it to work without having to pay for a new membership?</b></a></p>
					<p><span class="red_list">A:</span> We don't automatically convert accounts to Standard when a credit card is declined because once a Monthly account is cancelled a user loses access to his Personal Library. If your account is coming up as "invalid", please update your credit card information in the My Account section to re-validate your account. If you wish to cancel your account, please let us know by emailing us at <a href="mailto:cdubin@aish.com" style="display: inline; color:#6D6E71;"><b>cdubin@aish.com</b></a> , and we'll transfer your account to a Standard one that will let you access your remaining units and purchase downloads in a pay as you go basis.</p>

					<form method="post" action="http://www.salesforce.com/servlet/servlet.WebToCase?encoding=UTF-8" style="display: inline;" id="help_form">
					    <input type="hidden" value="Customer Service Request" id="subject" name="subject" />
					    <input type="hidden" value="00D7000000087ZS" name="orgid" />
					    <input type="hidden" value="http://www.aishaudio.com" name="retURL" />
					    <input type="hidden" value="1" name="external" />

					<p>Still have a question? Contact technical support using the email form below.</p><br>

					<table cellspacing="0" cellpadding="0" border="0" style="color:#6D6E71; line-height:20px;">
						<tbody>
                        	<tr>
                            	<td>Your Name:</td>
								<td><input type="text" maxlength="80" size="30" id="name" name="name" /></td>
							</tr>

							<tr>
                            	<td>Email:</td>
								<td><input type="text" maxlength="80" size="30" id="email" name="email" /></td>
							</tr>

							<tr>
                            	<td>Phone</td>
								<td><input type="text" maxlength="40" size="30" id="phone" name="phone" /></td>
							</tr>

							<tr>
								<td>Type of Problem:</td>
								<td>
									<select id="company" name="company">
									<option value="">--None--
									</option><option value="Billing">Billing
									</option><option value="Forgotten Password">Forgotten Password
									</option><option value="Download Problem">Download Problem
									</option><option value="Subscription won't go through">Subscription won't go through
									</option><option value="Other">Other</option>
									</select>
								</td>
							</tr>

							<tr>
                            	<td style="vertical-align: top;">Problem Description:</td>
								<td><textarea cols="40" rows="8" name="description"></textarea></td>
							</tr>

							<tr>
                            	<td>&nbsp;</td>
								<td><input type="image" src="<%= Url.Image("submit.jpg") %>" /></td>
							</tr>
						</tbody>
					</table>
				</form>
                    
					
                    
				</div>				
                </div>
</asp:Content>