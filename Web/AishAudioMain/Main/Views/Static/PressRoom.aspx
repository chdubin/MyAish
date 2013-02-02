<%@ Page Title="" Language="C#" MasterPageFile="../Shared/twoColumn.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">

				<div id="content_thin2">
					<img style="float: left; clear: both; margin-top: 20px;" height="36" width="549" src="<%= Url.Image("top_banner.jpg") %>" alt="AishAudio.com" />
					<div style=" border: 2px #ffcc33 solid; width: 400px; padding:25px 0 25px 0px; float: left; clear: both; margin-top: 40px; margin-left: 100px; *margin-left: 50px; text-align: center; line-height: 14px;">
						<img height="31" width="349" src="<%= Url.Image("if_you_text.jpg") %>">
						<br />
						<span style=" font-style: italic;">Aliza Dubin<br />
						<a href="mailto:mimi@aish.com">adubin@aish.com</a><br />
						(800) 864-2373</span>
					</div>
                    
					<ul id="menu_press">
						<li><a href="#release">Launch Press Release</a></li>
						<li><a href="<%= Url.RouteUrl("PressRoomAishFactsPage") %>">Aishaudio.com Facts</a>
							<ul>
								<li><a href="<%= Url.RouteUrl("PressRoomAishFactsPage") %>#Benefits">Benefits</a></li>
								<li><a href="<%= Url.RouteUrl("PressRoomAishFactsPage") %>#MP3">What is MP3 Listening?</a></li>
								<li><a href="<%= Url.RouteUrl("PressRoomAishFactsPage") %>#Voices">Voices from Jerusalem & Aish HaTorah</a></li>
								<li><a href="<%= Url.RouteUrl("PressRoomAishFactsPage") %>#Key">Key Staff</a></li>
							</ul>
						</li>
						<li><a href="/press-room-speaker-bios">Speaker Bios</a></li>
						<li><a href="/press-room-testimonials">Testimonials</a></li>
					</ul>
                 
					<img height="6" width="549" src="<%= Url.Image("blue_line.jpg") %>" style="float: left; clear: both; margin:50px 0 0 20px;">
                 
					<div style="float: left; clear: both; margin-top: 10px;">
						<p><b><a name="release"></a>For Immediate Release:</b><br />September 18, 2003</p>
						<div align="center" style="*width: 600px;"><b><span style=" display: inline-block; font-size: 18px; margin-bottom: 5px;">File Swappers Will Not Be Prosecuted</span><br />	  
						<i>Aishaudio.com launches September 18, 2003 featuring Jewish digital audio classes</i></b></div><br />
						<p>(Hollywood, FL) - www.aishaudio.com debuts Thursday, September 18, 2003 offering instant access to hundreds of digital audio classes by more than 30 of the great Jewish minds from around the world.   This is the first Jewish site dedicated to digital audio downloads.  New titles will be added weekly for visitors to listen anytime, anywhere by live streaming over the computer or by downloading and playing classes on their computer, MP3 player, or burned onto a CD.</p>
						<p>In time for the Jewish New Year that begins Friday evening September 26, aishaudio.com is launching an introduction to the site via an audio New Year's e-card with a 2-minute insight into Rosh Hashanah from Rabbi Yitzchak Berkowitz, scholar and renowned speaker.  www.aishaudio.com.</p>
						<p>Listeners are invited to hear a complete selection of classes on the High Holidays from aishaudio's extensive library on holidays, spirituality, relationships, the best selling "48 Ways" and much more.   "Is There a God and Does It Matter?", "Dealing with Anger: Loving the Unlovable" and "Pleasure: The Ultimate Energizer" are just a few of the classes from such engaging speakers as Rabbi Noah Weinberg, Rabbi Motty Berger, Rebbitzen Tziporah Heller, Rabbi Yitzchak Berkowitz and Dr. David Luchins.</p>
						<p>"People are becoming more accustomed to downloading their music and entertainment on-line. The next wave is downloading wisdom for living, happiness and spirituality," said Rabbi Aaron Dayan, creator of aishaudio and a long-time creative innovator in audio adult Jewish education.  "With aishaudio.com, people can access and hear the best speakers on whatever issue they want, whenever and wherever they happen to be, night or day."</p>
						<p>Rather than bring lawsuits against users who swap the files that they download from the site, aishaudio.com will rely on an admonition to its customers that file-swapping or retrieving the free streamed files from cache is against Jewish law.   When asked if he's worried about file swapping,  Rabbi Dayan responded, "Anyone who would steal Torah classes must really need the wisdom and moral understandings. Hopefully,  they will put the information to good use."</p>
						<p>To introduce visitors to the site, aishaudio.com is offering free online listening for half of its catalog.  Subscribers to the service receive the first month free.  Annual members receive a free MP3 player (value $120), 120 download units and the best selling "48 Ways to Wisdom" in MP3 format.  This package  - worth $419 -  is offered at an introductory $168.  Individual audio class downloads are available for $3.50 or $5.00.</p>
						<p>Aishaudio.com is a project of Voices from Jerusalem, a leading producer and distributor of Jewish content tapes and CDs with a library of 11,000 titles and 750,000 tapes sold-to-date.  Voices is a division of Aish HaTorah, the innovative adult Jewish education organization with 30 branches around the world and headquarters overlooking the Western Wall.  More than 100,000 people learn with Aish every year, and its award winning website, aish.com, welcomes 1 million visitors each month.</p>
						<p><a href="http://aishaudio.com">www.aishaudio.com</a><br /><br />
						<b>Contact:</b> Aliza Dubin <br />
						(800) VOICES3 <br />
						<a href="mailto:adubin@aish.com">adubin@aish.com</a><br />
						<br />
						<span>USA:</span> 28 Park Avenue, Monsey, NY 10952<br />
						Toll Free: 1-800-VOICES-3<br /><br /><br />
						<span>Israel:</span> POB 14149, Jerusalem, Israel<br />
						Tel: (02) 628-5666 ext. 232<br />
						Fax: (02) 627-3172
						</p>
					</div>
				</div>				


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="title" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
</asp:Content>
