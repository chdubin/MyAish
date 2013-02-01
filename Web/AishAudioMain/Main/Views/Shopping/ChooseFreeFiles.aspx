<%@ Page Title="" Language="C#" MasterPageFile="../Shared/twoColumn.Master" Inherits="System.Web.Mvc.ViewPage<string>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
<br /><br />Thank you, an email has been sent to <%=Model %>.
<p>Please click on the link in that email to get your two free downloads.</p>
<p>If you do not receive the verification email within 2 minutes, please <a href="mailto:cdubin@aish.com?subject=I did not receive my login information">click here</a>.</p>
<br /><br /><br /><div align="center"><img src="<%=Url.Image("wisdom_for_living_315x80.gif") %>" /></div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="title" runat="server">
Jewish mp3 audio tape downloads. Torah mp3s of Judaism - Free streaming.
</asp:Content>

