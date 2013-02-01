<%@ Page Title="" Language="C#" MasterPageFile="../Shared/oneColumnEmpty.Master" Inherits="System.Web.Mvc.ViewPage<Main.Models.Account.RegisterCustomModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">

<br/><br/>

<div>
    A verification email has been sent to your email account.<br/>
    Please go to your inbox, open the email and click on the enclosed link to continue the registration process.<br/><br/>
    <p>
        If for some reason the link doesn't work, please manually enter the password included in the email in the space 
        provided <a href="<%= Url.Action("RegisterCustomMembershipValidation", "Account", new { email = Model.Email, first_name = Model.FirstName, last_name = Model.LastName } ) %>">here</a>.         
    </p><br/>
    <p>
        If you do not receive the verification email within 2 minutes, 
        please <a href="mailto:cdubin@aish.com?subject=I did not receive my login information">click here</a>. <br/><br/>
	</p>
    <br/><br/><br/><br/>
</div>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="title" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
</asp:Content>
