<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Main.Models.Shopping.FreeOfferUnauthorizeModel>" %>
	<table cellpadding="0" cellspacing="0" border="0">
	<tr><td colspan="2" height="20" valign="top" class="bluebolditalictxt">We need your email address to verify that you are a real user.</td></tr>
	<tr><td width="10%"><%=Html.TextBoxFor(m=>m.Email, new { size = "25" })%>&nbsp;&nbsp;&nbsp;</td></tr>
	</table>
	<div class="email_explain">Your email address will remain private.</div>
	<div style="padding-top:10px;" class="blueboldtextsmall">Promotional Code:&nbsp;&nbsp;&nbsp;<%=Html.TextBoxFor(m=>m.PromoCode)%></div><br />
	<div class="bluebolditalictxt">After you submit, you will receive a verification email that will link to your 2 free downloads.</div><br />
	<div class="bluebolditalictxt"><input type="image" src="<%=Url.Image("submit.jpg") %>"/></div>

